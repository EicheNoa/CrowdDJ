using CrowdDJ.BL;
using CrowdDJ.DomainClasses;
using CrowdDJ.Playstation.Views;
using MyToolkit.Multimedia;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
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

        private Track selectedTrack;
        public Track SelectedTrack
        {
            get { return selectedTrack; }
            set { selectedTrack = value; }
        }


        private Uri currentTrack;
        public Uri CurrentTrack
        {
            get { return currentTrack; }
            set { currentTrack = value; OnPropertyChanged("CurrentTrack"); }
        }

        private String songTitle;
        public String SongTitle
        {
            get { return songTitle; }
            set { songTitle = value; OnPropertyChanged("SongTitle"); }
        }

        private double volume;

        public double Volume
        {
            get { return volume; }
            set { volume = value; OnPropertyChanged("Volume"); }
        }



        public ICommand AddNewTrackCommand { get; private set; }
        public ICommand ShowAllTracksCommand { get; private set; }
        public ICommand PlayMediaCommand { get; private set; }
        public ICommand PauseMediaCommand { get; private set; }
        public ICommand StopMediaCommand { get; private set; }
        public ICommand PreviousMediaCommand { get; private set; }
        public ICommand NextMediaCommand { get; private set; }

        private int trackIndex;
        private Party previousParty = null;
        private ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();
        private MediaElement mediaElement = null;

        public PlaylistVM(MediaElement mediaElement)
        {
            this.mediaElement = mediaElement;
            trackIndex = 0;

            this.AddNewTrackCommand = new RelayCommand(this.AddNewTrack);
            this.ShowAllTracksCommand = new RelayCommand(this.ShowAllTrack);
            this.PlayMediaCommand = new RelayCommand(this.PlayMedia);
            this.PauseMediaCommand = new RelayCommand(this.PauseMedia);
            this.StopMediaCommand = new RelayCommand(this.StopMedia);
            this.PreviousMediaCommand = new RelayCommand(this.PreviousMedia);
            this.NextMediaCommand = new RelayCommand(this.NextMedia);

            AllParties = bl.GetAllParties();
            previousParty = AllParties.First();
            IsSelectedParty = previousParty;
            UpdateTracks();
            SetCurrentTrack();
        }

        private void NextMedia(object obj)
        {
            trackIndex++;
            if (trackIndex >= Tracks.Count())
            {
                CurrentTrack = new Uri(Tracks.First().Url);
                trackIndex = 0;
            }
            PlayTrack();
        }

        private void PreviousMedia(object obj)
        {
            trackIndex--;
            if (trackIndex < 0)
            {
                trackIndex = 0;
            }
            PlayTrack();
        }

        private void StopMedia(object obj)
        {
            mediaElement.Stop();
        }

        private void PauseMedia(object obj)
        {
            mediaElement.Pause();
        }

        private void PlayMedia(object obj)
        {
            PlayTrack();
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
            if (playlist != null)
            {
                Tracks = bl.GetAllTracksForParty(playlist.PartyId);
                previousParty = IsSelectedParty;
                SetCurrentTrack();
            }
        }

        private void SetCurrentTrack()
        {
            if (Tracks.Count() > 0)
            {
                ConvertTrackUri(Tracks.First());
                trackIndex = 0;
            }
        }

        private void PlayTrack()
        {
            if (Tracks.Count() > 0)
            {
                mediaElement.Stop();
                SongTitle = Tracks[trackIndex].Title + " von " + Tracks[trackIndex].Artist;
                ConvertTrackUri(Tracks[trackIndex]);
                mediaElement.Play();
            }
        }

        private async void ConvertTrackUri(Track track)
        {
            string url = track.Url;
            bool youtube = url.Contains("youtu");
            if (youtube)
            {
                int index = url.IndexOf("=");
                string youtubeId = url.Substring(index + 1, (url.Length - index - 1));
                var youtubeurl = await YouTube.GetVideoUriAsync(youtubeId, YouTubeQuality.Quality720P);

                url = youtubeurl.Uri.ToString();
            }
            CurrentTrack = new Uri(url);
        }
    }
}
