using ShopSimpleClassic.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopSimpleClassic.Controller
{
    public class bProduct
    {
        private DBShopSimpleDataContext db = new DBShopSimpleDataContext();

        #region CURD

        /// <summary>
        /// thêm đối tượng vào cơ sở dữ liệu (database)
        /// </summary>
        /// <param name="obj"> Dữ liệu về đối tượng cần lưu </param>
        /// <returns> đúng: nếu thêm thành công, ngược lại là false </returns>
        public bool Add(Product obj)
        {
            try
            {
                db.Products.InsertOnSubmit(obj);
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
        public bool Update(Product obj)
        {
            try
            {
                var data = Detail(obj.ProductCode);
                data.Name = obj.Name;
                data.Image = obj.Image;
                data.CatalogID = obj.CatalogID;
                data.SupplierID = obj.SupplierID;
                data.Amount = obj.Amount;
                data.Price = obj.Price;
                data.CreateDate = obj.CreateDate;
                data.Status = obj.Status;

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
        /// <param name="code"> Mã sản phẩm </param>
        /// <param name="amount"> số lượng </param>
        /// <param name="isAdd"> sự kiện thêm hoặc trừ </param>
        /// <returns> đúng: nếu cập nhật thành công, ngược lại là false </returns>
        public bool UpdateAmount(string code, int amount, bool isAdd)
        {
            try
            {
                if (string.IsNullOrEmpty(code)) return false;

                var d = db.Products.FirstOrDefault(i => i.ProductCode == code);

                if (d == null) return false;

                var t = isAdd ? Convert.ToInt32(amount) + Convert.ToInt32(d.Amount) : Convert.ToInt32(d.Amount) - Convert.ToInt32(amount);

                d.Amount = t;

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
                db.Products.DeleteOnSubmit(Detail(code));
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
        /// <returns>Danh sách các Product phù hợp với tiêu chí tìm kiếm và trang hiện tại</returns>
        public IEnumerable<Product> List(string text, int pageNumber, int pageSize)
        {
            try
            {
                IEnumerable<Product> data = db.Products;

                // Kiểm tra nếu văn bản tìm kiếm không rỗng hoặc null
                if (!string.IsNullOrEmpty(text))
                {
                    // Lọc danh sách Product dựa trên tên chứa văn bản tìm kiếm (không phân biệt chữ hoa, chữ thường)
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

        /// <summary>
        /// Tạo mã tự dộng
        /// </summary>
        /// <returns></returns>
        public string CreateKey()
        => Library.Lib.CreateKey("SP", true);

        /// <summary>
        /// Lấy dữ liệu chi tiết của một đối tượng
        /// </summary>
        /// <param name="code"> mã đối tượng (thường là id) </param>
        /// <returns> dữ liệu chi tiết của đối tượng </returns>
        public Product Detail(string code)
        {
            var data = db.Products.FirstOrDefault(i => i.ProductCode == code);
            return data == null ? null : data;
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
                return db.InvoiceDetails.Select(p => p.ProductID).Distinct().Count();
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Lấy tổng số các sản phẩm tồn kho
        /// </summary>
        /// <returns>
        ///     Tổng số các data được sử dụng
        /// </returns>
        public int TotalStock()
        {
            try
            {
                return db.Products.Count(i => i.Amount > 0);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Lấy tổng số các sản phẩm tồn kho
        /// </summary>
        /// <returns>
        ///     Tổng số các data được sử dụng
        /// </returns>
        public int TotalOutOfStock()
        {
            try
            {
                return db.Products.Count(i => i.Amount <= 0);
            }
            catch
            {
                return 0;
            }
        }

        // Tổng số dòng dòng dữ liệu có trong cơ sở dữ liệu
        public int TotalRows()
        {
            try
            {
                return db.Products.Count();
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
        => db.Products.Any(i => i.ProductCode == code);

        /// <summary>
        /// Trả về chỉ số (index) của hàng (row) trong danh sách Product dựa trên mã code và văn bản tìm kiếm.
        /// </summary>
        /// <param name="code">Mã code cần tìm</param>
        /// <param name="text">Văn bản tìm kiếm</param>
        /// <returns>Chỉ số (index) của hàng (row) trong danh sách Product</returns>
        public int IndexRows(string code, string text)
        {
            IEnumerable<Product> data = db.Products;

            // Kiểm tra nếu văn bản tìm kiếm không rỗng hoặc null
            if (!string.IsNullOrEmpty(text))
            {
                // Lọc danh sách Product dựa trên tên chứa văn bản tìm kiếm (không phân biệt chữ hoa, chữ thường)
                data = getList(text);
            }

            // Chuyển danh sách Product thành danh sách và tìm chỉ số (index) của hàng (row) có mã code trùng khớp
            return data.ToList().FindIndex(i => i.ProductCode.Equals(code));
        }

        /// <summary>
        /// lấy danh sách dữ liệu
        /// </summary>
        /// <param name="text"> từ khóa tìm kiếm </param>
        /// <returns></returns>
        private IEnumerable<Product> getList(string text)
        {
            return string.IsNullOrEmpty(text) ? db.Products :
                                                db.Products.Where(i => i.ProductCode.ToLower().Contains(text) ||
                                                                       i.Name.ToLower().Contains(text) ||
                                                                       i.Catalog.Name.ToLower().Contains(text) ||
                                                                       i.Supplier.Name.ToLower().Contains(text));
        }

        #endregion Other
    }
}