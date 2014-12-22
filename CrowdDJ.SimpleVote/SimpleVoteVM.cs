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

        private User user;
        

        public ICommand ExitSimpleVoteCommand { get; private set; }
        public ICommand LogInGuestCommand { get; private set; } 

        private ICrowdDJBL bl = new CrowdDJBL();

        public SimpleVoteVM()
        {
            this.ExitSimpleVoteCommand = new RelayCommand(this.ExitSimpleVote);
            this.LogInGuestCommand = new RelayCommand(this.LogInGuest);
            AllParties = bl.GetAllParties();
            SelectedParty = AllParties.First();
        }

        private void LogInGuest(object obj)
        {
            if (bl.FindUserByEmail(Email) == null || bl.FindPartyById(PartyId) == null)
            {
                MessageBoxResult result = MessageBox.Show("Benutzer nicht vorhanden", "Error",
                                                            MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                Window window = new SimpleVoteUserWindow(PartyId);
                window.Show();
                Application.Current.Windows[0].Close();
            }
        }

        private void ExitSimpleVote(object obj)
        {
            Application.Current.Windows[0].Close();
        }

        private void UpdateQRCode()
        {
            Bitmap cur;
            QRCodeGenerator qrGenerator = new QRCodeGenerator();
            QRCodeGenerator.QRCode qrCode = qrGenerator.CreateQrCode(SelectedParty.PartyId, QRCodeGenerator.ECCLevel.Q);
            cur = qrCode.GetGraphic(20);
            QrPicture = BitMapConverter.ToWpfBitmap(cur);
        
        }

        
    }
}
