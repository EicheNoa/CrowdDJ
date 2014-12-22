﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CrowdDJ;
using CrowdDJ.BL;
using CrowdDJ.DomainClasses;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Configuration;

namespace CrowdDJ
{
    class Program
    {
        static void Main(string[] args)
        {
            ICrowdDJBL bl = new CrowdDJBL();
            User admin = new User("admin", "admin", "admin@admin.admin", true);
            bl.InsertUser(admin);
            int y = 0;
            int z = 0;
            Random random = new Random();
            bool r;
            User user = null;
            for (int i = 1; i < 1000; i++)
            {
                if (random.Next(0, 1) == 0)
                    r = true;
                else
                    r = false;
                user = new User("Peter der " + i + ".", i.ToString(), "ichBinnnnn" + i + "@dar.at", r);
                bl.InsertUser(user);
            }

            List<string> partylist = new List<string>();
            string pl;
            Party party = null;
            for (int i = 1; i < 1000; i++)
            {
                if (random.Next(0, 1) == 0)
                    r = true;
                else
                    r = false;
                pl = "Party numero " + i;
                partylist.Add(pl);
                party = new Party(pl, "Die " + i + ". wilde Hilde", "Am Berg " + i, "Susi von der " + i + ".",
                                        (i % 24).ToString() + ".00", (i % 24).ToString() + ".00", r);
                bl.AddParty(party);
            }

            Track track = null;
            for (int i = 5; i < 1000; i++)
            {
                if (random.Next(0, 1) == 0)
                    r = true;
                else
                    r = false;
                track = new Track("Bloodbath Massacre " + i, i.ToString(), "www." + i + ".com", (i % 300), "PoP", r);
                bl.InsertTrack(track);
            }

            Guest guest = null;
            for (int i = 13; i < 500; i++)
            {
                guest = new Guest(i, partylist[i]);
                bl.AddGuest(guest);
            }

            Partytweet partytweet = null;
            for (int i = 0; i < 230; i++)
            {
                partytweet = new Partytweet((i % 500) + 20, partylist[i], "Meine " + i + "-te Partey!");
                bl.AddTweet(partytweet);
            }

            Playlist playlist = null;
            for (int i = 100; i < 500; i++)
            {
                playlist = new Playlist(partylist[i], "Das Beste im " + i + "er Pack");
                bl.InsertPlaylist(playlist);
            }

            Tracklist tracklist = null;
            for (int i = 5; i < 10; i++)
            {
                Console.WriteLine(z++);
                tracklist = new Tracklist(i, i + 20, i + 20);
                bl.InsertIntoTracklist(tracklist);

            }

            Vote vote = null;
            //vote = new Vote(15, 20, 20, "900");
            //bl.InsertVote(vote);
            y = 15;
            z = 100;
            for (int i = 5; i < 50; i++, y++, z++)
            {
                vote = new Vote(y, z, i, (i % 24).ToString() + ".00");
                bl.InsertVote(vote);
            }




            #region test dummies
            //Vote vote = new Vote(22, 5, 6, "18.00");
            //yes = bl.voteDAO.AddVote(vote);
            //Console.WriteLine(yes);
            //vote = new Vote(23, 5, 6, "18.59");
            //yes = bl.voteDAO.AddVote(vote);
            //Console.WriteLine(yes);

            //int voted = bl.voteDAO.GetVotesForTrack(6, 5);
            //Console.WriteLine(voted);

            //ObservableCollection<Playlist> playlist = new ObservableCollection<Playlist>();
            //playlist = bl.playlistDAO.GetAllPlaylists();
            //Console.WriteLine(playlist.Count());

            //ObservableCollection<Track> trackshit = new ObservableCollection<Track>();
            //trackshit = bl.playlistDAO.GetAllTracksInPlaylist(5);
            //Console.WriteLine(trackshit.Count());

            //Tracklist tlist = new Tracklist(5, 15, 6);
            //yes = bl.tracklistDAO.AddTracklist(tlist);
            //Console.WriteLine(yes); 
            //tlist = new Tracklist(5, 15, 8);
            //yes = bl.tracklistDAO.AddTracklist(tlist);
            //Console.WriteLine(yes);

            //ObservableCollection<Track> tlist = new ObservableCollection<Track>();
            //tlist = bl.tracklistDAO.GetTracksRecommendedByUser(15);
            //Console.WriteLine(tlist.Count());

            //Partytweet pt = new Partytweet(13, 5, "YOLO VOLL SWAGGA DIE PARTY!");
            //yes = bl.partytweetDAO.AddTweet(pt);
            //Console.WriteLine(yes);
            //pt = new Partytweet(15, 5, "YOLO VOLL sheeeet DIE PARTY!");
            //yes = bl.partytweetDAO.AddTweet(pt);
            //Console.WriteLine(yes);

            //ObservableCollection<Partytweet> ObservableCollection = new ObservableCollection<Partytweet>();
            //ptList = bl.partytweetDAO.GetTweetsForParty(5);
            //Console.WriteLine(ptList.Count());
            //ptList = bl.partytweetDAO.GetAllTweets();
            //Console.WriteLine(ptList.Count());

            //User user = new User("Werner", "aaa", "www@email.at", false);
            //yes = bl.userDAO.InsertUser(user);
            //Console.WriteLine(yes); 
            //user = new User("Hans", "aaass", "sssssssss@email.at", true);
            //yes = bl.userDAO.InsertUser(user);
            //Console.WriteLine(yes);

            //Party party = new Party("Swagerino", "Hgb", "Werner", "22.11.2013 18.00", "22.11.2013 24.00", false);
            //yes = bl.partyDAO.AddParty(party);
            //Console.WriteLine(yes);

            //party = new Party("Swagerinho", "Linz", "Schronz", "22.11.2013 18.00", "22.11.2013 24.00", true);
            //yes = bl.partyDAO.AddParty(party);
            //Console.WriteLine(yes);

            //Guest guest = new Guest(13, 5);
            //yes = bl.guestDAO.AddGuest(guest);
            //Console.WriteLine(yes);
            //guest = new Guest(14, 5);
            //yes = bl.guestDAO.AddGuest(guest);
            //Console.WriteLine(yes);

            //ObservableCollection<User> userLG = new ObservableCollection<User>();
            //userLG = bl.guestDAO.GetGuestlistForParty(5);
            //Console.WriteLine(userLG.Count());

            //ObservableCollection<User> userL = new ObservableCollection<User>();
            //userL = bl.userDAO.GetAllUser();
            //Console.WriteLine(userL.Count());
            //foreach (var item in userL)
            //{
            //    bl.userDAO.DeleteUser(item.UserId);
            //}

            //Track track = new Track("Dancing", "Bobby", "ww.ww.ww.", 180, "Metallerino", false);
            //yes = bl.trackDAO.AddTrack(track);
            //Console.WriteLine(yes);
            //track = new Track("Partoy", "Mommay", "ww.ss.ww.", 231, "Metallerino", true);
            //yes = bl.trackDAO.AddTrack(track);
            //Console.WriteLine(yes);

            //ObservableCollection<Track> trackL = new ObservableCollection<Track>();
            //trackL = bl.trackDAO.GetAllTracks();
            //Console.WriteLine(trackL.Count());
            //foreach (var item in trackL)
            //{
            //    bl.trackDAO.RemoveTrackWithId(item.TrackId);
            //}

            //Party party = new Party("Swagerino", "Hgb", "Werner", "22.11.2013 18.00", "22.11.2013 24.00", false);
            //yes = bl.partyDAO.AddParty(party);
            //Console.WriteLine(yes);

            //party = new Party("Swagerinho", "Linz", "Schronz", "22.11.2013 18.00", "22.11.2013 24.00", true);
            //yes = bl.partyDAO.AddParty(party);
            //Console.WriteLine(yes);

            //ObservableCollection<Party> partyL = new ObservableCollection<Party>();
            //partyL = bl.partyDAO.GetAllParties();
            //Console.WriteLine(partyL.Count());
            //foreach (var item in partyL)
            //{
            //    bl.partyDAO.RemovePartyWithId(item.PartyId);
            //}

            //int i = 1;

            /*
             select * from [dbo].[User]
             select * from [dbo].[Party]
             select * from [dbo].[Track]
            select * from [dbo].[Playlist]
            select * from [dbo].[Tracklist]
             */
            #endregion
        }
    }
}