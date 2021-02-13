using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Game_Zone
{
    class Prize
    {
        public List<Pokemon> prize;

        public Prize()
        {
            prize = new List<Pokemon>();
        }
        public void AddXPrizes(int x, Deck player)
        {
            int i = 0;
            while(i < x)
            {
                prize.Add(player.DrawCard());
                i++;
            }     
        }
        public Pokemon ThisCard(int x)
        {
            return prize[x];
        }
        public int TotalNumber()
        {
            return prize.Count;
        }
        public void FetchPrize(int x, Hand hand)
        {
            hand.DrawCard(prize[x]);
            prize.RemoveAt(x);
        }
    }
}
