using System;
using CrowdDJ.DomainClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CrowdDJ.Interfaces
{
    public interface IVote
    {
        bool InsertVote(Vote newVote);
        bool AlreadyVotedForTrack(int trackId, int userId, int playlistId);
        int GetVotesForTrack(Track track, int playlistId);
        ObservableCollection<Track> GetTracklistSortedVotes(int playlistId);
    }
}