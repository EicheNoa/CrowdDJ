using System;
using CrowdDJ.DomainClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CrowdDJ.Interfaces
{
    public interface IPlaylist
    {
        bool InsertPlaylist(Playlist newPlaylist);
        bool DeletePlaylist(int id);
        bool UpdatePlaylist(int id, Playlist updatedPlaylist);
        Playlist GetPlaylistForParty(string id);
        ObservableCollection<Track> GetAllTracksForParty(string partyId);
        ObservableCollection<Playlist> GetAllPlaylists();
    }
}
