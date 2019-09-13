using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GreatOutdoors.Entities
{
    public class Product
    {
        private int _productID;
        private string _productName;
        private int _categoryID;
        private int _specificationID;
        private int _pCostPrice;
        private int _pSellingPrice;
        private Boolean _available;

        public int ProductID { get => _productID; set => _productID = value; }
        public string ProductName { get => _productName; set => _productName = value; }
        public int CategoryID { get => _categoryID; set => _categoryID = value; }
        public int SpecificationID { get => _specificationID; set => _specificationID = value; }
        public int PCostPrice { get => _pCostPrice; set => _pCostPrice = value; }
        public int PSellingPrice { get => _pSellingPrice; set => _pSellingPrice = value; }
        public bool Available { get => _available; set => _available = value; }
    }

    public class Category
    {
        private int _productID;
        private int _categoryID;
        private string _categoryName;

        public int ProductID { get => _productID; set => _productID = value; }
        public int CategoryID { get => _categoryID; set => _categoryID = value; }
        public string CategoryName { get => _categoryName; set => _categoryName = value; }
    }

    public class Specification
    {
        private int _productID;
        private int _specificationID;
        private string _color;
        private int _size;
        private string _techspec;

        public int ProductID { get => _productID; set => _productID = value; }
        public int SpecificationID { get => _specificationID; set => _specificationID = value; }
        public string Color { get => _color; set => _color = value; }
        public int Size { get => _size; set => _size = value; }
        public string Techspec { get => _techspec; set => _techspec = value; }
    }

    public class Inventory
    {
        private int _productID;
        private int _pQuantity;
        private double _pTotalCostPrice;

        public int ProductID { get => _productID; set => _productID = value; }
        public int PQuantity { get => _pQuantity; set => _pQuantity = value; }
        public double PTotalCostPrice { get => _pTotalCostPrice; set => _pTotalCostPrice = value; }
    }

    public class Retailer
    {
        private int _retailerID;
        private string _uName;
        private string _password;
        private string _name;
        private string _email;
        private string _phone;

        public int RetailerID { get => _retailerID; set => _retailerID = value; }
        public string UName { get => _uName; set => _uName = value; }
        public string Password { get => _password; set => _password = value; }
        public string Name { get => _name; set => _name = value; }
        public string Email { get => _email; set => _email = value; }
        public string Phone { get => _phone; set => _phone = value; }
    }

    public class Address
    {
        private int _retailerID;
        private int _addressID;
        private string _line1;
        private string _line2;
        private string _city;
        private int _pinCode;
        private string _state;

        public int RetailerID { get => _retailerID; set => _retailerID = value; }
        public int AddressID { get => _addressID; set => _addressID = value; }
        public string Line1 { get => _line1; set => _line1 = value; }
        public string Line2 { get => _line2; set => _line2 = value; }
        public string City { get => _city; set => _city = value; }
        public int PinCode { get => _pinCode; set => _pinCode = value; }
        public string State { get => _state; set => _state = value; }
    }

    public class Order
    {
        private int _orderID;
        private List<int>[] _productIDList;
        private int _retailerID;
        private bool _channelOfSale;
        private double _sellingPrice;

        public int OrderID { get => _orderID; set => _orderID = value; }
        //public int[] ProductIDList { get => _productIDList; set => _productIDList = value; }
        public int RetailerID { get => _retailerID; set => _retailerID = value; }
        public bool ChannelOfSale { get => _channelOfSale; set => _channelOfSale = value; }
        public double SellingPrice { get => _sellingPrice; set => _sellingPrice = value; }
    }

    public class Return
    {
        private int _returnID;
        private int _orderID;
        private int _productID;
        private bool _reasonIncomplete;
        private bool _reasonWrong;
        private double _returnValue;
        private int _returnQuantity;

        public int ReturnID { get => _returnID; set => _returnID = value; }
        public int OrderID { get => _orderID; set => _orderID = value; }
        public int ProductID { get => _productID; set => _productID = value; }
        public bool ReasonIncomplete { get => _reasonIncomplete; set => _reasonIncomplete = value; }
        public bool ReasonWrong { get => _reasonWrong; set => _reasonWrong = value; }
        public double ReturnValue { get => _returnValue; set => _returnValue = value; }
        public int ReturnQuantity { get => _returnQuantity; set => _returnQuantity = value; }
    }

    public class Discount
    {
        private int _orderID;
        private int _retailerID;
        private double _retailerDiscount;
        private double _categoryDiscount;

        public int OrderID { get => _orderID; set => _orderID = value; }
        public int RetailerID { get => _retailerID; set => _retailerID = value; }
        public double RetailerDiscount { get => _retailerDiscount; set => _retailerDiscount = value; }
        public double CategoryDiscount { get => _categoryDiscount; set => _categoryDiscount = value; }
    }
}
