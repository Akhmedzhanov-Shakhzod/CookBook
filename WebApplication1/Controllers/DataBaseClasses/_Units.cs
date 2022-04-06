namespace WebApplication1.Controllers
{
    public class _Units
    {
        private string nameUnit;

        public string NameUnit
        {
            get { return nameUnit; }
            set { nameUnit = value; }
        }

        public _Units(string nameUnit)
        {
            this.nameUnit = nameUnit;
        }
    }
}
