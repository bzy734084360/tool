namespace TransPDF
{
    partial class Form1
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要修改
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.btnimg = new System.Windows.Forms.Button();
            this.btnword = new System.Windows.Forms.Button();
            this.btnhtml = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.txtimgb = new System.Windows.Forms.TextBox();
            this.txtwordb = new System.Windows.Forms.TextBox();
            this.txthtmlb = new System.Windows.Forms.TextBox();
            this.txtimga = new System.Windows.Forms.TextBox();
            this.txtworda = new System.Windows.Forms.TextBox();
            this.txthtmla = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnimg
            // 
            this.btnimg.Location = new System.Drawing.Point(741, 32);
            this.btnimg.Name = "btnimg";
            this.btnimg.Size = new System.Drawing.Size(104, 34);
            this.btnimg.TabIndex = 0;
            this.btnimg.Text = "图片生成PDF";
            this.btnimg.UseVisualStyleBackColor = true;
            this.btnimg.Click += new System.EventHandler(this.btnimg_Click);
            // 
            // btnword
            // 
            this.btnword.Location = new System.Drawing.Point(741, 109);
            this.btnword.Name = "btnword";
            this.btnword.Size = new System.Drawing.Size(104, 34);
            this.btnword.TabIndex = 1;
            this.btnword.Text = "Word生成PDF";
            this.btnword.UseVisualStyleBackColor = true;
            this.btnword.Click += new System.EventHandler(this.btnword_Click);
            // 
            // btnhtml
            // 
            this.btnhtml.Location = new System.Drawing.Point(741, 185);
            this.btnhtml.Name = "btnhtml";
            this.btnhtml.Size = new System.Drawing.Size(104, 34);
            this.btnhtml.TabIndex = 2;
            this.btnhtml.Text = "HTML生成PDF";
            this.btnhtml.UseVisualStyleBackColor = true;
            this.btnhtml.Click += new System.EventHandler(this.btnhtml_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Location = new System.Drawing.Point(13, 43);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "图片路径";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.Transparent;
            this.label2.Location = new System.Drawing.Point(13, 120);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "word路径";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(13, 196);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 5;
            this.label3.Text = "html路径";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(372, 43);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 6;
            this.label4.Text = "生成路径";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(372, 120);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(53, 12);
            this.label5.TabIndex = 7;
            this.label5.Text = "生成路径";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(372, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(53, 12);
            this.label6.TabIndex = 8;
            this.label6.Text = "生成路径";
            // 
            // txtimgb
            // 
            this.txtimgb.Location = new System.Drawing.Point(72, 40);
            this.txtimgb.Name = "txtimgb";
            this.txtimgb.Size = new System.Drawing.Size(283, 21);
            this.txtimgb.TabIndex = 9;
            // 
            // txtwordb
            // 
            this.txtwordb.Location = new System.Drawing.Point(72, 117);
            this.txtwordb.Name = "txtwordb";
            this.txtwordb.Size = new System.Drawing.Size(283, 21);
            this.txtwordb.TabIndex = 10;
            // 
            // txthtmlb
            // 
            this.txthtmlb.Location = new System.Drawing.Point(72, 193);
            this.txthtmlb.Name = "txthtmlb";
            this.txthtmlb.Size = new System.Drawing.Size(283, 21);
            this.txthtmlb.TabIndex = 11;
            // 
            // txtimga
            // 
            this.txtimga.Location = new System.Drawing.Point(431, 40);
            this.txtimga.Name = "txtimga";
            this.txtimga.Size = new System.Drawing.Size(283, 21);
            this.txtimga.TabIndex = 12;
            // 
            // txtworda
            // 
            this.txtworda.Location = new System.Drawing.Point(431, 117);
            this.txtworda.Name = "txtworda";
            this.txtworda.Size = new System.Drawing.Size(283, 21);
            this.txtworda.TabIndex = 13;
            // 
            // txthtmla
            // 
            this.txthtmla.Location = new System.Drawing.Point(431, 193);
            this.txthtmla.Name = "txthtmla";
            this.txthtmla.Size = new System.Drawing.Size(283, 21);
            this.txthtmla.TabIndex = 14;
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(883, 307);
            this.Controls.Add(this.txthtmla);
            this.Controls.Add(this.txtworda);
            this.Controls.Add(this.txtimga);
            this.Controls.Add(this.txthtmlb);
            this.Controls.Add(this.txtwordb);
            this.Controls.Add(this.txtimgb);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnhtml);
            this.Controls.Add(this.btnword);
            this.Controls.Add(this.btnimg);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PDF转换工具";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnimg;
        private System.Windows.Forms.Button btnword;
        private System.Windows.Forms.Button btnhtml;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtimgb;
        private System.Windows.Forms.TextBox txtwordb;
        private System.Windows.Forms.TextBox txthtmlb;
        private System.Windows.Forms.TextBox txtimga;
        private System.Windows.Forms.TextBox txtworda;
        private System.Windows.Forms.TextBox txthtmla;
    }
}

