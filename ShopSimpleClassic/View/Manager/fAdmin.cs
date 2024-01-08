using ShopSimpleClassic.Controller;
using ShopSimpleClassic.Library;
using ShopSimpleClassic.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ShopSimpleClassic.View.Manager
{
    public partial class fAdmin : Form
    {
        // Variable
        private Admin _data;

        private int currentPage = 0, totalPage = 0, pageSize = 10;
        private bool isStart = true;
        private string codeSelected;

        // Main
        public fAdmin()
        {
            InitializeComponent();
            load();
        }

        private void fAdmin_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                AddOrUpdateData(true);
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
                AddOrUpdateData(false);
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
                Lib.ExportExcel(dgvList, "Loại Sản Phẩm");
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
            if (new bAdmin().TotalRows(tbSearch.Text.Trim()) == 0)
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

        private void dgvList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        => dgvList.Rows.Cast<DataGridViewRow>().ToList().ForEach(i => i.MinimumHeight = 28);

        // function
        /// <summary>
        /// tải các phần cần thiết cho giao diện
        /// </summary>
        private void load()
        {
            cbPageSize.SelectedIndex = 0;
            Role();
            updateListAndPageControl();
        }

        private void Role()
        {
            btAdd.Enabled = btEdit.Enabled = btDelete.Enabled = Temp.Username == "admin";
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
            int rows = new bAdmin().TotalRows(tbSearch.Text.Trim());
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

            // Lấy danh sách Admin từ bộ xử lý bAdmin dựa trên từ khóa tìm kiếm, trang hiện tại và kích thước trang
            IEnumerable<Admin> data = new bAdmin().List(tbSearch.Text.Trim(), currentPage - 1, pageSize);

            dgvList.Rows.Clear();

            // Thêm dữ liệu Admin vào DataGridView
            dgvList.Rows.AddRange(data.Select((i, count) => new DataGridViewRow
            {
                Cells =
                {
                    new DataGridViewTextBoxCell { Value = startIndex + count }, // Ô hiển thị chỉ số bắt đầu từ
                    new DataGridViewTextBoxCell { Value = i.Username }, // Ô hiển thị mã Admin
                    new DataGridViewTextBoxCell { Value = i.Name } // Ô hiển thị tên Admin
                }
            }).ToArray());

            getValueSelectedInList(); // Cập nhật giá trị đã chọn trong danh sách
        }

        // xoá trắng các textbox trong khung thông tin
        private void clearInformation()
        => Lib.ClearText(pnInformation);

        // xoá trắng các textbox trong khung tìm kiếm
        private void clearSearch()
        => Lib.ClearText(pnSearch);

        // gán các dữ liệu vào các control
        private void bindingData()
        {
            var data = new bAdmin().Detail(codeSelected);

            if (data == null) return;

            tbCode.Text = data.Username;
            tbName.Text = data.Name;
        }

        /// <summary>
        /// Thêm hoặc cập nhật dữ liệu.
        /// </summary>
        /// <param name="isAdd">True nếu là thêm mới, False nếu là cập nhật.</param>
        private void AddOrUpdateData(bool isAdd)
        {
            // Đóng gói dữ liệu để thêm mới hoặc cập nhật
            if (!packageData())
                return;

            if (!isAdd && ShowMess.Question__Update($"{tbCode.Text}: {tbName.Text}") == DialogResult.No) return;

            string mess = $"{_data.Username}: {_data.Name}";

            // Kiểm tra tồn tại cho thêm mới và cập nhật
            if (isAdd && new bAdmin().IsExists(_data.Username))
            {
                ShowMess.Error__AlreadyExist(mess); // Hiển thị thông báo lỗi: Admin đã tồn tại
                return;
            }
            else if (!isAdd && !new bAdmin().IsExists(_data.Username))
            {
                ShowMess.Error__NotExists(mess); // Hiển thị thông báo lỗi: Admin không tồn tại
                return;
            }

            bool result = isAdd ? new bAdmin().Add(_data) : new bAdmin().UpdateName(_data); // Thêm mới hoặc cập nhật Admin

            if (!result)
            {
                ShowMess.Error__AddedOrUpdated(mess, isAdd); // Hiển thị thông báo lỗi: Thêm mới hoặc cập nhật không thành công
                return;
            }

            updateListAndPageControl(); // Cập nhật danh sách và điều khiển trang
            clearInformation();

            if (_data.Name.ToLower().Contains(tbSearch.Text.Trim()))
            {
                setSelectInList(_data.Username); // Xử lý trường hợp từ khoá tìm kiếm khi có từ tương ứng
            }

            ShowMess.Success__AddedOrUpdated(mess, isAdd); // Hiển thị thông báo thành công: Thêm mới hoặc cập nhật thành công
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

            // Tạo thông điệp thông báo từ dữ liệu Admin
            string mess = $"{_data.Username}: {_data.Name}";

            // Kiểm tra xem Admin có tồn tại hay không
            if (!new bAdmin().IsExists(_data.Username))
            {
                // Hiển thị thông báo lỗi: Admin không tồn tại
                ShowMess.Error__NotExists(mess);
                return;
            }

            // Xóa Admin và kiểm tra kết quả xóa
            bool result = new bAdmin().Delete(_data.Username);

            if (!result)
            {
                // Hiển thị thông báo lỗi: Xóa Admin không thành công
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
                // Hiển thị thông báo thành công: Xóa Admin thành công
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

            // Hiển thị thông báo thành công: Xóa Admin thành công
            ShowMess.Success__Deleted(mess);
        }

        // kiểm tra các textbox điều có dữ liệu
        private bool checkInputNotEmpty()
        => Lib.ControlNotEmpty(pnInformation);

        // nhận mã đã chọn trên danh sách
        private void getValueSelectedInList()
        => codeSelected = dgvList.RowCount != 0 ? dgvList.CurrentRow.Cells[1].Value.ToString() : null;

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
            int index = new bAdmin().IndexRows(code, tbSearch.Text.Trim());

            // Đặt giá trị được chọn trong combobox trang hiện tại dựa trên chỉ số (index)
            cbPage.SelectedItem = ((int)Math.Ceiling((double)(index + 1) / pageSize)).ToString();

            // Tìm hàng (row) trong danh sách có mã code tương ứng
            var row = dgvList.Rows.Cast<DataGridViewRow>().First(r => r.Cells[1].Value.ToString().Equals(code));

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

            // Tạo một đối tượng Admin mới và gán giá trị từ các trường dữ liệu vào đó
            _data = new Admin
            {
                Username = tbCode.Text.Trim(),
                Name = tbName.Text.Trim(),
                Password = "1111"
            };

            return true; // Trả về true để biểu thị rằng quá trình đóng gói dữ liệu thành công
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