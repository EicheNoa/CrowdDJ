using CrowdDJ.Interfaces;
using CrowdDJ.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using System.Collections.ObjectModel;

namespace CrowdDJ.DAO
{
    public class UserDAO : IUser
    {
        const string CmdInsert = @"INSERT INTO [dbo].[User] (email, password, isAdmin, name) 
                                          VALUES (@pEmail, @pPassword, @pIsAdmin, @pName)";
        const string CmdDelete = @"DELETE FROM [dbo].[User] WHERE userId = @pUserId";
        const string CmdUpdate = @"UPDATE [dbo].[User] SET email = @pEmail, isAdmin = @pIsAdmin, name = @pName
                                    WHERE userId = @pUserId";
        const string CmdSearch = @"SELECT * FROM [dbo].[User] WHERE userId = @pUserId";
        const string CmdSearchWithEmail = @"SELECT * FROM [dbo].[User] WHERE email = @pEmail";
        const string CmdSelectAll = @"SELECT * FROM [dbo].[User]";
        const string CmdGetAllEMails = @"SELECT email, isAdmin FROM [dbo].[User]";

        private IDataBase database;

        public UserDAO(IDataBase database)
        {
            this.database = database;
        }

        #region private CreateCommand methods
        private DbCommand CreateInsertCmd(User newUser)
        {
            DbCommand cmd = database.CreateCommand(CmdInsert);
            database.DefineParameter(cmd, "@pEmail", DbType.String, newUser.Email);
            database.DefineParameter(cmd, "@pPassword", DbType.String, newUser.Password);
            database.DefineParameter(cmd, "@pIsAdmin", DbType.String, newUser.IsAdmin);
            database.DefineParameter(cmd, "@pName", DbType.String, newUser.Name);
            return cmd;
        }
        private DbCommand CreateDeleteCmd(int deleteUserId)
        {
            DbCommand cmd = database.CreateCommand(CmdDelete);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, deleteUserId);
            return cmd;
        }
        private DbCommand CreateSearchUserByEmailCmd(string email)
        {
            DbCommand cmd = database.CreateCommand(CmdSearchWithEmail);
            database.DefineParameter(cmd, "@pEmail", DbType.String, email);
            return cmd;
        }
        private DbCommand CreateUpdateUserCmd(User updateUser, int id)
        {
            DbCommand cmd = database.CreateCommand(CmdUpdate);
            database.DefineParameter(cmd, "@pEmail", DbType.String, updateUser.Email);
            database.DefineParameter(cmd, "@pIsAdmin", DbType.String, updateUser.IsAdmin);
            database.DefineParameter(cmd, "@pName", DbType.String, updateUser.Name);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, id);
            return cmd;
        }
        private DbCommand CreateSearchUserCmd(int userId)
        {
            DbCommand cmd = database.CreateCommand(CmdSearch);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, userId);
            return cmd;
        }
        private DbCommand CreateSearchAllCmd()
        {
            DbCommand cmd = database.CreateCommand(CmdSelectAll);
            return cmd;
        }
        private DbCommand CreateGetAllEMailsCmd()
        {
            DbCommand cmd = database.CreateCommand(CmdGetAllEMails);
            return cmd;
        }
        #endregion

        public bool InsertUser(User newUser)
        {
            try
            {
                if (FindUserByEmail(newUser.Email) == null)
                {
                    newUser.Password = newUser.Password.GetHashCode().ToString();
                    using (DbCommand cmd = CreateInsertCmd(newUser))
                    {
                        return database.ExecuteNonQuery(cmd) == 1;
                    }
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateUser(User updateUser, int id)
        {
            using (DbCommand cmd = CreateUpdateUserCmd(updateUser, id))
            {
                return database.ExecuteNonQuery(cmd) == 1;
            }
        }

        public bool DeleteUser(int id)
        {
            using (DbCommand cmd = CreateDeleteCmd(id))
            {
                return database.ExecuteNonQuery(cmd) == 1;
            }
        }

        public ObservableCollection<User> GetAllUser()
        {
            ObservableCollection<User> result = new ObservableCollection<User>();
            User user = null;
            int rUserId = 0;
            string rName = "";
            string rEmail = "";
            bool rIsAdmin = false;

            using (DbCommand cmd = CreateSearchAllCmd())
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    rUserId = rDr.GetInt32(0);
                    rEmail = rDr.GetString(1);
                    rIsAdmin = rDr.GetBoolean(3);
                    rName = rDr.GetString(4);
                    user = new User(rName, "", rEmail, rIsAdmin);
                    user.UserId = rUserId;
                    result.Add(user);
                }
            }

            return result;
        }

        public User FindUserById(int id)
        {
            User user = null;
            int rUserId = 0;
            string rName = "";
            string rEmail = "";
            bool rIsAdmin = false;

            using (DbCommand cmd = CreateSearchUserCmd(id))
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    rUserId = rDr.GetInt32(0);
                    rEmail = rDr.GetString(1);
                    rIsAdmin = rDr.GetBoolean(3);
                    rName = rDr.GetString(4);
                    user = new User(rName, "", rEmail, rIsAdmin);
                    user.UserId = rUserId;
                }
            }
            return user;
        }
        public User FindUserByEmail(string email)
        {
            User user = null;
            int rUserId = 0;
            string rName = "";
            string rEmail = "";
            string rPassword = "";
            bool rIsAdmin = false;

            using (DbCommand cmd = CreateSearchUserByEmailCmd(email))
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    rUserId = rDr.GetInt32(0);
                    rEmail = rDr.GetString(1);
                    rPassword = rDr.GetString(2);
                    rIsAdmin = rDr.GetBoolean(3);
                    rName = rDr.GetString(4);
                    user = new User(rName, rPassword, rEmail, rIsAdmin);
                    user.UserId = rUserId;
                }
            }
            return user;
        }
        public List<KeyValuePair<string, bool>> GetAllEMails()
        {
            List<KeyValuePair<string, bool>> result = new List<KeyValuePair<string, bool>>();
            string rEmail = "";
            bool rIsAdmin = false;

            using (DbCommand cmd = CreateGetAllEMailsCmd())
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    rEmail = rDr.GetString(0);
                    rIsAdmin = rDr.GetBoolean(1);
                    result.Add(new KeyValuePair<string, bool>(rEmail, rIsAdmin));
                }
            }
            return result;
        }
    }
}
