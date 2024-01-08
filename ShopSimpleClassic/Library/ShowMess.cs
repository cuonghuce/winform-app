using ShopSimpleClassic.CustomMessageBox;
using System;
using System.Windows.Forms;

namespace ShopSimpleClassic.Library
{
    public class ShowMess
    {
        // hiển thị thông báo exception (bắt lỗi ngoại lệ)
        public static void Exception(Exception e)
        => MessBox.Show(e.Message, MessageBoxButtons.OK, MessageBoxIcon.Error);

        #region Error

        public static void Error__AddedOrUpdated(string text, bool isAdd)
        => MessBox.Show($"{(isAdd ? "Thêm" : "Cập nhật")} [{text}] thất bại.", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__AlreadyExist(string text)
        => MessBox.Show($"[{text}] đã tồn tại!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__NotFind(string text)
        => MessBox.Show($"Không có dữ liệu nào trùng khớp với từ khoá [{text}]!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__CustomText(string text)
        => MessBox.Show($"{text}!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__ChangePassword(string text)
        => MessBox.Show($"Thay đổi mật khẩu cho tài khoản [{text}] thất bại!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__Deleted(string text)
        => MessBox.Show($"[{text}] xóa thất bại.", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__EmptyCode()
        => MessBox.Show("Không có mã đầu vào!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__EmptyList()
        => MessBox.Show($"Không có dữ liệu trong danh sách!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__EmptyListToExport()
        => MessBox.Show($"Không có dữ liệu để xuất ra tập tin!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__ExcessAmount()
        => MessBox.Show("Số lượng xuất kho vượt quá số lượng tồn kho.", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__Exist(string text, string textExist)
        => MessBox.Show($"[{text}] đã tồn tại !\n[{textExist}].", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__InputNotEmpty()
        => MessBox.Show("Không được để trống!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__InputEmpty()
        => MessBox.Show("Không có hoặc không đủ dữ liệu!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__NotChoice(string text)
        => MessBox.Show($"Chưa chọn [{text}]!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__NotExists(string text)
        => MessBox.Show($"[{text}] không tồn tại!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__NotImage()
        => MessBox.Show("Không có hình ảnh để hiển thị!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__NotOldEnough()
        => MessBox.Show("Không đủ 18 tuổi!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__PasswordIsNotCorrect()
        => MessBox.Show("Mật khẩu không trùng khớp!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__PasswordNotEquals()
        => MessBox.Show("Hai mật khẩu không trùng nhau!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__Pay()
        => MessBox.Show("Không thể tiến hành thanh toán!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__PleaseInputNumber()
        => MessBox.Show($"Vui lòng nhập số!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__SelectOneLine()
        => MessBox.Show("Chỉ có thể chọn 1 dòng!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__SystemNotWorking()
        => MessBox.Show($"Hệ thống đang gặp lỗi! Không thể đăng nhập.", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__UsernameNoPpermissions(string user)
        => MessBox.Show($"Tài khoản [{user}] không đủ đặc quyền!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__UsernameOrPassword()
        => MessBox.Show("Tài khoản hoặc mật khẩu không đúng!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static void Error__ViewImage()
        => MessBox.Show("Không thể xem hình ảnh.", MessageBoxButtons.OK, MessageBoxIcon.Error);

        #endregion Error

        #region Question

        // thông báo xác nhận
        public static DialogResult Question__CustomText(string text)
        => MessBox.Show($"{text}?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        // thông báo xác nhận làm mới lại control nhập liệu
        public static DialogResult Question__ClearInput()
        => MessBox.Show("Bạn có chắc chắn muốn làm mới dữ liệu không?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        // thông báo xác nhận một dữ liệu bất kì.
        public static DialogResult Question__Delete(string text)
        => MessBox.Show($"Bạn có muốn xóa [{text}] không?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        public static DialogResult Question__Update(string text)
       => MessBox.Show($"Bạn có muốn cập nhật [{text}] không?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        public static DialogResult Question__ExistInList(string text)
        => MessBox.Show($"[{text}] đã tồn tại trong danh sách!", MessageBoxButtons.OK, MessageBoxIcon.Error);

        public static DialogResult Question__ExitApplication()
        => MessBox.Show("Bạn có muốn thoát ứng dụng không?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        public static DialogResult Question__ExitForm(string text)
        => MessBox.Show($"Bạn có muốn thoát [{text}] không ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        public static DialogResult Question__KeepAddingNew(string text)
        => MessBox.Show($"Bạn muốn tiếp tục thêm mới [{text}] không?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        public static DialogResult Question__Logout()
        => MessBox.Show("Bạn có muốn đăng xuất?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        // thông báo xác nhận thay thế tập tin đã tồn tại
        public static DialogResult Question__ReplaceFile(string text)
        => MessBox.Show($"Tập tin [{text}] đã tồn tại, bạn muốn thay thế không?", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

        #endregion Question

        #region Success

        public static void Success__AddedOrUpdated(string text, bool isAdd)
        => MessBox.Show($"{(isAdd ? "Thêm" : "Cập nhật")} [{text}] thành công.", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static void Success__ChangePassword(string text)
        => MessBox.Show($"Thay đổi mật khẩu cho tài khoản [{text}] thành công.", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static void Success__Deleted(string text)
        => MessBox.Show($"Xoá [{text}] thành công.", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static void Success__CustomText(string text)
       => MessBox.Show($"{text}.", MessageBoxButtons.OK, MessageBoxIcon.Information);

        public static void SuccessOrError__Export(bool isSuccess, string text)
        => MessBox.Show($"Xuất tập tin [{text}] {(isSuccess ? "thành công" : "thất bại")}.", MessageBoxButtons.OK, isSuccess ? MessageBoxIcon.Information : MessageBoxIcon.Error);

        #endregion Success
    }
}