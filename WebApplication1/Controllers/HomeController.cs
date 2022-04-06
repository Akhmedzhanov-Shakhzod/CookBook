using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;


namespace WebApplication1.Controllers
{
    static class Actions
    {
        static _DataBase _dataBase = new _DataBase();
        static public List<_Product> ProductLoad()
        {
            List<_Product> products = new List<_Product>();

            List<string> name = new List<string>();
            List<string> price = new List<string>();
            List<string> codetype = new List<string>();

            name = _dataBase.ReadFromDataBase("product order by name_product", "name_product");
            price = _dataBase.ReadFromDataBase("product order by name_product", "price_product");
            codetype = _dataBase.ReadFromDataBase("product order by name_product", "code_unit");

            int count = _dataBase.ReadFromDataBase("product", "code_product").Count;
            for (int i = 0; i<count; i++)
            {
                _Product product = new _Product();

                product.Name = name[i];
                product.Price = float.Parse(price[i]);
                product.Type = _dataBase.ReadFromDataBaseOneItem("units", "name_unit", $"code_unit = {codetype[i]}");
                products.Add(product);
            }
            return products;
        }

        static public List<Orders> OrdersLoad()
        {
            List<Orders> orders = new List<Orders>();

            List<string> codeName = new List<string>();
            List<string> name = new List<string>();
            List<string> number = new List<string>();
            List<string> count = new List<string>();
            List<string> price = new List<string>();
            List<string> date = new List<string>();

            codeName = _dataBase.ReadFromDataBase("orders order by date_order desc", "code_dishes");
            number = _dataBase.ReadFromDataBase("orders order by date_order desc", "number_order");
            count = _dataBase.ReadFromDataBase("orders order by date_order desc", "count_order");
            price = _dataBase.ReadFromDataBase("orders order by date_order desc", "price_order");
            date = _dataBase.ReadFromDataBase("orders order by date_order desc", "date_order");

            int count1 = _dataBase.ReadFromDataBase("orders", "code_order").Count;

            for(int i = 0; i < count1; i++)
            {
                Orders o = new Orders();
                o.NameDish = _dataBase.ReadFromDataBaseOneItem("dishes","name_dishes",$"code_dishes = '{codeName[i]}'");
                o.NumberOrder = int.Parse(number[i]);
                o.CountOrder = int.Parse(count[i]);
                o.PriceOrder = price[i];
                o.Date = DateTime.Parse(date[i]);
                orders.Add(o);
            }

            return orders;
        }
    }

    public class Orders
    {
        private string nameDish;
        private int numberOrder;
        private int countOrder;
        private string priceOrder;
        private DateTime date;

        public string NameDish
        {
            get { return nameDish; }
            set { nameDish = value; }
        }
        public int NumberOrder
        {
            get { return numberOrder; }
            set { numberOrder = value; }
        }
        public int CountOrder
        {
            get { return countOrder; }
            set { countOrder = value; }
        }
        public string PriceOrder
        {
            get { return priceOrder; }
            set { priceOrder = value; }
        }
        public DateTime Date
        {
            get { return date; }
            set { date = value; }
        }

        public Orders(string nameDish,int numberOrder,int countOrder,string priceOrder,DateTime date)
        {
            this.nameDish = nameDish;
            this.numberOrder = numberOrder;
            this.countOrder = countOrder;
            this.priceOrder = priceOrder;
            this.date = date;
        }
        public Orders()
        {
        }
    }
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        _DataBase _dataBase = null;
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index(int id)
        {
            return View("Index",new _DishInfo(id));
        }

        public IActionResult Products()
        {
            _dataBase = new _DataBase();
            
            return View("Products",Actions.ProductLoad());
        }
        
        public IActionResult Orders()
        {

            return View("Orders",Actions.OrdersLoad());
        }
        
        public IActionResult Countdown()
        {
            return View("Countdown", new _Countdown());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}