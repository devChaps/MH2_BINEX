namespace MH2_BINEX
{
    partial class FRM_MAIN
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FRM_MAIN));
            this.AFS_LV = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader4 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader5 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader6 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.Debug_Log = new System.Windows.Forms.RichTextBox();
            this.PopUp_Menu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.extractSelectedToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.sldunpackToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.searchToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MainMenu = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readImageToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.readAFSToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.displayToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fontColorToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.debugLogToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.enableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.disableToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.refreshAllToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.tv_root = new System.Windows.Forms.TreeView();
            this.DataStrip = new System.Windows.Forms.StatusStrip();
            this.lbl_VolSel = new System.Windows.Forms.ToolStripStatusLabel();
            this.ProgressBar00 = new System.Windows.Forms.ToolStripProgressBar();
            this.lbl_relativeOffset = new System.Windows.Forms.ToolStripStatusLabel();
            this.Lbl_FCount = new System.Windows.Forms.ToolStripStatusLabel();
            this.lbl_stats = new System.Windows.Forms.ToolStripStatusLabel();
            this.Font_DLg = new System.Windows.Forms.FontDialog();
            this.PopUp_Menu.SuspendLayout();
            this.MainMenu.SuspendLayout();
            this.DataStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // AFS_LV
            // 
            this.AFS_LV.BackColor = System.Drawing.Color.Black;
            this.AFS_LV.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3,
            this.columnHeader4,
            this.columnHeader5,
            this.columnHeader6});
            this.AFS_LV.Font = new System.Drawing.Font("Moire", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.AFS_LV.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.AFS_LV.FullRowSelect = true;
            this.AFS_LV.HideSelection = false;
            this.AFS_LV.Location = new System.Drawing.Point(216, 27);
            this.AFS_LV.Name = "AFS_LV";
            this.AFS_LV.Size = new System.Drawing.Size(856, 705);
            this.AFS_LV.TabIndex = 0;
            this.AFS_LV.UseCompatibleStateImageBehavior = false;
            this.AFS_LV.View = System.Windows.Forms.View.Details;
            this.AFS_LV.ItemActivate += new System.EventHandler(this.AFS_LV_ItemActivate);
            this.AFS_LV.SelectedIndexChanged += new System.EventHandler(this.AFS_LV_SelectedIndexChanged);
            this.AFS_LV.MouseClick += new System.Windows.Forms.MouseEventHandler(this.AFS_LV_MouseClick);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Index";
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Offset";
            this.columnHeader2.Width = 69;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Size";
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Name";
            this.columnHeader4.Width = 139;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Description";
            this.columnHeader5.Width = 170;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Date";
            this.columnHeader6.Width = 163;
            // 
            // Debug_Log
            // 
            this.Debug_Log.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.Debug_Log.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Debug_Log.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.Debug_Log.Location = new System.Drawing.Point(209, 407);
            this.Debug_Log.Name = "Debug_Log";
            this.Debug_Log.Size = new System.Drawing.Size(243, 124);
            this.Debug_Log.TabIndex = 2;
            this.Debug_Log.Text = "";
            this.Debug_Log.Visible = false;
            // 
            // PopUp_Menu
            // 
            this.PopUp_Menu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.extractSelectedToolStripMenuItem,
            this.sldunpackToolStripMenuItem,
            this.searchToolStripMenuItem});
            this.PopUp_Menu.Name = "PopUp_Menu";
            this.PopUp_Menu.Size = new System.Drawing.Size(157, 70);
            // 
            // extractSelectedToolStripMenuItem
            // 
            this.extractSelectedToolStripMenuItem.Name = "extractSelectedToolStripMenuItem";
            this.extractSelectedToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.extractSelectedToolStripMenuItem.Text = "Extract Selected";
            this.extractSelectedToolStripMenuItem.Click += new System.EventHandler(this.extractSelectedToolStripMenuItem_Click);
            // 
            // sldunpackToolStripMenuItem
            // 
            this.sldunpackToolStripMenuItem.Name = "sldunpackToolStripMenuItem";
            this.sldunpackToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.sldunpackToolStripMenuItem.Text = "sld_unpack";
            this.sldunpackToolStripMenuItem.Click += new System.EventHandler(this.sldunpackToolStripMenuItem_Click);
            // 
            // searchToolStripMenuItem
            // 
            this.searchToolStripMenuItem.Name = "searchToolStripMenuItem";
            this.searchToolStripMenuItem.Size = new System.Drawing.Size(156, 22);
            this.searchToolStripMenuItem.Text = "Search >";
            this.searchToolStripMenuItem.Click += new System.EventHandler(this.searchToolStripMenuItem_Click);
            // 
            // MainMenu
            // 
            this.MainMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.aboutToolStripMenuItem,
            this.aboutToolStripMenuItem1});
            this.MainMenu.Location = new System.Drawing.Point(0, 0);
            this.MainMenu.Name = "MainMenu";
            this.MainMenu.Size = new System.Drawing.Size(1072, 24);
            this.MainMenu.TabIndex = 7;
            this.MainMenu.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.readImageToolStripMenuItem,
            this.readAFSToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "File";
            // 
            // readImageToolStripMenuItem
            // 
            this.readImageToolStripMenuItem.Name = "readImageToolStripMenuItem";
            this.readImageToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.O)));
            this.readImageToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.readImageToolStripMenuItem.Text = "Read Image";
            this.readImageToolStripMenuItem.Click += new System.EventHandler(this.readImageToolStripMenuItem_Click);
            // 
            // readAFSToolStripMenuItem
            // 
            this.readAFSToolStripMenuItem.Enabled = false;
            this.readAFSToolStripMenuItem.Name = "readAFSToolStripMenuItem";
            this.readAFSToolStripMenuItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)));
            this.readAFSToolStripMenuItem.Size = new System.Drawing.Size(179, 22);
            this.readAFSToolStripMenuItem.Text = "Read AFS";
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.displayToolStripMenuItem,
            this.refreshAllToolStripMenuItem});
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(61, 20);
            this.aboutToolStripMenuItem.Text = "Options";
            // 
            // displayToolStripMenuItem
            // 
            this.displayToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fontColorToolStripMenuItem,
            this.debugLogToolStripMenuItem});
            this.displayToolStripMenuItem.Name = "displayToolStripMenuItem";
            this.displayToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.displayToolStripMenuItem.Text = "Display";
            // 
            // fontColorToolStripMenuItem
            // 
            this.fontColorToolStripMenuItem.Name = "fontColorToolStripMenuItem";
            this.fontColorToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.fontColorToolStripMenuItem.Text = "Font/Color";
            this.fontColorToolStripMenuItem.Click += new System.EventHandler(this.fontColorToolStripMenuItem_Click);
            // 
            // debugLogToolStripMenuItem
            // 
            this.debugLogToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.enableToolStripMenuItem,
            this.disableToolStripMenuItem});
            this.debugLogToolStripMenuItem.Enabled = false;
            this.debugLogToolStripMenuItem.Name = "debugLogToolStripMenuItem";
            this.debugLogToolStripMenuItem.Size = new System.Drawing.Size(132, 22);
            this.debugLogToolStripMenuItem.Text = "Debug Log";
            // 
            // enableToolStripMenuItem
            // 
            this.enableToolStripMenuItem.Enabled = false;
            this.enableToolStripMenuItem.Name = "enableToolStripMenuItem";
            this.enableToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.enableToolStripMenuItem.Text = "Enable";
            this.enableToolStripMenuItem.Click += new System.EventHandler(this.enableToolStripMenuItem_Click);
            // 
            // disableToolStripMenuItem
            // 
            this.disableToolStripMenuItem.Enabled = false;
            this.disableToolStripMenuItem.Name = "disableToolStripMenuItem";
            this.disableToolStripMenuItem.Size = new System.Drawing.Size(112, 22);
            this.disableToolStripMenuItem.Text = "Disable";
            // 
            // refreshAllToolStripMenuItem
            // 
            this.refreshAllToolStripMenuItem.Name = "refreshAllToolStripMenuItem";
            this.refreshAllToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.F5;
            this.refreshAllToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.refreshAllToolStripMenuItem.Text = "Refresh All";
            this.refreshAllToolStripMenuItem.Click += new System.EventHandler(this.refreshAllToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem1
            // 
            this.aboutToolStripMenuItem1.Name = "aboutToolStripMenuItem1";
            this.aboutToolStripMenuItem1.Size = new System.Drawing.Size(52, 20);
            this.aboutToolStripMenuItem1.Text = "About";
            this.aboutToolStripMenuItem1.Click += new System.EventHandler(this.aboutToolStripMenuItem1_Click);
            // 
            // tv_root
            // 
            this.tv_root.BackColor = System.Drawing.Color.Black;
            this.tv_root.ForeColor = System.Drawing.SystemColors.ButtonFace;
            this.tv_root.Location = new System.Drawing.Point(0, 27);
            this.tv_root.Name = "tv_root";
            this.tv_root.Size = new System.Drawing.Size(210, 705);
            this.tv_root.TabIndex = 8;
            this.tv_root.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.tv_root_NodeMouseDoubleClick);
            // 
            // DataStrip
            // 
            this.DataStrip.Font = new System.Drawing.Font("Moire", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DataStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.lbl_VolSel,
            this.ProgressBar00,
            this.lbl_relativeOffset,
            this.Lbl_FCount,
            this.lbl_stats});
            this.DataStrip.Location = new System.Drawing.Point(0, 736);
            this.DataStrip.Name = "DataStrip";
            this.DataStrip.Size = new System.Drawing.Size(1072, 23);
            this.DataStrip.TabIndex = 9;
            this.DataStrip.Text = "statusStrip1";
            // 
            // lbl_VolSel
            // 
            this.lbl_VolSel.Name = "lbl_VolSel";
            this.lbl_VolSel.Size = new System.Drawing.Size(19, 18);
            this.lbl_VolSel.Text = "-*";
            // 
            // ProgressBar00
            // 
            this.ProgressBar00.ForeColor = System.Drawing.Color.YellowGreen;
            this.ProgressBar00.Name = "ProgressBar00";
            this.ProgressBar00.Size = new System.Drawing.Size(250, 17);
            this.ProgressBar00.Style = System.Windows.Forms.ProgressBarStyle.Continuous;
            // 
            // lbl_relativeOffset
            // 
            this.lbl_relativeOffset.Name = "lbl_relativeOffset";
            this.lbl_relativeOffset.Size = new System.Drawing.Size(20, 18);
            this.lbl_relativeOffset.Text = "--";
            // 
            // Lbl_FCount
            // 
            this.Lbl_FCount.Name = "Lbl_FCount";
            this.Lbl_FCount.Size = new System.Drawing.Size(85, 18);
            this.Lbl_FCount.Text = "Total Files";
            // 
            // lbl_stats
            // 
            this.lbl_stats.Name = "lbl_stats";
            this.lbl_stats.Size = new System.Drawing.Size(681, 18);
            this.lbl_stats.Spring = true;
            this.lbl_stats.Text = "....................";
            // 
            // Font_DLg
            // 
            this.Font_DLg.Apply += new System.EventHandler(this.Font_DLg_Apply);
            // 
            // FRM_MAIN
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1072, 759);
            this.Controls.Add(this.DataStrip);
            this.Controls.Add(this.tv_root);
            this.Controls.Add(this.MainMenu);
            this.Controls.Add(this.AFS_LV);
            this.Controls.Add(this.Debug_Log);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.MainMenu;
            this.Name = "FRM_MAIN";
            this.Text = "Monster Hunter Virtual File Explorer v1.1  Dchaps 2/17/2016";
            this.PopUp_Menu.ResumeLayout(false);
            this.MainMenu.ResumeLayout(false);
            this.MainMenu.PerformLayout();
            this.DataStrip.ResumeLayout(false);
            this.DataStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ListView AFS_LV;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.RichTextBox Debug_Log;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ContextMenuStrip PopUp_Menu;
        private System.Windows.Forms.ToolStripMenuItem extractSelectedToolStripMenuItem;
        private System.Windows.Forms.MenuStrip MainMenu;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readImageToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem readAFSToolStripMenuItem;
        private System.Windows.Forms.TreeView tv_root;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem1;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.StatusStrip DataStrip;
        private System.Windows.Forms.ToolStripStatusLabel lbl_VolSel;
        private System.Windows.Forms.ToolStripProgressBar ProgressBar00;
        private System.Windows.Forms.ToolStripStatusLabel lbl_relativeOffset;
        private System.Windows.Forms.ToolStripMenuItem displayToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem debugLogToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem enableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem disableToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fontColorToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem refreshAllToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel Lbl_FCount;
        private System.Windows.Forms.ToolStripMenuItem sldunpackToolStripMenuItem;
        private System.Windows.Forms.FontDialog Font_DLg;
        private System.Windows.Forms.ToolStripMenuItem searchToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel lbl_stats;
    }
}

