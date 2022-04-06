namespace WebApplication1.Controllers
{
    public class _Products
    {
        private string nameProduct;
        private float priceProduct;
        private int codeUnit;

        public string NameProduct
        {
            get { return nameProduct; }
            set { nameProduct = value; }
        }
        public float PriceProduct
        {
            get { return priceProduct; }
            set { priceProduct = value; }
        }
        public int CodeUnit
        {
            get { return codeUnit; }
            set { codeUnit = value; }
        }
        public _Products(string nameProduct, float priceProduct,int codeUnit)
        {
            this.nameProduct = nameProduct;
            this.priceProduct = priceProduct;
            this.codeUnit = codeUnit;
        }
    }
}
