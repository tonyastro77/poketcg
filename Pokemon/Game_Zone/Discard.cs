using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Game_Zone
{
    class Discard
    {
        public List<Pokemon> discard;

        public Discard()
        {
            discard = new List<Pokemon>();
        }
        public void Add(Pokemon x)
        {
            discard.Add(x);
        }

        public string ShowCard(int x)
        {
            return discard[x].Img;
        }
        public int TotalNumber()
        {
            return discard.Count;
        }

        public string ShowName(int x)
        {
            return discard[x].Name;
        }
        
        public string ShowTopImage()
        {
            int index = discard.Count - 1;
            return discard[index].Img;
        }

    }
}
