using System;
using CrowdDJ.DomainClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.Interfaces
{
    public interface IVote
    {
        bool InsertVote(Vote newVote);
        bool AlreadyVotedForTrack(int trackId, int userId, int playlistId);
        int GetVotesForTrack(int trackId, int playlistId);
    }
}