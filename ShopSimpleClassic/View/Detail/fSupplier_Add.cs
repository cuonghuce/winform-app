using ShopSimpleClassic.Controller;
using ShopSimpleClassic.Library;
using ShopSimpleClassic.Model;
using System;
using System.Windows.Forms;

namespace ShopSimpleClassic.View.Detail
{
    public partial class fSupplier_Add : Form
    {
        // Variable

        private SendSupplierData send;
        private Supplier _data;
        private bool isSave = false;

        // Main

        public fSupplier_Add(SendSupplierData sender)
        {
            InitializeComponent();
            this.send = sender;
        }

        private void btAdd_Click(object sender, EventArgs e)
        {
            try
            {
                save();
            }
            catch (Exception ex)
            {
                ShowMess.Exception(ex);
            }
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.isSave = false;
            this.Close();
        }

        private void fSupplier_Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            // thoát không cần hiển thị thông báo xác nhận khi không nhập giá trị nào và sau khi lưu dữ liệu thành công
            if ((!Lib.ControlNotEmpty(pnContent) && !isSave) || isSave)
            {
                e.Cancel = false;
                return;
            }

            // hiển thị thống báo xác nhận thoát
            if (ShowMess.Question__ExitApplication() == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        // function

        /// <summary>
        /// Lưu dữ liệu vào database
        /// </summary>
        private void save()
        {
            package();
            string mess = $"{_data.Name}: {_data.Phone}";

            if (!checkInput(mess)) return;

            bool result = new bSupplier().Add(_data);

            if (!result)
            {
                // hiển thị thông báo khi lưu dữ liệu không thành công
                ShowMess.Error__AddedOrUpdated(mess, true);
                return;
            }

            ShowMess.Success__AddedOrUpdated(mess, true);
            this.isSave = true;
            this.send(_data.SupplierCode);
            this.Close();
        }

        /// <summary>
        /// Kiểm tra dữ liệu đầu vào
        /// </summary>
        /// <param name="mess"> chuỗi cần thông báo (thường là tên và mã) </param>
        /// <returns></returns>
        private bool checkInput(string mess)
        {
            if (!Lib.CheckInputNotEmpty(pnContent, errorProvider1))
            {
                return false;
            }

            if (new bSupplier().IsExists(_data.SupplierCode))
            {
                ShowMess.Error__AlreadyExist(mess);
                return false;
            }

            return true;
        }

        /// <summary>
        /// đóng gói dữ liệu
        /// </summary>
        private void package()
        {
            _data = new Supplier
            {
                SupplierCode = tbCode.Text.Trim(),
                Name = tbName.Text.Trim(),
                Email = tbEmail.Text.Trim(),
                Phone = tbPhone.Text.Trim()
            };
        }
    }
}