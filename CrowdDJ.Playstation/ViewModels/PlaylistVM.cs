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
    class PlaylistVM : ViewModelBase
    {
        private ObservableCollection<Party> allparties;
        public ObservableCollection<Party> AllParties
        {
            get { return allparties; }
            set { allparties = value; OnPropertyChanged("AllParties"); }
        }

        private ObservableCollection<Track> tracks;
        public ObservableCollection<Track> Tracks
        {
            get { return tracks; }
            set { tracks = value; OnPropertyChanged("Tracks"); }
        }

        private Party isSelectedParty;
        public Party IsSelectedParty
        {
            get { return isSelectedParty; }
            set
            {
                isSelectedParty = value; 
                OnPropertyChanged("IsSelectedParty");
                UpdateTracks();
            }
        }
        private Party previousParty = null;
        private ICrowdDJBL bl = new CrowdDJBL();

        public ICommand AddNewTrackCommand { get; private set; }
        public ICommand ShowAllTracksCommand { get; private set; }

        public PlaylistVM()
        {
            this.AddNewTrackCommand = new RelayCommand(this.AddNewTrack);
            this.ShowAllTracksCommand = new RelayCommand(this.ShowAllTrack);
            AllParties = bl.GetAllParties();
            previousParty = AllParties[8];
            IsSelectedParty = previousParty;
        }

        private void ShowAllTrack(object obj)
        {
            Tracks = bl.GetAllTracks();
        }

        private void AddNewTrack(object obj)
        {
            Window window = new PlaylistAddTrackWindow();
            window.Show();
        }

        private void UpdateTracks()
        {
            Playlist playlist = bl.GetPlaylistForParty(IsSelectedParty.PartyId);
            if (playlist == null)
            {
                MessageBoxResult result = MessageBox.Show("Keine Playlist für " + IsSelectedParty.Name, "Fehler",
                                                            MessageBoxButton.OK, MessageBoxImage.Error);
                IsSelectedParty = previousParty;
            }
            else
            {
                Tracks = bl.GetAllTracksInPlaylist(playlist.PlaylistId);
                previousParty = IsSelectedParty;
            }
        }
    }
}
