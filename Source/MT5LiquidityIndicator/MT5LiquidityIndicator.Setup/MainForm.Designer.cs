namespace MT5LiquidityIndicator.Setup
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
			System.Windows.Forms.Button button1;
			this.m_metaTraders = new System.Windows.Forms.CheckedListBox();
			this.m_install = new System.Windows.Forms.Button();
			this.label1 = new System.Windows.Forms.Label();
			this.m_propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.m_dialog = new System.Windows.Forms.FolderBrowserDialog();
			button1 = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// button1
			// 
			button1.Location = new System.Drawing.Point(706, 13);
			button1.Name = "button1";
			button1.Size = new System.Drawing.Size(75, 23);
			button1.TabIndex = 4;
			button1.Text = "Extract";
			button1.UseVisualStyleBackColor = true;
			button1.Click += new System.EventHandler(this.OnExtract);
			// 
			// m_metaTraders
			// 
			this.m_metaTraders.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_metaTraders.FormattingEnabled = true;
			this.m_metaTraders.HorizontalScrollbar = true;
			this.m_metaTraders.Location = new System.Drawing.Point(24, 48);
			this.m_metaTraders.Name = "m_metaTraders";
			this.m_metaTraders.Size = new System.Drawing.Size(847, 229);
			this.m_metaTraders.TabIndex = 0;
			this.m_metaTraders.ItemCheck += new System.Windows.Forms.ItemCheckEventHandler(this.OnCheck);
			this.m_metaTraders.SelectedIndexChanged += new System.EventHandler(this.OnSelected);
			// 
			// m_install
			// 
			this.m_install.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.m_install.Enabled = false;
			this.m_install.Location = new System.Drawing.Point(796, 12);
			this.m_install.Name = "m_install";
			this.m_install.Size = new System.Drawing.Size(75, 23);
			this.m_install.TabIndex = 1;
			this.m_install.Text = "Install";
			this.m_install.UseVisualStyleBackColor = true;
			this.m_install.Click += new System.EventHandler(this.OnInstall);
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(21, 18);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(180, 13);
			this.label1.TabIndex = 2;
			this.label1.Text = "List of detected Meta Trader4 clients";
			// 
			// m_propertyGrid
			// 
			this.m_propertyGrid.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.m_propertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.m_propertyGrid.HelpVisible = false;
			this.m_propertyGrid.Location = new System.Drawing.Point(24, 291);
			this.m_propertyGrid.Name = "m_propertyGrid";
			this.m_propertyGrid.Size = new System.Drawing.Size(847, 195);
			this.m_propertyGrid.TabIndex = 3;
			// 
			// MainForm
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(895, 498);
			this.Controls.Add(button1);
			this.Controls.Add(this.m_propertyGrid);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.m_install);
			this.Controls.Add(this.m_metaTraders);
			this.MinimumSize = new System.Drawing.Size(370, 437);
			this.Name = "MainForm";
			this.Text = "MT5 Liquidity Indicator";
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.CheckedListBox m_metaTraders;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Button m_install;
		private System.Windows.Forms.PropertyGrid m_propertyGrid;
		private System.Windows.Forms.FolderBrowserDialog m_dialog;
	}
}

