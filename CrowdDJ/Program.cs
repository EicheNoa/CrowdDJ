using System;
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
            ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();
            int y = 0;
            int z = 0;
            bool r;
            User user = new User("a", "a", "a", true);
            bl.InsertUser(user);
            for (int i = 1; i < 200; i++)
            {
                if (z++ < 25)
                {
                    r = false;
                }
                else
                {
                    r = true;
                    z = 0;
                }
                user = new User("Peter der " + i + ".", i.ToString(), "ichBinnnnn" + i + "@dar.at", r);
                bl.InsertUser(user);
            }
            z = 0;

            List<string> partylist = new List<string>();
            string pl;
            Party party = null;
            party = new Party("All Songs", "All Songs", "", "", "", "", false);
            bl.AddParty(party);
            for (int i = 1; i < 20; i++)
            {
                if (z++ < 12)
                {
                    r = false;
                }
                else
                {
                    r = true;
                    z = 0;
                }
                pl = "Party numero " + i;
                partylist.Add(pl);
                party = new Party(pl, "Die " + i + ". wilde Hilde", "Am Berg " + i, "Susi von der " + i + ".",
                                        (i % 24).ToString() + ".00", (i % 24).ToString() + ".00", r);
                bl.AddParty(party);
            }
            z = 0;

            Track track = null;
            track = new Track("Taking Back My Love", "Enrique Iglesias", @"D:\Musik\_Takin' Back My Love_ - Enrique Iglesias feat. Ciara.mp4",
                                "Pop", true);
            bl.InsertTrack(track);
            track = new Track("Airplanes ft Eminem", "Hayley Williams", @"D:\Musik\_Takin' Back My Love_ - Enrique Iglesias feat. Ciara.mp4",
                                "Pop", true);
            bl.InsertTrack(track);
            track = new Track("Nobody's Home", "Avril Lavigne", @"D:\Musik\Avril Lavigne - Nobody's Home (With Lyrics).mp4",
                                "Pop & Rock", true);
            bl.InsertTrack(track);
            track = new Track("Smile", "Avril  Lavigne", @"D:\Musik\Avril Lavigne - Nobody's Home (With Lyrics).mp4",
                                "Pop & Rock", true);
            bl.InsertTrack(track);
            track = new Track("San Francisco", "Cascada", @"D:\Musik\CASCADA - San Francisco (Official Video HD).mp4",
                                "Dance", true);
            bl.InsertTrack(track);
            track = new Track("I Just Had Sex ft Akon", "The Lonely Island", @"D:\Musik\eig\I Just Had Sex (feat. Akon).mp3",
                                "Pop", false);
            bl.InsertTrack(track);
            track = new Track("Written In The Stars", "Tinie Tempah", @"D:\Musik\eig\Tinie Tempah - Written In The Stars ft. Eric Turner.mp3",
                                "Pop", false);
            bl.InsertTrack(track);

            Guest guest = null;
            for (int i = 1; i < 200; i++)
            {
                guest = new Guest(i, partylist[i % 19]);
                bl.AddGuest(guest);
            }

            Partytweet partytweet = null;
            for (int i = 1; i < 200; i++)
            {
                partytweet = new Partytweet((i % 99), partylist[i % 19], "Meine " + i + "-te Partey!");
                bl.AddTweet(partytweet);
            }

            Playlist playlist = null;
            for (int i = 1; i < 200; i++)
            {
                playlist = new Playlist(partylist[i % 19], "Das Beste im " + i + "er Pack");
                bl.InsertPlaylist(playlist);
            }

            Tracklist tracklist = null;
            for (int i = 1; i < 7; i++)
            {
                tracklist = new Tracklist(1, i, i);
                bl.InsertIntoTracklist(tracklist);

            }

            Vote vote = null;
            y = 15;
            z = 1;
            for (int i = 1; i < 7; i++)
            {
                vote = new Vote(i, 1, 3, (i % 24).ToString() + ".00");
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
