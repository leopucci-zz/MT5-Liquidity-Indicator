namespace MT5LiquidityIndicator.Net.View
{
	partial class Prices
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
			this.m_grid = new System.Windows.Forms.DataGridView();
			this.Volume = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Bid = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Ask = new System.Windows.Forms.DataGridViewTextBoxColumn();
			this.Diff = new System.Windows.Forms.DataGridViewTextBoxColumn();
			((System.ComponentModel.ISupportInitialize)(this.m_grid)).BeginInit();
			this.SuspendLayout();
			// 
			// m_grid
			// 
			this.m_grid.AllowUserToAddRows = false;
			this.m_grid.AllowUserToDeleteRows = false;
			this.m_grid.AllowUserToResizeColumns = false;
			this.m_grid.AllowUserToResizeRows = false;
			this.m_grid.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.m_grid.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
			this.m_grid.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Volume,
            this.Bid,
            this.Ask,
            this.Diff});
			this.m_grid.Cursor = System.Windows.Forms.Cursors.Hand;
			this.m_grid.Dock = System.Windows.Forms.DockStyle.Fill;
			this.m_grid.Location = new System.Drawing.Point(0, 0);
			this.m_grid.Name = "m_grid";
			this.m_grid.ReadOnly = true;
			this.m_grid.RowHeadersVisible = false;
			this.m_grid.ScrollBars = System.Windows.Forms.ScrollBars.None;
			this.m_grid.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.CellSelect;
			this.m_grid.Size = new System.Drawing.Size(215, 150);
			this.m_grid.TabIndex = 0;
			this.m_grid.MouseDown += new System.Windows.Forms.MouseEventHandler(this.OnMouseDown);
			this.m_grid.MouseMove += new System.Windows.Forms.MouseEventHandler(this.OnMouseMove);
			// 
			// Volume
			// 
			this.Volume.HeaderText = "Volume";
			this.Volume.Name = "Volume";
			this.Volume.ReadOnly = true;
			this.Volume.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Volume.Width = 50;
			// 
			// Bid
			// 
			this.Bid.HeaderText = "Bid";
			this.Bid.Name = "Bid";
			this.Bid.ReadOnly = true;
			this.Bid.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Bid.Width = 50;
			// 
			// Ask
			// 
			this.Ask.HeaderText = "Ask";
			this.Ask.Name = "Ask";
			this.Ask.ReadOnly = true;
			this.Ask.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Ask.Width = 50;
			// 
			// Diff
			// 
			this.Diff.HeaderText = "Diff";
			this.Diff.Name = "Diff";
			this.Diff.ReadOnly = true;
			this.Diff.SortMode = System.Windows.Forms.DataGridViewColumnSortMode.NotSortable;
			this.Diff.Width = 60;
			// 
			// Spread
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.Controls.Add(this.m_grid);
			this.Name = "Spread";
			this.Size = new System.Drawing.Size(215, 150);
			((System.ComponentModel.ISupportInitialize)(this.m_grid)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.DataGridView m_grid;
		private System.Windows.Forms.DataGridViewTextBoxColumn Volume;
		private System.Windows.Forms.DataGridViewTextBoxColumn Bid;
		private System.Windows.Forms.DataGridViewTextBoxColumn Ask;
		private System.Windows.Forms.DataGridViewTextBoxColumn Diff;
	}
}
