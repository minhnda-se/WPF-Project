using CardRoulette.Controller;
using CardRoulette.Model;
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
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace CardRoulette
{
    /// <summary>
    /// Interaction logic for GameWindow.xaml
    /// </summary>
    public partial class GameWindow : Window
    {
        private int selectedCount;

       
        private List<Button>  selectedCard;
        private Card tableCard;
        private DispatcherTimer timer;
        private string fullText;
        private int currentIndex;
        private List<Button> computerDeck;
        private List<Button> userDeck;
        private CardController cardController;
        public GameWindow(Card tableCard)
        {
            InitializeComponent();
            selectedCount = 0;
            cardController = new CardController();
            selectedCard = new List<Button>();
            this.tableCard = tableCard;
            TableCard.Source = new BitmapImage(new Uri(tableCard.ImgUrl, UriKind.RelativeOrAbsolute));
            computerDeck = new List<Button> { Card1, Card2, Card3, Card4, Card5 };
            userDeck = new List<Button> { UserCard1, UserCard2, UserCard3, UserCard4, UserCard5 };
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

       private void Throw_Click(object sender, RoutedEventArgs e)
        {
            if (selectedCard.Count > 0)
            {
               foreach (Button card in selectedCard)
                {
                    card.Visibility = Visibility.Collapsed;
                }
                CardThrowing.Text = tableCard.Name+" x" + selectedCard.Count.ToString();
                selectedCard.Clear();
                selectedCount = 0;
            }
           
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
                    selectedCard.Remove(clickedButton);

                }
                else
                {
                    if (selectedCount < 3)
                    {
                        // Set the background of the selected button
                        clickedButton.Background = new SolidColorBrush(Colors.Black);
                        image.Opacity = 0.5;
                        selectedCount++;
                        selectedCard.Add(clickedButton);
                        MessageBox.Show(selectedCard[0].Tag.ToString());
                    }
                }

            }
        }


        private void StartTypingEffect(TextBlock textBlock, string text, int speedInMs)
        {
            // Reset the current text state and setup initial conditions
            fullText = text;
            currentIndex = 0;

            // Initialize the timer with the desired speed (in ms)
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromMilliseconds(speedInMs) // Speed of typing (in ms)
            };

            // Event handler for each timer tick
            timer.Tick += (sender, e) =>
            {
                if (currentIndex < fullText.Length)
                {
                    // Append one character from the string to the TextBlock
                    textBlock.Text += fullText[currentIndex];
                    currentIndex++;
                }
                else
                {
                    // Stop the timer once all the text has been displayed
                    timer.Stop();
                }
            };

            // Start the timer to animate the text
            timer.Start();
        }

        // Example usage: Start typing effect when the window is loaded or on any other event
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            // Start typing effect for TextBlock with desired speed (100 ms between characters)
            StartTypingEffect(ComputerText, "Welcome, Player!!", 50);
            SetComputerDeck();
            SetUserDeck();
            
        }

        private void  SetComputerDeck()
        {
            List<Card> deck = cardController.CardDeck();
            foreach (Button card in computerDeck)
            {
                
                Random random = new Random();
                Card getCard = deck[random.Next(0, userDeck.Count)];
                card.Tag = getCard.Name;
                deck.Remove(getCard);
            }
        }
        private void SetUserDeck()
        {
            List<Card> deck = cardController.CardDeck();
            foreach (Button card in userDeck)
            {
                
                Random random = new Random();
                Card getCard = deck[random.Next(0,userDeck.Count)];
                Image image = card.Content as Image;
                image.Source = new BitmapImage(new Uri(getCard.ImgUrl, UriKind.RelativeOrAbsolute));
                card.Tag= getCard.Name;
                deck.Remove(getCard);
            }
        }

    }
}
