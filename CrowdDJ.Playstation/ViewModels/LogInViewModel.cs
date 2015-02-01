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
using System.Windows.Controls;
using System.Windows.Input;

namespace CrowdDJ.Playstation.ViewModels
{
    class LogInViewModel : ViewModelBase
    {
        private string email;
        public string Email
        {
            get { return email; }
            set { email = value; }
        }

        private string password;

        public string Password
        {
            get { return password; }
            set { password = value; }
        }

        private List<KeyValuePair<string, bool>> allUser;
        public List<KeyValuePair<string, bool>> AllUser
        {
            get { return allUser; }
            set { allUser = value; }
        }


        public ICommand LoginCommand { get; set; }
        public ICommand CancelLoginCommand { get; set; }

        private ICrowdDJBL bl = null;

        public LogInViewModel()
        {
            bl = CrowdDJBL.GetCrowdDJBL();
            AllUser = bl.GetAllEmails();
            this.LoginCommand = new RelayCommand(this.Login);
            this.CancelLoginCommand = new RelayCommand(this.CancelLogin);
        }

        private void CancelLogin(object obj)
        {
            Application.Current.Shutdown(0);      
        }

        private void Login(object obj)
        {
            Password = ((PasswordBox)obj).Password;
            MessageBoxResult result;
            User dummyUser = null;
            foreach (var item in AllUser)
            {
                if (item.Key.Equals(Email))
                {
                    if (item.Value.Equals(true))
                    {
                        dummyUser = bl.FindUserByEmail(Email);
                        if (dummyUser != null && dummyUser.Password.Equals(Password.GetHashCode().ToString()))
                        {
                            Window window = new MainWindowLayout();
                            window.Show();
                            Application.Current.Windows[0].Close();
                        }
                        else
                        {
                            result = MessageBox.Show("Ungültige Eingabe! Passwort / Email kombination falsch!", "Fehler",
                                                     MessageBoxButton.OK, MessageBoxImage.Error);
                        }   
                    }
                    else
                    {
                        result = MessageBox.Show("Sie sind kein Administrator!", "Fehler",
                                                 MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }         
        }
    }
}
