using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class AddDishController : Controller
    {
        _Product _Product = new _Product();
        _DishInfo _DishInfo = new _DishInfo();
        _DataBase _DataBase = new _DataBase();
        _Dishes _Dishes = null;
        _DishesFormula _DishesFormula = null;
        _Descriptions _Descriptions = null;
        public IActionResult AddDish(int n)
        {
            _DishInfo = new _DishInfo(n+1);
            
            return View("AddDish",_DishInfo);
        }

        public IActionResult OnPost()
        {
            try
            {
                string name = Request.Form["Name"];
                string type = Request.Form["Type"];
                float price = float.Parse(Request.Form["Price"]);
                string description = Request.Form["Description"];
                string composition = Request.Form["Composition"];
                composition += ',';


                _Product.Name = name;
                _Product.Type = type;
                _Product.Price = price;
                _Product.Description = description;
                _Product.Composition = StrToList(composition);
                List<string> tempProductUnit = new List<string>();
                for(int i = 0; i < _Product.Composition.Count; i++)
                {
                    int codeUnitProduct = Convert.ToInt32(_DataBase.ReadFromDataBaseOneItem("product", "code_unit", $"name_product = '{_Product.Composition[i]}'"));
                    string tempUnitName = _DataBase.ReadFromDataBaseOneItem("units", "name_unit", $"code_unit = {codeUnitProduct}");
                    tempProductUnit.Add(tempUnitName);
                }
                _Product.Unit = tempProductUnit;
                //_Product.Unit = _DataBase.ReadFromDataBase("units order by name_unit", "name_unit");

                int codeType = int.Parse(_DataBase.ReadFromDataBaseOneItem("dishes_type", "code_dishes_type",$"name_dishes_type = '{type}'"));
                _Dishes = new _Dishes(name, codeType, price);
                _DataBase.Insert("dishes", $"'{_Dishes.NameDishes}',{_Dishes.CodeDishesType},{_Dishes.PriceDishes}");
                
                int Code = int.Parse(_DataBase.ReadFromDataBaseOneItem("dishes", "code_dishes",$"name_dishes = '{name}'"));
                _Descriptions = new _Descriptions(Code,description);
                _DataBase.Insert("descriptions", $"{_Descriptions.CodeDishes},'{_Descriptions.Description}'");
            }
            catch
            {
                throw new Exception();
            }
            return View("AddFormulaForDish", _Product);
        }

        public IActionResult OnPostSecond(string name, string a)
        {
            try
            {
                string unit = Request.Form["Unit"];
                string count = Request.Form["Count"];
                unit += ',';
                count += ',';
                
                _Product.Unit = StrToList(unit);
                _Product.Count = StrToList(count);

                _Product.Composition = StrToList(a);

                _Product.Name = name;

                int CodeDishes = int.Parse(_DataBase.ReadFromDataBaseOneItem("dishes", "code_dishes",$"name_dishes = '{name}'"));
                
                for (int i = 0; i < _Product.Composition.Count; i++)
                {
                    int CodeProduct = int.Parse(_DataBase.ReadFromDataBaseOneItem("product", "code_product",$"name_product = '{_Product.Composition[i]}'"));
                    int CodeUnit = int.Parse(_DataBase.ReadFromDataBaseOneItem("units", "code_unit",$"name_unit = '{_Product.Unit[i]}'"));

                    _DishesFormula = new _DishesFormula(CodeDishes, CodeProduct, _Product.Count[i], CodeUnit);
                    _DataBase.Insert("dishes_formula",$"{_DishesFormula.CodeDishes},{_DishesFormula.CodeProduct},{_DishesFormula.CountProduct},{_DishesFormula.CodeUnit}");
                }
            }
            catch
            {
                throw new Exception();
            }
            return View("AddDish",new _DishInfo(_Product.Name));
        }

        private List<string> StrToList(string str)
        {
            List<string> list = new List<string>();
            string temp = "";
            foreach (char c in str)
            {
                if (c == ',')
                {
                    list.Add(temp);
                    temp = "";
                    continue;
                }
                temp += c;
            }
            return list;
        }
    }
}
