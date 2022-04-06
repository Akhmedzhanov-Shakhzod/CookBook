namespace WebApplication1.Controllers
{
    public class _Product
    {
        private List<string> composition;
        public string Name { get; set; }
        public string Type { get; set; }
        public float Price { get; set; }
        public string Description { get; set; }
        public List<string> Composition
        {
            get { return composition; }
            set { composition = value; }
        }
        public List<string> Unit { get; set; }
        public List<string> Count { get; set; }
        public _Product(string name,string type,float price,string description,List<string> composition)
        {
            Name = name;
            Type = type;
            Price = price;
            Description = description;
            Composition = composition;
        }
        public _Product(List<string> unit,List<string> count)
        {
            Unit = unit;
            Count = count;
        }
        public _Product()
        {

        }
    }
}
