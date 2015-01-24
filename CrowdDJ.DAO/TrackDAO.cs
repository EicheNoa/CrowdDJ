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
    public class TrackDAO : ITrack
    {
        const string CmdInsert = @"INSERT INTO [dbo].[Track] (title, artist, url, genre, isVideo) 
                                          VALUES (@pTitle, @pArtist, @pUrl, @pGenre, @pIsVideo)";
        const string CmdDelete = @"DELETE FROM [dbo].[Track] WHERE trackId = @pTrackId";
        const string CmdUpdate = @"UPDATE [dbo].[Track] SET title = @pTitle, artist = @pArtist, url = @pUrl,
                                                            genre = @pGenre, isVideo = @pIsVideo
                                    WHERE trackId = @pOldTrackId";
        const string CmdSearchTitle = @"SELECT * FROM [dbo].[Track] WHERE isVideo = false";
        const string CmdSearch = @"SELECT * FROM [dbo].[Track] WHERE trackId = @pTrackId";
        const string CmdSearchSongs = @"SELECT * FROM [dbo].[Track] WHERE isVideo = false";
        const string CmdSearchVideos = @"SELECT * FROM [dbo].[Track] WHERE isVideo = true";
        const string CmdSearchGenre = @"SELECT * FROM [dbo].[Track] WHERE genre = @pGenre";
        const string CmdGetAllTracks = @"SELECT * FROM [dbo].[Track]";

        private IDataBase database;

        public TrackDAO(IDataBase database)
        {
            this.database = database;
        }

        #region private CreateCommands methods
        private DbCommand CreateInsertCmd(Track newTrack)
        {
            DbCommand cmd = database.CreateCommand(CmdInsert);
            database.DefineParameter(cmd, "@pTitle", DbType.String, newTrack.Title);
            database.DefineParameter(cmd, "@pArtist", DbType.String, newTrack.Artist);
            database.DefineParameter(cmd, "@pUrl", DbType.String, newTrack.Url);
            database.DefineParameter(cmd, "@pGenre", DbType.String, newTrack.Genre);
            database.DefineParameter(cmd, "@pIsVideo", DbType.String, newTrack.IsVideo);
            return cmd;
        }
        private DbCommand CreateDeleteCmd(int deleteTrackId)
        {
            DbCommand cmd = database.CreateCommand(CmdDelete);
            database.DefineParameter(cmd, "@pTrackId", DbType.Int32, deleteTrackId);
            return cmd;
        }
        private DbCommand CreateUpdateCmd(Track updateTrack, int oldId)
        {
            DbCommand cmd = database.CreateCommand(CmdUpdate);
            database.DefineParameter(cmd, "@pTitle", DbType.String, updateTrack.Title);
            database.DefineParameter(cmd, "@pArtist", DbType.String, updateTrack.Artist);
            database.DefineParameter(cmd, "@pUrl", DbType.String, updateTrack.Url);
            database.DefineParameter(cmd, "@pGenre", DbType.String, updateTrack.Genre);
            database.DefineParameter(cmd, "@pIsVideo", DbType.String, updateTrack.IsVideo);
            database.DefineParameter(cmd, "@pOldTrackId", DbType.Int32, oldId);
            return cmd;
        }
        private DbCommand CreateSearchCmd(int trackId)
        {
            DbCommand cmd = database.CreateCommand(CmdSearch);
            database.DefineParameter(cmd, "@pTrackId", DbType.Int32, trackId);
            return cmd;
        }
        private DbCommand CreateSearchGenre(string trackGenre)
        {
            DbCommand cmd = database.CreateCommand(CmdSearchGenre);
            database.DefineParameter(cmd, "@pGenre",  DbType.String, trackGenre);
            return cmd;
        }
        private DbCommand CreateSearchSongs()
        {
            DbCommand cmd = database.CreateCommand(CmdSearchSongs);
            return cmd;
        }
        private DbCommand CreateSearchTitleCmd(string title)
        {
            DbCommand cmd = database.CreateCommand(CmdSearchTitle);
            database.DefineParameter(cmd, "@pTitle", DbType.String, title);
            return cmd;
        }
        private DbCommand CreateSearchVideosCmd()
        {
            DbCommand cmd = database.CreateCommand(CmdSearchVideos);
            return cmd;
        }
        private DbCommand CreateGetAllTracksCmd()
        {
            DbCommand cmd = database.CreateCommand(CmdGetAllTracks);
            return cmd;
        }
        #endregion

        public bool InsertTrack(Track newTrack)
        {
            try
            {
                using (DbCommand cmd = CreateInsertCmd(newTrack))
                {
                    return database.ExecuteNonQuery(cmd) == 1;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool RemoveTrackWithId(int deleteTrackId)
        {
            try
            {
                using (DbCommand cmd = CreateDeleteCmd(deleteTrackId))
                {
                    return database.ExecuteNonQuery(cmd) == 1;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool UpdateTrack(Track updatedTrack, int oldId)
        {
            try
            {
                using (DbCommand cmd = CreateUpdateCmd(updatedTrack, oldId))
                {
                    return database.ExecuteNonQuery(cmd) == 1;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public Track FindTrackById(int trackId)
        {
            Track track = null;
            int rTrackId = 0;
            string rTitle = "";
            string rArtist = "";
            string rUrl = "";
            string rGenre = "";
            bool rIsVideo = false;

            using (DbCommand cmd = CreateSearchCmd(trackId))
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
                 }
            }
            return track;
        }

        public ObservableCollection<Track> FindTrackWithTitle(string trackTitle)
        {
            ObservableCollection<Track> result = new ObservableCollection<Track>();
            Track track = null;
            int rTrackId = 0;
            string rTitle = "";
            string rArtist = "";
            string rUrl = "";
            string rGenre = "";
            bool rIsVideo = false;

            using (DbCommand cmd = CreateSearchTitleCmd(trackTitle))
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

        public ObservableCollection<Track> FindTracksInGenre(string trackGenre)
        {
            ObservableCollection<Track> result = new ObservableCollection<Track>();
            Track track = null;
            int rTrackId = 0;
            string rTitle = "";
            string rArtist = "";
            string rUrl = "";
            string rGenre = "";
            bool rIsVideo = false;

            using (DbCommand cmd = CreateSearchGenre(trackGenre))
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

        public ObservableCollection<Track> FindVideos()
        {
            ObservableCollection<Track> result = new ObservableCollection<Track>();
            Track track = null;
            int rTrackId = 0;
            string rTitle = "";
            string rArtist = "";
            string rUrl = "";
            string rGenre = "";
            bool rIsVideo = false;

            using (DbCommand cmd = CreateSearchVideosCmd())
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

        public ObservableCollection<Track> FindSongs()
        {
            ObservableCollection<Track> result = new ObservableCollection<Track>();
            Track track = null;
            int rTrackId = 0;
            string rTitle = "";
            string rArtist = "";
            string rUrl = "";
            string rGenre = "";
            bool rIsVideo = false;

            using (DbCommand cmd = CreateSearchSongs())
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

        public ObservableCollection<Track> GetAllTracks()
        {
            ObservableCollection<Track> result = new ObservableCollection<Track>();
            Track track = null;
            int rTrackId = 0;
            string rTitle = "";
            string rArtist = "";
            string rUrl = "";
            string rGenre = "";
            bool rIsVideo = false;

            using (DbCommand cmd = CreateGetAllTracksCmd())
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
    }
}
