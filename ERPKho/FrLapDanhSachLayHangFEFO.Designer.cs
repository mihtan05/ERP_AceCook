namespace ERPKho
{
    partial class FrLapDanhSachLayHangFEFO
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        private void InitializeComponent()
        {
            this.lblTitle = new System.Windows.Forms.Label();
            this.lblPhieuInfo = new System.Windows.Forms.Label();
            this.lblStatusNotice = new System.Windows.Forms.Label();
            this.dgvPickingList = new System.Windows.Forms.DataGridView();
            this.btnXacNhanVaIn = new System.Windows.Forms.Button();
            this.btnDong = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvPickingList)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblTitle.Location = new System.Drawing.Point(15, 12);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(288, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "DANH SÁCH LẤY HÀNG (FEFO)";
            // 
            // lblPhieuInfo
            // 
            this.lblPhieuInfo.AutoSize = true;
            this.lblPhieuInfo.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Italic);
            this.lblPhieuInfo.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.lblPhieuInfo.Location = new System.Drawing.Point(17, 42);
            this.lblPhieuInfo.Name = "lblPhieuInfo";
            this.lblPhieuInfo.Size = new System.Drawing.Size(183, 17);
            this.lblPhieuInfo.TabIndex = 1;
            this.lblPhieuInfo.Text = "Mã phiếu xuất: --- | Bộ phận: ---";
            // 
            // lblStatusNotice
            // 
            this.lblStatusNotice.AutoSize = true;
            this.lblStatusNotice.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.lblStatusNotice.ForeColor = System.Drawing.Color.Red;
            this.lblStatusNotice.Location = new System.Drawing.Point(17, 63);
            this.lblStatusNotice.Name = "lblStatusNotice";
            this.lblStatusNotice.Size = new System.Drawing.Size(342, 15);
            this.lblStatusNotice.TabIndex = 5;
            this.lblStatusNotice.Text = "🔒 CHẾ ĐỘ CHỈ XEM: Phiếu xuất đã hoàn tất hoặc đã tạo danh sách!";
            this.lblStatusNotice.Visible = false;
            // 
            // dgvPickingList
            // 
            this.dgvPickingList.AllowUserToAddRows = false;
            this.dgvPickingList.AllowUserToDeleteRows = false;
            this.dgvPickingList.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvPickingList.BackgroundColor = System.Drawing.Color.White;
            this.dgvPickingList.ColumnHeadersHeight = 32;
            this.dgvPickingList.Location = new System.Drawing.Point(15, 85);
            this.dgvPickingList.Name = "dgvPickingList";
            this.dgvPickingList.ReadOnly = true;
            this.dgvPickingList.RowHeadersVisible = false;
            this.dgvPickingList.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvPickingList.Size = new System.Drawing.Size(650, 290);
            this.dgvPickingList.TabIndex = 2;
            // 
            // btnXacNhanVaIn
            // 
            this.btnXacNhanVaIn.BackColor = System.Drawing.Color.DarkOrange;
            this.btnXacNhanVaIn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnXacNhanVaIn.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnXacNhanVaIn.ForeColor = System.Drawing.Color.White;
            this.btnXacNhanVaIn.Location = new System.Drawing.Point(385, 388);
            this.btnXacNhanVaIn.Name = "btnXacNhanVaIn";
            this.btnXacNhanVaIn.Size = new System.Drawing.Size(160, 35);
            this.btnXacNhanVaIn.TabIndex = 3;
            this.btnXacNhanVaIn.Text = "Tạo Danh Sách";
            this.btnXacNhanVaIn.UseVisualStyleBackColor = false;
            this.btnXacNhanVaIn.Click += new System.EventHandler(this.btnXacNhanVaIn_Click);
            // 
            // btnDong
            // 
            this.btnDong.BackColor = System.Drawing.Color.Gray;
            this.btnDong.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnDong.Font = new System.Drawing.Font("Segoe UI", 9.5F, System.Drawing.FontStyle.Bold);
            this.btnDong.ForeColor = System.Drawing.Color.White;
            this.btnDong.Location = new System.Drawing.Point(555, 388);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(110, 35);
            this.btnDong.TabIndex = 4;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = false;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click);
            // 
            // FrLapDanhSachLayHangFEFO
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(680, 435);
            this.Controls.Add(this.lblStatusNotice);
            this.Controls.Add(this.btnDong);
            this.Controls.Add(this.btnXacNhanVaIn);
            this.Controls.Add(this.dgvPickingList);
            this.Controls.Add(this.lblPhieuInfo);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrLapDanhSachLayHangFEFO";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Lập danh sách lấy hàng FEFO";
            this.Load += new System.EventHandler(this.FrLapDanhSachLayHangFEFO_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvPickingList)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Label lblPhieuInfo;
        private System.Windows.Forms.Label lblStatusNotice;
        private System.Windows.Forms.DataGridView dgvPickingList;
        private System.Windows.Forms.Button btnXacNhanVaIn;
        private System.Windows.Forms.Button btnDong;
    }
}