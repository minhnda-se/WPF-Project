using KoiCare_BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KoiCare_DAOs
{
    public class AccountDAO
    {
        private KoiCareSystemAtHomeDbContext dbContext;
        private static AccountDAO instance = null;
        public static AccountDAO Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new AccountDAO();
                }
                return instance;
            }
        }

        public AccountDAO()
        {
            dbContext = new KoiCareSystemAtHomeDbContext();
        }

        public  AccountTbl GetAccountByEmail(string email)
        {
            return  dbContext.AccountTbls.SingleOrDefault(a => a.Email.Equals(email));
        }
    }
}
