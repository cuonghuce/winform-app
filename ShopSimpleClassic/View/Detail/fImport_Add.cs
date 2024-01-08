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
    public delegate void SendCatalogData(string code);

    public delegate void SendSupplierData(string code);

    public partial class fImport_Add : Form
    {
        private List<Tuple<string, int, decimal>> productList = new List<Tuple<string, int, decimal>>();
        private DateTime date;
        private bool isAdd, isViewOrEdit;
        private bool isSave = false;
        private bool isChangeImage = false; // kiểm tra tình trạng có thay đổi ảnh hay không, dùng trong trường hợp đã thêm hình ảnh trước đó
        private string InvoiceCode;
        private string pathImage, code__selected__prod;
        private int TotalProduct = 0;
        private Import _invoice;
        private List<Product> listProd = new List<Product>(); // danh sách sản phẩm, thay đổi dữ liệu để không ảnh hưởng tới db
        private List<ImagePathList> listProdPathImage = new List<ImagePathList>(); // danh sách chứa thông tin hình ảnh, sử dụng khi thêm hình ảnh cho sản phẩm nhưng chưa lưu dữ liệu vào db
        private List<ImportDetail> listInvoiceDetail = new List<ImportDetail>();

        public fImport_Add(bool IsAdd)
        {
            InitializeComponent();
            this.isAdd = IsAdd;
            this.isViewOrEdit = true;
            load();
        }

        public fImport_Add(string Code, bool IsViewOrEdit)
        {
            InitializeComponent();
            this.isViewOrEdit = IsViewOrEdit;
            this.isAdd = false;
            load(Code);
        }

        private void tbPrice_Enter(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPrice.Text.Trim())) return;
            tbPrice.Text = Lib.ConvertPrice(tbPrice.Text.Trim(), false);
        }

        private void tbPrice_Leave(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(tbPrice.Text.Trim())) return;
            tbPrice.Text = Lib.ConvertPrice(tbPrice.Text.Trim(), true);
        }

        private void dgvList_CellPainting(object sender, DataGridViewCellPaintingEventArgs e)
        {
            dgvList.Rows.Cast<DataGridViewRow>().ToList().ForEach(i => i.MinimumHeight = 100);
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
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
                    ShowMess.Error__CustomText("Không thể lưu dữ liệu!");
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
                string text = isAdd ? "Bạn có muốn huỷ đơn nhập hàng này không" : "Bạn có muốn xoá đơn nhập hàng này không";

                if (ShowMess.Question__CustomText(text) == DialogResult.No) return;

                if (isAdd) this.Dispose();
                else deleteInvoice();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btRefreshCodeProduct_Click(object sender, EventArgs e)
        {
            try
            {
                tbCodeProd.Text = new bProduct().CreateKey();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btAdd_Catalog_Click(object sender, EventArgs e)
        {
            try
            {
                new fCatalog_Add(getSenderCatalog).ShowDialog();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btAdd_Supplier_Click(object sender, EventArgs e)
        {
            try
            {
                new fSupplier_Add(getSenderSupplier).ShowDialog();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
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

        private void btRefresh_Click(object sender, EventArgs e)
        {
            try
            {
                if (isAdd)
                {
                    Lib.ClearText(pnInformation);
                    Lib.ClearText(pnProduct);
                    Lib.ClearCombobox(pnProduct);
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

        private void btRefreshInvoide_Click(object sender, EventArgs e)
        {
            if (isAdd)
            {
                Lib.ClearText(pnInformation);
                resetInformation();
            }
        }

        private void btProdClear_Click(object sender, EventArgs e)
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

        private void btProdEdit_Click(object sender, EventArgs e)
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

        private void btProdDelete_Click(object sender, EventArgs e)
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

        private void btProdAddToList_Click(object sender, EventArgs e)
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

            tbCodeProd.Text = d.ProductCode;
            tbNameProd.Text = d.Name;
            tbImage.Text = d.Image;
            tbAmount.Text = d.Amount.ToString();
            tbPrice.Text = Lib.ConvertPrice(d.Price.ToString(), true);
            cbCatalog.SelectedValue = d.CatalogID;
            cbSupplier.SelectedValue = d.SupplierID;
            loadImage();
        }

        private void fImport_Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.DialogResult = DialogResult.Yes;
            if (isSave || dgvList.RowCount == 0 || isViewOrEdit)
            {
                e.Cancel = false;
                return;
            }

            if (ShowMess.Question__CustomText("Bạn có muốn thoát Nhập hàng không?") == DialogResult.No)
                e.Cancel = true;
            else
                e.Cancel = false;
        }

        // function
        /// <summary>
        /// Tải dữ liệu
        /// </summary>
        private void load()
        {
            getCombobox();
            resetInformation();
            btDanger.Text = "Huỷ Đơn";
            btDanger.Image = Properties.Resources.close;
        }

        /// <summary>
        /// Tải dữ liệu đã chỉ định dựa trên mã được cung cấp và định cấu hình các điều khiển tương ứng.
        /// </summary>
        /// <param name="code"> Mã được sử dụng để xác định dữ liệu cần tải </param>
        private void load(string code)
        {
            getCombobox();
            CURD__Control__EnableDisable();
            binding(code);
            resetInformation();

            btPrimary.Visible = false;
            btDanger.Text = "Xoá đơn";
            btDanger.Image = Properties.Resources.delete;
            cbCatalog.SelectedIndex = cbSupplier.SelectedIndex = -1;
        }

        /// <summary>
        /// Kích hoạt hoặc vô hiệu hóa các control dựa trên chế độ hiện tại (xem/chỉnh sửa).
        /// </summary>
        private void CURD__Control__EnableDisable()
        {
            // Làm cho các control trong pnInformation và pnProduct chỉ được đọc dựa trên cờ isViewOrEdit.
            Lib.ReadOnlyControl(pnInformation, isViewOrEdit);
            Lib.ReadOnlyControl(pnProduct, isViewOrEdit);

            // Đặt các control tbCode_invoice, tbUser, tbEmployeeName và tbDate là chỉ đọc.
            tbCodeInvoide.ReadOnly = tbUsername.ReadOnly = tbNameEmployee.ReadOnly = tbDate.ReadOnly = true;
            // Kích hoạt hoặc vô hiệu hóa các nút cụ thể dựa trên cờ isViewOrEdit.
            btAdd_Image.Visible = btEdit.Visible = btDelete.Visible = btRefreshCodeProduct.Visible = btAdd.Visible = btAdd_Catalog.Visible = btAdd_Supplier.Visible = btRefreshInvoide.Visible = !isViewOrEdit;
            cbCatalog.Enabled = cbSupplier.Enabled = !isViewOrEdit;
            cbCatalog.DropDownStyle = cbSupplier.DropDownStyle = isViewOrEdit ? ComboBoxStyle.Simple : ComboBoxStyle.DropDown;
        }

        /// <summary>
        /// Ràng buộc dữ liệu vào các điều khiển dựa trên mã code được cung cấp.
        /// </summary>
        /// <param name="code">Mã code được sử dụng để lấy chi tiết hóa đơn.</param>
        private void binding(string code)
        {
            _invoice = new bImport().Detail(code); // Lấy chi tiết hóa đơn dựa trên mã code
            date = _invoice.Date; // Đặt biến date bằng ngày của hóa đơn
            binding__control(); // Ràng buộc điều khiển

            try
            {
                binding__list(); // Thử ràng buộc danh sách
            }
            catch
            {
                dgvList.DataSource = null; // Nếu xảy ra lỗi, đặt nguồn dữ liệu của DataGridView là null
            }
        }

        /// <summary>
        /// Ràng buộc dữ liệu vào các control chính.
        /// </summary>
        private void binding__control()
        {
            tbCodeInvoide.Text = _invoice.ImportCode;
            tbDate.Text = Lib.ConvertDateToString(_invoice.Date);
            tbUsername.Text = _invoice.UserID;
            tbNameEmployee.Text = _invoice.Admin.Name;
            TotalProduct = _invoice.Total;
            lblTotal.Text = $"{lblTotal.Tag}: {Lib.ConvertPrice(TotalProduct.ToString(), true)}đ"; // Đặt giá trị của lblTotal thành thông tin tổng tiền sản phẩm (được định dạng và thêm đơn vị tiền tệ)
            lblCount.Text = $"{lblCount.Tag} {new bImportDetail().TotalAmount(_invoice.ImportCode)}"; // Đặt giá trị của lblCount thành thông tin số lượng sản phẩm trong hóa đơn
            lblQuantity.Text = $"{lblQuantity.Tag} {new bImportDetail().TotalQuantity(_invoice.ImportCode)}"; // Đặt giá trị của lblQuantity thành thông tin tổng số lượng sản phẩm trong hóa đơn
        }

        /// <summary>
        /// Ràng buộc danh sách sản phẩm.
        /// </summary>
        private void binding__list()
        {
            var list = new bImportDetail().List(_invoice.ImportCode);

            foreach (var i in list)
            {
                var dProd = new bProduct().Detail(i.ProductID);
                dProd.Amount = i.Quantity;
                listProd.Add(dProd);
            }

            loadList();
        }

        /// <summary>
        /// Thiết lập lại thông tin.
        /// </summary>
        private void resetInformation()
        {
            resetTime();
            setReportFromProdList();
            InvoiceCode = isAdd ? new bImport().CreateKey() : _invoice.ImportCode;
            lblTitle.Text = $"{lblTitle.Tag} #{InvoiceCode}";
            lblInformationInvoice.Text = $"{lblInformationInvoice.Tag} #{InvoiceCode}";
            tbCodeInvoide.Text = InvoiceCode;
            tbUsername.Text = isAdd ? Temp.Username : _invoice.UserID;
            tbNameEmployee.Text = new bAdmin().Detail(isAdd ? Temp.Username : _invoice.UserID).Name;

            Lib.CenterControl(panel1, lblTitle);
            Lib.CenterControl(panel2, lblInformationInvoice);
        }

        /// <summary>
        /// Thiết lập lại thời gian.
        /// </summary>
        private void resetTime()
        {
            if (isAdd)
            {
                date = DateTime.Now;
                tbDate.Text = Lib.ConvertDateToString(date);
            }
        }

        /// <summary>
        /// đóng gói dữ liệu
        /// </summary>
        private void package()
        {
            packageInvoice();
            packageInvoiceDetail();
        }

        /// <summary>
        /// đóng gói dữ liệu hóa đơn tổng
        /// </summary>
        private void packageInvoice()
        {
            _invoice = new Import
            {
                ImportCode = InvoiceCode,
                UserID = tbUsername.Text.Trim(),
                Date = date,
                Total = TotalProduct
            };
        }

        /// <summary>
        /// đóng gói dữ liệu hóa đơn chi tiết hay sản phẩm trong hóa đơn
        /// </summary>
        private void packageInvoiceDetail()
        {
            listInvoiceDetail = new List<ImportDetail>();
            listInvoiceDetail.AddRange(listProd.Select(i => new ImportDetail
            {
                ImportID = InvoiceCode,
                ProductID = i.ProductCode,
                Quantity = i.Amount,
                Price = i.Price
            }));
        }

        /// <summary>
        /// thiết lập các báo cáo khi thay đổi dữ liệu trong danh sách
        /// </summary>
        private void setReportFromProdList()
        {
            TotalProduct = 0;
            listProd.ForEach(i => TotalProduct += i.Amount * i.Price);

            lblCount.Text = $"{lblCount.Tag} {dgvList.RowCount}";
            lblQuantity.Text = $"{lblQuantity.Tag} {listProd.Sum(e => e.Amount)}";
            lblTotal.Text = $"{lblTotal.Tag} {Lib.ConvertPrice(TotalProduct.ToString(), true)}đ";
        }

        /// <summary>
        /// tải dữ liệu lên danh sách
        /// </summary>
        private void loadList()
        {
            dgvList.Rows.Clear();
            dgvList.Rows.AddRange(listProd.Select((i, count) => new DataGridViewRow
            {
                Cells =
                {
                    new DataGridViewTextBoxCell { Value = count + 1},
                    new DataGridViewImageCell   { Value = isAdd ? Lib.ImageLoad__ForList(i.Image, listProdPathImage[count].Path) : Lib.ImageLoad__ForList(i.Image) },
                    new DataGridViewTextBoxCell { Value = i.ProductCode },
                    new DataGridViewTextBoxCell { Value = i.Name },
                    new DataGridViewTextBoxCell { Value = new bCatalog().Detail(i.CatalogID).Name },
                    new DataGridViewTextBoxCell { Value = new bSupplier().Detail(i.SupplierID).Name },
                    new DataGridViewTextBoxCell { Value = i.Amount },
                    new DataGridViewTextBoxCell { Value = Lib.ConvertPrice(i.Price.ToString(), true) },
                    new DataGridViewTextBoxCell { Value = Lib.ConvertPrice((i.Price * i.Amount).ToString(), true) }
                }
            }).ToArray());

            setReportFromProdList();
        }

        /// <summary>
        /// lấy giá trị của sản phẩm
        /// </summary>
        /// <returns></returns>
        private Product getValueProduct()
        => new Product
        {
            ProductCode = tbCodeProd.Text.Trim(),
            Name = tbNameProd.Text.Trim(),
            CatalogID = cbCatalog.SelectedValue?.ToString(),
            SupplierID = cbSupplier.SelectedValue?.ToString(),
            Image = tbImage.Text.Trim(),
            Amount = int.Parse(tbAmount.Text.Trim()),
            Price = Lib.ConvertIntFromPrice(tbPrice),
            CreateDate = date,
            Status = true
        };

        /// <summary>
        /// thêm dữ liệu vào danh sách
        /// </summary>
        private void addProdList()
        {
            if (!checkInputProdNotEmpty())
            {
                ShowMess.Error__InputEmpty();
                return;
            }

            if (!Lib.isNumeric(tbAmount.Text.Trim()) || !Lib.isNumeric(tbPrice.Text.Trim()))
            {
                ShowMess.Error__CustomText("Vui lòng nhập số tại [Số lượng] hoặc [Đơn giá]");
                return;
            }

            var data = getValueProduct();

            if (string.IsNullOrEmpty(data.ProductCode))
            {
                ShowMess.Error__EmptyCode();
                return;
            }

            if (listProd.Any(e => e.ProductCode.Equals(data.ProductCode)))
            {
                ShowMess.Error__AlreadyExist($"{data.ProductCode}: {data.Name}");
                return;
            }

            listProd.Add(data);
            // lưu các dữ liệu về hình ảnh khi chưa lưu vào hệ thống
            listProdPathImage.Add(new ImagePathList
            {
                Id = data.ProductCode,
                Path = pathImage,
                Image = data.Image
            });

            clearProdControl();
            loadList();
        }

        /// <summary>
        /// sửa dữ liệu trong danh sách
        /// </summary>
        private void updateProdList()
        {
            if (!checkInputProdNotEmpty())
            {
                ShowMess.Error__InputEmpty();
                return;
            }

            var data = getValueProduct();

            if (string.IsNullOrEmpty(data.ProductCode))
            {
                ShowMess.Error__EmptyCode();
                return;
            }

            int index = listProd.FindIndex(e => e.ProductCode.Equals(data.ProductCode)); // lấy chỉ mục của sản phẩm trong danh sách sản phẩm tạm

            if (index < 0)
            {
                ShowMess.Error__NotExists($"{data.ProductCode}: {data.Name}");
                return;
            }

            listProd[index] = data;
            int indexPath = listProdPathImage.FindIndex(e => e.Id == data.ProductCode); // lấy chỉ mục hình ảnh của sản phẩm trnog danh sách hình ảnh tạm
            // cập nhật lại thông tin về hình ảnh khi có thay đổi về hình ảnh trước kia
            listProdPathImage[indexPath] = new ImagePathList
            {
                Id = data.ProductCode,
                Path = isChangeImage ? pathImage : listProdPathImage[indexPath].Path,
                Image = data.Image
            };

            clearProdControl();
            loadList();
        }

        /// <summary>
        /// xóa dữ liệu trong danh sách
        /// </summary>
        private void deleteProdList()
        {
            var idProd = tbCodeProd.Text.Trim();

            if (!checkInputProdNotEmpty())
            {
                ShowMess.Error__InputEmpty();
                return;
            }

            if (string.IsNullOrEmpty(idProd))
            {
                ShowMess.Error__EmptyCode();
                return;
            }

            int index = listProd.FindIndex(e => e.ProductCode.Equals(idProd)); // lấy chỉ mục của sản phẩm trong danh sách sản phẩm tạm

            if (index >= 0)
            {
                listProd.RemoveAt(index);
                // xóa dữ liệu về thông tin hình ảnh đã sau khi xóa sản phẩm tnong danh sách sản phẩm tạm
                listProdPathImage.RemoveAt(listProdPathImage.FindIndex(e => e.Id == idProd)); // trong ngoặc là lấy index chứa thông tin hình ảnh tương ứng với sản phẩm đã xóa

                clearProdControl();
                loadList();

                return;
            }

            ShowMess.Error__CustomText($"Không có mã {idProd} trong danh sách");
        }

        /// <summary>
        /// thiết lập giá trị cho combobox
        /// </summary>
        private void getCombobox()
        {
            getComboboxCatalog();
            getComboboxSupplier();
        }

        /// <summary>
        /// thiết lập giá trị cho loại sản phẩm
        /// </summary>
        private void getComboboxCatalog()
        {
            cbCatalog.DataSource = new bCatalog().ListForCombobox();
            cbCatalog.DisplayMember = "Display";
            cbCatalog.ValueMember = "Value";
            cbCatalog.SelectedIndex = isAdd ? -1 : 0;
        }

        /// <summary>
        /// thiết lập giá trị cho nhà cung cấp
        /// </summary>
        private void getComboboxSupplier()
        {
            cbSupplier.DataSource = new bSupplier().ListForCombobox();
            cbSupplier.DisplayMember = "Display";
            cbSupplier.ValueMember = "Value";
            cbSupplier.SelectedIndex = isAdd ? -1 : 0;
        }

        /// <summary>
        /// mở hộp thoại thêm hình ảnh cho sản phẩm
        /// </summary>
        private void openDialog()
        {
            using (OpenFileDialog dlg = new OpenFileDialog())
            {
                dlg.Title = "Chọn hình ảnh";
                dlg.Filter = "Kiểu hình ảnh | *.jpg;*.jpeg;*.png;";
                dlg.Multiselect = false;

                DialogResult dr = dlg.ShowDialog();

                isChangeImage = dr == DialogResult.OK; // Đặt isChangeImage thành true nếu kết quả hộp thoại là OK

                if (isChangeImage)
                {
                    tbImage.Text = dlg.SafeFileName; // lưu tên hình ảnh đã chọn
                    pathImage = Path.GetDirectoryName(dlg.FileName); // lưu đường dẫn đến hình nhả gốc
                    loadImage();
                }
            }
        }

        /// <summary>
        /// Lưu dữ liệu của sản phẩm vào database
        /// </summary>
        private void saveProd()
        {
            var bak = new List<string>();
            var bakImageImport = new List<ImagePathList>();

            foreach (var i in listProd)
            {
                var data = new Product
                {
                    ProductCode = i.ProductCode,
                    Name = i.Name,
                    CatalogID = i.CatalogID,
                    SupplierID = i.SupplierID,
                    Image = i.Image,
                    Amount = i.Amount,
                    Price = i.Price,
                    Status = i.Status,
                    CreateDate = i.CreateDate
                };

                bool rs = new bProduct().Add(data);

                if (!rs)
                {
                    // rollback khi thêm dữ liệu bị lỗi
                    bak.ForEach(e => new bProduct().Delete(e));
                    return;
                }

                bak.Add(data.ProductCode); // lưu dữ liệu đã thêm vào database vào bộ nhớ tạm
            }

            foreach (var i in listProdPathImage)
            {
                try
                {
                    // thêm hình ảnh vào hệ thống, và lưu thông tin hình ảnh vào bộ nhớ tạm
                    Lib.ImageImport(i.Image.Trim(), i.Path);
                    bakImageImport.Add(i);
                }
                catch
                {
                    bakImageImport.ForEach(x => Lib.ImageDelete(x.Image.Trim())); // rollback khi thêm hình ảnh bị lỗi
                    return;
                }
            }
        }

        /// <summary>
        /// Lưu dữ liệu hóa đơn
        /// </summary>
        private void saveInvoice()
        {
            package();
            bImport b = new bImport();
            string mess = $"#{_invoice.ImportCode}";

            if (b.IsExists(_invoice.ImportCode) && isAdd)
            {
                ShowMess.Error__CustomText($"Đã tồn tại đơn nhập hàng #{_invoice.ImportCode}");
                return;
            }

            bool result = isAdd ? new bImport().Add(_invoice) : new bImport().Update(_invoice);

            if (!result)
            {
                ShowMess.Error__AddedOrUpdated(mess, isAdd);
                return;
            }

            saveProd();
            saveInvoiceDetail();
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

        /// <summary>
        /// Lưu dữ liệu hóa đơn chi tiết
        /// </summary>
        private void saveInvoiceDetail()
        {
            var listBak = new List<ImportDetail>();

            try
            {
                listInvoiceDetail.ForEach(i =>
                {
                    bool rs = new bImportDetail().Add(i);
                    if (rs) listBak.Add(i);
                });
            }
            catch
            {
                listBak.ForEach(i => new bImportDetail().Delete(i.ImportID, i.ProductID)); // rollback khi lưu dữ liệu không thành công
            }
        }

        /// <summary>
        /// Xóa hóa đơn
        /// </summary>
        private void deleteInvoice()
        {
            package();
            var listProdBak = new List<Product>();
            var listInvoiceBak = new List<ImportDetail>();
            bImport b = new bImport();
            string mess = $"#{_invoice.ImportCode}: {_invoice.ImportCode}";

            if (!b.IsExists(_invoice.ImportCode))
            {
                ShowMess.Error__NotExists(mess);
                return;
            }

            int countExists = listInvoiceDetail.Sum(e => new bInvoiceDetail().TotalAmount(e.ProductID));

            if (countExists > 0)
            {
                ShowMess.Error__CustomText($"Không thể xoá đơn nhập hàng do có {countExists} sản phẩm đã bán trong đơn nhập hàng này.");
                return;
            }

            foreach (var i in listInvoiceDetail)
            {
                bool rs = new bImportDetail().Delete(i.ImportID, i.ProductID);

                if (!rs)
                {
                    mess = $"{i.ImportID}: {i.ProductID}";
                    ShowMess.Error__Deleted(mess);
                    listInvoiceBak.ForEach(e => new bImportDetail().Add(e)); // rollback khi xóa không thành công
                    return;
                }

                listInvoiceBak.Add(i); // thêm dữ liệu xóa thành công vào bộ nhớ tạm
            }

            foreach (var i in listInvoiceBak)
            {
                var prod = new bProduct().Detail(i.ProductID);
                bool rs = new bProduct().Delete(i.ProductID);

                mess = $"{prod.ProductCode}: {prod.Name}";

                if (!rs)
                {
                    ShowMess.Error__Deleted(mess);
                    // rollback khi xóa không thành công
                    listProdBak.ForEach(e => new bProduct().Add(e));
                    listInvoiceBak.ForEach(e => new bImportDetail().Add(e));
                    return;
                }

                // lưu dữ liệu xóa thành công vào bộ nhớ tạm
                listProdBak.Add(prod);
            }

            bool result = new bImport().Delete(_invoice.ImportCode);

            if (!result)
            {
                ShowMess.Error__Deleted(mess);
                // rollback khi xóa không thành công
                listProdBak.ForEach(e => new bProduct().Add(e));
                listInvoiceBak.ForEach(e => new bImportDetail().Add(e));
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
            isSave = true;
            this.Close();
        }

        /// <summary>
        /// kiểm tra các đầu vào không có giá trị rống
        /// </summary>
        /// <returns></returns>
        private bool checkInputProdNotEmpty()
        {
            return Lib.ControlNotEmpty(pnProduct);
        }

        /// <summary>
        /// xóa trống các control có giá trị
        /// </summary>
        private void clearProdControl()
        {
            Lib.ClearText(pnProduct);
            Lib.ImageLoad__Null(picImage);
            cbCatalog.SelectedIndex = cbSupplier.SelectedIndex = -1;
        }

        /// <summary>
        /// tải hình anh lên GUI
        /// </summary>
        private void loadImage()
        {
            if (isAdd || isChangeImage)
            {
                // hiển thị hình ảnh khi hình ảnh không có trong hệ thống
                string tempPath = Path.Combine(pathImage, tbImage.Text.Trim());
                picImage.Image = File.Exists(tempPath) ? Image.FromFile(tempPath) : Properties.Resources.noImages;
            }
            else if (isViewOrEdit)
            {
                // hiển thị hình ảnh khi hình ảnh đã tồn tại trong hệ hế
                Lib.ImageLoad(tbImage.Text.Trim(), picImage);
            }
        }

        /// <summary>
        /// Xử lý dữ liệu khi nhận được mã
        /// </summary>
        /// <param name="code"> mã từ form thêm mới truyền vào </param>
        private void getSenderCatalog(string code)
        {
            getComboboxCatalog();
            cbCatalog.SelectedValue = code;
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
            string customerName = $"Tên Khách Hàng: {tbUsername.Text}";
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
            e.Graphics.DrawString(purchaseTime, infoFont, infoBrush, new Point(xRightColumn, yStart + 30));

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

        /// <summary>
        /// xử lý dữ liệu khi nhận được mã
        /// </summary>
        /// <param name="code"> mã từ form thêm mới truyền vào </param>
        private void getSenderSupplier(string code)
        {
            getComboboxSupplier();
            cbSupplier.SelectedValue = code;
        }
    }
}