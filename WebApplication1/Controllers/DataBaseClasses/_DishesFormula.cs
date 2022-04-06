namespace WebApplication1.Controllers
{
    public class _DishesFormula
    {
        private int codeDishes;
        private int codeProduct;
        private string countProduct;
        private int codeUnit;

        public int CodeDishes
        {
            get { return codeDishes; }
            set { codeDishes = value; }
        }
        public int CodeProduct
        {
            get { return codeProduct; }
            set { codeProduct = value; }
        }
        public string CountProduct
        {
            get { return countProduct; }
            set { countProduct = value; }
        }
        public int CodeUnit
        {
            get { return codeUnit; }
            set { codeUnit = value; }
        }

        public _DishesFormula(int codeDishes, int codeProduct, string countProduct,int codeUnit)
        {
            this.codeDishes = codeDishes;
            this.codeProduct = codeProduct;
            this.countProduct = countProduct;
            this.codeUnit = codeUnit;
        }
    }
}
