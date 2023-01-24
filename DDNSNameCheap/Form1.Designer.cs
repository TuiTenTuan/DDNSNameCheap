namespace DDNSNameCheap
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.notiIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.ctmIcon = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.updateNowToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.settingProfileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.tssLastUpdate = new System.Windows.Forms.ToolStripStatusLabel();
            this.tssIp = new System.Windows.Forms.ToolStripStatusLabel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.btnClear = new System.Windows.Forms.Button();
            this.lvUpdate = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.exportSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.importSettingToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.ctmIcon.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // notiIcon
            // 
            this.notiIcon.ContextMenuStrip = this.ctmIcon;
            this.notiIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notiIcon.Icon")));
            this.notiIcon.Text = "DDNS";
            this.notiIcon.Visible = true;
            this.notiIcon.DoubleClick += new System.EventHandler(this.notiIcon_DoubleClick);
            // 
            // ctmIcon
            // 
            this.ctmIcon.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.aboutToolStripMenuItem,
            this.toolStripSeparator1,
            this.updateNowToolStripMenuItem,
            this.settingProfileToolStripMenuItem,
            this.toolStripSeparator2,
            this.importSettingToolStripMenuItem,
            this.exportSettingToolStripMenuItem,
            this.toolStripSeparator3,
            this.exitToolStripMenuItem});
            this.ctmIcon.Name = "ctmIcon";
            this.ctmIcon.Size = new System.Drawing.Size(181, 176);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.aboutToolStripMenuItem.Text = "About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(177, 6);
            // 
            // updateNowToolStripMenuItem
            // 
            this.updateNowToolStripMenuItem.Name = "updateNowToolStripMenuItem";
            this.updateNowToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.updateNowToolStripMenuItem.Text = "Update Now";
            this.updateNowToolStripMenuItem.Click += new System.EventHandler(this.updateNowToolStripMenuItem_Click);
            // 
            // settingProfileToolStripMenuItem
            // 
            this.settingProfileToolStripMenuItem.Name = "settingProfileToolStripMenuItem";
            this.settingProfileToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.settingProfileToolStripMenuItem.Text = "Setting Profile";
            this.settingProfileToolStripMenuItem.Click += new System.EventHandler(this.settingProfileToolStripMenuItem_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(177, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tssLastUpdate,
            this.tssIp});
            this.statusStrip1.Location = new System.Drawing.Point(0, 339);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(684, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // tssLastUpdate
            // 
            this.tssLastUpdate.Name = "tssLastUpdate";
            this.tssLastUpdate.Size = new System.Drawing.Size(162, 17);
            this.tssLastUpdate.Text = "Last update: 11:43 20/09/2022";
            // 
            // tssIp
            // 
            this.tssIp.Name = "tssIp";
            this.tssIp.Size = new System.Drawing.Size(102, 17);
            this.tssIp.Text = "Current Public Ip: ";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.btnClear, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.lvUpdate, 0, 0);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 13);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(659, 323);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // btnClear
            // 
            this.btnClear.Dock = System.Windows.Forms.DockStyle.Right;
            this.btnClear.Location = new System.Drawing.Point(528, 296);
            this.btnClear.Name = "btnClear";
            this.btnClear.Size = new System.Drawing.Size(128, 24);
            this.btnClear.TabIndex = 0;
            this.btnClear.Text = "Clear Status";
            this.btnClear.UseVisualStyleBackColor = true;
            this.btnClear.Click += new System.EventHandler(this.btnClear_Click);
            // 
            // lvUpdate
            // 
            this.lvUpdate.Alignment = System.Windows.Forms.ListViewAlignment.SnapToGrid;
            this.lvUpdate.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader4,
            this.columnHeader2,
            this.columnHeader3});
            this.lvUpdate.FullRowSelect = true;
            this.lvUpdate.GridLines = true;
            this.lvUpdate.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.lvUpdate.HideSelection = false;
            this.lvUpdate.LabelWrap = false;
            this.lvUpdate.Location = new System.Drawing.Point(3, 3);
            this.lvUpdate.MultiSelect = false;
            this.lvUpdate.Name = "lvUpdate";
            this.lvUpdate.Size = new System.Drawing.Size(653, 287);
            this.lvUpdate.TabIndex = 1;
            this.lvUpdate.UseCompatibleStateImageBehavior = false;
            this.lvUpdate.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "";
            this.columnHeader1.Width = 10;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Date Time";
            this.columnHeader4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.columnHeader4.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Host";
            this.columnHeader2.Width = 180;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Status";
            this.columnHeader3.Width = 338;
            // 
            // exportSettingToolStripMenuItem
            // 
            this.exportSettingToolStripMenuItem.Name = "exportSettingToolStripMenuItem";
            this.exportSettingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.exportSettingToolStripMenuItem.Text = "Export Setting";
            this.exportSettingToolStripMenuItem.Click += new System.EventHandler(this.exportSettingToolStripMenuItem_Click);
            // 
            // importSettingToolStripMenuItem
            // 
            this.importSettingToolStripMenuItem.Name = "importSettingToolStripMenuItem";
            this.importSettingToolStripMenuItem.Size = new System.Drawing.Size(180, 22);
            this.importSettingToolStripMenuItem.Text = "Import Setting";
            this.importSettingToolStripMenuItem.Click += new System.EventHandler(this.importSettingToolStripMenuItem_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            this.toolStripSeparator3.Size = new System.Drawing.Size(177, 6);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.statusStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "DDNS";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ctmIcon.ResumeLayout(false);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notiIcon;
        private System.Windows.Forms.ContextMenuStrip ctmIcon;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem updateNowToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem settingProfileToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.ToolStripStatusLabel tssLastUpdate;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btnClear;
        private System.Windows.Forms.ListView lvUpdate;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ToolStripStatusLabel tssIp;
        private System.Windows.Forms.ToolStripMenuItem importSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem exportSettingToolStripMenuItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
    }
}

