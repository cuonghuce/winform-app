namespace ShopSimpleClassic.View.Manager
{
    partial class fCatalog
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fCatalog));
            this.panel1 = new System.Windows.Forms.Panel();
            this.lblTitle = new System.Windows.Forms.Label();
            this.pnLeft = new System.Windows.Forms.Panel();
            this.pnInformation = new System.Windows.Forms.Panel();
            this.tbCode = new System.Windows.Forms.TextBox();
            this.btAdd = new System.Windows.Forms.Button();
            this.label9 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.tbName = new System.Windows.Forms.TextBox();
            this.btEdit = new System.Windows.Forms.Button();
            this.btClear = new System.Windows.Forms.Button();
            this.btReset = new System.Windows.Forms.Button();
            this.btDelete = new System.Windows.Forms.Button();
            this.panel7 = new System.Windows.Forms.Panel();
            this.panel9 = new System.Windows.Forms.Panel();
            this.panel10 = new System.Windows.Forms.Panel();
            this.tbExport = new System.Windows.Forms.Button();
            this.btRefresh = new System.Windows.Forms.Button();
            this.btExit = new System.Windows.Forms.Button();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.pnRight = new System.Windows.Forms.Panel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.dgvList = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.pnSearch = new System.Windows.Forms.Panel();
            this.lblResult = new System.Windows.Forms.Label();
            this.tbSearch = new System.Windows.Forms.TextBox();
            this.btSearch = new System.Windows.Forms.Button();
            this.panel5 = new System.Windows.Forms.Panel();
            this.cbPage = new System.Windows.Forms.ComboBox();
            this.cbPageSize = new System.Windows.Forms.ComboBox();
            this.btLast = new System.Windows.Forms.Button();
            this.btNext = new System.Windows.Forms.Button();
            this.btPrevious = new System.Windows.Forms.Button();
            this.btFirst = new System.Windows.Forms.Button();
            this.lblPage = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.pnLeft.SuspendLayout();
            this.pnInformation.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel9.SuspendLayout();
            this.panel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel2.SuspendLayout();
            this.pnRight.SuspendLayout();
            this.panel6.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).BeginInit();
            this.pnSearch.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.lblTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1014, 50);
            this.panel1.TabIndex = 0;
            // 
            // lblTitle
            // 
            this.lblTitle.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 22F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblTitle.Location = new System.Drawing.Point(336, 5);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(343, 41);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Quản Lý Loại Sản Phẩm";
            // 
            // pnLeft
            // 
            this.pnLeft.Controls.Add(this.pnInformation);
            this.pnLeft.Controls.Add(this.panel7);
            this.pnLeft.Controls.Add(this.panel2);
            this.pnLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.pnLeft.Location = new System.Drawing.Point(0, 50);
            this.pnLeft.Name = "pnLeft";
            this.pnLeft.Padding = new System.Windows.Forms.Padding(4, 3, 3, 3);
            this.pnLeft.Size = new System.Drawing.Size(237, 597);
            this.pnLeft.TabIndex = 1;
            // 
            // pnInformation
            // 
            this.pnInformation.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.pnInformation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pnInformation.Controls.Add(this.tbCode);
            this.pnInformation.Controls.Add(this.btAdd);
            this.pnInformation.Controls.Add(this.label9);
            this.pnInformation.Controls.Add(this.label4);
            this.pnInformation.Controls.Add(this.label8);
            this.pnInformation.Controls.Add(this.label7);
            this.pnInformation.Controls.Add(this.tbName);
            this.pnInformation.Controls.Add(this.btEdit);
            this.pnInformation.Controls.Add(this.btClear);
            this.pnInformation.Controls.Add(this.btReset);
            this.pnInformation.Controls.Add(this.btDelete);
            this.pnInformation.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnInformation.Location = new System.Drawing.Point(4, 45);
            this.pnInformation.Name = "pnInformation";
            this.pnInformation.Size = new System.Drawing.Size(230, 426);
            this.pnInformation.TabIndex = 1;
            // 
            // tbCode
            // 
            this.tbCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbCode.Location = new System.Drawing.Point(17, 37);
            this.tbCode.Name = "tbCode";
            this.tbCode.Size = new System.Drawing.Size(194, 23);
            this.tbCode.TabIndex = 28;
            // 
            // btAdd
            // 
            this.btAdd.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btAdd.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btAdd.Image = global::ShopSimpleClassic.Properties.Resources.add;
            this.btAdd.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btAdd.Location = new System.Drawing.Point(17, 339);
            this.btAdd.Name = "btAdd";
            this.btAdd.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btAdd.Size = new System.Drawing.Size(94, 37);
            this.btAdd.TabIndex = 18;
            this.btAdd.Text = "Thêm";
            this.btAdd.UseVisualStyleBackColor = true;
            this.btAdd.Click += new System.EventHandler(this.btAdd_Click);
            // 
            // label9
            // 
            this.label9.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Red;
            this.label9.Location = new System.Drawing.Point(121, 12);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(20, 15);
            this.label9.TabIndex = 24;
            this.label9.Text = "(*)";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(17, 12);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(104, 15);
            this.label4.TabIndex = 24;
            this.label4.Text = "Mã loại sản phẩm:";
            // 
            // label8
            // 
            this.label8.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label8.ForeColor = System.Drawing.Color.Red;
            this.label8.Location = new System.Drawing.Point(121, 70);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(20, 15);
            this.label8.TabIndex = 23;
            this.label8.Text = "(*)";
            // 
            // label7
            // 
            this.label7.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.Location = new System.Drawing.Point(17, 70);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(107, 15);
            this.label7.TabIndex = 23;
            this.label7.Text = "Tên loại sản phẩm:";
            // 
            // tbName
            // 
            this.tbName.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.tbName.Location = new System.Drawing.Point(17, 95);
            this.tbName.Name = "tbName";
            this.tbName.Size = new System.Drawing.Size(194, 23);
            this.tbName.TabIndex = 27;
            // 
            // btEdit
            // 
            this.btEdit.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btEdit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btEdit.Image = global::ShopSimpleClassic.Properties.Resources.edit;
            this.btEdit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btEdit.Location = new System.Drawing.Point(117, 339);
            this.btEdit.Name = "btEdit";
            this.btEdit.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btEdit.Size = new System.Drawing.Size(94, 37);
            this.btEdit.TabIndex = 19;
            this.btEdit.Text = "Sửa";
            this.btEdit.UseVisualStyleBackColor = true;
            this.btEdit.Click += new System.EventHandler(this.btEdit_Click);
            // 
            // btClear
            // 
            this.btClear.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btClear.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btClear.Image = global::ShopSimpleClassic.Properties.Resources.clear;
            this.btClear.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btClear.Location = new System.Drawing.Point(17, 382);
            this.btClear.Name = "btClear";
            this.btClear.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btClear.Size = new System.Drawing.Size(94, 37);
            this.btClear.TabIndex = 20;
            this.btClear.Text = "Xóa Trắng";
            this.btClear.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btClear.UseVisualStyleBackColor = true;
            this.btClear.Click += new System.EventHandler(this.btClear_Click);
            // 
            // btReset
            // 
            this.btReset.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btReset.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btReset.Image = global::ShopSimpleClassic.Properties.Resources.reset;
            this.btReset.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btReset.Location = new System.Drawing.Point(117, 382);
            this.btReset.Name = "btReset";
            this.btReset.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btReset.Size = new System.Drawing.Size(94, 37);
            this.btReset.TabIndex = 20;
            this.btReset.Text = "Khôi Phục";
            this.btReset.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btReset.UseVisualStyleBackColor = true;
            this.btReset.Click += new System.EventHandler(this.btReset_Click);
            // 
            // btDelete
            // 
            this.btDelete.Anchor = System.Windows.Forms.AnchorStyles.Bottom;
            this.btDelete.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btDelete.Image = global::ShopSimpleClassic.Properties.Resources.delete;
            this.btDelete.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btDelete.Location = new System.Drawing.Point(117, 296);
            this.btDelete.Name = "btDelete";
            this.btDelete.Padding = new System.Windows.Forms.Padding(3, 0, 0, 0);
            this.btDelete.Size = new System.Drawing.Size(94, 37);
            this.btDelete.TabIndex = 20;
            this.btDelete.Text = "Xóa";
            this.btDelete.UseVisualStyleBackColor = true;
            this.btDelete.Click += new System.EventHandler(this.btDelete_Click);
            // 
            // panel7
            // 
            this.panel7.Controls.Add(this.panel9);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel7.Location = new System.Drawing.Point(4, 471);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(0, 6, 0, 0);
            this.panel7.Size = new System.Drawing.Size(230, 123);
            this.panel7.TabIndex = 0;
            // 
            // panel9
            // 
            this.panel9.Controls.Add(this.panel10);
            this.panel9.Controls.Add(this.panel11);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(0, 6);
            this.panel9.Name = "panel9";
            this.panel9.Size = new System.Drawing.Size(230, 117);
            this.panel9.TabIndex = 1;
            // 
            // panel10
            // 
            this.panel10.BackColor = System.Drawing.Color.White;
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.tbExport);
            this.panel10.Controls.Add(this.btRefresh);
            this.panel10.Controls.Add(this.btExit);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(0, 32);
            this.panel10.Name = "panel10";
            this.panel10.Size = new System.Drawing.Size(230, 85);
            this.panel10.TabIndex = 4;
            // 
            // tbExport
            // 
            this.tbExport.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.tbExport.Cursor = System.Windows.Forms.Cursors.Hand;
            this.tbExport.Image = global::ShopSimpleClassic.Properties.Resources.export_excel;
            this.tbExport.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.tbExport.Location = new System.Drawing.Point(117, 1);
            this.tbExport.Name = "tbExport";
            this.tbExport.Padding = new System.Windows.Forms.Padding(3, 0, 5, 0);
            this.tbExport.Size = new System.Drawing.Size(104, 37);
            this.tbExport.TabIndex = 1;
            this.tbExport.Text = "Xuất Excel";
            this.tbExport.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.tbExport.UseVisualStyleBackColor = true;
            this.tbExport.Click += new System.EventHandler(this.tbExport_Click);
            // 
            // btRefresh
            // 
            this.btRefresh.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btRefresh.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btRefresh.Image = global::ShopSimpleClassic.Properties.Resources.refresh;
            this.btRefresh.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btRefresh.Location = new System.Drawing.Point(7, 44);
            this.btRefresh.Name = "btRefresh";
            this.btRefresh.Padding = new System.Windows.Forms.Padding(3, 0, 5, 0);
            this.btRefresh.Size = new System.Drawing.Size(104, 37);
            this.btRefresh.TabIndex = 1;
            this.btRefresh.Text = "Làm Mới";
            this.btRefresh.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btRefresh.UseVisualStyleBackColor = true;
            this.btRefresh.Click += new System.EventHandler(this.btRefresh_Click);
            // 
            // btExit
            // 
            this.btExit.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btExit.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btExit.Image = global::ShopSimpleClassic.Properties.Resources.close;
            this.btExit.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btExit.Location = new System.Drawing.Point(117, 44);
            this.btExit.Name = "btExit";
            this.btExit.Padding = new System.Windows.Forms.Padding(3, 0, 14, 0);
            this.btExit.Size = new System.Drawing.Size(104, 37);
            this.btExit.TabIndex = 3;
            this.btExit.Text = "Thoát";
            this.btExit.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.btExit.UseVisualStyleBackColor = true;
            this.btExit.Click += new System.EventHandler(this.btExit_Click);
            // 
            // panel11
            // 
            this.panel11.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(55)))), ((int)(((byte)(22)))), ((int)(((byte)(1)))));
            this.panel11.Controls.Add(this.label10);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel11.Location = new System.Drawing.Point(0, 0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(230, 32);
            this.panel11.TabIndex = 3;
            // 
            // label10
            // 
            this.label10.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(68, 6);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(95, 21);
            this.label10.TabIndex = 0;
            this.label10.Text = "Chức Năng";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(59)))), ((int)(((byte)(59)))), ((int)(((byte)(59)))));
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(4, 3);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(230, 42);
            this.panel2.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(71, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(87, 21);
            this.label1.TabIndex = 0;
            this.label1.Text = "Thông Tin";
            // 
            // pnRight
            // 
            this.pnRight.Controls.Add(this.panel6);
            this.pnRight.Controls.Add(this.panel5);
            this.pnRight.Controls.Add(this.panel3);
            this.pnRight.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnRight.Location = new System.Drawing.Point(237, 50);
            this.pnRight.Name = "pnRight";
            this.pnRight.Padding = new System.Windows.Forms.Padding(3, 3, 6, 3);
            this.pnRight.Size = new System.Drawing.Size(777, 597);
            this.pnRight.TabIndex = 1;
            // 
            // panel6
            // 
            this.panel6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.dgvList);
            this.panel6.Controls.Add(this.pnSearch);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(3, 45);
            this.panel6.Name = "panel6";
            this.panel6.Size = new System.Drawing.Size(768, 509);
            this.panel6.TabIndex = 1;
            // 
            // dgvList
            // 
            this.dgvList.AllowUserToAddRows = false;
            this.dgvList.AllowUserToDeleteRows = false;
            this.dgvList.AllowUserToResizeRows = false;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(243)))), ((int)(((byte)(246)))), ((int)(((byte)(247)))));
            this.dgvList.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle1;
            this.dgvList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvList.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.dgvList.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvList.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(189)))), ((int)(((byte)(200)))), ((int)(((byte)(200)))));
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dgvList.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle2;
            this.dgvList.ColumnHeadersHeight = 28;
            this.dgvList.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvList.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2,
            this.Column3});
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(70)))), ((int)(((byte)(67)))));
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(26)))), ((int)(((byte)(70)))), ((int)(((byte)(67)))));
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dgvList.DefaultCellStyle = dataGridViewCellStyle3;
            this.dgvList.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvList.EnableHeadersVisualStyles = false;
            this.dgvList.GridColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(75)))), ((int)(((byte)(76)))));
            this.dgvList.Location = new System.Drawing.Point(0, 42);
            this.dgvList.MultiSelect = false;
            this.dgvList.Name = "dgvList";
            this.dgvList.ReadOnly = true;
            this.dgvList.RowHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            this.dgvList.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(254)))), ((int)(((byte)(255)))));
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(236)))), ((int)(((byte)(162)))), ((int)(((byte)(125)))));
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dgvList.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dgvList.RowTemplate.Height = 28;
            this.dgvList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvList.Size = new System.Drawing.Size(766, 465);
            this.dgvList.TabIndex = 2;
            this.dgvList.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvList_CellClick);
            this.dgvList.CellPainting += new System.Windows.Forms.DataGridViewCellPaintingEventHandler(this.dgvList_CellPainting);
            // 
            // Column1
            // 
            this.Column1.FillWeight = 27.45489F;
            this.Column1.HeaderText = "#";
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.FillWeight = 60.9137F;
            this.Column2.HeaderText = "Mã Loại Sản Phẩm";
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            // 
            // Column3
            // 
            this.Column3.FillWeight = 211.6314F;
            this.Column3.HeaderText = "Tên Loại Sản Phẩm";
            this.Column3.Name = "Column3";
            this.Column3.ReadOnly = true;
            // 
            // pnSearch
            // 
            this.pnSearch.Controls.Add(this.lblResult);
            this.pnSearch.Controls.Add(this.tbSearch);
            this.pnSearch.Controls.Add(this.btSearch);
            this.pnSearch.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnSearch.Location = new System.Drawing.Point(0, 0);
            this.pnSearch.Name = "pnSearch";
            this.pnSearch.Size = new System.Drawing.Size(766, 42);
            this.pnSearch.TabIndex = 1;
            // 
            // lblResult
            // 
            this.lblResult.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblResult.AutoSize = true;
            this.lblResult.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblResult.Location = new System.Drawing.Point(3, 14);
            this.lblResult.Name = "lblResult";
            this.lblResult.Size = new System.Drawing.Size(53, 15);
            this.lblResult.TabIndex = 0;
            this.lblResult.Tag = "Kết quả:";
            this.lblResult.Text = "Kết quả:";
            // 
            // tbSearch
            // 
            this.tbSearch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.tbSearch.Location = new System.Drawing.Point(483, 10);
            this.tbSearch.Name = "tbSearch";
            this.tbSearch.Size = new System.Drawing.Size(278, 23);
            this.tbSearch.TabIndex = 27;
            // 
            // btSearch
            // 
            this.btSearch.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.btSearch.BackgroundImage = global::ShopSimpleClassic.Properties.Resources.search;
            this.btSearch.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btSearch.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btSearch.FlatAppearance.BorderSize = 0;
            this.btSearch.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btSearch.Location = new System.Drawing.Point(452, 9);
            this.btSearch.Name = "btSearch";
            this.btSearch.Size = new System.Drawing.Size(25, 25);
            this.btSearch.TabIndex = 20;
            this.btSearch.UseVisualStyleBackColor = true;
            this.btSearch.Click += new System.EventHandler(this.btSearch_Click);
            // 
            // panel5
            // 
            this.panel5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(247)))), ((int)(((byte)(247)))), ((int)(((byte)(247)))));
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.cbPage);
            this.panel5.Controls.Add(this.cbPageSize);
            this.panel5.Controls.Add(this.btLast);
            this.panel5.Controls.Add(this.btNext);
            this.panel5.Controls.Add(this.btPrevious);
            this.panel5.Controls.Add(this.btFirst);
            this.panel5.Controls.Add(this.lblPage);
            this.panel5.Controls.Add(this.label5);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel5.Location = new System.Drawing.Point(3, 554);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(768, 40);
            this.panel5.TabIndex = 0;
            // 
            // cbPage
            // 
            this.cbPage.Anchor = System.Windows.Forms.AnchorStyles.Right;
            this.cbPage.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbPage.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPage.FormattingEnabled = true;
            this.cbPage.Items.AddRange(new object[] {
            "1",
            "2",
            "3",
            "4",
            "5"});
            this.cbPage.Location = new System.Drawing.Point(649, 8);
            this.cbPage.Name = "cbPage";
            this.cbPage.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.cbPage.Size = new System.Drawing.Size(60, 23);
            this.cbPage.TabIndex = 29;
            this.cbPage.SelectedIndexChanged += new System.EventHandler(this.cbPage_SelectedIndexChanged);
            // 
            // cbPageSize
            // 
            this.cbPageSize.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.cbPageSize.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cbPageSize.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbPageSize.FormattingEnabled = true;
            this.cbPageSize.Items.AddRange(new object[] {
            "10",
            "20",
            "30",
            "40",
            "50",
            "100",
            "150",
            "200",
            "250",
            "300"});
            this.cbPageSize.Location = new System.Drawing.Point(73, 8);
            this.cbPageSize.Name = "cbPageSize";
            this.cbPageSize.Size = new System.Drawing.Size(60, 23);
            this.cbPageSize.TabIndex = 29;
            this.cbPageSize.SelectedIndexChanged += new System.EventHandler(this.cbPageSize_SelectedIndexChanged);
            // 
            // btLast
            // 
            this.btLast.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btLast.BackgroundImage = global::ShopSimpleClassic.Properties.Resources.last;
            this.btLast.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btLast.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btLast.FlatAppearance.BorderSize = 0;
            this.btLast.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btLast.Location = new System.Drawing.Point(437, 7);
            this.btLast.Name = "btLast";
            this.btLast.Size = new System.Drawing.Size(25, 25);
            this.btLast.TabIndex = 20;
            this.btLast.UseVisualStyleBackColor = true;
            this.btLast.Click += new System.EventHandler(this.btLast_Click);
            // 
            // btNext
            // 
            this.btNext.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btNext.BackgroundImage = global::ShopSimpleClassic.Properties.Resources.next;
            this.btNext.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btNext.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btNext.FlatAppearance.BorderSize = 0;
            this.btNext.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btNext.Location = new System.Drawing.Point(393, 7);
            this.btNext.Name = "btNext";
            this.btNext.Size = new System.Drawing.Size(25, 25);
            this.btNext.TabIndex = 20;
            this.btNext.UseVisualStyleBackColor = true;
            this.btNext.Click += new System.EventHandler(this.btNext_Click);
            // 
            // btPrevious
            // 
            this.btPrevious.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btPrevious.BackgroundImage = global::ShopSimpleClassic.Properties.Resources.previous;
            this.btPrevious.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btPrevious.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btPrevious.FlatAppearance.BorderSize = 0;
            this.btPrevious.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btPrevious.Location = new System.Drawing.Point(349, 7);
            this.btPrevious.Name = "btPrevious";
            this.btPrevious.Size = new System.Drawing.Size(25, 25);
            this.btPrevious.TabIndex = 20;
            this.btPrevious.UseVisualStyleBackColor = true;
            this.btPrevious.Click += new System.EventHandler(this.btPrevious_Click);
            // 
            // btFirst
            // 
            this.btFirst.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btFirst.BackgroundImage = global::ShopSimpleClassic.Properties.Resources.first;
            this.btFirst.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btFirst.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btFirst.FlatAppearance.BorderSize = 0;
            this.btFirst.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btFirst.Location = new System.Drawing.Point(305, 7);
            this.btFirst.Name = "btFirst";
            this.btFirst.Size = new System.Drawing.Size(25, 25);
            this.btFirst.TabIndex = 20;
            this.btFirst.UseVisualStyleBackColor = true;
            this.btFirst.Click += new System.EventHandler(this.btFirst_Click);
            // 
            // lblPage
            // 
            this.lblPage.Dock = System.Windows.Forms.DockStyle.Right;
            this.lblPage.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblPage.Location = new System.Drawing.Point(715, 0);
            this.lblPage.Name = "lblPage";
            this.lblPage.Size = new System.Drawing.Size(51, 38);
            this.lblPage.TabIndex = 0;
            this.lblPage.Tag = "/";
            this.lblPage.Text = "/00";
            this.lblPage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Dock = System.Windows.Forms.DockStyle.Left;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(0, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(67, 38);
            this.label5.TabIndex = 0;
            this.label5.Text = "Số dòng:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(75)))), ((int)(((byte)(76)))));
            this.panel3.Controls.Add(this.label2);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel3.Location = new System.Drawing.Point(3, 3);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(768, 42);
            this.panel3.TabIndex = 0;
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(339, 11);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 21);
            this.label2.TabIndex = 0;
            this.label2.Text = "Danh Sách";
            // 
            // fCatalog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1014, 647);
            this.Controls.Add(this.pnRight);
            this.Controls.Add(this.pnLeft);
            this.Controls.Add(this.panel1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(937, 453);
            this.Name = "fCatalog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Quản Lý Loại Sản Phẩm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fCatalog_FormClosing);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.pnLeft.ResumeLayout(false);
            this.pnInformation.ResumeLayout(false);
            this.pnInformation.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel9.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.pnRight.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvList)).EndInit();
            this.pnSearch.ResumeLayout(false);
            this.pnSearch.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel3.ResumeLayout(false);
            this.panel3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Panel pnLeft;
        private System.Windows.Forms.Panel pnRight;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Panel pnSearch;
        private System.Windows.Forms.Label lblResult;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label lblPage;
        private System.Windows.Forms.DataGridView dgvList;
        private System.Windows.Forms.Button btAdd;
        private System.Windows.Forms.TextBox tbName;
        private System.Windows.Forms.Button btEdit;
        private System.Windows.Forms.TextBox tbCode;
        private System.Windows.Forms.Button btDelete;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel pnInformation;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Button btRefresh;
        private System.Windows.Forms.Button btExit;
        private System.Windows.Forms.Panel panel11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Button tbExport;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column3;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox tbSearch;
        private System.Windows.Forms.ComboBox cbPage;
        private System.Windows.Forms.ComboBox cbPageSize;
        private System.Windows.Forms.Button btSearch;
        private System.Windows.Forms.Button btLast;
        private System.Windows.Forms.Button btNext;
        private System.Windows.Forms.Button btPrevious;
        private System.Windows.Forms.Button btFirst;
        private System.Windows.Forms.Button btReset;
        private System.Windows.Forms.Button btClear;
    }
}