using System;
using CrowdDJ.DomainClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CrowdDJ.Interfaces
{
    public interface ITrack
    {
        bool InsertTrack(Track newTrack);
        bool RemoveTrackWithId(int id);
        bool UpdateTrack(Track track, int id);
        Track FindTrackById(int id);
        ObservableCollection<Track> FindTrackWithTitle(string title);
        ObservableCollection<Track> FindTracksInGenre(string genre);
        ObservableCollection<Track> FindVideos();
        ObservableCollection<Track> FindSongs();
        ObservableCollection<Track> GetAllTracks();
    }
}
