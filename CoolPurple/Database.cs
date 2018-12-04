using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CoolPurple
{
    class Database
    {
        private static Database instance;
        private MySqlConnectionStringBuilder connStr = new MySqlConnectionStringBuilder();
        private MySqlConnection conn;
        private bool isOpen = false;

        private Database() {
            connStr.Server = "127.0.0.1";
            connStr.UserID = "root";
            connStr.Password = "prot";
            connStr.Database = "CoolPurple";

            conn = new MySqlConnection(connStr.ToString());
        }

        public static Database getInstance()
        {
            if (instance == null)
                instance = new Database();
            return instance;
        }

        public bool open()
        {
            try
            {
                if (!isOpen)
                    conn.Open();
                isOpen = true;
            }
            catch (MySqlException ex)
            {
                String errString = "Application crashed with errorcode \"" + ex.ErrorCode.ToString() + "\". \nPlease contact a technician.";
                MessageBox.Show(errString, "Application Crashed :(", MessageBoxButton.OK, MessageBoxImage.Error);
                isOpen = false;
            }

            return isOpen;
        }

        public bool close()
        {
            if (isOpen) conn.Close();
            isOpen = false;

            return true;
        }


        public MySqlDataReader executeQuery(MySqlCommand cmd)
        {
            try
            {
                open();

                cmd.Connection = conn;

                MySqlDataReader rdr = cmd.ExecuteReader();


                return rdr;

            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Application Crashed", MessageBoxButton.OK, MessageBoxImage.Error);
                close();
                return null;
            }


        }

    }
}
