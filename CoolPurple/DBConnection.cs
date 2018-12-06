using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace CoolPurple
{
    abstract class DBConnection<T> where T : new()
    {

        protected String tableName, tableID;
        private Type workingClass ;

        public DBConnection()
        {
            workingClass = typeof(T);
            tableName = workingClass.Name;
            tableID = tableName + "ID";
        }



        public T find (object id)
        {
            MySqlCommand cmd = new MySqlCommand();
            cmd.CommandText = "SELECT * FROM " + tableName + " WHERE "+tableID+" = @id";
            cmd.Parameters.AddWithValue("@id", id);

            MySqlDataReader result = Database.getInstance().executeQuery(cmd);

            T obj = new T();
            while (result.Read())
            {   
                foreach (PropertyInfo propertyInfo in workingClass.GetProperties())
                {
                    propertyInfo.SetValue(obj, result[propertyInfo.Name]);
                }
            }

            return obj;
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
