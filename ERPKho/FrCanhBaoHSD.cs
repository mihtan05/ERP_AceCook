using System;
using System.Data;
using System.Windows.Forms;

namespace ERPKho
{
    public partial class FrCanhBaoHSD : Form
    {
        public FrCanhBaoHSD()
        {
            InitializeComponent();
        }

        private void FrCanhBaoHSD_Load(object sender, EventArgs e)
        {
            DataTable dtCanhBao = new DataTable();
            dtCanhBao.Columns.Add("MaLoHang", typeof(string));
            dtCanhBao.Columns.Add("TenVatTu", typeof(string));
            dtCanhBao.Columns.Add("TenKho", typeof(string));
            dtCanhBao.Columns.Add("HanSuDung", typeof(DateTime));
            dtCanhBao.Columns.Add("SoLuongTonLo", typeof(double));
            dtCanhBao.Columns.Add("CanhBao", typeof(string));

            dtCanhBao.Rows.Add("BATCH-20260315-02", "Gia vị hương tôm", "Kho Nguyên Liệu A1", new DateTime(2026, 10, 10), 350, "Sắp hết hạn (< 30 ngày)");
            dtCanhBao.Rows.Add("BATCH-20251101-05", "Dầu ăn thực vật", "Kho Nguyên Liệu A1", new DateTime(2026, 8, 30), 120, "Đã quá hạn (Cần tiêu hủy)");
            dtCanhBao.Rows.Add("BATCH-20260820-09", "Mì tôm Hảo Hảo", "Kho Thành Phẩm B2", new DateTime(2026, 10, 05), 1500, "Cần ưu tiên xuất trước (FEFO)");

            dgvCanhBao.DataSource = dtCanhBao;
            if (dgvCanhBao.Columns.Contains("HanSuDung"))
                dgvCanhBao.Columns["HanSuDung"].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void btnDong_Click(object sender, EventArgs e) => this.Close();
    }
}