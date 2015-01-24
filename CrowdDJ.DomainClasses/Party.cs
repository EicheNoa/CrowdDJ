using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.DomainClasses
{
    [Serializable]
    public class Party
    {
        public Party() { }
        public Party(string partyId, string name, string location, string host, string partyBegin, string partyEnd, bool isActive)
        {
            PartyId = partyId;
            Name = name;
            Location = location;
            Host = host;
            PartyBegin = partyBegin;
            PartyEnd = partyEnd;
            IsActive = isActive;
        }

        private string partyId;

        public string PartyId
        {
            get { return partyId; }
            set { partyId = value; }
        }
        

        private string name;
        public string Name
        {
            get { return name; }
            set { name = value; }
        }

        private string location;
        public string Location
        {
            get { return location; }
            set { location = value; }
        }

        private string host;
        public string Host
        {
            get { return host; }
            set { host = value; }
        }

        private string partyBegin;
        public string PartyBegin
        {
            get { return partyBegin; }
            set { partyBegin = value; }
        }

        private string partyEnd;
        public string PartyEnd
        {
            get { return partyEnd; }
            set { partyEnd = value; }
        }

        private bool isActive;
        public bool IsActive
        {
            get { return isActive; }
            set { isActive = value; }
        }
        
        
    }
}
