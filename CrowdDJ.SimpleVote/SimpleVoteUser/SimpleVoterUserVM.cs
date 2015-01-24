using CrowdDJ.BL;
using CrowdDJ.DomainClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows.Controls;
using System.Windows.Input;

namespace CrowdDJ.SimpleVote.SimpleVoteUser
{
    class SimpleVoterUserVM : ViewModelBase
    {
        public string PartyId { get; set; }
        public int UserId { get; set; }
        public Playlist Playlist { get; set; }

        public string CurParty { get; set; }

        private ObservableCollection<Track> allTracks;
        public ObservableCollection<Track> AllTracks
        {
            get { return allTracks; }
            set { allTracks = value; OnPropertyChanged("AllTracks"); }
        }

        private Track currentTrack;
        public Track CurrentTrack
        {
            get { return currentTrack; }
            set { currentTrack = value; OnPropertyChanged("CurrentTrack"); }
        }

        private String sentTweets;
        public String SentTweets
        {
            get { return sentTweets; }
            set { sentTweets = value; OnPropertyChanged("SentTweets"); }
        }


        public ICommand VoteForTrackCommand { get; private set; }
        public ICommand SendNewTweetCommand { get; private set; }
        public ICommand SuggestTrackCommand { get; private set; }

        private ICrowdDJBL bl = null;
        private Timer timer = new Timer(3000);


        public SimpleVoterUserVM(string partyId, int userId)
        {
            bl = CrowdDJBL.GetCrowdDJBL();
            PartyId = partyId;
            UserId = userId;
            Playlist = bl.GetPlaylistForParty(PartyId);

            this.VoteForTrackCommand = new RelayCommand(this.VoteForTrack);
            this.SendNewTweetCommand = new RelayCommand(this.SendNewTweet);
            this.SuggestTrackCommand = new RelayCommand(this.SuggestTrack);

            timer.Elapsed += Init;
            timer.Start();
        }

        private void SuggestTrack(object obj)
        {
            throw new NotImplementedException();
        }

        private void SendNewTweet(object obj)
        {
            throw new NotImplementedException();
        }

        private void VoteForTrack(object obj)
        {
            CurrentTrack = obj as Track;
            if (!bl.AlreadyVotedForTrack(CurrentTrack.TrackId, UserId, Playlist.PlaylistId))
            {
                bl.InsertVote(new Vote(UserId, Playlist.PlaylistId, CurrentTrack.TrackId, DateTime.Now.ToShortDateString()));
            }
        }

        private void Init(object sender, ElapsedEventArgs e)
        {
            try
            {
                CurParty = bl.FindPartyById(PartyId).Name;
                AllTracks = bl.GetAllTracksForParty(PartyId);
                CurrentTrack = AllTracks.First();
            }
            catch (Exception)
            {
            }
            Debug.WriteLine("...und ich warte");
        }
     }
}
