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
    public class VoteDAO : IVote
    {
        const string CmdInsertVote = @"INSERT INTO [dbo].[Vote] (userId, playlistId, trackId, TS_created) 
                                          VALUES (@pUserId, @pPlaylistId, @pTrackId, @pTS_created)";
        const string CmdAlreadyVotedForTrack = @"SELECT * FROM [dbo].[Vote] WHERE userId = @pUserId 
                                                    AND trackId = @pTrackId AND playlistId = @pPlaylistId";
        const string CmdGetVotesForTrack = @"SELECT count(*) FROM [dbo].[Vote] WHERE trackId = @pTrackId 
                                                                            AND playlistId = @pPlaylistId";
        const string CmdGetTracklistSortedVotes = @"select t.trackId, t.title, t.artist, t.url, t.genre, t.isVideo,
                                                        (SELECT count(*) 
                                                         FROM [dbo].[Vote] v
                                                    	 WHERE t.trackId = v.trackId AND v.trackId = tl.trackId 
                                                                                     AND v.playlistId = tl.playlistId
                                                        ) as votes
                                                    from [dbo].[Track] t, [dbo].[Tracklist] tl 
                                                    where t.trackId = tl.trackId AND tl.playlistId = @pPlaylistId
                                                    order by votes desc";

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
        private DbCommand CreateGetTracklistSortedVotesCmd(int playlistId)
        {
            DbCommand cmd = database.CreateCommand(CmdGetTracklistSortedVotes);
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
            if (userId != null && userId > 0 && trackId != null && trackId > 0 && playlistId != null && playlistId > 0)
            {
                using (DbCommand cmd = CreateAlreadyVotedForTrack(userId, trackId, playlistId))
                using (IDataReader rDr = database.ExecuteReader(cmd))
                {
                    while (rDr.Read())
                    {
                        if (rDr.GetInt32(0) > 0)
                            return true;
                        else
                            return false;
                    }
                    return false;
                }
            }
            else
            {
                return false;
            }

        }

        public int GetVotesForTrack(Track track, int playlistId)
        {
            int result = -1;
            try
            {
                using (DbCommand cmd = CreateGetVotesForTrackCmd(track.TrackId, playlistId))
                using (IDataReader rDr = database.ExecuteReader(cmd))
                {
                    while (rDr.Read())
                    {
                        result = rDr.GetInt32(0);
                    }
                    return result;
                }
            }
            catch (Exception)
            {
                return result;
            }
        }


        public ObservableCollection<Track> GetTracklistSortedVotes(int playlistId)
        {
            ObservableCollection<Track> result = new ObservableCollection<Track>();
            Track track;
            try
            {
                using (DbCommand cmd = CreateGetTracklistSortedVotesCmd(playlistId))
                using (IDataReader rDr = database.ExecuteReader(cmd))
                {
                    while (rDr.Read())
                    {
                        track = new Track(rDr.GetString(1), rDr.GetString(2), rDr.GetString(3), rDr.GetString(4), rDr.GetBoolean(5));
                        track.TrackId = rDr.GetInt32(0);
                        track.NrVotes = rDr.GetInt32(6);
                        result.Add(track);
                    }
                    return result;
                }
            }
            catch (Exception)
            {
                return result;
            }
                
        }
    }
}
