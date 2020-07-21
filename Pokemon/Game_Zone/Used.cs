using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon.Game_Zone
{
    class Used
    {
        public List<Pokemon> used;

        public Used()
        {
            used = new List<Pokemon>();
        }
        public void Add(Pokemon card)
        {
            used.Add(card);
        }

        public string GetName(int x)
        {
            return used[x].Name;
        }

        public Pokemon GetCard(int x)
        {
            return used[x];
        }

        public int Count()
        {
            return used.Count;
        }

        public void RemoveAt(int index)
        {
            used.RemoveAt(index);
        }
        public void DiscardAll()
        {
            for(int i = used.Count-1; i >= 0; i--)
            {
                used.RemoveAt(i);
            }
        }
    }
}
