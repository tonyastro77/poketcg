

namespace Pokemon.Card
{
    class EnergyCost
    {
        public int colorless, earth, fire, grass, lightning, psychic, water;

        public EnergyCost(int colorless, int earth, int fire, int grass, int lightning, int psychic, int water)
        {
            this.colorless = colorless;
            this.earth = earth;
            this.fire = fire;
            this.grass = grass;
            this.lightning = lightning;
            this.psychic = psychic;
            this.water = water;
        }
        
        public int SumOfEnergies() 
        {
            return (colorless + earth + fire + lightning + psychic + water);     
        }
    }
}
