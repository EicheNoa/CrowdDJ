using System;
using CrowdDJ.DomainClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CrowdDJ.Interfaces
{
    public interface ITracklist
    {
        bool InsertIntoTracklist(Tracklist insertIntoTracklist);
        ObservableCollection<Track> GetTracksRecommendedByUser(int userId);
        ObservableCollection<Tracklist> GetAllTracklists();
        Tracklist FindTrackInTracklist(Tracklist tracklist);
    }
}
