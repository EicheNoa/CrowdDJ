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

namespace CrowdDJ.DAO
{
    public class VoteDAO : IVote
    {
        const string CmdInsertVote = @"INSERT INTO [dbo].[Vote] (userId, playlistId, trackId, TS_created) 
                                          VALUES (@pUserId, @pPlaylistId, @pTrackId, @pTS_created)";
        const string CmdAlreadyVotedForTrack = @"SELECT * FROM [dbo].[Vote] WHERE userId = @pUserId 
                                                    AND trackId = @pTrackId AND playlistId = @pPlaylistId";
        const string CmdGetVotesForTrack = @"SELECT count(*) FROM [dbo].[Vote] WHERE trackId = @pTrackId AND playlistId = @pPlaylistId";

        private IDataBase database;

        public VoteDAO(IDataBase database)
        {
            this.database = database;
        }
        #region private CreateCommand methods
        private DbCommand CreateInsertVoteCmd(Vote newVote)
        {
            DbCommand cmd = database.CreateCommand(CmdInsertVote);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, newVote.UserId);
            database.DefineParameter(cmd, "@pPlaylistId", DbType.Int32, newVote.PlaylistId);
            database.DefineParameter(cmd, "@pTrackId", DbType.Int32, newVote.TrackId);
            database.DefineParameter(cmd, "@pTS_created", DbType.String, newVote.TS_created);
            return cmd;
        }
        private DbCommand CreateAlreadyVotedForTrack(int userId, int trackId, int playlistId)
        {
            DbCommand cmd = database.CreateCommand(CmdAlreadyVotedForTrack);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, userId);
            database.DefineParameter(cmd, "@pTrackId", DbType.Int32, trackId);
            database.DefineParameter(cmd, "@pPlaylistId", DbType.Int32, playlistId);
            return cmd;
        }
        private DbCommand CreateGetVotesForTrackCmd(int trackId, int playlistId)
        {
            DbCommand cmd = database.CreateCommand(CmdGetVotesForTrack);
            database.DefineParameter(cmd, "@pTrackId", DbType.Int32, trackId);
            database.DefineParameter(cmd, "@pPlaylistId", DbType.Int32, playlistId);
            return cmd;
        }
        #endregion
        public bool InsertVote(Vote newVote)
        {
            if (!AlreadyVotedForTrack(newVote.TrackId, newVote.UserId, newVote.PlaylistId))
            {
                using (DbCommand cmd = CreateInsertVoteCmd(newVote))
                {
                    return database.ExecuteNonQuery(cmd) == 1;
                }
            }
            else
            {
                return false;
            }
        }

        public bool AlreadyVotedForTrack(int userId, int trackId, int playlistId)
        {
            using (DbCommand cmd = CreateAlreadyVotedForTrack(userId, trackId, playlistId))
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    if (!rDr.IsDBNull(0))
                        return true;
                    else
                        return false;
                }
                return false;
            }
        }

        public int GetVotesForTrack(int trackId, int playlistId)
        {
            int result = 0;
            using (DbCommand cmd = CreateGetVotesForTrackCmd(trackId, playlistId))
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    result = rDr.GetInt32(0);
                }
                return result;
            }
        }
    }
}
