namespace netTK
{
    partial class ProgramForm
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
            this.btn_play = new System.Windows.Forms.Button();
            this.btn_step = new System.Windows.Forms.Button();
            this.rdb_pohlepna = new System.Windows.Forms.RadioButton();
            this.rdb_djikstra = new System.Windows.Forms.RadioButton();
            this.rdb_A_star = new System.Windows.Forms.RadioButton();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnSetup = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btn_play
            // 
            this.btn_play.Location = new System.Drawing.Point(12, 139);
            this.btn_play.Name = "btn_play";
            this.btn_play.Size = new System.Drawing.Size(98, 38);
            this.btn_play.TabIndex = 0;
            this.btn_play.Text = "Play/Pause";
            this.btn_play.UseVisualStyleBackColor = true;
            this.btn_play.Click += new System.EventHandler(this.btn_play_Click);
            // 
            // btn_step
            // 
            this.btn_step.Location = new System.Drawing.Point(12, 84);
            this.btn_step.Name = "btn_step";
            this.btn_step.Size = new System.Drawing.Size(98, 38);
            this.btn_step.TabIndex = 1;
            this.btn_step.Text = "Step";
            this.btn_step.UseVisualStyleBackColor = true;
            this.btn_step.Click += new System.EventHandler(this.btn_step_Click);
            // 
            // rdb_pohlepna
            // 
            this.rdb_pohlepna.AutoSize = true;
            this.rdb_pohlepna.Location = new System.Drawing.Point(12, 214);
            this.rdb_pohlepna.Name = "rdb_pohlepna";
            this.rdb_pohlepna.Size = new System.Drawing.Size(89, 21);
            this.rdb_pohlepna.TabIndex = 2;
            this.rdb_pohlepna.TabStop = true;
            this.rdb_pohlepna.Text = "Pohlepna";
            this.rdb_pohlepna.UseVisualStyleBackColor = true;
            // 
            // rdb_djikstra
            // 
            this.rdb_djikstra.AutoSize = true;
            this.rdb_djikstra.Location = new System.Drawing.Point(12, 256);
            this.rdb_djikstra.Name = "rdb_djikstra";
            this.rdb_djikstra.Size = new System.Drawing.Size(76, 21);
            this.rdb_djikstra.TabIndex = 3;
            this.rdb_djikstra.TabStop = true;
            this.rdb_djikstra.Text = "Djikstra";
            this.rdb_djikstra.UseVisualStyleBackColor = true;
            // 
            // rdb_A_star
            // 
            this.rdb_A_star.AutoSize = true;
            this.rdb_A_star.Location = new System.Drawing.Point(12, 300);
            this.rdb_A_star.Name = "rdb_A_star";
            this.rdb_A_star.Size = new System.Drawing.Size(43, 21);
            this.rdb_A_star.TabIndex = 4;
            this.rdb_A_star.TabStop = true;
            this.rdb_A_star.Text = "A*";
            this.rdb_A_star.UseVisualStyleBackColor = true;
            // 
            // timer1
            // 
            this.timer1.Interval = 16;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // btnSetup
            // 
            this.btnSetup.Location = new System.Drawing.Point(12, 29);
            this.btnSetup.Name = "btnSetup";
            this.btnSetup.Size = new System.Drawing.Size(98, 38);
            this.btnSetup.TabIndex = 5;
            this.btnSetup.Text = "Setup";
            this.btnSetup.UseVisualStyleBackColor = true;
            this.btnSetup.Click += new System.EventHandler(this.btnSetup_Click);
            // 
            // ProgramForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(728, 583);
            this.Controls.Add(this.btnSetup);
            this.Controls.Add(this.rdb_A_star);
            this.Controls.Add(this.rdb_djikstra);
            this.Controls.Add(this.rdb_pohlepna);
            this.Controls.Add(this.btn_step);
            this.Controls.Add(this.btn_play);
            this.Name = "ProgramForm";
            this.Text = "ProgramForm";
            this.Load += new System.EventHandler(this.ProgramForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_play;
        private System.Windows.Forms.Button btn_step;
        private System.Windows.Forms.RadioButton rdb_pohlepna;
        private System.Windows.Forms.RadioButton rdb_djikstra;
        private System.Windows.Forms.RadioButton rdb_A_star;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnSetup;
    }
}