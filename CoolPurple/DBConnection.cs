using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CoolPurple
{
    abstract class DBConnection<T> where T : new()
    {

        protected String tableName;
        private Type workingClass ;

        public DBConnection()
        {
            workingClass = typeof(T);
            tableName = workingClass.Name;
        }

        public List<T> findAll()
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + tableName;

            List<T> returnList = new List<T>();

            MySqlDataReader result = Database.getInstance().executeQuery(cmd);

            while (result.Read())
            {
                T obj = new T();
                foreach (PropertyInfo propertyInfo in workingClass.GetProperties())
                {
                    propertyInfo.SetValue(obj, result[propertyInfo.Name]);
                }
                returnList.Add(obj);
            }

            return returnList;
        }

         
    }
}
