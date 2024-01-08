using ShopSimpleClassic.Model;
using System.Collections.Generic;
using System.Linq;

namespace ShopSimpleClassic.Controller
{
    public class bAdmin
    {
        private DBShopSimpleDataContext db = new DBShopSimpleDataContext();

        #region CURD

        /// <summary>
        /// thêm đối tượng vào cơ sở dữ liệu (database)
        /// </summary>
        /// <param name="obj"> Dữ liệu về đối tượng cần lưu </param>
        /// <returns> đúng: nếu thêm thành công, ngược lại là false </returns>
        public bool Add(Admin obj)
        {
            try
            {
                db.Admins.InsertOnSubmit(obj);
                db.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// cập nhật đối tượng trong cơ sở dữ liệu (database)
        /// </summary>
        /// <param name="obj"> Dữ liệu về đối tượng cần chỉnh sửa </param>
        /// <returns> đúng: nếu cập nhật thành công, ngược lại là false </returns>
        public bool Update(Admin obj)
        {
            try
            {
                var data = Detail(obj.Username);
                data.Name = obj.Name;
                data.Password = obj.Password;

                db.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// cập nhật đối tượng trong cơ sở dữ liệu (database)
        /// </summary>
        /// <param name="obj"> Dữ liệu về đối tượng cần chỉnh sửa </param>
        /// <returns> đúng: nếu cập nhật thành công, ngược lại là false </returns>
        public bool UpdateName(Admin obj)
        {
            try
            {
                var data = Detail(obj.Username);
                data.Name = obj.Name;

                db.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }
        /// <summary>
        /// cập nhật mật khẩu cho đối tượng trong cơ sở dữ liệu (database)
        /// </summary>
        /// <param name="username"> Tài khoản nhân viên </param>
        /// <param name="password"> Mật khẩu mới </param>
        /// <returns> đúng: nếu cập nhật thành công, ngược lại là false </returns>
        public bool UpdatePassword(string username, string password)
        {
            try
            {
                var data = Detail(username);
                data.Password = password;

                db.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Xoá đối tượng trong cơ sở dữ liệu (database)
        /// </summary>
        /// <param name="code"> mã đối tượng cần xoá </param>
        /// <returns> đúng: nếu xoá thành công, ngược lại là false </returns>
        public bool Delete(string code)
        {
            try
            {
                db.Admins.DeleteOnSubmit(Detail(code));
                db.SubmitChanges();

                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion CURD

        #region List

        /// <summary>
        /// Trả về một danh sách dữ liệu dựa trên ký tự tìm kiếm, số trang và kích thước trang.
        /// </summary>
        /// <param name="text">Ký tự tìm kiếm</param>
        /// <param name="pageNumber">Số trang</param>
        /// <param name="pageSize">Kích thước trang</param>
        /// <returns>Danh sách các Admin phù hợp với tiêu chí tìm kiếm và trang hiện tại</returns>
        public IEnumerable<Admin> List(string text, int pageNumber, int pageSize)
        {
            try
            {
                IEnumerable<Admin> data = db.Admins;

                // Kiểm tra nếu văn bản tìm kiếm không rỗng hoặc null
                if (!string.IsNullOrEmpty(text))
                {
                    // Lọc danh sách Admin dựa trên tên chứa văn bản tìm kiếm (không phân biệt chữ hoa, chữ thường)
                    data = getList(text);
                }

                // Bỏ qua số lượng hàng (row) trên các trang trước đó và lấy số lượng hàng (row) theo kích thước trang
                return data.Skip(pageNumber * pageSize).Take(pageSize);
            }
            catch
            {
                return null;
            }
        }

        #endregion List

        #region Other

        // Tổng số dòng dòng dữ liệu có trong cơ sở dữ liệu
        public int TotalRows()
        {
            try
            {
                return db.Admins.Count();
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Lấy dữ liệu chi tiết của một đối tượng
        /// </summary>
        /// <param name="code"> mã đối tượng (thường là id) </param>
        /// <returns> dữ liệu chi tiết của đối tượng </returns>
        public Admin Detail(string code)
        {
            var data = db.Admins.FirstOrDefault(i => i.Username == code);
            return data == null ? null : data;
        }

        /// <summary>
        /// Kiểm tra dữ liệu trong cơ sở dữ liệu
        /// </summary>
        /// <param name="code"> mã đối tượng cần kiểm tra </param>
        /// <returns>
        ///     true: nếu dữ liệu có tồn tại
        ///     false: nếu dữ liệu không tồn tại
        /// </returns>
        public bool IsExists(string code)
        => db.Admins.Any(i => i.Username == code);

        /// <summary>
        /// Kiểm tra dữ liệu trong cơ sở dữ liệu
        /// </summary>
        /// <param name="code"> tên tài khoản </param>
        /// <param name="pass"> mật khẩu </param>
        /// <returns>
        ///     true: nếu dữ liệu có tồn tại
        ///     false: nếu dữ liệu không tồn tại
        /// </returns>
        public bool IsExists(string code, string pass)
        => db.Admins.Any(i => i.Username == code && i.Password == pass);

        /// <summary>
        /// Trả về chỉ số (index) của hàng (row) trong danh sách Catalog dựa trên mã code và văn bản tìm kiếm.
        /// </summary>
        /// <param name="code">Mã code cần tìm</param>
        /// <param name="text">Văn bản tìm kiếm</param>
        /// <returns>Chỉ số (index) của hàng (row) trong danh sách Catalog</returns>
        public int IndexRows(string code, string text)
        {
            IEnumerable<Admin> data = db.Admins;

            // Kiểm tra nếu văn bản tìm kiếm không rỗng hoặc null
            if (!string.IsNullOrEmpty(text))
            {
                // Lọc danh sách Catalog dựa trên tên chứa văn bản tìm kiếm (không phân biệt chữ hoa, chữ thường)
                data = getList(text);
            }

            // Chuyển danh sách Catalog thành danh sách và tìm chỉ số (index) của hàng (row) có mã code trùng khớp
            return data.ToList().FindIndex(i => i.Username.Equals(code));
        }

        /// <summary>
        /// Tổng số dòng dòng dữ liệu có trong cơ sở dữ liệu
        /// </summary>
        /// <param name="text"> từ khoá tìm kiếm </param>
        /// <returns> số dòng dữ liệu tương ứng với [text] </returns>
        public int TotalRows(string text)
        {
            try
            {
                return getList(text).Count();
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// lấy danh sách dữ liệu
        /// </summary>
        /// <param name="text"> từ khóa tìm kiếm </param>
        /// <returns></returns>
        private IEnumerable<Admin> getList(string text)
        {
            return string.IsNullOrEmpty(text) ? db.Admins :
                                                db.Admins.Where(i => i.Username.Contains(text) ||
                                                                     i.Name.ToLower().Contains(text.ToLower()));
        }

        #endregion Other
    }
}