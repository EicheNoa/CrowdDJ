using CrowdDJ.BL;
using CrowdDJ.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CrowdDJ.Playstation.ViewModels
{
    public class PartyAddNewPartyVM
    {
        public string PartyId { get; set; }
        public string Name { get; set; }
        public string Host { get; set; }
        public string Location { get; set; }
        public string PartyBegin { get; set; }
        public string PartyEnd { get; set; }
        public bool IsActive { get; set; }
        public ICommand AddPartyCommand { get; private set; }
        public ICommand CancelAddPartyCommand { get; private set; }

        private ICrowdDJBL bl = new CrowdDJBL();

        public PartyAddNewPartyVM()
        {
            this.AddPartyCommand = new RelayCommand(this.AddParty);
            this.CancelAddPartyCommand = new RelayCommand(this.CancelAddParty);
        }

        private void CancelAddParty(object obj)
        {
            Application.Current.Windows[1].Close();
        }

        private void AddParty(object obj)
        {
            bl.AddParty(new Party(PartyId, Name, Location, Host, PartyBegin, PartyEnd, IsActive));
            Application.Current.Windows[1].Close();
            MessageBoxResult result = MessageBox.Show("Party wurde hinzugefügt!!", "Gratuliere",
                                                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
        }
    }
}
