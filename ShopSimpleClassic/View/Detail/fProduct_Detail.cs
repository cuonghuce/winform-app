using ShopSimpleClassic.Controller;
using ShopSimpleClassic.Library;
using System;
using System.Windows.Forms;

namespace ShopSimpleClassic.View.Detail
{
    public partial class fProduct_Detail : Form
    {
        /// Main
        public fProduct_Detail(string code)
        {
            InitializeComponent();
            load(code);
            btExit.Select();
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// Function
        private void load(string code)
        {
            var _data = new bProduct().Detail(code);

            // tải hình ảnh
            Lib.ImageLoad(_data.Image, picImage);
            tbImage.Text = _data.Image;

            // tải thông tin
            tbCode.Text = _data.ProductCode;
            tbName.Text = _data.Name;
            tbCatalog.Text = new bCatalog().Detail(_data.CatalogID).Name;
            tbSupplier.Text = new bSupplier().Detail(_data.SupplierID).Name;
            tbAmount.Text = _data.Amount.ToString();
            tbPrice.Text = Lib.ConvertPrice(_data.Price.ToString(), true);
            tbDate.Text = Lib.ConvertDateToString(_data.CreateDate);
            tbStatus.Text = _data.Status ? "Còn hàng" : "Ngừng kinh doanh";

            this.Text = $"{this.Tag} {_data.ProductCode} - {_data.Name}";
        }
    }
}