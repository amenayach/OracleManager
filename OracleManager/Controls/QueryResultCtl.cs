using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using OracleManager.Classes;

namespace OracleManager.Controls
{
    public partial class QueryResultCtl : UserControl
    {
        private DateTime _execStarTime;
        private bool _disableExec = false;
        private string _lastOutputParam = string.Empty;
        public event EventHandler<ExecDoneEventArgs> OnExecutionDone;

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
                if (_disableExec) return 0;
                if (OracleHelper.constr.NotEmpty())
                {
                    if (tbScript.Text.NotEmpty())
                    {
                        _execStarTime = DateTime.Now;

                        var script = tbScript.Text;

                        Async.GetDataAsync<DataTable>(() =>
                        {
                            var data = OracleHelper.GetDatatable(script);
                            return data;
                        }, (DataTable data) =>
                        {
                            grd.DataSource = null;
                            grd.DataSource = data;
                            if (data.NotEmpty())
                            {
                                var delta = (DateTime.Now - _execStarTime);
                                if (OnExecutionDone != null) OnExecutionDone(this, new ExecDoneEventArgs() { ExecTime = delta.TotalMilliseconds });
                            }
                        });
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
                if (_disableExec) return 0;
                if (OracleHelper.constr.NotEmpty())
                {
                    if (tbScript.Text.NotEmpty())
                    {

                        _execStarTime = DateTime.Now;
                        
                        var data = OracleHelper.GetDatatable(tbScript.Text);
                        grd.DataSource = null;
                        grd.DataSource = data.Columns.Cast<DataColumn>().Select(col => new { ColumnName = col.ColumnName, Type = col.DataType, MaxLength = col.MaxLength }).OrderBy(col => col.ColumnName).ToList();

                        if (grd.Columns.Count > 0)
                        {
                            grd.Columns[0].Width = 230;
                        }

                        if (data.NotEmpty())
                        {
                            if (OnExecutionDone != null) OnExecutionDone(this, new ExecDoneEventArgs() { ExecTime = (_execStarTime - DateTime.Now).Milliseconds });
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
                if (_disableExec) return 0;

                _execStarTime = DateTime.Now;
                
                if (OracleHelper.constr.NotEmpty())
                {
                    if (tbScript.Text.NotEmpty())
                    {
                        _lastOutputParam = ControlMod.InputBox("", "Enter output param", _lastOutputParam);
                        var data = OracleHelper.ExecuteFunctionOrProcedure(tbScript.Text, _lastOutputParam);
                        var dt = new DataTable();
                        dt.Columns.Add(new DataColumn(_lastOutputParam, typeof(string)));
                        var row = dt.NewRow();
                        row[0] = data;
                        grd.DataSource = null;
                        grd.DataSource = dt;
                        if (OnExecutionDone != null) OnExecutionDone(this, new ExecDoneEventArgs() { ExecTime = (_execStarTime - DateTime.Now).Milliseconds });
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

        public void ExecuteBulk()
        {
            try
            {
                if (_disableExec) return;
                if (OracleHelper.constr.NotEmpty())
                {
                    if (tbScript.Text.NotEmpty())
                    {
                        _execStarTime = DateTime.Now;

                        var errorMessage = string.Empty;

                        var success = OracleHelper.ExecuteBulk(tbScript.Text, ref errorMessage);

                        var dt = new DataTable();
                        dt.Columns.Add(new DataColumn("Result", typeof(string)));
                        var row = dt.NewRow();
                        row[0] = success ? "Done" : errorMessage;
                        dt.Rows.Add(row);

                        grd.DataSource = null;
                        grd.DataSource = dt;

                        if (OnExecutionDone != null) OnExecutionDone(this, new ExecDoneEventArgs() { ExecTime = (_execStarTime - DateTime.Now).Milliseconds });

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
        }

        public void ExtractViewsAsTables()
        {
            try
            {
                if (_disableExec) return;
                if (OracleHelper.constr.NotEmpty())
                {

                    var viewsSplittedBySemiColumn = tbScript.Text;

                    StringBuilder sb = new StringBuilder();

                    var views = viewsSplittedBySemiColumn.Split(new string[] { ";" }, StringSplitOptions.RemoveEmptyEntries).Select(lambView => lambView.NullTrimer()).ToList();

                    //handles the splitted by comma case
                    if (views.NotEmpty() && views.Count == 1 && viewsSplittedBySemiColumn.Contains(","))
                    {
                        views = viewsSplittedBySemiColumn.Split(new string[] { "," }, StringSplitOptions.RemoveEmptyEntries).Select(lambView => lambView.NullTrimer()).ToList();
                    }

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
                                                      (int.Parse(row["fld_length"].ToString()) == 0 || row["data_type"].ToString().ToLower() == "date" || row["data_type"].ToString().ToLower() == "blob"
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
                if (_disableExec) return;
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
                                                "'" + ((DateTime)row[column]).ToString("dd-MMM-yyyy") + "'" : "'" + row[column].ToString().Replace("'", "''").Replace("&", "and") + "'");

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

        public string GetText()
        {
            return tbScript.Text;
        }

        public void HidePanel2()
        {
            splitContainer2.Panel2.Hide();
            splitContainer2.SplitterDistance = splitContainer2.Height;
            _disableExec = true;
        }

        private void grd_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {
            ControlMod.SaveLog(e.Exception);
            e.Cancel = true;
        }

        internal bool DoFind()
        {

            if (!tbSearch.Visible)
            {
                tbSearch.Show();
                tbSearch.Focus();
                tbSearch.SelectAll();
                return false;
            }

            tbSearch.Focus();

            var searchKeyword = tbSearch.Text;

            if (grd.Rows.Count == 0 || string.IsNullOrEmpty(searchKeyword))
            {
                return false;
            }

            if (grd.CurrentCell == null)
            {
                grd.CurrentCell = grd.Rows[0].Cells[0];
            }

            for (int i = grd.CurrentCell.RowIndex; i < grd.RowCount; i++)
            {

                var row = grd.Rows[i];

                for (int j = 0; j < grd.ColumnCount; j++)
                {

                    if (grd.CurrentCell.RowIndex == i && grd.CurrentCell.ColumnIndex <= j)
                    {
                        continue;
                    }

                    var cellValue = row.Cells[j].Value == null || DBNull.Value.Equals(row.Cells[j].Value) ? "" : row.Cells[j].Value.ToString();

                    if (!string.IsNullOrEmpty(cellValue) && cellValue.ToLower().Contains(searchKeyword.ToLower()))
                    {

                        grd.CurrentCell = grd.Rows[i].Cells[j];
                        grd.FirstDisplayedScrollingRowIndex = i;
                        return true;

                    }

                }
            }

            if (grd.Rows.Count > 0)
            {
                grd.CurrentCell = grd.Rows[0].Cells[0];
            }

            return false;
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                DoFind();
            }
            else if (e.KeyCode == Keys.Escape)
            {
                tbSearch.Hide();
            }
        }

    }

    public class ExecDoneEventArgs : EventArgs
    {

        /// <summary>
        /// Rerpresents the total execution time in milliseconds
        /// </summary>
        public double ExecTime { get; set; }

    }
}
