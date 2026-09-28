namespace ERPKho
{
    partial class FrCanhBaoTonKho
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
            this.dgvCanhBao = new System.Windows.Forms.DataGridView();
            this.btnGuiCanhBao = new System.Windows.Forms.Button();
            this.btnThoat = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBao)).BeginInit();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.AutoSize = true;
            this.lblTitle.Font = new System.Drawing.Font("Segoe UI", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.DarkOrange;
            this.lblTitle.Location = new System.Drawing.Point(12, 15);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(262, 25);
            this.lblTitle.TabIndex = 0;
            this.lblTitle.Text = "Cảnh Báo Tồn Kho Thấp / Min";
            // 
            // dgvCanhBao
            // 
            this.dgvCanhBao.AllowUserToAddRows = false;
            this.dgvCanhBao.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvCanhBao.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCanhBao.Location = new System.Drawing.Point(17, 60);
            this.dgvCanhBao.Name = "dgvCanhBao";
            this.dgvCanhBao.ReadOnly = true;
            this.dgvCanhBao.RowHeadersVisible = false;
            this.dgvCanhBao.Size = new System.Drawing.Size(715, 330);
            this.dgvCanhBao.TabIndex = 1;
            // 
            // btnGuiCanhBao
            // 
            this.btnGuiCanhBao.BackColor = System.Drawing.Color.DarkOrange;
            this.btnGuiCanhBao.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnGuiCanhBao.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnGuiCanhBao.ForeColor = System.Drawing.Color.White;
            this.btnGuiCanhBao.Location = new System.Drawing.Point(17, 410);
            this.btnGuiCanhBao.Name = "btnGuiCanhBao";
            this.btnGuiCanhBao.Size = new System.Drawing.Size(160, 30);
            this.btnGuiCanhBao.TabIndex = 2;
            this.btnGuiCanhBao.Text = "Gửi cảnh báo bổ sung";
            this.btnGuiCanhBao.UseVisualStyleBackColor = false;
            this.btnGuiCanhBao.Click += new System.EventHandler(this.btnGuiCanhBao_Click);
            // 
            // btnThoat
            // 
            this.btnThoat.BackColor = System.Drawing.Color.Gray;
            this.btnThoat.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btnThoat.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.btnThoat.ForeColor = System.Drawing.Color.White;
            this.btnThoat.Location = new System.Drawing.Point(632, 410);
            this.btnThoat.Name = "btnThoat";
            this.btnThoat.Size = new System.Drawing.Size(100, 30);
            this.btnThoat.TabIndex = 3;
            this.btnThoat.Text = "Đóng";
            this.btnThoat.UseVisualStyleBackColor = false;
            this.btnThoat.Click += new System.EventHandler(this.btnThoat_Click);
            // 
            // FrCanhBaoTonKho
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(750, 460);
            this.Controls.Add(this.btnThoat);
            this.Controls.Add(this.btnGuiCanhBao);
            this.Controls.Add(this.dgvCanhBao);
            this.Controls.Add(this.lblTitle);
            this.Name = "FrCanhBaoTonKho";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Cảnh Báo Tồn Kho Thấp";
            this.Load += new System.EventHandler(this.FrCanhBaoTonKho_Load);
            ((System.ComponentModel.ISupportInitialize)(this.dgvCanhBao)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGridView dgvCanhBao;
        private System.Windows.Forms.Button btnGuiCanhBao;
        private System.Windows.Forms.Button btnThoat;
    }
}