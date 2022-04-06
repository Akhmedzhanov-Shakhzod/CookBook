using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class UnitsP 
    {
        private string nameProduct;
        private List<string> units;
        public string NameProduct
        {
            get { return nameProduct; }
            set { nameProduct = value; }
        }
        public List<string> Units
        {
            get { return units; }
            set { units = value; }
        }
        public UnitsP() 
        {
        }
    }
    public class AddProductController : Controller
    {
        _DataBase _dataBase;
        public IActionResult AddProduct()
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
            return View("AddProduct",unitsP);
        }

        public IActionResult Post()
        {
            _dataBase = new _DataBase();
            UnitsP unitsP = new UnitsP();

            try
            {
                unitsP.Units = _dataBase.ReadFromDataBase("units order by name_unit", "name_unit");

                string name = Request.Form["name"];
                string unit = Request.Form["unit"];
                string price = Request.Form["price"];
                string unitCode = _dataBase.ReadFromDataBaseOneItem("units", "code_unit", $"name_unit = '{unit}'");
                unitsP.NameProduct = name;

                _dataBase.Insert("product", $"'{name}',{price},{unitCode}");
            }
            catch
            {
                throw new Exception();
            }
            return View("AddProduct", unitsP);
        }
    }
}
