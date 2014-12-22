using CrowdDJ.BL;
using CrowdDJ.DomainClasses;
using CrowdDJ.Playstation.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        public ICommand DeleteUserCommand { get; private set; }
        public ICommand AddUserWindowCommand { get; private set; }
        public ICommand UpdateUserCommand { get; private set; }
        public ICommand DoubleClickCommand { get; set; }

        private ICrowdDJBL bl = new CrowdDJBL();

        public UserVM()
        {
            this.DeleteUserCommand = new RelayCommand(this.DeleteUser);
            this.DoubleClickCommand = new RelayCommand(this.SetIsSelectedUser);
            this.UpdateUserCommand = new RelayCommand(this.UpdateUser);
            this.AddUserWindowCommand = new RelayCommand(this.AddNewUserWindow);
            AllUser = bl.GetAllUser();
            IsSelectedUser = AllUser.First();
            UpdateDataGrids();
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
            SetIsSelectedUser(obj);
            bl.UpdateUser(IsSelectedUser, IsSelectedUser.UserId);
        }
    }
}
