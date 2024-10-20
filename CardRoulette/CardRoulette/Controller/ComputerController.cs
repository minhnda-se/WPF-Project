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
    }
}
