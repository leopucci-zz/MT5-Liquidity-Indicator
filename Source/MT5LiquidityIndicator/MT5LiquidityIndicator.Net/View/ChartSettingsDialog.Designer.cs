namespace MT5LiquidityIndicator.Net.View
{
	partial class ChartSettingsDialog
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
			this.m_propertyGrid = new System.Windows.Forms.PropertyGrid();
			this.m_apply = new System.Windows.Forms.Button();
			this.m_save = new System.Windows.Forms.Button();
			this.m_cancel = new System.Windows.Forms.Button();
			this.m_reset = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// m_propertyGrid
			// 
			this.m_propertyGrid.CategoryForeColor = System.Drawing.SystemColors.InactiveCaptionText;
			this.m_propertyGrid.Dock = System.Windows.Forms.DockStyle.Top;
			this.m_propertyGrid.Location = new System.Drawing.Point(0, 0);
			this.m_propertyGrid.Name = "m_propertyGrid";
			this.m_propertyGrid.Size = new System.Drawing.Size(388, 342);
			this.m_propertyGrid.TabIndex = 0;
			// 
			// m_apply
			// 
			this.m_apply.DialogResult = System.Windows.Forms.DialogResult.OK;
			this.m_apply.Location = new System.Drawing.Point(12, 353);
			this.m_apply.Name = "m_apply";
			this.m_apply.Size = new System.Drawing.Size(75, 23);
			this.m_apply.TabIndex = 1;
			this.m_apply.Text = "Apply";
			this.m_apply.UseVisualStyleBackColor = true;
			// 
			// m_save
			// 
			this.m_save.DialogResult = System.Windows.Forms.DialogResult.Yes;
			this.m_save.Location = new System.Drawing.Point(204, 353);
			this.m_save.Name = "m_save";
			this.m_save.Size = new System.Drawing.Size(75, 23);
			this.m_save.TabIndex = 3;
			this.m_save.Text = "Save";
			this.m_save.UseVisualStyleBackColor = true;
			// 
			// m_cancel
			// 
			this.m_cancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.m_cancel.Location = new System.Drawing.Point(300, 353);
			this.m_cancel.Name = "m_cancel";
			this.m_cancel.Size = new System.Drawing.Size(75, 23);
			this.m_cancel.TabIndex = 4;
			this.m_cancel.Text = "Cancel";
			this.m_cancel.UseVisualStyleBackColor = true;
			// 
			// m_reset
			// 
			this.m_reset.Location = new System.Drawing.Point(108, 353);
			this.m_reset.Name = "m_reset";
			this.m_reset.Size = new System.Drawing.Size(75, 23);
			this.m_reset.TabIndex = 2;
			this.m_reset.Text = "Reset";
			this.m_reset.UseVisualStyleBackColor = true;
			this.m_reset.Click += new System.EventHandler(this.OnReset);
			// 
			// ChartSettingsDialog
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(388, 388);
			this.Controls.Add(this.m_reset);
			this.Controls.Add(this.m_cancel);
			this.Controls.Add(this.m_save);
			this.Controls.Add(this.m_apply);
			this.Controls.Add(this.m_propertyGrid);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "ChartSettingsDialog";
			this.ShowInTaskbar = false;
			this.Text = "Chart settings of ";
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.PropertyGrid m_propertyGrid;
		private System.Windows.Forms.Button m_apply;
		private System.Windows.Forms.Button m_save;
		private System.Windows.Forms.Button m_cancel;
		private System.Windows.Forms.Button m_reset;
	}
}