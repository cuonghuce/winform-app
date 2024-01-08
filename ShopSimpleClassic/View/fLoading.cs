using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ShopSimpleClassic.View
{
    public partial class fLoading : Form
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

        private const int CS_DROPSHADOW = 0x00020000;
        public Action Worker { get; set; }
        private bool isAutoCLose = false;

        public fLoading(Action worker)
        {
            InitializeComponent();
            loadFrameForm();
            isAutoCLose = true;
            if (worker == null)
                throw new ArgumentNullException();
            this.Worker = worker;
        }

        public fLoading()
        {
            InitializeComponent();
            loadFrameForm();
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (isAutoCLose)
                Task.Factory.StartNew(Worker).ContinueWith(t => { this.Dispose(); }, TaskScheduler.FromCurrentSynchronizationContext());
        }

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
                this.Region = System.Drawing.Region.FromHrgn(CreateRoundRectRgn(0, 0, this.Width, this.Height, 16, 16));
            }
        }
    }
}