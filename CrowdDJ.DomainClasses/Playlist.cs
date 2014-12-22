using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.DomainClasses
{
    [Serializable]
    public class Playlist
    {
        public Playlist(string partyId, string name)
        {
            PartyId = partyId;
            Name = name;
        }

        private int playlistId;
        public int PlaylistId
        {
            get { return playlistId; }
            set { playlistId = value; }
        }

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string partyId;
        public string PartyId
        {
            get { return partyId; }
            set { partyId = value; }
        }

    }
}
