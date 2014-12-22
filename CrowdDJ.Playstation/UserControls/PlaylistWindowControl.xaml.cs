using CrowdDJ.Playstation.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrowdDJ.Playstation.UserControls
{
    /// <summary>
    /// Interaktionslogik für PlaylistWindowControl.xaml
    /// </summary>
    public partial class PlaylistWindowControl : UserControl
    {
        ObservableCollection<Uri> youtubeLinks = new ObservableCollection<Uri>();
        public Uri SelectedItem { get; set; }
        int i = 0;
        public PlaylistWindowControl()
        {
            youtubeLinks.Add(new Uri(@"https://www.youtube.com/watch?v=uKDuIhNVLyg"));
            youtubeLinks.Add(new Uri(@"https://www.youtube.com/watch?v=TiVIFMbwxOc"));
            youtubeLinks.Add(new Uri(@"https://www.youtube.com/watch?v=Sj9A-5VmDTE"));
            SelectedItem = youtubeLinks.First();
            InitializeComponent();
            this.DataContext = new PlaylistVM();
        }

        private void bttnRewind_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem == youtubeLinks.First())
            {
                mediaPlay.Play();
            }
            else
            {
                i--;
                SelectedItem = youtubeLinks[i];
                mediaPlay.Play();
            }
            
                
        }

        private void bttnNext_Click(object sender, RoutedEventArgs e)
        {
            if (SelectedItem == youtubeLinks.Last())
            {
                mediaPlay.Play();
            }
            else
            {
                i++;
                SelectedItem = youtubeLinks[i];
                mediaPlay.Play();
            }
        }

        private void bttnPlay_Click(object sender, RoutedEventArgs e)
        {
            mediaPlay.Play();
        }

        private void bttnStop_Click(object sender, RoutedEventArgs e)
        {
            mediaPlay.Stop();
        }

        private void bttnPause_Click(object sender, RoutedEventArgs e)
        {
            mediaPlay.Pause();
        }
    }
}
