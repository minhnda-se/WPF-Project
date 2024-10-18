using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardRoulette.Model
{
    public class Card
    {
      
        public string Name { get; set; }
        public string ImgUrl { get; set; }

        public Card() { }
        public Card( string name, string imgUrl)
        {
            Name = name;
            ImgUrl = imgUrl;
        }
    }
}
