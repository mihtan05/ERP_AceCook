using System;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace KeToanTaiChinh.Infrastructure
{
    internal static class Ui
    {
        // Bảng màu đồng bộ với giao diện ERP bán hàng mẫu.
        public static readonly Color Primary = Color.FromArgb(22, 119, 238);
        public static readonly Color Sidebar = Color.FromArgb(235, 4, 55);
        public static readonly Color SidebarHover = Color.FromArgb(205, 0, 43);
        public static readonly Color Danger = Color.FromArgb(220, 53, 69);
        public static readonly Color Success = Color.FromArgb(25, 135, 84);
        public static readonly Color Warning = Color.FromArgb(255, 193, 7);
        public static readonly Color Canvas = Color.FromArgb(248, 248, 248);
        public static readonly Color GridBackground = Color.FromArgb(221, 221, 221);

        public static Button Button(string text, EventHandler onClick, Color color)
        {
            Button button = new Button();
            button.Text = text;
            button.AutoSize = false;
            button.Width = 125;
            button.Height = 36;
            button.FlatStyle = FlatStyle.Flat;
            button.FlatAppearance.BorderSize = 0;
            button.BackColor = color;
            button.ForeColor = Color.White;
            button.Font = new Font("Segoe UI", 9.5F, FontStyle.Bold);
            button.Cursor = Cursors.Hand;
            if (onClick != null) button.Click += onClick;
            return button;
        }

        public static Label Title(string text)
        {
            return new Label
            {
                Text = text,
                AutoSize = true,
                Font = new Font("Segoe UI", 20F, FontStyle.Bold),
                ForeColor = Color.Black,
                Margin = new Padding(0, 0, 0, 14)
            };
        }

        public static DataGridView Grid()
        {
            DataGridView grid = new DataGridView();
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AllowUserToDeleteRows = false;
            grid.AllowUserToResizeRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            grid.BackgroundColor = GridBackground;
            grid.BorderStyle = BorderStyle.FixedSingle;
            grid.MultiSelect = false;
            grid.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            grid.RowHeadersVisible = false;
            grid.EnableHeadersVisualStyles = false;
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.White;
            grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.FromArgb(33, 37, 41);
            grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            grid.ColumnHeadersHeight = 40;
            grid.DefaultCellStyle.Font = new Font("Segoe UI", 9F);
            grid.DefaultCellStyle.BackColor = Color.White;
            grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(18, 127, 217);
            grid.DefaultCellStyle.SelectionForeColor = Color.White;
            grid.RowTemplate.Height = 34;
            return grid;
        }

        public static TextBox SearchBox(string placeholder)
        {
            TextBox box = new TextBox();
            box.Width = 560;
            box.Height = 30;
            box.Font = new Font("Segoe UI", 10F);
            box.Text = placeholder;
            box.ForeColor = Color.Gray;
            box.GotFocus += delegate
            {
                if (box.ForeColor == Color.Gray)
                {
                    box.Text = string.Empty;
                    box.ForeColor = Color.Black;
                }
            };
            box.LostFocus += delegate
            {
                if (string.IsNullOrWhiteSpace(box.Text))
                {
                    box.Text = placeholder;
                    box.ForeColor = Color.Gray;
                }
            };
            return box;
        }

        public static void StyleFilter(ComboBox box, int width)
        {
            box.DropDownStyle = ComboBoxStyle.DropDownList;
            box.Width = width;
            box.Height = 30;
            box.Font = new Font("Segoe UI", 9.5F);
            box.FlatStyle = FlatStyle.Flat;
            box.BackColor = Color.White;
        }

        public static Panel PageHeader(string text, Button action)
        {
            TableLayoutPanel panel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 70,
                BackColor = Canvas,
                ColumnCount = 2,
                RowCount = 1,
                Margin = new Padding(0),
                Padding = new Padding(0)
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, 160F));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            Label title = Title(text);
            title.Dock = DockStyle.Fill;
            title.TextAlign = ContentAlignment.MiddleLeft;
            title.Margin = new Padding(4, 0, 0, 0);
            panel.Controls.Add(title, 0, 0);
            if (action != null)
            {
                action.Dock = DockStyle.Fill;
                action.Margin = new Padding(15, 12, 0, 20);
                panel.Controls.Add(action, 1, 0);
            }
            return panel;
        }

        public static Panel SearchFilterBar(TextBox search, ComboBox filter)
        {
            TableLayoutPanel panel = new TableLayoutPanel
            {
                Dock = DockStyle.Top,
                Height = 48,
                BackColor = Canvas,
                ColumnCount = filter == null ? 1 : 2,
                RowCount = 1,
                Padding = new Padding(0),
                Margin = new Padding(0)
            };
            panel.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 100F));
            if (filter != null) panel.ColumnStyles.Add(new ColumnStyle(SizeType.Absolute, filter.Width));
            panel.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));
            search.Dock = DockStyle.Fill;
            search.Margin = new Padding(0, 9, filter == null ? 0 : 16, 9);
            panel.Controls.Add(search, 0, 0);
            if (filter != null)
            {
                filter.Dock = DockStyle.Fill;
                filter.Margin = new Padding(0, 8, 0, 8);
                panel.Controls.Add(filter, 1, 0);
            }
            return panel;
        }

        public static FlowLayoutPanel ActionBar()
        {
            return new FlowLayoutPanel
            {
                Dock = DockStyle.Bottom,
                Height = 54,
                FlowDirection = FlowDirection.RightToLeft,
                Padding = new Padding(0, 9, 0, 5),
                BackColor = Canvas,
                WrapContents = false
            };
        }

        public static string SearchValue(TextBox box)
        {
            return box.ForeColor == Color.Gray ? string.Empty : box.Text.Trim();
        }

        public static void MoneyColumns(DataGridView grid)
        {
            foreach (DataGridViewColumn column in grid.Columns)
            {
                string name = column.DataPropertyName ?? column.Name;
                if (name.IndexOf("Tien", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    name.IndexOf("SoDu", StringComparison.OrdinalIgnoreCase) >= 0 ||
                    name.IndexOf("ThucLinh", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    column.DefaultCellStyle.Format = "N0";
                    column.DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                }
            }
        }

        public static string SelectedId(DataGridView grid, string columnName)
        {
            if (grid.CurrentRow == null || !grid.Columns.Contains(columnName)) return null;
            object value = grid.CurrentRow.Cells[columnName].Value;
            return value == null || value == DBNull.Value ? null : Convert.ToString(value);
        }

        public static bool Confirm(string message, string title)
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo,
                MessageBoxIcon.Question, MessageBoxDefaultButton.Button2) == DialogResult.Yes;
        }

        public static void Info(string message)
        {
            MessageBox.Show(message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public static void Error(Exception ex)
        {
            MessageBox.Show(ex.Message, "Không thể thực hiện", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public static void ExportCsv(DataTable table, IWin32Window owner, string suggestedName)
        {
            if (table == null || table.Rows.Count == 0)
            {
                Info("Không có dữ liệu để xuất.");
                return;
            }

            using (SaveFileDialog dialog = new SaveFileDialog())
            {
                dialog.Filter = "Tệp CSV (*.csv)|*.csv";
                dialog.FileName = suggestedName;
                if (dialog.ShowDialog(owner) != DialogResult.OK) return;

                using (StreamWriter writer = new StreamWriter(dialog.FileName, false, new UTF8Encoding(true)))
                {
                    for (int i = 0; i < table.Columns.Count; i++)
                    {
                        if (i > 0) writer.Write(",");
                        writer.Write(Csv(table.Columns[i].ColumnName));
                    }
                    writer.WriteLine();

                    foreach (DataRow row in table.Rows)
                    {
                        for (int i = 0; i < table.Columns.Count; i++)
                        {
                            if (i > 0) writer.Write(",");
                            object value = row[i];
                            string text = value is DateTime
                                ? ((DateTime)value).ToString("dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture)
                                : Convert.ToString(value, CultureInfo.CurrentCulture);
                            writer.Write(Csv(text));
                        }
                        writer.WriteLine();
                    }
                }
                Info("Đã xuất dữ liệu thành công.");
            }
        }

        private static string Csv(string value)
        {
            string safe = value ?? string.Empty;
            return "\"" + safe.Replace("\"", "\"\"") + "\"";
        }
    }
}
