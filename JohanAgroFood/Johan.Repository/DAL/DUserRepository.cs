using Johan.DATA;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Johan.Repository
{
    public class DUserRepository:Disposable,IUserRepository
    {
        private JohanAgroFoodDBEntities context = null;

        public DUserRepository(JohanAgroFoodDBEntities context)
            : base(context)
        {
            this.context = context;    
        }
        public List<tblUser> GetUser()
        {
            List<tblUser> users = context.tblUsers.ToList();
            return users;
        }

        public bool SaveUser(tblUser user)
        {
            user.password = Md5.CalculateMD5Hash(user.password);
            context.tblUsers.Add(user);
            return context.SaveChanges() > 0;
        }
        public bool Edit(tblUser objUser)
        {
            var orgEditUser = context.tblUsers.Where(ss => ss.ID == objUser.ID).FirstOrDefault();
            orgEditUser.userId = objUser.userId;
            orgEditUser.password = Md5.CalculateMD5Hash(objUser.password);
            orgEditUser.firstName = objUser.firstName;
            orgEditUser.lastName = objUser.lastName;
            orgEditUser.userType = objUser.userType;

            return context.SaveChanges() > 0;
        }
        public bool Delete(int pk)
        {
            var orgDeleteUser = context.tblUsers.Where(ss => ss.ID == pk).FirstOrDefault();


            context.tblUsers.Remove(orgDeleteUser);
            return context.SaveChanges() > 0;
        }
    }
}
