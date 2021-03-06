﻿using Pokemon.Card;
using Pokemon.Game_Zone;
using System.Windows.Forms;

namespace Pokemon
{
    class Attack
    {
        public string name;
        public string type;
        public string information;
        public int damage;
        public int self_damage;
        public bool afflicted_state;
        public string check_status;
        public EnergyCost energycost;

        // Common attack dealing damage
        public Attack(string name, string information, EnergyCost energycost)
        {
            this.name = name;
            this.information = information;
            this.energycost = energycost;
        }
        // Attack dealing damage and self damage
        public Attack(string name, string type, string information, int damage, int self_damage, EnergyCost energycost)
        {
            this.name = name;
            this.type = type;
            this.information = information;
            this.damage = damage;
            this.self_damage = self_damage;
            this.energycost = energycost;
        }
        public Attack(string name, string type, string information, int damage, bool afflicted_state, string check_status, EnergyCost energycost)
        {
            this.name = name;
            this.type = type;
            this.information = information;
            this.damage = damage;
            this.afflicted_state = afflicted_state;
            this.check_status = check_status;
            this.energycost = energycost;
        }
        public delegate void DealDamage(Active character, Active opponent, int damage)
        {
            //If the opponent played a card that allows it to have no weakeness then
            if (opponent.withoutWeaknessEffect)
            {
                opponent.Active_Pokemon.Rem_Hp -= damage;
            }
            //Normal game effects when it comes to weakness
            else
            {
                //if the opponent has weakness to your character then it takes double damage
                if (opponent.Active_Pokemon.Weakness == character.Active_Pokemon.Energy)
                {
                    opponent.Active_Pokemon.Rem_Hp -= (damage * 2);
                }
                //if the opponent has resistance to your character then it takes less damage or zero
                else if (opponent.Active_Pokemon.Resistance == character.Active_Pokemon.Energy)
                {
                    if (damage <= opponent.Active_Pokemon.Resistance_Modifier)
                    {
                        opponent.Active_Pokemon.Rem_Hp -= 0;
                    }
                    else
                    {
                        opponent.Active_Pokemon.Rem_Hp -= (damage - opponent.Active_Pokemon.Resistance_Modifier);
                    }
                }
                //if the opponent has no weakness to your character then it takes normal damage
                else
                {
                    opponent.Active_Pokemon.Rem_Hp -= damage;
                }
            }
            //if the opponet's remaining hp is less that zero then 0
            if (opponent.Active_Pokemon.Rem_Hp < 0)
            {
                opponent.Active_Pokemon.Rem_Hp = 0;
            }
        }
        public int ResultingDamage(Active character, Active opponent, int damage)
        {
            if (opponent.withoutWeaknessEffect)
            {
                return damage;
            }
            else
            {
                //if the opponent has weakness to your character then it takes double damage
                if (opponent.Active_Pokemon.Weakness == character.Active_Pokemon.Energy)
                {
                    return (damage * 2);
                }
                //if the opponent has resistance to your character then it takes less damage or zero
                else if (opponent.Active_Pokemon.Resistance == character.Active_Pokemon.Energy)
                {
                    if (damage <= opponent.Active_Pokemon.Resistance_Modifier)
                    {
                        return 0;
                    }
                    else
                    {
                        return damage - opponent.Active_Pokemon.Resistance_Modifier;
                    }
                }
                //if the opponent has no weakness to your character then it takes normal damage
                else
                {
                    return damage;
                }
            }
        }
    }
}
