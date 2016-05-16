namespace OracleManager
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.btnDotNetTypes = new System.Windows.Forms.Button();
            this.btnExec = new System.Windows.Forms.Button();
            this.lblWait = new System.Windows.Forms.Label();
            this.numRowLimit = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.lblCount = new System.Windows.Forms.Label();
            this.lstObjects = new System.Windows.Forms.ListBox();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.cmbData = new System.Windows.Forms.ComboBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.tab = new System.Windows.Forms.TabControl();
            this.k1 = new System.Windows.Forms.TabPage();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.schemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.openWithAllFieldsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateCClassToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateCClassWCFToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.generateClassWithCollectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numRowLimit)).BeginInit();
            this.tab.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.btnDotNetTypes);
            this.splitContainer1.Panel1.Controls.Add(this.btnExec);
            this.splitContainer1.Panel1.Controls.Add(this.lblWait);
            this.splitContainer1.Panel1.Controls.Add(this.numRowLimit);
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.lblCount);
            this.splitContainer1.Panel1.Controls.Add(this.lstObjects);
            this.splitContainer1.Panel1.Controls.Add(this.tbSearch);
            this.splitContainer1.Panel1.Controls.Add(this.cmbData);
            this.splitContainer1.Panel1.Controls.Add(this.btnConnect);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.tab);
            this.splitContainer1.Size = new System.Drawing.Size(859, 480);
            this.splitContainer1.SplitterDistance = 286;
            this.splitContainer1.TabIndex = 0;
            // 
            // btnDotNetTypes
            // 
            this.btnDotNetTypes.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnDotNetTypes.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnDotNetTypes.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDotNetTypes.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnDotNetTypes.Location = new System.Drawing.Point(210, 80);
            this.btnDotNetTypes.Name = "btnDotNetTypes";
            this.btnDotNetTypes.Size = new System.Drawing.Size(32, 26);
            this.btnDotNetTypes.TabIndex = 10;
            this.btnDotNetTypes.Text = "!";
            this.toolTip1.SetToolTip(this.btnDotNetTypes, "Get .Net Types (F6)");
            this.btnDotNetTypes.UseVisualStyleBackColor = true;
            this.btnDotNetTypes.Click += new System.EventHandler(this.btnDotNetTypes_Click);
            // 
            // btnExec
            // 
            this.btnExec.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnExec.BackgroundImage = global::OracleManager.Properties.Resources.Exclamationmarkicon;
            this.btnExec.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnExec.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnExec.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnExec.Location = new System.Drawing.Point(248, 80);
            this.btnExec.Name = "btnExec";
            this.btnExec.Size = new System.Drawing.Size(32, 26);
            this.btnExec.TabIndex = 9;
            this.toolTip1.SetToolTip(this.btnExec, "Execute (F5)");
            this.btnExec.UseVisualStyleBackColor = true;
            this.btnExec.Click += new System.EventHandler(this.btnExec_Click);
            // 
            // lblWait
            // 
            this.lblWait.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWait.AutoSize = true;
            this.lblWait.BackColor = System.Drawing.Color.Yellow;
            this.lblWait.Font = new System.Drawing.Font("Microsoft Sans Serif", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWait.ForeColor = System.Drawing.Color.Black;
            this.lblWait.Location = new System.Drawing.Point(113, 234);
            this.lblWait.Name = "lblWait";
            this.lblWait.Size = new System.Drawing.Size(206, 37);
            this.lblWait.TabIndex = 8;
            this.lblWait.Text = "Please wait...";
            this.lblWait.Visible = false;
            // 
            // numRowLimit
            // 
            this.numRowLimit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.numRowLimit.ForeColor = System.Drawing.Color.RoyalBlue;
            this.numRowLimit.Location = new System.Drawing.Point(240, 460);
            this.numRowLimit.Maximum = new decimal(new int[] {
            99999999,
            0,
            0,
            0});
            this.numRowLimit.Name = "numRowLimit";
            this.numRowLimit.Size = new System.Drawing.Size(43, 20);
            this.numRowLimit.TabIndex = 4;
            this.numRowLimit.Value = new decimal(new int[] {
            50,
            0,
            0,
            0});
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.RoyalBlue;
            this.label1.Location = new System.Drawing.Point(181, 462);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(61, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Dft row limit";
            // 
            // lblCount
            // 
            this.lblCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.lblCount.AutoSize = true;
            this.lblCount.BackColor = System.Drawing.Color.Yellow;
            this.lblCount.Location = new System.Drawing.Point(-1, 464);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(0, 13);
            this.lblCount.TabIndex = 5;
            // 
            // lstObjects
            // 
            this.lstObjects.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstObjects.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lstObjects.ForeColor = System.Drawing.Color.RoyalBlue;
            this.lstObjects.FormattingEnabled = true;
            this.lstObjects.ItemHeight = 20;
            this.lstObjects.Location = new System.Drawing.Point(0, 112);
            this.lstObjects.Name = "lstObjects";
            this.lstObjects.Size = new System.Drawing.Size(283, 344);
            this.lstObjects.TabIndex = 3;
            this.lstObjects.SelectedIndexChanged += new System.EventHandler(this.lstObjects_SelectedIndexChanged);
            this.lstObjects.KeyDown += new System.Windows.Forms.KeyEventHandler(this.lstObjects_KeyDown);
            this.lstObjects.MouseDown += new System.Windows.Forms.MouseEventHandler(this.lstObjects_MouseDown);
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tbSearch.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tbSearch.ForeColor = System.Drawing.Color.RoyalBlue;
            this.tbSearch.Location = new System.Drawing.Point(2, 80);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(202, 26);
            this.tbSearch.TabIndex = 2;
            this.tbSearch.TextChanged += new System.EventHandler(this.tbSearch_TextChanged);
            this.tbSearch.KeyDown += new System.Windows.Forms.KeyEventHandler(this.tbSearch_KeyDown);
            // 
            // cmbData
            // 
            this.cmbData.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbData.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbData.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.cmbData.ForeColor = System.Drawing.Color.RoyalBlue;
            this.cmbData.FormattingEnabled = true;
            this.cmbData.Location = new System.Drawing.Point(1, 46);
            this.cmbData.Name = "cmbData";
            this.cmbData.Size = new System.Drawing.Size(282, 28);
            this.cmbData.TabIndex = 1;
            this.cmbData.SelectedIndexChanged += new System.EventHandler(this.cmbData_SelectedIndexChanged);
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnConnect.ForeColor = System.Drawing.Color.RoyalBlue;
            this.btnConnect.Location = new System.Drawing.Point(1, 0);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(282, 40);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "&Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // tab
            // 
            this.tab.Controls.Add(this.k1);
            this.tab.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tab.Location = new System.Drawing.Point(0, 0);
            this.tab.Name = "tab";
            this.tab.SelectedIndex = 0;
            this.tab.Size = new System.Drawing.Size(569, 480);
            this.tab.TabIndex = 3;
            this.tab.Click += new System.EventHandler(this.tab_SelectedIndexChanged);
            this.tab.MouseDown += new System.Windows.Forms.MouseEventHandler(this.tab_MouseDown);
            // 
            // k1
            // 
            this.k1.Location = new System.Drawing.Point(4, 22);
            this.k1.Name = "k1";
            this.k1.Padding = new System.Windows.Forms.Padding(3);
            this.k1.Size = new System.Drawing.Size(561, 454);
            this.k1.TabIndex = 0;
            this.k1.Text = "Query1   X ";
            this.k1.UseVisualStyleBackColor = true;
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.schemaToolStripMenuItem,
            this.openWithAllFieldsToolStripMenuItem,
            this.generateCClassToolStripMenuItem,
            this.generateCClassWCFToolStripMenuItem,
            this.generateClassWithCollectionToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(283, 136);
            // 
            // schemaToolStripMenuItem
            // 
            this.schemaToolStripMenuItem.Name = "schemaToolStripMenuItem";
            this.schemaToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.schemaToolStripMenuItem.Text = "&Schema";
            this.schemaToolStripMenuItem.Click += new System.EventHandler(this.schemaToolStripMenuItem_Click);
            // 
            // openWithAllFieldsToolStripMenuItem
            // 
            this.openWithAllFieldsToolStripMenuItem.Name = "openWithAllFieldsToolStripMenuItem";
            this.openWithAllFieldsToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.openWithAllFieldsToolStripMenuItem.Text = "Open with &all fields";
            this.openWithAllFieldsToolStripMenuItem.Click += new System.EventHandler(this.schemaToolStripMenuItem_Click);
            // 
            // generateCClassToolStripMenuItem
            // 
            this.generateCClassToolStripMenuItem.Name = "generateCClassToolStripMenuItem";
            this.generateCClassToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.generateCClassToolStripMenuItem.Text = "&Generate C# Class";
            this.generateCClassToolStripMenuItem.Click += new System.EventHandler(this.schemaToolStripMenuItem_Click);
            // 
            // generateCClassWCFToolStripMenuItem
            // 
            this.generateCClassWCFToolStripMenuItem.Name = "generateCClassWCFToolStripMenuItem";
            this.generateCClassWCFToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.generateCClassWCFToolStripMenuItem.Text = "Generate C# Class with &WCF decorators";
            this.generateCClassWCFToolStripMenuItem.Click += new System.EventHandler(this.schemaToolStripMenuItem_Click);
            // 
            // generateClassWithCollectionToolStripMenuItem
            // 
            this.generateClassWithCollectionToolStripMenuItem.Name = "generateClassWithCollectionToolStripMenuItem";
            this.generateClassWithCollectionToolStripMenuItem.Size = new System.Drawing.Size(282, 22);
            this.generateClassWithCollectionToolStripMenuItem.Text = "Generate C# Class With C&ollection";
            this.generateClassWithCollectionToolStripMenuItem.Click += new System.EventHandler(this.schemaToolStripMenuItem_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(859, 480);
            this.Controls.Add(this.splitContainer1);
            this.Icon = global::OracleManager.Properties.Resources.Hopstarter_Rounded_Square_Database;
            this.KeyPreview = true;
            this.Name = "MainForm";
            this.Text = "Oracle Manager";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MainForm_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numRowLimit)).EndInit();
            this.tab.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.ListBox lstObjects;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.ComboBox cmbData;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Label lblCount;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numRowLimit;
        private System.Windows.Forms.TabControl tab;
        private System.Windows.Forms.TabPage k1;
        private System.Windows.Forms.Label lblWait;
        private System.Windows.Forms.Button btnExec;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem schemaToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem openWithAllFieldsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateCClassToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem generateCClassWCFToolStripMenuItem;
        private System.Windows.Forms.Button btnDotNetTypes;
        private System.Windows.Forms.ToolStripMenuItem generateClassWithCollectionToolStripMenuItem;
    }
}

