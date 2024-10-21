using CardRoulette.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardRoulette.Controller
{
    public class ComputerController
    {
        public ComputerController() { }

        public int ComputerAction()
        {
            Random random = new Random();

            return random.Next(1, 10); 
        }

        public List<int> ComputerThrowCard(int numCard)
        {
            Random random = new Random();
            List<int> cards = new List<int>();
            int previous = -1;
            int index = 0;
            while (index < numCard)
            {
                int card = random.Next(0,4);
                if (card != previous)
                {
                    previous = card;
                    cards.Add(card);
                    index++;
                }

            }
            return cards;
        }
    }
}
