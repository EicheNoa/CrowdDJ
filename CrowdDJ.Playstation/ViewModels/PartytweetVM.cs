﻿using CrowdDJ.BL;
using CrowdDJ.DomainClasses;
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
    class PartytweetVM : ViewModelBase
    {
        private ObservableCollection<Partytweet> allPartytweets;
        public ObservableCollection<Partytweet> AllPartytweets
        {
            get { return allPartytweets; }
            set { allPartytweets = value; }
        }
        public Partytweet IsSelectedPartytweet { get; set; }

        public ICommand DoubleClickCommand { get; set; }
        public ICommand DeletePartytweetCommand { get; set; }

        private ICrowdDJBL bl = new CrowdDJBL();

        public PartytweetVM()
        {
            this.DoubleClickCommand = new RelayCommand(this.SetSelectedPartytweet);
            this.DeletePartytweetCommand = new RelayCommand(this.DeletePartytweet);
            AllPartytweets = bl.GetAllTweets();
        }

        private void DeletePartytweet(object obj)
        {
            if ((IsSelectedPartytweet != null) && (IsSelectedPartytweet == obj as Partytweet))
            {
                bl.DeletePartytweet(IsSelectedPartytweet);
                AllPartytweets.Remove(IsSelectedPartytweet);
            } 
            else 
            {
                MessageBoxResult result = MessageBox.Show("Keine Zeile ausgewählt! Bitte einen Doppelklick auf eine Zeile!", "Fehler...",
                                                            MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SetSelectedPartytweet(object obj)
        {
            IsSelectedPartytweet = obj as Partytweet;
        }
    }
}
