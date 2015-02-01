using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CrowdDJ.BL;
using CrowdDJ.DomainClasses;
using System.Diagnostics;
using System.Collections.ObjectModel;

namespace CrowdDJ.UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void UserTestSuite()
        {
            ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();
            User user = new User("Peter von der Alm", "hallo", "peter@hausen.de", false);
            User secUser = new User("Heidi von der Wiese", "yoyo", "almdirndl@alm.at", true);
            Assert.IsTrue(bl.InsertUser(user));
            Assert.IsFalse(bl.InsertUser(user));
            Assert.IsTrue(bl.InsertUser(secUser));
            Assert.IsFalse(bl.InsertUser(secUser));
            user = bl.FindUserByEmail(user.Email);
            Assert.IsTrue(bl.DeleteUser(user.UserId));
            Assert.IsTrue(bl.InsertUser(user));
            user = bl.FindUserByEmail(user.Email);
            user.Email = "almrocker@alm.at";
            Assert.IsTrue(bl.UpdateUser(user, user.UserId));
            user = null;
            secUser = null;
            user = bl.FindUserByEmail("almrocker@alm.at");
            secUser = bl.FindUserByEmail("almdirndl@alm.at");
            Assert.IsTrue(user.Email.Equals("almrocker@alm.at"));
            Assert.IsTrue(bl.DeleteUser(user.UserId));
            Assert.IsTrue(bl.DeleteUser(secUser.UserId));
            Assert.IsTrue(bl.GetGuestlistForParty("Party numero 2").Count > 0);
            Assert.IsTrue(bl.GetAttemptedPartyList(1).Count > 0);
        }
        [TestMethod]
        public void PartyTestSuite()
        {
            ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();
            Party party = new Party("Meine Party", "Geburtstagsfeier von Hause", "Hagenberg", "Peter",
                DateTime.Now.ToString(), DateTime.Now.ToString(), true);
            Assert.IsTrue(bl.AddParty(party));
            party = new Party("Meine Party 2", "Geburtstagsfeier von Hause", "Hagenberg", "Peter",
                DateTime.Now.ToString(), DateTime.Now.ToString(), true);
            Assert.IsTrue(bl.AddParty(party));
            party = new Party("Meine Party 3", "Geburtstagsfeier von Hause", "Hagenberg", "Peter",
                DateTime.Now.ToString(), DateTime.Now.ToString(), true);
            Assert.IsTrue(bl.AddParty(party));
            party.IsActive = false;
            Assert.IsTrue(bl.UpdateParty(party, party.PartyId));
            Party secondP = bl.FindPartyById(party.PartyId);
            Assert.IsTrue(secondP.PartyId == party.PartyId && secondP.Name == party.Name);
            Assert.IsTrue(bl.FindPartyWithHost("Peter").Count == 3);
            Assert.IsTrue(bl.GetAllParties().Count > 0);
            Assert.IsTrue(bl.RemovePartyWithId("Meine Party"));
            Assert.IsTrue(bl.RemovePartyWithId("Meine Party 2"));
            Assert.IsTrue(bl.RemovePartyWithId("Meine Party 3"));
        }
        [TestMethod]
        public void PartyTweetTestSuite()
        {
            ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();
            User user = new User("Peter von der Alm", "hallo", "peter@hausen.de", false);
            Assert.IsTrue(bl.InsertUser(user));
            //user = bl.FindUserByEmail(user.Email);
            //bl.DeleteUser(user.UserId);
            //Assert.IsTrue(bl.InsertUser(user));
            user = bl.FindUserByEmail(user.Email);
            Party party = new Party("Meine Party", "Geburtstagsfeier von Hause", "Hagenberg", "Peter",
                DateTime.Now.ToString(), DateTime.Now.ToString(), true);
            Assert.IsTrue(bl.AddParty(party));
            Partytweet pt = new Partytweet(bl.FindUserByEmail(user.Email).UserId, party.PartyId, "Fetzig");
            Assert.IsTrue(bl.AddTweet(pt));
            Assert.IsTrue(bl.GetTweetsForUser(user.UserId).Count == 1);
            Assert.IsTrue(bl.DeleteUser(bl.FindUserByEmail(user.Email).UserId));
            Assert.IsTrue(bl.RemovePartyWithId("Meine Party"));
        }
        [TestMethod]
        public void PlaylistTestSuit()
        {
            ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();
            Party party = new Party("Meine Party", "Geburtstagsfeier von Hause", "Hagenberg", "Peter",
                DateTime.Now.ToString(), DateTime.Now.ToString(), true);
            Assert.IsTrue(bl.AddParty(party));
            Playlist pl = new Playlist(party.PartyId, party.Name + " Feier!!");
            Assert.IsTrue(bl.InsertPlaylist(pl));
            Assert.IsTrue(bl.GetAllPlaylists().Count > 0);
            Assert.IsTrue(bl.RemovePartyWithId("Meine Party"));
        }
        [TestMethod]
        public void TrackTestSuite()
        {
            ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();
            Track track = new Track("Hail Mary", "My Dead Fathers", @"https://www.youtube.com/watch?v=9nMZo0M36ZQ", "Death Metal", true);
            Assert.IsTrue(bl.InsertTrack(track));
            ObservableCollection<Track> tc;
            string s = track.Title;
            track = null;
            tc = bl.FindTrackWithTitle(s);
            Assert.IsTrue(tc.Count == 1);
            track = tc[0];
            Assert.IsTrue(track.Title == s);
            Assert.IsTrue(bl.FindVideos().Count > 0);
            Assert.IsTrue(bl.FindSongs().Count > 0);
            Assert.IsTrue(bl.RemoveTrackWithId(track.TrackId));
        }
        [TestMethod]
        public void TracklistTestSuite()
        {
            ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();
            User user = new User("Peter von der Alm", "hallo", "peter@hausen.de", false);
            Assert.IsTrue(bl.InsertUser(user));
            user = bl.FindUserByEmail(user.Email);
            Party party = new Party("Meine Party", "Geburtstagsfeier von Hause", "Hagenberg", "Peter",
                DateTime.Now.ToString(), DateTime.Now.ToString(), true);
            Assert.IsTrue(bl.AddParty(party));
            Playlist pl = new Playlist(party.PartyId, party.Name + " Feier!!");
            Assert.IsTrue(bl.InsertPlaylist(pl));
            Track track = new Track("Hail Mary", "My Dead Fathers", @"https://www.youtube.com/watch?v=9nMZo0M36ZQ", "Death Metal", true);
            Assert.IsTrue(bl.InsertTrack(track));
            ObservableCollection<Track> tc = bl.FindTrackWithTitle(track.Title);
            track = tc[0];
            Tracklist tl = new Tracklist(bl.GetPlaylistForParty("Meine Party").PlaylistId, 
                                         bl.FindUserByEmail(user.Email).UserId, 
                                         track.TrackId);

            Assert.IsTrue(bl.DeleteUser(user.UserId));
            Assert.IsTrue(bl.RemoveTrackWithId(track.TrackId));
            Assert.IsTrue(bl.RemovePartyWithId("Meine Party"));
        }
        [TestMethod]
        public void VoteTestSuite()
        {
            ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();
            Track track = new Track("Hail Mary", "My Dead Fathers", @"https://www.youtube.com/watch?v=9nMZo0M36ZQ", "Death Metal", true);
            Assert.IsTrue(bl.InsertTrack(track));
            ObservableCollection<Track> tc = bl.FindTrackWithTitle(track.Title);
            track = tc[0];
            
            Party party = new Party("Meine Party", "Geburtstagsfeier von Hause", "Hagenberg", "Peter",
                DateTime.Now.ToString(), DateTime.Now.ToString(), true);
            Assert.IsTrue(bl.AddParty(party));
            Playlist pl = new Playlist(party.PartyId, party.Name + " Feier!!");
            Assert.IsTrue(bl.InsertPlaylist(pl));
            pl = bl.GetPlaylistForParty("Meine Party");
            User user = new User("Peter von der Alm", "hallo", "peter@hausen.de", false);
            Assert.IsTrue(bl.InsertUser(user));
            user = bl.FindUserByEmail(user.Email);

            Vote vote = new Vote(user.UserId, pl.PlaylistId, track.TrackId, DateTime.Now.ToString());
            Assert.IsTrue(bl.InsertVote(vote));
            Assert.IsTrue(bl.AlreadyVotedForTrack(user.UserId, track.TrackId, pl.PlaylistId));
            Assert.IsTrue(bl.GetVotesForTrack(track, pl.PlaylistId) == 1);
            
            Assert.IsTrue(bl.RemovePartyWithId("Meine Party"));
            Assert.IsTrue(bl.RemoveTrackWithId(track.TrackId));
            Assert.IsTrue(bl.DeleteUser(user.UserId));
        }
        [TestMethod]
        public void GuestTestSuit()
        {
            ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();
            Party party = new Party("Meine Party", "Geburtstagsfeier von Hause", "Hagenberg", "Peter",
                DateTime.Now.ToString(), DateTime.Now.ToString(), true);
            Assert.IsTrue(bl.AddParty(party));
            User user = new User("Peter von der Alm", "hallo", "peter@hausen.de", false);
            Assert.IsTrue(bl.InsertUser(user));
            user = bl.FindUserByEmail(user.Email);

            Guest guest = new Guest(user.UserId, party.PartyId);
            Assert.IsTrue(bl.AddGuest(guest));
            Assert.IsTrue(bl.PartyIsVisitedByGuest(user.UserId, party.PartyId));
            Assert.IsFalse(bl.PartyIsVisitedByGuest(-1, party.PartyId));
            Assert.IsTrue(bl.GetGuestlistForParty(party.PartyId).Count > 0);
            Assert.IsTrue(bl.RemoveGuest(guest));

            Assert.IsTrue(bl.RemovePartyWithId("Meine Party"));
            Assert.IsTrue(bl.DeleteUser(user.UserId));
        }
    }
}
