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
    /// Interaction logic for ReferenceWindow.xaml
    /// </summary>
    public partial class ReferenceWindow : Window
    {
        public ReferenceWindow(string title)
        {
            InitializeComponent();
            Title.Text = title;
            DisplayContentGrid(title);
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Tutorial_Click(object sender, RoutedEventArgs e)
        {
            Title.Text = "Tutorial";
            DisplayContentGrid("Tutorial");

        }
        private void Credits_Click(object sender, RoutedEventArgs e)
        {
            Title.Text = "Credits";
            DisplayContentGrid("Credits");
        }


      



        private void DisplayContentGrid(string title)
        {

            if (title.Equals("Tutorial"))
            {
                TutorialGrid.Visibility = Visibility.Visible;
                CreditsGrid.Visibility = Visibility.Collapsed;
            }
            else
            {
                CreditsGrid.Visibility = Visibility.Visible;
                TutorialGrid.Visibility = Visibility.Collapsed;
            }
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }
    }
}
