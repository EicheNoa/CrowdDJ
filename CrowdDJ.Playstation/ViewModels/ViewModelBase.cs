using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrowdDJ.Playstation.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        public ViewModelBase()
        {
        }
        public void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        public void CloseWindow(String win)
        {
            int i = 0;
            foreach (var item in Application.Current.Windows)
            {
                if (item.ToString().Equals(win))
                {
                    Application.Current.Windows[i].Close();
                }
                i++;
            }
        }
    }
}
