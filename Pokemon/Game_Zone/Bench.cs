using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Game_Zone
{
    class Bench
    {
        public List<Pokemon> bench;

        public Bench()
        {
            bench = new List<Pokemon>();
        }

        public void Add(Pokemon x)
        {
            bench.Add(x);
        }

        public int NumberOfCards()
        {
            return bench.Count;
        }

        public string ShowCard(int num)
        {
            return bench[num].Img;
        }
        public string ShowName(int num)
        {
            return bench[num].Name;
        }
        public int ShowHp(int num)
        {
            return bench[num].Hp;
        }
        public int ShowRemHp(int num)
        {
            return bench[num].Rem_Hp;
        }
        public char ShowEnergy(int num)
        {
            return bench[num].Energy;
        }

        public void EnergyLoad(int x, char y)
        {
            bench[x].LoadEnergy(y);
          
        }

        public string EnergyLoaded(int x)
        {
            string txt = "";

            for (int i = 0; i < bench[x].EnergyLoaded.Count; i++) {
                txt += bench[x].EnergyLoaded[i] + " ";
            }
            return txt;
            ;
        }

        public List<char> GetEnergyLoaded(int x)
        {
            return bench[x].EnergyLoaded;
        }

        public int LoadedEnergyCount(int index)
        {
            return bench[index].EnergyLoaded.Count;
        }

        public string ReturnEnergyLoadedImg(int index, int y)
        {
            string Image_name = "";
            if (bench[index].EnergyLoaded[y].Equals('w')){
                Image_name = "..\\..\\Img\\EnergyBox\\Water.gif";
            }
            else if (bench[index].EnergyLoaded[y].Equals('f'))
            {
                Image_name = "..\\..\\Img\\EnergyBox\\Fire.gif";
            }
            else if (bench[index].EnergyLoaded[y].Equals('g'))
            {
                Image_name = "..\\..\\Img\\EnergyBox\\Grass.gif";
            }
            else if (bench[index].EnergyLoaded[y].Equals('p'))
            {
                Image_name = "..\\..\\Img\\EnergyBox\\Psychic.gif";
            }
            else if (bench[index].EnergyLoaded[y].Equals('l'))
            {
                Image_name = "..\\..\\Img\\EnergyBox\\Fighting.gif";
            }
            else if (bench[index].EnergyLoaded[y].Equals('e'))
            {
                Image_name = "..\\..\\Img\\EnergyBox\\Lightning.gif";
            }
            else if (bench[index].EnergyLoaded[y].Equals('c'))
            {
                Image_name = "..\\..\\Img\\EnergyBox\\Colorless.gif";
            }
            return Image_name;
        }
        public Pokemon PlayCard(int x)
        {
            return bench[x];
        }

        public void RemoveFromBench(int x)
        {
            bench.RemoveAt(x);
        }
        public void PutInto(int index, Pokemon x)
        {
            bench.RemoveAt(index);
            bench.Insert(index, x);
        }

        public int InheritDamage(int index, int previous_damage)
        {
            return bench[index].Rem_Hp = bench[index].Hp - previous_damage;
        }
        public List<char> InheritEnergies(int index, List<char> previouse_energies)
        {
            return bench[index].EnergyLoaded = previouse_energies;
        }

    }
}
