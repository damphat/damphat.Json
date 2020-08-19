namespace damphat.Json.Win
{
    partial class DemoForm
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.txtSrc = new System.Windows.Forms.RichTextBox();
            this.txtDes = new System.Windows.Forms.RichTextBox();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 0);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.txtSrc);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.txtDes);
            this.splitContainer1.Size = new System.Drawing.Size(577, 331);
            this.splitContainer1.SplitterDistance = 192;
            this.splitContainer1.TabIndex = 0;
            this.splitContainer1.Text = "splitContainer1";
            // 
            // txtSrc
            // 
            this.txtSrc.AcceptsTab = true;
            this.txtSrc.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtSrc.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtSrc.Location = new System.Drawing.Point(0, 0);
            this.txtSrc.Name = "txtSrc";
            this.txtSrc.Size = new System.Drawing.Size(192, 331);
            this.txtSrc.TabIndex = 0;
            this.txtSrc.Text = "";
            this.txtSrc.TextChanged += new System.EventHandler(this.txtSrc_TextChanged);
            // 
            // txtDes
            // 
            this.txtDes.AcceptsTab = true;
            this.txtDes.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtDes.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point);
            this.txtDes.Location = new System.Drawing.Point(0, 0);
            this.txtDes.Name = "txtDes";
            this.txtDes.Size = new System.Drawing.Size(381, 331);
            this.txtDes.TabIndex = 0;
            this.txtDes.Text = "";
            // 
            // DemoForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(577, 331);
            this.Controls.Add(this.splitContainer1);
            this.Name = "DemoForm";
            this.Text = "Demo";
            this.Load += new System.EventHandler(this.DemoForm_Load);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox txtSrc;
        private System.Windows.Forms.RichTextBox txtDes;
    }
}

