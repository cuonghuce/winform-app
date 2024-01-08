using ShopSimpleClassic.Library;
using ShopSimpleClassic.Model;
using ShopSimpleClassic.View.Manager;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopSimpleClassic.Controller
{
    public class bInvoice
    {
        private DBShopSimpleDataContext db = new DBShopSimpleDataContext();

        #region CURD

        /// <summary>
        /// thêm đối tượng vào cơ sở dữ liệu (database)
        /// </summary>
        /// <param name="obj"> Dữ liệu về đối tượng cần lưu </param>
        /// <returns> đúng: nếu thêm thành công, ngược lại là false </returns>
        public bool Add(Invoice data)
        {
            try
            {
                db.Invoices.InsertOnSubmit(data);
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
        public bool Update(Invoice data)
        {
            try
            {
                if (data == null) return false;

                var d = db.Invoices.FirstOrDefault(i => i.InvoiceCode == data.InvoiceCode);

                if (d == null) return false;

                d.UserID = data.UserID;
                d.CustomerPhone = data.CustomerPhone;
                d.Date = data.Date;
                d.Total = data.Total;
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
                if (string.IsNullOrEmpty(code)) return false;

                var d = db.Invoices.FirstOrDefault(i => i.InvoiceCode == code);

                if (d == null) return false;

                db.Invoices.DeleteOnSubmit(d);
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
        public IEnumerable<Invoice> List(string text, DateTime dateFrom, DateTime dateTo, bool isDate,
                                         string priceFrom, string priceTo, bool isPrice, int pageNumber, int pageSize)
        {
            try
            {
                var lst = isDate && isPrice ? getList(text, priceFrom, priceTo, dateFrom, dateTo) :
                          isDate ? getList(text, dateFrom, dateTo) :
                          isPrice ? getList(text, priceFrom, priceTo) :
                          getList(text);
                return lst.Skip(pageNumber * pageSize).Take(pageSize);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// lấy danh sách dữ liệu
        /// </summary>
        /// <param name="pageSize"> Số lượng cần hiển thị </param>
        /// <param name="filter">
        ///     Lọc theo điều kiện
        ///     0: theo ngày
        ///     1: theo tháng
        ///     2: theo năm
        /// </param>
        /// <returns> Danh sách dữ liệu tương ứng </returns>
        public IEnumerable<Invoice> List(int pageSize, int filter)
        {
            try
            {
                IEnumerable<Invoice> lst = db.Invoices;
                var dateNow = DateTime.Now;
                switch (filter)
                {
                    case 0: // for today
                        lst = lst.Where(i => i.Date.Day == dateNow.Day && i.Date.Month == dateNow.Month && i.Date.Year == dateNow.Year);
                        break;

                    case 1: // for month
                        lst = lst.Where(i => i.Date.Month == dateNow.Month && i.Date.Year == dateNow.Year);
                        break;

                    case 2: // for year
                        lst = lst.Where(i => i.Date.Year == dateNow.Year);
                        break;

                    default: // for today
                        lst = lst.Where(i => i.Date.Day == dateNow.Day && i.Date.Month == dateNow.Month && i.Date.Year == dateNow.Year);
                        break;
                }

                return lst.OrderByDescending(i => i.Date).Take(pageSize);
            }
            catch
            {
                return null;
            }
        }
        /// <summary>
        /// lấy danh sách dữ liệu
        /// </summary>
        /// <param name="filter">
        ///     Lọc theo điều kiện
        ///     0: theo ngày
        ///     1: theo tháng
        ///     2: theo năm
        /// </param>
        /// <returns> Danh sách dữ liệu tương ứng </returns>
        public IEnumerable<DTO_Chart> List(int filter)
        {
            try
            {
                IEnumerable<Invoice> lst = db.Invoices;
                IEnumerable<DTO_Chart> lstResult;
                switch (filter)
                {
                    case 0: // for today
                        lstResult = lst
                                    .GroupBy(i => new { i.Date.Year, i.Date.Month, i.Date.Day })
                                    .Select(g => new DTO_Chart
                                    {
                                        Date = $"{g.Key.Day}/{g.Key.Month}/{g.Key.Year}",
                                        NumberOfInvoices = g.Count(),
                                        Total = Lib.ConvertPrice(g.Sum(i => i.Total).ToString(), true)
                                    });
                        break;

                    case 1: // for month
                        lstResult = lst
                                    .GroupBy(i => new { i.Date.Year, i.Date.Month })
                                    .Select(g => new DTO_Chart
                                    {
                                        Date = $"{g.Key.Month}/{g.Key.Year}",
                                        NumberOfInvoices = g.Count(),
                                        Total = Lib.ConvertPrice(g.Sum(i => i.Total).ToString(), true)
                                    });
                        break;

                    case 2: // for year
                        lstResult = lst
                                    .GroupBy(i => i.Date.Year)
                                    .Select(g => new DTO_Chart
                                    {
                                        Date = $"{g.Key}",
                                        NumberOfInvoices = g.Count(),
                                        Total = Lib.ConvertPrice(g.Sum(i => i.Total).ToString(), true)
                                    });
                        break;

                    default: // for today
                        lstResult = lst
                                    .GroupBy(i => new { i.Date.Year, i.Date.Month, i.Date.Date })
                                    .Select(g => new DTO_Chart
                                    {
                                        Date = $"{g.Key.Date.Day}/{g.Key.Month}/{g.Key.Year}",
                                        NumberOfInvoices = g.Count(),
                                        Total = Lib.ConvertPrice(g.Sum(i => i.Total).ToString(), true)
                                    });
                        break;
                }

                return lstResult;
            }
            catch
            {
                return null;
            }
        }
        #endregion List

        #region Other

        /// <summary>
        /// Tạo mã tự động
        /// </summary>
        /// <returns></returns>
        public string CreateKey()
        {
            return ShopSimpleClassic.Library.Lib.CreateKey("BH", true);
        }

        /// <summary>
        /// Lấy dữ liệu chi tiết của một đối tượng
        /// </summary>
        /// <param name="code"> mã đối tượng (thường là id) </param>
        /// <returns> dữ liệu chi tiết của đối tượng </returns>
        public Invoice Detail(string code)
        {
            return db.Invoices.FirstOrDefault(i => i.InvoiceCode == code);
        }

        /// <summary>
        /// Trả về chỉ số (index) của hàng (row) trong danh sách Catalog dựa trên mã code và văn bản tìm kiếm.
        /// </summary>
        /// <param name="code">Mã code cần tìm</param>
        /// <param name="text">Văn bản tìm kiếm</param>
        /// <returns>Chỉ số (index) của hàng (row) trong danh sách Catalog</returns>
        public int IndexRows(string code, string text, DateTime dateFrom, DateTime dateTo, bool isDate,
                                        string priceFrom, string priceTo, bool isPrice)
        {
            var lst = isDate && isPrice ? getList(text, priceFrom, priceTo, dateFrom, dateTo) :
                          isDate ? getList(text, dateFrom, dateTo) :
                          isPrice ? getList(text, priceFrom, priceTo) :
                          getList(text);

            // Chuyển danh sách Catalog thành danh sách và tìm chỉ số (index) của hàng (row) có mã code trùng khớp
            return lst.ToList().FindIndex(i => i.InvoiceCode.Equals(code));
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
            return db.Invoices.Any(i => i.InvoiceCode == code);
        }

        public int TotalRows(string text, DateTime dateFrom, DateTime dateTo, bool isDate,
                                         string priceFrom, string priceTo, bool isPrice)
        {
            try
            {
                var lst = isDate && isPrice ? getList(text, priceFrom, priceTo, dateFrom, dateTo) :
                          isDate ? getList(text, dateFrom, dateTo) :
                          isPrice ? getList(text, priceFrom, priceTo) :
                          getList(text);
                return lst.Count();
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Lấy tổng số hoá đơn trong ngày hiện tại
        /// </summary>
        /// <returns> tổng số hoá đơn trng ngày hiện tại </returns>
        public int TotalBillToday()
        {
            try
            {
                var dateTime = DateTime.Now;
                return db.Invoices.Count(i => i.Date.Day == dateTime.Day && i.Date.Month == dateTime.Month && i.Date.Year == dateTime.Year);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Lấy tổng số hoá đơn trong tháng hiện tại
        /// </summary>
        /// <returns> tổng số hoá đơn trnog tháng hiện tại </returns>
        public int TotalBillMonth()
        {
            try
            {
                var dateTime = DateTime.Now;
                return db.Invoices.Count(i => i.Date.Month == dateTime.Month && i.Date.Year == dateTime.Year);
            }
            catch
            {
                return 0;
            }
        }

        private IEnumerable<Invoice> getList(string text, string priceFrom, string priceTo, DateTime dateFrom, DateTime dateTo)
        {
            int pFrom = Convert.ToInt32(string.IsNullOrEmpty(priceFrom) ? "0" : priceFrom);
            int pTo = Convert.ToInt32(string.IsNullOrEmpty(priceTo) ? "0" : priceTo);
            return getList(text).Where(i => i.Total >= pFrom && i.Total <= pTo && i.Date.Date >= dateFrom.Date && i.Date.Date <= dateTo.Date);
        }

        private IEnumerable<Invoice> getList(string text, string priceFrom, string priceTo)
        {
            int pFrom = Convert.ToInt32(string.IsNullOrEmpty(priceFrom) ? "0" : priceFrom);
            int pTo = Convert.ToInt32(string.IsNullOrEmpty(priceTo) ? "0" : priceTo);
            return getList(text).Where(i => i.Total >= pFrom && i.Total <= pTo);
        }

        private IEnumerable<Invoice> getList(string text, DateTime dateFrom, DateTime dateTo)
        {
            return getList(text).Where(i => i.Date.Date >= dateFrom.Date && i.Date.Date <= dateTo.Date);
        }

        private IEnumerable<Invoice> getList(string text)
        {
            return db.Invoices.Where(i => i.InvoiceCode.ToLower().Contains(text.ToLower()) ||
                                         i.User.Name.ToLower().Contains(text.ToLower()) ||
                                         i.CustomerPhone.Contains(text) ||
                                         i.Customer.Name.ToLower().Contains(text.ToLower()));
        }

        #endregion Other
    }
}