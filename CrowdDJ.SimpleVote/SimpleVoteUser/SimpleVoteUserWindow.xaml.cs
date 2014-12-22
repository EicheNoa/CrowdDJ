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

namespace CrowdDJ.SimpleVote.SimpleVoteUser
{
    /// <summary>
    /// Interaktionslogik für SimpleVoteUserWindow.xaml
    /// </summary>
    public partial class SimpleVoteUserWindow : Window
    {
        public SimpleVoteUserWindow(string PartyId)
        {
            InitializeComponent();
            this.DataContext = new SimpleVoterUserVM(PartyId);
        }
    }
}
