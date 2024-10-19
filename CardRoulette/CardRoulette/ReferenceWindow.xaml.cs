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
        private GameWindow gameWindow;
        
        public ReferenceWindow(string title)
        {
            InitializeComponent();
            Title.Text = title;
            DisplayContentGrid(title);
        }

        // Constructor that accepts a GameWindow reference
        public ReferenceWindow(GameWindow gameWindow, string title)
        {
            InitializeComponent();
            Title.Text = title;
            DisplayContentGrid(title);
            this.gameWindow = gameWindow;
            if (gameWindow != null)
            {
                Start.Visibility = Visibility.Collapsed;
            }
            else
            {
                Start.Visibility = Visibility.Visible;
            }
        }
        private void Return_Click(object sender, RoutedEventArgs e)
        {
            if (gameWindow != null)
            {
                this.Close();
            }
            else
            {
                MainWindow mainWindow = new MainWindow();
                mainWindow.Show();
                this.Close();
            }
        }
        private void Start_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show();
            this.Close();
        }
        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to exit?",
                "Exit Confirmation",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning
            );

            // If the user clicks Yes, close both windows.
            if (result == MessageBoxResult.Yes)
            {
                this.Close();
                gameWindow.Close();
            }
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
