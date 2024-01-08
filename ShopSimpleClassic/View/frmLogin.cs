using ShopSimpleClassic.Controller;
using ShopSimpleClassic.Library;
using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ShopSimpleClassic.View
{
    public partial class frmLogin : Form
    {
        // Variable
        // di chuyển nguyên Form tới bất kì vị trí nào trên màn hình
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

        private string username, password;
        private bool isExit = false;

        #region Round Form

        #region Win 10

        [DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(int nLeftRect, int nTopRect, int nRightRect, int nBottomRect, int nWidthEllipse, int nHeightEllipse);

        #endregion Win 10

        #region Win 11

        // The enum flag for DwmSetWindowAttribute's second parameter, which tells the function what attribute to set.
        // Copied from dwmapi.h
        public enum DWMWINDOWATTRIBUTE
        { DWMWA_WINDOW_CORNER_PREFERENCE = 33 }

        // The DWM_WINDOW_CORNER_PREFERENCE enum for DwmSetWindowAttribute's third parameter, which tells the function
        // what value of the enum to set.
        // Copied from dwmapi.h
        public enum DWM_WINDOW_CORNER_PREFERENCE
        { DWMWCP_DEFAULT = 0, DWMWCP_DONOTROUND = 1, DWMWCP_ROUND = 2, DWMWCP_ROUNDSMALL = 3 }

        // Import dwmapi.dll and define DwmSetWindowAttribute in C# corresponding to the native function.
        [DllImport("dwmapi.dll", CharSet = CharSet.Unicode, PreserveSig = false)]
        internal static extern void DwmSetWindowAttribute(IntPtr hwnd, DWMWINDOWATTRIBUTE attribute, ref DWM_WINDOW_CORNER_PREFERENCE pvAttribute, uint cbAttribute);

        #endregion Win 11

        #endregion Round Form

        // Main
        public frmLogin()
        {
            InitializeComponent();
            loadFrameForm();
        }

        private void frmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (isExit)
            {
                Application.Exit();
                return;
            }

            eForm.ApplicationExit(e);
        }

        private void pnRight_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }

        private void chbShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            tbPassword.UseSystemPasswordChar = !chbShowPassword.Checked;
        }

        private void btLogin_Click(object sender, EventArgs e)
        {
            try
            {
                login();
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

        // Function
        private void login()
        {
            if (!checkInputNotEmpty()) return;

            package();

            Temp.Username = username;

            if (!new bUser().IsExists(username, password) && !new bAdmin().IsExists(username, password))
            {
                ShowMess.Error__UsernameOrPassword();
                return;
            }

            if (new bAdmin().IsExists(username, password))
            {
                openMainForm(true);
                return;
            }

            openMainForm(false);
        }

        /// <summary>
        /// mở form dashboard
        /// </summary>
        /// <param name="isAdmin"></param>
        private void openMainForm(bool isAdmin)
        {
            Temp.IsAdmin = isAdmin;
            this.Hide();
            var frm = new fDashboard();
            frm.ShowDialog();

            if (frm.DialogResult == DialogResult.OK)
            {
                this.Show();
            }
            else
            {
                isExit = true;
                this.Close();
            }
        }

        /// <summary>
        /// Đóng gói dữ liệu
        /// </summary>
        private void package()
        {
            username = tbUsername.Text.Trim();
            password = tbPassword.Text.Trim();
        }

        /// <summary>
        /// kiểm tra dữ dữ liệu đầu vào
        /// </summary>
        /// <returns></returns>
        private bool checkInputNotEmpty()
        {
            return Lib.CheckInputNotEmpty(pnRight, errorMain);
        }

        private void loadFrameForm()
        {
            try
            {
                // Rounded corners for the form when the user runs the application on windows 11
                var attribute = DWMWINDOWATTRIBUTE.DWMWA_WINDOW_CORNER_PREFERENCE;
                var preference = DWM_WINDOW_CORNER_PREFERENCE.DWMWCP_ROUND;
                DwmSetWindowAttribute(this.Handle, attribute, ref preference, sizeof(uint));
            }
            catch
            {
                // Rounded corners for the form when the user runs the application on windows 10
                this.Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 16, 16));
            }
        }
    }
}