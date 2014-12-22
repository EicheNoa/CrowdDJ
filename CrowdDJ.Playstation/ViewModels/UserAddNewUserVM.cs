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
        private ICrowdDJBL bl;
        public string Name {get;set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public string RePassword { get; set; }
        public bool IsAdmin { get; set; }
        public ICommand AddNewUserCommand { get; private set; }
        public ICommand NewUserCancelCommand { get; private set; }

        public UserAddNewUserVM()
        {
            bl = new CrowdDJBL();
            this.AddNewUserCommand = new RelayCommand(this.AddNewUser);
            this.NewUserCancelCommand = new RelayCommand(this.NewUserCancel);

        }
        private void NewUserCancel(object obj)
        {
            Application.Current.Windows[1].Close();
        }
        private void AddNewUser(object obj)
        {
            MessageBoxResult result;
            if (Password.Equals(RePassword))
            {
                bl.InsertUser(new User(Name, Password, Email, IsAdmin));
                Application.Current.Windows[1].Close();
                result = MessageBox.Show("User has successfully been added!", "Success",
                                                          MessageBoxButton.OK, MessageBoxImage.Exclamation);
            }
            else
            {
                result = MessageBox.Show("Passwords are not equal!", "Error",
                                                          MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
    }
}
