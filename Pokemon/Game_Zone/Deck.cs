using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon.Game_Zone
{
    class Deck
    {
        public List<Pokemon> deck;

        public Deck()
        {
            deck = new List<Pokemon>();
        }
        public void AddToDeck(Pokemon card)
        {
            deck.Add(card);
        }

        public Pokemon DrawCard()
        {
            if(deck.Count > 0) {
                int indexing = deck.Count() - 1;
                Pokemon card = deck[indexing];
                deck.RemoveAt(indexing);
                if (deck.Count == 0)
                {
                    MessageBox.Show("You lose the game!");
                }
            return card;
            }
            else
            {
                return null;
            }            
                       
        }

        public void ShowCards()
        {
            string txt = "";

            for (int i = 0; i < deck.Count; i++)
            {
                txt += deck[i].Name + "\n";
            }

            Console.WriteLine("Your deck is: \n");
            Console.WriteLine(txt);
        }

        public void Shuffle()
        {
            Random rng = new Random();
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Pokemon value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }

        }
        public int NumberOfCards()
        {
            return deck.Count;
        }
    }
}
