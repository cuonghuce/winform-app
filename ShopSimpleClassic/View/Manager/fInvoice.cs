using ShopSimpleClassic.Controller;
using ShopSimpleClassic.Library;
using ShopSimpleClassic.Model;
using ShopSimpleClassic.View.Detail;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;

namespace ShopSimpleClassic.View.Manager
{
    public partial class fInvoice : Form
    {
        /// Variable
        private string codeSelected;
        private bool isStart = true;
        private int currentPage = 0, totalPage = 0, pageSize = 10;

        /// Main
        public fInvoice()
        {
            InitializeComponent();
            load();
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new fInvoice_Add(true);
                frm.ShowDialog();
                if (frm.DialogResult == DialogResult.Yes)
                    loadList();
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
                var invoiceCode = tbCode.Text.Trim();
                if (string.IsNullOrEmpty(invoiceCode))
                {
                    ShowMess.Error__NotChoice("Đơn Bán Hàng");
                    return;
                }

                deleteInvoice(invoiceCode);
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
                var invoiceCode = tbCode.Text.Trim();
                if (string.IsNullOrEmpty(invoiceCode))
                {
                    ShowMess.Error__NotChoice("Đơn Bán Hàng");
                    return;
                }

                var frm = new fInvoice_Add(invoiceCode, true);
                frm.ShowDialog();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void tbExport_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvList.RowCount == 0)
                {
                    ShowMess.Error__EmptyList();
                    return;
                }

                Lib.ExportExcel(dgvList, "Đơn Bán Hàng");
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btRefresh_Click(object sender, EventArgs e)
        {
            Lib.ClearText(pnInformation);
            Lib.ClearText(pnSearch);
            chbDate.Checked = chbPrice.Checked = false;
            dtpFrom.Value = dtpTo.Value = DateTime.Now;
            updateListAndPageControl();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Lib.ClearText(pnSearch);
            chbDate.Checked = chbPrice.Checked = false;
            dtpFrom.Value = dtpTo.Value = DateTime.Now;
            updateListAndPageControl();
        }

        private void btSearch_Click(object sender, EventArgs e)
        {
            // kiểm tra từ khoá tìm kiếm không cho ra kết quả
            if (new bInvoice().TotalRows(tbSearch.Text.Trim(), dtpFrom.Value, dtpTo.Value, chbDate.Checked,
                                         Lib.ConvertIntFromPrice(tbPriceFrom).ToString(), Lib.ConvertIntFromPrice(tbPriceFrom).ToString(), chbPrice.Checked) == 0)
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

        private void chbPrice_CheckedChanged(object sender, EventArgs e)
        {
            tbPriceFrom.Visible = tbPriceTo.Visible = lblPrice.Visible = chbPrice.Checked;
        }

        private void chbDate_CheckedChanged(object sender, EventArgs e)
        {
            dtpFrom.Visible = dtpTo.Visible = lblDate.Visible = chbDate.Checked;
        }

        private void tbPriceFrom_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPriceFrom.Text.Trim())) return;
            tbPriceFrom.Text = Lib.ConvertPrice(tbPriceFrom.Text.Trim(), false);
        }

        private void tbPriceFrom_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPriceFrom.Text.Trim())) return;
            tbPriceFrom.Text = Lib.ConvertPrice(tbPriceFrom.Text.Trim(), true);
        }

        private void tbPriceTo_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPriceTo.Text.Trim())) return;
            tbPriceTo.Text = Lib.ConvertPrice(tbPriceTo.Text.Trim(), false);
        }

        private void tbPriceTo_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPriceTo.Text.Trim())) return;
            tbPriceTo.Text = Lib.ConvertPrice(tbPriceTo.Text.Trim(), true);
        }

        private void cbPageSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (isStart) return;
                codeSelected = null;
                updateListAndPageControl();
                Lib.ClearText(pnInformation);
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
            role();
        }

        private void dgvList_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            try
            {
                getValueSelectedInList(); // lấy mã dòng đang chọn trên danh sách
                if (string.IsNullOrEmpty(codeSelected)) return;
                bindingData();
                role();

                var frm = new fInvoice_Add(codeSelected, true);
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.Yes)
                    loadList();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void dgvList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        => dgvList.Rows.Cast<DataGridViewRow>().ToList().ForEach(i => i.MinimumHeight = 30);

        private void fInvoice_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
        }

        // function
        /// <summary>
        /// tải các phần cần thiết cho giao diện
        /// </summary>
        private void load()
        {
            cbPageSize.SelectedIndex = 0;
            updateListAndPageControl();
            role();
        }

        /// <summary>
        /// sự khiện ẩn các tính năng khi phân quyền
        /// </summary>
        private void role()
        {
            btDelete.Visible = new bInvoice().Detail(codeSelected).UserID == Temp.Username;
            if (Temp.IsAdmin)
                btDelete.Visible = true;
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
            int rows = new bInvoice().TotalRows(tbSearch.Text.Trim(), dtpFrom.Value, dtpTo.Value, chbDate.Checked,
                                               Lib.ConvertIntFromPrice(tbPriceFrom).ToString(), Lib.ConvertIntFromPrice(tbPriceFrom).ToString(), chbPrice.Checked);
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

            // Lấy danh sách Catalog từ bộ xử lý bInvoice dựa trên từ khóa tìm kiếm, trang hiện tại và kích thước trang
            IEnumerable<Invoice> data = new bInvoice().List(tbSearch.Text.Trim(), dtpFrom.Value, dtpTo.Value, chbDate.Checked,
                                                            Lib.ConvertIntFromPrice(tbPriceFrom).ToString(), Lib.ConvertIntFromPrice(tbPriceFrom).ToString(), chbPrice.Checked, currentPage - 1, pageSize);

            dgvList.Rows.Clear();

            // Thêm dữ liệu Catalog vào DataGridView
            dgvList.Rows.AddRange(data.Select((i, count) => new DataGridViewRow
            {
                Cells =
                {
                    new DataGridViewTextBoxCell { Value = startIndex + count },
                    new DataGridViewTextBoxCell { Value = i.InvoiceCode },
                    new DataGridViewTextBoxCell { Value = Lib.ConvertDateToString(i.Date) },
                    new DataGridViewTextBoxCell { Value = new bUser().Detail(i.UserID).Name },
                    new DataGridViewTextBoxCell { Value = new bCustomer().Detail(i.CustomerPhone).Name },
                    new DataGridViewTextBoxCell { Value = i.CustomerPhone },
                    new DataGridViewTextBoxCell { Value = new bInvoiceDetail().TotalAmount(i.InvoiceCode) },
                    new DataGridViewTextBoxCell { Value = new bInvoiceDetail().TotalQuantity(i.InvoiceCode) },
                    new DataGridViewTextBoxCell { Value = Lib.ConvertPrice(i.Total.ToString(), true) }
                }
            }).ToArray());

            getValueSelectedInList(); // Cập nhật giá trị đã chọn trong danh sách
        }

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
            int index = new bInvoice().IndexRows(code, tbSearch.Text.Trim(), dtpFrom.Value, dtpTo.Value, chbDate.Checked,
                                                Lib.ConvertIntFromPrice(tbPriceFrom).ToString(), Lib.ConvertIntFromPrice(tbPriceFrom).ToString(), chbPrice.Checked);

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

        // gán các dữ liệu vào các control
        private void bindingData()
        {
            var data = new bInvoice().Detail(codeSelected);

            if (data == null) return;

            tbCode.Text = data.InvoiceCode;
            tbName.Text = new bUser().Detail(data.UserID).Name;
            tbDate.Text = Lib.ConvertDateToString(data.Date);
            tbPhone.Text = data.CustomerPhone;
            tbCount.Text = new bInvoiceDetail().TotalAmount(data.InvoiceCode).ToString();
            tbQuantity.Text = new bInvoiceDetail().TotalQuantity(data.InvoiceCode).ToString();
            tbTotal.Text = Lib.ConvertPrice(data.Total.ToString(), true);
        }

        private void deleteInvoice(string invoiceCode)
        {
            var _invoice = new bInvoice().Detail(invoiceCode);
            var listProdBak = new List<Product>();
            var listInvoiceBak = new List<InvoiceDetail>();
            bInvoice b = new bInvoice();
            string mess = $"#{_invoice.InvoiceCode}: {_invoice.InvoiceCode}";

            if (!b.IsExists(_invoice.InvoiceCode))
            {
                ShowMess.Error__NotExists(mess);
                return;
            }

            var listInvoiceDetail = new bInvoiceDetail().List(_invoice.InvoiceCode);

            foreach (var i in listInvoiceDetail)
            {
                bool rs = new bInvoiceDetail().Delete(i.InvoiceID, i.ProductID);

                if (!rs)
                {
                    ShowMess.Error__Deleted($"{i.InvoiceID}: {i.ProductID}");
                    listInvoiceBak.ForEach(e => new bInvoiceDetail().Add(e));
                    return;
                }

                listInvoiceBak.Add(i);
            }

            foreach (var i in listInvoiceBak)
            {
                var prod = new bProduct().Detail(i.ProductID);
                bool rs = new bProduct().Delete(i.ProductID);

                if (!rs)
                {
                    ShowMess.Error__CustomText($"Không thể xoá {prod.ProductCode}: {prod.Name}");
                    listProdBak.ForEach(e => new bProduct().Add(e));
                    listInvoiceBak.ForEach(e => new bInvoiceDetail().Add(e));
                    return;
                }

                listProdBak.Add(prod);
            }

            bool result = new bInvoice().Delete(_invoice.InvoiceCode);

            if (!result)
            {
                ShowMess.Error__CustomText($"Không thể xoá {mess}!");
                listProdBak.ForEach(e => new bProduct().Add(e));
                listInvoiceBak.ForEach(e => new bInvoiceDetail().Add(e));
                return;
            }

            ShowMess.Success__Deleted(mess);

            var lstImageNotDelete = new List<string>();

            // thực hiên xóa các hình ảnh
            foreach (var i in listProdBak)
            {
                try
                {
                    Lib.ImageDelete(i.Image.Trim());
                }
                catch
                {
                    lstImageNotDelete.Add(i.Image); // thêm tên hình ảnh không xóa được vào bộ nhớ tạm
                }
            }

            // hiển thị thông báo có hình ảnh không xóa được
            if (lstImageNotDelete.Count > 0)
            {
                ShowMess.Error__CustomText($"Không thể xoá các hình ảnh [{String.Join(", ", lstImageNotDelete.ToArray())}]!");
            }

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        // kiểm tra các textbox điều có dữ liệu
        private bool checkInputNotEmpty()
        => Lib.ControlNotEmpty(pnInformation);

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