using CrowdDJ.BL;
using CrowdDJ.DomainClasses;
using CrowdDJ.QRCode;
using CrowdDJ.SimpleVote.SimpleVoteUser;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media.Imaging;

namespace CrowdDJ.SimpleVote
{
    public class SimpleVoteVM : ViewModelBase
    {
        public string Email { get; set; }
        public string PartyId { get; set; }
        public User LogUser { get; set; }

        private Party selectedParty;
        public Party SelectedParty
        {
            get { return selectedParty; }
            set
            {
                selectedParty = value;
                OnPropertyChanged("IsSelectedParty");
                UpdateQRCode();
            }
        }

        private BitmapSource qrPicture { get; set; }
        public BitmapSource QrPicture
        {
            get { return qrPicture; }
            set { qrPicture = value; OnPropertyChanged("QrPicture"); }
        }

        private ObservableCollection<Party> allParties;
        public ObservableCollection<Party> AllParties
        {
            get { return allParties; }
            set { allParties = value; OnPropertyChanged("AllParties"); }
        }        

        public ICommand ExitSimpleVoteCommand { get; private set; }
        public ICommand LogInGuestCommand { get; private set; }

        private ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();

        public SimpleVoteVM()
        {
            this.ExitSimpleVoteCommand = new RelayCommand(this.ExitSimpleVote);
            this.LogInGuestCommand = new RelayCommand(this.LogInGuest);
            AllParties = bl.GetAllParties();
            SelectedParty = AllParties.First();
        }

        private void LogInGuest(object obj)
        {
            try
            {
                if (Email == null || PartyId == null)
                {
                    ErrorMsg(0);
                }
                else if (!bl.PartyIsVisitedByGuest(bl.FindUserByEmail(Email).UserId, PartyId))
                {
                    ErrorMsg(2);
                }
                else
                {
                    LogUser = bl.FindUserByEmail(Email);
                    Window window = new SimpleVoteUserWindow(PartyId, LogUser.UserId);
                    window.Show();
                    Application.Current.Windows[0].Close();
                }
            }
            catch (Exception)
            {
                ErrorMsg(1);
            }
        }

        private void ExitSimpleVote(object obj)
        {
            Application.Current.Shutdown(0);
        }

        private void UpdateQRCode()
        {
            Bitmap cur;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(SelectedParty.PartyId, QRCodeGenerator.ECCLevel.Q);
            cur = qrCode.GetGraphic(20);
            QrPicture = BitMapConverter.ToWpfBitmap(cur);
        
        }

        private void ErrorMsg(int erroNr)
        {
            MessageBoxResult result;
            switch (erroNr)
            {
                case 0:
                    result = MessageBox.Show("Bitte alle Felder ausfüllen!", "Error",
                                                            MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case 1:
                    result = MessageBox.Show("Benutzer / Party nicht vorhanden!", "Error",
                                                            MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                case 2:
                    result = MessageBox.Show("Benutzer ist nicht eingeladen!", "Error",
                                                            MessageBoxButton.OK, MessageBoxImage.Error);
                    break;                    
            }
        }

        
    }
}
