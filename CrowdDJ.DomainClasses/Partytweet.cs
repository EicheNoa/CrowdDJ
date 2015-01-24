using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrowdDJ.DomainClasses
{
    [Serializable]
    public class Partytweet
    {
        public Partytweet() { }
        public Partytweet(int userId, string partyId, string message)
        {
            UserId = userId;
            PartyId = partyId;
            Message = message;            
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

        private string message;
        public string Message
        {
            get { return message; }
            set { message = value; }
        }

    }
}
