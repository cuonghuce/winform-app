using System;

namespace ShopSimpleClassic.Model
{
    public class BestSeller
    {
        private DateTime _date;
        private string _nameProduct;
        private string _idProduct;
        private int _quantity;

        public DateTime Date { get => _date; set => _date = value; }
        public string NameProduct { get => _nameProduct; set => _nameProduct = value; }
        public string IdProduct { get => _idProduct; set => _idProduct = value; }
        public int Quantity { get => _quantity; set => _quantity = value; }
    }
}