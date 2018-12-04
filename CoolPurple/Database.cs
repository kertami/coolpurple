using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoolPurple
{
    class Database
    {
        private static Database instance;

        private Database() { }

        public static Database getInstance()
        {
            if (instance == null)
                instance = new Database();
            return instance;
        }


    }
}
