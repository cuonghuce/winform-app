using ShopSimpleClassic.Model;
using System.Collections.Generic;
using System.Linq;

namespace ShopSimpleClassic.Controller
{
    public class bCatalog
    {
        private DBShopSimpleDataContext db = new DBShopSimpleDataContext();

        #region CURD

        /// <summary>
        /// thêm đối tượng vào cơ sở dữ liệu (database)
        /// </summary>
        /// <param name="obj"> Dữ liệu về đối tượng cần lưu </param>
        /// <returns> đúng: nếu thêm thành công, ngược lại là false </returns>
        public bool Add(Catalog obj)
        {
            try
            {
                db.Catalogs.InsertOnSubmit(obj);
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
        public bool Update(Catalog obj)
        {
            try
            {
                var data = Detail(obj.CatalogCode);
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
        /// Xoá đối tượng trong cơ sở dữ liệu (database)
        /// </summary>
        /// <param name="code"> mã đối tượng cần xoá </param>
        /// <returns> đúng: nếu xoá thành công, ngược lại là false </returns>
        public bool Delete(string code)
        {
            try
            {
                db.Catalogs.DeleteOnSubmit(Detail(code));
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
        /// <returns>Danh sách các Catalog phù hợp với tiêu chí tìm kiếm và trang hiện tại</returns>
        public IEnumerable<Catalog> List(string text, int pageNumber, int pageSize)
        {
            try
            {
                IEnumerable<Catalog> data = db.Catalogs;

                // Kiểm tra nếu văn bản tìm kiếm không rỗng hoặc null
                if (!string.IsNullOrEmpty(text))
                {
                    // Lọc danh sách Catalog dựa trên tên chứa văn bản tìm kiếm (không phân biệt chữ hoa, chữ thường)
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

        // Danh sách cho combobox
        public List<ItemForSelect> ListForCombobox()
        => db.Catalogs.Select(i => new ItemForSelect { Display = i.Name, Value = i.CatalogCode }).OrderBy(i => i.Display).ToList();

        #endregion List

        #region Other

        /// <summary>
        /// Lấy dữ liệu chi tiết của một đối tượng
        /// </summary>
        /// <param name="code"> mã đối tượng (thường là id) </param>
        /// <returns> dữ liệu chi tiết của đối tượng </returns>
        public Catalog Detail(string code)
        {
            var data = db.Catalogs.FirstOrDefault(i => i.CatalogCode == code);
            return data == null ? null : data;
        }

        // Tổng số dòng dòng dữ liệu có trong cơ sở dữ liệu
        public int TotalRows()
        {
            try
            {
                return db.Catalogs.Count();
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Lấy tổng số các data được sử dụng trong Product
        /// </summary>
        /// <returns>
        ///     Tổng số các data được sử dụng
        /// </returns>
        public int TotalUsed()
        {
            try
            {
                return db.Products.Select(p => p.CatalogID).Distinct().Count();
            }
            catch
            {
                return 0;
            }
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
        /// Kiểm tra dữ liệu trong cơ sở dữ liệu
        /// </summary>
        /// <param name="code"> mã đối tượng cần kiểm tra </param>
        /// <returns>
        ///     true: nếu dữ liệu có tồn tại
        ///     false: nếu dữ liệu không tồn tại
        /// </returns>
        public bool IsExists(string code)
        => db.Catalogs.Any(i => i.CatalogCode == code);

        /// <summary>
        /// Trả về chỉ số (index) của hàng (row) trong danh sách Catalog dựa trên mã code và văn bản tìm kiếm.
        /// </summary>
        /// <param name="code">Mã code cần tìm</param>
        /// <param name="text">Văn bản tìm kiếm</param>
        /// <returns>Chỉ số (index) của hàng (row) trong danh sách Catalog</returns>
        public int IndexRows(string code, string text)
        {
            IEnumerable<Catalog> data = db.Catalogs;

            // Kiểm tra nếu văn bản tìm kiếm không rỗng hoặc null
            if (!string.IsNullOrEmpty(text))
            {
                // Lọc danh sách Catalog dựa trên tên chứa văn bản tìm kiếm (không phân biệt chữ hoa, chữ thường)
                data = getList(text);
            }

            // Chuyển danh sách Catalog thành danh sách và tìm chỉ số (index) của hàng (row) có mã code trùng khớp
            return data.ToList().FindIndex(i => i.CatalogCode.Equals(code));
        }

        /// <summary>
        /// lấy danh sách dữ liệu
        /// </summary>
        /// <param name="text"> từ khóa tìm kiếm </param>
        /// <returns></returns>
        private IEnumerable<Catalog> getList(string text)
        {
            return string.IsNullOrEmpty(text) ? db.Catalogs :
                                                db.Catalogs.Where(i => i.CatalogCode.Contains(text) ||
                                                                    i.Name.ToLower().Contains(text.ToLower()));
        }

        #endregion Other
    }
}