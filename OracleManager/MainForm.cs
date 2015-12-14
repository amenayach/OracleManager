using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OracleManager
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            //AmenControls.TextBoxHinter.AddPlaceholder(tbSearch, "Search (F3)");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                var sInput = OracleConnectorDialog.Connect(OracleHelper.constr); // ControlMod.InputBox("Enter connection string", "Enter connection string", OracleHelper.constr);
                if (sInput.NotEmpty())
                {
                    OracleHelper.constr = sInput;
                    using (var con = new System.Data.OracleClient.OracleConnection(OracleHelper.constr))
                    {
                        var owners = DBHelper.ExecuteQuery<string>(OracleHelper.owners, con).Select(o => new { id = o }).ToList();
                        ControlMod.FillCombo(cmbData, owners, "id", "id");
                    }
                }
            }
            catch (Exception ex)
            {
                ex.PromptMsg();
            }
        }

        private void Search()
        {
            try
            {
                if (cmbData.Text.NotEmpty())
                {
                    using (var con = new System.Data.OracleClient.OracleConnection(OracleHelper.constr))
                    {
                        var s = tbSearch.Text.NullTrimer().ToLower();
                        var tables = DBHelper.ExecuteQuery<string>(OracleHelper.tables.Replace("?", cmbData.Text), con)
                                                         .Where(o => s.IsEmpty() || o.NullTrimer().ToLower().Contains(s)).ToList();
                        lstObjects.Items.Clear();
                        lstObjects.Items.AddRange(tables.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                ex.PromptMsg();
            }
        }

        private void MainForm_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.F5:
                        Exec();
                        break;
                    case Keys.F3:
                        tbSearch.Focus();
                        break;
                    default:
                        break;
                }
            }
            catch //(Exception)
            {
            }
        }

        private void Exec()
        {
            try
            {
                if (OracleHelper.constr.NotEmpty())
                {
                    if (tbScript.Text.NotEmpty())
                    {
                        var data = OracleHelper.GetDatatable(tbScript.Text);
                        grd.DataSource = null;
                        grd.DataSource = data;
                        if (data.NotEmpty())
                        {
                            lblCount.Text = data.Rows.Count.ToString() + " row" + (data.Rows.Count > 1 ? "s" : "");
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
        }

        private void cmbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void lstObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (lstObjects.SelectedIndex >= 0 && lstObjects.SelectedItem != null)
                {
                    tbScript.Text = "SELECT * FROM " + lstObjects.SelectedItem.ToString() + " WHERE ROWNUM <= " + (numRowLimit.Text);
                }
            }
            catch (Exception ex)
            {
                ex.PromptMsg();
            }
        }

        private void tbSearch_TextChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void tbSearch_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Down) lstObjects.Focus();
            }
            catch { }
        }

        private void lstObjects_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Up && lstObjects.Items.Count > 0 && lstObjects.SelectedIndex == 0) tbSearch.Focus();
            }
            catch { }
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            try
            {
                var lastConStr = ControlMod.GetRegistryValue(ControlMod.cntConStr, "").AsString();
                if (lastConStr.NotEmpty())
                {
                    OracleHelper.constr = lastConStr;
                }
            }
            catch (Exception ex)
            {
                ex.PromptMsg();
            }
        }
    }
}
