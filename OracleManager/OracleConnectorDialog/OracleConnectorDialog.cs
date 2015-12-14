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
    public partial class OracleConnectorDialog : Form
    {

        private string SelectedConnectionString = "";

        public OracleConnectorDialog()
        {
            InitializeComponent();
        }

        public static string Connect(string InitialConStr = "")
        {
            try
            {
                var frm = new OracleConnectorDialog();
                if (InitialConStr.IsEmpty())
                {
                    frm.rdBasic.Checked = true;
                }
                else
                {
                    frm.rdConStr.Checked = true;
                    frm.pnlBasic.Enabled = false;
                    frm.pnlConStr.Enabled = true;
                    frm.tbFullConStr.Text = InitialConStr;
                }

                var dlgRes = frm.ShowDialog();
                return frm.SelectedConnectionString;
            }
            catch (Exception ex)
            {
                ControlMod.PromptMsg(ex);
            }
            return "";
        }
    
        /*========================================*/

        private void OracleConnectorDialog_Load(object sender, EventArgs e)
        {
            cmbProtocol.SelectedIndex = 0;
        }
    
        private void OracleConnectorDialog_KeyDown(object sender, KeyEventArgs e)
        {
            try
            {
                switch (e.KeyCode)
                {
                    case Keys.Escape:
                        btnCancel.PerformClick();
                        break;
                    case Keys.Enter:
                        btnConnect.PerformClick();
                        break;
                    default:
                        break;
                }
            }
            catch { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.None;
            this.Close();
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                if (rdBasic.Checked)
                {
                    if (tbUsername.Text.NullTrimer().NotEmpty() && tbPassword.Text.NotEmpty() && cmbProtocol.Text.NotEmpty() &&
                         tbHost.Text.NotEmpty() && tbPort.Text.NotEmpty() && tbSID.Text.NotEmpty())
                    {
                        var __conStr = String.Format(@"user id={0};password={1};Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL={2})
                                            (HOST={3})(PORT={4})))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME={5})))",
                                                tbUsername.Text.NullTrimer(), tbPassword.Text, cmbProtocol.Text,
                                                tbHost.Text, tbPort.Text, tbSID.Text);

                        this.SelectedConnectionString = __conStr;
                        this.DialogResult = System.Windows.Forms.DialogResult.OK;
                        this.Close();
                    }
                }
                else if(tbFullConStr.Text.NotEmpty())
                {
                    this.SelectedConnectionString = tbFullConStr.Text;
                    DialogResult = System.Windows.Forms.DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ControlMod.PromptMsg(ex);
            }
        }

        private void rdBasic_CheckedChanged(object sender, EventArgs e)
        {
            if (rdBasic.Checked)
            {
                pnlBasic.Enabled = true;
                pnlConStr.Enabled = false;
            }
            else
            {
                pnlConStr.Enabled = true;
                pnlBasic.Enabled = false;
            }
        }

    }
}
