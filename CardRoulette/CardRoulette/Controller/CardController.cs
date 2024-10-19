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
        
    }
}
