using System.Windows.Forms;

namespace ShopSimpleClassic.CustomMessageBox
{
    public class MessBox
    {
        // hiển thị thông báo mặc định
        public static DialogResult Show(string text)
        => new cMessagebox(text).ShowDialog();

        // hiển thị thông báo dưới dạng đầy đủ
        public static DialogResult Show(string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        => new cMessagebox(text, buttons, icon).ShowDialog();

        public static DialogResult Show(string text, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        => new cMessagebox(text, buttons, icon, defaultButton).ShowDialog();

        /*-> IWin32Window Owner:
        *      Displays a message box in front of the specified object and with the other specified parameters.
        *      An implementation of IWin32Window that will own the modal dialog box.*/

        public static DialogResult Show(IWin32Window owner, string text)
        => new cMessagebox(text).ShowDialog(owner);

        public static DialogResult Show(IWin32Window owner, string text, MessageBoxButtons buttons, MessageBoxIcon icon)
        => new cMessagebox(text, buttons, icon).ShowDialog(owner);

        public static DialogResult Show(IWin32Window owner, string text, MessageBoxButtons buttons, MessageBoxIcon icon, MessageBoxDefaultButton defaultButton)
        => new cMessagebox(text, buttons, icon, defaultButton).ShowDialog(owner);
    }
}