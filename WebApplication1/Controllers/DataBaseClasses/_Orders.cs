namespace WebApplication1.Controllers
{
    public class _Orders
    {
        private int codeDishes;
        private int numberOrder;
        private int countOrder;
        private float priceOrder;
        private DateTime dateOrder;

        public int CodeDishes
        {
            get { return codeDishes; }
            set { codeDishes = value; }
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
        public float PriceOrder
        {
            get { return priceOrder; }
            set { priceOrder = value; }
        }
        public DateTime DateOrder
        {
            get { return dateOrder; }
            set { dateOrder = value; }
        }
        public _Orders(int codeDishes,int numberOrder,int countOrder,float priceOrder,DateTime dateOrder)
        {
            this.codeDishes = codeDishes;
            this.numberOrder = numberOrder;
            this.countOrder = countOrder;
            this.priceOrder = priceOrder;
            this.dateOrder = dateOrder;
        }
    }
}
