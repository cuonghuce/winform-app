namespace ShopSimpleClassic.CustomMessageBox
{
    partial class cMessagebox
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
            this.pnTitle = new System.Windows.Forms.Panel();
            this.picIcon = new System.Windows.Forms.PictureBox();
            this.pnButton = new System.Windows.Forms.Panel();
            this.btDanger = new System.Windows.Forms.Button();
            this.btPrimary = new System.Windows.Forms.Button();
            this.pnContent = new System.Windows.Forms.Panel();
            this.lblContent = new System.Windows.Forms.Label();
            this.pnTitle.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).BeginInit();
            this.pnButton.SuspendLayout();
            this.pnContent.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnTitle
            // 
            this.pnTitle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.pnTitle.Controls.Add(this.picIcon);
            this.pnTitle.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnTitle.Location = new System.Drawing.Point(0, 0);
            this.pnTitle.Name = "pnTitle";
            this.pnTitle.Size = new System.Drawing.Size(358, 85);
            this.pnTitle.TabIndex = 0;
            this.pnTitle.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnTitle_MouseDown);
            // 
            // picIcon
            // 
            this.picIcon.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.picIcon.Image = global::ShopSimpleClassic.Properties.Resources.success;
            this.picIcon.Location = new System.Drawing.Point(139, 2);
            this.picIcon.Name = "picIcon";
            this.picIcon.Size = new System.Drawing.Size(80, 80);
            this.picIcon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picIcon.TabIndex = 0;
            this.picIcon.TabStop = false;
            this.picIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnTitle_MouseDown);
            // 
            // pnButton
            // 
            this.pnButton.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(235)))), ((int)(((byte)(239)))), ((int)(((byte)(255)))));
            this.pnButton.Controls.Add(this.btDanger);
            this.pnButton.Controls.Add(this.btPrimary);
            this.pnButton.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.pnButton.Location = new System.Drawing.Point(0, 232);
            this.pnButton.Name = "pnButton";
            this.pnButton.Size = new System.Drawing.Size(358, 54);
            this.pnButton.TabIndex = 0;
            this.pnButton.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pnTitle_MouseDown);
            // 
            // btDanger
            // 
            this.btDanger.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btDanger.BackColor = System.Drawing.Color.White;
            this.btDanger.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btDanger.ForeColor = System.Drawing.Color.Black;
            this.btDanger.Image = global::ShopSimpleClassic.Properties.Resources.close;
            this.btDanger.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btDanger.Location = new System.Drawing.Point(188, 8);
            this.btDanger.Name = "btDanger";
            this.btDanger.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btDanger.Size = new System.Drawing.Size(109, 39);
            this.btDanger.TabIndex = 0;
            this.btDanger.Text = "Danger";
            this.btDanger.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btDanger.UseVisualStyleBackColor = false;
            // 
            // btPrimary
            // 
            this.btPrimary.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btPrimary.BackColor = System.Drawing.Color.White;
            this.btPrimary.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btPrimary.ForeColor = System.Drawing.Color.Black;
            this.btPrimary.Image = global::ShopSimpleClassic.Properties.Resources.done;
            this.btPrimary.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btPrimary.Location = new System.Drawing.Point(62, 8);
            this.btPrimary.Name = "btPrimary";
            this.btPrimary.Padding = new System.Windows.Forms.Padding(8, 0, 8, 0);
            this.btPrimary.Size = new System.Drawing.Size(109, 39);
            this.btPrimary.TabIndex = 0;
            this.btPrimary.Text = "Primary";
            this.btPrimary.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btPrimary.UseVisualStyleBackColor = false;
            // 
            // pnContent
            // 
            this.pnContent.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(245)))), ((int)(((byte)(247)))), ((int)(((byte)(255)))));
            this.pnContent.Controls.Add(this.lblContent);
            this.pnContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnContent.Location = new System.Drawing.Point(0, 85);
            this.pnContent.Name = "pnContent";
            this.pnContent.Size = new System.Drawing.Size(358, 147);
            this.pnContent.TabIndex = 0;
            // 
            // lblContent
            // 
            this.lblContent.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblContent.AutoSize = true;
            this.lblContent.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblContent.ForeColor = System.Drawing.Color.Black;
            this.lblContent.Location = new System.Drawing.Point(144, 63);
            this.lblContent.MaximumSize = new System.Drawing.Size(340, 0);
            this.lblContent.Name = "lblContent";
            this.lblContent.Size = new System.Drawing.Size(71, 21);
            this.lblContent.TabIndex = 0;
            this.lblContent.Text = "Content";
            // 
            // cMessagebox
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 17F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(358, 286);
            this.Controls.Add(this.pnContent);
            this.Controls.Add(this.pnButton);
            this.Controls.Add(this.pnTitle);
            this.Font = new System.Drawing.Font("Segoe UI", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "cMessagebox";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "cMessagebox";
            this.pnTitle.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picIcon)).EndInit();
            this.pnButton.ResumeLayout(false);
            this.pnContent.ResumeLayout(false);
            this.pnContent.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnTitle;
        private System.Windows.Forms.Panel pnButton;
        private System.Windows.Forms.Panel pnContent;
        private System.Windows.Forms.PictureBox picIcon;
        private System.Windows.Forms.Label lblContent;
        private System.Windows.Forms.Button btPrimary;
        private System.Windows.Forms.Button btDanger;
    }
}