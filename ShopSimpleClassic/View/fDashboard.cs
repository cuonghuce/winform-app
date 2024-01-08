using ShopSimpleClassic.Controller;
using ShopSimpleClassic.Library;
using ShopSimpleClassic.Model;
using ShopSimpleClassic.View.Detail;
using ShopSimpleClassic.View.Manager;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace ShopSimpleClassic.View
{
    public partial class fDashboard : Form
    {
        // Variable
        private bool isLogout = false;

        // Main
        public fDashboard()
        {
            InitializeComponent();
            load();
        }

        private void btDashboad_Click(object sender, EventArgs e)
        {
            tabDashboard.SelectedTab = tabDashboard.TabPages["tabPage_Dashboard"];
        }

        private void btCategory_Click(object sender, EventArgs e)
        {
            tabDashboard.SelectedTab = tabDashboard.TabPages["tabPage_Category"];
        }

        private void btBill_Click(object sender, EventArgs e)
        {
            tabDashboard.SelectedTab = tabDashboard.TabPages["tabPage_Bill"];
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            isLogout = false;
            this.Close();
        }

        private void btProfile_Click(object sender, EventArgs e)
        {
            try
            {
                DialogResult dr;
                if (Temp.IsAdmin)
                {
                    var frm = new fProfile_Admin(true);
                    dr = frm.ShowDialog();
                }
                else
                {
                    var frm = new fProfile_Employee(true);
                    dr = frm.ShowDialog();
                }

                if (dr == DialogResult.Yes)
                {
                    lblName.Text = Temp.IsAdmin ? new bAdmin().Detail(Temp.Username).Name : new bUser().Detail(Temp.Username).Name;
                }

                Lib.CenterControl_Horizontal(pnProfile, lblName);
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btChangePass_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new fChangePassword();
                frm.ShowDialog();

                if (frm.DialogResult == DialogResult.Yes)
                {
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btLogout_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            isLogout = true;
            this.Close();
        }

        private void btMore_Catalog_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new fCatalog();
                eForm.ShowWaitForm();
                frm.Show();

                if (frm.DialogResult == DialogResult.OK)
                {
                    getCategory();
                }
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btMore_Supplier_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new fSupplier();
                eForm.ShowWaitForm();
                frm.Show();
                if (frm.DialogResult == DialogResult.OK)
                {
                    getCategory();
                }
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btMore_Customer_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new fCustomer();
                eForm.ShowWaitForm();
                frm.Show();
                if (frm.DialogResult == DialogResult.OK)
                {
                    getCategory();
                }
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btMore_Product_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new fProduct();
                eForm.ShowWaitForm();
                frm.Show();

                if (frm.DialogResult == DialogResult.OK)
                {
                    getCategory();
                }
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btMore_Employee_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new fEmployee();
                eForm.ShowWaitForm();
                frm.Show();
                if (frm.DialogResult == DialogResult.OK)
                {
                    getCategory();
                }
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btMore_Admin_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new fAdmin();
                eForm.ShowWaitForm();
                frm.Show();
                if (frm.DialogResult == DialogResult.OK)
                {
                    getCategory();
                }
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btMore_Invoice_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new fInvoice();
                eForm.ShowWaitForm();
                frm.Show();
                if (frm.DialogResult == DialogResult.OK)
                {
                    getBill();
                    list_BestSeller();
                    list_LastOrder();
                }
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btMore_Import_Click(object sender, EventArgs e)
        {
            try
            {
                var frm = new fImport();
                eForm.ShowWaitForm();
                frm.Show();
                if (frm.DialogResult == DialogResult.Yes)
                    getBill();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void fDashboard_FormClosing(object sender, FormClosingEventArgs e)
        {
            try
            {
                exit(e);
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void cbSize_LastOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_LastOrder();
        }

        private void cbFilter_LastOrder_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_LastOrder();
        }

        private void cbSize_BestSeller_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_BestSeller();
        }

        private void cbFilter_BestSeller_SelectedIndexChanged(object sender, EventArgs e)
        {
            list_BestSeller();
        }

        private void cbFilter_Bill_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter_QuantityOrTotal.SelectedIndex == 0)
            {
                showReportInvoice_Quantity();
            }
            else
            {
                showReportInvoice_Total();
            }
        }

        private void cbFilter_QuantityOrTotal_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilter_QuantityOrTotal.SelectedIndex == 0)
            {
                showReportInvoice_Quantity();
            }
            else
            {
                showReportInvoice_Total();
            }
        }

        private void chart1_FormatNumber(object sender, FormatNumberEventArgs e)
        {
            if (cbFilter_QuantityOrTotal.SelectedIndex == 1)
            {
                if (e.ElementType == System.Windows.Forms.DataVisualization.Charting.ChartElementType.AxisLabels)
                {
                    e.LocalizedValue = FormatNumber(e.Value.ToString());
                }
            }
        }

        private void btAddBill_Click(object sender, EventArgs e)
        {
            using (var waitForm = new fLoading()) // Sử dụng biến cục bộ để tạo form chờ
            {
                waitForm.TopMost = true;
                waitForm.StartPosition = FormStartPosition.CenterScreen;
                waitForm.Show();
                waitForm.Refresh();
                System.Threading.Thread.Sleep(200);

                waitForm.Close(); // Đóng form chờ

                if (Temp.IsAdmin)
                {
                    var frm = new fImport_Add(true);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.Yes)
                    {
                        getBill_Import();
                        getCategory();
                    }
                }
                else
                {
                    var frm = new fInvoice_Add(true);
                    frm.ShowDialog();
                    if (frm.DialogResult == DialogResult.Yes)
                    {
                        getBill_Invoice();
                        getCategory();
                        cbFilter_QuantityOrTotal.SelectedIndex = 0;
                        cbFilter_Bill.SelectedIndex = 0;
                    }
                }
            }
        }

        // Function

        private void load()
        {
            lblDateNow.Text = DateTime.Now.ToShortDateString();
            cbFilter_Bill.SelectedIndex = 0;
            cbFilter_QuantityOrTotal.SelectedIndex = 0;
            getInforUsername();
            getLastOrder();
            getBestSeller();
            getCategory();
            getBill();
            role();
        }

        private void role()
        {
            pnAdmin.Visible = pnImport.Visible = Temp.IsAdmin;
        }

        private void showReportInvoice_Quantity()
        {
            IEnumerable<DTO_Chart> chartData = new bInvoice().List(cbFilter_Bill.SelectedIndex); ;

            chart1.Series.Clear();
            chart1.Series.Add("NumberOfInvoices");
            chart1.Series["NumberOfInvoices"].XValueMember = "Date";
            chart1.Series["NumberOfInvoices"].YValueMembers = "NumberOfInvoices";
            chart1.Series["NumberOfInvoices"].ChartType = SeriesChartType.Column;
            chart1.Series["NumberOfInvoices"].LegendText = "Số Hoá Đơn";

            // Đặt nhãn cho các giá trị trên trục y (nhãn của các cột)
            chart1.Series["NumberOfInvoices"].IsValueShownAsLabel = true;
            chart1.Series["NumberOfInvoices"].LabelFormat = "({#})";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "{#}";
            chart1.DataSource = chartData;
            chart1.DataBind();
        }

        private void showReportInvoice_Total()
        {
            IEnumerable<DTO_Chart> chartData = new bInvoice().List(cbFilter_Bill.SelectedIndex); ;

            chart1.Series.Clear();
            chart1.Series.Add("Total");
            chart1.Series["Total"].XValueMember = "Date";
            chart1.Series["Total"].YValueMembers = "Total";
            chart1.Series["Total"].ChartType = SeriesChartType.Column;
            chart1.Series["Total"].LegendText = "Tổng Tiền";

            // Đặt nhãn cho các giá trị trên trục y (nhãn của các cột)
            chart1.Series["Total"].IsValueShownAsLabel = true;
            chart1.Series["Total"].LabelFormat = "{#}";
            chart1.ChartAreas[0].AxisY.LabelStyle.Format = "MyAxisYCustomFormat";

            // hiển thị dữ liệu
            chart1.DataSource = chartData;
            chart1.DataBind();

            foreach (var point in chart1.Series["Total"].Points)
            {
                double totalValue = Convert.ToDouble(point.YValues[0]);
                point.Label = FormatNumber(totalValue.ToString());
            }
        }

        private string FormatNumber(string number)
        {
            var numb = double.Parse(number);
            if (numb >= 1000000000)
                return (numb / 1000000000).ToString("0.###") + " tỷ";
            else if (numb >= 1000000)
                return (numb / 1000000).ToString("0.###") + "tr";
            else if (numb >= 1000)
                return (numb / 1000).ToString("0.###") + "k";
            else
                return numb.ToString("#,0");
        }

        /// <summary>
        /// lấy thông tin người đăng nhập
        /// </summary>
        private void getInforUsername()
        {
            lblUsername.Text = Temp.Username;
            lblName.Text = Temp.IsAdmin ? new bAdmin().Detail(Temp.Username).Name : new bUser().Detail(Temp.Username).Name;

            // align center
            Lib.CenterControl_Horizontal(pnProfile, lblUsername);
            Lib.CenterControl_Horizontal(pnProfile, lblName);
        }

        /// <summary>
        /// hiển thị hoá đơn cuối cùng
        /// </summary>
        private void getLastOrder()
        {
            cbSize_LastOrder.SelectedIndex = 0;
            cbFilter_LastOrder.SelectedIndex = 0;
        }

        private void list_LastOrder()
        {
            int pageSize = Convert.ToInt32(cbSize_LastOrder.SelectedItem?.ToString());
            int filter = cbFilter_LastOrder.SelectedIndex;
            // Lấy danh sách Catalog từ bộ xử lý bCatalog dựa trên từ khóa tìm kiếm, trang hiện tại và kích thước trang
            IEnumerable<Invoice> data = new bInvoice().List(pageSize, filter);

            dgvList_LastOrder.Rows.Clear();

            // Thêm dữ liệu Catalog vào DataGridView
            dgvList_LastOrder.Rows.AddRange(data.Select((i, count) => new DataGridViewRow
            {
                Cells =
                {
                    new DataGridViewTextBoxCell { Value = count + 1 }, // Ô hiển thị chỉ số bắt đầu từ
                    new DataGridViewTextBoxCell { Value = i.InvoiceCode }, // Ô hiển thị mã Catalog
                    new DataGridViewTextBoxCell { Value = new bCustomer().Detail(i.CustomerPhone).Name }, // Ô hiển thị mã Catalog
                    new DataGridViewTextBoxCell { Value = new bInvoiceDetail().TotalQuantity(i.InvoiceCode) }, // Ô hiển thị mã Catalog
                    new DataGridViewTextBoxCell { Value = Lib.ConvertPrice(i.Total.ToString(), true) } // Ô hiển thị tên Catalog
                }
            }).ToArray());
        }

        private void getBestSeller()
        {
            cbSize_BestSeller.SelectedIndex = 0;
            cbFilter_BestSeller.SelectedIndex = 0;
        }

        private void list_BestSeller()
        {
            int pageSize = Convert.ToInt32(cbSize_BestSeller.SelectedItem?.ToString());
            int filter = cbFilter_BestSeller.SelectedIndex;
            // Lấy danh sách Catalog từ bộ xử lý bCatalog dựa trên từ khóa tìm kiếm, trang hiện tại và kích thước trang
            IEnumerable<BestSeller> data = new bInvoiceDetail().List_BestSeller(pageSize, filter);

            dgvList_BestSeller.Rows.Clear();

            // Thêm dữ liệu Catalog vào DataGridView
            dgvList_BestSeller.Rows.AddRange(data.Select((i, count) => new DataGridViewRow
            {
                Cells =
                {
                    new DataGridViewTextBoxCell { Value = count + 1 },
                    new DataGridViewTextBoxCell { Value = i.NameProduct },
                    new DataGridViewTextBoxCell { Value = new bProduct().Detail(i.IdProduct).Amount },
                    new DataGridViewTextBoxCell { Value = i.Quantity }
                }
            }).ToArray());
        }

        private void getCategory()
        {
            getCategory_Catalog();
            getCategory_Supplier();
            getCategory_Customer();
            getCategory_Employee();
            getCategory_Admin();
            getCategory_Product();
        }

        private void getCategory_Catalog()
        {
            int total = new bCatalog().TotalRows();
            int used = new bCatalog().TotalUsed();
            lblTotal_Catalog.Text = $"{lblTotal_Catalog.Tag} {total}";
            lblUsed_Catalog.Text = $"{lblUsed_Catalog.Tag} {used}/{total}";
        }

        private void getCategory_Supplier()
        {
            int total = new bSupplier().TotalRows();
            int used = new bSupplier().TotalUsed();
            lblTotal_Supplier.Text = $"{lblTotal_Supplier.Tag} {total}";
            lblUsed_Supplier.Text = $"{lblUsed_Supplier.Tag} {used}/{total}";
        }

        private void getCategory_Customer()
        {
            int total = new bCustomer().TotalRows();
            int used = new bCustomer().TotalUsed();
            lblTotal_Customer.Text = $"{lblTotal_Customer.Tag} {total}";
            lblUsed_Customer.Text = $"{lblUsed_Customer.Tag} {used}/{total}";
        }

        private void getCategory_Employee()
        {
            lblTotal_Employee.Text = $"{lblTotal_Employee.Tag} {new bUser().TotalRows()}";
        }

        private void getCategory_Admin()
        {
            lblTotal_Admin.Text = $"{lblTotal_Admin.Tag} {new bAdmin().TotalRows()}";
        }

        private void getCategory_Product()
        {
            int total = new bProduct().TotalRows();
            int used = new bProduct().TotalUsed();
            lblTotal_Product.Text = $"{lblTotal_Product.Tag} {total}";
            lblSelled_Product.Text = $"{lblSelled_Product.Tag} {used}/{total}";
            lblStock_Product.Text = $"{lblStock_Product.Tag} {new bProduct().TotalStock()}/{total}";
            lblOOT_Product.Text = $"{lblOOT_Product.Tag} {new bProduct().TotalOutOfStock()}/{total}";
        }

        private void getBill()
        {
            getBill_Import();
            getBill_Invoice();
        }

        private void getBill_Import()
        {
            lblImport_TotalToday.Text = $"{lblImport_TotalToday.Tag} {new bImport().TotalBillToday()}";
            lblImport_TotalMonth.Text = $"{lblImport_TotalMonth.Tag} {new bImport().TotalBillMonth()}";
            lblImport_CountProdToday.Text = $"{lblImport_CountProdToday.Tag} {new bImportDetail().TotalAmountProductToday()}";
            lblImport_CountProdMonth.Text = $"{lblImport_CountProdMonth.Tag} {new bImportDetail().TotalAmountProductMonth()}";
            lblImport_QuantityProdToday.Text = $"{lblImport_QuantityProdToday.Tag} {new bImportDetail().TotalQuantityProductToday()}";
            lblImport_QuantityProdMonth.Text = $"{lblImport_QuantityProdMonth.Tag} {new bImportDetail().TotalQuantityProductMonth()}";
        }

        private void getBill_Invoice()
        {
            lblInvoice_TotalToday.Text = $"{lblInvoice_TotalToday.Tag} {new bInvoice().TotalBillToday()}";
            lblInvoice_TotalMonth.Text = $"{lblInvoice_TotalMonth.Tag} {new bInvoice().TotalBillMonth()}";
            lblInvoice_CountProdToday.Text = $"{lblInvoice_CountProdToday.Tag} {new bInvoiceDetail().TotalAmountProductToday()}";
            lblInvoice_CountProdMonth.Text = $"{lblInvoice_CountProdMonth.Tag} {new bInvoiceDetail().TotalAmountProductMonth()}";
            lblInvoice_QuantityProdToday.Text = $"{lblInvoice_QuantityProdToday.Tag} {new bInvoiceDetail().TotalQuantityProductToday()}";
            lblInvoice_QuantityProdMonth.Text = $"{lblInvoice_QuantityProdMonth.Tag} {new bInvoiceDetail().TotalQuantityProductMonth()}";
        }

        /// <summary>
        /// exit
        /// </summary>
        /// <param name="e"></param>
        private void exit(FormClosingEventArgs e)
        {
            // kiểm tra trạng thái đang xuất khỏi form
            if (isLogout)
            {
                if (ShowMess.Question__CustomText("Bạn có muốn đăng xuất không?") == DialogResult.Yes)
                {
                    Temp.Username = null;
                    Temp.IsAdmin = false;
                    e.Cancel = false;
                }
                else
                {
                    isLogout = false;
                    e.Cancel = true;
                }

                return;
            }

            // nếu không phải là đăng xuất form thì thực hiện đống chương trình
            if (ShowMess.Question__CustomText("Bạn có muốn thoát ứng dụng?") == DialogResult.Yes)
            {
                this.DialogResult = DialogResult.Yes;
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}