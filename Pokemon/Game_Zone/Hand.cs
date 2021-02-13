using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon.Game_Zone
{
    class Hand
    {
        public List<Pokemon> hand;

        public Hand()
        {
            hand = new List<Pokemon>();
        }

        public void DrawCard(Pokemon taken)
        {
            hand.Add(taken);
        }

        public void ShowHand()
        {
            string txt = "";

            for (int i = 0; i < hand.Count; i++)
            {
                txt += hand[i].Name + "\n";
            }

            Console.WriteLine("Your hand is: \n");
            Console.WriteLine(txt);
        }

        public string ShowCard(int num)
        {
            return hand[num].Img;
        }

        public string ShowName(int num)
        {
            return hand[num].Name;
        }

        public string ShowType(int num)
        {
            return hand[num].Type;
        }
        public string ShowImpact(int num)
        {
            return hand[num].Impact;
        }
        public char ShowWeakness(int num)
        {
            return hand[num].Weakness;
        }
        public char ShowResistance(int num)
        {
            return hand[num].Resistance;
        }
        public int ShowHP(int num)
        {
            return hand[num].Hp;
        }

        public int ShowRemHp(int num)
        {
            return hand[num].Rem_Hp;
        }

        public char ShowEnergy(int num)
        {
            return hand[num].Energy;
        }

        public string ShowFirstStage(int num)
        {
            return hand[num].Stage_1;
        }
        public string ShowSecondtStage(int num)
        {
            return hand[num].Stage_2;
        }
        public int NumberOfCards()
        {
            return hand.Count;
        }

        public int GetRetreatCost(int num)
        {
            return hand[num].Retreat;
        }

        public Pokemon ThisCard(int x) {
            return hand[x];
        }

        public void RemoveFromHand(int x)
        {
            hand.RemoveAt(x);
        }

        public void EnergyLoad(int x)
        {
            hand[x].LoadEnergy('c');
            MessageBox.Show("You loaded an energy");
        }

        public int NumOfBasicPokemon()
        {
            int count = 0;
            for(int i = 0; i < hand.Count; i++)
            {
                if(hand[i].Type == "basic")
                {
                    count++;
                }
            }
            return count;
        }
        public int NumOfBasicEnergies()
        {
            int count = 0;
            for (int i = 0; i < hand.Count; i++)
            {
                if (hand[i].Type == "energy")
                {
                    count++;
                }
            }
            return count;
        }
        public int ReturnFirstIndexMatchingEnergywithActivePokémon(Active active)
        {
            int index = 0;
            for (int i = 0; i < hand.Count; i++)
            {
                if(hand[i].Type == "energy" && hand[i].Energy == active.ShowEnergy())
                {
                    index = i;
                    return index;
                    break;
                }
                else if(hand[i].Type == "energy" && hand[i].Energy != active.ShowEnergy())
                {
                    index = i;
                    return index;
                    break;
                }
            }
            return index;
        }
    }
}
