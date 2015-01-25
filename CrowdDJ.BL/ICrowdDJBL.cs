using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrowdDJ.DomainClasses;
using System.Collections.ObjectModel;

namespace CrowdDJ.BL
{
    public interface ICrowdDJBL
    {
        #region Guest
        bool AddGuest(Guest newGuest);
        bool RemoveGuest(Guest removeGuest);
        bool PartyIsVisitedByGuest(int searchGuestId, string partyId);
        ObservableCollection<User> GetGuestlistForParty(string partyId); 
        ObservableCollection<Party> GetAttemptedPartyList(int userId);
        #endregion //Guest

        #region Party
        bool AddParty(Party newParty);
        bool RemovePartyWithId(string partyId);
        bool UpdateParty(Party party, string id);
        Party FindPartyById(string partyId);
        ObservableCollection<Party> FindPartyWithHost(string hostName);
        ObservableCollection<Party> GetAllParties();
        #endregion //Party

        #region Partytweet
        bool AddTweet(Partytweet newTweet);
        bool DeletePartytweet(Partytweet deletePartytweet);
        ObservableCollection<Partytweet> GetTweetsForParty(string partyId);
        ObservableCollection<Partytweet> GetTweetsForUser(int userId);
        ObservableCollection<Partytweet> GetAllTweets();
        #endregion //Partytweet

        #region Playlist
        bool InsertPlaylist(Playlist newPlaylist);
        bool DeletePlaylist(int id);
        bool UpdatePlaylist(int id, Playlist updatedPlaylist);
        Playlist GetPlaylistForParty(string id);
        ObservableCollection<Track> GetAllTracksForParty(string partyId);
        ObservableCollection<Playlist> GetAllPlaylists();
        #endregion //Playlist

        #region Track
        bool InsertTrack(Track newTrack);
        bool RemoveTrackWithId(int id);
        bool UpdateTrack(Track track, int id);
        Track FindTrackById(int id);
        ObservableCollection<Track> FindTrackWithTitle(string title);
        ObservableCollection<Track> FindTracksInGenre(string genre);
        ObservableCollection<Track> FindVideos();
        ObservableCollection<Track> FindSongs();
        ObservableCollection<Track> GetAllTracks();
        #endregion //Track

        #region Tracklist
        bool InsertIntoTracklist(Tracklist insertIntoTracklist);
        ObservableCollection<Track> GetTracksRecommendedByUser(int userId);
        ObservableCollection<Tracklist> GetAllTracklists();
        Tracklist FindTrackInTracklist(Tracklist tracklist);
        #endregion //Tracklist

        #region User
        bool InsertUser(User user);
        bool DeleteUser(int id);
        bool UpdateUser(User user, int id);
        ObservableCollection<User> GetAllUser();
        User FindUserById(int id);
        User FindUserByEmail(string email);
        List<KeyValuePair<string, bool>> GetAllEmails();
        #endregion //User

        #region Vote
        bool InsertVote(Vote newVote);
        bool AlreadyVotedForTrack(int userId, int trackId, int playlistId);
        int GetVotesForTrack(Track track, int playlist);
        ObservableCollection<Track> GetTracklistSortedVotes(int playlistId);
        #endregion //Vote
    }
}
