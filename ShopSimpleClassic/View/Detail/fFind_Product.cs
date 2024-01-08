using ShopSimpleClassic.Controller;
using ShopSimpleClassic.Library;
using ShopSimpleClassic.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ShopSimpleClassic.View.Detail
{
    public partial class fFind_Product : Form
    {
        // Variable
        public SendProductChoice send;

        private string codeSelected;
        private int currentPage = 0, totalPage = 0, pageSize = 10;

        // Main
        public fFind_Product(SendProductChoice sender)
        {
            InitializeComponent();
            this.send = sender;
            loadList();
            cbPageSize.SelectedIndex = 0;
        }

        private void btChoose_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(codeSelected))
                {
                    ShowMess.Error__NotChoice("Sản Phẩm");
                    return;
                }

                this.send(codeSelected);
                this.Close();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                codeSelected = null;
                Lib.ClearText(pnSearch);
                Lib.ClearText(pnInformation);
                Lib.ImageLoad__Null(picImage);
                setList();
                updateListAndPageControl();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
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
                updateListAndPageControl();
                bindingData();
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
                loadList();
                getValueSelectedInList(); // lấy mã dòng đang chọn trên danh sách
                bindingData();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void dgvList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        => dgvList.Rows.Cast<DataGridViewRow>().ToList().ForEach(i => i.MinimumHeight = 100);

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

            btChoose.PerformClick();
        }

        // function

        // tải danh sách dữ liệu
        private void loadList()
        {
            try
            {
                setList();
            }
            catch (Exception ex)
            {
                dgvList.DataSource = null;
                ShowMess.Exception(ex);
            }
        }

        /// <summary>
        /// cập nhật lại dữ liệu cho danh sách và các control hiển thị thông tin cho danh sách tương ứng
        /// </summary>
        private void updateListAndPageControl()
        {
            UpdatePageControl();
            setList();
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
            cbPage.SelectedIndex = 0; // Đặt chỉ mục để hiển thị trang đầu tiên
            setList(); // Cập nhật danh sách dữ liệu trên trang
        }

        // gán dữ liệu vào datagridview
        private void setList()
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

        // nhận mã đã chọn trên danh sách
        private void getValueSelectedInList()
        => codeSelected = dgvList.RowCount != 0 ? dgvList.CurrentRow.Cells[2].Value.ToString() : null;

        // gán các dữ liệu vào các control
        private void bindingData()
        {
            var _dataBinding = new bProduct().Detail(codeSelected);

            if (_dataBinding == null) return;

            tbCode.Text = _dataBinding.ProductCode;
            tbName.Text = _dataBinding.Name;
            tbCatalog.Text = new bCatalog().Detail(_dataBinding.CatalogID).Name;
            tbSupplier.Text = new bSupplier().Detail(_dataBinding.SupplierID).Name;
            tbStatus.Text = _dataBinding.Status ? "Còn hàng" : "Ngừng kinh doanh";
            tbImage.Text = _dataBinding.Image;
            loadImage();
        }

        private void loadImage()
        => Lib.ImageLoad(tbImage.Text.Trim(), picImage);

        /// <summary>
        /// thay đổi số trang và tải lại danh sách
        /// </summary>
        /// <param name="page"></param>
        private void changeCurrentPage(int page)
        {
            currentPage = page;
            cbPage.SelectedItem = currentPage.ToString();
            setList();
        }
    }
}