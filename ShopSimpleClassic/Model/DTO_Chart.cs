using System;

namespace ShopSimpleClassic.Model
{
    public class DTO_Chart
    {
        private string _date;
        private int _numberOfInvoices;
        private string _Total;

        public string Date { get => _date; set => _date = value; }
        public int NumberOfInvoices { get => _numberOfInvoices; set => _numberOfInvoices = value; }
        public string Total { get => _Total; set => _Total = value; }
    }
}