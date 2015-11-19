namespace MT5LiquidityIndicator.Net.View
{
	partial class Chart
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.Windows.Forms.ToolStripMenuItem saveAsCSVToolStripMenuItem;
			this.m_frozeContextMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_viewOptionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_resetPricesWindowPositionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_timer = new System.Windows.Forms.Timer(this.components);
			this.m_contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
			this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
			this.m_saveFileDialog = new System.Windows.Forms.SaveFileDialog();
			this.m_spreads = new MT5LiquidityIndicator.Net.View.Prices();
			saveAsCSVToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
			this.m_contextMenu.SuspendLayout();
			this.SuspendLayout();
			// 
			// saveAsCSVToolStripMenuItem
			// 
			saveAsCSVToolStripMenuItem.Name = "saveAsCSVToolStripMenuItem";
			saveAsCSVToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
			saveAsCSVToolStripMenuItem.Text = "Save as CSV";
			saveAsCSVToolStripMenuItem.Click += new System.EventHandler(this.OnSaveAsCSV);
			// 
			// m_frozeContextMenuItem
			// 
			this.m_frozeContextMenuItem.Name = "m_frozeContextMenuItem";
			this.m_frozeContextMenuItem.Size = new System.Drawing.Size(227, 22);
			this.m_frozeContextMenuItem.Text = "Froze";
			this.m_frozeContextMenuItem.Click += new System.EventHandler(this.OnFroze);
			// 
			// m_viewOptionsToolStripMenuItem
			// 
			this.m_viewOptionsToolStripMenuItem.Name = "m_viewOptionsToolStripMenuItem";
			this.m_viewOptionsToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
			this.m_viewOptionsToolStripMenuItem.Text = "View options";
			this.m_viewOptionsToolStripMenuItem.Click += new System.EventHandler(this.OnOptions);
			// 
			// m_resetPricesWindowPositionToolStripMenuItem
			// 
			this.m_resetPricesWindowPositionToolStripMenuItem.Name = "m_resetPricesWindowPositionToolStripMenuItem";
			this.m_resetPricesWindowPositionToolStripMenuItem.Size = new System.Drawing.Size(227, 22);
			this.m_resetPricesWindowPositionToolStripMenuItem.Text = "Reset prices window position";
			this.m_resetPricesWindowPositionToolStripMenuItem.Click += new System.EventHandler(this.OnResetPricesWindowPosition);
			// 
			// m_timer
			// 
			this.m_timer.Enabled = true;
			this.m_timer.Interval = 500;
			this.m_timer.Tick += new System.EventHandler(this.OnTick);
			// 
			// m_contextMenu
			// 
			this.m_contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.m_viewOptionsToolStripMenuItem,
            this.m_resetPricesWindowPositionToolStripMenuItem,
            this.m_frozeContextMenuItem,
            this.toolStripSeparator2,
            saveAsCSVToolStripMenuItem});
			this.m_contextMenu.Name = "m_contextMenu";
			this.m_contextMenu.Size = new System.Drawing.Size(228, 98);
			// 
			// toolStripSeparator2
			// 
			this.toolStripSeparator2.Name = "toolStripSeparator2";
			this.toolStripSeparator2.Size = new System.Drawing.Size(224, 6);
			// 
			// m_saveFileDialog
			// 
			this.m_saveFileDialog.Filter = "CSV files|*.csv|All files|*.*";
			// 
			// m_spreads
			// 
			this.m_spreads.Cursor = System.Windows.Forms.Cursors.Hand;
			this.m_spreads.Location = new System.Drawing.Point(19, 94);
			this.m_spreads.Name = "m_spreads";
			this.m_spreads.Size = new System.Drawing.Size(215, 121);
			this.m_spreads.TabIndex = 1;
			// 
			// Chart
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ContextMenuStrip = this.m_contextMenu;
			this.Controls.Add(this.m_spreads);
			this.Name = "Chart";
			this.Size = new System.Drawing.Size(672, 318);
			this.Click += new System.EventHandler(this.OnClick);
			this.m_contextMenu.ResumeLayout(false);
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.Timer m_timer;
		private System.Windows.Forms.ContextMenuStrip m_contextMenu;
		private MT5LiquidityIndicator.Net.View.Prices m_spreads;
		private System.Windows.Forms.ToolStripMenuItem m_resetPricesWindowPositionToolStripMenuItem;
		private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
		private System.Windows.Forms.SaveFileDialog m_saveFileDialog;
		private System.Windows.Forms.ToolStripMenuItem m_viewOptionsToolStripMenuItem;
		private System.Windows.Forms.ToolStripMenuItem m_frozeContextMenuItem;
	}
}
