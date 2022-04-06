using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ChangeProduct
    {
        private string name;
        private string price;
        private string typeunit;
        private string codetypeunit;
        private UnitsP unit;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public string Price
        {
            get { return price; }
            set { price = value; }
        }
        public string TypeUnit
        {
            get { return typeunit; }
            set { typeunit = value; }
        }
        public string CodeTypeUnit
        {
            get { return codetypeunit; }
            set { codetypeunit = value; }
        }
        public UnitsP Unit
        {
            get { return unit; }
            set { unit = value; }
        }

        public ChangeProduct(string name, string price, string typeunit, UnitsP unit)
        {
            this.name = name;
            this.price = price;
            this.typeunit = typeunit;
            this.unit = unit;
        }
    }
    public class ChangeProductInfoController : Controller
    {
        _DataBase _dataBase;
        public IActionResult ChangeProductInfo(string name, string price, string typeunit)
        {
            _dataBase = new _DataBase();

            UnitsP unitsP = new UnitsP();
            try
            {
                unitsP.Units = _dataBase.ReadFromDataBase("units order by name_unit", "name_unit");
            }
            catch
            {
                throw new Exception();
            }

            ChangeProduct change = new ChangeProduct(name, price, typeunit,unitsP);

            return View("ChangeProductInfo", change);
        }

        public IActionResult Save(string name)
        {
            _dataBase = new _DataBase();

            string price = Request.Form["Price"];
            string typeunit = Request.Form["unit"];
            string codetypeunit = _dataBase.ReadFromDataBaseOneItem("units", "code_unit",$"name_unit = '{typeunit}'");

            _dataBase.Update("product", $"price_product = {price},code_unit = {codetypeunit}", $"name_product = '{name}'");

            return View("/Views/Home/Products.cshtml", Actions.ProductLoad());
        }

        public IActionResult Delete(string Name)
        {
            _dataBase = new _DataBase();

            _dataBase.Delete("product", $"name_product = '{Name}'");


            return View("/Views/Home/Products.cshtml",Actions.ProductLoad());
        }
    }
}
