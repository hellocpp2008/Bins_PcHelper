
namespace Bins_PcQuickStart
{
    partial class Form3
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.TestOpenDirBtn = new System.Windows.Forms.Button();
            this.sourceTextBox1 = new System.Windows.Forms.TextBox();
            this.targetTextBox1 = new System.Windows.Forms.TextBox();
            this.resultTextBox1 = new System.Windows.Forms.TextBox();
            this.btnRun = new System.Windows.Forms.Button();
            this.button2Target = new System.Windows.Forms.Button();
            this.button1Source = new System.Windows.Forms.Button();
            this.panelSource = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.panelSource.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            // 
            // TestOpenDirBtn
            // 
            this.TestOpenDirBtn.BackColor = System.Drawing.Color.Linen;
            this.TestOpenDirBtn.Font = new System.Drawing.Font("SimSun", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.TestOpenDirBtn.Location = new System.Drawing.Point(2174, 77);
            this.TestOpenDirBtn.Name = "TestOpenDirBtn";
            this.TestOpenDirBtn.Size = new System.Drawing.Size(117, 110);
            this.TestOpenDirBtn.TabIndex = 0;
            this.TestOpenDirBtn.Text = "测";
            this.TestOpenDirBtn.UseVisualStyleBackColor = false;
            this.TestOpenDirBtn.Click += new System.EventHandler(this.TestOpenDirBtn_Click);
            // 
            // sourceTextBox1
            // 
            this.sourceTextBox1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.sourceTextBox1.Location = new System.Drawing.Point(312, 21);
            this.sourceTextBox1.Multiline = true;
            this.sourceTextBox1.Name = "sourceTextBox1";
            this.sourceTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.sourceTextBox1.Size = new System.Drawing.Size(1690, 363);
            this.sourceTextBox1.TabIndex = 0;
            // 
            // targetTextBox1
            // 
            this.targetTextBox1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.targetTextBox1.Location = new System.Drawing.Point(312, 459);
            this.targetTextBox1.Multiline = true;
            this.targetTextBox1.Name = "targetTextBox1";
            this.targetTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.targetTextBox1.Size = new System.Drawing.Size(1690, 396);
            this.targetTextBox1.TabIndex = 1;
            // 
            // resultTextBox1
            // 
            this.resultTextBox1.Font = new System.Drawing.Font("Microsoft YaHei", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.resultTextBox1.Location = new System.Drawing.Point(312, 926);
            this.resultTextBox1.Multiline = true;
            this.resultTextBox1.Name = "resultTextBox1";
            this.resultTextBox1.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.resultTextBox1.Size = new System.Drawing.Size(1690, 464);
            this.resultTextBox1.TabIndex = 2;
            // 
            // btnRun
            // 
            this.btnRun.BackColor = System.Drawing.Color.Lime;
            this.btnRun.Location = new System.Drawing.Point(42, 1085);
            this.btnRun.Name = "btnRun";
            this.btnRun.Size = new System.Drawing.Size(226, 57);
            this.btnRun.TabIndex = 4;
            this.btnRun.Text = "（第3步）运行";
            this.btnRun.UseVisualStyleBackColor = false;
            this.btnRun.Click += new System.EventHandler(this.btnRun_Click);
            // 
            // button2Target
            // 
            this.button2Target.BackColor = System.Drawing.Color.Wheat;
            this.button2Target.Location = new System.Drawing.Point(42, 602);
            this.button2Target.Name = "button2Target";
            this.button2Target.Size = new System.Drawing.Size(226, 57);
            this.button2Target.TabIndex = 2;
            this.button2Target.Text = "（第2步）目标目录";
            this.button2Target.UseVisualStyleBackColor = false;
            this.button2Target.Click += new System.EventHandler(this.button2Target_Click);
            // 
            // button1Source
            // 
            this.button1Source.BackColor = System.Drawing.Color.Wheat;
            this.button1Source.Location = new System.Drawing.Point(42, 179);
            this.button1Source.Name = "button1Source";
            this.button1Source.Size = new System.Drawing.Size(226, 57);
            this.button1Source.TabIndex = 1;
            this.button1Source.Text = "（第1步）源目录";
            this.button1Source.UseVisualStyleBackColor = false;
            this.button1Source.Click += new System.EventHandler(this.button1Source_Click);
            // 
            // panelSource
            // 
            this.panelSource.BackColor = System.Drawing.Color.Red;
            this.panelSource.Controls.Add(this.textBox1);
            this.panelSource.Controls.Add(this.label1);
            this.panelSource.Controls.Add(this.button1Source);
            this.panelSource.Controls.Add(this.button2Target);
            this.panelSource.Controls.Add(this.btnRun);
            this.panelSource.Controls.Add(this.resultTextBox1);
            this.panelSource.Controls.Add(this.targetTextBox1);
            this.panelSource.Controls.Add(this.sourceTextBox1);
            this.panelSource.Location = new System.Drawing.Point(53, 56);
            this.panelSource.Name = "panelSource";
            this.panelSource.Size = new System.Drawing.Size(2048, 1512);
            this.panelSource.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(28, 273);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 48);
            this.label1.TabIndex = 5;
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Wheat;
            this.textBox1.Font = new System.Drawing.Font("SimSun", 7.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.textBox1.Location = new System.Drawing.Point(42, 266);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(226, 55);
            this.textBox1.TabIndex = 6;
            this.textBox1.Text = "注：子目录中的文件不会处理";
            // 
            // Form3
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 24F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(2357, 1599);
            this.Controls.Add(this.panelSource);
            this.Controls.Add(this.TestOpenDirBtn);
            this.Name = "Form3";
            this.Text = "文件夹复制";
            this.panelSource.ResumeLayout(false);
            this.panelSource.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.Button TestOpenDirBtn;
        private System.Windows.Forms.TextBox sourceTextBox1;
        private System.Windows.Forms.TextBox targetTextBox1;
        private System.Windows.Forms.TextBox resultTextBox1;
        private System.Windows.Forms.Button btnRun;
        private System.Windows.Forms.Button button2Target;
        private System.Windows.Forms.Button button1Source;
        private System.Windows.Forms.Panel panelSource;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
    }
}