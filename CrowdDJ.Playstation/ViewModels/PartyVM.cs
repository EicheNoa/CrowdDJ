using CrowdDJ.BL;
using CrowdDJ.DomainClasses;
using CrowdDJ.Playstation.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace CrowdDJ.Playstation.ViewModels
{
    class PartyVM : ViewModelBase
    {
        private ObservableCollection<Party> allParties;
        public ObservableCollection<Party> AllParties
        {
            get { return allParties; }
            set { allParties = value; OnPropertyChanged("AllParties"); }
        }
        public Party IsSelectedParty { get; set; }
        public ICommand DeletePartyCommand { get; private set; }
        public ICommand UpdatePartyCommand { get; private set; }
        public ICommand AddNewPartyCommand { get; private set; }
        public ICommand DoubleClickCommand { get; private set; }

        private ICrowdDJBL bl = new CrowdDJBL();

        public PartyVM()
        {
            this.DeletePartyCommand = new RelayCommand(this.DeleteParty);
            this.UpdatePartyCommand = new RelayCommand(this.UpdateParty);
            this.AddNewPartyCommand = new RelayCommand(this.AddParty);
            this.DoubleClickCommand = new RelayCommand(this.SetSelectedParty);
            AllParties = bl.GetAllParties();
            IsSelectedParty = AllParties.First();
        }

        private void SetSelectedParty(object obj)
        {
            IsSelectedParty = obj as Party;
        }

        private void AddParty(object obj)
        {
            Window window = new PartyAddNewPartyWindow();
            window.Show();
        }

        private void UpdateParty(object obj)
        {
            SetSelectedParty(obj);
            bl.UpdateParty(IsSelectedParty, IsSelectedParty.PartyId);
        }

        private void DeleteParty(object obj)
        {
            if ((IsSelectedParty == null) && (obj as Party != IsSelectedParty))
            {
                bl.RemovePartyWithId(IsSelectedParty.PartyId);
                AllParties.Remove(IsSelectedParty);
            }
            else
            {
                MessageBoxResult result = MessageBox.Show("Keine Zeile ausgewählt! Bitte einen Doppelklick auf eine Zeile!", "Fehler...",
                                                            MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
