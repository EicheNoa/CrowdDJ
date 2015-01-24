using CrowdDJ.BL;
using CrowdDJ.DomainClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace CrowdDJ.Playstation.ViewModels
{
    class UserAddNewUserVM : ViewModelBase
    {
        private ICrowdDJBL bl = CrowdDJBL.GetCrowdDJBL();
        public string Name {get;set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public bool IsAdmin { get; set; }
        public ICommand AddNewUserCommand { get; private set; }
        public ICommand NewUserCancelCommand { get; private set; }

        public UserAddNewUserVM()
        {
            this.AddNewUserCommand = new RelayCommand(this.AddNewUser);
            this.NewUserCancelCommand = new RelayCommand(this.NewUserCancel);
            Name = ""; Email = ""; Password = ""; RePassword = "";

        }
        private void NewUserCancel(object obj)
        {
            CloseWindow("CrowdDJ.Playstation.Views.UserAddNewUserWindow");  
        }
        private void AddNewUser(object obj)
        {
            MessageBoxResult result;
            if (Name == "" || Password == "" || 
                RePassword == "" || Email == "")
            {
                result = MessageBox.Show("Alle Felder müssen ausgefüllt sein!", "Fehler",
                                                          MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else if (Password.Equals(RePassword))
            {
                bl.InsertUser(new User(Name, Password, Email, IsAdmin));
                CloseWindow("CrowdDJ.Playstation.Views.UserAddNewUserWindow");                
                result = MessageBox.Show("Benutzer wurde erfolgreich hinzugefügt!", "Erfolg",
                                                          MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                result = MessageBox.Show("Passwörter müssen gleich sein!", "Fehler",
                                                          MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
