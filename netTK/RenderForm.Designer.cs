namespace netTK
{
    partial class RenderForm
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
            this.renderCanvas = new OpenTK.GLControl();
            this.btnSnapToCenter = new System.Windows.Forms.Button();
            this.trackBar1 = new System.Windows.Forms.TrackBar();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtXcor = new System.Windows.Forms.TextBox();
            this.txtYcor = new System.Windows.Forms.TextBox();
            this.chkAxes = new System.Windows.Forms.CheckBox();
            this.chkGrid = new System.Windows.Forms.CheckBox();
            this.chkPatchBorders = new System.Windows.Forms.CheckBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).BeginInit();
            this.SuspendLayout();
            // 
            // renderCanvas
            // 
            this.renderCanvas.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.renderCanvas.AutoSize = true;
            this.renderCanvas.BackColor = System.Drawing.Color.Black;
            this.renderCanvas.Location = new System.Drawing.Point(14, 17);
            this.renderCanvas.Margin = new System.Windows.Forms.Padding(5);
            this.renderCanvas.Name = "renderCanvas";
            this.renderCanvas.Size = new System.Drawing.Size(735, 650);
            this.renderCanvas.TabIndex = 0;
            this.renderCanvas.VSync = false;
            this.renderCanvas.Paint += new System.Windows.Forms.PaintEventHandler(this.renderCanvas_Paint);
            this.renderCanvas.MouseDown += new System.Windows.Forms.MouseEventHandler(this.renderCanvas_MouseDown);
            this.renderCanvas.MouseMove += new System.Windows.Forms.MouseEventHandler(this.renderCanvas_MouseMove);
            this.renderCanvas.MouseUp += new System.Windows.Forms.MouseEventHandler(this.renderCanvas_MouseUp);
            this.renderCanvas.Resize += new System.EventHandler(this.renderCanvas_Resize);
            // 
            // btnSnapToCenter
            // 
            this.btnSnapToCenter.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btnSnapToCenter.Location = new System.Drawing.Point(916, 76);
            this.btnSnapToCenter.Name = "btnSnapToCenter";
            this.btnSnapToCenter.Size = new System.Drawing.Size(77, 51);
            this.btnSnapToCenter.TabIndex = 1;
            this.btnSnapToCenter.Text = "Snap to center";
            this.btnSnapToCenter.UseVisualStyleBackColor = true;
            this.btnSnapToCenter.Click += new System.EventHandler(this.btnSnapToCenter_Click);
            // 
            // trackBar1
            // 
            this.trackBar1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.trackBar1.Cursor = System.Windows.Forms.Cursors.Default;
            this.trackBar1.LargeChange = 10;
            this.trackBar1.Location = new System.Drawing.Point(937, 189);
            this.trackBar1.Maximum = 100;
            this.trackBar1.Minimum = 1;
            this.trackBar1.Name = "trackBar1";
            this.trackBar1.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trackBar1.Size = new System.Drawing.Size(56, 333);
            this.trackBar1.TabIndex = 3;
            this.trackBar1.TickFrequency = 5;
            this.trackBar1.Value = 10;
            this.trackBar1.Scroll += new System.EventHandler(this.trackBar1_Scroll);
            // 
            // timer1
            // 
            this.timer1.Interval = 1;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(892, 20);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 5;
            this.label1.Text = "X:";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(892, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 17);
            this.label2.TabIndex = 6;
            this.label2.Text = "Y:";
            // 
            // txtXcor
            // 
            this.txtXcor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtXcor.Enabled = false;
            this.txtXcor.Location = new System.Drawing.Point(919, 17);
            this.txtXcor.Name = "txtXcor";
            this.txtXcor.Size = new System.Drawing.Size(74, 22);
            this.txtXcor.TabIndex = 7;
            // 
            // txtYcor
            // 
            this.txtYcor.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.txtYcor.Enabled = false;
            this.txtYcor.Location = new System.Drawing.Point(919, 45);
            this.txtYcor.Name = "txtYcor";
            this.txtYcor.Size = new System.Drawing.Size(74, 22);
            this.txtYcor.TabIndex = 8;
            // 
            // chkAxes
            // 
            this.chkAxes.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkAxes.AutoSize = true;
            this.chkAxes.Checked = true;
            this.chkAxes.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkAxes.Location = new System.Drawing.Point(933, 602);
            this.chkAxes.Name = "chkAxes";
            this.chkAxes.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkAxes.Size = new System.Drawing.Size(60, 21);
            this.chkAxes.TabIndex = 9;
            this.chkAxes.Text = "Axes";
            this.chkAxes.UseVisualStyleBackColor = true;
            this.chkAxes.CheckedChanged += new System.EventHandler(this.chkAxes_CheckedChanged);
            // 
            // chkGrid
            // 
            this.chkGrid.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkGrid.AutoSize = true;
            this.chkGrid.Location = new System.Drawing.Point(936, 629);
            this.chkGrid.Name = "chkGrid";
            this.chkGrid.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkGrid.Size = new System.Drawing.Size(57, 21);
            this.chkGrid.TabIndex = 9;
            this.chkGrid.Text = "Grid";
            this.chkGrid.UseVisualStyleBackColor = true;
            this.chkGrid.CheckedChanged += new System.EventHandler(this.chkGrid_CheckedChanged);
            // 
            // chkPatchBorders
            // 
            this.chkPatchBorders.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.chkPatchBorders.AutoSize = true;
            this.chkPatchBorders.Checked = true;
            this.chkPatchBorders.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkPatchBorders.Location = new System.Drawing.Point(873, 656);
            this.chkPatchBorders.Name = "chkPatchBorders";
            this.chkPatchBorders.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.chkPatchBorders.Size = new System.Drawing.Size(120, 21);
            this.chkPatchBorders.TabIndex = 9;
            this.chkPatchBorders.Text = "Patch Borders";
            this.chkPatchBorders.UseVisualStyleBackColor = true;
            this.chkPatchBorders.CheckedChanged += new System.EventHandler(this.chkPatchBorders_CheckedChanged);
            // 
            // RenderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1005, 689);
            this.Controls.Add(this.chkPatchBorders);
            this.Controls.Add(this.chkGrid);
            this.Controls.Add(this.chkAxes);
            this.Controls.Add(this.txtYcor);
            this.Controls.Add(this.txtXcor);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.trackBar1);
            this.Controls.Add(this.btnSnapToCenter);
            this.Controls.Add(this.renderCanvas);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "RenderForm";
            this.Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)(this.trackBar1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public OpenTK.GLControl renderCanvas;
        private System.Windows.Forms.Button btnSnapToCenter;
        private System.Windows.Forms.TrackBar trackBar1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtXcor;
        private System.Windows.Forms.TextBox txtYcor;
        private System.Windows.Forms.CheckBox chkAxes;
        private System.Windows.Forms.CheckBox chkGrid;
        private System.Windows.Forms.CheckBox chkPatchBorders;
    }
}

