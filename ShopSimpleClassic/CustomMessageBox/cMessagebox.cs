using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ShopSimpleClassic.CustomMessageBox
{
    public partial class cMessagebox : Form
    {
        #region Event for form

        // di chuyển nguyên Form tới bất kì vị trí nào trên màn hình
        [DllImport("user32.DLL", EntryPoint = "ReleaseCapture")]
        private static extern void ReleaseCapture();

        [DllImport("user32.DLL", EntryPoint = "SendMessage")]
        private static extern void SendMessage(System.IntPtr hWnd, int wMsg, int wParam, int lParam);

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

        #endregion Event for form

        private int distanceButton = 8;
        private const int CS_DROPSHADOW = 0x00020000;

        public cMessagebox(string text)
        {
            GUI(text);
            SetFormSize();
            centerContent();
            SetButtons(MessageBoxButtons.OK, MessageBoxDefaultButton.Button1);
            loadFrameForm();
        }

        public cMessagebox(string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        {
            GUI(text);
            SetFormSize();
            centerContent();
            SetButtons(buttons, MessageBoxDefaultButton.Button1);
            SetIcon(icon);
            loadFrameForm();
        }

        public cMessagebox(string text, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        {
            GUI(text);
            SetFormSize();
            centerContent();
            SetButtons(buttons, defaultButton);
            SetIcon(icon);
            loadFrameForm();
        }

        private void GUI(string text)
        {
            InitializeComponent();
            InitializeItems();
            this.lblContent.Text = text.Trim();
        }

        // Khởi tạo các button
        private void InitializeItems()
        {
            //this.bt__cancel.DialogResult = DialogResult.OK;
            this.btPrimary.Visible = false;
            this.btDanger.Visible = false;
            //this.bt__cancel.Visible = false;
        }

        // hiển thị các button tương ứng
        private void SetButtons(MessageBoxButtons buttons, MessageBoxDefaultButton defaultButton)
        {
            int xCenter = (this.pnButton.Width - btPrimary.Width) / 2;
            int yCenter = (this.pnButton.Height - btPrimary.Height) / 2;

            btPrimary.Visible = btDanger.Visible = false;
            //bt__cancel.Visible = false;

            switch (buttons)
            {
                case MessageBoxButtons.OK:
                    btPrimary.Visible = true;
                    btPrimary.Location = new Point(xCenter, yCenter);
                    btPrimary.Text = "Đóng";
                    btPrimary.Padding = new Padding(12, Padding.Top, 15, Padding.Bottom);
                    btPrimary.DialogResult = DialogResult.OK;
                    SetDefaultButton(defaultButton);
                    break;

                //case MessageBoxButtons.OKCancel:
                //    btPrimary.Visible = bt__cancel.Visible = true;
                //    btPrimary.Location = new Point(xCenter - (btPrimary.Width / 2) - distanceButton, yCenter);
                //    btPrimary.Text = "Được";
                //    btPrimary.DialogResult = DialogResult.OK;

                //    bt__cancel.Location = new Point(xCenter + (bt__cancel.Width / 2) + distanceButton, yCenter);
                //    bt__cancel.Text = "Hủy";
                //    bt__cancel.DialogResult = DialogResult.Cancel;

                //    if (defaultButton != MessageBoxDefaultButton.Button3)
                //        SetDefaultButton(defaultButton);
                //    else
                //        SetDefaultButton(MessageBoxDefaultButton.Button1);

                //    break;

                case MessageBoxButtons.YesNo:
                    btPrimary.Visible = btDanger.Visible = true;
                    btPrimary.Location = new Point(xCenter - (btPrimary.Width / 2) - distanceButton, yCenter);
                    btPrimary.Text = "Đồng ý";
                    btPrimary.DialogResult = DialogResult.Yes;

                    btDanger.Location = new Point(xCenter + (btDanger.Width / 2) + distanceButton, yCenter);
                    btDanger.Text = "Không";
                    btDanger.DialogResult = DialogResult.No;

                    if (defaultButton != MessageBoxDefaultButton.Button3)
                        SetDefaultButton(defaultButton);
                    else SetDefaultButton(MessageBoxDefaultButton.Button1);

                    break;

                    //case MessageBoxButtons.YesNoCancel:
                    //    btPrimary.Visible = btDanger.Visible = bt__cancel.Visible = true;
                    //    btPrimary.Location = new Point(xCenter - btPrimary.Width - distanceButton, yCenter);
                    //    btPrimary.Text = "Có";
                    //    btPrimary.DialogResult = DialogResult.Yes;

                    //    btDanger.Location = new Point(xCenter, yCenter);
                    //    btDanger.Text = "Không";
                    //    btDanger.DialogResult = DialogResult.No;

                    //    bt__cancel.Location = new Point(xCenter + btDanger.Width + distanceButton, yCenter);
                    //    bt__cancel.Text = "Hủy";
                    //    bt__cancel.DialogResult = DialogResult.Cancel;
                    //    SetDefaultButton(defaultButton);
                    //    break;
            }
        }

        private void SetDefaultButton(MessageBoxDefaultButton defaultButton)
        {
            Button button = null;
            switch (defaultButton)
            {
                case MessageBoxDefaultButton.Button1:
                    button = btPrimary;
                    break;

                case MessageBoxDefaultButton.Button2:
                    button = btDanger;
                    break;

                    //case MessageBoxDefaultButton.Button3:
                    //    button = bt__cancel;
                    //    break;
            }
            if (button != null)
            {
                button.Select();
                button.Font = new Font(button.Font, FontStyle.Underline);
            }
        }

        // hiển thị các icon tương ứng
        private void SetIcon(MessageBoxIcon icon)
        {
            string resourceName;
            switch (icon)
            {
                case MessageBoxIcon.Error:
                    resourceName = "error";
                    break;

                case MessageBoxIcon.Information:
                    resourceName = "success";
                    break;

                case MessageBoxIcon.Question:
                    resourceName = "question";
                    break;

                default:
                    resourceName = "error";
                    break;
            }

            this.picIcon.Image = (Bitmap)Properties.Resources.ResourceManager.GetObject(resourceName);
        }

        // cập nhật kích thước form thông báo, sử dụng cho trường hợp thông báo quá dài mà kích thước mặc định không hiển thị hết.
        private void SetFormSize()
        {
            int sizeLbly = this.lblContent.Size.Height;
            int sizepny = this.pnContent.Size.Height;
            int newSizeY = this.Size.Height + (sizeLbly - sizepny) + 30;

            if (sizeLbly > sizepny)
                this.Size = new Size(this.Size.Width, newSizeY);
        }

        private void centerContent()
        => this.lblContent.Location = new Point
        (
            (this.pnContent.Width - this.lblContent.Width) / 2,
            (this.pnContent.Height - this.lblContent.Height) / 2
        );

        // show drop shadow
        protected override CreateParams CreateParams
        {
            get
            {
                // add the drop shadow flag for automatically drawing
                // a drop shadow around the form
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
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

        private void pnTitle_MouseDown(object sender, MouseEventArgs e)
        {
            ReleaseCapture();
            SendMessage(this.Handle, 0x112, 0xf012, 0);
        }
    }
}