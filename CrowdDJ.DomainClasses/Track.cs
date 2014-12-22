using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.DomainClasses
{
    [Serializable]
    public class Track
    {
        public Track(string title, string artist, string url, int length, string genre, bool isVideo)
        {
            Title = title;
            Artist = artist;
            Url = url;
            Length = length;
            Genre = genre;
            IsVideo = isVideo;
        }

        private int trackId;
        public int TrackId
        {
            get { return trackId; }
            set { trackId = value; }
        }
        

        private string title;
        public string Title
        {
            get { return title; }
            set { title = value; }
        }

        private string artist;
        public string Artist
        {
            get { return artist; }
            set { artist = value; }
        }

        private string url;
        public string Url
        {
            get { return url; }
            set { url = value; }
        }

        private int length;
        public int Length
        {
            get { return length; }
            set { length = value; }
        }

        private string genre;
        public string Genre
        {
            get { return genre; }
            set { genre = value; }
        }

        private bool isVideo;
        public bool IsVideo
        {
            get { return isVideo; }
            set { isVideo = value; }
        }
        
    }
}
