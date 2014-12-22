using System;
using System.Collections.Generic;
using CrowdDJ.DomainClasses;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CrowdDJ.Interfaces
{
    public interface IParty
    {
        bool AddParty(Party newParty);
        bool RemovePartyWithId(string partyId);
        bool UpdateParty(Party party, string id);
        Party FindPartyById(string partyId);
        ObservableCollection<Party> FindPartyWithHost(string hostName);
        ObservableCollection<Party> GetAllParties();
    }
}
