using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AddOrderInfo
    {
        _DataBase _dataBase = new _DataBase();
        private string name;
        private string nameDishes;
        private int numberOrder;
        private int countOrder;
        private string priceDishes;
        private string priceOrder;
        private DateTime date;
        private List<string> allNameDishes;
        private List<string> allPriceDishes;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string NameDishes
        {
            get { return nameDishes; }
            set { nameDishes = value; }
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
        public string PriceDishes
        {
            get { return priceDishes; }
            set { priceDishes = value; }
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
        public List<string> AllNameDishes
        {
            get { return allNameDishes; }
            set { allNameDishes = value; }
        }
        public List<string> AllPriceDishes
        {
            get { return allPriceDishes; }
            set { allPriceDishes = value; }
        }

        public AddOrderInfo()
        {
            this.allNameDishes = _dataBase.ReadFromDataBase("dishes order by name_dishes", "name_dishes");
            this.allPriceDishes = _dataBase.ReadFromDataBase("dishes order by name_dishes", "price_dishes");
        }
    }
    public class AddOrderController : Controller
    {
        _DataBase _dataBase = new _DataBase();
        public IActionResult AddOrder()
        {
            AddOrderInfo orderInfo = new AddOrderInfo();
            try
            {
                List<string> numbers = _dataBase.ReadFromDataBase("orders", "number_order");
                numbers.Sort();
                orderInfo.NumberOrder = int.Parse(numbers[numbers.Count-1]) +1;
            }
            catch
            {
                orderInfo.NumberOrder=1;
            }
            return View("AddOrder", orderInfo);
        }
        public IActionResult Post()
        {
            
            AddOrderInfo addOrderInfo = new AddOrderInfo();
            try
            {
                addOrderInfo.Date = DateTime.Now;
                string select = Request.Form["NameDish"];
                string number = Request.Form["Number"];
                int count = int.Parse(Request.Form["Count"]);
                addOrderInfo.Name = _dataBase.StrToList(select)[0];
                addOrderInfo.PriceDishes = _dataBase.StrToList(select)[1];
                addOrderInfo.PriceOrder = (int.Parse(addOrderInfo.PriceDishes)*count).ToString();
                addOrderInfo.NumberOrder = int.Parse(number);
                string codeDishes = _dataBase.ReadFromDataBaseOneItem("dishes", "code_dishes", $"name_dishes = '{addOrderInfo.Name}'");
                _dataBase.Insert("orders", $"{codeDishes},{number},{count},{addOrderInfo.PriceOrder},'{addOrderInfo.Date}'");
            }
            catch
            {
                throw new Exception();
            }
            return View("AddOrder",addOrderInfo);
        }

        public IActionResult PostContinue(int number)
        {
            AddOrderInfo addOrderInfo = new AddOrderInfo();
            addOrderInfo.NumberOrder = number;
            return View("AddOrder", addOrderInfo);
        }
    }
}
