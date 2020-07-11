using Pokemon.Card;
using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Pokemon
{
    class Attack
    {
        public string name;
        public string type;
        public string information;
        public int damage;
        public EnergyCost energycost;
        public Attack(string name, string type, string information, int damage, EnergyCost energycost)
        {
            this.name = name;
            this.type = type;
            this.information = information;
            this.damage = damage;
            this.energycost = energycost;
        }

    }
}
