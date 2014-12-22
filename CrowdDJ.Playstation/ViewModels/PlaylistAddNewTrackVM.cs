using CrowdDJ.BL;
using CrowdDJ.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CrowdDJ.Playstation.ViewModels
{
    class PlaylistAddNewTrackVM : ViewModelBase
    {
        public string Title { get; set; }
        public string Artist { get; set; }
        public string Url { get; set; }
        public int Length { get; set; }
        public string Genre { get; set; }
        public bool IsVideo { get; set; }

        public ICommand AddNewTrackCommand { get; private set; }
        public ICommand CancelAddTrackCommand { get; private set; }

        private ICrowdDJBL bl = new CrowdDJBL();

        public PlaylistAddNewTrackVM()
        {
            this.AddNewTrackCommand = new RelayCommand(this.AddNewTrack);
            this.CancelAddTrackCommand = new RelayCommand(this.CancelAddTrack);
        }

        private void AddNewTrack(object obj)
        {
            bl.InsertTrack(new Track(Title, Artist, Url, Length, Genre, IsVideo));
            Application.Current.Windows[1].Close();
            MessageBoxResult result = MessageBox.Show("Track wurde hinzugefügt!!", "Gratuliere",
                                                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
        private void CancelAddTrack(object obj)
        {
            Application.Current.Windows[1].Close();
        }
    }
}
