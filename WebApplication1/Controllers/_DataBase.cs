
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace WebApplication1.Controllers
{
    public class _DataBase
    {

        //private const string _ConnectionStrings = "Data Source = WIN-33F9NLTJ14K\\SQLEXPRESS; Initial Catalog = CookBook; Integrated Security = True";
        
        public string _ConnectionString()
        {
            var builder = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: false);
            IConfiguration config = builder.Build();
            return  config.GetValue<string>("ConnectionString");
        }

        private SqlConnection _SqlConnection = null;
        private SqlCommand _SqlCommand = null;
        private SqlDataReader _SqlDataReader = null;

        public List<string> StrToList(string str)
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

        public void Insert(string tableName,string values)
        {
            using(_SqlConnection = new SqlConnection(_ConnectionString()))
            {
                _SqlConnection.Open();
                try
                {
                    _SqlCommand = new SqlCommand($"insert into {tableName} values ({values});", _SqlConnection);
                    _SqlCommand.ExecuteNonQuery();
                }
                catch 
                {
                    throw new Exception();
                }
            }
        }
        public void Delete(string tableName,string condition)
        {
            using (_SqlConnection = new SqlConnection(_ConnectionString()))
            {
                _SqlConnection.Open();
                try
                {
                    _SqlCommand = new SqlCommand($"delete from {tableName} where {condition};", _SqlConnection);
                    _SqlCommand.ExecuteNonQuery();
                }
                catch
                {
                    throw new Exception();
                }
            }
        }
        public void Update(string tableName,string setValues,string condition)
        {
            using(_SqlConnection = new SqlConnection(_ConnectionString()))
            {
                _SqlConnection.Open();
                try
                {
                    _SqlCommand = new SqlCommand($"update {tableName} set {setValues} where {condition};", _SqlConnection);
                    _SqlCommand.ExecuteNonQuery();
                }
                catch 
                {
                    throw new Exception();
                }
            }
        }
        public List<string> ReadFromDataBase(string tableName,string columns)
        {
            List<string> list = new List<string>();
            string Columns = columns + ',';
            List<string> Col = StrToList(Columns);
            using(_SqlConnection = new SqlConnection(_ConnectionString()))
            {
                _SqlConnection.Open();
                try
                {
                    _SqlCommand = new SqlCommand($"select {columns} from {tableName};",_SqlConnection);
                    _SqlDataReader = _SqlCommand.ExecuteReader();

                    while (_SqlDataReader.Read())
                    {
                        for(int i = 0; i < _SqlDataReader.FieldCount; i++)
                        {
                            list.Add(_SqlDataReader[$"{Col[i]}"].ToString());
                        }
                    }
                }
                catch
                {
                    throw new Exception();
                }
            }
            return list;
        }
        public List<string> ReadFromDataBase(string query)
        {
            List<string> list = new List<string>();

            using(_SqlConnection = new SqlConnection(_ConnectionString()))
            {
                _SqlConnection.Open();
                try
                {
                    _SqlCommand = new SqlCommand($"{query}",_SqlConnection);
                    _SqlDataReader = _SqlCommand.ExecuteReader();
                    while (_SqlDataReader.Read())
                    {
                        for(int i = 0; i < _SqlDataReader.FieldCount; i++)
                        {
                            list.Add(_SqlDataReader[i].ToString());
                        }
                    }
                }
                catch
                {
                    throw new Exception();
                }
            }
            return list;
        }
        public string ReadFromDataBaseOneItem(string tableName,string columns, string condition)
        {
            string item = "";
            using(_SqlConnection = new SqlConnection(_ConnectionString()))
            {
                _SqlConnection.Open();
                try
                {
                    _SqlCommand = new SqlCommand($"select {columns} from {tableName} where {condition};",_SqlConnection);
                    _SqlDataReader = _SqlCommand.ExecuteReader();

                    while (_SqlDataReader.Read())
                    {
                       item = _SqlDataReader[$"{columns}"].ToString();
                    }
                }
                catch
                {
                    throw new Exception();
                }
            }
            return item;
        }

        public string ReadFromDataBaseOneItem(string tableName,string columns)
        {
            string item = "";
            using(_SqlConnection = new SqlConnection(_ConnectionString()))
            {
                _SqlConnection.Open();
                try
                {
                    _SqlCommand = new SqlCommand($"select {columns} from {tableName};",_SqlConnection);
                    _SqlDataReader = _SqlCommand.ExecuteReader();

                    while (_SqlDataReader.Read())
                    {
                       item = _SqlDataReader[$"{columns}"].ToString();
                    }
                }
                catch
                {
                    throw new Exception();
                }
            }
            return item;
        }
        public string ReadFromDataBaseOneItem(string query)
        {
            string item = "";
            using (_SqlConnection = new SqlConnection(_ConnectionString()))
            {
                _SqlConnection.Open();
                try
                {
                    _SqlCommand = new SqlCommand($"{query}", _SqlConnection);
                    _SqlDataReader = _SqlCommand.ExecuteReader();

                    while (_SqlDataReader.Read())
                    {
                        item = _SqlDataReader[0].ToString();
                    }
                }
                catch
                {
                    throw new Exception();
                }
            }
            return item;
        }
        public _DataBase()
        {

        }
    }
}
