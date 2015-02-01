using CrowdDJ.DomainClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.Interfaces
{
    public interface IUser
    {
        bool InsertUser(User user);
        bool LoginUser(String email, String password);
        bool DeleteUser(int id);
        bool UpdateUser(User user, int id);
        ObservableCollection<User> GetAllUser();
        User FindUserById(int id);
        User FindUserByEmail(string email);
        List<KeyValuePair<string, bool>> GetAllEMails();
    }
}
