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
    public class PlaylistDAO : IPlaylist
    {
        const string CmdInsertPlaylist = @"INSERT INTO [dbo].[Playlist] (partyId, name) VALUES (@pPartyId, @pName)";
        const string CmdDeletePlaylist = @"DELETE FROM [dbo].[Playlist] WHERE playlistId = @pPlaylistId";
        const string CmdUpdatePlaylist = @"UPDATE [dbo].[Playlist] SET playlistId = @pNewPlaylistId, name = @pName
                                          WHERE playlistId = @pOldPlaylistId";
        const string CmdGetPlaylistForParty = @"SELECT * FROM [dbo].[Playlist] WHERE partyId = @pPartyId";
        const string CmdGetAllTracksInPlaylist = @"SELECT t.trackId, t.title, t.artist, t.url, t.length, t.genre, t.isVideo 
                                                   FROM [dbo].[Playlist] pl, [dbo].[Tracklist] tl, [dbo].[Track] t
                                                   WHERE pl.playlistId = @pPlaylistId AND 
                                                         pl.playlistId = tl.playlistId AND tl.trackId = t.trackId";
        const string CmdGetAllPlaylists = @"SELECT * FROM [dbo].[Playlist]";

        private IDataBase database;

        public PlaylistDAO(IDataBase database)
        {
            this.database = database;
        }
        #region private CreateCmd methods
        private DbCommand CreateInsertPlaylistCmd(Playlist newPlaylist)
        {
            DbCommand cmd = database.CreateCommand(CmdInsertPlaylist);
            database.DefineParameter(cmd, "@pPartyId", DbType.String, newPlaylist.PartyId);
            database.DefineParameter(cmd, "@pName", DbType.String, newPlaylist.Name);
            return cmd;
        }
        private DbCommand CreateUpdatePlaylistCmd(int oldId, Playlist updatePlaylist)
        {
            DbCommand cmd = database.CreateCommand(CmdUpdatePlaylist);
            database.DefineParameter(cmd, "@pNewPartyId", DbType.String, updatePlaylist.PartyId);
            database.DefineParameter(cmd, "@pName", DbType.String, updatePlaylist.Name);
            database.DefineParameter(cmd, "@pNewPartyId", DbType.String, oldId);
            return cmd;
        }
        private DbCommand CreateDeletePlaylistCmd(int deletePlaylistId)
        {
            DbCommand cmd = database.CreateCommand(CmdDeletePlaylist);
            database.DefineParameter(cmd, "@pPartyId", DbType.Int32, deletePlaylistId);
            return cmd;
        }
        private DbCommand CreateGetPlaylistForPartyCmd(string id)
        {
            DbCommand cmd = database.CreateCommand(CmdGetPlaylistForParty);
            database.DefineParameter(cmd, "@pPartyId", DbType.String, id);
            return cmd;
        }
        private DbCommand CreateGetAllTracksInPlaylistCmd(int id)
        {
            DbCommand cmd = database.CreateCommand(CmdGetAllTracksInPlaylist);
            database.DefineParameter(cmd, "@pPlaylistId", DbType.Int32, id);
            return cmd;
        }
        private DbCommand CreateGetAllPlaylistsCmd()
        {
            DbCommand cmd = database.CreateCommand(CmdGetAllPlaylists);
            return cmd;
        }
        #endregion

        #region public methods
        public bool InsertPlaylist(Playlist newPlaylist)
        {
            using (DbCommand cmd = CreateInsertPlaylistCmd(newPlaylist))
            {
                return database.ExecuteNonQuery(cmd) == 1;
            }
        }

        public bool DeletePlaylist(int deletePlaylistId)
        {
            using (DbCommand cmd = CreateDeletePlaylistCmd(deletePlaylistId))
            {
                return database.ExecuteNonQuery(cmd) == 1;
            }
        }

        public bool UpdatePlaylist(int oldId, Playlist updatedPlaylist)
        {
            using (DbCommand cmd = CreateUpdatePlaylistCmd(oldId, updatedPlaylist))
            {
                return database.ExecuteNonQuery(cmd) == 1;
            }
        }

        public Playlist GetPlaylistForParty(string id)
        {
            Playlist result = null;
            int rPlaylistId = 0;
            string rPartyId = "";
            string rName = "";
            
            using (DbCommand cmd = CreateGetPlaylistForPartyCmd(id))
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    rPlaylistId = rDr.GetInt32(0);
                    rPartyId = rDr.GetString(1);
                    rName = rDr.GetString(2);
                    result = new Playlist(rPartyId, rName);
                    result.PlaylistId = rPlaylistId;
                }
            }
            return result;
        }

        public ObservableCollection<Track> GetAllTracksInPlaylist(int playlistId)
        {
            ObservableCollection<Track> result = new ObservableCollection<Track>();
            Track track = null;
            int rTrackId = 0;
            string rTitle = "";
            string rArtist = "";
            string rUrl = "";
            int rLength = 0;
            string rGenre = "";
            bool rIsVideo = false;

            using (DbCommand cmd = CreateGetAllTracksInPlaylistCmd(playlistId))
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    rTrackId = rDr.GetInt32(0);
                    rTitle = rDr.GetString(1);
                    rArtist = rDr.GetString(2);
                    rUrl = rDr.GetString(3);
                    rLength = rDr.GetInt32(4);
                    rGenre = rDr.GetString(5);
                    rIsVideo = rDr.GetBoolean(6);
                    track = new Track(rTitle, rArtist, rUrl, rLength, rGenre, rIsVideo);
                    track.TrackId = rTrackId;
                    result.Add(track);
                }
            }
            return result;
        }

        public ObservableCollection<Playlist> GetAllPlaylists()
        {
            ObservableCollection<Playlist> result = new ObservableCollection<Playlist>();
            Playlist playlist = null;
            string rPartyId = "";
            string rName = "";

            using (DbCommand cmd = CreateGetAllPlaylistsCmd())
            using (IDataReader rDr = database.ExecuteReader(cmd))
            {
                while (rDr.Read())
                {
                    rPartyId = rDr.GetString(0);
                    rName = rDr.GetString(1);
                    playlist = new Playlist(rPartyId, rName);
                    result.Add(playlist);
                }
            }
            return result;
        }
        #endregion
    }
}
