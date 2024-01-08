using ShopSimpleClassic.View;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Excel = Microsoft.Office.Interop.Excel;

namespace ShopSimpleClassic.Library
{
    public class Lib
    {
        #region Common

        public static bool isNumeric(string str)
        {
            foreach (char c in str)
            {
                if (!char.IsDigit(c) && c != '.' && c != ',')
                {
                    return false;
                }
            }
            return true;
        }

        public static void ClearText(Control parent)
        => parent.Controls.OfType<TextBox>().ToList().ForEach(i => i.Clear());

        public static void ClearCombobox(Control parent)
        => parent.Controls.OfType<ComboBox>().ToList().ForEach(i => i.SelectedIndex = -1);

        public static bool ControlNotEmpty(Control parent)
        => parent.Controls.OfType<TextBox>().Count(i => string.IsNullOrEmpty(i.Text.Trim())) == 0;

        public static bool ControlNotEmpty(Panel parent)
        => parent.Controls.OfType<TextBox>().Count(i => string.IsNullOrEmpty(i.Text.Trim())) == 0;

        // Hàm tự động chuyển trạng thái cho phép thao tác hoặc không cho phép
        public static void ReadOnlyControl(Control parent, bool isReadOnly)
        {
            var tbs = parent.Controls.OfType<TextBox>();
            var cbs = parent.Controls.OfType<ComboBox>();
            var chbs = parent.Controls.OfType<CheckBox>();

            tbs.ToList().ForEach(i => i.ReadOnly = isReadOnly);
            cbs.ToList().ForEach(i => i.Enabled = !isReadOnly);
            chbs.ToList().ForEach(i => i.Enabled = !isReadOnly);
        }

        // Hàm tự động chuyển trạng thái cho phép thao tác hoặc không cho phép
        public static void ReadOnlyControlButton(Control parent, Button[] skipButtons, bool isReadOnly)
        {
            var buttons = parent.Controls.OfType<Button>().ToList();
            var controls = buttons.ToArray().Except(skipButtons).ToArray();
            controls.ToList().ForEach(i => i.Visible = isReadOnly);
        }

        public static int CheckAndShowErrorProvider(Control control, ErrorProvider errorProvider)
        {
            if (string.IsNullOrEmpty(control.Text.Trim()))
            {
                errorProvider.SetError(control, "Không được để trống!");
                return 1;
            }
            else
            {
                errorProvider.SetError(control, "");
                return 0;
            }
        }

        public static bool CheckInputNotEmpty(Panel parent, ErrorProvider errorProvider)
        {
            int count = 0;
            parent.Controls.OfType<TextBox>().ToList().ForEach(i => count += CheckAndShowErrorProvider(i, errorProvider));
            return count == 0;
        }

        public static bool CheckInputNotEmpty(TextBox[] textboxs, ErrorProvider errorProvider)
        {
            int count = 0;
            textboxs.ToList().ForEach(i => count += CheckAndShowErrorProvider(i, errorProvider));
            return count == 0;
        }

        public static bool CheckInputNotEmpty(TextBox[] textboxs, TextBox[] skipTextboxs, ErrorProvider errorProvider)
        {
            int count = 0;
            var controls = textboxs.Except(skipTextboxs).ToArray();
            controls.ToList().ForEach(i => count += CheckAndShowErrorProvider(i, errorProvider));
            return count == 0;
        }

        // Hàm thay đổi định dạng tiền, Nếu định dạng không hoạt động thì chuyển Regional format thành Vietnamese, hoặc đổi price.Replace(".", "") => price.Replace(",", "") nếu như Regional là Eng US
        public static string ConvertPrice(string price, bool isFormat)
        {
            try
            {
                if (string.IsNullOrEmpty(price)) return "";
                return isFormat ? String.Format("{0:#,####}", long.Parse(price)) : price.Replace(".", "");
            }
            catch
            {
                return price;
            }
        }

        // Hàm lấy dữ liệu từ định dạng tiền
        public static int ConvertIntFromPrice(TextBox tb)
        {
            var t = tb.Text.Trim();

            if (string.IsNullOrEmpty(t)) return 0;
            return int.Parse(ConvertPrice(t, false));
        }

        // định dạng hiển thị ngày cho textbox
        public static string ConvertDateToString(DateTime date)
        {
            return $"{date.Day.ToString("d2")}/{date.Month.ToString("d2")}/{date.Year} {date.Hour.ToString("d2")}:{date.Minute.ToString("d2")}:{date.Second.ToString("d2")}";
        }

        // căn chỉnh cho control nằm ở giữa
        public static void CenterControl(Control parent, Control control)
        => control.Location = new Point
        (
            (parent.Width - control.Width) / 2,
            (parent.Height - control.Height) / 2
        );

        // căn chỉnh cho control nằm ở giữa theo chiều ngang
        public static void CenterControl_Horizontal(Control parent, Control control)
        => control.Left = (parent.Width - control.Width) / 2;

        // căn chỉnh cho control nằm ở giữa theo chiều dọc
        public static void CenterControl_Vertical(Control parent, Control control)
        => control.Top = (parent.Height - control.Height) / 2;

        #endregion Common

        #region Export Excel

        public static void ExportExcel(DataGridView dataGridView, string title)
        {
            int rowHeight = 20;
            if (!Directory.Exists(ExportFolderPath()))
            {
                Directory.CreateDirectory(ExportFolderPath());
            }

            // Đường dẫn lưu file Excel
            string filePath = Path.Combine(ExportFolderPath(), $"{title}.xlsx");

            using (SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Excel Workbook|*.xlsx";
                sfd.Title = "Chọn Nơi Lưu Tập Tin";
                sfd.FileName = $"{title}.xlsx";

                DialogResult dr = sfd.ShowDialog();

                if (dr == DialogResult.OK)
                {
                    filePath = Path.Combine(Path.GetDirectoryName(sfd.FileName), $"{title}.xlsx");
                }
                else
                {
                    if (ShowMess.Question__CustomText("Bạn có muốn lưu tập tin Excel vào thư mục ứng dụng?") == DialogResult.No)
                        return;
                }

                eForm.ShowWaitForm();
            }

            // Tạo một đối tượng Excel mới
            Excel.Application excelApp = new Excel.Application();
            excelApp.Visible = false;

            // Tạo một workbook mới và mở nó
            Excel.Workbook workbook = excelApp.Workbooks.Add();
            Excel.Worksheet worksheet = workbook.ActiveSheet;
            worksheet.Name = $"Danh Sách {title}";

            // Đặt tiêu đề cho toàn bộ sheet
            string sheetTitle = $"Danh Sách {title}";
            worksheet.Cells[1, 1] = sheetTitle;
            Excel.Range titleRange = worksheet.Range[worksheet.Cells[1, 1], worksheet.Cells[1, dataGridView.Columns.Count]];
            titleRange.Merge(); // Merge cells
            titleRange.Font.Bold = true;
            titleRange.Interior.Color = Color.FromArgb(180, 198, 231); // Màu nền tiêu đề cột
            titleRange.HorizontalAlignment = Excel.XlHAlign.xlHAlignCenter;
            titleRange.Rows.VerticalAlignment = HorizontalAlignment.Center;
            titleRange.Rows.RowHeight = 30;

            // Đặt tiêu đề cho các cột trong file Excel và thêm màu nền
            Excel.Range headerRange = worksheet.Range[worksheet.Cells[2, 1], worksheet.Cells[2, dataGridView.Columns.Count]];
            headerRange.Value = dataGridView.Columns.Cast<DataGridViewColumn>().Select(c => c.HeaderText).ToArray();
            headerRange.Interior.Color = Color.FromArgb(180, 198, 231); // Màu nền tiêu đề cột
            headerRange.Font.Bold = true;

            // Lấy dữ liệu từ DataGridView và ghi vào Excel
            for (int i = 0; i < dataGridView.Rows.Count; i++)
            {
                Excel.Range data = worksheet.Range[worksheet.Cells[i + 3, 1], worksheet.Cells[i + 3, dataGridView.Columns.Count]];

                // Gán màu sắc cho dòng dữ liệu
                data.Interior.Color = i % 2 == 0 ? Color.White : Color.FromArgb(230, 247, 255);

                for (int j = 0; j < dataGridView.Columns.Count; j++)
                {
                    string value = dataGridView.Rows[i].Cells[j].Value.ToString();
                    // Kiểm tra nếu giá trị là số tiền
                    decimal amount;
                    if (decimal.TryParse(value, out amount))
                    {
                        // Định dạng số tiền và lưu giá trị dưới dạng chuỗi
                        value = ConvertPrice(value, false);
                    }

                    if (dataGridView.Rows[i].Cells[j].GetType() == typeof(DataGridViewImageCell))
                    {
                        rowHeight = 50;
                        // Chuyển đổi hình ảnh thành dạng byte array
                        System.Drawing.Bitmap img = (System.Drawing.Bitmap)dataGridView.Rows[i].Cells[j].Value;
                        byte[] imageData = ImageToByteArray(img);
                        // Tạo tên tạm cho tập tin hình ảnh
                        string tempImagePath = Path.GetTempFileName() + ".jpg";
                        // Lưu dữ liệu hình ảnh vào tập tin tạm
                        File.WriteAllBytes(tempImagePath, imageData);

                        // Thêm hình ảnh vào file Excel
                        Excel.Range imageCell = data.Cells[1, j + 1];
                        float left = (float)imageCell.Left + 2;
                        float top = (float)imageCell.Top + 2;
                        float width = (float)rowHeight - 4;
                        float height = (float)rowHeight - 4;

                        worksheet.Shapes.AddPicture(tempImagePath, Microsoft.Office.Core.MsoTriState.msoFalse, Microsoft.Office.Core.MsoTriState.msoCTrue, left, top, width, height);
                        // Xóa tập tin hình ảnh tạm sau khi sử dụng
                        File.Delete(tempImagePath);
                    }
                    else
                    {
                        // Gán giá trị vào cell và định dạng là kiểu chuỗi
                        Excel.Range cell = data.Cells[1, j + 1];
                        cell.Value = value;
                    }
                }
            }

            // Auto-fit cột sau khi ghi dữ liệu
            Excel.Range columnRange = worksheet.UsedRange.Columns;
            columnRange.AutoFit();
            // Auto-fit cột sau khi ghi dữ liệu
            Excel.Range rowsRange = worksheet.UsedRange.Rows;
            rowsRange.AutoFit();

            // Thay đổi chiều cao của hàng
            Excel.Range usedRange = worksheet.UsedRange;
            Excel.Range dataRows = usedRange.Offset[1, 0].Resize[usedRange.Rows.Count - 1]; // Loại bỏ hàng tiêu đề
            dataRows.Rows.RowHeight = rowHeight;
            dataRows.Rows.VerticalAlignment = HorizontalAlignment.Center;

            // Thêm đường viền cho dữ liệu
            Excel.Range dataRange = worksheet.UsedRange;
            Excel.Borders borders = dataRange.Borders;
            borders.LineStyle = Excel.XlLineStyle.xlContinuous;
            borders.Weight = Excel.XlBorderWeight.xlThin;
            borders.Color = Color.FromArgb(122, 181, 192);

            // xoá file khi file tồn tại trước đó
            if (File.Exists(filePath)) File.Delete(filePath);

            // Lưu file Excel
            workbook.SaveAs(filePath);

            // Đóng workbook và ứng dụng Excel
            workbook.Close();
            excelApp.Quit();
        }

        // Phương thức chuyển đổi hình ảnh thành mảng byte
        private static byte[] ImageToByteArray(System.Drawing.Image image)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                image.Save(ms, System.Drawing.Imaging.ImageFormat.Jpeg);
                return ms.ToArray();
            }
        }

        #endregion Export Excel

        #region Image

        // lấy đường dẫn đến thư mục .exe của chương trình
        private static string CurrentPath()
        {
            return Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
        }

        // tạo thư mục chứa hình ảnh
        public static void CreateImageFolder()
        {
            Directory.CreateDirectory(ImagesFolderPath());
        }

        // lấy đường dẫn đến thư mục chứa hình ảnh cho sản phẩm
        public static string ImagesFolderPath()
        {
            return Path.Combine(CurrentPath(), $"Products Image");
        }

        public static string ExportFolderPath()
        {
            return Path.Combine(CurrentPath(), $"Export");
        }

        // lấy đường dẫn đến file hình ảnh
        public static string GetImage(string imageName)
        {
            return Path.Combine(ImagesFolderPath(), imageName.Trim());
        }

        public static void ImageDelete(string imageName)
        {
            var path = GetImage(imageName);
            if (File.Exists(path)) File.Delete(path);
        }

        public static void ImageImport(string imageName, string sourcePath)
        {
            if (!File.Exists(ImagesFolderPath())) CreateImageFolder();

            if (File.Exists(GetImage(imageName)))
            {
                //if (ShowMess.Question__ReplaceFile(imageName) == DialogResult.No)
                //    return;

                File.Delete(GetImage(imageName));
            }

            File.Copy(Path.Combine(sourcePath, imageName), GetImage(imageName), true);
        }

        // hiển thị hình ảnh lên PictureBox, với PictureBox là control hiển thị ảnh
        public static void ImageLoad(string imageName, PictureBox picture)
        {
            if (string.IsNullOrEmpty(imageName) || !File.Exists(GetImage(imageName)))
            {
                ImageLoad__Null(picture);
                return;
            }
            using (FileStream stream = new FileStream(GetImage(imageName), FileMode.Open, FileAccess.Read))
            {
                picture.Image = Image.FromStream(stream);
                stream.Dispose();
            }
        }

        // hiển thị hình ảnh lên PictureBox, với PictureBox là control hiển thị ảnh
        public static void ImageLoadOneImage(string imageName, PictureBox picture)
        {
            imageName = GetImage__First(imageName);
            if (string.IsNullOrEmpty(imageName) || !File.Exists(GetImage(imageName)))
            {
                ImageLoad__Null(picture);
                return;
            }

            using (FileStream stream = new FileStream(GetImage(imageName), FileMode.Open, FileAccess.Read))
            {
                picture.Image = Image.FromStream(stream);
                stream.Dispose();
            }
        }

        // hiển thị hình ảnh lên PictureBox, với PictureBox là control hiển thị ảnh
        public static void ImageLoad(string imageName, string desSource, PictureBox picture)
        {
            var path = Path.Combine(desSource, imageName);
            if (string.IsNullOrEmpty(imageName) || !File.Exists(path))
            {
                ImageLoad__Null(picture);
                return;
            }
            using (FileStream stream = new FileStream(path, FileMode.Open, FileAccess.Read))
            {
                picture.Image = Image.FromStream(stream);
                stream.Dispose();
            }
        }

        // Hiển thị hình ảnh có sẵn khi không tìm thấy hình ảnh cho sản phẩm
        public static void ImageLoad__Null(PictureBox picture)
        {
            picture.Image = Properties.Resources.noImages;
        }

        // hàm hiển thị hình ảnh lên danh sách.
        public static Image ImageLoad__ForList(string imageName)
        {
            var path = GetImage(imageName);
            return (string.IsNullOrEmpty(imageName) || !File.Exists(path)) ? new Bitmap(Properties.Resources.noImages).GetThumbnailImage(100, 100, null, IntPtr.Zero) : new Bitmap(GetImage(imageName)).GetThumbnailImage(100, 100, null, IntPtr.Zero);
        }

        // hàm hiển thị hình ảnh lên danh sách.
        public static Image ImageLoad__ForList(string imageName, string desPath)
        {
            var path = Path.Combine(desPath, imageName);
            return (string.IsNullOrEmpty(imageName) || !File.Exists(path)) ? new Bitmap(Properties.Resources.noImages).GetThumbnailImage(100, 100, null, IntPtr.Zero) : new Bitmap(path).GetThumbnailImage(100, 100, null, IntPtr.Zero);
        }

        // Tách các hình ảnh trong chuỗi thành một từng hình riêng biệt, VD: img1.png, img2.png => array ["img1.png", "img2.png"]
        public static string[] SplitImage(string strImage) => strImage.Split(',').Select(x => x.Trim()).ToArray();

        // Lấy hình đâu tiên trong chuỗi hình ảnh
        public static string GetImage__First(string strImage) => SplitImage(strImage)[0];

        #endregion Image

        #region Key

        // chuyển thời gian thành ký tự kết hợp với ký tự đầu tiên của key
        // chỉ lấy ngày
        private static string ConstantDate(string firstKey)
        {
            string[] partsDay = System.DateTime.Now.ToString("yyyy/MM/dd").Split('/');
            return $"{firstKey}{partsDay[0]}{partsDay[1]}{partsDay[2]}";
        }

        // chuyển thời gian thành ký tự kết hợp với ký tự đầu tiên của key
        // lấy ngày và thời gian
        private static string ConstantDateFull(string firstKey)
        {
            string[] partsDay = System.DateTime.Now.ToString("yyyy/MM/dd").Split('/');
            string[] partsTime = System.DateTime.Now.ToString("HH:mm:ss").Split(':');
            return $"{firstKey}{partsDay[0]}{partsDay[1]}{partsDay[2]}{partsTime[0]}{partsTime[1]}{partsTime[2]}";
        }

        // tạo mã tự động
        public static string CreateKey(string key, bool isFullDateTime)
        {
            return isFullDateTime ? ConstantDateFull(key) : ConstantDate(key);
        }

        #endregion Key
    }
}