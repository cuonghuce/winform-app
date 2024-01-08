using ShopSimpleClassic.Controller;
using ShopSimpleClassic.Library;
using ShopSimpleClassic.Model;
using System;
using System.Windows.Forms;

namespace ShopSimpleClassic.View.Detail
{
    public partial class fCustomer_Add : Form
    {
        // Variable
        private SendNewCustomer send;

        private Customer _data;
        private bool isSave = false;

        // Main
        public fCustomer_Add(SendNewCustomer sender)
        {
            InitializeComponent();
            this.send = sender;
        }

        private void btExit_Click(object sender, EventArgs e)
        {
            this.isSave = false;
            this.Close();
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

        private void fCustomer_Add_FormClosing(object sender, FormClosingEventArgs e)
        {
            // thoát không cần hiển thị thông báo khi không nhập dữ lieuj và sau khi đã lưu dữ liệu thành công
            if ((!Lib.ControlNotEmpty(pnContent) && !isSave) || isSave)
            {
                e.Cancel = false;
                return;
            }

            // hiển thị thông báo xác nhận thoát
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

            string mess = $"{_data.Phone}: {_data.Name}";

            if (!checkInput(mess)) return; // kiểm tra dữ liệu đầu vào

            bool result = new bCustomer().Add(_data);

            if (!result)
            {
                // hiển thị thông báo lỗi khi lưu không thành công
                ShowMess.Error__AddedOrUpdated(mess, true);
                return;
            }

            ShowMess.Success__AddedOrUpdated(mess, true);
            this.isSave = true;
            this.send(_data.Phone);
            this.Close();
        }

        /// <summary>
        /// kiểm tra đầu vào của dữ liệu
        /// </summary>
        /// <param name="mess"></param>
        /// <returns></returns>
        private bool checkInput(string mess)
        {
            if (!Lib.CheckInputNotEmpty(pnContent, errorProvider1))
            {
                return false;
            }

            if (new bCustomer().IsExists(_data.Phone))
            {
                // thông báo khi mã tồn tại trong database trước đó
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
            _data = new Customer
            {
                Phone = tbCode.Text.Trim(),
                Name = tbName.Text.Trim()
            };
        }
    }
}