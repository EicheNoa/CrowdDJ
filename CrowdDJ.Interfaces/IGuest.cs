using System;
using CrowdDJ.DomainClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CrowdDJ.Interfaces
{
    public interface IGuest
    {
        bool AddGuest(Guest newGuest);
        bool RemoveGuest(Guest removeGuest);
        bool PartyIsVisitedByGuest(int searchGuestId, string partyId);
        ObservableCollection<User> GetGuestlistForParty(string partyId);
        ObservableCollection<Party> GetAttemptedPartyList(int userId);
    }
}
