using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Collections;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Drawing2D;
//By amen ayach
using System.IO;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Microsoft.Win32;
using System.Text.RegularExpressions;

public static class ControlMod
{
    public static DialogResult PromptMsg(this Exception ex)
    {
        DialogResult res = DialogResult.None;
        try
        {
            if (ex != null)
            {
                res = MessageBox.Show(ex.Message);
            }
        }
        catch (Exception)
        {
            SaveLog(ex);
        }
        return res;
    }

    public static void SaveLog(Exception ex)
    {
        try
        {
            if (ex != null)
            {
                string s = "Exception @" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\nMsg: " +
                                ex.Message + "\nStackTrace: " + ex.StackTrace;
                System.IO.File.AppendAllText(System.IO.Path.Combine(Application.StartupPath, "Log" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt"), s);
            }
        }
        catch (Exception)
        {

        }
    }

    public static void SaveLog(string sLog)
    {
        try
        {
            if (!string.IsNullOrEmpty(sLog))
            {
                string s = "Log @" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\n " + sLog;
                System.IO.File.WriteAllText(System.IO.Path.Combine(Application.StartupPath, "Log" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt"), s);
            }
        }
        catch (Exception)
        {

        }
    }

    //==================================================================

    [DllImport("user32", CharSet = CharSet.Ansi, SetLastError = true, ExactSpelling = true)]

    #region "KeyLogger"

    public static extern int GetAsyncKeyState(long vKey);

    #endregion

    #region "Lists"

    public static List<T> Except<T>(this List<T> L, Func<T, bool> fnc)
    {
        List<T> res = new List<T>();
        foreach (var item_loopVariable in L)
        {
            var item = item_loopVariable;
            if (!fnc(item))
                res.Add(item);
        }
        return res;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T">System types like integer, boolean ...</typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    /// <remarks></remarks>
    public static List<T> CObject<T>(List<object> obj)
    {
        List<T> L = new List<T>();
        for (int i = 0; i <= obj.Count - 1; i++)
        {
            L.Add((T)obj[i]);
        }
        return L;
    }

    /// <summary>
    /// Return true if (L Is Nothing OrElse L.Count = 0)
    /// </summary>
    public static bool IsEmpty(this IList L)
    {
        bool res = false;
        try
        {
            res = (L == null || L.Count == 0);
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
        return res;
    }

    /// <summary>
    /// Return true if (L Is Nothing OrElse L.Count = 0)
    /// </summary>
    public static bool NotEmpty(this IList L)
    {
        bool res = false;
        try
        {
            res = !(L == null || L.Count == 0);
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
        return res;
    }

    public static bool NotEmpty(this DataTable L)
    {
        bool res = false;
        try
        {
            res = !(L == null || L.Rows.Count == 0);
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
        return res;
    }

    public static bool IsEmpty(this DataTable L)
    {
        bool res = false;
        try
        {
            res = (L == null || L.Rows.Count == 0);
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
        return res;
    }

    public static bool FillDataRow(this DataRow RowToFill, DataRow DataSourceRow)
    {
        bool res = false;
        try
        {
            if (RowToFill != null && DataSourceRow != null)
            {

                foreach (DataColumn item in DataSourceRow.Table.Columns)
                {
                    RowToFill[item.ColumnName] = DataSourceRow[item.ColumnName];
                }
                res = true;
      
            }
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
        return res;
    }

    #endregion

    #region "Enums"

    public static int AsIntEnum<T>(this T value) where T : struct, IConvertible
    {
        if (typeof(T).IsEnum)
        {
            return (int)(IConvertible)value;
        }
        return -1;
    }

    //public static List<iddesc> GetEnumAsList<T>()
    //{
    //    List<iddesc> res = new List<iddesc>();
    //    try
    //    {
    //        foreach (var p_loopVariable in Enum.GetValues(typeof(T)))
    //        {
    //            var p = p_loopVariable;
    //            res.Add(new iddesc
    //            {
    //                id = Convert.ToInt32(p),
    //                description = p.ToString
    //            });
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        PromptMsg(ex);
    //    }
    //    return res;
    //}

    #endregion

    #region "Devices"

    //public static Microsoft.VisualBasic.Devices.Computer GetComputer()
    //{
    //    return new Microsoft.VisualBasic.Devices.Computer();
    //}

    #endregion

    #region "Grid"

    public static void sgAll(this DataGridView grd, DataGridViewColumn Col, object value)
    {
        grd.sgAll(Col.Name, value);
    }

    public static void sgAll(this DataGridView grd, string ColName, object value)
    {
        for (int i = 0; i <= grd.RowCount - 1; i++)
        {
            grd.Rows[i].Cells[ColName].Value = value;
        }
    }

    public static void sg(this DataGridView grd, int rowIndex, DataGridViewColumn Col, object value)
    {
        grd.Rows[rowIndex].Cells[Col.Name].Value = value;
    }

    public static object gg(this DataGridView grd, int rowIndex, DataGridViewColumn Col)
    {
        return grd.Rows[rowIndex].Cells[Col.Name].Value;
    }

    public static object gg(this DataGridView grd, DataGridViewRow row, DataGridViewColumn Col)
    {
        return grd.Rows[row.Index].Cells[Col.Name].Value;
    }

    public static object GetSelectedRowValue(this DataGridView grd, int SelectedRowIndex, DataGridViewColumn Col)
    {
        return grd.SelectedRows[SelectedRowIndex].Cells[Col.Name].Value;
    }

    public static void sg(this DataGridView grd, int rowIndex, string ColName, object value)
    {
        grd.Rows[rowIndex].Cells[ColName].Value = value;
    }

    public static object gg(this DataGridView grd, int rowIndex, string ColName)
    {
        return grd.Rows[rowIndex].Cells[ColName].Value;
    }

    public static List<T> ggAll<T>(this DataGridView grd, DataGridViewColumn Col)
    {
        return grd.ggAll<T>(Col.Name);
    }

    public static List<T> ggAll<T>(this DataGridView grd, string ColName)
    {
        List<T> res = new List<T>();
        for (int i = 0; i <= grd.RowCount - 1; i++)
        {
            res.Add((T)grd.Rows[i].Cells[ColName].Value);
        }
        return res;
    }

    public static void ResizeCols(DataGridView grd)
    {
        try
        {
            int colCount = 0;
            foreach (DataGridViewColumn c in grd.Columns)
            {
                if (c.Visible)
                {
                    colCount += 1;
                }
            }
            var averageW = Math.Max(Convert.ToInt32(grd.Width / (colCount)) - 20, 100);
            for (int i = 0; i <= grd.ColumnCount - 1; i++)
            {
                if (grd.Columns[i].Visible)
                {
                    grd.Columns[i].Width = averageW;
                }
            }
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
    }

    #endregion

    #region "Registry"


    public const string constAppMainKey = "Software\\OracleManager";

    public const string cntConStr = "ConStr";
    public static object GetRegistryValue(string KeyName, object DefaultValue = null)
    {
        return GetRegistryValue(constAppMainKey, KeyName, DefaultValue);
    }

    public static void SetRegistryValue(string KeyName, object _Value)
    {
        SetRegistryValue(constAppMainKey, KeyName, _Value);
    }

    public static object GetRegistryValue(string CurrentUserParentKey, string KeyName, object DefaultValue)
    {
        object res = null;
        try
        {
            var k = Registry.CurrentUser.OpenSubKey(CurrentUserParentKey, true);
            if (k != null)
            {
                res = k.GetValue(KeyName, DefaultValue);
            }
            else
            {
                k = Registry.CurrentUser.CreateSubKey(CurrentUserParentKey);
            }
            if (k != null)
                k.Close();
            // ex As Exception
        }
        catch
        {
            //PromptMsg(ex)
        }
        return res;
    }

    public static void SetRegistryValue(string CurrentUserParentKey, string KeyName, object _Value)
    {
        try
        {
            var k = Registry.CurrentUser.OpenSubKey(CurrentUserParentKey, true);
            if (k != null)
            {
                k.SetValue(KeyName, _Value);
            }
            else
            {
                k = Registry.CurrentUser.CreateSubKey(CurrentUserParentKey);
                k.SetValue(KeyName, _Value);
            }
            if (k != null)
                k.Close();
            // ex As Exception
        }
        catch
        {
            //PromptMsg(ex)
        }
    }

    public static void AddAppToWinStartup()
    {
        try
        {
            Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).SetValue(Application.ProductName, Application.ExecutablePath);
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
    }

    public static void RemoveAppFromWinStartup()
    {
        try
        {
            Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true).SetValue(Application.ProductName, "");
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
    }

    #endregion

    #region "Binary"

    public static void saveBinary(object c, string filepath)
    {
        try
        {
            using (Stream sr = File.Open(filepath, FileMode.Create))
            {
                System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                bf.Serialize(sr, c);
                sr.Close();
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
    }

    public static object loadBinary(string path)
    {
        try
        {
            if (File.Exists(path))
            {
                using (Stream sr = File.Open(path, FileMode.Open))
                {
                    System.Runtime.Serialization.Formatters.Binary.BinaryFormatter bf = new System.Runtime.Serialization.Formatters.Binary.BinaryFormatter();
                    var c = bf.Deserialize(sr);
                    sr.Close();
                    return c;
                }
            }
            else
            {
                throw new Exception("File not found");
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }
        //return null;
    }

    #endregion

    #region "Controls"

    public static void ClearControls(this Control ctl)
    {
        if (ctl != null && !ctl.IsDisposed)
        {
            ctl.SuspendLayout();

            while (ctl.Controls.Count > 0)
            {
                var cc = ctl.Controls[0];
                ctl.Controls.Remove(cc);
                cc.Dispose();
                cc = null;

            }

            ctl.ResumeLayout(true);
            ctl.Refresh();
        }

    }

    public static void CheckAll(this CheckedListBox chList, bool @checked)
    {
        for (int i = 0; i <= chList.Items.Count - 1; i++)
        {
            chList.SetItemChecked(i, @checked);
        }
    }

    public static List<object> GetCheckedValues(this CheckedListBox chList)
    {
        List<object> res = new List<object>();
        if (chList.Items.Count > 0)
        {
            var tp = chList.Items[0].GetType();

            for (int i = 0; i <= chList.Items.Count - 1; i++)
            {
                if (chList.GetItemChecked(i))
                {
                    res.Add(tp.GetProperty(chList.ValueMember).GetValue(chList.Items[i], null));
                }
            }
        }

        return res;
    }

    public static void SetCheckedValues(this CheckedListBox chList, IList L, bool check = true, bool InvertCheckElse = false)
    {
        if (chList.Items.Count > 0)
        {
            var tp = chList.Items[0].GetType();

            for (int i = 0; i <= chList.Items.Count - 1; i++)
            {
                if (L.Contains(tp.GetProperty(chList.ValueMember).GetValue(chList.Items[i], null)))
                {
                    chList.SetItemChecked(i, check);
                }
                else if (InvertCheckElse)
                {
                    chList.SetItemChecked(i, !check);
                }
            }
        }
    }

    public static void CenterControlVerticaly(Control cnt)
    {
        try
        {
            if (cnt == null)
                return;
            if (cnt.Parent == null)
                return;
            cnt.Top = Convert.ToInt32((cnt.Parent.Height - cnt.Height) / 2);
        }
        catch
        {
        }
    }

    public static void CenterControl(Control cnt, bool andVertically = false)
    {
        try
        {
            if (cnt == null)
                return;
            if (!(cnt is Form) && cnt.Parent == null)
                return;
            if (cnt is Form)
            {
                cnt.Left = Convert.ToInt32((Screen.PrimaryScreen.WorkingArea.Width - cnt.Width) / 2);
                if (andVertically)
                {
                    cnt.Top = Convert.ToInt32((Screen.PrimaryScreen.WorkingArea.Height - cnt.Height) / 2);
                }
            }
            else
            {
                cnt.Left = Convert.ToInt32((cnt.Parent.Width - cnt.Width) / 2);
                if (andVertically)
                {
                    cnt.Top = Convert.ToInt32((cnt.Parent.Height - cnt.Height) / 2);
                }
            }

        }
        catch
        {
        }
    }

    public static DialogResult PromptMsg(string s)
    {
        if (string.IsNullOrEmpty(s))
            return DialogResult.None;
        return MessageBox.Show(s, "");
    }

    //public static DialogResult PromptMsg(Exception ex)
    //{
    //    if (ex == null)
    //        return DialogResult.None;
    //    return MessageBox.Show(ex.Message, "");
    //}

    //public static void SaveErrorLog(Exception ex)
    //{
    //    try
    //    {

    //        if (ex != null)
    //        {
    //            string logPath = CombinePath(Application.StartupPath, "Errorlog-" + DateTime.Now.ToString("yyyy-MM-dd") + ".txt");
    //            My.Computer.FileSystem.WriteAllText(logPath, "Exception at " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\nErrorMsg: " + ex.Message.NullTrimer() + "\nStackSource: " + ex.StackTrace.NullTrimer() + "\n\n", true);

    //        }
    //        // ex IsNot Nothing Then

    //    }
    //    catch
    //    {
    //    }
    //}

    public static void FillCombo(ComboBox cmb, object db, string valueMember = "id", string DisplayMember = "description")
    {
        var _with1 = cmb;
        _with1.DataSource = null;
        _with1.ValueMember = valueMember;
        _with1.DisplayMember = DisplayMember;
        _with1.DataSource = db;
        _with1.Invalidate();
        _with1.Refresh();
    }

    public static void FillCombo(DataGridViewComboBoxColumn cmb, object db, string valueMember = "id", string DisplayMember = "description")
    {
        var _with2 = cmb;
        _with2.DataSource = null;
        _with2.ValueMember = valueMember;
        _with2.DisplayMember = DisplayMember;
        _with2.DataSource = db;
    }

    public static void FillCheckBoxList(CheckedListBox cmb, object db, string valueMember = "id", string DisplayMember = "description")
    {
        var _with3 = cmb;
        _with3.Items.Clear();
        _with3.ValueMember = valueMember;
        _with3.DisplayMember = DisplayMember;
        _with3.Items.AddRange(((IList)db).Cast<object>().ToArray());
    }

    public static void FillListBox(ListBox cmb, object db, string valueMember = "id", string DisplayMember = "description")
    {
        var _with4 = cmb;
        _with4.Items.Clear();
        _with4.ValueMember = valueMember;
        _with4.DisplayMember = DisplayMember;
        _with4.Items.AddRange(((IList)db).Cast<object>().ToArray());
    }

    public static void ValidateTowDateTimePicker(DateTimePicker FromDate, DateTimePicker ToDate, bool ValidateSecond = false)
    {
        if (FromDate.Value > ToDate.Value)
        {
            if (ValidateSecond)
            {
                ToDate.Value = FromDate.Value;
            }
            else
            {
                FromDate.Value = ToDate.Value;
            }
        }
    }

    public static void ClearForm(ref Control frm)
    {
        try
        {
            foreach (Control c in frm.Controls)
            {
                var _with5 = c;
                if ((c is NumericUpDown))
                {
                    ((NumericUpDown)c).Value = 0m;
                    //(TypeOf c Is Label) Or

                }
                else if (((c is TextBox)) && !(c.Parent is NumericUpDown))
                {
                    if (c.Text != null && c.Text.Length > 0)
                    {
                        if (c.Text.Last() != ':')
                        {
                            c.Text = "";
                        }
                    }
                }
                else if ((c is ComboBox))
                {
                    //Try
                    var cmb = (ComboBox)c;
                    if (cmb.Items.Count > 0)
                    {
                        cmb.SelectedItem = cmb.Items[0];
                    }
                    //Catch
                    //    CType(c, ComboBox).SelectedIndex = -1
                    //End Try
                }
                else if ((c is CheckBox))
                {
                    ((CheckBox)c).Checked = false;
                }
                else if ((c is DataGridView))
                {
                    ((DataGridView)c).Rows.Clear();
                }
                else if ((c is DataGridView))
                {
                    ((DataGridView)c).Rows.Clear();

                    //Else
                }
                if (c.Controls.Count > 0)
                {
                    var tempC = c;
                    ClearForm(ref tempC);
                }
            }
        }
        catch (Exception ex)
        {
            PromptMsg(ex.Message);
        }
    }

    public static void ClearForm(ref Control frm, ref Control Ctrl_ToFocus)
    {
        try
        {
            ClearForm(ref frm);
            Ctrl_ToFocus.Select();
            Ctrl_ToFocus.Focus();
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
    }

    public static void FillListView(ListView cmb, object db, string displayMember = "description")
    {
        var _with6 = cmb;
        _with6.Items.Clear();
        if (db != null)
        {
            if (db is DataTable)
            {
                for (int i = 0; i <= ((DataTable)db).Rows.Count - 1; i++)
                {
                    ListViewItem _item = new ListViewItem(((DataTable)db).Rows[i][displayMember].ToString());
                    _item.Tag = ((DataTable)db).Rows[i];
                    _with6.Items.Add(_item);
                }
            }
            else if (db is IList)
            {
                for (int i = 0; i <= ((IList)db).Count - 1; i++)
                {
                    ListViewItem _item = new ListViewItem(GetPropertyValue(((IList)db)[i], displayMember).ToString());
                    _item.Tag = ((IList)db)[i];
                    _with6.Items.Add(_item);
                }
            }

        }
    }

    //[Extension()]
    public static void AddItem(ListView LstView, object _item, string Caption, int ImageIndex)
    {
        var _with7 = LstView;

        ListViewItem _itemView = new ListViewItem(Caption, ImageIndex);
        _itemView.Tag = _item;
        _with7.Items.Add(_itemView);

    }

    public static void ShowPanel(this Control _someForm, Panel pnl, bool DockIt = false)
    {
        try
        {
            if (_someForm != null && _someForm.IsDisposed == false)
            {
                foreach (Control p in _someForm.Controls)
                {
                    if (p is Panel && !p.Equals(pnl))
                    {
                        p.Dock = DockStyle.None;
                        p.Visible = false;
                    }
                }

                pnl.Location = new Point(0, 0);
                pnl.Visible = true;
                if (DockIt)
                {
                    pnl.Dock = DockStyle.Fill;
                }
                _someForm.ClientSize = pnl.Size;
            }
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
    }

    public static string InputBox(string title, string promptText, string DefaultValue = "")
    {
        string value = DefaultValue;
        Form form = new Form();
        Label label = new Label();
        TextBox textBox = new TextBox();
        Button buttonOk = new Button();
        Button buttonCancel = new Button();

        form.Text = title;
        label.Text = promptText;
        textBox.Text = value;

        buttonOk.Text = "&OK";
        buttonCancel.Text = "Ca&ncel";
        buttonOk.DialogResult = DialogResult.OK;
        buttonCancel.DialogResult = DialogResult.Cancel;

        label.SetBounds(9, 20, 372, 13);
        textBox.SetBounds(12, 36, 372, 20);
        buttonOk.SetBounds(228, 72, 75, 23);
        buttonCancel.SetBounds(309, 72, 75, 23);

        label.AutoSize = true;
        textBox.Anchor = textBox.Anchor | AnchorStyles.Right;
        buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        form.BackColor = Color.White;
        form.ClientSize = new Size(396, 107);
        form.Controls.AddRange(new Control[] { label, textBox, buttonOk, buttonCancel });
        form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
        form.FormBorderStyle = FormBorderStyle.FixedDialog;
        form.StartPosition = FormStartPosition.CenterScreen;
        form.MinimizeBox = false;
        form.MaximizeBox = false;
        form.AcceptButton = buttonOk;
        form.CancelButton = buttonCancel;
        Mac.Slide(form);
        DialogResult dialogResult = form.ShowDialog();
        return value = textBox.Text;
        //return dialogResult;
    }

    public static string InputComboBox(string title, string promptText, string[] data, string DefaultValue = "")
    {
        string value = DefaultValue;
        Form form = new Form();
        Label label = new Label();
        ComboBox combo = new ComboBox();
        Button buttonOk = new Button();
        Button buttonCancel = new Button();

        form.Text = title;
        label.Text = promptText;
        combo.Items.AddRange(data);
        combo.Text = value;
        combo.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
        combo.AutoCompleteSource = AutoCompleteSource.ListItems;

        buttonOk.Text = "&OK";
        buttonCancel.Text = "Ca&ncel";
        buttonOk.DialogResult = DialogResult.OK;
        buttonCancel.DialogResult = DialogResult.Cancel;

        label.SetBounds(9, 20, 372, 13);
        combo.SetBounds(12, 36, 372, 20);
        buttonOk.SetBounds(228, 72, 75, 23);
        buttonCancel.SetBounds(309, 72, 75, 23);

        label.AutoSize = true;
        combo.Anchor = combo.Anchor | AnchorStyles.Right;
        buttonOk.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;
        buttonCancel.Anchor = AnchorStyles.Bottom | AnchorStyles.Right;

        form.BackColor = Color.White;
        form.ClientSize = new Size(396, 107);
        form.Controls.AddRange(new Control[] { label, combo, buttonOk, buttonCancel });
        form.ClientSize = new Size(Math.Max(300, label.Right + 10), form.ClientSize.Height);
        form.FormBorderStyle = FormBorderStyle.FixedDialog;
        form.StartPosition = FormStartPosition.CenterScreen;
        form.MinimizeBox = false;
        form.MaximizeBox = false;
        form.AcceptButton = buttonOk;
        form.CancelButton = buttonCancel;
        Mac.Slide(form);
        DialogResult dialogResult = form.ShowDialog();
        if (dialogResult == DialogResult.OK)
        {
            return value = combo.Text;
        }
        else return "";
        //return dialogResult;
    }

    #endregion

    #region "var Graphics"

    public static int gX(int x, int BaseX = 1024)
    {
        return Convert.ToInt32(x * Screen.PrimaryScreen.Bounds.Width / BaseX);
    }

    public static int gY(int y, int BaseY = 768)
    {
        return Convert.ToInt32(y * Screen.PrimaryScreen.Bounds.Height / BaseY);
    }

    public static Point[] gPoints(Point[] pts, int BaseX = 1024, int BaseY = 768)
    {
        List<Point> res = new List<Point>();

        if (pts != null)
        {
            foreach (var p_loopVariable in pts)
            {
                var p = p_loopVariable;
                res.Add(new Point(gX(p.X), gY(p.Y)));
            }

        }

        return res.ToArray();
    }

    public static Point gPoint(Point p, int BaseX = 1024, int BaseY = 768)
    {
        Point res = new Point();
        if (p != null)
        {
            res = new Point(gX(p.X), gY(p.Y));
        }
        return res;
    }

    public static Size gSize(Size p, int BaseX = 1024, int BaseY = 768)
    {
        Size res = new Size();
        if (p != null)
        {
            res = new Size(gX(p.Width), gY(p.Height));
        }
        return res;
    }

    public static bool PointVisible(Point[] pts, Point pt)
    {
        bool res = false;
        using (GraphicsPath gr = new GraphicsPath())
        {
            gr.AddClosedCurve(pts);
            Region rg = new Region(gr);
            res = rg.IsVisible(pt);
            rg.Dispose();
            rg = null;
        }

        return res;
    }

    //[Extension()]
    public static Point Add(Point pt, int Pixcels)
    {
        return new Point(pt.X + Pixcels, pt.Y + Pixcels);
    }

    //[Extension()]
    public static Point Add(Point pt, int xPixcels, int yPixcels)
    {
        return new Point(pt.X + xPixcels, pt.Y + yPixcels);
    }

    //[Extension()]
    public static Point Add(Point pt, Size sz)
    {
        return new Point(pt.X + sz.Width, pt.Y + sz.Height);
    }

    public static List<PointF> PiePoint(int iWidth, int PointCount)
    {
        List<PointF> res = new List<PointF>();
        try
        {
            var R = Convert.ToSingle(iWidth / 2);
            var Alfa = Convert.ToSingle(2 * Math.PI / PointCount);

            for (int i = 0; i <= Convert.ToInt32(PointCount) - 1; i++)
            {
                res.Add(new PointF((float)(R * (1 + Math.Cos(Convert.ToDouble(i * Alfa)))), (float)(R * (1 + Math.Sin(Convert.ToDouble(i * Alfa))))));

            }

        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
        return res;
    }

    public static List<Point> CartesianCtrl(int ContainerWidth, Size CtrlSize, int CtrlCount)
    {

        List<Point> lp = new List<Point>();
        var WCount = Convert.ToInt32(ContainerWidth / CtrlSize.Width);
        if (CtrlSize.Width * WCount > ContainerWidth)
        {
            WCount -= 1;
        }
        var HCount = -1;
        //var WIncrement = 0;
        for (int i = 0; i <= CtrlCount - 1; i++)
        {
            if ((i) % WCount == 0)
            {
                HCount += 1;
            }
            lp.Add(new Point(((i % WCount) * (CtrlSize.Width)), (HCount * (CtrlSize.Height))));
        }
        return lp;

    }

    public static int GetCartesianCtrlPerLine(int ContainerWidth, int CtrlWidth)
    {

        var WCount = Convert.ToInt32(ContainerWidth / CtrlWidth);
        if (CtrlWidth * WCount > ContainerWidth)
        {
            WCount -= 1;
        }
        return WCount;

    }

    public static List<Point> CartesianIndex(int ContainerWidth, int CtrlCount)
    {

        List<Point> lp = new List<Point>();
        var WCount = ContainerWidth;
        var HCount = -1;
        //var WIncrement = 0;
        for (int i = 0; i <= CtrlCount - 1; i++)
        {
            if ((i) % WCount == 0)
            {
                HCount += 1;
            }
            lp.Add(new Point((i % WCount), HCount));
        }
        return lp;

    }

    public static void RoundShape(System.Windows.Forms.Control ctl, float CirRay = 14f)
    {
        System.Drawing.Drawing2D.GraphicsPath gr = new System.Drawing.Drawing2D.GraphicsPath();
        gr.AddPie(0, 0, CirRay, CirRay, 180f, 90f);
        gr.AddPie(ctl.Width - CirRay, 0, CirRay, CirRay, 270f, 90f);
        gr.AddPie(0, ctl.Height - CirRay, CirRay, CirRay, 90f, 90f);
        gr.AddPie(ctl.Width - CirRay, ctl.Height - CirRay, CirRay, CirRay, 0f, 90f);

        gr.AddRectangle(new System.Drawing.Rectangle((int)CirRay / 2, 0, (int)(ctl.Width - CirRay), (int)ctl.Height));
        gr.AddRectangle(new System.Drawing.Rectangle(0, (int)CirRay / 2, (int)CirRay / 2, (int)(ctl.Height - CirRay)));
        gr.AddRectangle(new System.Drawing.Rectangle((int)(ctl.Width - CirRay / 2), (int)CirRay / 2, (int)CirRay / 2, (int)(ctl.Height - CirRay)));

        ctl.Region = new System.Drawing.Region(gr);
    }

    public static Region RoundShape(Size ctl, float CirRay = 14f)
    {
        System.Drawing.Drawing2D.GraphicsPath gr = new System.Drawing.Drawing2D.GraphicsPath();
        gr.AddPie(0, 0, CirRay, CirRay, 180f, 90f);
        gr.AddPie(ctl.Width - CirRay, 0, CirRay, CirRay, 270f, 90f);
        gr.AddPie(0, ctl.Height - CirRay, CirRay, CirRay, 90f, 90f);
        gr.AddPie(ctl.Width - CirRay, ctl.Height - CirRay, CirRay, CirRay, 0f, 90f);

        gr.AddRectangle(new System.Drawing.Rectangle((int)CirRay / 2, 0, (int)(ctl.Width - CirRay), (int)ctl.Height));
        gr.AddRectangle(new System.Drawing.Rectangle(0, (int)CirRay / 2, (int)CirRay / 2, (int)(ctl.Height - CirRay)));
        gr.AddRectangle(new System.Drawing.Rectangle((int)(ctl.Width - CirRay / 2), (int)CirRay / 2, (int)CirRay / 2, (int)(ctl.Height - CirRay)));

        return new System.Drawing.Region(gr);
    }

    public static Bitmap GetArrowImage(bool RightOne, int iWidth = 20, int iHeight = 20)
    {
        Bitmap res = new Bitmap(iWidth, iHeight);
        try
        {
            PointF[] pts = null;
            if (RightOne)
            {
                pts = new PointF[] {
					new PointF(iWidth / 4, 0),
					new PointF(3 * iWidth / 4, iHeight / 2),
					new PointF(iWidth / 4, iHeight)
				};
            }
            else
            {
                pts = new PointF[] {
					new PointF(3 * iWidth / 4, 0),
					new PointF(iWidth / 4, iHeight / 2),
					new PointF(3 * iWidth / 4, iHeight)
				};
            }

            using (Graphics gr = Graphics.FromImage(res))
            {
                gr.FillPolygon(Brushes.RoyalBlue, pts);
            }

        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
        return res;
    }

    #endregion

    #region "Reflection"

    public static object GetPropertyValue(object obj, string prpName)
    {
        object res = null;

        if (obj != null)
        {
            var prp = obj.GetType().GetProperty(prpName);
            if (prp != null)
            {
                res = prp.GetValue(obj, null);
            }

        }

        return res;
    }

    public static void SetPropertyValue(object obj, string prpName, object value)
    {

        if (obj != null)
        {
            var prp = obj.GetType().GetProperty(prpName);
            if (prp != null)
            {
                prp.SetValue(obj, value, null);
            }

        }
    }

    #endregion

    #region "Strings"

    public static string AsString(this object obj, string DefaultValue = "")
    {
        return (obj != null && !System.DBNull.Value.Equals(obj) ? obj.ToString() : "");
    }

    public static string SplitByCamelAndNumbers(this string s)
    {
        var sb = new StringBuilder();

        StringBuilder builder = new StringBuilder();
        for (int i = 0; i < s.Length; i++)
        {
            char c = s[i];
            if (
                    builder.Length > 0 &&
                    (
                        Char.IsUpper(c) ||
                        (Char.IsDigit(c) && !Char.IsDigit(s[i - 1])) ||
                        (!Char.IsDigit(c) && Char.IsDigit(s[i - 1]))
                    )
                ) builder.Append(' ');
            builder.Append(c);
        }
        return builder.ToString();
    }

    public static string SplitCamelCase(string source)
    {
        var r = new System.Text.RegularExpressions.Regex(@"
                (?<=[A-Z])(?=[A-Z][a-z]) |
                 (?<=[^A-Z])(?=[A-Z]) |
                 (?<=[A-Za-z])(?=[^A-Za-z])", System.Text.RegularExpressions.RegexOptions.IgnorePatternWhitespace);

        //string s = "TodayILiveInTheUSAWithSimon";
        //YYY Today I Live In The USA With Simon ZZZ
        return r.Replace(source, " ");
    }

    public static int AsInt(this object obj, int DefaultValue = 0)
    {
        if (obj != null)
        {
            var __v = 0;
            if (obj.GetType().IsEnum)
            {
                return (int)(IConvertible)obj;
            }
            else if (!int.TryParse(obj.ToString(), out __v))
                return DefaultValue;
            else
                return __v;
        }
        return DefaultValue;
    }
    
    public static decimal AsDecimal(this string s, decimal DefaultValue = 0)
    {
        decimal res = DefaultValue;
        if (!decimal.TryParse(s, out res))
        {
            res = DefaultValue;
        }
        return res;
    }

    public static string SplitByCapitals(this string stringToSplit)
    {
        string res = "";
        try
        {
            res = System.Text.RegularExpressions.Regex.Replace(stringToSplit, "((?<=[a-z])[A-Z]|[A-Z](?=[a-z]))", " $1").Trim().Replace("_", " ");
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
        return res;
    }

    public static string GetURLWithNoRoot(string URL)
    {
        if (string.IsNullOrEmpty(URL)) return "";
        var noHttp = URL.ToLower().Replace("http://", "").Replace("https://", "");
        var spl = noHttp.Split('/');
        var sNoRoot = noHttp.ToLower().Replace(spl[0], "");
        return sNoRoot;
    }

    public static string RepeatString(string s, int times)
    {
        string res = "";
        if (!string.IsNullOrEmpty(s))
        {
            for (int i = 0; i <= times - 1; i++)
            {
                res += s;
            }
        }
        return res;
    }

    public static string CapitalSplitter(string strToSplit)
    {
        string res = "";
        try
        {
            //Empty String
            if (string.IsNullOrEmpty(strToSplit))
                return strToSplit;

            //Real Stuff
            for (int i = 0; i <= strToSplit.Length - 1; i++)
            {
                if (char.IsUpper(strToSplit[i]))
                {
                    res += " ";
                }
                res += strToSplit[i];
            }

        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
        return res;
    }

    public static string SplitterByUnderscore(this string strToSplit)
    {
        string res = "";
        try
        {
            if (string.IsNullOrEmpty(strToSplit)) return strToSplit;

            var spl = strToSplit.Split('_');
            foreach (var str in spl)
            {
                if (!string.IsNullOrEmpty(str))
                    res += str.Length > 1 ? str[0].ToString().ToUpper() + str.Substring(1).ToLower() : str.ToLower();
            }
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
        return res;
    }

    public static bool IsRational(object obj)
    {
        bool res = false;
        try
        {
            double d = 0;
            if (double.TryParse(obj.ToString(), out d))
            {
                res = (d - Convert.ToInt32(d) != 0);
            }
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
        return res;
    }

    public static bool IsEmpty(this string obj)
    {
        return string.IsNullOrEmpty(obj);
    }

    public static bool NotEmpty(this string obj)
    {
        return !string.IsNullOrEmpty(obj);
    }

    public static string NullTrimer(this string obj)
    {
        return string.IsNullOrEmpty(obj) ? "" : obj.Trim();
    }

    //12.32 => 12.30 , 12.33 => 12.35 , 12.37 => 12.35 , 12.38 > 12.40
    public static decimal GetSalimWeirdRounding(decimal nb, int digits)
    {
        decimal res = nb;
        try
        {
            var splComma = Convert.ToString(nb).Split('.').Where(f => f.NotEmpty()).ToList();


            if (splComma.NotEmpty() && splComma.Count > 1)
            {
                bool changeBeforeLast = false;

                //12.333 >> 12.33
                var afterComma = splComma[1];
                if (splComma[1].Length > digits)
                {
                    afterComma = splComma[1].Substring(0, digits);
                }
                else if (splComma[1].Length < digits)
                {
                    afterComma = splComma[1] +
                    new String('0', digits - splComma[1].Length);
                }

                //12.32 => 12.30 , 12.33 => 12.35 , 12.37 => 12.35 , 12.38 > 12.40
                var lastDigit = Convert.ToInt32(afterComma.Last().ToString());
                if (lastDigit < 3)
                {
                    lastDigit = 0;
                }
                else if (lastDigit >= 3 && lastDigit < 8)
                {
                    lastDigit = 5;
                }
                else if (lastDigit > 7)
                {
                    lastDigit = 0;
                    changeBeforeLast = true;
                }

                afterComma = afterComma.Substring(0, afterComma.Length - 1) + lastDigit.ToString();

                //12.338 > 12.340 ---------------- 12.8 > 13.0

                if (changeBeforeLast)
                {

                    if (afterComma.Length > 1)
                    {
                        //12.238 > 12.240                                                                   3             (2+1)                                                                         0     
                        afterComma = afterComma.Substring(0, afterComma.Length - 2) + Convert.ToString(Convert.ToInt32(Convert.ToString(afterComma[afterComma.Length - 2])) + 1) + lastDigit;


                    }
                    else
                    {
                        splComma[0] = Convert.ToString(Convert.ToInt32(splComma[0]) + 1);

                    }
                    //splComma(1).Length > 1 Then

                }
                //changeBeforeLast Then

                //Assembling all parts
                res = Convert.ToDecimal(splComma[0] + "." + afterComma);

            }
            // splComma.NotEmpty AndAlso splComma.Count > 1 Then

        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
        return res;
    }

    public static List<T> GetList<T>(this string s, string Splitter = ",")
    {
        var res = new List<T>();
        try
        {
            if (!string.IsNullOrEmpty(s))
            {
                var type = typeof(T);
                if (!type.IsPrimitive)
                {
                    throw new Exception("T should be a primitive type like string or int");
                }
                if (s.StartsWith("[") && s.EndsWith("]"))
                {
                    s = s.Substring(1, s.Length - 2);
                }
                res = s.Split(new string[] { Splitter }, StringSplitOptions.RemoveEmptyEntries).Select(o => (T)Convert.ChangeType(o.Trim(), type)).ToList();
            }
        }
        catch (Exception ex)
        {
            throw ex;// handle in a suitable way, depending on your project case
        }
        return res;
    }

    #endregion

    #region "Numbers"

    public static Boolean IsNumber(this String value)
    {
        return string.IsNullOrEmpty(value)?false : value.NullTrimer().All(Char.IsDigit);
    }

    #endregion

    #region "Dev"

    //Public Function IsHorizontalScrollVisible(ByVal aGrid As DevExpress.XtraGrid.Views.Grid.GridView) As Boolean
    //    Try
    //        If aGrid Is Nothing Then Return False
    //        Dim tp As Type = aGrid.GetType() 'scrollInfo IsNeededVScrollBar
    //        Dim pi = tp.GetProperty("ScrollInfo", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
    //        pi = tp.GetProperty("ScrollInfo", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
    //        Return CBool(pi.GetValue(aGrid, Nothing).HScrollVisible)
    //    Catch
    //        Return False
    //    End Try
    //End Function

    //Public Function GetHorizontalScrollSize(ByVal aGrid As DevExpress.XtraGrid.Views.Grid.GridView) As Integer
    //    Try
    //        If aGrid Is Nothing Then Return 0
    //        Dim tp As Type = aGrid.GetType()
    //        Dim pi = tp.GetProperty("ScrollInfo", Reflection.BindingFlags.NonPublic Or Reflection.BindingFlags.Instance)
    //        Return CInt(pi.GetValue(aGrid, Nothing).HScrollSize)
    //    Catch
    //        Return 0
    //    End Try
    //End Function

    #endregion

    #region "IO"

    public static string CombinePath(string p1, string p2)
    {
        if (string.IsNullOrEmpty(p1))
        {
            throw new Exception("p1 is empty !");
        }
        else if (string.IsNullOrEmpty(p2))
        {
            throw new Exception("p2 is empty !");
        }
        else
        {
            return System.IO.Path.Combine(p1, p2);
        }
        //return "";
    }

    public static string FolderBrowse(string StartPath = "")
    {
        try
        {
            using (FolderBrowserDialog fl = new FolderBrowserDialog())
            {
                fl.SelectedPath = StartPath;
                if (fl.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(fl.SelectedPath))
                {
                    return fl.SelectedPath;
                }
            }

        }
        catch //(Exception ex)
        {
        }
        return "";
    }

    public static string FileBrowse(string Filter = "")
    {
        try
        {
            using (OpenFileDialog fl = new OpenFileDialog())
            {
                fl.FileName = "";
                if (!string.IsNullOrEmpty(Filter))
                    fl.Filter = Filter;
                if (fl.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(fl.FileName))
                {
                    return fl.FileName;
                }
            }

        }
        catch //(Exception ex)
        {
        }
        return "";
    }

    public static string FileSave(string Filter = "")
    {
        try
        {
            using (SaveFileDialog fl = new SaveFileDialog())
            {
                fl.FileName = "";
                if (!string.IsNullOrEmpty(Filter))
                    fl.Filter = Filter;
                if (fl.ShowDialog() == DialogResult.OK && !string.IsNullOrEmpty(fl.FileName))
                {
                    return fl.FileName;
                }
            }

        }
        catch //(Exception ex)
        {
        }
        return "";
    }

    public static string FileNameCleaner(string fileName)
    {
        //Ex: var fileName = "a ccc d.d [ b ] '  ..... ......_files.docx";
        var fileInfo = new System.IO.FileInfo(fileName);
        var ext = fileInfo.Extension.Replace("_", "");
        fileName = fileName.Replace(fileInfo.Extension, "").Replace("..", "");

        string pattern = "[\\~#%&*{}/:<>?|\"-'\\]\\[]";
        string replacement = "";

        Regex regEx = new Regex(pattern);
        string sanitized = Regex.Replace(regEx.Replace(fileName, replacement), @"\s+", "_");
        if (sanitized.EndsWith("."))
        {
            sanitized = sanitized.Substring(0, sanitized.Length - 1);
        }
        return sanitized + ext;
    }

    public static string GetFileAtStartupPath(string filename)
    {
        return ControlMod.CombinePath(Application.StartupPath, filename);
    }

    public static string GetFileExtension(this string FileName)
    {
        var res = "";
        try
        {
            if (FileName.NotEmpty())
            {
                var dotIndex = FileName.LastIndexOf('.');
                if (dotIndex > -1)
                {
                    res = FileName.Substring(dotIndex);
                }
            }
        }
        catch// (Exception)
        {
        }
        return res;
    }

    #endregion

    #region "Paper and printing"

    //public static List<iddesc> GetPaperKinds()
    //{
    //    List<iddesc> res = new List<iddesc>();
    //    try
    //    {
    //        foreach (System.Drawing.Printing.PaperKind p in Enum.GetValues(typeof(System.Drawing.Printing.PaperKind)))
    //        {
    //            res.Add(new iddesc
    //            {
    //                id = Convert.ToInt32(p),
    //                description = p.ToString
    //            });
    //        }
    //    }
    //    catch (Exception ex)
    //    {
    //        PromptMsg(ex);
    //    }
    //    return res;
    //}

    public static List<string> GetPrinterList()
    {
        List<string> res = new List<string>();
        try
        {
            foreach (string p in System.Drawing.Printing.PrinterSettings.InstalledPrinters)
            {
                res.Add(p);
            }
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
        return res;
    }

    #endregion

    #region "TreeView"

    public static TreeNode GetNode(object tag, string stext, int ImageIndex = 0, TreeNode[] childrens = null)
    {
        if (childrens != null && childrens.Length > 0)
        {
            return new System.Windows.Forms.TreeNode(stext, ImageIndex, ImageIndex, childrens) { Tag = tag };
        }
        else
        {
            return new System.Windows.Forms.TreeNode(stext, ImageIndex, ImageIndex) { Tag = tag };
        }
    }

    //public static TreeNode GetNode(iddesc iddescT)
    //{
    //    return GetNode(iddescT.id, iddescT.description);
    //}

    // = -1)
    public static void SetNodeCaption(this TreeView trv, object tag, string Caption, int spcLevel)
    {
        try
        {
            if (trv.Nodes.Count > 0)
            {
                for (int i = 0; i <= trv.Nodes.Count - 1; i++)
                {
                    if ((trv.Nodes[i].Level == spcLevel | spcLevel == -1) && trv.Nodes[i].Tag != null && trv.Nodes[i].Tag.Equals(tag))
                    {
                        trv.Nodes[i].Text = Caption;
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
    }

    public static void DeleteNode(this TreeView trv, object tag, int spcLevel = -1)
    {
        try
        {
            if (trv.Nodes.Count > 0)
            {
                for (int i = 0; i <= trv.Nodes.Count - 1; i++)
                {
                    if ((trv.Nodes[i].Level == spcLevel | spcLevel == -1) && trv.Nodes[i].Tag != null && trv.Nodes[i].Tag.Equals(tag))
                    {
                        trv.Nodes.Remove(trv.Nodes[i]);
                        break; // TODO: might not be correct. Was : Exit For
                    }
                }
            }
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
    }

    #endregion

    #region "Proccess"

    //http://www.developerfusion.com/code/4662/execute-a-process-and-fetch-its-output/
    public static string GetProcessText(string process, string param, string workingDir)
    {
        Process p = new Process();
        // this is the name of the process we want to execute 
        p.StartInfo.FileName = process;
        if (workingDir.NotEmpty())
        {
            p.StartInfo.WorkingDirectory = workingDir;
        }
        p.StartInfo.Arguments = param;
        // need to set this to false to redirect output
        p.StartInfo.UseShellExecute = false;
        p.StartInfo.RedirectStandardOutput = true;
        // start the process 
        p.Start();
        // read all the output
        // here we could just read line by line and display it
        // in an output window 
        string output = p.StandardOutput.ReadToEnd();
        // wait for the process to terminate 
        p.WaitForExit();
        return output;
    }

    #endregion

    #region "DateTime"

    //[Extension()]
    public static string ToHHmm(double _hours, bool ShowMinusIfExists)
    {
        string res = "";
        try
        {
            var hours = Math.Abs(_hours);
            int h = (int)Math.Truncate(hours);
            var m = Convert.ToInt32(60 * (hours - h));
            if (m == 60)
            {
                h += 1;
                m = 0;
            }
            res = _hours < 0.0 && ShowMinusIfExists ? "-" : "" + string.Format("{0}:{1:00}", h, m);
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
        return res;
    }

    #endregion

    #region "Skin"

    public static void ApplySkin(Control ctl, Color ForeColor, Color BackColor)
    {
        try
        {
            ctl.ForeColor = ForeColor;
            ctl.BackColor = BackColor;
            foreach (Control c in ctl.Controls)
            {
                c.ForeColor = Color.White;
                c.BackColor = Color.DarkGray;
                //Color.FromArgb(51, 51, 51)
                ApplySkin(c, ForeColor, BackColor);
            }
        }
        catch (Exception ex)
        {
            PromptMsg(ex);
        }
    }

    #endregion

}