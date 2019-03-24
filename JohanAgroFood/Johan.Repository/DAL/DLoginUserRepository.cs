using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Johan.DATA;

namespace Johan.Repository
{
    public class DLoginUserRepository : Disposable,ILoginUserRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DLoginUserRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {
            this.context = context;
        }

        public tblUser GetUserById(tblUser user)
        {
            //return null;
            var existingUser = context.tblUsers.Where(l => l.userId == user.userId).FirstOrDefault();
            if (existingUser != null)
            {
                string hashedPass = Md5.CalculateMD5Hash(user.password);
                if (string.Compare(existingUser.password, hashedPass) == 0)
                {
                    return existingUser;
                }
            }
            return null;
        }

    }
}