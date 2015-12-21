﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using OracleManager.Controls;
//using Oracle.DataAccess.Client;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;
using Oracle.ManagedDataAccess.Types;

namespace OracleManager
{
    public partial class MainForm : Form
    {
        private bool DisableListboxSelection = false;

        public MainForm()
        {
            InitializeComponent();
            FillTab(tab.TabPages[0]);
            //AmenControls.TextBoxHinter.AddPlaceholder(tbSearch, "Search (F3)");
        }

        private void btnConnect_Click(object sender, EventArgs e)
        {
            try
            {
                var sInput = OracleConnectorDialog.Connect(OracleHelper.constr); // ControlMod.InputBox("Enter connection string", "Enter connection string", OracleHelper.constr);
                if (sInput.NotEmpty())
                {
                    DoWait(true);
                    OracleHelper.constr = sInput;
                    using (var con = new OracleConnection(OracleHelper.constr))
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
            DoWait(false);
        }

        private void Search()
        {
            try
            {
                if (cmbData.Text.NotEmpty())
                {
                    using (var con = new OracleConnection(OracleHelper.constr))
                    {
                        DoWait(true);
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
            DoWait(false);
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
                DoWait(true);
                var tb = tab.SelectedTab;
                if (tb != null && tb.Controls.Count > 0)
                {
                    var qr = tb.Controls.Find("qr", true)[0] as Controls.QueryResultCtl;
                    qr.ExecQuery();
                }
            }
            catch (Exception ex)
            {
                ex.PromptMsg();
            }
            DoWait(false);
        }

        private void btnExec_Click(object sender, EventArgs e)
        {
            Exec();
        }

        private void cmbData_SelectedIndexChanged(object sender, EventArgs e)
        {
            Search();
        }

        private void lstObjects_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DisableListboxSelection) return;
            try
            {
                if (lstObjects.SelectedIndex >= 0 && lstObjects.SelectedItem != null && tab.SelectedTab != null && tab.SelectedTab.Controls.Count > 0)
                {
                    var qr = tab.SelectedTab.Controls.Find("qr", true)[0] as QueryResultCtl;
                    if (qr != null)
                    {
                        qr.SetText("SELECT * FROM " + lstObjects.SelectedItem.ToString() + " WHERE ROWNUM <= " + (numRowLimit.Text));
                    }
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

        private void tab_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (tab.SelectedTab != null) FillTab(tab.SelectedTab);
        }

        private void FillTab(TabPage tbp)
        {
            try
            {
                if (tbp != null && tbp.Controls.Count == 0)
                {
                    var qr = new Controls.QueryResultCtl() { Name = "qr", Dock = DockStyle.Fill };
                    qr.OnExecutionDone += (object sender1, EventArgs e1) =>
                    {
                        var __count = ((Controls.QueryResultCtl)sender1).Count;
                        lblCount.Text = __count.ToString() + " row" + (__count > 1 ? "s" : "");
                    };
                    tbp.Controls.Add(qr);
                    tbp.Text = "Query" + tab.TabPages.Count + "  X";
                    tab.TabPages.Add("k" + tab.TabCount, "+");
                }
            }
            catch (Exception ex)
            {
                ex.PromptMsg();
            }
        }

        private void tab_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                for (int i = 0; i < this.tab.TabPages.Count; i++)
                {
                    Rectangle r = tab.GetTabRect(i);
                    //Getting the position of the "x" mark.
                    Rectangle closeButton = new Rectangle(r.Right - 15, r.Top + 4, 9, 7);
                    if (closeButton.Contains(e.Location))
                    {
                        if (!tab.TabPages[i].Text.Contains("+") && MessageBox.Show("Would you like to Close this Tab?", "Confirm", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            this.tab.TabPages.RemoveAt(i);
                            break;
                        }
                    }
                }
            }
            catch //(Exception)
            {

            }
        }

        private void DoWait(bool wait)
        {
            this.Controls.Add(lblWait);
            lblWait.BringToFront();
            ControlMod.CenterControl(lblWait, true);
            lblWait.Visible = wait;
            Application.DoEvents();
        }

        private void schemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var newTab = tab.TabPages[tab.TabPages.Count - 1];
            FillTab(newTab);
            tab.SelectedTab = newTab;
            var qr = newTab.Controls.Find("qr", true)[0] as QueryResultCtl;
            if (sender.Equals(schemaToolStripMenuItem))//Schema
            {
                qr.SetText(@"SELECT table_name, column_name, data_type, data_length
                                FROM USER_TAB_COLUMNS
                                WHERE table_name = '" + lstObjects.SelectedItem.ToString() + @"'");
                qr.ExecQuery();
            }
            else if (sender.Equals(openWithAllFieldsToolStripMenuItem))// Open with all fields
            {
                var fields = OracleHelper.GetDatatable(@"SELECT column_name
                                FROM USER_TAB_COLUMNS
                                WHERE table_name = '" + lstObjects.SelectedItem.ToString() + @"'");
                if (fields.NotEmpty())
                {
                    var sCols = fields.Rows.Cast<DataRow>().Select(o => o[0]).Aggregate((f1, f2) => f1 + ", " + f2);
                    qr.SetText(String.Format(@"SELECT {0} FROM {1}  WHERE ROWNUM <= {2}", sCols, lstObjects.SelectedItem.ToString(), numRowLimit.Text));
                    qr.ExecQuery();
                }
            }
            else if (sender.Equals(generateCClassToolStripMenuItem))// Generate C# class
            {
                var data = OracleHelper.GetDatatable(String.Format(@"SELECT * FROM {0} WHERE 0=1", lstObjects.SelectedItem.ToString()));
                var __s = ClassGenerater.GetCSharpClass(data, lstObjects.SelectedItem.ToString());
                qr.SetText(__s);
                qr.HidePanel2();
            }
        }

        private void lstObjects_MouseDown(object sender, MouseEventArgs e)
        {
            try
            {
                if (e.Button != MouseButtons.Right) return;
                DisableListboxSelection = true;
                var index = lstObjects.IndexFromPoint(e.Location);
                if (index != ListBox.NoMatches)
                {
                    lstObjects.SelectedIndex = index;// lstObjects.Items[index].ToString();
                    contextMenuStrip1.Show(Cursor.Position);
                    contextMenuStrip1.Visible = true;
                }
                else
                {
                    contextMenuStrip1.Visible = false;
                }
            }
            catch { }
            DisableListboxSelection = false;
        }

    }
}
