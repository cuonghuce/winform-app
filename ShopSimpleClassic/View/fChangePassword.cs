using ShopSimpleClassic.Controller;
using ShopSimpleClassic.Library;
using System;
using System.Windows.Forms;

namespace ShopSimpleClassic.View
{
    public partial class fChangePassword : Form
    {
        /// Variable
        private string passCurrent, passNew, passConfirm;

        private bool isSave = false;

        /// Main
        public fChangePassword()
        {
            InitializeComponent();
        }

        private void fChangePassword_FormClosing(object sender, FormClosingEventArgs e)
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

        private void chbShowPass_CheckedChanged(object sender, EventArgs e)
        {
            tbCurrent.UseSystemPasswordChar = tbConfirm.UseSystemPasswordChar = tbNew.UseSystemPasswordChar = !chbShowPass.Checked;
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

        /// function
        /// <summary>
        /// Lưu dữ liệu vào database
        /// </summary>
        private void save()
        {
            if (!checkInput()) return; // kiểm tra dữ liệu đầu vào

            bool result = Temp.IsAdmin ? new bAdmin().UpdatePassword(Temp.Username, passConfirm) : new bUser().UpdatePassword(Temp.Username, passConfirm);

            string mess = $"{Temp.Username} - {(Temp.IsAdmin ? new bAdmin().Detail(Temp.Username).Name : new bUser().Detail(Temp.Username).Name)}";

            if (!result)
            {
                // hiển thị thông báo lỗi khi lưu không thành công
                ShowMess.Error__AddedOrUpdated(mess, false);
                return;
            }

            ShowMess.Success__AddedOrUpdated(mess, false);
            this.isSave = true;
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        /// <summary>
        /// kiểm tra đầu vào của dữ liệu
        /// </summary>
        /// <param name="mess"></param>
        /// <returns></returns>
        private bool checkInput()
        {
            package();

            if (!Lib.CheckInputNotEmpty(pnContent, errorProvider1))
            {
                return false;
            }

            if (!new bAdmin().IsExists(Temp.Username) && !new bUser().IsExists(Temp.Username))
            {
                ShowMess.Error__CustomText("Hiện tại thì tài khoản này không còn tồn tại trong CSDL!");
                return false;
            }

            // kiểm tra tài khoản
            if (Temp.IsAdmin ? !new bAdmin().IsExists(Temp.Username, passCurrent) : !new bUser().IsExists(Temp.Username, passCurrent))
            {
                ShowMess.Error__PasswordIsNotCorrect();
                return false;
            }

            if (!passNew.Equals(passConfirm))
            {
                ShowMess.Error__PasswordNotEquals();
                return false;
            }

            return true;
        }

        /// <summary>
        /// đóng gói dữ liệu
        /// </summary>
        private void package()
        {
            passCurrent = tbCurrent.Text.Trim();
            passNew = tbNew.Text.Trim();
            passConfirm = tbConfirm.Text.Trim();
        }
    }
}