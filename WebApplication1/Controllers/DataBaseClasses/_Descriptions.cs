namespace WebApplication1.Controllers
{
    public class _Descriptions
    {
        private int codeDishes;
        private string description;

        public int CodeDishes
        {
            get { return codeDishes; }
            set { codeDishes = value; }
        }
        public string Description
        {
            get { return description; }
            set { description = value; }
        }

        public _Descriptions(int codeDishes, string description)
        {
            this.codeDishes = codeDishes;
            this.description = description;
        }
    }
}
