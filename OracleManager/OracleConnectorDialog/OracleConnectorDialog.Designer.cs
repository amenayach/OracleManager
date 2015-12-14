namespace OracleManager
{
    partial class OracleConnectorDialog
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
            this.rdBasic = new System.Windows.Forms.RadioButton();
            this.rdConStr = new System.Windows.Forms.RadioButton();
            this.pnlBasic = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.tbPort = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.tbHost = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.tbSID = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.tbPassword = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.cmbProtocol = new System.Windows.Forms.ComboBox();
            this.tbUsername = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlConStr = new System.Windows.Forms.Panel();
            this.tbFullConStr = new System.Windows.Forms.TextBox();
            this.btnConnect = new System.Windows.Forms.Button();
            this.btnCancel = new System.Windows.Forms.Button();
            this.pnlBasic.SuspendLayout();
            this.pnlConStr.SuspendLayout();
            this.SuspendLayout();
            // 
            // rdBasic
            // 
            this.rdBasic.AutoSize = true;
            this.rdBasic.Checked = true;
            this.rdBasic.Location = new System.Drawing.Point(12, 12);
            this.rdBasic.Name = "rdBasic";
            this.rdBasic.Size = new System.Drawing.Size(51, 17);
            this.rdBasic.TabIndex = 0;
            this.rdBasic.TabStop = true;
            this.rdBasic.Text = "&Basic";
            this.rdBasic.UseVisualStyleBackColor = true;
            this.rdBasic.CheckedChanged += new System.EventHandler(this.rdBasic_CheckedChanged);
            // 
            // rdConStr
            // 
            this.rdConStr.AutoSize = true;
            this.rdConStr.Location = new System.Drawing.Point(12, 144);
            this.rdConStr.Name = "rdConStr";
            this.rdConStr.Size = new System.Drawing.Size(107, 17);
            this.rdConStr.TabIndex = 1;
            this.rdConStr.Text = "Connection &string";
            this.rdConStr.UseVisualStyleBackColor = true;
            // 
            // pnlBasic
            // 
            this.pnlBasic.Controls.Add(this.label6);
            this.pnlBasic.Controls.Add(this.tbPort);
            this.pnlBasic.Controls.Add(this.label5);
            this.pnlBasic.Controls.Add(this.tbHost);
            this.pnlBasic.Controls.Add(this.label4);
            this.pnlBasic.Controls.Add(this.tbSID);
            this.pnlBasic.Controls.Add(this.label3);
            this.pnlBasic.Controls.Add(this.tbPassword);
            this.pnlBasic.Controls.Add(this.label2);
            this.pnlBasic.Controls.Add(this.cmbProtocol);
            this.pnlBasic.Controls.Add(this.tbUsername);
            this.pnlBasic.Controls.Add(this.label1);
            this.pnlBasic.Location = new System.Drawing.Point(12, 35);
            this.pnlBasic.Name = "pnlBasic";
            this.pnlBasic.Size = new System.Drawing.Size(615, 89);
            this.pnlBasic.TabIndex = 0;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(318, 64);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 13);
            this.label6.TabIndex = 11;
            this.label6.Text = "Protocol";
            // 
            // tbPort
            // 
            this.tbPort.Location = new System.Drawing.Point(382, 35);
            this.tbPort.Name = "tbPort";
            this.tbPort.Size = new System.Drawing.Size(227, 20);
            this.tbPort.TabIndex = 4;
            this.tbPort.Text = "1521";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(318, 38);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(26, 13);
            this.label5.TabIndex = 9;
            this.label5.Text = "Port";
            // 
            // tbHost
            // 
            this.tbHost.Location = new System.Drawing.Point(382, 9);
            this.tbHost.Name = "tbHost";
            this.tbHost.Size = new System.Drawing.Size(227, 20);
            this.tbHost.TabIndex = 3;
            this.tbHost.Text = "localhost";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(318, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 13);
            this.label4.TabIndex = 7;
            this.label4.Text = "Host";
            // 
            // tbSID
            // 
            this.tbSID.Location = new System.Drawing.Point(69, 61);
            this.tbSID.Name = "tbSID";
            this.tbSID.Size = new System.Drawing.Size(227, 20);
            this.tbSID.TabIndex = 2;
            this.tbSID.Text = "XE";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(5, 64);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "SID";
            // 
            // tbPassword
            // 
            this.tbPassword.Location = new System.Drawing.Point(69, 35);
            this.tbPassword.Name = "tbPassword";
            this.tbPassword.PasswordChar = '*';
            this.tbPassword.Size = new System.Drawing.Size(227, 20);
            this.tbPassword.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(5, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Password";
            // 
            // cmbProtocol
            // 
            this.cmbProtocol.FormattingEnabled = true;
            this.cmbProtocol.Items.AddRange(new object[] {
            "TCP"});
            this.cmbProtocol.Location = new System.Drawing.Point(382, 61);
            this.cmbProtocol.Name = "cmbProtocol";
            this.cmbProtocol.Size = new System.Drawing.Size(227, 21);
            this.cmbProtocol.TabIndex = 5;
            // 
            // tbUsername
            // 
            this.tbUsername.Location = new System.Drawing.Point(69, 9);
            this.tbUsername.Name = "tbUsername";
            this.tbUsername.Size = new System.Drawing.Size(227, 20);
            this.tbUsername.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(5, 12);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(55, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Username";
            // 
            // pnlConStr
            // 
            this.pnlConStr.Controls.Add(this.tbFullConStr);
            this.pnlConStr.Enabled = false;
            this.pnlConStr.Location = new System.Drawing.Point(12, 167);
            this.pnlConStr.Name = "pnlConStr";
            this.pnlConStr.Size = new System.Drawing.Size(613, 75);
            this.pnlConStr.TabIndex = 1;
            // 
            // tbFullConStr
            // 
            this.tbFullConStr.Location = new System.Drawing.Point(8, 3);
            this.tbFullConStr.Multiline = true;
            this.tbFullConStr.Name = "tbFullConStr";
            this.tbFullConStr.Size = new System.Drawing.Size(601, 69);
            this.tbFullConStr.TabIndex = 0;
            // 
            // btnConnect
            // 
            this.btnConnect.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnConnect.Location = new System.Drawing.Point(465, 248);
            this.btnConnect.Name = "btnConnect";
            this.btnConnect.Size = new System.Drawing.Size(75, 23);
            this.btnConnect.TabIndex = 0;
            this.btnConnect.Text = "&Connect";
            this.btnConnect.UseVisualStyleBackColor = true;
            this.btnConnect.Click += new System.EventHandler(this.btnConnect_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.Location = new System.Drawing.Point(546, 248);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "Ca&ncel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // OracleConnectorDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(629, 277);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.btnConnect);
            this.Controls.Add(this.pnlConStr);
            this.Controls.Add(this.pnlBasic);
            this.Controls.Add(this.rdConStr);
            this.Controls.Add(this.rdBasic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "OracleConnectorDialog";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Oracle Connector Dialog";
            this.Load += new System.EventHandler(this.OracleConnectorDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.OracleConnectorDialog_KeyDown);
            this.pnlBasic.ResumeLayout(false);
            this.pnlBasic.PerformLayout();
            this.pnlConStr.ResumeLayout(false);
            this.pnlConStr.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RadioButton rdBasic;
        private System.Windows.Forms.RadioButton rdConStr;
        private System.Windows.Forms.Panel pnlBasic;
        private System.Windows.Forms.Panel pnlConStr;
        private System.Windows.Forms.TextBox tbFullConStr;
        private System.Windows.Forms.ComboBox cmbProtocol;
        private System.Windows.Forms.TextBox tbUsername;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox tbPort;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox tbHost;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox tbSID;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox tbPassword;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnConnect;
        private System.Windows.Forms.Button btnCancel;
    }
}