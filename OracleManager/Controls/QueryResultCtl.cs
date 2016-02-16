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
