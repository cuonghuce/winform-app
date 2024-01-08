using ShopSimpleClassic.Controller;
using ShopSimpleClassic.Library;
using ShopSimpleClassic.Model;
using ShopSimpleClassic.View.Detail;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ShopSimpleClassic.View.Manager
{
    public partial class fProduct : Form
    {
        // Variable

        private Product _data;
        private Product _dataBinding;
        private int currentPage = 0, totalPage = 0, pageSize = 10;
        private bool isStart = true;
        private string codeSelected;
        private string pathImage;
        private bool isChangeImage = false;

        // Main
        public fProduct()
        {
            InitializeComponent();
            load();
        }

        private void fProduct_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btAdd_Image_Click(object sender, EventArgs e)
        {
            try
            {
                openDialog();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btDetail_Click(object sender, EventArgs e)
        {
            try
            {
                var code = tbCode.Text.Trim();
                if (string.IsNullOrEmpty(code))
                {
                    ShowMess.Error__NotChoice("Sản Phẩm");
                    return;
                }

                eForm.ShowWaitForm();
                new fProduct_Detail(code).ShowDialog();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btEdit_Click(object sender, EventArgs e)
        {
            try
            {
                saveData();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btDelete_Click(object sender, EventArgs e)
        {
            try
            {
                deleteData();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btReset_Click(object sender, EventArgs e)
        {
            try
            {
                getValueSelectedInList(); // lấy mã dòng đang chọn trên danh sách
                if (string.IsNullOrEmpty(codeSelected))
                {
                    ShowMess.Error__EmptyCode();
                    return;
                }

                bindingData();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btClear_Click(object sender, EventArgs e)
        {
            clearInformation();
            cbCatalog.SelectedIndex = cbStatus.SelectedIndex = cbSupplier.SelectedIndex = -1;
            Lib.ImageLoad__Null(picImage);
        }

        private void tbExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvList.RowCount == 0)
                {
                    ShowMess.Error__EmptyListToExport();
                    return;
                }
                Lib.ExportExcel(dgvList, "Sản Phẩm");
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            clearInformation();
            clearSearch();
            updateListAndPageControl();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            // kiểm tra từ khoá tìm kiếm không cho ra kết quả
            if (new bProduct().TotalRows(tbSearch.Text.Trim()) == 0)
            {
                ShowMess.Error__NotFind(tbSearch.Text.Trim());
                return;
            }

            updateListAndPageControl();
        }

        private void btFirst_Click(object sender, EventArgs e)
        {
            try
            {
                changeCurrentPage(1);
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btPrevious_Click(object sender, EventArgs e)
        {
            try
            {
                changeCurrentPage(currentPage > 1 ? currentPage - 1 : currentPage);
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btNext_Click(object sender, EventArgs e)
        {
            try
            {
                changeCurrentPage(currentPage < totalPage ? currentPage + 1 : currentPage);
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btLast_Click(object sender, EventArgs e)
        {
            try
            {
                changeCurrentPage(totalPage);
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void cbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isStart) return;
                codeSelected = null;
                updateListAndPageControl();
                clearInformation();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void cbPage_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isStart) return;
                loadList();
                getValueSelectedInList(); // lấy mã dòng đang chọn trên danh sách
                bindingData();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            getValueSelectedInList(); // lấy mã dòng đang chọn trên danh sách
            if (string.IsNullOrEmpty(codeSelected)) return;
            bindingData();
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            getValueSelectedInList(); // lấy mã dòng đang chọn trên danh sách
            if (string.IsNullOrEmpty(codeSelected)) return;
            bindingData();

            using (var waitForm = new fLoading()) // Sử dụng biến cục bộ để tạo form chờ
            {
                waitForm.TopMost = true;
                waitForm.StartPosition = FormStartPosition.CenterScreen;
                waitForm.Show();
                waitForm.Refresh();
                System.Threading.Thread.Sleep(200);

                waitForm.Close(); // Đóng form chờ

                using (var productDetailForm = new fProduct_Detail(codeSelected))
                {
                    productDetailForm.ShowDialog();
                }
            }

            //new fProduct_Detail(codeSelected).ShowDialog();
        }

        private void dgvList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        => dgvList.Rows.Cast<DataGridViewRow>().ToList().ForEach(i => i.MinimumHeight = 100);

        // function
        /// <summary>
        /// tải các phần cần thiết cho giao diện
        /// </summary>
        private void load()
        {
            getCombobox();
            cbPageSize.SelectedIndex = 0;
            updateListAndPageControl();
            role();
        }

        /// <summary>
        /// sự khiện ẩn các tính năng khi phân quyền
        /// </summary>
        private void role()
        {
            Lib.ReadOnlyControl(pnInformation, !Temp.IsAdmin);
            Lib.ReadOnlyControlButton(pnInformation, new Button[] { btClear, btReset, btDetail }, Temp.IsAdmin);
            tbCode.ReadOnly = tbImage.ReadOnly = true;
        }

        /// <summary>
        /// cập nhật lại dữ liệu cho danh sách và các control hiển thị thông tin cho danh sách tương ứng
        /// </summary>
        private void updateListAndPageControl()
        {
            UpdatePageControl();
            loadList();
        }

        /// <summary>
        /// cập nhật lại các control cho phân trang
        /// </summary>
        private void UpdatePageControl()
        {
            int rows = new bProduct().TotalRows(tbSearch.Text.Trim());
            pageSize = Convert.ToInt32(cbPageSize.SelectedItem?.ToString());
            totalPage = (int)Math.Ceiling((double)rows / pageSize); // Tính tổng số trang
            lblPage.Text = $"{lblPage.Tag}{totalPage}"; // Cập nhật nhãn với tổng số trang
            lblResult.Text = $"{lblResult.Tag} {rows}"; // Cập nhật nhãn với tổng số kết quả

            cbPage.Items.Clear();
            cbPage.Items.AddRange(totalPage != 0 ? Enumerable.Range(1, totalPage).Select(i => i.ToString()).ToArray() : new[] { "0" });
            isStart = false;
            cbPage.SelectedIndex = 0; // Đặt chỉ mục để hiển thị trang đầu tiên
            loadList(); // Cập nhật danh sách dữ liệu trên trang
        }

        /// <summary>
        ///  hiển thị dữ liệu lên danh sách
        /// </summary>
        private void loadList()
        {
            pageSize = Convert.ToInt32(cbPageSize.SelectedItem?.ToString());
            currentPage = Convert.ToInt32(cbPage.SelectedItem?.ToString());

            int startIndex = (currentPage - 1) * pageSize + 1;

            // Lấy danh sách Product từ bộ xử lý bProduct dựa trên từ khóa tìm kiếm, trang hiện tại và kích thước trang
            IEnumerable<Product> data = new bProduct().List(tbSearch.Text.Trim(), currentPage - 1, pageSize);

            dgvList.Rows.Clear();

            // Thêm dữ liệu Product vào DataGridView
            dgvList.Rows.AddRange(data.Select((i, count) => new DataGridViewRow
            {
                Cells =
                {
                    new DataGridViewTextBoxCell { Value = startIndex + count },
                    new DataGridViewImageCell   { Value = Lib.ImageLoad__ForList(i.Image) },
                    new DataGridViewTextBoxCell { Value = i.ProductCode },
                    new DataGridViewTextBoxCell { Value = i.Name },
                    new DataGridViewTextBoxCell { Value = new bCatalog().Detail(i.CatalogID).Name },
                    new DataGridViewTextBoxCell { Value = new bSupplier().Detail(i.SupplierID).Name },
                    new DataGridViewTextBoxCell { Value = i.Amount },
                    new DataGridViewTextBoxCell { Value = Lib.ConvertPrice(i.Price.ToString(), true) },
                    new DataGridViewTextBoxCell { Value = i.Status ? "Còn hàng" : "Ngừng kinh doanh" }
                }
            }).ToArray());

            getValueSelectedInList(); // Cập nhật giá trị đã chọn trong danh sách
        }

        /// <summary>
        /// xoá trắng các thông tin trong khung thông tin
        /// </summary>
        private void clearInformation()
        {
            Lib.ClearText(pnInformation);
            Lib.ClearCombobox(pnInformation);
            Lib.ImageLoad__Null(picImage);
        }

        // xoá trắng các textbox trong khung tìm kiếm
        private void clearSearch()
        => Lib.ClearText(pnSearch);

        // gán các dữ liệu vào các control
        private void bindingData()
        {
            _dataBinding = new bProduct().Detail(codeSelected);

            if (_dataBinding == null) return;

            tbCode.Text = _dataBinding.ProductCode;
            tbName.Text = _dataBinding.Name;
            cbCatalog.SelectedValue = _dataBinding.CatalogID;
            cbSupplier.SelectedValue = _dataBinding.SupplierID;
            cbStatus.SelectedIndex = _dataBinding.Status ? 1 : 0;
            tbImage.Text = _dataBinding.Image;
            loadImage();
        }

        /// <summary>
        /// Thêm hoặc cập nhật dữ liệu.
        /// </summary>
        /// <param name="isAdd">True nếu là thêm mới, False nếu là cập nhật.</param>
        private void saveData()
        {
            // Đóng gói dữ liệu để thêm mới hoặc cập nhật
            if (!packageData())
                return;

            if (ShowMess.Question__Update($"{tbCode.Text}: {tbName.Text}") == DialogResult.No) return;

            string mess = $"{_data.ProductCode}: {_data.Name}";

            // Kiểm tra tồn tại cho thêm mới
            if (!new bProduct().IsExists(_data.ProductCode))
            {
                ShowMess.Error__NotExists(mess); // Hiển thị thông báo lỗi: Product không tồn tại
                return;
            }

            bool result = new bProduct().Update(_data);

            if (!result)
            {
                ShowMess.Error__AddedOrUpdated(mess, false); // Hiển thị thông báo lỗi: Thêm mới hoặc cập nhật không thành công
                return;
            }

            if (isChangeImage)
            {
                Lib.ImageImport(_data.Image, pathImage);
            }

            updateListAndPageControl(); // Cập nhật danh sách và điều khiển trang
            clearInformation();

            if (_data.Name.ToLower().Contains(tbSearch.Text.Trim()))
            {
                setSelectInList(_data.ProductCode); // Xử lý trường hợp từ khoá tìm kiếm khi có từ tương ứng
            }

            ShowMess.Success__AddedOrUpdated(mess, false); // Hiển thị thông báo thành công: Thêm mới hoặc cập nhật thành công
        }

        // xoá dữ liệu trong database
        private void deleteData()
        {
            // Đóng gói dữ liệu để xóa
            if (!packageData())
                return;

            if (ShowMess.Question__Delete($"{tbCode.Text}: {tbName.Text}") == DialogResult.No) return;

            // Lấy giá trị trang được chọn từ combobox
            string page = cbPage.SelectedItem.ToString();

            // Tạo thông điệp thông báo từ dữ liệu Product
            string mess = $"{_data.ProductCode}: {_data.Name}";

            // Kiểm tra xem Product có tồn tại hay không
            if (!new bProduct().IsExists(_data.ProductCode))
            {
                // Hiển thị thông báo lỗi: Product không tồn tại
                ShowMess.Error__NotExists(mess);
                return;
            }

            // Xóa Product và kiểm tra kết quả xóa
            bool result = new bProduct().Delete(_data.ProductCode);

            if (!result)
            {
                // Hiển thị thông báo lỗi: Xóa Product không thành công
                ShowMess.Error__Deleted(mess);
                return;
            }

            // Cập nhật danh sách và điều khiển trang
            updateListAndPageControl();
            clearInformation();

            // Tạo danh sách trang từ combobox
            var listPage = cbPage.Items.Cast<string>().ToList();

            if (listPage.Count == 0)
            {
                // Hiển thị thông báo thành công: Xóa Product thành công
                ShowMess.Success__Deleted(mess);
                return;
            }

            // Kiểm tra xem trang hiện tại có tồn tại trong danh sách trang hay không
            if (!listPage.Contains(page))
            {
                // Chọn trang trước đó nếu trang hiện tại không tồn tại
                cbPage.SelectedItem = (int.Parse(page) - 1).ToString();
            }

            cbPage.SelectedItem = page;

            // Hiển thị thông báo thành công: Xóa Product thành công
            ShowMess.Success__Deleted(mess);
        }

        // kiểm tra các textbox điều có dữ liệu
        private bool checkInputNotEmpty()
        => Lib.ControlNotEmpty(pnInformation);

        // nhận mã đã chọn trên danh sách
        private void getValueSelectedInList()
        => codeSelected = dgvList.RowCount != 0 ? dgvList.CurrentRow.Cells[2].Value.ToString() : null;

        /// <summary>
        /// chọn dòng dữ liệu trong danh sách dựa trên mã cung cấp.
        /// </summary>
        /// <param name="code">Mã cảu dòng cần hiển thị</param>
        private void setSelectInList(string code)
        {
            // Kiểm tra nếu không có hàng (row) trong danh sách
            if (dgvList.RowCount == 0)
                return;

            // Tìm chỉ số (index) của hàng (row) dựa trên mã code và văn bản tìm kiếm
            int index = new bProduct().IndexRows(code, tbSearch.Text.Trim());

            // Đặt giá trị được chọn trong combobox trang hiện tại dựa trên chỉ số (index)
            cbPage.SelectedItem = ((int)Math.Ceiling((double)(index + 1) / pageSize)).ToString();

            // Tìm hàng (row) trong danh sách có mã code tương ứng
            var row = dgvList.Rows.Cast<DataGridViewRow>().First(r => r.Cells[2].Value.ToString().Equals(code));

            // Kiểm tra nếu không tìm thấy hàng (row)
            if (row == null)
                return;

            // Đánh dấu hàng (row) được chọn trong danh sách
            dgvList.Rows[row.Index].Selected = true;

            // Lấy giá trị mã code từ hàng (row) được chọn
            codeSelected = dgvList.Rows[row.Index].Cells[1].Value?.ToString();

            // Cập nhật dữ liệu theo mã code được chọn
            bindingData();
        }

        /// <summary>
        /// Đóng gói dữ liệu để xử lý.
        /// </summary>
        /// <returns>
        /// Trả về true nếu dữ liệu được đóng gói thành công; ngược lại, trả về false.
        /// </returns>
        private bool packageData()
        {
            // Kiểm tra nếu dữ liệu đầu vào không rỗng
            if (!checkInputNotEmpty())
            {
                // Hiển thị thông báo lỗi: Dữ liệu đầu vào không được để trống
                ShowMess.Error__InputNotEmpty();
                return false; // Trả về false để biểu thị rằng quá trình đóng gói dữ liệu không thành công
            }

            // Tạo một đối tượng Product mới và gán giá trị từ các trường dữ liệu vào đó
            _data = new Product
            {
                ProductCode = tbCode.Text.Trim(),
                Name = tbName.Text.Trim(),
                Image = tbImage.Text.Trim(),
                CatalogID = cbCatalog.SelectedValue?.ToString(),
                SupplierID = cbSupplier.SelectedValue?.ToString(),
                Amount = _dataBinding.Amount,
                Price = _dataBinding.Price,
                CreateDate = _dataBinding.CreateDate,
                Status = cbStatus.SelectedIndex == 1
            };

            return true; // Trả về true để biểu thị rằng quá trình đóng gói dữ liệu thành công
        }

        private void getCombobox()
        {
            cbCatalog.DataSource = new bCatalog().ListForCombobox();
            cbSupplier.DataSource = new bSupplier().ListForCombobox();

            cbCatalog.DisplayMember = cbSupplier.DisplayMember = "Display";
            cbCatalog.ValueMember = cbSupplier.ValueMember = "Value";
            cbCatalog.SelectedIndex = cbStatus.SelectedIndex = cbSupplier.SelectedIndex = -1;
        }

        private void loadImage()
        {
            if (isChangeImage)
            {
                Lib.ImageLoad(tbImage.Text.Trim(), pathImage, picImage);
                return;
            }

            Lib.ImageLoad(tbImage.Text.Trim(), picImage);
        }

        private void openDialog()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Chọn hình ảnh";
                dlg.Filter = "Kiểu hình ảnh | *.jpg;*.jpeg;*.png;";
                dlg.Multiselect = false;

                DialogResult dr = dlg.ShowDialog();

                isChangeImage = dr == DialogResult.OK;

                if (isChangeImage)
                {
                    tbImage.Text = dlg.SafeFileName;
                    pathImage = Path.GetDirectoryName(dlg.FileName);
                    loadImage();
                }
            }
        }

        /// <summary>
        /// thay đổi số trang và tải lại danh sách
        /// </summary>
        /// <param name="page"></param>
        private void changeCurrentPage(int page)
        {
            currentPage = page;
            cbPage.SelectedItem = currentPage.ToString();
            loadList();
        }
    }
}