using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.DomainClasses
{
    [Serializable]
    public class Guest
    {
        public Guest() { }
        public Guest(int userId, string partyId)
        {
            PartyId = partyId;
            UserId = userId;
        }

        private int userId;
        public int UserId
        {
            get { return userId; }
            set { userId = value; }
        }

        private string partyId;
        public string PartyId
        {
            get { return partyId; }
            set { partyId = value; }
        }

    }
}
