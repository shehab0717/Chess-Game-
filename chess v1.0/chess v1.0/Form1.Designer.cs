namespace chess_v1._0
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
            this.button1 = new System.Windows.Forms.Button();
            this.piecesPic = new System.Windows.Forms.ImageList(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.deadWhite = new System.Windows.Forms.Panel();
            this.deadBlack = new System.Windows.Forms.Panel();
            this.BackBtn = new System.Windows.Forms.Button();
            this.ForwardBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.White;
            this.button1.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(44, 9);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(120, 42);
            this.button1.TabIndex = 1;
            this.button1.Text = "New game";
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // piecesPic
            // 
            this.piecesPic.ImageStream = ((System.Windows.Forms.ImageListStreamer)(resources.GetObject("piecesPic.ImageStream")));
            this.piecesPic.TransparentColor = System.Drawing.Color.Transparent;
            this.piecesPic.Images.SetKeyName(0, "Chess_pdt60.png");
            this.piecesPic.Images.SetKeyName(1, "Chess_plt60.png");
            this.piecesPic.Images.SetKeyName(2, "Chess_rdt60.png");
            this.piecesPic.Images.SetKeyName(3, "Chess_rlt60.png");
            this.piecesPic.Images.SetKeyName(4, "Chess_ndt60.png");
            this.piecesPic.Images.SetKeyName(5, "Chess_nlt60.png");
            this.piecesPic.Images.SetKeyName(6, "Chess_bdt60.png");
            this.piecesPic.Images.SetKeyName(7, "Chess_blt60.png");
            this.piecesPic.Images.SetKeyName(8, "Chess_qdt60.png");
            this.piecesPic.Images.SetKeyName(9, "Chess_qlt60.png");
            this.piecesPic.Images.SetKeyName(10, "Chess_kdt60.png");
            this.piecesPic.Images.SetKeyName(11, "Chess_klt60.png");
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Location = new System.Drawing.Point(221, 78);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(640, 640);
            this.panel1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Maroon;
            this.label1.Font = new System.Drawing.Font("Bauhaus 93", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(511, 22);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 28);
            this.label1.TabIndex = 2;
            // 
            // deadWhite
            // 
            this.deadWhite.BackColor = System.Drawing.Color.White;
            this.deadWhite.Location = new System.Drawing.Point(44, 78);
            this.deadWhite.Name = "deadWhite";
            this.deadWhite.Size = new System.Drawing.Size(120, 480);
            this.deadWhite.TabIndex = 3;
            // 
            // deadBlack
            // 
            this.deadBlack.BackColor = System.Drawing.Color.White;
            this.deadBlack.Location = new System.Drawing.Point(918, 78);
            this.deadBlack.Name = "deadBlack";
            this.deadBlack.Size = new System.Drawing.Size(120, 480);
            this.deadBlack.TabIndex = 4;
            // 
            // BackBtn
            // 
            this.BackBtn.BackColor = System.Drawing.Color.White;
            this.BackBtn.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BackBtn.Location = new System.Drawing.Point(221, 22);
            this.BackBtn.Name = "BackBtn";
            this.BackBtn.Size = new System.Drawing.Size(43, 29);
            this.BackBtn.TabIndex = 5;
            this.BackBtn.Text = "B";
            this.BackBtn.UseVisualStyleBackColor = false;
            this.BackBtn.Click += new System.EventHandler(this.BackBtn_Click);
            // 
            // ForwardBtn
            // 
            this.ForwardBtn.BackColor = System.Drawing.Color.White;
            this.ForwardBtn.Font = new System.Drawing.Font("Century Gothic", 10.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForwardBtn.Location = new System.Drawing.Point(290, 22);
            this.ForwardBtn.Name = "ForwardBtn";
            this.ForwardBtn.Size = new System.Drawing.Size(43, 29);
            this.ForwardBtn.TabIndex = 6;
            this.ForwardBtn.Text = "F";
            this.ForwardBtn.UseVisualStyleBackColor = false;
            this.ForwardBtn.Click += new System.EventHandler(this.ForwardBtn_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = global::chess_v1._0.Properties.Resources.Abstract_Digital_Art_Wallpaper_free_high_resolution_amazing_2560x1338;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1082, 792);
            this.Controls.Add(this.ForwardBtn);
            this.Controls.Add(this.BackBtn);
            this.Controls.Add(this.deadBlack);
            this.Controls.Add(this.deadWhite);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.panel1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        public System.Windows.Forms.ImageList piecesPic;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel deadWhite;
        private System.Windows.Forms.Panel deadBlack;
        private System.Windows.Forms.Button BackBtn;
        private System.Windows.Forms.Button ForwardBtn;

    }
}

