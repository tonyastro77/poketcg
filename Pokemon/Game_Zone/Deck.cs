using Pokemon.Card;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon.Game_Zone
{
    class Deck
    {
        public List<Pokemon> deck;
        public AttackEffect effect;
        public Attack attack;
        bool CanPlayTrainers { get; set; }
        bool HasWeakness { get; set; }
        bool PlayedEnergy { get; set; }
        bool PlayedAttack { get; set; }
        public Deck()
        {
            deck = new List<Pokemon>();
            CanPlayTrainers = true;
            HasWeakness = false;
            PlayedEnergy = false;
            PlayedAttack = false;
        }
        public void AddToDeck(Pokemon card)
        {
            deck.Add(card);
        }
        public Pokemon DrawCard()
        {
            if(deck.Count > 0) {
                int indexing = deck.Count() - 1;
                Pokemon card = deck[indexing];
                deck.RemoveAt(indexing);
                if (deck.Count == 0)
                {
                    MessageBox.Show("You lose the game!");
                }
            return card;
            }
            else
            {
                return null;
            }            
                       
        }
        public void PlayWith(string name, Active player_active, Active opponent_active, Bench player_bench, Bench opponent_bench, Deck player_deck, Deck opponent_deck, Hand player_hand, Hand opponent_hand, Discard player_discard, Discard opponent_discard, Prize player_prize, Prize opponent_prize, Used player_used, Used opponent_used)
        {
            if(name == "Brushfire")
            {
                InsertCardTimesX(new Pokemon(46, "Charmander", "basic", 50, 50, 'f', 'w', 'n', 0, 1, "Obviously prefers hot places. If it gets caught in the rain, steam is said to spout from the tip of its tail.", "..\\..\\Img\\BSMade\\BS_046.jpeg", new attack.DealDamage(player_active, opponent_active, 10), new Attack("Ember", "DealDamageDiscardEnergy", "Discard 1 Fire Energy card attached to Charmander in order to use this attack.", 30, new EnergyCost(1, 0, 1, 0, 0, 0, 0)), new List<char>(), false, new List<Pokemon>()), 4);
                InsertCardTimesX(new Pokemon(24, "Charmeleon", "second", "Charmander", 80, 80, 'f', 'w', 'n', 0, 1, "It lashes about with its tail to knock down its foe. It then tears up the fallen opponent with sharp claws.", "..\\..\\Img\\BSMade\\BS_024.jpeg", new Attack("Slash", "DealDamage", "", 30, new EnergyCost(3, 0, 0, 0, 0, 0, 0)), new Attack("Flamethrower", "DealDamageDiscardEnergy", "Discard 1 Fire Energy card attached to Charmeleon in order to use this attack.", 50, new EnergyCost(1, 0, 2, 0, 0, 0, 0)), new List<char>(), false, new List<Pokemon>()), 2);
                InsertCardTimesX(new Pokemon(68, "Vulpix", "basic", 50, 50, 'f', 'w', 'n', 0, 1, "At the time of birth, it has just one tail. Its tail splits from the tip as it grows older.", "..\\..\\Img\\BSMade\\BS_068.jpeg", new Attack("Confuse Ray", "DealDamageWithEffect", "Flip a coin. If heads, the Defending Pokémon is now Confused.", 10, opponent_active.Confused, "Confusion", new EnergyCost(0, 0, 2, 0, 0, 0, 0)), new List<char>(), false, new List<Pokemon>()), 2);
                InsertCardTimesX(new Pokemon(12, "Ninetales", "second", "Vulpix", 80, 80, 'f', 'w', 'n', 0, 1, "If your opponent has any Benched Pokémon, choose 1 of them and switch it with the Defending Pokémon.", "..\\..\\Img\\BSMade\\BS_012.jpeg", new Attack("Lure", "defensive", "If your opponent has any Benched Pokémon, choose 1 of them and switch it with the Defending Pokémon.", 0, new EnergyCost(2, 0, 0, 0, 0, 0, 0)), new Attack("Fire Blast", "offensive", "Discard 1 Fire Energy card attached to Ninetales in order to use this attack.", 80, new EnergyCost(0, 0, 4, 0, 0, 0, 0)), new List<char>(), false, new List<Pokemon>()), 1);
                InsertCardTimesX(new Pokemon(28, "Growlithe", "basic", 60, 60, 'f', 'w', 'n', 0, 1, "Very protective of its territory. It will bark and bite to repel intruders from its space.", "..\\..\\Img\\BSMade\\BS_028.jpeg", new Attack("Flare", "DealDamage", "", 20, new EnergyCost(1, 0, 1, 0, 0, 0, 0)), new List<char>(), false, new List<Pokemon>()), 2);
                InsertCardTimesX(new Pokemon(23, "Arcanine", "second", "Growlithe", 100, 100, 'f', 'w', 'n', 0, 3, "A Pokémon that has been long admired for its beauty. It runs gracefully, as if on wings.", "..\\..\\Img\\BSMade\\BS_023.jpeg", new Attack("DealDamageDiscardEnergy", "offensive", "Discard 1 Fire Energy card attached to Arcanine in order to use this attack.", 50, new EnergyCost(1, 0, 2, 0, 0, 0, 0)), new Attack("Take Down", "DealDamageSelfDamage", "Arcanine does 30 damage to itself.", 80, 30, new EnergyCost(2, 0, 2, 0, 0, 0, 0)), new List<char>(), false, new List<Pokemon>()), 1);
                InsertCardTimesX(new Pokemon(69, "Weedle", "basic", 40, 40, 'g', 'f', 'n', 0, 1, "Often found in forests, eating leaves. It has a sharp, venomous stinger on its head.", "..\\..\\Img\\BSMade\\BS_069.jpeg", new Attack("Poison Sting", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Poisoned.", 10, new EnergyCost(0, 0, 0, 1, 0, 0, 0)), new List<char>(), false, new List<Pokemon>()), 4);
                InsertCardTimesX(new Pokemon(55, "Nidoran Male", "basic", 40, 40, 'g', 'p', 'n', 0, 1, "Stiffens its ears to sense danger. The larger, more powerful of its horns secretes venom.", "..\\..\\Img\\BSMade\\BS_055.jpeg", new Attack("Horn Hazard", "offensive", "Flip a coin. If tails, this attack does nothing.", 30, new EnergyCost(0, 0, 0, 1, 0, 0, 0)), new List<char>(), false, new List<Pokemon>()), 4);
                InsertCardTimesX(new Pokemon(66, "Tangela", "basic", 50, 50, 'g', 'f', 'n', 0, 2, "Its whole body is swathed with wide vines that are similar to seaweed. These vines shake as it walks.", "..\\..\\Img\\BSMade\\BS_066.jpeg", new Attack("Bind", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Paralyzed.", 20, new EnergyCost(1, 0, 0, 1, 0, 0, 0)), new Attack("Poisonpowder", "offensive", "The Defending Pokémon is now Poisoned.", 20, new EnergyCost(0, 0, 0, 3, 0, 0, 0)), new List<char>(), false, new List<Pokemon>()), 2);
                InsertCardTimesX(new Pokemon(92, "Energy Removal", "trainer", "global", 't', "Choose 1 Energy card attached to 1 of your opponent's Pokémon and discard it.", "..\\..\\Img\\BS\\BS_092.jpg"), 1);
                InsertCardTimesX(new Pokemon(81, "Energy Retrieval", "trainer", "global", 't', "Trade 1 of the other cards in your hand for up to 2 basic Energy cards from your discard pile.", "..\\..\\Img\\BS\\BS_081.jpg"), 2);
                InsertCardTimesX(new Pokemon(93, "Gust of Wind", "trainer", "global", 't', "Choose 1 of your opponent's Benched Pokémon and switch it with his or her Active Pokémon.", "..\\..\\Img\\BS\\BS_093.jpg"), 1);
                InsertCardTimesX(new Pokemon(75, "Lass", "trainer", "global", 't', "You and your opponent show each other your hands, then shuffle all the Trainer cards from your hands into your decks.", "..\\..\\Img\\BS\\BS_075.jpg"), 1);
                InsertCardTimesX(new Pokemon(84, "PlusPower", "trainer", "global", 't', "Attach PlusPower to your Active Pokémon. At the end of your turn, discard PlusPower. If this Pokémon's attack does damage to the Defending Pokémon (after applying Weakness and Resistance), the attack does 10 more damage to the Defending Pokémon.", "..\\..\\Img\\BS\\BS_084.jpg"), 1);
                InsertCardTimesX(new Pokemon(94, "Potion", "trainer", "directed", 't', "Remove up to 2 damage counters from 1 of your Pokémon.", "..\\..\\Img\\BS\\BS_094.jpg"), 3);
                InsertCardTimesX(new Pokemon(95, "Switch", "trainer", "global", 't', "Switch 1 of your Benched Pokémon with your Active Pokémon.", "..\\..\\Img\\BS\\BS_095.jpg"), 1);
                InsertCardTimesX(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"), 18);
                InsertCardTimesX(new Pokemon(99, "Grass Energy", "energy", 'g', "..\\..\\Img\\BS\\BS_099.jpg"), 10);
            }
            else if(name == "Zap")
            {
                InsertCardTimesX(new Pokemon(58, "Pikachu", "basic", 40, 40, 'e', 'l', 'n', 0, 1, "When several of these Pokémon gather, their electricity can cause lightning storms.", "..\\..\\Img\\BSMade\\BS_058.jpeg", new Attack("Gnaw", "offensive", "", 10, new EnergyCost(1, 0, 0, 0, 0, 0, 0)), new Attack("Thunder Jolt", "offensive", "Flip a coin. If tails, Pikachu does 10 damage to itself.", 30, new EnergyCost(1, 0, 0, 0, 1, 0, 0)), new List<char>(), false, new List<Pokemon>()), 4);
                InsertCardTimesX(new Pokemon(53, "Magnemite", "basic", 40, 40, 'e', 'l', 'n', 0, 1, "Uses anti-gravity to stay suspended. Appears without warning and uses attacks like Thunder Wave.", "..\\..\\Img\\BSMade\\BS_053.jpeg", new Attack("Thunder Wave", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Paralyzed.", 10, new EnergyCost(0, 0, 0, 0, 1, 0, 0)), new Attack("Self Destruct", "offensive", "Does 10 damage to each Pokémon on each player's Bench. (Don't apply Weakness and Resistance for Benched Pokémon.) Magnemite does 40 damage to itself.", 40, new EnergyCost(1, 0, 0, 0, 1, 0, 0)), new List<char>(), false, new List<Pokemon>()), 3);
                InsertCardTimesX(new Pokemon(43, "Abra", "basic", 30, 30, 'p', 'p', 'n', 0, 0, "Using its ability to read minds, it will identify impending danger and teleport to safety.", "..\\..\\Img\\BSMade\\BS_043.jpeg", new Attack("Psychock", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Paralyzed.", 10, new EnergyCost(0, 0, 0, 0, 0, 1, 0)), new List<char>(), false, new List<Pokemon>()), 3);
                InsertCardTimesX(new Pokemon(32, "Kadabra", "second", "Abra", 60, 60, 'p', 'p', 'n', 0, 3, "It emits special alpha waves from its body that induce headaches even to those just nearby.", "..\\..\\Img\\BSMade\\BS_032.jpeg", new Attack("Recover", "defensive", "Discard 1 Psychic Energy card attached to Kadabra in order to use this attack. Remove all damage counters from Kadabra.", 0, new EnergyCost(0, 0, 0, 0, 0, 2, 0)), new Attack("Super Psy", "offensive", "", 50, new EnergyCost(1, 0, 0, 0, 0, 2, 0)), new List<char>(), false, new List<Pokemon>()), 1);
                InsertCardTimesX(new Pokemon(50, "Gastly", "basic", 30, 30, 'p', 'n', 'l', 30, 0, "Almost invisible, this gaseous Pokémon cloaks the target and puts it to sleep without notice.", "..\\..\\Img\\BSMade\\BS_050.jpeg", new Attack("Sleeping Gas", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Asleep.", 0, new EnergyCost(0, 0, 0, 0, 0, 1, 0)), new Attack("Destiny Bond", "defensive", "Discard 1 Psychic Energy card attached to Gastly in order to use this attack. If a Pokémon Knocks Out Gastly during your opponent's next turn, Knock Out that Pokémon.", 0, new EnergyCost(1, 0, 0, 0, 0, 1, 0)), new List<char>(), false, new List<Pokemon>()), 3);
                InsertCardTimesX(new Pokemon(29, "Haunter", "second", "Gastly", 60, 60, 'p', 'n', 'l', 30, 1, "Because of its ability to slip through block walls, it is said to be from another dimension.", "..\\..\\Img\\BSMade\\BS_029.jpeg", new Attack("Hypnosis", "offensive", "The Defending Pokémon is now Asleep.", 0, new EnergyCost(0, 0, 0, 0, 0, 1, 0)), new Attack("Dream Eater", "offensive", "You can't use this attack unless the Defending Pokémon is Asleep.", 50, new EnergyCost(0, 0, 0, 0, 0, 2, 0)), new List<char>(), false, new List<Pokemon>()), 2);
                InsertCardTimesX(new Pokemon(49, "Drowzee", "basic", 50, 50, 'p', 'p', 'n', 0, 1, "Puts enemies to sleep, then eats their dreams. Occasionally gets sick from eating bad dreams.", "..\\..\\Img\\BSMade\\BS_049.jpeg", new Attack("Pound", "offensive", "", 10, new EnergyCost(1, 0, 0, 0, 0, 0, 0)), new Attack("Confuse Ray", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Confused.", 10, new EnergyCost(0, 0, 0, 0, 0, 2, 0)), new List<char>(), false, new List<Pokemon>()), 2);
                InsertCardTimesX(new Pokemon(31, "Jynx", "basic", 70, 70, 'p', 'p', 'n', 0, 2, "Merely by meditating, the Pokémon launches a powerful psychic energy attack.", "..\\..\\Img\\BSMade\\BS_031.jpeg", new Attack("Doubleslap", "offensive", "Flip 2 coins. This attack does 10 damage times the number of heads.", 10, new EnergyCost(0, 0, 0, 0, 0, 1, 0)), new Attack("Meditate", "offensive", "Does 20 damage plus 10 more damage for each damage counter on the Defending Pokémon.", 20, new EnergyCost(1, 0, 0, 0, 0, 2, 0)), new List<char>(), false, new List<Pokemon>()), 2);
                InsertCardTimesX(new Pokemon(10, "Mewtwo", "basic", 60, 60, 'p', 'p', 'n', 0, 3, "A scientist created this Pokémon after years of horrific gene-splicing and DNA engineering experiments.", "..\\..\\Img\\BSMade\\BS_010.jpeg", new Attack("Psychic", "offensive", "Does 10 damage plus 10 more damage for each Energy card attached to the Defending Pokémon.", 10, new EnergyCost(1, 0, 0, 0, 0, 1, 0)), new Attack("Barrier", "defensive", "Discard 1 Psychic Energy card attached to Mewtwo in order to use this attack. During your opponent's next turn, prevent all effects of attacks, including damage, done to Mewtwo.", 0, new EnergyCost(0, 0, 0, 0, 0, 2, 0)), new List<char>(), false, new List<Pokemon>()), 1);
                InsertCardTimesX(new Pokemon(91, "Bill", "trainer", "global", 't', "Draw 2 cards.", "..\\..\\Img\\BS\\BS_091.jpg"), 2);
                InsertCardTimesX(new Pokemon(71, "Computer Search", "global", "trainer", 't', "Discard 2 of the other cards from your hand in order to search your deck for any card and put it into your hand. Shuffle your deck afterward.", "..\\..\\Img\\BS\\BS_071.jpg"), 1);
                InsertCardTimesX(new Pokemon(80, "Defender", "trainer", "directed", 't', "Attach Defender to 1 of your Pokémon. At the end of your opponent's next turn, discard Defender. Damage done to that Pokémon by attacks is reduced by 20 (after applying Weakness and Resistance).", "..\\..\\Img\\BS\\BS_080.jpg"), 1);
                InsertCardTimesX(new Pokemon(93, "Gust of Wind", "trainer", "global", 't', "Choose 1 of your opponent's Benched Pokémon and switch it with his or her Active Pokémon.", "..\\..\\Img\\BS\\BS_093.jpg"), 2);
                InsertCardTimesX(new Pokemon(94, "Potion", "trainer", "directed", 't', "Remove up to 2 damage counters from 1 of your Pokémon.", "..\\..\\Img\\BS\\BS_094.jpg"), 1);
                InsertCardTimesX(new Pokemon(88, "Professor Oak", "trainer", "global", 't', "Discard your hand, then draw 7 cards.", "..\\..\\Img\\BS\\BS_088.jpg"), 1);
                InsertCardTimesX(new Pokemon(90, "Super Potion", "trainer", "directed", 't', "Discard 1 Energy card attached to 1 of your own Pokémon in order to remove up to 4 damage counters from that Pokémon.", "..\\..\\Img\\BS\\BS_090.jpg"), 1);
                InsertCardTimesX(new Pokemon(95, "Switch", "trainer", "global", 't', "Switch 1 of your Benched Pokémon with your Active Pokémon.", "..\\..\\Img\\BS\\BS_095.jpg"), 2);
                InsertCardTimesX(new Pokemon(100, "Lightning Energy", "energy", 'e', "..\\..\\Img\\BS\\BS_100.jpg"), 12);
                InsertCardTimesX(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"), 16);
            }
        }
        public void InsertCardTimesX(Pokemon card, int times)
        {
            int i = 0;
            while(i < times)
            {
                deck.Add(card);
                i++;
            }        
        }
        public void ShowCards()
        {
            string txt = "";

            for (int i = 0; i < deck.Count; i++)
            {
                txt += deck[i].Name + "\n";
            }

            Console.WriteLine("Your deck is: \n");
            Console.WriteLine(txt);
        }
        public void Shuffle()
        {
            Random rng = new Random();
            int n = deck.Count;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                Pokemon value = deck[k];
                deck[k] = deck[n];
                deck[n] = value;
            }

        }
        public int NumberOfCards()
        {
            return deck.Count;
        }
        public void ShuffleCardFromHandIntoDeck(Pokemon taken)
        {    
                deck.Add(taken);                
        }
    }
}
