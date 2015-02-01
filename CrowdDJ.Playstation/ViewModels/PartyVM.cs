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
    class PartyVM : ViewModelBase
    {
        private ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();
        Timer timer = new Timer(10000);
        private ObservableCollection<User> collectionAllUser = new ObservableCollection<User>();

        private ObservableCollection<User> attendingUser;
        public ObservableCollection<User> AttendingUser
        {
            get { return attendingUser; }
            set { attendingUser = value; OnPropertyChanged("AttendingUser"); }
        }

        private ObservableCollection<User> allUser;
        public ObservableCollection<User> AllUser
        {
            get { return allUser; }
            set { allUser = value; OnPropertyChanged("AllUser"); }
        }


        private ObservableCollection<Party> allParties;
        public ObservableCollection<Party> AllParties
        {
            get { return allParties; }
            set { allParties = value; OnPropertyChanged("AllParties"); }
        }
        public Party IsSelectedParty { get; set; }
        public User IsSelectedAddUser { get; set; }
        public User IsSelectedAttUser { get; set; }
        public ICommand DeletePartyCommand { get; private set; }
        public ICommand UpdatePartyCommand { get; private set; }
        public ICommand AddNewPartyCommand { get; private set; }
        public ICommand DoubleClickCommand { get; private set; }
        public ICommand AddUserToPartyCommand { get; private set; }

        public PartyVM()
        {
            this.DeletePartyCommand = new RelayCommand(this.DeleteParty);
            this.UpdatePartyCommand = new RelayCommand(this.UpdateParty);
            this.AddNewPartyCommand = new RelayCommand(this.AddParty);
            this.DoubleClickCommand = new RelayCommand(this.SetSelectedParty);
            this.AddUserToPartyCommand = new RelayCommand(this.AddUserToParty);
            timer.Elapsed += Init;
            timer.Start();
        }

        private void Init(object sender, ElapsedEventArgs e)
        {
            AllParties = bl.GetAllParties();
            collectionAllUser = bl.GetAllUser();
            AllUser = collectionAllUser;
            IsSelectedParty = AllParties.First();
            UpdateViewForParty();
        }

        private void UpdateViewForParty( )
        {
            AllUser = collectionAllUser;
            AttendingUser = bl.GetGuestlistForParty(IsSelectedParty.PartyId);
            for (int i = AllUser.Count() - 1; i >= 0; i--)
            {
                var item = AllUser[i];
                if (AttendingUser.Contains(item))
                {
                    AllUser.RemoveAt(i);
                }
            }
        }

        private void AddUserToParty(object obj)
        {
            IsSelectedAddUser = obj as User;
            if (!bl.PartyIsVisitedByGuest(IsSelectedAddUser.UserId, IsSelectedParty.PartyId))
            {
                if (bl.AddGuest(new Guest(IsSelectedAddUser.UserId, IsSelectedParty.PartyId)))
                    AttendingUser.Add(IsSelectedAddUser);
            }
        }

        private void SetSelectedParty(object obj)
        {
            IsSelectedParty = obj as Party;
            UpdateViewForParty();
        }

        private void AddParty(object obj)
        {
            Window window = new PartyAddNewPartyWindow();
            window.Show();
        }

        private void UpdateParty(object obj)
        {
            SetSelectedParty(obj);
            bl.UpdateParty(IsSelectedParty, IsSelectedParty.PartyId);
        }

        private void DeleteParty(object obj)
        {
            if ((IsSelectedParty == null) && (obj as Party != IsSelectedParty))
            {
                bl.RemovePartyWithId(IsSelectedParty.PartyId);
                AllParties.Remove(IsSelectedParty);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show(
                    "Keine Zeile ausgewählt! Bitte einen Doppelklick auf eine Zeile!", "Fehler...",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
