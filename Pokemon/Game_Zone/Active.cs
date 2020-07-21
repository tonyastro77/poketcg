using Microsoft.SqlServer.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Game_Zone
{
    class Active
    {
        public Pokemon Active_Pokemon { get; set; }

        public Active()
        {
            Active_Pokemon = new Pokemon(0, "null", "", 'u', ""); 
        }

        public bool ThereIsActivePokemon()
        {
            bool existance;
            if(Active_Pokemon != null)
            {
                existance = true;
            }
            else
            {
                existance = false;
            }
            return existance;
        }
        public Pokemon GetActivePokemon()
        {
            return Active_Pokemon;
        }

        public string ShowName()
        {
            return Active_Pokemon.Name;
        }

        public int ShowRetreatCost()
        {
            return Active_Pokemon.Retreat;
        }

        public string ShowImage()
        {
            return Active_Pokemon.Img;
        }

        public int ShowHP()
        {
            return Active_Pokemon.Hp;
        }
        public int ShowRemHP()
        {
            return Active_Pokemon.Rem_Hp;
        }
        public char ShowEnergy()
        {
            return Active_Pokemon.Energy;
        }

        //Returns number of Attacks
        public int NumberOfAttacks()
        {
            if (Active_Pokemon.attack1 != null & Active_Pokemon.attack2 != null & Active_Pokemon.attack3 != null)
            {
                return 3;
            }
            else if (Active_Pokemon.attack1 != null & Active_Pokemon.attack2 != null & Active_Pokemon.attack3 == null)
            {
                return 2;
            }
            else if (Active_Pokemon.attack1 != null & Active_Pokemon.attack2 == null & Active_Pokemon.attack3 == null)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }

        //Returns attack names available from the Active Pokémon
        public string ShowAttackName(int x)
        {
            switch (x)
            {
                case 1:
                   return Active_Pokemon.attack1.name;
                case 2:
                    return Active_Pokemon.attack2.name;
                case 3:
                    return Active_Pokemon.attack3.name;
                default:
                    return null;
            }    
        }

        //Returns number of loaded energies to the Active Pokémon
        public int EnergyLoadedCount()
        {
            return Active_Pokemon.EnergyLoaded.Count;
        }

        public void EnergyLoad(char x)
        {
            Active_Pokemon.LoadEnergy(x);

        }

        public List<char> GetEnergyLoaded()
        {
            return Active_Pokemon.EnergyLoaded;
        }

        public char GetEnergyLoadedAt(int x)
        {
            return Active_Pokemon.EnergyLoaded[x];
        }

        public bool CanEvolve()
        {
            return Active_Pokemon.CanEvolve;
        }

        public string ReturnEnergyLoadedImg(int x)
        {
            string Image_name = "";
            if (Active_Pokemon.EnergyLoaded[x].Equals('w'))
            {
                Image_name = "..\\..\\Img\\EnergyBox\\Water.gif";
            }
            else if (Active_Pokemon.EnergyLoaded[x].Equals('f'))
            {
                Image_name = "..\\..\\Img\\EnergyBox\\Fire.gif";
            }
            else if (Active_Pokemon.EnergyLoaded[x].Equals('g'))
            {
                Image_name = "..\\..\\Img\\EnergyBox\\Grass.gif";
            }
            else if (Active_Pokemon.EnergyLoaded[x].Equals('p'))
            {
                Image_name = "..\\..\\Img\\EnergyBox\\Psychic.gif";
            }
            else if (Active_Pokemon.EnergyLoaded[x].Equals('l'))
            {
                Image_name = "..\\..\\Img\\EnergyBox\\Fighting.gif";
            }
            else if (Active_Pokemon.EnergyLoaded[x].Equals('e'))
            {
                Image_name = "..\\..\\Img\\EnergyBox\\Lightning.gif";
            }
            else if (Active_Pokemon.EnergyLoaded[x].Equals('c'))
            {
                Image_name = "..\\..\\Img\\EnergyBox\\Colorless.gif";
            }
            return Image_name;
        }

        public void DiscardEnergyAt(int x)
        {
            Active_Pokemon.EnergyLoaded.RemoveAt(x);
        }

        public void DiscardEnergyType(char x)
        {
            int i = 0;
            while(Active_Pokemon.EnergyLoaded[i] != x)
            {
                i++;
            }
            Active_Pokemon.EnergyLoaded.RemoveAt(i);
        }

        //The Benched Pokémon becomes the new Active Pokémon
        public void Become(Pokemon pokemon)
        {
                Active_Pokemon = pokemon;
        }

        public int InheritDamage(int previous_damage)
        {
            return Active_Pokemon.Rem_Hp = Active_Pokemon.Hp - previous_damage;
        }
        public List<char> InheritEnergies(List<char> previouse_energies)
        {
            return Active_Pokemon.EnergyLoaded = previouse_energies;
        }
        public bool CanPerformAttack(int x)
        {
            int colorless = 0;
            int dark = 0;
            int fairy = 0;
            int fighting = 0;
            int fire = 0;
            int grass = 0;
            int lightning = 0;
            int metal = 0;
            int psychic = 0;
            int water = 0;

            for (var i = 0; i < Active_Pokemon.EnergyLoaded.Count; ++i)
            {
                if (Active_Pokemon.EnergyLoaded[i] == 'c')
                    colorless++;
            }
            for (var i = 0; i < Active_Pokemon.EnergyLoaded.Count; ++i)
            {
                if (Active_Pokemon.EnergyLoaded[i] == 'd')
                    dark++;
            }
            for (var i = 0; i < Active_Pokemon.EnergyLoaded.Count; ++i)
            {
                if (Active_Pokemon.EnergyLoaded[i] == 'a')
                    fairy++;
            }
            for (var i = 0; i < Active_Pokemon.EnergyLoaded.Count; ++i)
            {
                if (Active_Pokemon.EnergyLoaded[i] == 'l')
                    fighting++;
            }
            for (var i = 0; i < Active_Pokemon.EnergyLoaded.Count; ++i)
            {
                if (Active_Pokemon.EnergyLoaded[i] == 'f')
                    fire++;
            }
            for (var i = 0; i < Active_Pokemon.EnergyLoaded.Count; ++i)
            {
                if (Active_Pokemon.EnergyLoaded[i] == 'g')
                    grass++;
            }
            for (var i = 0; i < Active_Pokemon.EnergyLoaded.Count; ++i)
            {
                if (Active_Pokemon.EnergyLoaded[i] == 'e')
                    lightning++;
            }
            for (var i = 0; i < Active_Pokemon.EnergyLoaded.Count; ++i)
            {
                if (Active_Pokemon.EnergyLoaded[i] == 'm')
                    metal++;
            }
            for (var i = 0; i < Active_Pokemon.EnergyLoaded.Count; ++i)
            {
                if (Active_Pokemon.EnergyLoaded[i] == 'p')
                    psychic++;
            }
            for (var i = 0; i < Active_Pokemon.EnergyLoaded.Count; ++i)
            {
                if (Active_Pokemon.EnergyLoaded[i] == 'w')
                    water++;
            }

            switch (Active_Pokemon.Energy)
            {
                case 'c':
                    if( x == 1)
                    {
                        if(Active_Pokemon.attack1.energycost.SumOfEnergies() <= Active_Pokemon.EnergyLoaded.Count)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    else if (x == 2)
                    {
                        if (Active_Pokemon.attack2.energycost.SumOfEnergies() <= Active_Pokemon.EnergyLoaded.Count)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }

                    else
                    {
                        if (Active_Pokemon.attack3.energycost.SumOfEnergies() <= Active_Pokemon.EnergyLoaded.Count)
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                                 
                case 'd':
                    return false;            
                case 'a':
                    return false;
                case 'l':
                    if (x == 1)
                    {
                        if (Active_Pokemon.attack1.energycost.earth > fighting)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack1.energycost.earth;
                            if (Active_Pokemon.attack1.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else if (x == 2)
                    {
                        if (Active_Pokemon.attack2.energycost.earth > fighting)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack2.energycost.earth;
                            if (Active_Pokemon.attack2.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (Active_Pokemon.attack3.energycost.earth > fighting)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack3.energycost.earth;
                            if (Active_Pokemon.attack3.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                case 'f':
                    if(x == 1)
                    {
                        if(Active_Pokemon.attack1.energycost.fire > fire)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack1.energycost.fire;
                            if(Active_Pokemon.attack1.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else if(x == 2)
                    {
                        if (Active_Pokemon.attack2.energycost.fire > fire)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack2.energycost.fire;
                            if (Active_Pokemon.attack2.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (Active_Pokemon.attack3.energycost.fire > fire)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack3.energycost.fire;
                            if (Active_Pokemon.attack3.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                case 'g':
                    if (x == 1)
                    {
                        if (Active_Pokemon.attack1.energycost.grass > grass)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack1.energycost.grass;
                            if (Active_Pokemon.attack1.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else if (x == 2)
                    {
                        if (Active_Pokemon.attack2.energycost.grass > grass)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack2.energycost.grass;
                            if (Active_Pokemon.attack2.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (Active_Pokemon.attack3.energycost.grass > grass)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack3.energycost.grass;
                            if (Active_Pokemon.attack3.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                case 'e':
                    if (x == 1)
                    {
                        if (Active_Pokemon.attack1.energycost.lightning > lightning)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack1.energycost.lightning;
                            if (Active_Pokemon.attack1.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else if (x == 2)
                    {
                        if (Active_Pokemon.attack2.energycost.lightning > lightning)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack2.energycost.lightning;
                            if (Active_Pokemon.attack2.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (Active_Pokemon.attack3.energycost.lightning > lightning)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack3.energycost.lightning;
                            if (Active_Pokemon.attack3.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                case 'm':
                    return false;
                case 'p':
                    if (x == 1)
                    {
                        if (Active_Pokemon.attack1.energycost.psychic > psychic)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack1.energycost.psychic;
                            if (Active_Pokemon.attack1.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else if (x == 2)
                    {
                        if (Active_Pokemon.attack2.energycost.psychic > psychic)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack2.energycost.psychic;
                            if (Active_Pokemon.attack2.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (Active_Pokemon.attack3.energycost.psychic > psychic)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack3.energycost.psychic;
                            if (Active_Pokemon.attack3.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                case 'w':
                    if (x == 1)
                    {
                        if (Active_Pokemon.attack1.energycost.water > water)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack1.energycost.water;
                            if (Active_Pokemon.attack1.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else if (x == 2)
                    {
                        if (Active_Pokemon.attack2.energycost.water > water)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack2.energycost.water;
                            if (Active_Pokemon.attack2.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }
                    else
                    {
                        if (Active_Pokemon.attack3.energycost.water > water)
                        {
                            return false;
                        }
                        else
                        {
                            int used = Active_Pokemon.attack3.energycost.water;
                            if (Active_Pokemon.attack3.energycost.colorless <= (Active_Pokemon.EnergyLoaded.Count - used))
                            {
                                return true;
                            }
                            else
                            {
                                return false;
                            }
                        }
                    }

                default:
                    return false;
            }

          
        }

        public void ChangeCanEvolveStatusToTrue()
        {
            Active_Pokemon.CanEvolve = true;
        }

        public void ReceiveDamage(int damage)
        {
            Active_Pokemon.Rem_Hp -= damage;
            if(Active_Pokemon.Rem_Hp < 0)
            {
                Active_Pokemon.Rem_Hp = 0;
            }
        }
        public void ClearEverthingFromCard()
        {
            Active_Pokemon.Rem_Hp = Active_Pokemon.Hp;
            Active_Pokemon.CanEvolve = false;
        }

    }
}
