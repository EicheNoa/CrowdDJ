using CrowdDJ.BL;
using CrowdDJ.DomainClasses;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.SimpleVote.SimpleVoteUser
{
    class SimpleVoterUserVM : ViewModelBase
    {
        public string PartyId { get; set; }

        public string CurParty { get; set; }

        private ObservableCollection<Track> allTracks;
        public ObservableCollection<Track> AllTracks
        {
            get { return allTracks; }
            set { allTracks = value; }
        }
        private ICrowdDJBL bl = new CrowdDJBL();


        public SimpleVoterUserVM(string partyId)
        {
            PartyId = partyId;
            CurParty = bl.FindPartyById(PartyId).Name;
            AllTracks = bl.GetAllTracksInPlaylist(bl.GetPlaylistForParty(PartyId).PlaylistId);
        }
    }
}
