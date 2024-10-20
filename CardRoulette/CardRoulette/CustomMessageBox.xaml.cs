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

namespace CardRoulette
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public CustomMessageBox()
        {
            InitializeComponent();
        }
      
        public async Task UserTakeShoot()
        {
            await Task.Delay(1000);
            secondAction.Visibility = Visibility.Visible;
            await Task.Delay(1000);
            thirdAction.Visibility = Visibility.Visible;
            await Task.Delay(1000);
            finalAction.Visibility = Visibility.Visible;
            await Task.Delay(1000);
            this.Close();
        }
    }
}
