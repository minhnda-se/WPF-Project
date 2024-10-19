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
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private int selectedCount;
        public GameWindow()
        {
            InitializeComponent();
            selectedCount = 0;
        }



        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Menu_Click(object sender, RoutedEventArgs e)
        {
            ReferenceWindow referenceWindow = new ReferenceWindow(this, "Tutorial");
            referenceWindow.Show();
        }

       

        private void Image_Click(object sender, RoutedEventArgs e)
        {
            Button clickedButton = sender as Button;
            Image image = clickedButton.Content as Image;
            if (clickedButton != null)
            {
                // If there's a previously selected button, reset it
                if (clickedButton.Background != null && clickedButton.Background is SolidColorBrush brush && brush.Color != Colors.Transparent)
                {
                    clickedButton.Background = null;
                    image.Opacity = 1;
                    selectedCount--;
                }
                else
                {
                    if (selectedCount < 3)
                    {
                        // Set the background of the selected button (you can customize this color)
                        clickedButton.Background = new SolidColorBrush(Colors.Black);
                        image.Opacity = 0.5;
                        selectedCount++;
                    }
                }

            }
        }

    }
}
