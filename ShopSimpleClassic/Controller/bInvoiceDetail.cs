using ShopSimpleClassic.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ShopSimpleClassic.Controller
{
    public class bInvoiceDetail
    {
        private DBShopSimpleDataContext db = new DBShopSimpleDataContext();

        #region CURD

        /// <summary>
        /// thêm đối tượng vào cơ sở dữ liệu (database)
        /// </summary>
        /// <param name="obj"> Dữ liệu về đối tượng cần lưu </param>
        /// <returns> đúng: nếu thêm thành công, ngược lại là false </returns>
        public bool Add(InvoiceDetail data)
        {
            try
            {
                db.InvoiceDetails.InsertOnSubmit(data);
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
        public bool Update(InvoiceDetail data)
        {
            try
            {
                if (data == null) return false;

                var d = db.InvoiceDetails.FirstOrDefault(i => i.InvoiceID == data.InvoiceID);

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

                var d = db.InvoiceDetails.FirstOrDefault(i => i.InvoiceID == codeInvoice && i.ProductID == codeProduct);

                if (d == null) return false;

                db.InvoiceDetails.DeleteOnSubmit(d);
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
        /// <param name="codeInvoice"> mã hóa đơn </param>
        /// <returns>Danh sách các Catalog phù hợp với tiêu chí tìm kiếm và trang hiện tại</returns>
        public IEnumerable<InvoiceDetail> List(string codeInvoice)
        {
            try
            {
                return db.InvoiceDetails.Where(i => i.InvoiceID == codeInvoice);
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// Lấy danh sách các sản phẩm sản phẩm bán chạy nhất
        /// </summary>
        /// <param name="pageSize"> Số dòng hiển thị </param>
        /// <param name="filter">
        ///     Bộ lọc dữ liệu
        ///     0: Theo ngày
        ///     1: theo tháng
        ///     2: theo năm
        /// </param>
        /// <returns></returns>
        public IEnumerable<BestSeller> List_BestSeller(int pageSize, int filter)
        {
            try
            {
                var invoice = db.Invoices;
                var invoiceDetail = db.InvoiceDetails;
                var product = db.Products;
                //IEnumerable<BestSeller> lst = invoice
                //            .Join(invoiceDetail, invoices => invoices.InvoiceCode, invoiceDetails => invoiceDetails.InvoiceID, (invoices, invoiceDetails) => new { invoices, invoiceDetails })
                //            .Join(product, combined => combined.invoiceDetails.ProductID, products => products.ProductCode, (combined, products) => new { combined.invoices, combined.invoiceDetails, products })
                //            .Select(result => new BestSeller { Date = result.invoices.Date, NameProduct = result.products.Name, Amount = result.products.Amount, Quantity = result.invoiceDetails.Quantity});
                IEnumerable<BestSeller> lst = invoice
                .Join(invoiceDetail, inv => inv.InvoiceCode, invDetail => invDetail.InvoiceID, (inv, invDetail) => new { inv, invDetail })
                .GroupBy(combined => combined.invDetail.ProductID)
                .Select(group => new BestSeller
                {
                    NameProduct = group.FirstOrDefault().invDetail.Product.Name,
                    IdProduct = group.FirstOrDefault().invDetail.ProductID,
                    Quantity = group.Sum(item => item.invDetail.Quantity),
                    Date = group.Max(item => item.inv.Date)
                });
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

                return lst.OrderByDescending(i => i.Quantity).Take(pageSize);
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
        public InvoiceDetail Detail(string codeInvoice, string codeProduct)
        {
            return db.InvoiceDetails.FirstOrDefault(i => i.InvoiceID == codeInvoice && i.ProductID == codeProduct);
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
            return db.InvoiceDetails.Any(i => i.InvoiceID == code);
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
            return db.InvoiceDetails.Any(i => i.InvoiceID == codeInvoice && i.ProductID == codeProduct);
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
                return db.InvoiceDetails.Where(i => i.InvoiceID == codeInvoice).Sum(i => i.Quantity);
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
                return db.InvoiceDetails.Count(i => i.InvoiceID == codeInvoice);
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// Đến các sản phẩm có trong đơn bán hàng
        /// </summary>
        /// <param name="codeProduct"> Mã sản phẩm cần tìm </param>
        /// <returns> số lượng sản phẩm có trong hoá đơn bán </returns>
        public int CountProductInInvoice(string codeProduct)
        {
            try
            {
                return db.InvoiceDetails.Count(i => i.ProductID == codeProduct);
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
                var result = db.Invoices
                            .Join(db.InvoiceDetails, invoice => invoice.InvoiceCode, invoiceDetail => invoiceDetail.InvoiceID, (invoice, invoiceDetail) => new { invoice, invoiceDetail })
                            .Where(i => i.invoice.Date.Day == dateTime.Day && i.invoice.Date.Month == dateTime.Month && i.invoice.Date.Year == dateTime.Year)
                            .GroupBy(x => x.invoice.Date)
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
                var result = db.Invoices
                            .Join(db.InvoiceDetails, invoice => invoice.InvoiceCode, invoiceDetail => invoiceDetail.InvoiceID, (invoice, invoiceDetail) => new { invoice, invoiceDetail })
                            .Where(i => i.invoice.Date.Month == dateTime.Month && i.invoice.Date.Year == dateTime.Year)
                            .GroupBy(x => x.invoice.Date)
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
                var result = db.Invoices
                            .Join(db.InvoiceDetails, invoice => invoice.InvoiceCode, invoiceDetail => invoiceDetail.InvoiceID, (invoice, invoiceDetail) => new { invoice, invoiceDetail })
                            .Where(i => i.invoice.Date.Day == dateTime.Day && i.invoice.Date.Month == dateTime.Month && i.invoice.Date.Year == dateTime.Year)
                            .GroupBy(x => x.invoice.Date)
                            .Select(g => new
                            {
                                Date = g.Key,
                                TotalQuantity = g.Sum(x => x.invoiceDetail.Quantity)
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
                var result = db.Invoices
                            .Join(db.InvoiceDetails, invoice => invoice.InvoiceCode, invoiceDetail => invoiceDetail.InvoiceID, (invoice, invoiceDetail) => new { invoice, invoiceDetail })
                            .Where(i => i.invoice.Date.Month == dateTime.Month && i.invoice.Date.Year == dateTime.Year)
                            .GroupBy(x => x.invoice.Date)
                            .Select(g => new
                            {
                                Date = g.Key,
                                TotalQuantity = g.Sum(x => x.invoiceDetail.Quantity)
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