using CrowdDJ.DomainClasses;
using CrowdDJ.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.DAO
{
    public class TracklistDAO : ITracklist
    {
        const string CmdInsertIntoTracklist = @"INSERT INTO [dbo].[Tracklist] (playlistId, userId, trackId) VALUES (@pPlaylistId, @pUserId, @pTrackId)";
        const string CmdGetTracksRecommendedByUser = @"SELECT t.trackId, t.title, t.artist, t.url, t.genre, t.isVideo 
                                                       FROM [dbo].[Tracklist] tl, [dbo].[Track] t
                                                       WHERE tl.userId = @pUserId AND t.trackId = tl.trackId";
        const string CmdGetAllTracklists = @"SELECT * FROM [dbo].[Tracklist]";
        const string CmdFindTrackInTracklist = @"SELECT * FROM [dbo].[Tracklist] WHERE trackId = @pTrackId AND
                                                          playlistId = @pPlaylistId AND userId = @pUserId";

        private IDataBase database;

        public TracklistDAO(IDataBase database)
        {
            this.database = database;
        }

        #region private CreateCommand methods
        private DbCommand CreateInsertIntoTracklistCmd(Tracklist insertIntoTracklist)
        {
            DbCommand cmd = database.CreateCommand(CmdInsertIntoTracklist);
            database.DefineParameter(cmd, "@pPlaylistId", DbType.Int32, insertIntoTracklist.PlaylistId);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, insertIntoTracklist.UserId);
            database.DefineParameter(cmd, "@pTrackId", DbType.Int32, insertIntoTracklist.TrackId);
            return cmd;
        }
        private DbCommand CreateGetTracksRecommendedByUserCmd(int userId){
            DbCommand cmd = database.CreateCommand(CmdGetTracksRecommendedByUser);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, userId);
            return cmd;
        }
        private DbCommand CreateGetAllTracklistsCmd()
        {
            DbCommand cmd = database.CreateCommand(CmdGetAllTracklists);
            return cmd;
        }
        private DbCommand CreateFindTrackInTracklistCmd(Tracklist tracklist)
        {
            DbCommand cmd = database.CreateCommand(CmdFindTrackInTracklist);
            database.DefineParameter(cmd, "@pTrackId", DbType.String, tracklist.TrackId);
            database.DefineParameter(cmd, "@pPlaylistId", DbType.Int32, tracklist.PlaylistId);
            database.DefineParameter(cmd, "@pUserId", DbType.Int32, tracklist.UserId);
            return cmd;
        }
        #endregion

        public Tracklist FindTrackInTracklist(Tracklist tracklist)
        {
            Tracklist tl = null;
            int rUserId = 0;
            int rPlaylistId = 0;
            int rTrackId = 0;

            using (DbCommand cmd = CreateFindTrackInTracklistCmd(tracklist))
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    rPlaylistId = rDr.GetInt32(0);
                    rUserId = rDr.GetInt32(1);
                    rTrackId = rDr.GetInt32(2);
                    tl = new Tracklist(rPlaylistId, rUserId, rTrackId);
                }
            }
            return tl;
        }

        public bool InsertIntoTracklist(Tracklist insertIntoTracklist)
        {
            try
            {
                if (FindTrackInTracklist(insertIntoTracklist) == null)
                {
                    using (DbCommand cmd = CreateInsertIntoTracklistCmd(insertIntoTracklist))
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

        public ObservableCollection<Track> GetTracksRecommendedByUser(int userId)
        {
            ObservableCollection<Track> result = new ObservableCollection<Track>();
            Track track = null;
            int rTrackId = 0;
            string rTitle = "";
            string rArtist = "";
            string rUrl = "";
            string rGenre = "";
            bool rIsVideo = false;

            using (DbCommand cmd = CreateGetTracksRecommendedByUserCmd(userId))
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    rTrackId = rDr.GetInt32(0);
                    rTitle = rDr.GetString(1);
                    rArtist = rDr.GetString(2);
                    rUrl = rDr.GetString(3);
                    rGenre = rDr.GetString(4);
                    rIsVideo = rDr.GetBoolean(5);
                    track = new Track(rTitle, rArtist, rUrl, rGenre, rIsVideo);
                    track.TrackId = rTrackId;

                    result.Add(track);
                }
            }
            return result;
        }

        public ObservableCollection<Tracklist> GetAllTracklists()
        {
            ObservableCollection<Tracklist> result = new ObservableCollection<Tracklist>();
            Tracklist tl = null;
            int rUserId = 0;
            int rPlaylistId = 0;
            int rTrackId = 0;

            using (DbCommand cmd = CreateGetAllTracklistsCmd())
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    rUserId = rDr.GetInt32(1);
                    rPlaylistId = rDr.GetInt32(0);
                    rTrackId = rDr.GetInt32(2);
                    tl = new Tracklist(rPlaylistId, rUserId, rTrackId);
                    result.Add(tl);
                }
            }
            return result;
        }
    }
}
