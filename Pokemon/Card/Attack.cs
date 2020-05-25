using Pokemon.Card;

namespace Pokemon
{
    class Attack
    {
        public string name;
        public string information;
        public int damage;
        public EnergyCost energycost;

        public Attack(string name, string information, int damage, EnergyCost energycost)
        {
            this.name = name;
            this.information = information;
            this.damage = damage;
            this.energycost = energycost;

        }
    }
}
