using CrowdDJ.Interfaces;
using CrowdDJ.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.Common;
using System.Data;
using System.Collections.ObjectModel;

namespace CrowdDJ.DAO
{
    public class GuestDAO : IGuest
    {
        private IDataBase database;

        const string CmdInsert = @"INSERT INTO [dbo].[Guest] (userId, partyId) VALUES (@pUserId, @pPartyId)";
        const string CmdDelete = @"DELETE FROM [dbo].[Guest] WHERE userId = @pUserId AND partyId = @pPartyId";
        const string CmdSearch = @"SELECT COUNT(*) FROM [dbo].[Guest] WHERE userId = @pUserId AND partyId = @pPartyId";
        const string CmdSearchGuestlist = @"SELECT u.userId, u.name, u.email, u.isAdmin FROM [dbo].[Guest] g, [dbo].[USER] u WHERE u.userId = g.userId AND g.partyId = @pPartyId";
        const string CmdSearchParties = @"SELECT p.partyId, p.name, p.location, p.host FROM [dbo].[Guest] g, [dbo].[PARTY] p WHERE p.partyId = g.partyId AND g.userId = @pUserId";

        public GuestDAO(IDataBase database)
        {
            this.database = database;
        }
        #region private CreateCommand methods
        private DbCommand CreateInsertCmd(Guest newGuest)
        {
            DbCommand cmd = database.CreateCommand(CmdInsert);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, newGuest.UserId);
            database.DefineParameter(cmd, "@pPartyId", DbType.String, newGuest.PartyId);
            return cmd;
        }
        private DbCommand CreateDeleteCmd(Guest removeGuest)
        {
            DbCommand cmd = database.CreateCommand(CmdDelete);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, removeGuest.UserId);
            database.DefineParameter(cmd, "@pPartyId", DbType.String, removeGuest.PartyId);
            return cmd;
        }
        private DbCommand CreateSearchCmd(int guestID, string partyID)
        {
            DbCommand cmd = database.CreateCommand(CmdSearch);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, guestID);
            database.DefineParameter(cmd, "@pPartyId", DbType.String, partyID);
            return cmd;
        }
        private DbCommand CreateSearchGuestListCmd(string partyID)
        {
            DbCommand cmd = database.CreateCommand(CmdSearchGuestlist);
            database.DefineParameter(cmd, "@pPartyId", DbType.String, partyID);
            return cmd;
        }
        private DbCommand CreateSearchPartiesForUserCmd(int userID)
        {
            DbCommand cmd = database.CreateCommand(CmdSearchParties);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, userID);
            return cmd;
        }
        #endregion

        public bool AddGuest(Guest newGuest)
        {
            if (newGuest.PartyId != null && newGuest.UserId != null)
            {
                try
                {
                    using (DbCommand cmd = CreateInsertCmd(newGuest))
                    {
                        return database.ExecuteNonQuery(cmd) == 1;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool RemoveGuest(Guest removeGuest)
        {
            if (removeGuest.UserId != null && removeGuest.PartyId != null)
            {
                try
                {
                    using (DbCommand cmd = CreateDeleteCmd(removeGuest))
                    {
                        return database.ExecuteNonQuery(cmd) == 1;
                    }
                }
                catch (Exception)
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public bool PartyIsVisitedByGuest(int searchGuestId, string partyId)
        {
            if (searchGuestId != null && partyId != null && partyId != "")
            {
                int i = 0;
                using (DbCommand cmd = CreateSearchCmd(searchGuestId, partyId))
                using (IDataReader rDr = database.ExecuteReader(cmd))
                {
                    while (rDr.Read())
                    {
                        i = rDr.GetInt32(0);
                    }
                    return i > 0;
                }
            }
            else
            {
                return false;
            }

        }

        public ObservableCollection<User> GetGuestlistForParty(string partyId)
        {
            ObservableCollection<User> result = new ObservableCollection<User>();
            if (partyId != null && partyId != "")
            {
                User user = null;
                int rUserId = 0;
                string rName = "";
                string rEmail = "";
                bool rIsAdmin = false;

                using (DbCommand cmd = CreateSearchGuestListCmd(partyId))
                using (IDataReader rDr = database.ExecuteReader(cmd))
                {
                    while (rDr.Read())
                    {
                        rUserId = rDr.GetInt32(0);
                        rName = rDr.GetString(1);
                        rEmail = rDr.GetString(2);
                        rIsAdmin = rDr.GetBoolean(3);

                        user = new User(rName, "", rEmail, rIsAdmin);
                        user.UserId = rUserId;

                        result.Add(user);
                    }
                }
            }
            return result;
        }


        public ObservableCollection<Party> GetAttemptedPartyList(int userId)
        {
            ObservableCollection<Party> result = new ObservableCollection<Party>();
            if (userId != null && userId > 0)
            {
                Party party = null;
                string rPartyId = "";
                string rName = "";
                string rLocation = "";
                string rHost = "";

                using (DbCommand cmd = CreateSearchPartiesForUserCmd(userId))
                using (IDataReader rDr = database.ExecuteReader(cmd))
                {
                    while (rDr.Read())
                    {
                        rPartyId = rDr.GetString(0);
                        rName = rDr.GetString(1);
                        rLocation = rDr.GetString(2);
                        rHost = rDr.GetString(3);

                        party = new Party(rPartyId, rName, rLocation, rHost, "", "", false);

                        result.Add(party);
                    }
                }
            }
            return result;
        }
    }
}
