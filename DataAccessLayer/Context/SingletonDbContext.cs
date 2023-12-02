using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Context
{
    internal class SingletonDbContext
    {
        public static AuthAPIDbContext? dbContext;
        private SingletonDbContext()
        {

        }
        public static AuthAPIDbContext GetDbContext()
        {
            if (dbContext == null)
            {
                dbContext = new AuthAPIDbContext();
            }
            return dbContext;
        }
    }
}
