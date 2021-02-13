using System;
using Pokemon.Game_Zone;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon.Card
{
    class AttackEffect
    {
        public void OnlyDamage(int index, Active character, Active opponent, int damage, Label message)
        {
            DealDamage(character, opponent, damage);
            message.Text = "You perform " + character.ShowAttackName(index) + " and deal " + ResultingDamage(character, opponent, damage).ToString() + " damage on " + opponent.ShowName();
        }
        public void DealDamage(Active character, Active opponent, int damage)
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
                    if(damage <= opponent.Active_Pokemon.Resistance_Modifier)
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
