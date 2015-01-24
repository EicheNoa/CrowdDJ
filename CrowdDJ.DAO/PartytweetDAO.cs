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
    public class PartytweetDAO : IPartytweet
    {
        const string CmdInsert = @"INSERT INTO [dbo].[Partytweet] (userId, partyId, message) VALUES (@pUserId, @pPartyId, @pMessage)";
        const string CmdDelete = @"DELETE FROM [dbo].[Partytweet] WHERE partyId = @pPartyId AND UserId = @pUserId";
        const string CmdGetTweetsForParty = @"SELECT * FROM [dbo].[Partytweet] WHERE partyId = @pPartyId";
        const string CmdGetTweetsForUser = @"SELECT * FROM [dbo].[Partytweet] WHERE userId = @pUserId";
        const string CmdGetAllTweets = @"SELECT * FROM [dbo].[Partytweet]";

        private IDataBase database;

        public PartytweetDAO(IDataBase database)
        {
            this.database = database;
        }

        #region private CreateCmd methods
        private DbCommand CreateInsertCmd(Partytweet newPartytweet)
        {
            DbCommand cmd = database.CreateCommand(CmdInsert);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, newPartytweet.UserId);
            database.DefineParameter(cmd, "@pPartyId", DbType.String, newPartytweet.PartyId);
            database.DefineParameter(cmd, "@pMessage", DbType.String, newPartytweet.Message);
            return cmd;
        }
        private DbCommand CreateDeleteCmd(Partytweet deletePartytweet)
        {
            DbCommand cmd = database.CreateCommand(CmdDelete);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, deletePartytweet.UserId);
            database.DefineParameter(cmd, "@pPartyId", DbType.String, deletePartytweet.PartyId);
            return cmd;
        }
        private DbCommand CreateGetTweetsForPartyCmd(string partyId)
        {
            DbCommand cmd = database.CreateCommand(CmdGetTweetsForParty);
            database.DefineParameter(cmd, "@pPartyId", DbType.String, partyId);
            return cmd;
        }
        private DbCommand CreateGetTweetsForUserCmd(int userId)
        {
            DbCommand cmd = database.CreateCommand(CmdGetTweetsForUser);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, userId);
            return cmd;
        }
        private DbCommand CreateGetAllTweetsCmd()
        {
            DbCommand cmd = database.CreateCommand(CmdGetAllTweets);
            return cmd;
        }
        #endregion

        #region public methods
        public bool AddTweet(Partytweet newPartytweet)
        {
            try
            {
                using (DbCommand cmd = CreateInsertCmd(newPartytweet))
                {
                    return database.ExecuteNonQuery(cmd) == 1;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public bool DeletePartytweet(Partytweet deletePartytweet)
        {
            try
            {
                using (DbCommand cmd = CreateDeleteCmd(deletePartytweet))
                {
                    return database.ExecuteNonQuery(cmd) == 1;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }
        public ObservableCollection<Partytweet> GetTweetsForParty(string partyId)
        {
            ObservableCollection<Partytweet> result = new ObservableCollection<Partytweet>();
            Partytweet partytweet = null;
            int rUserId = 0;
            string rPartyId = "";
            string rMessage = "";

            using (DbCommand cmd = CreateGetTweetsForPartyCmd(partyId))
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    rUserId = rDr.GetInt32(0);
                    rPartyId = rDr.GetString(1);
                    rMessage = rDr.GetString(2);
                    partytweet = new Partytweet(rUserId, rPartyId, rMessage);
                    result.Add(partytweet);
                }
            }
            return result;
        }
        public ObservableCollection<Partytweet> GetTweetsForUser(int userId)
        {
            ObservableCollection<Partytweet> result = new ObservableCollection<Partytweet>();
            Partytweet partytweet = null;
            int rUserId = 0;
            string rPartyId = "";
            string rMessage = "";

            using (DbCommand cmd = CreateGetTweetsForUserCmd(userId))
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    rUserId = rDr.GetInt32(0);
                    rPartyId = rDr.GetString(1);
                    rMessage = rDr.GetString(2);
                    partytweet = new Partytweet(rUserId, rPartyId, rMessage);
                    result.Add(partytweet);
                }
            }
            return result;
        }
        public ObservableCollection<Partytweet> GetAllTweets()
        {
            ObservableCollection<Partytweet> result = new ObservableCollection<Partytweet>();
            Partytweet partytweet = null;
            int rUserId = 0;
            string rPartyId = "";
            string rMessage = "";

            using (DbCommand cmd = CreateGetAllTweetsCmd())
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    rUserId = rDr.GetInt32(0);
                    rPartyId = rDr.GetString(1);
                    rMessage = rDr.GetString(2);
                    partytweet = new Partytweet(rUserId, rPartyId, rMessage);
                    result.Add(partytweet);
                }
            }
            return result;
        }
        #endregion
    }
}
