using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Reflection;
using System.Windows.Forms;

namespace ERP
{
    public enum ButtonRole
    {
        Primary,     // Thêm mới, Xác nhận (Crimson)
        Secondary,   // Làm mới, Bỏ qua (Slate Gray)
        Report,      // In ấn, Xuất báo cáo (Teal Green #0D9488)
        Edit,        // Sửa, Cập nhật trạng thái (Royal Blue #2563EB)
        Danger       // Xóa, Hủy đơn (Rose Red #DC2626)
    }

    /// <summary>
    /// UIThemeHelper: Bộ công cụ chuẩn hóa giao diện theo quy chuẩn UI/UX Pro Max (Swiss & Modern Enterprise ERP).
    /// Đảm bảo tính nhất quán 100% về bảng màu, typography, DataGridView badge, sidebar và các nút thao tác.
    /// </summary>
    public static class UIThemeHelper
    {
        // Bảng màu chuẩn Enterprise ERP đồng bộ hệ thống
        public static readonly Color ColorSidebar = Color.Crimson;                   // #DC143C
        public static readonly Color ColorSidebarActive = Color.FromArgb(139, 0, 0);  // #8B0000 (DarkRed highlight tab active)
        public static readonly Color ColorSidebarHover = Color.FromArgb(185, 28, 28); // #B91C1C
        public static readonly Color ColorBackground = Color.FromArgb(248, 250, 252);  // Slate 50
        public static readonly Color ColorHeaderBg = Color.White;
        public static readonly Color ColorHeaderText = Color.FromArgb(30, 41, 59);    // Slate 800
        public static readonly Color ColorBorder = Color.FromArgb(226, 232, 240);     // Slate 200

        // Font chữ chuẩn hệ thống Segoe UI
        public static readonly Font FontTitle = new Font("Segoe UI", 12f, FontStyle.Bold);
        public static readonly Font FontSection = new Font("Segoe UI Semibold", 10.5f, FontStyle.Bold);
        public static readonly Font FontBody = new Font("Segoe UI", 9f, FontStyle.Regular);
        public static readonly Font FontBold = new Font("Segoe UI Semibold", 9f, FontStyle.Bold);
        public static readonly Font FontSmall = new Font("Segoe UI", 8.25f, FontStyle.Regular);
        public static readonly Font FontBadge = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold);

        /// <summary>
        /// Chuẩn hóa toàn diện Form (Double buffering chống giật hình, font chữ, màu nền)
        /// </summary>
        public static void ApplyFormStyle(Form form)
        {
            if (form == null) return;

            // Vô hiệu hóa AutoScale để tránh layout bị "phóng to" khi đổi font sau InitializeComponent
            form.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            form.Font = FontBody;
            form.BackColor = ColorBackground;

            // Bật DoubleBuffering cho Form và mọi Control con để loại bỏ giật nháy WinForms
            EnableDoubleBuffering(form);
        }

        /// <summary>
        /// Kích hoạt DoubleBuffering cho control thông qua Reflection
        /// </summary>
        public static void EnableDoubleBuffering(Control control)
        {
            if (control == null) return;
            try
            {
                PropertyInfo pi = typeof(Control).GetProperty("DoubleBuffered", BindingFlags.Instance | BindingFlags.NonPublic);
                pi?.SetValue(control, true, null);
            }
            catch { }

            foreach (Control child in control.Controls)
            {
                EnableDoubleBuffering(child);
            }
        }

        /// <summary>
        /// Chuẩn hóa Sidebar điều hướng đồng bộ với toàn hệ thống ERP
        /// </summary>
        public static void ApplySidebar(Panel pnlSidebar, Button activeButton, Form parentForm)
        {
            if (pnlSidebar == null) return;

            pnlSidebar.BackColor = ColorSidebar;
            // Không thay đổi Width để tránh gây layout shift

            foreach (Control c in pnlSidebar.Controls)
            {
                if (c is Button btn && btn.Name != "btnDangNhap")
                {
                    btn.FlatStyle = FlatStyle.Flat;
                    btn.FlatAppearance.BorderSize = 0;
                    // Chuẩn hóa kích thước đồng đều cho tất cả menu buttons
                    btn.Size = new Size(pnlSidebar.Width, 44);
                    btn.Location = new Point(0, btn.Location.Y);
                    btn.TextAlign = ContentAlignment.MiddleLeft;
                    btn.Padding = new Padding(12, 0, 0, 0); // Giảm padding từ 16 xuống 12 để đủ chỗ cho emoji
                    btn.Cursor = Cursors.Hand;

                    // Tự động chèn Emoji vào text
                    string text = btn.Text;
                    if (btn.Name.Contains("Xe") && !text.Contains("🚚")) btn.Text = "🚚  " + text;
                    else if (btn.Name.Contains("NhaCungCap") && !text.Contains("🏢")) btn.Text = "🏢  " + text;
                    else if (btn.Name.Contains("DiemVanChuyen") && !text.Contains("📍")) btn.Text = "📍  " + text;
                    else if (btn.Name.Contains("TraHang") && !text.Contains("↩️")) btn.Text = "↩️  " + text;
                    else if (btn.Name.Contains("DonVanChuyen") && !text.Contains("📦")) btn.Text = "📦  " + text;

                    if (btn == activeButton)
                    {
                        // Tab đang được chọn: Đổi màu nền tối hơn
                        btn.BackColor = ColorSidebarActive;
                        btn.ForeColor = Color.White;
                        btn.Font = FontBold; // Giữ nguyên font size 9f, không tăng lên 9.5f để tránh lỗi rớt dòng
                        btn.FlatAppearance.MouseOverBackColor = ColorSidebarActive;
                        btn.FlatAppearance.MouseDownBackColor = ColorSidebarActive;
                    }
                    else
                    {
                        btn.BackColor = ColorSidebar;
                        btn.ForeColor = Color.FromArgb(254, 226, 226); // Hồng nhạt nhẹ
                        btn.Font = FontBold;

                        // Hiệu ứng rê chuột
                        btn.MouseEnter -= Button_MouseEnter;
                        btn.MouseEnter += Button_MouseEnter;
                        btn.MouseLeave -= Button_MouseLeave;
                        btn.MouseLeave += Button_MouseLeave;
                    }
                }
            }
        }

        private static void Button_MouseEnter(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BackColor != ColorSidebarActive)
            {
                btn.BackColor = ColorSidebarHover;
                btn.ForeColor = Color.White;
            }
        }

        private static void Button_MouseLeave(object sender, EventArgs e)
        {
            if (sender is Button btn && btn.BackColor != ColorSidebarActive)
            {
                btn.BackColor = ColorSidebar;
                btn.ForeColor = Color.FromArgb(254, 226, 226);
            }
        }

        /// <summary>
        /// Chuẩn hóa DataGridView: Padding 38px, kẻ sọc xen kẽ, tự vẽ Badge trạng thái bo góc mềm mại
        /// </summary>
        public static void ApplyModernGridStyle(DataGridView dgv)
        {
            if (dgv == null) return;

            EnableDoubleBuffering(dgv);

            dgv.BackgroundColor = Color.White;
            dgv.BorderStyle = BorderStyle.None;
            dgv.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgv.GridColor = ColorBorder;
            dgv.EnableHeadersVisualStyles = false;
            dgv.RowHeadersVisible = false;
            dgv.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            dgv.MultiSelect = false;
            dgv.AutoGenerateColumns = false;
            dgv.AllowUserToResizeRows = false;
            dgv.ReadOnly = true; // NGĂN CHẶN EDIT MODE GÂY MẤT MÀU NỀN VÀ HIỆN VIỀN ĐEN

            // Header Style
            dgv.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(241, 245, 249); // Slate 100
            dgv.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(30, 41, 59);    // Slate 800
            dgv.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI Semibold", 9.5f, FontStyle.Bold);
            dgv.ColumnHeadersHeight = 42;
            dgv.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgv.ColumnHeadersBorderStyle = DataGridViewHeaderBorderStyle.None;

            // Row Template
            dgv.RowTemplate.Height = 38;
            dgv.DefaultCellStyle.Font = FontBody;
            dgv.DefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);                  // Slate 700
            dgv.DefaultCellStyle.BackColor = Color.White;
            // Màu selection đủ sáng, đủ tương phản để dòng không bị "mờ" khi click
            dgv.DefaultCellStyle.SelectionBackColor = Color.FromArgb(199, 225, 255);       // Blue 200 (rõ hơn)
            dgv.DefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);          // Slate 900

            // Alternating Row Color
            dgv.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(248, 250, 252); // Slate 50
            dgv.AlternatingRowsDefaultCellStyle.ForeColor = Color.FromArgb(51, 65, 85);   // Slate 700
            dgv.AlternatingRowsDefaultCellStyle.SelectionBackColor = Color.FromArgb(199, 225, 255);
            dgv.AlternatingRowsDefaultCellStyle.SelectionForeColor = Color.FromArgb(15, 23, 42);

            // Gán sự kiện RowPrePaint để vẽ dải highlight rõ ràng khi dòng được chọn
            dgv.RowPrePaint -= Dgv_RowPrePaint;
            dgv.RowPrePaint += Dgv_RowPrePaint;

            // Gán sự kiện vẽ Badge tự động cho các cột Trạng Thái
            dgv.CellPainting -= Dgv_CellPainting;
            dgv.CellPainting += Dgv_CellPainting;
        }

        /// <summary>
        /// Vẽ nền dòng highlight khi dòng được chọn, đảm bảo dòng không bị mờ hoặc biến mất
        /// </summary>
        private static void Dgv_RowPrePaint(object sender, DataGridViewRowPrePaintEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null || e.RowIndex < 0) return;

            // Vẽ nền xanh highlight rõ ràng cho toàn bộ dòng được chọn
            if ((e.State & DataGridViewElementStates.Selected) == DataGridViewElementStates.Selected)
            {
                using (SolidBrush brush = new SolidBrush(Color.FromArgb(199, 225, 255)))
                {
                    e.Graphics.FillRectangle(brush, e.RowBounds);
                }
                // Vẽ đường viền nổi bật bên trái (left accent bar)
                using (SolidBrush accent = new SolidBrush(Color.FromArgb(37, 99, 235)))
                {
                    e.Graphics.FillRectangle(accent, new Rectangle(e.RowBounds.X, e.RowBounds.Y, 3, e.RowBounds.Height));
                }
            }
        }

        /// <summary>
        /// Sự kiện tự vẽ Badge trạng thái (Pill / Chip) theo quy tắc UI/UX Pro Max
        /// </summary>
        private static void Dgv_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0) return;

            DataGridView dgv = sender as DataGridView;
            if (dgv == null) return;

            string colName = dgv.Columns[e.ColumnIndex].Name.ToLower();
            string headerText = dgv.Columns[e.ColumnIndex].HeaderText.ToLower();

            // Nhận diện cột trạng thái
            bool isStatusCol = colName.Contains("trangthai") || colName.Contains("status") ||
                              headerText.Contains("trạng thái") || headerText.Contains("tình trạng");

            bool isSelected = (e.State & DataGridViewElementStates.Selected) != 0;

            if (isStatusCol && e.Value != null)
            {
                e.Handled = true;

                // Vẽ nền dòng với màu selection nếu đang được chọn, hoặc màu row mặc định
                Color rowBg = isSelected
                    ? Color.FromArgb(199, 225, 255)   // Blue 200 - đủ tương phản
                    : (e.RowIndex % 2 == 0 ? Color.White : Color.FromArgb(248, 250, 252));

                using (SolidBrush bgBrush = new SolidBrush(rowBg))
                {
                    e.Graphics.FillRectangle(bgBrush, e.CellBounds);
                }

                // Vẽ đường kẻ dòng ngang phía dưới
                using (Pen linePen = new Pen(ColorBorder, 1))
                {
                    e.Graphics.DrawLine(linePen,
                        e.CellBounds.Left, e.CellBounds.Bottom - 1,
                        e.CellBounds.Right, e.CellBounds.Bottom - 1);
                }

                DrawStatusBadge(e);
            }
            else if (isSelected)
            {
                e.Handled = true;

                // Vẽ nền xanh selection mượt mà, loại bỏ 100% viền đen focus đứt nét
                using (SolidBrush bgBrush = new SolidBrush(Color.FromArgb(199, 225, 255)))
                {
                    e.Graphics.FillRectangle(bgBrush, e.CellBounds);
                }

                // Đường viền ngang phân cách nhẹ nhàng giữa các dòng
                using (Pen linePen = new Pen(ColorBorder, 1))
                {
                    e.Graphics.DrawLine(linePen,
                        e.CellBounds.Left, e.CellBounds.Bottom - 1,
                        e.CellBounds.Right, e.CellBounds.Bottom - 1);
                }

                // Vẽ nội dung chữ của ô (loại bỏ hoàn toàn Focus Rectangle)
                e.Paint(e.ClipBounds, DataGridViewPaintParts.ContentForeground);
            }
        }

        private static void DrawStatusBadge(DataGridViewCellPaintingEventArgs e)
        {
            string text = e.Value?.ToString().Trim();
            if (string.IsNullOrEmpty(text)) return;

            Color badgeBg;
            Color badgeFg;

            // Quy tắc màu ngữ cảnh
            string lower = text.ToLower();
            if (lower.Contains("hoàn thành") || lower.Contains("đã giao") || lower.Contains("sẵn sàng") ||
                lower.Contains("đang rảnh") || lower.Contains("hoạt động") || lower.Contains("đã duyệt"))
            {
                // Green: Hoàn thành, sẵn sàng
                badgeBg = Color.FromArgb(220, 252, 231); // #DCFCE7
                badgeFg = Color.FromArgb(21, 128, 61);   // #15803D
            }
            else if (lower.Contains("đang vận chuyển") || lower.Contains("đang giao") || lower.Contains("đang xử lý"))
            {
                // Blue: Đang vận chuyển
                badgeBg = Color.FromArgb(224, 242, 254); // #E0F2FE
                badgeFg = Color.FromArgb(3, 105, 161);   // #0369A1
            }
            else if (lower.Contains("chờ") || lower.Contains("mới tạo") || lower.Contains("bảo trì") ||
                     lower.Contains("sửa chữa") || lower.Contains("khởi tạo"))
            {
                // Amber / Yellow: Chờ xử lý, bảo trì, khởi tạo
                badgeBg = Color.FromArgb(254, 243, 199); // #FEF3C7
                badgeFg = Color.FromArgb(180, 83, 9);    // #B45309
            }
            else if (lower.Contains("hủy") || lower.Contains("từ chối") || lower.Contains("quá tải") || lower.Contains("hỏng"))
            {
                // Red: Hủy, lỗi
                badgeBg = Color.FromArgb(254, 226, 226); // #FEE2E2
                badgeFg = Color.FromArgb(185, 28, 28);   // #B91C1C
            }
            else
            {
                // Neutral Gray
                badgeBg = Color.FromArgb(241, 245, 249);
                badgeFg = Color.FromArgb(71, 85, 105);
            }

            // Tính toán kích thước Badge bo góc nằm giữa cell
            Size textSize = TextRenderer.MeasureText(text, FontBadge);
            int padX = 14;
            int padY = 5;
            int badgeW = textSize.Width + padX * 2;
            int badgeH = textSize.Height + padY;

            int posX = e.CellBounds.X + (e.CellBounds.Width - badgeW) / 2;
            int posY = e.CellBounds.Y + (e.CellBounds.Height - badgeH) / 2;

            Rectangle badgeRect = new Rectangle(posX, posY, badgeW, badgeH);

            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            // Vẽ nền Pill bo góc
            using (GraphicsPath path = GetRoundedRectanglePath(badgeRect, 10))
            using (SolidBrush brush = new SolidBrush(badgeBg))
            {
                e.Graphics.FillPath(brush, path);
            }

            TextRenderer.DrawText(
                e.Graphics,
                text,
                FontBadge,
                badgeRect,
                badgeFg,
                TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter | TextFormatFlags.SingleLine
            );
        }

        private static GraphicsPath GetRoundedRectanglePath(Rectangle rect, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            int diameter = radius * 2;
            Rectangle arc = new Rectangle(rect.Location, new Size(diameter, diameter));

            path.AddArc(arc, 180, 90);
            arc.X = rect.Right - diameter;
            path.AddArc(arc, 270, 90);
            arc.Y = rect.Bottom - diameter;
            path.AddArc(arc, 0, 90);
            arc.X = rect.Left;
            path.AddArc(arc, 90, 90);
            path.CloseFigure();
            return path;
        }

        /// <summary>
        /// Chuẩn hóa giao diện Nút thao tác (Button Hierarchy)
        /// </summary>
        public static void ApplyActionButton(Button btn, ButtonRole role)
        {
            if (btn == null) return;

            btn.FlatStyle = FlatStyle.Flat;
            btn.FlatAppearance.BorderSize = 0;
            btn.Font = FontBold;
            btn.Cursor = Cursors.Hand;
            btn.ForeColor = Color.White;
            btn.Height = 36;

            switch (role)
            {
                case ButtonRole.Primary:
                    btn.BackColor = ColorSidebar; // Crimson
                    break;
                case ButtonRole.Report:
                    btn.BackColor = Color.FromArgb(13, 148, 136); // Teal 600
                    break;
                case ButtonRole.Edit:
                    btn.BackColor = Color.FromArgb(37, 99, 235);  // Blue 600
                    break;
                case ButtonRole.Danger:
                    btn.BackColor = Color.FromArgb(220, 38, 38);  // Red 600
                    break;
                case ButtonRole.Secondary:
                default:
                    btn.BackColor = Color.FromArgb(100, 116, 139); // Slate 500
                    break;
            }
        }

        /// <summary>
        /// Tạo thẻ KPI Card hiện đại hiển thị số liệu tóm tắt trên đầu màn hình
        /// </summary>
        public static Panel CreateKpiCard(string title, string value, string subtitle, Color accentColor)
        {
            Panel card = new Panel
            {
                Width = 210,
                Height = 72,
                BackColor = Color.White,
                Padding = new Padding(12, 8, 12, 8),
                Margin = new Padding(0, 0, 14, 0)
            };

            // Dải màu nhấn bên trái
            Panel bar = new Panel
            {
                Dock = DockStyle.Left,
                Width = 4,
                BackColor = accentColor
            };
            card.Controls.Add(bar);

            Panel content = new Panel
            {
                Dock = DockStyle.Fill,
                BackColor = Color.Transparent,
                Padding = new Padding(10, 0, 0, 0)
            };

            Label lblTitle = new Label
            {
                Text = title.ToUpper(),
                Font = new Font("Segoe UI Semibold", 7.5f, FontStyle.Bold),
                ForeColor = Color.FromArgb(100, 116, 139), // Slate 500
                Dock = DockStyle.Top,
                Height = 16
            };

            Label lblVal = new Label
            {
                Text = value,
                Font = new Font("Segoe UI", 16f, FontStyle.Bold),
                ForeColor = Color.FromArgb(15, 23, 42),   // Slate 900
                Dock = DockStyle.Fill,
                TextAlign = ContentAlignment.MiddleLeft
            };

            Label lblSub = new Label
            {
                Text = subtitle,
                Font = FontSmall,
                ForeColor = accentColor,
                Dock = DockStyle.Bottom,
                Height = 16
            };

            content.Controls.Add(lblVal);
            content.Controls.Add(lblSub);
            content.Controls.Add(lblTitle);
            card.Controls.Add(content);

            // Viền nhạt
            card.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(ColorBorder, 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, card.Width - 1, card.Height - 1);
                }
            };

            return card;
        }

        /// <summary>
        /// Tạo và chuẩn hóa thanh tìm kiếm & lọc dữ liệu (Filter Toolbar Card) chuẩn UI/UX Pro Max.
        /// Giải quyết dứt điểm lỗi tràn viền, đè chồng controls, bổ sung nhãn rõ ràng và nút Đặt lại tiện dụng.
        /// </summary>
        public static Panel SetupModernFilterCard(
            Panel pnlActionTool, 
            Control searchBox, 
            int searchWidth, 
            Action onReset, 
            params FilterItem[] filterItems)
        {
            if (pnlActionTool == null) return null;

            // Xóa các controls lọc khỏi pnlActionTool để chuyển vào Filter Card
            if (searchBox != null && pnlActionTool.Controls.Contains(searchBox))
                pnlActionTool.Controls.Remove(searchBox);

            if (filterItems != null)
            {
                foreach (var item in filterItems)
                {
                    if (item?.FilterControl != null && pnlActionTool.Controls.Contains(item.FilterControl))
                    {
                        pnlActionTool.Controls.Remove(item.FilterControl);
                    }
                }
            }

            // Tạo Panel Card màu trắng sang trọng
            Panel pnlCard = new Panel
            {
                Name = "pnlFilterCard",
                Height = 48,
                Location = new Point(0, 58),
                Width = pnlActionTool.ClientSize.Width,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                BackColor = Color.White
            };

            // Vẽ viền thanh lịch và đổ bóng nhẹ (border Slate 200 #E2E8F0)
            pnlCard.Paint += (s, e) =>
            {
                using (Pen pen = new Pen(ColorBorder, 1))
                {
                    e.Graphics.DrawRectangle(pen, 0, 0, pnlCard.Width - 1, pnlCard.Height - 1);
                }
            };

            int curX = 12;

            // 1. Ô tìm kiếm
            if (searchBox != null)
            {
                searchBox.Font = FontBody;
                searchBox.Width = searchWidth > 0 ? searchWidth : 280;
                searchBox.Height = 28;
                searchBox.Location = new Point(curX, 10);
                searchBox.Anchor = AnchorStyles.Top | AnchorStyles.Left;

                if (searchBox is TextBox tb)
                {
                    tb.BorderStyle = BorderStyle.FixedSingle;
                }

                pnlCard.Controls.Add(searchBox);
                curX += searchBox.Width + 12;

                // Đường phân cách dọc thanh mảnh
                Panel separator = new Panel
                {
                    Width = 1,
                    Height = 24,
                    Location = new Point(curX, 12),
                    BackColor = ColorBorder
                };
                pnlCard.Controls.Add(separator);
                curX += 14;
            }

            // 2. Các bộ lọc theo ngữ cảnh
            if (filterItems != null)
            {
                foreach (var item in filterItems)
                {
                    if (item == null || item.FilterControl == null) continue;

                    // Nhãn bộ lọc
                    if (!string.IsNullOrEmpty(item.LabelText))
                    {
                        Label lbl = new Label
                        {
                            Text = item.LabelText,
                            Font = new Font("Segoe UI Semibold", 8.5f, FontStyle.Bold),
                            ForeColor = Color.FromArgb(100, 116, 139), // Slate 500
                            AutoSize = true,
                            Location = new Point(curX, 15)
                        };
                        pnlCard.Controls.Add(lbl);
                        curX += lbl.PreferredWidth + 6;
                    }

                    // Control lọc
                    Control ctrl = item.FilterControl;
                    ctrl.Font = FontBody;
                    ctrl.Width = item.ControlWidth > 0 ? item.ControlWidth : 140;
                    ctrl.Height = 28;
                    ctrl.Location = new Point(curX, 10);
                    ctrl.Anchor = AnchorStyles.Top | AnchorStyles.Left;

                    if (ctrl is ComboBox cmb)
                    {
                        cmb.FlatStyle = FlatStyle.Flat;
                        cmb.BackColor = Color.White;
                    }

                    pnlCard.Controls.Add(ctrl);
                    curX += ctrl.Width + 16;
                }
            }

            // 3. Nút đặt lại / làm mới bộ lọc (Reset)
            if (onReset != null)
            {
                Button btnReset = new Button
                {
                    Text = "🔄 Đặt lại",
                    Font = FontBadge,
                    Size = new Size(86, 28),
                    Location = new Point(curX, 10),
                    FlatStyle = FlatStyle.Flat,
                    BackColor = Color.FromArgb(241, 245, 249), // Slate 100
                    ForeColor = Color.FromArgb(71, 85, 105),  // Slate 600
                    Cursor = Cursors.Hand
                };
                btnReset.FlatAppearance.BorderSize = 0;
                btnReset.MouseEnter += (s, e) => btnReset.BackColor = Color.FromArgb(226, 232, 240);
                btnReset.MouseLeave += (s, e) => btnReset.BackColor = Color.FromArgb(241, 245, 249);
                btnReset.Click += (s, e) => onReset();

                pnlCard.Controls.Add(btnReset);
            }

            pnlActionTool.Controls.Add(pnlCard);
            pnlCard.BringToFront();

            return pnlCard;
        }

        /// <summary>
        /// Nâng cấp TopHeader: Breadcrumb điều hướng + Live Clock + User Chip 
        /// Học hỏi từ giao diện HR nhưng giữ lại tone Đỏ Crimson của Logistics
        /// </summary>
        public static void ApplyModernTopHeader(Panel pnlTopHeader, string moduleName, string pageName, Form form)
        {
            if (pnlTopHeader == null) return;

            // Xóa/Ẩn nội dung cũ
            pnlTopHeader.Controls.Clear();
            pnlTopHeader.BackColor = Color.White;
            pnlTopHeader.Height = 56; 

            // --- 1. Panel bên trái: Breadcrumb ---
            FlowLayoutPanel pnlLeft = new FlowLayoutPanel
            {
                FlowDirection = FlowDirection.LeftToRight,
                AutoSize = true,
                AutoSizeMode = AutoSizeMode.GrowAndShrink,
                Location = new Point(20, 18),
                BackColor = Color.Transparent
            };

            Label lblModule = new Label
            {
                Text = moduleName,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Bold),
                ForeColor = ColorSidebar, // Dùng màu Đỏ Crimson làm điểm nhấn
                AutoSize = true,
                Margin = new Padding(0, 0, 5, 0)
            };

            Label lblSeparator = new Label
            {
                Text = "›",
                Font = new Font("Segoe UI", 12f, FontStyle.Regular),
                ForeColor = Color.Silver,
                AutoSize = true,
                Margin = new Padding(0, -3, 5, 0)
            };

            Label lblPage = new Label
            {
                Text = pageName,
                Font = new Font("Segoe UI", 10.5f, FontStyle.Regular),
                ForeColor = ColorHeaderText,
                AutoSize = true,
                Margin = new Padding(0, 0, 0, 0)
            };

            pnlLeft.Controls.Add(lblModule);
            pnlLeft.Controls.Add(lblSeparator);
            pnlLeft.Controls.Add(lblPage);
            pnlTopHeader.Controls.Add(pnlLeft);

            // --- 2. Panel bên phải: Đồng hồ & User Chip ---
            Panel pnlRight = new Panel
            {
                Dock = DockStyle.Right,
                Width = 400,
                BackColor = Color.Transparent
            };

            // User Chip
            Label lblUserChip = new Label
            {
                Text = "👤 Admin [Quản trị viên]",
                Font = new Font("Segoe UI", 9.5f, FontStyle.Bold),
                ForeColor = Color.White,
                BackColor = ColorSidebar, // Đỏ Crimson đồng bộ
                AutoSize = false,
                Size = new Size(200, 32),
                Location = new Point(180, 12),
                TextAlign = ContentAlignment.MiddleCenter,
                Cursor = Cursors.Hand
            };
            // Bo góc User Chip bằng GraphicsPath
            lblUserChip.Paint += (s, e) =>
            {
                e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
                System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
                int radius = 16;
                Rectangle r = new Rectangle(0, 0, lblUserChip.Width, lblUserChip.Height);
                path.AddArc(r.X, r.Y, radius, radius, 180, 90);
                path.AddArc(r.Right - radius, r.Y, radius, radius, 270, 90);
                path.AddArc(r.Right - radius, r.Bottom - radius, radius, radius, 0, 90);
                path.AddArc(r.X, r.Bottom - radius, radius, radius, 90, 90);
                path.CloseFigure();
                lblUserChip.Region = new Region(path);
            };

            // Đồng hồ Live Clock
            Label lblClock = new Label
            {
                Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"),
                Font = new Font("Segoe UI", 10f, FontStyle.Regular),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(10, 17)
            };

            Timer clockTimer = new Timer { Interval = 1000 };
            clockTimer.Tick += (s, e) => {
                if(lblClock.IsDisposed) { clockTimer.Stop(); return; }
                lblClock.Text = DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss");
            };
            clockTimer.Start();

            pnlRight.Controls.Add(lblClock);
            pnlRight.Controls.Add(lblUserChip);
            
            pnlTopHeader.Controls.Add(pnlRight);
        }
    }

    /// <summary>
    /// Định nghĩa thành phần bộ lọc đưa vào Filter Card
    /// </summary>
    public class FilterItem
    {
        public string LabelText { get; set; }
        public Control FilterControl { get; set; }
        public int ControlWidth { get; set; }

        public FilterItem(string labelText, Control filterControl, int controlWidth = 140)
        {
            LabelText = labelText;
            FilterControl = filterControl;
            ControlWidth = controlWidth;
        }
    }
}
