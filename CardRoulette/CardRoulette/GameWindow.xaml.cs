using CardRoulette.Controller;
using CardRoulette.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
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
        private int userCardCount;
        private int computerCardCount;
        private List<Button> computerDeck;
        private List<Button> userDeck;
        private List<string> selectedCardText;
        private List<string> computerSelected;
        private List<int> selectedIndex;
        private List<Image> throwingCards;
        private CardController cardController;
        private ComputerController computerController;
        private ReferenceWindow referenceWindow;
        private bool isEndTurn;

        public GameWindow(Card tableCard)
        {
            InitializeComponent();
            selectedCount = 0;
            userCardCount = 0;
            computerCardCount = 0;
            isEndTurn = false;
            cardController = new CardController();
            computerController = new ComputerController();
            referenceWindow  = new ReferenceWindow(this, "Tutorial");
            selectedCard = new List<Button>();
            selectedCardText = new List<string>();
            computerSelected = new List<string>();
            selectedIndex = new List<int> { 0, 1, 2, 3, 4};
            this.tableCard = tableCard;
            TableCard.Source = new BitmapImage(new Uri(tableCard.ImgUrl, UriKind.RelativeOrAbsolute));
            computerDeck = new List<Button> { Card1, Card2, Card3, Card4, Card5 };
            userDeck = new List<Button> { UserCard1, UserCard2, UserCard3, UserCard4, UserCard5 };
            throwingCards = new List<Image> { ShowCard1, ShowCard2, ShowCard3, };
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
            
            referenceWindow.Show();
        }

       private async void Throw_Click(object sender, RoutedEventArgs e)
        {
            isEndTurn = false;
            //User throw card
            if (selectedCard.Count > 0)
            {
                btnThrow.IsEnabled = false;
                btnLiar.IsEnabled = false;
                foreach (Button card in selectedCard)
                {
                    card.Visibility = Visibility.Collapsed;
                    selectedCardText.Add(card.Tag.ToString());
                }
                CardThrowing.Text = tableCard.Name + " x" + selectedCard.Count.ToString();
                PlayerThrowing.Text = "USER THREW";
                userCardCount += selectedCard.Count;
                ComputerText.Text = null;
               
                await Task.Delay(1200);
                //Computer guess
                //computer with go with liar with 60% changes
                if (computerController.ComputerAction() >= 5)
                {

                    await StartTypingEffect(ComputerText, "Liar!!", 50);
                    await Task.Delay(1000);
                    int index = 0;
                    foreach (Button card in selectedCard)
                    {
                        Image image = card.Content as Image;

                        if (card.Tag.Equals("Joker")) {
                            Card crd = cardController.GetCardByName(card.Tag.ToString());
                            throwingCards[index].Source = new BitmapImage(new Uri(crd.ImgUrl, UriKind.RelativeOrAbsolute));
                        }
                        else {
                            throwingCards[index].Source = new BitmapImage(new Uri(image.Source.ToString(), UriKind.RelativeOrAbsolute));
                        }
                        throwingCards[index].Visibility = Visibility.Visible;
                        index++;
                        await Task.Delay(200);
                    }
                    await Task.Delay(1000);
                    ComputerText.Text = null;
                    if (cardController.IsTableCard(selectedCardText, tableCard.Name))
                    {
                        await StartTypingEffect(ComputerText, "Fuck!!", 50);
                        bool result = await ComputerTakeShoot();
                        CheckStatus(result, "Victory");

                    }
                    else
                    {
                        await StartTypingEffect(ComputerText, "Hah!! I got your $ss.", 50);
                        bool result = await UserTakeShoot();
                        CheckStatus(result, "Game Over");
                    }
                }
            
                else
                {
                    await StartTypingEffect(ComputerText, "Good Move, Good Move!! hmm", 50);
                    await Task.Delay(1000);


                }
                if (userCardCount == 5)
                {
                    await StartTypingEffect(ComputerText, "Well play!! Victory is your", 50);
                    await Task.Delay(1000);
                    NoCardLeftEnding("User");
                }
                selectedCard.Clear();
                selectedCount = 0;
                selectedCardText.Clear();
                //Computer throw card
                if (!isEndTurn)
                {
                    await StartTypingEffect(ComputerText, "It's my turn!", 50);
                    await Task.Delay(1000);
                    computerSelected.Clear();
                    Random random = new Random();
                    List<int> cards = computerController.ComputerThrowCard(random.Next(1, 3), selectedIndex);
                    PlayerThrowing.Text = "COMPUTER THREW";
                    CardThrowing.Text = tableCard.Name + " x" + cards.Count.ToString();
                    foreach (int index in cards)
                    {
                        computerDeck[index].Visibility = Visibility.Collapsed;
                        computerSelected.Add(computerDeck[index].Tag.ToString());
                        selectedIndex.Remove(index);
                    }
                    CardThrowing.Text = tableCard.Name + " x" + cards.Count.ToString();
                    if (selectedIndex.Count == 0)
                    {
                        await StartTypingEffect(ComputerText, "You still can not beat me, Loser!!", 50);
                        await Task.Delay(1000);
                        NoCardLeftEnding("Computer");
                    }
                    btnLiar.IsEnabled = true;
                    btnThrow.IsEnabled = true;
                }

                
            }
        }

        private async void Liar_Click(object sender, RoutedEventArgs e)
        {

            int index = 0;
            foreach (String cardName in computerSelected)
            {
                Card card = cardController.GetCardByName(cardName);

                if (card.Name.Equals("Joker"))
                {
                    Card crd = cardController.GetCardByName(tableCard.ToString());
                    throwingCards[index].Source = new BitmapImage(new Uri(crd.ImgUrl, UriKind.RelativeOrAbsolute));
                }
                else
                {
                    throwingCards[index].Source = new BitmapImage(new Uri(card.ImgUrl.ToString(), UriKind.RelativeOrAbsolute));
                }
                throwingCards[index].Visibility = Visibility.Visible;
                index++;
                await Task.Delay(200);
                
            }
            await Task.Delay(1000);
            if (!cardController.IsTableCard(computerSelected, tableCard.Name))
            {
                await StartTypingEffect(ComputerText, "Fuck!!", 50);
                bool result = await ComputerTakeShoot();
                CheckStatus(result, "Victory");

            }
            else
            {
                await StartTypingEffect(ComputerText, "Hah!! I got your $ss.", 50);
                bool result = await UserTakeShoot();
                CheckStatus(result, "Game Over");
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
                    }
                }

            }
        }


        private async Task StartTypingEffect(TextBlock textBlock, string text, int speedInMs)
        {
            // Reset the current text state and setup initial conditions
            fullText = text;
            currentIndex = 0;
            textBlock.Text = string.Empty;  // Clear any existing text

            // Simulate the typing effect asynchronously
            while (currentIndex < fullText.Length)
            {
                // Append one character from the string to the TextBlock
                textBlock.Text += fullText[currentIndex];
                currentIndex++;

                // Wait for the specified speed before continuing to the next character
                await Task.Delay(speedInMs);
            }
        }


        // Example usage: Start typing effect when the window is loaded or on any other event
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StartTypingEffect(ComputerText, "Welcome!! Challenger", 50);
            SetComputerDeck();
            SetUserDeck();
        }
        private void RestartGame()
        {
            // Start typing effect for TextBlock with desired speed (100 ms between characters)
            
            for(int i = 0; i < userDeck.Count; i++)
            {
                userDeck[i].Visibility = Visibility.Visible;
                userDeck[i].Background = null;
                Image image = userDeck[i].Content as Image;
                image.Opacity = 1;
                computerDeck[i].Visibility = Visibility.Visible;
                if (i < 3) throwingCards[i].Visibility = Visibility.Collapsed;
                
            }
            tableCard = cardController.GetTableCard();
            TableCard.Source = new BitmapImage(new Uri(tableCard.ImgUrl, UriKind.RelativeOrAbsolute));
            SetComputerDeck();
            SetUserDeck();
            ComputerText.Text = null;
            userCardCount = 0;
            computerCardCount = 0;
            PlayerThrowing.Text = string.Empty;
            CardThrowing.Text = string.Empty;
            StartTypingEffect(ComputerText, "Try again", 50);
            selectedIndex = new List<int> {0, 1, 2, 3, 4 };
            btnLiar.IsEnabled = false;
            btnThrow.IsEnabled = true;
            isEndTurn = true;

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
        private void NoCardLeftEnding(string player)
        {
            string title = player.Equals("User") ? "Victory" : "Game Over";
            GameEndWindow gameEndWindow = new GameEndWindow(title);
            gameEndWindow.Show();
            this.Close();
            referenceWindow.Close();
        }

        private async Task<bool> ComputerTakeShoot()
        {
            bool isDead = true;
            CustomMessageBox customMessageBox = new CustomMessageBox();
            customMessageBox.Show();
            await customMessageBox.UserTakeShoot();
            Random random = new Random();
            if (random.Next(1, 6) > 3)
            {

                // Close the custom message box
                var result = MessageBox.Show("Computer survive!");
                if (result == MessageBoxResult.OK)
                {
                   isDead = false;
                }

            }
            else
            {
                var result = MessageBox.Show("Computer is dead!");
                if (result == MessageBoxResult.OK)
                {
                    isDead = true;
                }

            }
            return isDead;

        }
        private async Task<bool> UserTakeShoot()
        {
            bool isDead = true;
            CustomMessageBox customMessageBox = new CustomMessageBox();
            customMessageBox.Show();
            await customMessageBox.UserTakeShoot();
            Random random = new Random();
            if (random.Next(1, 6) > 3)
            {

                // Close the custom message box
                var result = MessageBox.Show(" How lucky are you!!");
                if (result == MessageBoxResult.OK)
                {
                    isDead = false;
                }

            }
            else
            {
                var result = MessageBox.Show("You die!!");
                if (result == MessageBoxResult.OK)
                {
                    isDead = true;
                }

            }
            return isDead;

        }

        private void CheckStatus(bool result, string title)
        {
            if (result)
            {
                GameEndWindow gameEndWindow = new GameEndWindow(title);
                gameEndWindow.Show();
                this.Close();
                referenceWindow.Close();

            }
            RestartGame();
            
        }


    }
}
