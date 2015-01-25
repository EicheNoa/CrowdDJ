using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.DomainClasses
{
    [Serializable]
    public class Vote
    {
        public Vote() { }
        public Vote(int userId, int playlistId, int trackId, string ts_created)
        {
            UserId = userId;
            PlaylistId = playlistId;
            TrackId = trackId;
            TS_created = ts_created;
        }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private int playlistId;
        public int PlaylistId
        {
            get { return playlistId; }
            set { playlistId = value; }
        }

        private int trackId;
        public int TrackId
        {
            get { return trackId; }
            set { trackId = value; }
        }

        private string ts_created;
        public string TS_created
        {
            get { return ts_created; }
            set { ts_created = value; }
        }

        private User myUser;
        public User MyUser
        {
            get { return myUser; }
            set { myUser = value; }
        }

    }
}
