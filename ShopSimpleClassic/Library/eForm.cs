using ShopSimpleClassic.View;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace ShopSimpleClassic.Library
{
    public class eForm
    {
        private static fLoading _waitForm;

        public static void ShowWaitForm(Form mainForm)
        {
            // don't display more than one wait form at a time
            if (_waitForm != null && !_waitForm.IsDisposed)
            {
                return;
            }
            _waitForm = new fLoading();
            _waitForm.TopMost = true;
            _waitForm.StartPosition = FormStartPosition.CenterScreen;
            _waitForm.Show();
            _waitForm.Refresh();

            // force the wait window to display for at least 700ms so it doesn't just flash on the screen
            System.Threading.Thread.Sleep(700);
            //Application.Idle += OnLoaded;
            mainForm.BeginInvoke(new Action(() => {
                mainForm.Enabled = false; // Vô hiệu hóa form chính
            }));
            Application.Idle += (sender, e) => OnLoaded(mainForm);
        }
        public static void ShowWaitForm()
        {
            // don't display more than one wait form at a time
            if (_waitForm != null && !_waitForm.IsDisposed)
            {
                return;
            }
            _waitForm = new fLoading();
            _waitForm.TopMost = true;
            _waitForm.StartPosition = FormStartPosition.CenterScreen;
            _waitForm.Show();
            _waitForm.Refresh();

            // force the wait window to display for at least 700ms so it doesn't just flash on the screen
            System.Threading.Thread.Sleep(700);
            Application.Idle += OnLoaded;
        }

        private static void OnLoaded(object sender, EventArgs e)
        {
            Application.Idle -= OnLoaded;
            _waitForm.Dispose();
        }

        private static void OnLoaded(Form mainForm)
        {
            Application.Idle -= OnLoaded;
            mainForm.BeginInvoke(new Action(() => {
                mainForm.Enabled = true; // Kích hoạt lại form chính
            }));
            _waitForm.Close();
        }

        //  phóng to hoặc thu nhỏ cửa sổ
        public static void FormRestore(Form frm, Size frmSize)
        {
            if (frm.WindowState == FormWindowState.Normal)
            {
                frm.WindowState = FormWindowState.Maximized;
            }
            else
            {
                frm.WindowState = FormWindowState.Normal;
                frm.Size = frmSize;
            }
        }

        // ẩn form vào thanh taskbar
        public static void FormMinimize(Form frm)
        {
            frm.WindowState = FormWindowState.Minimized;
        }

        // thoát chương trình
        public static void ApplicationExit(FormClosingEventArgs e)
        {
            if (ShowMess.Question__ExitApplication() == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }

        public static void FormExit(Form frm, FormClosingEventArgs e)
        {
            if (ShowMess.Question__ExitForm(frm.Text) == DialogResult.Yes)
            {
                e.Cancel = false;
            }
            else
            {
                e.Cancel = true;
            }
        }
    }
}