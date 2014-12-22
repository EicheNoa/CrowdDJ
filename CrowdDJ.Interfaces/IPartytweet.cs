using System;
using CrowdDJ.DomainClasses;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;

namespace CrowdDJ.Interfaces
{
    public interface IPartytweet
    {
        bool AddTweet(Partytweet newTweet);
        bool DeletePartytweet(Partytweet deletePartytweet);
        ObservableCollection<Partytweet> GetTweetsForParty(string partyId);
        ObservableCollection<Partytweet> GetTweetsForUser(int userId);
        ObservableCollection<Partytweet> GetAllTweets();
    }
}
