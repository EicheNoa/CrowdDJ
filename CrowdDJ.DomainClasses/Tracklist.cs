using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.DomainClasses
{
    [Serializable]
    public class Tracklist
    {
        public Tracklist() { }
        public Tracklist(int playlistId, int userId, int trackId)
        {
            PlaylistId = playlistId;
            UserId = userId;
            TrackId = trackId;
        }

        private int palylistId;
        public int PlaylistId
        {
            get { return palylistId; }
            set { palylistId = value; }
        }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private int trackId;
        public int TrackId
        {
            get { return trackId; }
            set { trackId = value; }
        }

    }
}
