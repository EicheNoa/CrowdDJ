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
    public class PartyDAO : IParty
    {
        const string CmdInsert = @"INSERT INTO [dbo].[Party] (partyId, name, location, host, partyBegin, partyEnd, isActive) 
                                          VALUES (@pPartyId, @pName, @pLocation, @pHost, @pBegin, @pEnd, @pIsActive)";
        const string CmdDelete = @"DELETE FROM [dbo].[Party] WHERE partyId = @pPartyId";
        const string CmdUpdate = @"UPDATE [dbo].[Party] SET name = @pName, location = @pLocation, host = @pHost,
                                                            partyBegin = @pBegin, partyEnd = @pEnd, isActive = @pIsActive
                                          WHERE partyId = @pPartyId";
        const string CmdSearch = @"SELECT * FROM [dbo].[Party] WHERE partyId = @pPartyId";
        const string CmdSearchHost = @"SELECT * FROM [dbo].[Party] WHERE host = @pHost";
        const string CmdSelectAll = @"SELECT * FROM [dbo].[Party]";

        private IDataBase database;

        public PartyDAO(IDataBase database)
        {
            this.database = database;
        }
        
        #region private CreateCmd methods
        private DbCommand CreateInsertCmd(Party newParty)
        {
            DbCommand cmd = database.CreateCommand(CmdInsert);
            database.DefineParameter(cmd, "@pPartyId", DbType.String, newParty.PartyId);
            database.DefineParameter(cmd, "@pName", DbType.String, newParty.Name);
            database.DefineParameter(cmd, "@pLocation", DbType.String, newParty.Location);
            database.DefineParameter(cmd, "@pHost", DbType.String, newParty.Host);
            database.DefineParameter(cmd, "@pBegin", DbType.String, newParty.PartyBegin);
            database.DefineParameter(cmd, "@pEnd", DbType.String, newParty.PartyEnd);
            database.DefineParameter(cmd, "@pIsActive", DbType.String, newParty.IsActive);
            return cmd;
        }
        private DbCommand CreateDeleteCmd(string partyID)
        {
            DbCommand cmd = database.CreateCommand(CmdDelete);
            database.DefineParameter(cmd, "@pPartyId", DbType.String, partyID);
            return cmd;
        }
        private DbCommand CreateUpdateCmd(Party party, string partyID)
        {
            DbCommand cmd = database.CreateCommand(CmdUpdate);
            database.DefineParameter(cmd, "@pName", DbType.String, party.Name);
            database.DefineParameter(cmd, "@pLocation", DbType.String, party.Location);
            database.DefineParameter(cmd, "@pHost", DbType.String, party.Host);
            database.DefineParameter(cmd, "@pBegin", DbType.String, party.PartyBegin);
            database.DefineParameter(cmd, "@pEnd", DbType.String, party.PartyEnd);
            database.DefineParameter(cmd, "@pIsActive", DbType.String, party.IsActive);
            database.DefineParameter(cmd, "@pPartyId", DbType.String, partyID);
            return cmd;
        }
        private DbCommand CreateSearchCmd(string partyID)
        {
            DbCommand cmd = database.CreateCommand(CmdSearch);
            database.DefineParameter(cmd, "@pPartyId", DbType.String, partyID);
            return cmd;
        }
        private DbCommand CreateSearchHostCmd(string hostName)
        {
            DbCommand cmd = database.CreateCommand(CmdSearchHost);
            database.DefineParameter(cmd, "@pHost", DbType.String, hostName);
            return cmd;
        }
        private DbCommand CreateSearchAllCmd()
        {
            DbCommand cmd = database.CreateCommand(CmdSelectAll);
            return cmd;
        }
        #endregion

        #region public methods
        public bool AddParty(Party newParty)
        {
            try
            {
                using (DbCommand cmd = CreateInsertCmd(newParty))
                {
                    return database.ExecuteNonQuery(cmd) == 1;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateParty(Party party, string id)
        {
            try
            {
                using (DbCommand cmd = CreateUpdateCmd(party, id))
                {
                    return database.ExecuteNonQuery(cmd) == 1;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemovePartyWithId(String partyId)
        {
            try
            {
                using (DbCommand cmd = CreateDeleteCmd(partyId))
                {
                    return database.ExecuteNonQuery(cmd) == 1;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Party FindPartyById(string partyId)
        {
            Party party= null;
            string rPartyId = "";
            string rName = "";
            string rLocation = "";
            string rHost = "";
            string rBegin;
            string rEnd;
            bool rIsActive = false;

            using (DbCommand cmd = CreateSearchCmd(partyId))
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    rPartyId = rDr.GetString(0);
                    rName = rDr.GetString(1);
                    rLocation = rDr.GetString(2);
                    rHost = rDr.GetString(3);
                    rBegin = rDr.GetString(4);
                    rEnd = rDr.GetString(5);
                    rIsActive = rDr.GetBoolean(6);
                    party = new Party(rPartyId, rName, rLocation, rHost, rBegin, rEnd, rIsActive);
                }
            }
            return party;
        }

        public ObservableCollection<Party> FindPartyWithHost(string hostName)
        {
            ObservableCollection<Party> result = new ObservableCollection<Party>();
            Party party = null;
            string rPartyId ="";
            string rName = "";
            string rLocation = "";
            string rHost = "";
            string rBegin;
            string rEnd;
            bool rIsActive = false;

            using (DbCommand cmd = CreateSearchHostCmd(hostName))
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    if (!rDr.IsDBNull(0))
                    {
                        rPartyId = rDr.GetString(0);
                        rName = rDr.GetString(1);
                        rLocation = rDr.GetString(2);
                        rHost = rDr.GetString(3);
                        rBegin = rDr.GetString(4);
                        rEnd = rDr.GetString(5);
                        rIsActive = rDr.GetBoolean(6);
                        party = new Party(rPartyId, rName, rLocation, rHost, rBegin, rEnd, rIsActive);
                    }
                    result.Add(party);
                }
            }
            return result;
        }

        public ObservableCollection<Party> GetAllParties()
        {
            ObservableCollection<Party> result = new ObservableCollection<Party>();
            Party party = null;
            string rPartyId = "";
            string rName = "";
            string rLocation = "";
            string rHost = "";
            string rBegin;
            string rEnd;
            bool rIsActive = false;

            using (DbCommand cmd = CreateSearchAllCmd())
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    if (!rDr.IsDBNull(0))
                    {
                        rPartyId = rDr.GetString(0);
                        rName = rDr.GetString(1);
                        rLocation = rDr.GetString(2);
                        rHost = rDr.GetString(3);
                        rBegin = rDr.GetString(4);
                        rEnd = rDr.GetString(5);
                        rIsActive = rDr.GetBoolean(6);
                        party = new Party(rPartyId, rName, rLocation, rHost, rBegin, rEnd, rIsActive);
                    }
                    result.Add(party);
                }
            }
            return result;
        }
        #endregion
    }
}
