using ShopSimpleClassic.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopSimpleClassic.Controller
{
    public class bImportDetail
    {
        private DBShopSimpleDataContext db = new DBShopSimpleDataContext();

        #region CURD

        /// <summary>
        /// thêm đối tượng vào cơ sở dữ liệu (database)
        /// </summary>
        /// <param name="obj"> Dữ liệu về đối tượng cần lưu </param>
        /// <returns> đúng: nếu thêm thành công, ngược lại là false </returns>
        public bool Add(ImportDetail data)
        {
            try
            {
                db.ImportDetails.InsertOnSubmit(data);
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
        public bool Update(ImportDetail data)
        {
            try
            {
                if (data == null) return false;

                var d = db.ImportDetails.FirstOrDefault(i => i.ImportID == data.ImportID);

                if (d == null) return false;

                d.ProductID = data.ProductID;
                d.Quantity = data.Quantity;
                d.Price = data.Price;
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
        /// <param name="codeInvoice"> mã hóa đơn </param>
        /// <param name="codeProduct"> mã sản phẩm </param>
        /// <returns> đúng: nếu xoá thành công, ngược lại là false </returns>
        public bool Delete(string codeInvoice, string codeProduct)
        {
            try
            {
                if (string.IsNullOrEmpty(codeInvoice) || string.IsNullOrEmpty(codeProduct)) return false;

                var d = db.ImportDetails.FirstOrDefault(i => i.ImportID == codeInvoice && i.ProductID == codeProduct);

                if (d == null) return false;

                db.ImportDetails.DeleteOnSubmit(d);
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
        public IEnumerable<ImportDetail> List(string codeInvoice)
        {
            try
            {
                return db.ImportDetails.Where(i => i.ImportID == codeInvoice);
            }
            catch
            {
                return null;
            }
        }

        #endregion List

        #region Other

        /// <summary>
        /// Lấy dữ liệu chi tiết của một đối tượng
        /// </summary>
        /// <param name="code"> mã đối tượng (thường là id) </param>
        /// <returns> dữ liệu chi tiết của đối tượng </returns>
        public ImportDetail Detail(string codeInvoice, string codeProduct)
        {
            return db.ImportDetails.FirstOrDefault(i => i.ImportID == codeInvoice && i.ProductID == codeProduct);
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
        {
            return db.ImportDetails.Any(i => i.ImportID == code);
        }

        /// <summary>
        /// Kiểm tra dữ liệu trong cơ sở dữ liệu
        /// </summary>
        /// <param name="codeInvoice"> mã hóa đơn cần kiểm tra </param>
        /// <param name="codeProduct"> mã sản phẩm cần kiểm tra </param>
        /// <returns>
        ///     true: nếu dữ liệu có tồn tại
        ///     false: nếu dữ liệu không tồn tại
        /// </returns>
        public bool IsExists(string codeInvoice, string codeProduct)
        {
            return db.ImportDetails.Any(i => i.ImportID == codeInvoice && i.ProductID == codeProduct);
        }

        /// <summary>
        /// Lấy tổng số lượng sản phẩm
        /// </summary>
        /// <param name="codeInvoice"> Mã hóa đơn </param>
        /// <returns>
        ///     tổng số lượng của các sản phẩm trong hóa đơn
        /// </returns>
        public int TotalQuantity(string codeInvoice)
        {
            try
            {
                return db.ImportDetails.Where(i => i.ImportID == codeInvoice).Sum(i => i.Quantity);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Lấy tổng số sản phẩm có trong hóa đơn
        /// </summary>
        /// <param name="codeInvoice"> mã hóa đơn cần lấy </param>
        /// <returns>
        ///     số sản phẩm có trnog hóa đơn
        /// </returns>
        public int TotalAmount(string codeInvoice)
        {
            try
            {
                return db.ImportDetails.Count(i => i.ImportID == codeInvoice);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Lấy tổng số sản phẩm trnog hoá đơn của ngày hiện tại
        /// </summary>
        /// <returns> tổng số sản phẩm trong hoá đơn của ngày hiện tại </returns>
        public int TotalAmountProductToday()
        {
            try
            {
                var dateTime = DateTime.Now;
                var result = db.Imports
                            .Join(db.ImportDetails, import => import.ImportCode, importDetail => importDetail.ImportID, (import, importDetail) => new { import, importDetail })
                            .Where(i => i.import.Date.Day == dateTime.Day && i.import.Date.Month == dateTime.Month && i.import.Date.Year == dateTime.Year)
                            .GroupBy(x => x.import.Date)
                            .Select(g => new
                            {
                                Date = g.Key,
                                TotalAmount = g.Count()
                            });
                return result.Sum(i => i.TotalAmount);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Lấy tổng số sản phẩm trong hoá đơn của tháng hiện tại
        /// </summary>
        /// <returns> Tổng số sản phẩm trong hoá đơn của tháng hiện tại </returns>
        public int TotalAmountProductMonth()
        {
            try
            {
                var dateTime = DateTime.Now;
                var result = db.Imports
                            .Join(db.ImportDetails, import => import.ImportCode, importDetail => importDetail.ImportID, (import, importDetail) => new { import, importDetail })
                            .Where(i => i.import.Date.Month == dateTime.Month && i.import.Date.Year == dateTime.Year)
                            .GroupBy(x => x.import.Date)
                            .Select(g => new
                            {
                                Date = g.Key,
                                TotalAmount = g.Count()
                            });
                return result.Sum(i => i.TotalAmount);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Lấy tổng số lượng của các sản phẩm trong hoá đơn trong thời gian hiện tại
        /// </summary>
        /// <returns> Tổng số lượng của sản phẩm trong thời gian hiện tại </returns>
        public int TotalQuantityProductToday()
        {
            try
            {
                var dateTime = DateTime.Now;
                var result = db.Imports
                            .Join(db.ImportDetails, import => import.ImportCode, importDetail => importDetail.ImportID, (import, importDetail) => new { import, importDetail })
                            .Where(i => i.import.Date.Day == dateTime.Day && i.import.Date.Month == dateTime.Month && i.import.Date.Year == dateTime.Year)
                            .GroupBy(x => x.import.Date)
                            .Select(g => new
                            {
                                Date = g.Key,
                                TotalQuantity = g.Sum(x => x.importDetail.Quantity)
                            });
                return result.Sum(i => i.TotalQuantity);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Lấy tổng số lượng của sản phẩm trong tháng hiện tại
        /// </summary>
        /// <returns> Tổng số lượng của sản phẩm trnog tháng hiện tại </returns>
        public int TotalQuantityProductMonth()
        {
            try
            {
                var dateTime = DateTime.Now;
                var result = db.Imports
                            .Join(db.ImportDetails, import => import.ImportCode, importDetail => importDetail.ImportID, (import, importDetail) => new { import, importDetail })
                            .Where(i => i.import.Date.Month == dateTime.Month && i.import.Date.Year == dateTime.Year)
                            .GroupBy(x => x.import.Date)
                            .Select(g => new
                            {
                                Date = g.Key,
                                TotalQuantity = g.Sum(x => x.importDetail.Quantity)
                            });
                return result.Sum(i => i.TotalQuantity);
            }
            catch
            {
                return 0;
            }
        }

        #endregion Other
    }
}