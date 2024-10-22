using System;
using System.Collections.Generic;
using System.Diagnostics;
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

namespace CardRoulette
{
    /// <summary>
    /// Interaction logic for ReferenceWindow.xaml
    /// </summary>
    public partial class ReferenceWindow : Window
    {
        private GameWindow gameWindow;
        private StartWindow startWindow;
        
        public ReferenceWindow(string title)
        {
            InitializeComponent();
            Title.Text = title;
            DisplayContentGrid(title);
            gmailLink.NavigateUri = new Uri("https://mail.google.com/mail/?view=cm&fs=1&to=anhminh02111@gmail.com");
        }

        // Constructor that accepts a GameWindow reference
        public ReferenceWindow(GameWindow gameWindow, string title)
        {
            InitializeComponent();
            Title.Text = title;
            DisplayContentGrid(title);
            this.gameWindow = gameWindow;
            gmailLink.NavigateUri = new Uri("https://mail.google.com/mail/?view=cm&fs=1&to=anhminh02111@gmail.com");
            if (gameWindow != null)
            {
                Start.Visibility = Visibility.Collapsed;
            }
            else
            {
                Start.Visibility = Visibility.Visible;
            }
        }
        public ReferenceWindow(StartWindow startWindow, string title)
        {
            InitializeComponent();
            Title.Text = title;
            DisplayContentGrid(title);
            this.startWindow = startWindow;
            gmailLink.NavigateUri = new Uri("https://mail.google.com/mail/?view=cm&fs=1&to=anhminh02111@gmail.com");
            if (gameWindow != null || startWindow != null)
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
            if (gameWindow != null || startWindow != null)
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
            StartWindow startWindow = new StartWindow();
            startWindow.Show();
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
                if (gameWindow != null) gameWindow.Close();
                if (startWindow != null) startWindow.Close();
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

        private void Hyperlink_RequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            
            try
            {
                // Open the URL in the default browser (Gmail or GitHub)
                Process.Start(new ProcessStartInfo
                {
                    FileName = e.Uri.ToString(),
                    UseShellExecute = true
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error opening link: {ex.Message}");
            }

            // Mark the event as handled
            e.Handled = true;
        }
    }
}
