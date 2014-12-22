using CrowdDJ.Playstation.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CrowdDJ.Playstation.Views
{
    /// <summary>
    /// Interaktionslogik für PlaylistAddTrackWindow.xaml
    /// </summary>
    public partial class PlaylistAddTrackWindow : Window
    {
        public PlaylistAddTrackWindow()
        {
            InitializeComponent();
            this.DataContext = new PlaylistAddNewTrackVM();
        }
    }
}
