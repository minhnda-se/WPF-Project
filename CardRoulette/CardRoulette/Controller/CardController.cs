using CardRoulette.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardRoulette.Controller
{
    public class CardController
    {

        public CardController() { }

        public Card GetCard(int number)
        {
            Card card = new Card();
            switch (number)
            {
                case 0:
                    card = new Card("Joker", "Imgs/Joker.png");
                    break;
                case 1:
                    card = new Card("Queen", "Imgs/Queen.png");
                    break;
                case 2:
                    card = new Card("King", "Imgs/King.png");
                    break;
                case 3:
                    card = new Card("Jack", "Imgs/Jack.png");
                    break;
            }
            return card;
        }

        public Card GetCardByName(string cardName)
        {
            int index = 0;
            if (cardName.Equals("Queen")) index = 1;
            else if (cardName.Equals("King")) index = 2;
            else if (cardName.Equals("Jack")) index = 3;
            return GetCard(index);
        }

        public Card GetTableCard()
        {
            Random random = new Random();
            return GetCard(random.Next(1, 3));
        }

        public List<Card> CardDeck()
        {
            return new List<Card>
            {
                new Card("Joker", "Imgs/Joker.png"),
                new Card("Joker", "Imgs/Joker.png"),
                new Card("Queen", "Imgs/Queen.png"),
                new Card("Queen", "Imgs/Queen.png"),
                new Card("Queen", "Imgs/Queen.png"),
                new Card("King", "Imgs/King.png"),
                new Card("King", "Imgs/King.png"),
                new Card("King", "Imgs/King.png"),
                new Card("Jack", "Imgs/Jack.png"),
                new Card("Jack", "Imgs/Jack.png"),
                new Card("Jack", "Imgs/Jack.png")
            };
        }

        public bool IsTableCard(List<string> cards, string tableCard)
        {
            int count = 0;
            foreach (string card in cards)
            {
                if (card.Equals(tableCard) || card.Equals("Joker")) count++;
            }
            if (count == cards.Count) return true;
            return false;
        }

        public List<int> GetGun()
        {
            var turn = new List<int>();
            Random random = new Random();
            int index = random.Next(0, 3);
            for (int i = 0; i < 4; i++)
            {
                int bullet = 0;
                if (i == index) bullet = 1;
                turn.Add(bullet);
            }
            return turn;
        }
        
    }
}
