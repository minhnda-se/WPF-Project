using System.Text;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        public MainWindow()
        {
            InitializeComponent();
        }
       

        private void Quit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Tutorial_Click(object sender, RoutedEventArgs e)
        {
            SwitchToReferenceWindow("Tutorial");
            
        }
        private void Credits_Click(object sender, RoutedEventArgs e)
        {
            SwitchToReferenceWindow("Credits");
        }

        private void Start_Click(object sender, RoutedEventArgs e)
        {
            GameWindow gameWindow = new GameWindow();
            gameWindow.Show();
            this.Close();
        }

        private void SwitchToReferenceWindow(string title)
        {
            ReferenceWindow referenceWindow = new ReferenceWindow(title);
            referenceWindow.Show();
            this.Close();
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