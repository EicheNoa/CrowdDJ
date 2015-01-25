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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CrowdDJ.SimpleVote.SimpleVoteUser
{
    class SimpleVoterUserVM : ViewModelBase
    {
        public string PartyId { get; set; }
        public User MyUser { get; set; }
        public Playlist Playlist { get; set; }

        private string newTweet;
        public string NewTweet
        {
            get { return newTweet; }
            set { newTweet = value; OnPropertyChanged("NewTweet"); }
        }

        private Track selectedTrack;
        public Track SelectedTrack
        {
            get { return selectedTrack; }
            set { selectedTrack = value; OnPropertyChanged("SelectedTrack"); }
        }

        private string curParty;
        public string CurParty
        {
            get { return curParty; }
            set { curParty = value; OnPropertyChanged("CurParty"); }
        }

        private ObservableCollection<Track> allTracks;
        public ObservableCollection<Track> AllTracks
        {
            get { return allTracks; }
            set { allTracks = value; OnPropertyChanged("AllTracks"); }
        }

        private ObservableCollection<Track> partyTracks;
        public ObservableCollection<Track> PartyTracks
        {
            get { return partyTracks; }
            set { partyTracks = value; OnPropertyChanged("PartyTracks"); }
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

        private ObservableCollection<Partytweet> allTweetsForParty;
        public ObservableCollection<Partytweet> AllTweetsForParty
        {
            get { return allTweetsForParty; }
            set { allTweetsForParty = value; OnPropertyChanged("AllTweetsForParty"); }
        }

        public ICommand VoteForTrackCommand { get; private set; }
        public ICommand SendNewTweetCommand { get; private set; }
        public ICommand SuggestTrackCommand { get; private set; }

        private ICrowdDJBL bl = null;
        private Timer timer = new Timer(5000);


        public SimpleVoterUserVM(string partyId, int userId)
        {
            bl = CrowdDJBL.GetCrowdDJBL();
            PartyId = partyId;

            try
            {
                MyUser = bl.FindUserById(userId);
                Playlist = bl.GetPlaylistForParty(PartyId);
                CurParty = bl.FindPartyById(PartyId).Name;
                AllTracks = bl.GetAllTracks();
                AllTweetsForParty = bl.GetTweetsForParty(PartyId);
                PartyTracks = bl.GetTracklistSortedVotes(Playlist.PlaylistId);
            }
            catch (Exception)
            {
                throw;
            }

            this.VoteForTrackCommand = new RelayCommand(this.VoteForTrack);
            this.SendNewTweetCommand = new RelayCommand(this.SendNewTweet);
            this.SuggestTrackCommand = new RelayCommand(this.SuggestTrack);
            
            timer.Elapsed += Init;
            timer.Start();
        }

        private void SuggestTrack(object obj)
        {
            try
            {
                SelectedTrack = obj as Track;
                if ((PartyTracks.Any(p => p.TrackId == SelectedTrack.TrackId)))
                {
                    MessageBoxResult result = MessageBox.Show("Track ist schon in der Playlist.",
                                                              "Pass auf!", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    bl.InsertIntoTracklist(new Tracklist(Playlist.PlaylistId, MyUser.UserId, (obj as Track).TrackId));
                }
            }
            catch (Exception)
            {

            }
        }

        private void SendNewTweet(object obj)
        {
            if (NewTweet != null || NewTweet != "")
            {
                bl.AddTweet(new Partytweet(MyUser.UserId, PartyId, NewTweet));
                NewTweet = "";
            }
        }

        private void VoteForTrack(object obj)
        {
            CurrentTrack = obj as Track;
            if (CurrentTrack != null && !bl.AlreadyVotedForTrack(MyUser.UserId, CurrentTrack.TrackId, Playlist.PlaylistId))
            {
                bl.InsertVote(new Vote(MyUser.UserId, Playlist.PlaylistId, CurrentTrack.TrackId, DateTime.Now.ToString()));
            }
            else
            {
                bl.AddTweet(new Partytweet(1, PartyId, "No 'oh! Du kannst nicht mehrmals für einen Song abstimmen, "
                                                       + MyUser.Name + "!"));
            }
        }

        private void Init(object sender, ElapsedEventArgs e)
        {
            try
            {
                AllTweetsForParty = bl.GetTweetsForParty(PartyId);
                PartyTracks = bl.GetTracklistSortedVotes(Playlist.PlaylistId);
                Debug.WriteLine("...und ich warte");
            }
            catch (Exception)
            {
            }
        }
     }
}
