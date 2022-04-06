using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Controllers
{
    public class ChangeDish
    {
        _DataBase _dataBase;
        private string name;
        private _Dishes dishes;
        private _Descriptions description;
        private List<_DishesFormula> formula;
        private List<string> formula_product;
        private List<string> formula_count_product;
        private List<string> formula_unit_product;
        
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
        public _Dishes Dishes
        {
            get { return dishes; }
            set { dishes = value; }
        }
        public _Descriptions Description
        {
            get { return description; }
            set { description = value; }
        }
        public List<_DishesFormula> Formula
        {
            get { return formula; }
            set { formula = value; }
        }
        public List<string> Formula_Product
        {
            get { return formula_product; }
            set { formula_product = value; }
        }
        public List<string> Formula_Count_Product
        {
            get { return formula_count_product; }
            set { formula_count_product = value; }
        }
        public List<string> Formula_Unit_Product
        {
            get { return formula_unit_product; }
            set { formula_unit_product = value; }
        }
        public ChangeDish(string name)
        {
            this.name = name;
            _dataBase = new _DataBase();
            dishes = new _Dishes(name, 
                int.Parse(_dataBase.ReadFromDataBaseOneItem("dishes", "code_dishes_type", $"name_dishes = '{name}'")),
                float.Parse(_dataBase.ReadFromDataBaseOneItem("dishes", "price_dishes", $"name_dishes = '{name}'")));
            int code_dishes = int.Parse(_dataBase.ReadFromDataBaseOneItem("dishes", "code_dishes", $"name_dishes = '{name}'"));
            description = new _Descriptions(code_dishes,
                _dataBase.ReadFromDataBaseOneItem("descriptions", "descriptions",$"code_dishes = {code_dishes}"));

            List<string> code_product = _dataBase.ReadFromDataBase($"dishes_formula where code_dishes = '{code_dishes}'", "code_ingredient");
            List<string> count_product = _dataBase.ReadFromDataBase($"dishes_formula where code_dishes = '{code_dishes}'", "count_ingredient");
            List<string> unit_product = _dataBase.ReadFromDataBase($"dishes_formula where code_dishes = '{code_dishes}'", "unit_ingredient");

            formula = new List<_DishesFormula>();

            formula_product = new List<string>();
            formula_count_product = new List<string>();
            formula_unit_product = new List<string>();

            for (int i = 0; i < code_product.Count; i++)
            {
                formula.Add(new _DishesFormula(code_dishes,int.Parse(code_product[i]),count_product[i],int.Parse(unit_product[i])));
            }
            for(int i = 0;i< formula.Count; i++)
            {
                formula_product.Add(_dataBase.ReadFromDataBaseOneItem("product", "name_product",$"code_product = {formula[i].CodeProduct}"));
                formula_count_product.Add(formula[i].CountProduct);
                formula_unit_product.Add(_dataBase.ReadFromDataBaseOneItem("units", "name_unit",$"code_unit = {formula[i].CodeUnit}"));
            }

        }

    }
    public class ChangeDishInfoController : Controller
    {
        _DataBase _dataBase;

        public IActionResult ChangeDishInfo(string name)
        {
            return View("ChangeDishInfo",new ChangeDish(name));
        }

        public IActionResult Save(string name)
        {
            ChangeDish save = new ChangeDish(name);

            string Price = Request.Form["Price"].ToString();
            string Description = Request.Form["Description"].ToString();

            _dataBase = new _DataBase();
            string code_dishes = _dataBase.ReadFromDataBaseOneItem("dishes","code_dishes",$"name_dishes = '{name}'");
            _dataBase.Update("dishes", $"price_dishes = {Price}", $"name_dishes = '{name}'");
            _dataBase.Insert("descriptions", $"{code_dishes},'{Description}'");

            return View("/Views/Home/Index.cshtml", new _DishInfo(save.Dishes.CodeDishesType-1));
        }

        public IActionResult Delete(string name)
        {
            ChangeDish delete = new ChangeDish(name);

            _dataBase = new _DataBase();
            _dataBase.Delete("dishes", $"name_dishes = '{name}'");

            return View("/Views/Home/Index.cshtml", new _DishInfo(delete.Dishes.CodeDishesType-1));
        }
    }
}
