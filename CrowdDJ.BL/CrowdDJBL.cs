using CrowdDJ.DAL;
using CrowdDJ.DAO;
using CrowdDJ.Interfaces;
using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CrowdDJ.BL
{
    public class CrowdDJBL : ICrowdDJBL
    {
        private IDataBase database = null;
        private IGuest guest = null;
        private IParty party = null;
        private IPartytweet partytweet = null;
        private IPlaylist playlist = null;
        private ITrack track = null;
        private ITracklist tracklist = null;
        private IUser user = null;
        private IVote vote = null;

        public CrowdDJBL()
        {
            string s = ConfigurationManager.ConnectionStrings["CrowdDJ.Properties.Settings.CrowdDJDBConnectionString"].ConnectionString;
            database = new DataBase(s);
            guest = new GuestDAO(database);
            party = new PartyDAO(database);
            partytweet = new PartytweetDAO(database);
            playlist = new PlaylistDAO(database);
            track = new TrackDAO(database);
            tracklist = new TracklistDAO(database);
            user = new UserDAO(database);
            vote = new VoteDAO(database);
        }

        #region Guest
        public bool AddGuest(DomainClasses.Guest newGuest)
        {
            return guest.AddGuest(newGuest);
        }
        public bool RemoveGuest(DomainClasses.Guest removeGuest)
        {
            return guest.RemoveGuest(removeGuest);
        }
        public bool PartyIsVisitedByGuest(int searchGuestId, string partyId)
        {
            return guest.PartyIsVisitedByGuest(searchGuestId, partyId);
        }
        public ObservableCollection<DomainClasses.User> GetGuestlistForParty(string partyId)
        {
            return guest.GetGuestlistForParty(partyId);
        }
        public ObservableCollection<DomainClasses.Party> GetAttemptedPartyList(int userId)
        {
            return guest.GetAttemptedPartyList(userId);
        }
        #endregion

        #region Party
        public bool AddParty(DomainClasses.Party newParty)
        {
            return party.AddParty(newParty);
        }
        public bool RemovePartyWithId(string partyId)
        {
            return party.RemovePartyWithId(partyId);
        }
        public bool UpdateParty(DomainClasses.Party updateParty, string id)
        {
            return party.UpdateParty(updateParty, id);
        }
        public DomainClasses.Party FindPartyById(string partyId)
        {
            return party.FindPartyById(partyId);
        }
        public ObservableCollection<DomainClasses.Party> FindPartyWithHost(string hostName)
        {
            return party.FindPartyWithHost(hostName);
        }
        public ObservableCollection<DomainClasses.Party> GetAllParties()
        {
            return party.GetAllParties();
        }
        #endregion

        #region Partytweet
        public bool AddTweet(DomainClasses.Partytweet newTweet)
        {
            return partytweet.AddTweet(newTweet);
        }

        public bool DeletePartytweet(DomainClasses.Partytweet deletePartytweet)
        {
            return partytweet.DeletePartytweet(deletePartytweet);
        }

        public ObservableCollection<DomainClasses.Partytweet> GetTweetsForParty(string partyId)
        {
            return partytweet.GetTweetsForParty(partyId);
        }

        public ObservableCollection<DomainClasses.Partytweet> GetAllTweets()
        {
            return partytweet.GetAllTweets();
        }
        public ObservableCollection<DomainClasses.Partytweet> GetTweetsForUser(int userId)
        {
            return partytweet.GetTweetsForUser(userId);
        }
        #endregion

        #region Playlist
        public bool InsertPlaylist(DomainClasses.Playlist newPlaylist)
        {
            return playlist.InsertPlaylist(newPlaylist);
        }

        public bool DeletePlaylist(int id)
        {
            return playlist.DeletePlaylist(id);
        }

        public bool UpdatePlaylist(int id, DomainClasses.Playlist updatedPlaylist)
        {
            return playlist.UpdatePlaylist(id, updatedPlaylist);
        }

        public DomainClasses.Playlist GetPlaylistForParty(string id)
        {
            return playlist.GetPlaylistForParty(id);
        }

        public ObservableCollection<DomainClasses.Track> GetAllTracksInPlaylist(int playlistId)
        {
            return playlist.GetAllTracksInPlaylist(playlistId);
        }

        public ObservableCollection<DomainClasses.Playlist> GetAllPlaylists()
        {
            return playlist.GetAllPlaylists();
        }
        #endregion

        #region Track
        public bool InsertTrack(DomainClasses.Track newTrack)
        {
            return track.InsertTrack(newTrack);
        }

        public bool RemoveTrackWithId(int id)
        {
            return track.RemoveTrackWithId(id);
        }

        public bool UpdateTrack(DomainClasses.Track updateTrack, int id)
        {
            return track.UpdateTrack(updateTrack, id);
        }

        public DomainClasses.Track FindTrackById(int id)
        {
            return track.FindTrackById(id);
        }

        public ObservableCollection<DomainClasses.Track> FindTrackWithTitle(string title)
        {
            return track.FindTrackWithTitle(title);
        }

        public ObservableCollection<DomainClasses.Track> FindTracksInGenre(string genre)
        {
            return track.FindTracksInGenre(genre);
        }

        public ObservableCollection<DomainClasses.Track> FindVideos()
        {
            return track.FindVideos();
        }

        public ObservableCollection<DomainClasses.Track> FindSongs()
        {
            return track.FindSongs();
        }

        public ObservableCollection<DomainClasses.Track> GetAllTracks()
        {
            return track.GetAllTracks();
        }
        #endregion

        #region Tracklist
        public bool InsertIntoTracklist(DomainClasses.Tracklist insertIntoTracklist)
        {
            return tracklist.InsertIntoTracklist(insertIntoTracklist);
        }

        public ObservableCollection<DomainClasses.Track> GetTracksRecommendedByUser(int userId)
        {
            return tracklist.GetTracksRecommendedByUser(userId);
        }

        public ObservableCollection<DomainClasses.Tracklist> GetAllTracklists()
        {
            return tracklist.GetAllTracklists();
        }
        #endregion

        #region User
        public bool InsertUser(DomainClasses.User newUser)
        {
            return user.InsertUser(newUser);
        }

        public bool DeleteUser(int id)
        {
            return user.DeleteUser(id);
        }

        public bool UpdateUser(DomainClasses.User updateUser, int id)
        {
            return user.UpdateUser(updateUser, id);
        }

        public ObservableCollection<DomainClasses.User> GetAllUser()
        {
            return user.GetAllUser();
        }

        public DomainClasses.User FindUserById(int id)
        {
            return user.FindUserById(id);
        }
        public DomainClasses.User FindUserByEmail(string email)
        {
            return user.FindUserByEmail(email);
        }
        #endregion

        #region Vote
        public bool InsertVote(DomainClasses.Vote newVote)
        {
            return vote.InsertVote(newVote);
        }

        public bool AlreadyVotedForTrack(int userId, int trackId, int playlistId)
        {
            return vote.AlreadyVotedForTrack(userId, trackId, playlistId);
        }

        public int GetVotesForTrack(int trackId, int playlistId)
        {
            return vote.GetVotesForTrack(trackId, playlistId);
        }
        #endregion //Vote
    }
}
