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

        public List<int> ComputerThrowCard(int numCard, List<int> selectedIndex)
        {
            Random random = new Random();
            List<int> cards = new List<int>();
            for (int i = 0; i < selectedIndex.Count; i++)
            {
                if (i < numCard)
                {
                    int card = selectedIndex[random.Next(0, selectedIndex.Count())];
                    if (cards.Contains(card))
                    {
                        i--;
                    }
                    else
                    {
                        cards.Add(card);
                    }
                }
            }
            return cards;
        }
    }
}
