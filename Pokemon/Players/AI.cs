using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Players
{
    class AI
    {
        public string name;
        public List<Pokemon> deck = new List<Pokemon>();

        public AI(string name)
        {
            this.name = name;
        }
        public void Draw()
        {
            Console.WriteLine("The computer draws one card");
        }

        public void DrawAtBeginning()
        {
            Console.WriteLine("The computer draws 8 cards");
        }
       
        public int PokemonHP()
        {
            return deck[0].Hp;
        }


        public int Attack()
        {
            return deck[0].Hp - deck[0].attack1.damage;
        }

        public int GetHP()
        {
            return deck[0].Hp;
        }


        public int GetDamage()
        {
            return deck[0].attack1.damage;
        }

        public string GetName()
        {
            return deck[0].Name;
        }

        public int Attack(int hp, int damage)
        {
            int resulting_hp = hp - damage;

            return resulting_hp;
        }
    }
}
