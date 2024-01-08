using ShopSimpleClassic.Controller;
using ShopSimpleClassic.Library;
using ShopSimpleClassic.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace ShopSimpleClassic.View.Detail
{
    public delegate void SendProductChoice(string productCode);

    public delegate void SendNewCustomer(string phone);

    public partial class fInvoice_Add : Form
    {
        // Variable

        private List<Tuple<string, int, decimal>> productList = new List<Tuple<string, int, decimal>>();
        private DateTime date;
        private bool isAdd, isViewOrEdit;
        private bool isSave = false, isStartCombobox = true;
        private string InvoiceCode;
        private string code__selected__prod;
        private int TotalProduct = 0;
        private Invoice _invoice;
        private List<Product> listProd = new List<Product>(); // danh sách sản phẩm, thay đổi dữ liệu để không ảnh hưởng tới db
        private List<InvoiceDetail> listInvoiceDetail = new List<InvoiceDetail>();

        // Main

        public fInvoice_Add(bool IsAdd)
        {
            InitializeComponent();
            this.isAdd = IsAdd;
            this.isViewOrEdit = true;
            load();
        }

        public fInvoice_Add(string Code, bool IsViewOrEdit)
        {
            InitializeComponent();
            this.isViewOrEdit = IsViewOrEdit;
            this.isAdd = false;
            load(Code);
        }

        private void dgvList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            dgvList.Rows.Cast<DataGridViewRow>().ToList().ForEach(i => i.MinimumHeight = 100);
        }

        private void cbPhone_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (isStartCombobox) return;
            tbNameCust.Text = string.IsNullOrEmpty(cbPhone.Text) ? "" : new bCustomer().Detail(cbPhone.Text?.ToString()).Name;
        }

        private void tbAmount_TextChanged(object sender, EventArgs e)
        {
            if (!int.TryParse(tbAmount.Text.Trim(), out _) || string.IsNullOrEmpty(tbAmount.Text.Trim()))
                return;

            var amount = int.Parse(tbAmount.Text.Trim());
            var price = Lib.ConvertIntFromPrice(tbCast);

            tbPrice.Text = Lib.ConvertPrice((price * amount).ToString(), true);
        }

        private void btPrint_Click(object sender, EventArgs e)
        {
            if (listProd.Count == 0)
            {
                ShowMess.Error__CustomText("Không thể in hoá đơn khi không có sản phẩm!");
                return;
            }

            showPrintPreview();
        }

        private void btPrimary_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvList.RowCount == 0)
                {
                    ShowMess.Error__InputNotEmpty();
                    return;
                }

                saveInvoice();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btDanger_Click(object sender, EventArgs e)
        {
            try
            {
                string text = isAdd ? "Bạn có muốn huỷ đơn bán hàng này không" : "Bạn có muốn xoá đơn bán hàng này không";

                if (ShowMess.Question__CustomText(text) == DialogResult.No) return;

                if (isAdd) this.Dispose();
                else deleteInvoice();
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
                if (isAdd)
                {
                    Lib.ClearText(pnInformation);
                    Lib.ClearText(pnProduct);
                    Lib.ClearCombobox(pnInformation);
                    dgvList.Rows.Clear();
                    resetInformation();
                }
                else
                {
                    dgvList.Rows.Clear();
                    clearProdControl();
                    loadList();
                    resetInformation();
                }
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btAdd_Phone_Click(object sender, EventArgs e)
        {
            using (var frm = new fCustomer_Add(getSenderCustomer))
            {
                frm.ShowDialog();
            }
        }

        private void btFind_Click(object sender, EventArgs e)
        {
            using (var waitForm = new fLoading()) // Sử dụng biến cục bộ để tạo form chờ
            {
                waitForm.TopMost = true;
                waitForm.StartPosition = FormStartPosition.CenterParent;
                waitForm.Show();
                waitForm.Refresh();
                System.Threading.Thread.Sleep(200);

                waitForm.Close(); // Đóng form chờ

                using (var frm = new fFind_Product(getSenderProduct))
                {
                    frm.ShowDialog();
                }
            }
        }

        private void btRefreshInvoide_Click(object sender, EventArgs e)
        {
            Lib.ClearText(pnInformation);
            cbPhone.SelectedIndex = -1;
            resetInformation();
        }

        private void btRefreshProd_Click(object sender, EventArgs e)
        {
            try
            {
                clearProdControl();
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
                updateProdList();
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
                deleteProdList();
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
                addProdList();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void dgvList_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            code__selected__prod = dgvList.RowCount != 0 ? dgvList.CurrentRow.Cells[2].Value.ToString() : null;

            if (code__selected__prod == null) return;

            var d = listProd.FirstOrDefault(i => i.ProductCode == code__selected__prod); // lấy dữ liệu của sản phẩm trong danh tạm của form, khi chỉnh sửa hay thay đổi điều không ảnh hưởng tới db

            if (d == null) return;

            tbProdCode.Text = d.ProductCode;
            tbProdName.Text = d.Name;
            tbAmount.Text = d.Amount.ToString();
            tbCast.Text = Lib.ConvertPrice(new bProduct().Detail(code__selected__prod).Price.ToString(), true);
            tbPrice.Text = Lib.ConvertPrice(d.Price.ToString(), true);
            Lib.ImageLoadOneImage(new bProduct().Detail(code__selected__prod).Image, picImage);
        }

        private void frmInvoice__Detail_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isSave || dgvList.RowCount == 0 || isViewOrEdit)
            {
                e.Cancel = false;
                return;
            }

            if (ShowMess.Question__ExitForm(this.Text) == DialogResult.No)
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        // function
        private void load()
        {
            getCombobox();
            resetInformation();
        }

        private void load(string code)
        {
            getCombobox();
            CURD__Control__EnableDisable();
            binding(code);
            resetInformation();

            btPrimary.Visible = false;
            btDanger.Text = "Xoá đơn";
            eventRole();
        }

        // kiểm tra phân quyền để hiển thị chức năng đúng với phân quyền người dùng.
        private void eventRole()
        {
            if (Temp.IsAdmin) return;
            btDanger.Visible = Temp.Username.Equals(tbUsername.Text.Trim());
        }

        // cho phép hoặc không cho phép thao tác khi ở các trạng thái thêm, sửa hoặc chi tiết
        private void CURD__Control__EnableDisable()
        {
            Lib.ReadOnlyControl(pnProduct, isViewOrEdit);
            Lib.ReadOnlyControl(pnInformation, isViewOrEdit);
            tbCodeInvoide.ReadOnly = tbUsername.ReadOnly = tbNameEmployee.ReadOnly = tbNameCust.ReadOnly = tbDate.ReadOnly = true;
            btAdd_Phone.Visible = btRefreshInvoide.Visible = isAdd;

            if (!isAdd)
            {
                cbPhone.DropDownStyle = isViewOrEdit ? ComboBoxStyle.Simple : ComboBoxStyle.DropDown;
                btAdd_Phone.Enabled = btDelete.Enabled = btEdit.Enabled = btAdd.Enabled = btFind.Enabled = btRefreshProd.Enabled = !isViewOrEdit;
            }
        }

        // hiển thị dữ liệu đã lưu lên các control
        private void binding(string code)
        {
            _invoice = new bInvoice().Detail(code);
            date = _invoice.Date;
            binding__control();
            binding__list();
        }

        private void binding__control()
        {
            tbCodeInvoide.Text = _invoice.InvoiceCode;
            tbDate.Text = Lib.ConvertDateToString(_invoice.Date);
            tbUsername.Text = _invoice.UserID;
            tbNameCust.Text = _invoice.Customer.Name;
            tbNameEmployee.Text = _invoice.User.Name;
            TotalProduct = _invoice.Total;
            cbPhone.SelectedValue = _invoice.CustomerPhone;
            lblTotal.Text = $"{lblTotal.Tag}: {Lib.ConvertPrice(TotalProduct.ToString(), true)}đ";
            lblCount.Text = $"{lblCount.Tag} {new bInvoiceDetail().TotalAmount(_invoice.InvoiceCode)}";
            lblQuantity.Text = $"{lblQuantity.Tag} {new bInvoiceDetail().TotalQuantity(_invoice.InvoiceCode)}";
        }

        // tải danh sách đã lưu
        private void binding__list()
        {
            try
            {
                var list = new bInvoiceDetail().List(_invoice.InvoiceCode);
                list.ToList().ForEach(i =>
                {
                    var dProd = new bProduct().Detail(i.ProductID);
                    dProd.Amount = i.Quantity;
                    dProd.Price = i.Price;
                    listProd.Add(dProd);
                });
                loadList();
            }
            catch
            {
                dgvList.DataSource = null;
            }
        }

        // làm mới lại các thông tin chính trong form
        private void resetInformation()
        {
            resetTime();
            setReportFromProdList();
            InvoiceCode = isAdd ? new bInvoice().CreateKey() : _invoice.InvoiceCode;
            pnInformation.Text = $"{pnInformation.Tag}: #{InvoiceCode}";
            tbCodeInvoide.Text = InvoiceCode;

            if (isAdd)
            {
                tbUsername.Text = Temp.Username;
                tbNameEmployee.Text = new bUser().Detail(Temp.Username).Name;
            }
        }

        // làm mới thời gian
        private void resetTime()
        {
            if (isAdd)
            {
                date = DateTime.Now;
                tbDate.Text = Lib.ConvertDateToString(date);
            }
        }

        // đóng gói dữ liệu cho đơn bán hàng
        private void package()
        {
            packageInvoice();
            packageInvoiceDetail();
        }

        // đóng gói đơn tổng hợp
        private void packageInvoice()
        {
            _invoice = new Invoice
            {
                InvoiceCode = InvoiceCode,
                UserID = tbUsername.Text.Trim(),
                CustomerPhone = cbPhone.SelectedValue?.ToString(),
                Date = date,
                Total = TotalProduct
            };
        }

        // đóng gói đơn chi tiết
        private void packageInvoiceDetail()
        {
            listInvoiceDetail = new List<InvoiceDetail>();
            listInvoiceDetail.AddRange(listProd.Select(i => new InvoiceDetail
            {
                InvoiceID = InvoiceCode,
                ProductID = i.ProductCode,
                Quantity = i.Amount,
                Price = i.Price
            }));
        }

        // hiển thị thông tin tổng hợp khi thay đổi dữ liệu của các thành phần trong danh sách sản phẩm
        private void setReportFromProdList()
        {
            TotalProduct = 0;
            listProd.ForEach(i => TotalProduct += i.Price);

            lblCount.Text = $"{lblCount.Tag} {dgvList.RowCount}";
            lblQuantity.Text = $"{lblQuantity.Tag} {listProd.Sum(e => e.Amount)}";
            lblTotal.Text = $"{lblTotal.Tag} {Lib.ConvertPrice(TotalProduct.ToString(), true)}đ";
        }

        // tải dữ liệu lên datagridview
        private void loadList()
        {
            dgvList.Rows.Clear();
            dgvList.Rows.AddRange(listProd.Select((i, count) => new DataGridViewRow
            {
                Cells =
                    {
                        new DataGridViewTextBoxCell { Value = count + 1},
                        new DataGridViewImageCell {   Value = Lib.ImageLoad__ForList(new bProduct().Detail(i.ProductCode).Image) },
                        new DataGridViewTextBoxCell { Value = i.ProductCode },
                        new DataGridViewTextBoxCell { Value = i.Name },
                        new DataGridViewTextBoxCell { Value = new bCatalog().Detail(new bProduct().Detail(i.ProductCode).CatalogID).Name },
                        new DataGridViewTextBoxCell { Value = new bSupplier().Detail(new bProduct().Detail(i.ProductCode).SupplierID).Name },
                        new DataGridViewTextBoxCell { Value = i.Amount },
                        new DataGridViewTextBoxCell { Value = Lib.ConvertPrice(new bProduct().Detail(i.ProductCode).Price.ToString(), true) },
                        new DataGridViewTextBoxCell { Value = Lib.ConvertPrice(i.Price.ToString(), true) }
                    }
            }).ToArray());

            setReportFromProdList();
        }

        // nhận giá trị từ control trong thông tin sản phẩm
        private Product getValueProduct()
        {
            return new Product
            {
                ProductCode = tbProdCode.Text.Trim(),
                Name = tbProdName.Text.Trim(),
                Amount = int.Parse(tbAmount.Text.Trim()),
                Price = Lib.ConvertIntFromPrice(tbPrice)
            };
        }

        // thêm sản phẩm vào danh sách sản phẩm
        private void addProdList()
        {
            if (!checkInputProdNotEmpty())
            {
                ShowMess.Error__InputNotEmpty();
                return;
            }

            var data = getValueProduct();

            if (string.IsNullOrEmpty(data.ProductCode))
            {
                ShowMess.Error__EmptyCode();
                return;
            }

            if (listProd.FindIndex(e => e.ProductCode.Equals(data.ProductCode)) >= 0)
            {
                ShowMess.Error__AlreadyExist($"{data.ProductCode}: {data.Name}");
                return;
            }

            listProd.Add(data);

            clearProdControl();
            loadList();
        }

        // sửa sản phẩm trong danh sách
        private void updateProdList()
        {
            if (!checkInputProdNotEmpty())
            {
                ShowMess.Error__InputNotEmpty();
                return;
            }

            var data = getValueProduct();

            if (string.IsNullOrEmpty(data.ProductCode))
            {
                ShowMess.Error__InputNotEmpty();
                return;
            }

            int index = listProd.FindIndex(e => e.ProductCode.Equals(data.ProductCode));

            if (index < 0)
            {
                ShowMess.Error__CustomText($"Không tồn tại \'{data.ProductCode}: {data.Name}\' trong danh sách!");
                return;
            }

            listProd[index] = data;

            clearProdControl();
            loadList();
        }

        // xoá snar phẩm trong danh sách
        private void deleteProdList()
        {
            var idProd = tbProdCode.Text.Trim();

            if (!checkInputProdNotEmpty())
            {
                ShowMess.Error__CustomText("không có dữ liệu để xoá!");
                return;
            }

            if (string.IsNullOrEmpty(idProd))
            {
                ShowMess.Error__CustomText("Không có mã sản phẩm!");
                return;
            }

            int index = listProd.FindIndex(e => e.ProductCode.Equals(idProd));

            if (index >= 0)
            {
                listProd.RemoveAt(index);
                clearProdControl();
                loadList();
                return;
            }

            ShowMess.Error__CustomText($"Không có mã {idProd} trong danh sách!");
        }

        private void getCombobox()
        {
            getComboboxPhone();
        }

        private void getComboboxPhone()
        {
            cbPhone.DataSource = new bCustomer().ListForCombobox();
            cbPhone.DisplayMember = "Display";
            cbPhone.ValueMember = "Value";
            isStartCombobox = false;
            cbPhone.SelectedIndex = -1;
        }

        private void saveInvoice()
        {
            package();
            bInvoice b = new bInvoice();
            string mess = $"#{_invoice.InvoiceCode}";

            if (b.IsExists(_invoice.InvoiceCode) && isAdd)
            {
                ShowMess.Error__CustomText($"Đã tồn tại đơn bán hàng #{_invoice.InvoiceCode}!");
                return;
            }

            foreach (var i in listInvoiceDetail)
            {
                if (i.Quantity > new bProduct().Detail(i.ProductID).Amount)
                {
                    ShowMess.Error__CustomText($"Số lượng của \'{i.ProductID}: {new bProduct().Detail(i.ProductID).Name}, ({i.Quantity})\' lớn hơn số lượng tồn kho ({new bProduct().Detail(i.ProductID).Amount})!");
                    return;
                }
            }

            bool result = isAdd ? new bInvoice().Add(_invoice) : new bInvoice().Update(_invoice);

            if (!result)
            {
                ShowMess.Error__AddedOrUpdated(mess, isAdd);
                return;
            }

            result = saveInvoiceDetail();

            if (!result)
            {
                new bInvoice().Delete(_invoice.InvoiceCode);
                ShowMess.Error__CustomText("Không thể lưu đơn chi tiết!");
                return;
            }

            ShowMess.Success__AddedOrUpdated(mess, isAdd);

            if (!isAdd)
            {
                isViewOrEdit = true;
                CURD__Control__EnableDisable();
            }
            else
            {
                this.DialogResult = DialogResult.Yes;
                isSave = true;
                this.Close();
            }
        }

        private bool saveInvoiceDetail()
        {
            var listBak = new List<InvoiceDetail>();
            var listProdBak = new List<Product>();

            try
            {
                foreach (var i in listInvoiceDetail)
                {
                    bool rs = new bInvoiceDetail().Add(i);

                    if (!rs)
                    {
                        if (listBak.Count != 0)
                        {
                            listBak.ForEach(e => new bInvoiceDetail().Delete(e.InvoiceID, e.ProductID));
                            listProdBak.ForEach(e => new bProduct().UpdateAmount(e.ProductCode, e.Amount, true));
                        }

                        return false;
                    }

                    listBak.Add(i);

                    rs = new bProduct().UpdateAmount(i.ProductID, i.Quantity, false);

                    if (!rs)
                    {
                        listBak.ForEach(e => new bInvoiceDetail().Delete(e.InvoiceID, e.ProductID));
                        listProdBak.ForEach(e => new bProduct().UpdateAmount(e.ProductCode, e.Amount, true));
                        ShowMess.Error__CustomText("Không thể cập nhật lại số lượng sản phẩm!");
                        return false;
                    }

                    var d = new Product { ProductCode = i.ProductID, Amount = i.Quantity };
                    listProdBak.Add(d);
                }
            }
            catch
            {
                listBak.ForEach(i => new bInvoiceDetail().Delete(i.InvoiceID, i.ProductID));
            }
            return true;
        }

        // xoá hoá đơn
        private void deleteInvoice()
        {
            package();
            var listProdBak = new List<Product>();
            var listInvoiceBak = new List<InvoiceDetail>();
            bInvoice b = new bInvoice();
            string mess = $"#{_invoice.InvoiceCode}: {Lib.ConvertDateToString(_invoice.Date)}";

            if (!b.IsExists(_invoice.InvoiceCode))
            {
                ShowMess.Error__NotExists(mess);
                return;
            }

            foreach (var i in listInvoiceDetail)
            {
                bool rs = new bInvoiceDetail().Delete(i.InvoiceID, i.ProductID);

                if (!rs)
                {
                    ShowMess.Error__Deleted($"{i.InvoiceID}: {i.ProductID} - {i.Product.Name}");
                    listInvoiceBak.ForEach(e => new bInvoiceDetail().Add(e));
                    return;
                }

                listInvoiceBak.Add(i);
            }

            foreach (var i in listInvoiceBak)
            {
                var prod = new bProduct().Detail(i.ProductID);
                bool rs = new bProduct().UpdateAmount(i.ProductID, i.Quantity, true);

                if (!rs)
                {
                    ShowMess.Error__Deleted($"{i.ProductID} - {i.Product.Name}");
                    rollbackForDelete(listProdBak, listInvoiceBak);
                    return;
                }

                prod.Amount = i.Quantity;
                listProdBak.Add(prod);
            }

            bool result = new bInvoice().Delete(_invoice.InvoiceCode);

            if (!result)
            {
                ShowMess.Error__CustomText($"Không thể xoá {mess}!");
                rollbackForDelete(listProdBak, listInvoiceBak);
                return;
            }

            ShowMess.Success__Deleted(mess);

            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        // khôi phục lại dữ liệu đã xoá
        private void rollbackForDelete(List<Product> listProdBak, List<InvoiceDetail> listInvoiceBak)
        {
            listProdBak.ForEach(e => new bProduct().UpdateAmount(e.ProductCode, e.Amount, false));
            listInvoiceBak.ForEach(e => new bInvoiceDetail().Add(e));
        }

        private bool checkInputProdNotEmpty()
        {
            return Lib.ControlNotEmpty(pnProduct);
        }

        // Làm mới lại thông tin sản phẩm
        private void clearProdControl()
        {
            tbProdCode.Clear();
            tbProdName.Clear();
            tbAmount.Clear();
            tbCast.Clear();
            tbPrice.Clear();
            Lib.ImageLoad__Null(picImage);
        }

        // xử lý dữ liệu sau khi thêm khách hàng
        private void getSenderCustomer(string phone)
        {
            getComboboxPhone();
            cbPhone.SelectedValue = phone;
            tbNameCust.Text = new bCustomer().Detail(phone).Name;
        }

        // xử lý dữ liệu sau khi thêm khách hàng
        private void getSenderProduct(string productCode)
        {
            var d = new bProduct().Detail(productCode);
            tbProdCode.Text = productCode;
            tbProdName.Text = d.Name;
            tbAmount.Text = "1";
            tbCast.Text = Lib.ConvertPrice(d.Price.ToString(), true);
            tbPrice.Text = Lib.ConvertPrice((d.Price * int.Parse(tbAmount.Text.ToString())).ToString(), true);
            Lib.ImageLoadOneImage(new bProduct().Detail(productCode).Image, picImage);
        }

        private void fillList()
        => productList = listProd.Select(i => new Tuple<string, int, decimal>(i.Name, i.Amount, i.Price)).ToList();

        private void showPrintPreview()
        {
            if (!Directory.Exists(Lib.ExportFolderPath()))
            {
                Directory.CreateDirectory(Lib.ExportFolderPath());
            }

            fillList();

            // Cấu hình PrintDocument (thay đổi tùy theo nhu cầu in của bạn)
            printDocument1.PrintPage += PrintDocument_PrintPage;

            // Liên kết PrintDocument với PrintPreviewDialog
            printPreviewDialog1.Document = printDocument1;
            // In dữ liệu vào tập tin ảnh
            printDocument1.PrinterSettings.PrintToFile = true;
            printDocument1.PrinterSettings.PrintFileName = Path.Combine(Library.Lib.ExportFolderPath(), "temp.png");
            printDocument1.Print();

            // Hiển thị PrintPreviewDialog
            printPreviewDialog1.Size = new Size(800, 600);
            printPreviewDialog1.StartPosition = FormStartPosition.CenterScreen;
            printPreviewDialog1.ShowDialog();
        }

        private void PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            // Điều chỉnh nội dung cần in theo ý muốn
            string title = $"Hoá Đơn #{InvoiceCode}";
            string storeName = "Cửa Hàng: ShopSimple";
            string contactNumber = "SĐT Liên Hệ: 0988576412";
            string employeeInfo = $"Tên Nhân Viên: {tbNameEmployee.Text}";
            string customerName = $"Tên Khách Hàng: {tbNameCust.Text}";
            string customerPhone = $"SĐT Khách Hàng: {cbPhone.Text}";
            string purchaseTime = $"TNgày Mua: {tbDate.Text}";
            string productNameHeader = "Sản Phẩm";
            string quantityHeader = "Số Lượng";
            string unitPriceHeader = "Đơn Giá";
            string totalPriceHeader = "Thành Tiền";
            string separatorLine = new string('-', e.PageBounds.Width - 100);
            // Tính tổng số sản phẩm và tổng số lượng
            int totalProducts = productList.Count;
            int totalQuantity = productList.Sum(p => p.Item2);

            Font titleFont = new Font("Arial", 28, FontStyle.Bold, GraphicsUnit.Point);
            Font headerFont = new Font("Arial", 14, FontStyle.Bold, GraphicsUnit.Point);
            Font infoFont = new Font("Arial", 14, FontStyle.Regular, GraphicsUnit.Point);
            Brush infoBrush = Brushes.Black;

            int columnWidth = e.PageBounds.Width / 2;
            int xLeftColumn = 50;
            int xRightColumn = columnWidth + 50;
            int yStart = 100; // Vị trí bắt đầu vẽ thông tin

            Brush titleBrush = Brushes.Black;
            Point titlePoint = new Point(e.PageBounds.Width / 2 - (int)e.Graphics.MeasureString(title, titleFont).Width / 2, 50);

            // Vẽ tiêu đề hoá đơn
            e.Graphics.DrawString(title, titleFont, titleBrush, titlePoint);

            // Vẽ thông tin cửa hàng và liên hệ (cột bên trái)
            e.Graphics.DrawString(storeName, infoFont, infoBrush, new Point(xLeftColumn, yStart));
            e.Graphics.DrawString(contactNumber, infoFont, infoBrush, new Point(xLeftColumn, yStart + 30));
            e.Graphics.DrawString(employeeInfo, infoFont, infoBrush, new Point(xLeftColumn, yStart + 60));

            // Vẽ thông tin khách hàng và thời gian mua (cột bên phải)
            e.Graphics.DrawString(customerName, infoFont, infoBrush, new Point(xRightColumn, yStart));
            e.Graphics.DrawString(customerPhone, infoFont, infoBrush, new Point(xRightColumn, yStart + 30));
            e.Graphics.DrawString(purchaseTime, infoFont, infoBrush, new Point(xRightColumn, yStart + 60));

            // Vẽ danh sách sản phẩm (cột bên trái)
            int x = xLeftColumn;
            int y = yStart + 100; // Dịch chuyển xuống dưới các thông tin trước đó

            e.Graphics.DrawString(productNameHeader, headerFont, infoBrush, new Point(x, y));
            e.Graphics.DrawString(quantityHeader, headerFont, infoBrush, new Point(x + columnWidth / 2, y));
            e.Graphics.DrawString(unitPriceHeader, headerFont, infoBrush, new Point(x + columnWidth, y));
            e.Graphics.DrawString(totalPriceHeader, headerFont, infoBrush, new Point(x + columnWidth + columnWidth / 2, y));

            y += 40;
            foreach (var product in productList)
            {
                string productName = product.Item1;
                int quantity = product.Item2;
                decimal unitPrice = product.Item3;
                decimal totalPrice = quantity * unitPrice;

                e.Graphics.DrawString(productName, infoFont, infoBrush, new Point(x, y));
                e.Graphics.DrawString(quantity.ToString(), infoFont, infoBrush, new Point(x + columnWidth / 2, y));
                e.Graphics.DrawString(Lib.ConvertPrice(unitPrice.ToString(), true), infoFont, infoBrush, new Point(x + columnWidth, y));
                e.Graphics.DrawString(Lib.ConvertPrice(totalPrice.ToString(), true), infoFont, infoBrush, new Point(x + columnWidth + columnWidth / 2, y));

                y += 40;
            }
            // Vẽ tổng số sản phẩm và tổng số lượng
            // Tính vị trí x để tổng số sản phẩm và tổng số lượng nằm giữa trang
            int centerPointX = e.PageBounds.Width / 2;
            int totalProductsX = centerPointX - (int)e.Graphics.MeasureString($"Tổng số sản phẩm: {totalProducts}  Tổng số lượng: {totalQuantity}", infoFont).Width / 2;

            e.Graphics.DrawString($"Tổng số sản phẩm: {totalProducts}  Tổng số lượng: {totalQuantity}", infoFont, infoBrush, new Point(totalProductsX, y));

            // Vẽ đường gạch ngang cuối cùng
            // Tính vị trí để căn chỉnh gạch ngang giữa trang
            int separatorX = e.PageBounds.Width / 2 - (int)e.Graphics.MeasureString(separatorLine, infoFont).Width / 2;
            int separatorY = y + 40;
            e.Graphics.DrawString(separatorLine, infoFont, infoBrush, new Point(separatorX, separatorY));

            // Tính tổng tiền và vẽ (cột bên phải)
            decimal totalAmount = productList.Sum(p => p.Item2 * p.Item3);
            int totalAmountY = separatorY + 20;
            e.Graphics.DrawString($"Tổng Tiền: {Lib.ConvertPrice(totalAmount.ToString(), true)}", infoFont, infoBrush, new Point(xRightColumn, totalAmountY));

            // Đánh dấu đã hoàn thành việc in
            e.HasMorePages = false;
        }
    }
}