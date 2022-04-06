
using System.Data.SqlClient;


namespace WebApplication1.Controllers
{
    public class _DishInfo
    {

        private int id = 0;
        private string name;
        private string currentType;
        private string currentTypeCode;

        //public const string _ConnectionStrings = "Data Source = WIN-33F9NLTJ14K\\SQLEXPRESS; Initial Catalog = CookBook; Integrated Security = True";
        _DataBase _dataBase = new _DataBase();        
        public SqlConnection _Connection;
        public SqlCommand _Command;

        public List<string> _DishesTypes = new List<string>();
        public List<List<List<string>>> _Dishes = new List<List<List<string>>>();
        public List<string> _Products = new List<string>();
        public List<string> _Units = new List<string>();
        public string CurrentType
        {
            get { return currentType; }
            set { currentType = value; }
        }
        public string CurrentTypeCode
        {
            get { return currentTypeCode; }
            set { currentTypeCode = value; }
        }
        public string _Name
        {
            get { return name; }
            set { name = value; }
        }

        public int ID
        {
            get { return id; }
            set { id = value; }
        }
        public _DishInfo()
        {

        }
        public _DishInfo(int id)
        {
            ID = id;
            this.currentTypeCode = id.ToString();
            load();
        }
        
        public _DishInfo(string name)
        {
            _Name = name;
            load();
        }

        private void load()
        {
            string query = "Select name_dishes_type from dishes_type";
            string colunm = "name_dishes_type";
            _DishesTypes = ReadAndWrite(query,colunm);
            
            query = "select name_product from product order by name_product";
            colunm = "name_product";
            _Products = ReadAndWrite(query, colunm);
            
            query = "select name_unit from units order by name_unit";
            colunm = "name_unit";
            _Units = ReadAndWrite(query, colunm);

            for (int i = 0; i<_DishesTypes.Count; i++)
            {
                _Dishes.Add(_Data(i));
            }
            if(currentTypeCode != null)
            {
                currentType = _dataBase.ReadFromDataBaseOneItem("dishes_type", "name_dishes_type", $"code_dishes_type = {currentTypeCode}");
            }
        }
        private List<List<string>> _Data(int i)
        {
            List<List<string>> _retList = new List<List<string>>();

            using (_Connection = new SqlConnection(_dataBase._ConnectionString()))
            {
                _Connection.Open();

                string _QueryDishInfo = $"exec DishesInfo {_DishesTypes[i]}";
                _Command = new SqlCommand(_QueryDishInfo, _Connection);
                SqlDataReader _reader = _Command.ExecuteReader();

                while (_reader.Read())
                {
                    List<string> _List = new List<string>();
                    _List.Add(_reader["name_dishes"].ToString());
                    _List.Add(_reader["price_dishes"].ToString());
                    _List.Add(_reader["descriptions"].ToString());

                    _retList.Add(_List);
                }
                _reader.Close();
            }

            return _retList;
        }
        private List<string> ReadAndWrite(string query, string colunm)
        {
            List<string> list = new List<string>();
            using (_Connection = new SqlConnection(_dataBase._ConnectionString()))
            {
                _Connection.Open();

                _Command = new SqlCommand(query, _Connection);
                SqlDataReader _reader = _Command.ExecuteReader();

                // Call Read before accessing data.
                while (_reader.Read())
                {
                    list.Add((string)_reader[$"{colunm}"]);
                }

                // Call Close when done reading.
                _reader.Close();
            }
            return list;
        }
    }
}
