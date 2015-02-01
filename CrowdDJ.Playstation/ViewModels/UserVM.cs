using CrowdDJ.BL;
using CrowdDJ.DomainClasses;
using CrowdDJ.Playstation.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;
using System.Windows;
using System.Windows.Input;

namespace CrowdDJ.Playstation.ViewModels
{
    public class UserVM : ViewModelBase
    {
        private ObservableCollection<User> allUser { get; set; }
        public ObservableCollection<User> AllUser
        {
            get { return allUser; }
            set { allUser = value; OnPropertyChanged("AllUser"); }
        }

        private ObservableCollection<Party> attemptedParties { get; set; }
        public ObservableCollection<Party> AttemptedParties
        {
            get { return attemptedParties; }
            set { attemptedParties = value; OnPropertyChanged("AttemptedParties"); }
        }

        private ObservableCollection<Partytweet> sentPartyTweets { get; set; }
        public ObservableCollection<Partytweet> SentPartyTweets
        {
            get { return sentPartyTweets; }
            set { sentPartyTweets = value; OnPropertyChanged("SentPartyTweets"); }
        }

        private ObservableCollection<Track> votedTracks { get; set; }
        public ObservableCollection<Track> VotedTracks
        {
            get { return votedTracks; }
            set { votedTracks = value; OnPropertyChanged("VotedTracks"); }
        }
        public User IsSelectedUser { get; set; }
        public Partytweet IsSelectedPartytweet { get; set; }
        public Party IsSelectedParty { get; set; }
        public ICommand DeleteUserCommand { get; private set; }
        public ICommand AddUserWindowCommand { get; private set; }
        public ICommand UpdateUserCommand { get; private set; }
        public ICommand DeletePartytweetCommand { get; private set; }
        public ICommand DeleteUserFromPartyCommand { get; private set; }
        public ICommand DoubleClickCommand { get; private set; }
        public ICommand UpdateUserViewCommand { get; private set; }

        private ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();
        Timer timer = new Timer(10000);

        public UserVM()
        {
            this.DeleteUserCommand = new RelayCommand(this.DeleteUser);
            this.DoubleClickCommand = new RelayCommand(this.SetIsSelectedUser);
            this.UpdateUserCommand = new RelayCommand(this.UpdateUser);
            this.AddUserWindowCommand = new RelayCommand(this.AddNewUserWindow);
            this.DeleteUserFromPartyCommand = new RelayCommand(this.DeleteUserFromParty);
            this.DeletePartytweetCommand = new RelayCommand(this.DeletePartytweet);
            this.UpdateUserViewCommand = new RelayCommand(this.UpdateUserView);
            timer.Elapsed += Init;
            timer.Start();
        }

        private void Init(object sender, ElapsedEventArgs e)
        {
            SetCollections();
        }

        private void UpdateUserView(object obj)
        {
            SetCollections();
        }

        private void SetCollections()
        {
            AllUser = bl.GetAllUser();
            if (AllUser.Count() > 0)
                IsSelectedUser = AllUser.First();
            UpdateDataGrids();
            if (AttemptedParties.Count() > 0)
                IsSelectedParty = AttemptedParties.First();
            if (SentPartyTweets.Count() > 0)
                IsSelectedPartytweet = SentPartyTweets.First();  
        }

        private void DeleteUserFromParty(object obj)
        {
            IsSelectedParty = obj as Party;
            if ((IsSelectedParty != null) && (obj as Party == IsSelectedParty))
            {
                bl.RemoveGuest(new Guest(IsSelectedUser.UserId, IsSelectedParty.PartyId));
                AttemptedParties.Remove(IsSelectedParty);
            }
        }

        private void DeletePartytweet(object obj)
        {
            IsSelectedPartytweet = obj as Partytweet;
            if ((IsSelectedPartytweet != null) && (obj as Partytweet == IsSelectedPartytweet))
            {
                bl.DeletePartytweet(IsSelectedPartytweet);
                SentPartyTweets.Remove(IsSelectedPartytweet);
            }
        }

        private void UpdateDataGrids()
        {
            AttemptedParties = bl.GetAttemptedPartyList(IsSelectedUser.UserId);
            SentPartyTweets = bl.GetTweetsForUser(IsSelectedUser.UserId);
            VotedTracks = bl.GetTracksRecommendedByUser(IsSelectedUser.UserId);
        }

        private void SetIsSelectedUser(object obj)
        {
            IsSelectedUser = obj as User;
            UpdateDataGrids();
        }

        private void AddNewUserWindow(object obj)
        {
            Window window = new UserAddNewUserWindow();
            window.Show();
        }

        private void DeleteUser(object obj)
        {
            //SetIsSelectedUser(obj);
            if ((IsSelectedUser != null) && (obj as User == IsSelectedUser))
            {
                bl.DeleteUser(IsSelectedUser.UserId);
                AllUser.Remove(IsSelectedUser);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Keine Zeile ausgewählt! Bitte einen Doppelklick auf eine Zeile!", "Fehler...",
                                                            MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateUser(object obj)
        {
            bl.UpdateUser(IsSelectedUser, IsSelectedUser.UserId);
        }
    }
}
