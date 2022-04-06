namespace WebApplication1.Controllers
{
    public class _Dishes
    {
        private string nameDishes;
        private int codeDishesType;
        private float priceDishes;

        public string NameDishes
        {
            get { return nameDishes; }
            set { nameDishes = value; }
        }
        public int CodeDishesType
        {
            get { return codeDishesType; }
            set { codeDishesType = value; }
        }
        public float PriceDishes
        {
            get { return priceDishes; }
            set { priceDishes = value; }
        }

        public _Dishes(string nameDishes,int codeDishesType,float priceDishes)
        {
            this.codeDishesType = codeDishesType;
            this.nameDishes = nameDishes;   
            this.priceDishes = priceDishes;
        }
    }
}
