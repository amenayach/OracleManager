using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OracleManager.Controls
{
    public partial class QueryResultCtl : UserControl
    {
        private bool DisableExec = false;
        private string LastOutputParam = string.Empty;
        public event EventHandler OnExecutionDone;

        public int Count
        {
            get { return grd.RowCount; }
        }


        public QueryResultCtl()
        {
            InitializeComponent();
        }

        public int ExecQuery()
        {
            try
            {
                if (DisableExec) return 0;
                if (OracleHelper.constr.NotEmpty())
                {
                    if (tbScript.Text.NotEmpty())
                    {
                        var data = OracleHelper.GetDatatable(tbScript.Text);
                        grd.DataSource = null;
                        grd.DataSource = data;
                        if (data.NotEmpty())
                        {
                            if (OnExecutionDone != null) OnExecutionDone(this, new EventArgs());
                        }
                    }
                }
                else
                {
                    ControlMod.PromptMsg("Please connect to an Oracle data first !");
                }
            }
            catch (Exception ex)
            {
                ex.PromptMsg();
            }
            return grd.RowCount;
        }

        public int ExecQueryForDotNetTypes()
        {
            try
            {
                if (DisableExec) return 0;
                if (OracleHelper.constr.NotEmpty())
                {
                    if (tbScript.Text.NotEmpty())
                    {
                        var data = OracleHelper.GetDatatable(tbScript.Text);
                        grd.DataSource = null;
                        grd.DataSource = data.Columns.Cast<DataColumn>().Select(col => new { ColumnName = col.ColumnName, Type = col.DataType, MaxLength = col.MaxLength }).ToList();
                        if (data.NotEmpty())
                        {
                            if (OnExecutionDone != null) OnExecutionDone(this, new EventArgs());
                        }
                    }
                }
                else
                {
                    ControlMod.PromptMsg("Please connect to an Oracle data first !");
                }
            }
            catch (Exception ex)
            {
                ex.PromptMsg();
            }
            return grd.ColumnCount;
        }

        public int ExecProc()
        {
            try
            {
                if (DisableExec) return 0;
                if (OracleHelper.constr.NotEmpty())
                {
                    if (tbScript.Text.NotEmpty())
                    {
                        LastOutputParam = ControlMod.InputBox("", "Enter output param", LastOutputParam);
                        var data = OracleHelper.ExecuteFunctionOrProcedure(tbScript.Text, LastOutputParam);
                        var dt = new DataTable();
                        dt.Columns.Add(new DataColumn(LastOutputParam, typeof(string)));
                        var row = dt.NewRow();
                        row[0] = data;
                        grd.DataSource = null;
                        grd.DataSource = dt;
                        if (OnExecutionDone != null) OnExecutionDone(this, new EventArgs());
                    }
                }
                else
                {
                    ControlMod.PromptMsg("Please connect to an Oracle data first !");
                }
            }
            catch (Exception ex)
            {
                ex.PromptMsg();
            }
            return grd.RowCount;
        }

        public void ExtractViewsAsTables()
        {
            try
            {
                if (DisableExec) return;
                if (OracleHelper.constr.NotEmpty())
                {

                    var viewsSplittedBySemiColumn = tbScript.Text;

                    StringBuilder sb = new StringBuilder();

                    var views = //"adasdasd;XXMOB_LEAVE_TYPES_V;XXMOB_LEAVE_APP_HIST_V;XXMOB_CUSTOM_ATTACHMENTS_V;XXMOB_LEAVES_WITH_STATUS_V"
                        //"xxmob_currencies_v;xxmob_emp_po_access_v;xxmob_gl_codes_v;xxmob_po_headers_v;xxmob_po_lines_v;xxenec_ap_payment_terms_v;xxenec_hr_dept_v;xxenec_ou_names_v;xxenec_po_categories_v;xxenec_po_inv_locations_v;xxenec_po_line_types_v;xxmob_po_receipt_lines_v;xxenec_po_pr_req_types_v;xxenec_po_price_types_v;xxenec_po_shipto_loc_v;xxenec_po_types_v;xxenec_po_uom_types_v;xxenec_sup_sites_v;xxenec_suppliers_v;xxmob_purchase_action_hist_v;xxmob_purchase_notfy_v;xxmob_pr_headers_v;xxmob_pr_lines_v;xxmob_po_release_headers_v;xxmob_po_release_lines_v;xxmob_po_receipts_v"
                        viewsSplittedBySemiColumn
                            .Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string view in views)
                    {
                        try
                        {

                            var fields =
                                OracleHelper.GetDatatable(string.Format(
                                    @"SELECT table_name view_name, column_name, data_type, nvl(DATA_LENGTH,0) fld_length, NULLABLE
                                          FROM all_tab_columns
                                         WHERE upper(table_name) = upper('{0}')
                                         ORDER BY column_id", view));

                            if (fields != null && fields.Rows.Count > 0)
                            {

                                sb.AppendLine(@"-------------------------- " + view + " --------------------------");
                                sb.AppendLine(@"CREATE TABLE apps." + view + " (");

                                fields.Rows.Cast<DataRow>().ToList().ForEach(
                                    row =>
                                        sb.AppendLine("  " +
                                                      row["column_name"].ToString() + " " +
                                                      row["data_type"].ToString() + " " +
                                                      (int.Parse(row["fld_length"].ToString()) == 0 || row["data_type"].ToString().ToLower() == "date"
                                                          ? ""
                                                          : "(" + row["fld_length"].ToString() + ")") +
                                                      (row.Equals(fields.Rows[fields.Rows.Count - 1]) ? "  " : ",  ") +
                                                      (row["NULLABLE"].ToString().ToUpper() == "Y" ? "-- NULLABLE" : ""))
                                    );

                                sb.AppendLine(@");");
                                sb.AppendLine(@"");
                            }
                            else
                            {
                                sb.AppendLine("-- " + view + " not found\n\n");
                            }
                        }
                        catch (Exception ex)
                        {
                            sb.AppendLine("/* Error >> " + ex.Message + "\n" + ex.StackTrace + "*/");
                        }
                    }

                    System.Windows.Forms.Clipboard.SetText(sb.ToString());

                }
                else
                {
                    ControlMod.PromptMsg("Please connect to an Oracle data first !");
                }
            }
            catch (Exception ex)
            {
                ex.PromptMsg();
            }
        }

        public void ExtractDataAsInsertScript(int rowLimit)
        {
            try
            {
                if (DisableExec) return;
                if (OracleHelper.constr.NotEmpty())
                {

                    var viewsSplittedBySemiColumn = tbScript.Text;

                    StringBuilder sb = new StringBuilder();

                    var views = //"adasdasd;XXMOB_LEAVE_TYPES_V;XXMOB_LEAVE_APP_HIST_V;XXMOB_CUSTOM_ATTACHMENTS_V;XXMOB_LEAVES_WITH_STATUS_V"
                        //"xxmob_currencies_v;xxmob_emp_po_access_v;xxmob_gl_codes_v;xxmob_po_headers_v;xxmob_po_lines_v;xxenec_ap_payment_terms_v;xxenec_hr_dept_v;xxenec_ou_names_v;xxenec_po_categories_v;xxenec_po_inv_locations_v;xxenec_po_line_types_v;xxmob_po_receipt_lines_v;xxenec_po_pr_req_types_v;xxenec_po_price_types_v;xxenec_po_shipto_loc_v;xxenec_po_types_v;xxenec_po_uom_types_v;xxenec_sup_sites_v;xxenec_suppliers_v;xxmob_purchase_action_hist_v;xxmob_purchase_notfy_v;xxmob_pr_headers_v;xxmob_pr_lines_v;xxmob_po_release_headers_v;xxmob_po_release_lines_v;xxmob_po_receipts_v"
                        viewsSplittedBySemiColumn
                            .Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries);

                    foreach (string view in views)
                    {
                        try
                        {

                            var data =
                                OracleHelper.GetDatatable(string.Format(
                                @"SELECT * FROM {0} {1}", view, (rowLimit > 0 ? " WHERE ROWNUM <= " + rowLimit : "")));

                            if (data != null && data.Rows.Count > 0)
                            {

                                foreach (DataRow row in data.Rows.Cast<DataRow>())
                                {

                                    var fieldNames = "(";
                                    var fieldValues = " VALUES (";

                                    foreach (DataColumn column in data.Columns)
                                    {

                                        if (column.DataType != typeof(byte[]))
                                        {

                                            var firstCol = fieldNames != "(";

                                            fieldNames += (firstCol ? ", " : " ") + column.ColumnName;

                                            fieldValues += (firstCol ? ", " : " ") +

                                                (column.DataType == typeof(DateTime) && !DBNull.Value.Equals(row[column]) ?
                                                "'" + ((DateTime)row[column]).ToString("dd-MMM-yyyy") + "'" : "'" + row[column].ToString() + "'");

                                        }

                                    }

                                    if (fieldNames != "(")
                                    {

                                        sb.AppendLine(@"-------------------------- " + view + " --------------------------");
                                        sb.AppendLine(@"INSERT INTO " + view + " ");
                                        sb.AppendLine(fieldNames + ")");
                                        sb.AppendLine(fieldValues + ");" + Environment.NewLine);

                                    }

                                }

                            }
                            else
                            {
                                sb.AppendLine("-- " + view + " not found\n\n");
                            }
                        }
                        catch (Exception ex)
                        {
                            sb.AppendLine("/* Error >> " + ex.Message + "\n" + ex.StackTrace + "*/");
                        }
                    }

                    System.Windows.Forms.Clipboard.SetText(sb.ToString());

                }
                else
                {
                    ControlMod.PromptMsg("Please connect to an Oracle data first !");
                }
            }
            catch (Exception ex)
            {
                ex.PromptMsg();
            }
        }

        public void SetText(string sText)
        {
            tbScript.Text = sText;
        }

        public void HidePanel2()
        {
            splitContainer2.Panel2.Hide();
            splitContainer2.SplitterDistance = splitContainer2.Height;
            DisableExec = true;
        }
    }
}
