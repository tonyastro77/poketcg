

using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

namespace Pokemon
{
    class Pokemon
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }

        public string Impact { get; set; }
        public string Effect { get; set; }
        public string Stage_1 {get; set;}
        public string Stage_2 { get; set;}
        public int Hp { get; set; }
        public int Rem_Hp { get; set; }
        public char Energy { get; set; }
        public char Weakness { get; set; }
        public char Resistance { get; set; }
        public int Retreat { get; set; }
        public string Pokedex { get; set; }
        public string Img { get; set; }

        public Attack attack1, attack2, attack3;

        public List<char> EnergyLoaded;

        public bool CanEvolve { get; set; }

        public Pokemon(int id)
        {
            this.Id = id;
        }

        // Any card that is not a pokemon, can be an energy or trainer card
        public Pokemon(int id, string name, string type, char energy, string Img)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Img = Img;
  
        }

        public Pokemon(int id, string name, string type, string impact, char energy, string effect, string Img)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Impact = impact;
            this.Effect = effect;
            this.Img = Img;

        }

        // A basic pokemon with one attack or pokemon power
        public Pokemon(int id, string name, string type, int hp, int remhp, char energy, char weakness, char resistance, int retreat, string pokedex, string Img, Attack attack1, List<char> energyLoaded, bool CanEvolve)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Hp = hp;
            this.Rem_Hp = remhp;
            this.Energy = energy;
            this.Weakness = weakness;
            this.Resistance = resistance;
            this.Retreat = retreat;
            this.Pokedex = pokedex;
            this.Img = Img;
            this.attack1 = attack1;
            this.EnergyLoaded = energyLoaded;
            this.CanEvolve = CanEvolve;
        }

        // A stage 2 evolution with one attack or pokemon power
        public Pokemon(int id, string name, string type, string stage_1, int hp, int remhp, char energy, char weakness, char resistance, int retreat, string pokedex, string Img, Attack attack1, List<char> energyLoaded, bool CanEvolve)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Stage_1 = stage_1;
            this.Hp = hp;
            this.Rem_Hp = remhp;
            this.Energy = energy;
            this.Weakness = weakness;
            this.Resistance = resistance;
            this.Retreat = retreat;
            this.Pokedex = pokedex;
            this.Img = Img;
            this.attack1 = attack1;
            this.EnergyLoaded = energyLoaded;
            this.CanEvolve = CanEvolve;
        }

        // A stage 3 evolution with one attack or pokemon power
        public Pokemon(int id, string name, string type, string stage_1, string stage_2, int hp, int remhp, char energy, char weakness, char resistance, int retreat, string pokedex, string Img, Attack attack1, List<char> energyLoaded, bool CanEvolve)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Stage_1 = stage_1;
            this.Stage_2 = stage_2;
            this.Hp = hp;
            this.Rem_Hp = remhp;
            this.Energy = energy;
            this.Weakness = weakness;
            this.Resistance = resistance;
            this.Retreat = retreat;
            this.Pokedex = pokedex;
            this.Img = Img;
            this.attack1 = attack1;
            this.EnergyLoaded = energyLoaded;
            this.CanEvolve = CanEvolve;
        }

        // A basic pokemon with 2 attacks or pokemon powers
        public Pokemon(int id, string name, string type, int hp, int remhp, char energy, char weakness, char resistance, int retreat, string pokedex, string Img, Attack attack1, Attack attack2, List<char> energyLoaded, bool CanEvolve)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Hp = hp;
            this.Rem_Hp = remhp;
            this.Energy = energy;
            this.Weakness = weakness;
            this.Resistance = resistance;
            this.Retreat = retreat;
            this.Pokedex = pokedex;
            this.Img = Img;
            this.attack1 = attack1;
            this.attack2 = attack2;
            this.EnergyLoaded = energyLoaded;
            this.CanEvolve = CanEvolve;
        }

        // a stage 2 evolution with 2 attacks or pokemon powers
        public Pokemon(int id, string name, string type, string stage_1, int hp, int remhp, char energy, char weakness, char resistance, int retreat, string pokedex, string Img, Attack attack1, Attack attack2, List<char> energyLoaded, bool CanEvolve)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Stage_1 = stage_1;
            this.Hp = hp;
            this.Rem_Hp = remhp;
            this.Energy = energy;
            this.Weakness = weakness;
            this.Resistance = resistance;
            this.Retreat = retreat;
            this.Pokedex = pokedex;
            this.Img = Img;
            this.attack1 = attack1;
            this.attack2 = attack2;
            this.EnergyLoaded = energyLoaded;
            this.CanEvolve = CanEvolve;
        }

        // a stage 3 evolution with 2 attacks or pokemon powers
        public Pokemon(int id, string name, string type, string stage_1, string stage_2, int hp, int remhp, char energy, char weakness, char resistance, int retreat, string pokedex, string Img, Attack attack1, Attack attack2, List<char> energyLoaded, bool CanEvolve)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Stage_1 = stage_1;
            this.Stage_2 = stage_2;
            this.Hp = hp;
            this.Rem_Hp = remhp;
            this.Energy = energy;
            this.Weakness = weakness;
            this.Resistance = resistance;
            this.Retreat = retreat;
            this.Pokedex = pokedex;
            this.Img = Img;
            this.attack1 = attack1;
            this.attack2 = attack2;
            this.EnergyLoaded = energyLoaded;
            this.CanEvolve = CanEvolve;
        }

        // A basic pokemon with 3 attacks or pokemon powers
        public Pokemon(int id, string name, string type, int hp, int remhp, char energy, char weakness, char resistance, int retreat, string pokedex, string Img, Attack attack1, Attack attack2, Attack attack3, List<char> energyLoaded, bool CanEvolve)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Hp = hp;
            this.Rem_Hp = remhp;
            this.Energy = energy;
            this.Weakness = weakness;
            this.Resistance = resistance;
            this.Retreat = retreat;
            this.Pokedex = pokedex;
            this.Img = Img;
            this.attack1 = attack1;
            this.attack2 = attack2;
            this.attack3 = attack3;
            this.EnergyLoaded = energyLoaded;
            this.CanEvolve = CanEvolve;
        }

        // a stage 2 evolution with 3 attacks or pokemon powers
        public Pokemon(int id, string name, string type, string stage_1, int hp, int remhp, char energy, char weakness, char resistance, int retreat, string pokedex, string Img, Attack attack1, Attack attack2, Attack attack3, List<char> energyLoaded, bool CanEvolve)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Stage_1 = stage_1;
            this.Hp = hp;
            this.Rem_Hp = remhp;
            this.Energy = energy;
            this.Weakness = weakness;
            this.Resistance = resistance;
            this.Retreat = retreat;
            this.Pokedex = pokedex;
            this.Img = Img;
            this.attack1 = attack1;
            this.attack2 = attack2;
            this.attack3 = attack3;
            this.EnergyLoaded = energyLoaded;
            this.CanEvolve = CanEvolve;
        }

        // a stage 3 evolution with 3 attacks or pokemon powers
        public Pokemon(int id, string name, string type, string stage_1, string stage_2, int hp, int remhp, char energy, char weakness, char resistance, int retreat, string pokedex, string Img, Attack attack1, Attack attack2, Attack attack3, List<char> energyLoaded, bool CanEvolve)
        {
            this.Id = id;
            this.Name = name;
            this.Type = type;
            this.Stage_1 = stage_1;
            this.Stage_2 = stage_2;
            this.Hp = hp;
            this.Rem_Hp = remhp;
            this.Energy = energy;
            this.Weakness = weakness;
            this.Resistance = resistance;
            this.Retreat = retreat;
            this.Pokedex = pokedex;
            this.Img = Img;
            this.attack1 = attack1;
            this.attack2 = attack2;
            this.attack3 = attack3;
            this.EnergyLoaded = energyLoaded;
            this.CanEvolve = CanEvolve;
        }

        public void LoadEnergy(char x)
        {
            EnergyLoaded.Add(x);
        }

        public void EnergyDelete(int x)
        {
            EnergyLoaded.RemoveAt(x);
        }
        
    }
}
