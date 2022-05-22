using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ChangeOrderInfo
    {
        _DataBase _dataBase;
        private int numberOrder;
        private List<string> dishesName;
        private List<string> countOrders;
        private List<string> priceOrders;
        private string allPrice;
        public List<DateTime> dateOrders;
        private AddOrderInfo addOrderInfo;

        public int NumbreOrder
        {
            get { return numberOrder; }
            set { numberOrder = value; }
        }
        public List <string> DishesName
        {
            get { return dishesName; }
            set { dishesName = value; }
        }
        public List <string> CountOrders
        {
            get { return countOrders; }
            set { countOrders = value; }
        }
        public List<string> PriceOrders
        {
            get { return priceOrders; }
            set { priceOrders = value; }
        }
        public string AllPrice
        {
            get { return allPrice; }
            set { allPrice = value; }
        }
        public List<DateTime> DateOrders
        {
            get { return dateOrders; }
            set { dateOrders = value; }
        }
        public AddOrderInfo AddOrderInfo
        {
            get { return addOrderInfo; }
            set { addOrderInfo = value; }
        }

        public ChangeOrderInfo(int numberOrder)
        {
            _dataBase = new _DataBase();
            this.numberOrder = numberOrder;
            int p=0;
            List<string> codeDishes = _dataBase.ReadFromDataBase($"orders where number_order = {numberOrder}", "code_dishes");
            List<string> dateOrder = _dataBase.ReadFromDataBase($"orders where number_order = {numberOrder}", "date_order"); ;

            this.countOrders = _dataBase.ReadFromDataBase($"orders where number_order = {numberOrder}", "count_order");
            this.priceOrders = _dataBase.ReadFromDataBase($"orders where number_order = {numberOrder}", "price_order");
            this.DateOrders = new List<DateTime>();
            this.dishesName = new List<string>();
            
            for(int i = 0; i < codeDishes.Count; i++)
            {
                this.DateOrders.Add(DateTime.Parse(dateOrder[i]));
                this.dishesName.Add(_dataBase.ReadFromDataBaseOneItem("dishes","name_dishes", $"code_dishes = {codeDishes[i]}"));
                p += int.Parse(this.priceOrders[i]);
            }
            this.allPrice = p.ToString();
            this.addOrderInfo = new AddOrderInfo();
        }
    }
    public class ChangeOrderInfoController : Controller
    {
        _DataBase _dataBase;
        public IActionResult ChangeOrderInfo(int number)
        {

            return View("ChangeOrderInfo",new ChangeOrderInfo(number));
        }
        public IActionResult Save(int number,int n)
        {
            try
            {
                _dataBase = new _DataBase();

                List<string> name = new List<string>();
                List<string> price = new List<string>();
                List<string> codename = new List<string>();
                List<string> count = new List<string>();
                List<string> oldPrice = new List<string>();
                List<string> oldCount = new List<string>();
                int newprice;

                for (int i = 0; i<n; i++)
                {
                    string select = Request.Form["NameDish"][i];
                    name.Add(_dataBase.StrToList(select)[0]);
                    price.Add(_dataBase.StrToList(select)[1]);
                    count.Add(Request.Form["Count"][i]);
                    codename.Add(_dataBase.ReadFromDataBaseOneItem("dishes", "code_dishes", $"name_dishes = '{name[i]}'"));
                    oldPrice.Add(Request.Form["oldPrice"][i]);
                    oldCount.Add(Request.Form["oldCount"][i]);
                    newprice = int.Parse(price[i])*int.Parse(count[i]);
                    _dataBase.Update("orders", $"code_dishes = {codename[i]},count_order = {count[i]},price_order = {newprice},date_order = '{DateTime.Now.ToString("yyyy-MM-dd")}'", $"price_order = {oldPrice[i]} and number_order = {number} and count_order = {oldCount[i]}");
                }
            }
            catch
            {
                
                throw new Exception();
            }

            return View("/Views/Home/Orders.cshtml", Actions.OrdersLoad());
        }

        public IActionResult Delete(int number)
        {
            _dataBase = new _DataBase();
            try
            {
                _dataBase.Delete("orders",$"number_order = {number}");
            }
            catch
            {
                throw new Exception();
            }
            return View("/Views/Home/Orders.cshtml",Actions.OrdersLoad());
        }
    }
}
