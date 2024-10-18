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
                case 1:
                    card = new Card("queen", "");
                    break;
            }
            return card;
        }

        //public List<Card> JackTableDeck()
        //{

        //}
    }
}
