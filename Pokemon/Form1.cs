using Pokemon.Card;
using Pokemon.Game_Zone;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Media;
using System.Threading;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Form1 : Form
    {
        //Game Zone Piles
        Deck player = new Deck();
        Deck ai = new Deck();
        Hand player_Hand = new Hand();
        Hand ai_Hand = new Hand();
        Bench bench = new Bench();
        Bench aibench = new Bench();
        Discard discard = new Discard();
        Discard ai_discard = new Discard();
        Prizes prize = new Prizes();
        Prizes aiprize = new Prizes();
        Active active_Pokemon = new Active();
        Active ai_Active_Pokemon = new Active();
        Used player_used = new Used();
        Used ai_used = new Used();

        //Phase Restrictions
        bool isPreGameTurn = true;
        bool youStartTurn = false;
        bool aiStartsTurn = false;
        bool isyourFirstTurn = false;
        bool isOpponentFirstTurn = false;
        bool isYourTurn = false;
        bool isOpponentsTurn = false;

        //Pokémon Conditions
        bool PlayerConfused = false;
        bool AIConfused = false;
        bool confusedCheck = false;
        bool PlayerPoisoned = false;      
        bool AIPoisoned = false;
        bool poisonedCheck = false;
        bool PlayerAsleep = false;
        bool AIAsleep = false;
        bool asleepCheck = false;
        bool PlayerParalyzed = false;
        bool AIParalyzed = false;
        bool paralyzedCheck = false;
        bool PlayerBurned = false;
        bool AIBurned = false;
        bool burnedCheck = false;
        bool dealsNothingCheck = false;
        bool PlusPowerOn = false;
        int PlusPowerDamage = 0;
        bool hasWeakness = false;
        bool playedEnergy = false;
        bool aiPlayedEnergy = false;
        bool playedAttack = false;
        bool aiPlayedAttack = false;
        bool active_Pokemon_Retreat = false;
        bool ai_Active_Pokemon_Retreat = false;
        int energydiscard = 0;
        bool energytodiscard = false;
        bool aienergytodiscard = false;
        bool energy_retrieval = false;
        bool energy_retrieval_2 = false;
        bool energy_retrieval_3 = false;
        bool trainer_switch = false;
        bool trainer_lass = false;
        int playerPrizes = 4;
        int aiPrizes = 4;

        private int _ticks;

        public Form1()
        {
            InitializeComponent();   
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            //function for quick testing only
            LureTest.Visible = false;

            player.AddToDeck(new Pokemon(46, "Charmander", "basic", 50, 50, 'f', 'w', 'n', 1, "Obviously prefers hot places. If it gets caught in the rain, steam is said to spout from the tip of its tail.", "..\\..\\Img\\BSPainted\\BS_046.jpg", new Attack("Scratch", "offensive", "", 10, new EnergyCost(1, 0, 0, 0, 0, 0, 0)), new Attack("Ember", "offensive", "Discard 1 Fire Energy card attached to Charmander in order to use this attack.", 30, new EnergyCost(1, 0, 1, 0, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(46, "Charmander", "basic", 50, 50, 'f', 'w', 'n', 1, "Obviously prefers hot places. If it gets caught in the rain, steam is said to spout from the tip of its tail.", "..\\..\\Img\\BSPainted\\BS_046.jpg", new Attack("Scratch", "offensive", "", 10, new EnergyCost(1, 0, 0, 0, 0, 0, 0)), new Attack("Ember", "offensive", "Discard 1 Fire Energy card attached to Charmander in order to use this attack.", 30, new EnergyCost(1, 0, 1, 0, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(46, "Charmander", "basic", 50, 50, 'f', 'w', 'n', 1, "Obviously prefers hot places. If it gets caught in the rain, steam is said to spout from the tip of its tail.", "..\\..\\Img\\BSPainted\\BS_046.jpg", new Attack("Scratch", "offensive", "", 10, new EnergyCost(1, 0, 0, 0, 0, 0, 0)), new Attack("Ember", "offensive", "Discard 1 Fire Energy card attached to Charmander in order to use this attack.", 30, new EnergyCost(1, 0, 1, 0, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(46, "Charmander", "basic", 50, 50, 'f', 'w', 'n', 1, "Obviously prefers hot places. If it gets caught in the rain, steam is said to spout from the tip of its tail.", "..\\..\\Img\\BSPainted\\BS_046.jpg", new Attack("Scratch", "offensive", "", 10, new EnergyCost(1, 0, 0, 0, 0, 0, 0)), new Attack("Ember", "offensive", "Discard 1 Fire Energy card attached to Charmander in order to use this attack.", 30, new EnergyCost(1, 0, 1, 0, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(24, "Charmeleon", "second", "Charmander", 80, 80, 'f', 'w', 'n', 1, "It lashes about with its tail to knock down its foe. It then tears up the fallen opponent with sharp claws.", "..\\..\\Img\\BS\\BS_024.jpg", new Attack("Slash", "offensive", "", 30, new EnergyCost(3, 0, 0, 0, 0, 0, 0)), new Attack("Flamethrower", "offensive", "Discard 1 Fire Energy card attached to Charmeleon in order to use this attack.", 50, new EnergyCost(1, 0, 2, 0, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(24, "Charmeleon", "second", "Charmander", 80, 80, 'f', 'w', 'n', 1, "It lashes about with its tail to knock down its foe. It then tears up the fallen opponent with sharp claws.", "..\\..\\Img\\BS\\BS_024.jpg", new Attack("Slash", "offensive", "", 30, new EnergyCost(3, 0, 0, 0, 0, 0, 0)), new Attack("Flamethrower", "offensive", "Discard 1 Fire Energy card attached to Charmeleon in order to use this attack.", 50, new EnergyCost(1, 0, 2, 0, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(68, "Vulpix", "basic", 50, 50, 'f', 'w', 'n', 1, "At the time of birth, it has just one tail. Its tail splits from the tip as it grows older.", "..\\..\\Img\\BS\\BS_068.jpg", new Attack("Confuse Ray", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Confused.", 10, new EnergyCost(0, 0, 2, 0, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(68, "Vulpix", "basic", 50, 50, 'f', 'w', 'n', 1, "At the time of birth, it has just one tail. Its tail splits from the tip as it grows older.", "..\\..\\Img\\BS\\BS_068.jpg", new Attack("Confuse Ray", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Confused.", 10, new EnergyCost(0, 0, 2, 0, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(12, "Ninetales", "second", "Vulpix", 80, 80, 'f', 'w', 'n', 1, "If your opponent has any Benched Pokémon, choose 1 of them and switch it with the Defending Pokémon.", "..\\..\\Img\\BS\\BS_012.jpg", new Attack("Lure", "defensive", "If your opponent has any Benched Pokémon, choose 1 of them and switch it with the Defending Pokémon.", 0, new EnergyCost(2, 0, 0, 0, 0, 0, 0)), new Attack("Fire Blast", "offensive", "Discard 1 Fire Energy card attached to Ninetales in order to use this attack.", 80, new EnergyCost(0, 0, 4, 0, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(28, "Growlithe", "basic", 60, 60, 'f', 'w', 'n', 1, "Very protective of its territory. It will bark and bite to repel intruders from its space.", "..\\..\\Img\\BS\\BS_028.jpg", new Attack("Flare", "offensive", "", 20, new EnergyCost(1, 0, 1, 0, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(28, "Growlithe", "basic", 60, 60, 'f', 'w', 'n', 1, "Very protective of its territory. It will bark and bite to repel intruders from its space.", "..\\..\\Img\\BS\\BS_028.jpg", new Attack("Flare", "offensive", "", 20, new EnergyCost(1, 0, 1, 0, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(23, "Arcanine", "second", "Growlithe", 100, 100, 'f', 'w', 'n', 3, "A Pokémon that has been long admired for its beauty. It runs gracefully, as if on wings.", "..\\..\\Img\\BS\\BS_023.jpg", new Attack("Flamethrower", "offensive", "Discard 1 Fire Energy card attached to Arcanine in order to use this attack.", 50, new EnergyCost(1, 0, 2, 0, 0, 0, 0)), new Attack("Take Down", "offensive", "Arcanine does 30 damage to itself.", 80, new EnergyCost(2, 0, 2, 0, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(69, "Weedle", "basic", 40, 40, 'g', 'f', 'n', 1, "Often found in forests, eating leaves. It has a sharp, venomous stinger on its head.", "..\\..\\Img\\BSPainted\\BS_069.jpg", new Attack("Poison Sting", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Poisoned.", 10, new EnergyCost(0, 0, 0, 1, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(69, "Weedle", "basic", 40, 40, 'g', 'f', 'n', 1, "Often found in forests, eating leaves. It has a sharp, venomous stinger on its head.", "..\\..\\Img\\BSPainted\\BS_069.jpg", new Attack("Poison Sting", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Poisoned.", 10, new EnergyCost(0, 0, 0, 1, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(69, "Weedle", "basic", 40, 40, 'g', 'f', 'n', 1, "Often found in forests, eating leaves. It has a sharp, venomous stinger on its head.", "..\\..\\Img\\BSPainted\\BS_069.jpg", new Attack("Poison Sting", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Poisoned.", 10, new EnergyCost(0, 0, 0, 1, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(69, "Weedle", "basic", 40, 40, 'g', 'f', 'n', 1, "Often found in forests, eating leaves. It has a sharp, venomous stinger on its head.", "..\\..\\Img\\BSPainted\\BS_069.jpg", new Attack("Poison Sting", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Poisoned.", 10, new EnergyCost(0, 0, 0, 1, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(55, "Nidoran Male", "basic", 40, 40, 'g', 'p', 'n', 1, "Stiffens its ears to sense danger. The larger, more powerful of its horns secretes venom.", "..\\..\\Img\\BS\\BS_055.jpg", new Attack("Horn Hazard", "offensive", "Flip a coin. If tails, this attack does nothing.", 30, new EnergyCost(0, 0, 0, 1, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(55, "Nidoran Male", "basic", 40, 40, 'g', 'p', 'n', 1, "Stiffens its ears to sense danger. The larger, more powerful of its horns secretes venom.", "..\\..\\Img\\BS\\BS_055.jpg", new Attack("Horn Hazard", "offensive", "Flip a coin. If tails, this attack does nothing.", 30, new EnergyCost(0, 0, 0, 1, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(55, "Nidoran Male", "basic", 40, 40, 'g', 'p', 'n', 1, "Stiffens its ears to sense danger. The larger, more powerful of its horns secretes venom.", "..\\..\\Img\\BS\\BS_055.jpg", new Attack("Horn Hazard", "offensive", "Flip a coin. If tails, this attack does nothing.", 30, new EnergyCost(0, 0, 0, 1, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(55, "Nidoran Male", "basic", 40, 40, 'g', 'p', 'n', 1, "Stiffens its ears to sense danger. The larger, more powerful of its horns secretes venom.", "..\\..\\Img\\BS\\BS_055.jpg", new Attack("Horn Hazard", "offensive", "Flip a coin. If tails, this attack does nothing.", 30, new EnergyCost(0, 0, 0, 1, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(66, "Tangela", "basic", 50, 50, 'g', 'f', 'n', 2, "Its whole body is swathed with wide vines that are similar to seaweed. These vines shake as it walks.", "..\\..\\Img\\BS\\BS_066.jpg", new Attack("Bind", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Paralyzed.", 20, new EnergyCost(1, 0, 0, 1, 0, 0, 0)), new Attack("Poisonpowder", "offensive", "The Defending Pokémon is now Poisoned.", 20, new EnergyCost(0, 0, 0, 3, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(66, "Tangela", "basic", 50, 50, 'g', 'f', 'n', 2, "Its whole body is swathed with wide vines that are similar to seaweed. These vines shake as it walks.", "..\\..\\Img\\BS\\BS_066.jpg", new Attack("Bind", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Paralyzed.", 20, new EnergyCost(1, 0, 0, 1, 0, 0, 0)), new Attack("Poisonpowder", "offensive", "The Defending Pokémon is now Poisoned.", 20, new EnergyCost(0, 0, 0, 3, 0, 0, 0)), new List<char>(), false));
            player.AddToDeck(new Pokemon(92, "Energy Removal", "trainer", "global", 't', "Choose 1 Energy card attached to 1 of your opponent's Pokémon and discard it.", "..\\..\\Img\\BS\\BS_092.jpg"));
            player.AddToDeck(new Pokemon(81, "Energy Retrieval", "trainer", "global", 't', "Trade 1 of the other cards in your hand for up to 2 basic Energy cards from your discard pile.", "..\\..\\Img\\BS\\BS_081.jpg"));
            player.AddToDeck(new Pokemon(81, "Energy Retrieval", "trainer", "global", 't', "Trade 1 of the other cards in your hand for up to 2 basic Energy cards from your discard pile.", "..\\..\\Img\\BS\\BS_081.jpg"));
            player.AddToDeck(new Pokemon(93, "Gust of Wind", "trainer", "global", 't', "Choose 1 of your opponent's Benched Pokémon and switch it with his or her Active Pokémon.", "..\\..\\Img\\BS\\BS_093.jpg"));
            player.AddToDeck(new Pokemon(75, "Lass", "trainer", "global", 't', "You and your opponent show each other your hands, then shuffle all the Trainer cards from your hands into your decks.", "..\\..\\Img\\BS\\BS_075.jpg"));
            player.AddToDeck(new Pokemon(84, "PlusPower", "trainer", "global", 't', "Attach PlusPower to your Active Pokémon. At the end of your turn, discard PlusPower. If this Pokémon's attack does damage to the Defending Pokémon (after applying Weakness and Resistance), the attack does 10 more damage to the Defending Pokémon.", "..\\..\\Img\\BS\\BS_084.jpg"));
            player.AddToDeck(new Pokemon(94, "Potion", "trainer", "directed", 't', "Remove up to 2 damage counters from 1 of your Pokémon.", "..\\..\\Img\\BS\\BS_094.jpg"));
            player.AddToDeck(new Pokemon(94, "Potion", "trainer", "directed", 't', "Remove up to 2 damage counters from 1 of your Pokémon.", "..\\..\\Img\\BS\\BS_094.jpg"));
            player.AddToDeck(new Pokemon(94, "Potion", "trainer", "directed", 't', "Remove up to 2 damage counters from 1 of your Pokémon.", "..\\..\\Img\\BS\\BS_094.jpg"));
            player.AddToDeck(new Pokemon(95, "Switch", "trainer", "global", 't', "Switch 1 of your Benched Pokémon with your Active Pokémon.", "..\\..\\Img\\BS\\BS_095.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", 'f', "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(99, "Grass Energy", "energy", 'g', "..\\..\\Img\\BS\\BS_099.jpg"));
            player.AddToDeck(new Pokemon(99, "Grass Energy", "energy", 'g', "..\\..\\Img\\BS\\BS_099.jpg"));
            player.AddToDeck(new Pokemon(99, "Grass Energy", "energy", 'g', "..\\..\\Img\\BS\\BS_099.jpg"));
            player.AddToDeck(new Pokemon(99, "Grass Energy", "energy", 'g', "..\\..\\Img\\BS\\BS_099.jpg"));
            player.AddToDeck(new Pokemon(99, "Grass Energy", "energy", 'g', "..\\..\\Img\\BS\\BS_099.jpg"));
            player.AddToDeck(new Pokemon(99, "Grass Energy", "energy", 'g', "..\\..\\Img\\BS\\BS_099.jpg"));
            player.AddToDeck(new Pokemon(99, "Grass Energy", "energy", 'g', "..\\..\\Img\\BS\\BS_099.jpg"));
            player.AddToDeck(new Pokemon(99, "Grass Energy", "energy", 'g', "..\\..\\Img\\BS\\BS_099.jpg"));
            player.AddToDeck(new Pokemon(99, "Grass Energy", "energy", 'g', "..\\..\\Img\\BS\\BS_099.jpg"));
            player.AddToDeck(new Pokemon(99, "Grass Energy", "energy", 'g', "..\\..\\Img\\BS\\BS_099.jpg"));


            ai.AddToDeck(new Pokemon(58, "Pikachu", "basic", 40, 40, 'e', 'l', 'n', 1, "When several of these Pokémon gather, their electricity can cause lightning storms.", "..\\..\\Img\\BSPainted\\BS_058.jpg", new Attack("Gnaw", "offensive", "", 10, new EnergyCost(1, 0, 0, 0, 0, 0, 0)), new Attack("Thunder Jolt", "offensive", "Flip a coin. If tails, Pikachu does 10 damage to itself.", 30, new EnergyCost(1, 0, 0, 0, 1, 0, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(58, "Pikachu", "basic", 40, 40, 'e', 'l', 'n', 1, "When several of these Pokémon gather, their electricity can cause lightning storms.", "..\\..\\Img\\BSPainted\\BS_058.jpg", new Attack("Gnaw", "offensive", "", 10, new EnergyCost(1, 0, 0, 0, 0, 0, 0)), new Attack("Thunder Jolt", "offensive", "Flip a coin. If tails, Pikachu does 10 damage to itself.", 30, new EnergyCost(1, 0, 0, 0, 1, 0, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(58, "Pikachu", "basic", 40, 40, 'e', 'l', 'n', 1, "When several of these Pokémon gather, their electricity can cause lightning storms.", "..\\..\\Img\\BSPainted\\BS_058.jpg", new Attack("Gnaw", "offensive", "", 10, new EnergyCost(1, 0, 0, 0, 0, 0, 0)), new Attack("Thunder Jolt", "offensive", "Flip a coin. If tails, Pikachu does 10 damage to itself.", 30, new EnergyCost(1, 0, 0, 0, 1, 0, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(58, "Pikachu", "basic", 40, 40, 'e', 'l', 'n', 1, "When several of these Pokémon gather, their electricity can cause lightning storms.", "..\\..\\Img\\BSPainted\\BS_058.jpg", new Attack("Gnaw", "offensive", "", 10, new EnergyCost(1, 0, 0, 0, 0, 0, 0)), new Attack("Thunder Jolt", "offensive", "Flip a coin. If tails, Pikachu does 10 damage to itself.", 30, new EnergyCost(1, 0, 0, 0, 1, 0, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(53, "Magnemite", "basic", 40, 40, 'e', 'l', 'n', 1, "Uses anti-gravity to stay suspended. Appears without warning and uses attacks like Thunder Wave.", "..\\..\\Img\\BS\\BS_053.jpg", new Attack("Thunder Wave", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Paralyzed.", 10, new EnergyCost(0, 0, 0, 0, 1, 0, 0)), new Attack("Self Destruct", "offensive", "Does 10 damage to each Pokémon on each player's Bench. (Don't apply Weakness and Resistance for Benched Pokémon.) Magnemite does 40 damage to itself.", 40, new EnergyCost(1, 0, 0, 0, 1, 0, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(53, "Magnemite", "basic", 40, 40, 'e', 'l', 'n', 1, "Uses anti-gravity to stay suspended. Appears without warning and uses attacks like Thunder Wave.", "..\\..\\Img\\BS\\BS_053.jpg", new Attack("Thunder Wave", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Paralyzed.", 10, new EnergyCost(0, 0, 0, 0, 1, 0, 0)), new Attack("Self Destruct", "offensive", "Does 10 damage to each Pokémon on each player's Bench. (Don't apply Weakness and Resistance for Benched Pokémon.) Magnemite does 40 damage to itself.", 40, new EnergyCost(1, 0, 0, 0, 1, 0, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(53, "Magnemite", "basic", 40, 40, 'e', 'l', 'n', 1, "Uses anti-gravity to stay suspended. Appears without warning and uses attacks like Thunder Wave.", "..\\..\\Img\\BS\\BS_053.jpg", new Attack("Thunder Wave", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Paralyzed.", 10, new EnergyCost(0, 0, 0, 0, 1, 0, 0)), new Attack("Self Destruct", "offensive", "Does 10 damage to each Pokémon on each player's Bench. (Don't apply Weakness and Resistance for Benched Pokémon.) Magnemite does 40 damage to itself.", 40, new EnergyCost(1, 0, 0, 0, 1, 0, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(43, "Abra", "basic", 30, 30, 'p', 'p', 'n', 0, "Using its ability to read minds, it will identify impending danger and teleport to safety.", "..\\..\\Img\\BSPainted\\BS_043.jpg", new Attack("Psychock", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Paralyzed.", 10, new EnergyCost(0, 0, 0, 0, 0, 1, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(43, "Abra", "basic", 30, 30, 'p', 'p', 'n', 0, "Using its ability to read minds, it will identify impending danger and teleport to safety.", "..\\..\\Img\\BSPainted\\BS_043.jpg", new Attack("Psychock", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Paralyzed.", 10, new EnergyCost(0, 0, 0, 0, 0, 1, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(43, "Abra", "basic", 30, 30, 'p', 'p', 'n', 0, "Using its ability to read minds, it will identify impending danger and teleport to safety.", "..\\..\\Img\\BSPainted\\BS_043.jpg", new Attack("Psychock", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Paralyzed.", 10, new EnergyCost(0, 0, 0, 0, 0, 1, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(32, "Kadabra", "second", "Abra", 60, 60, 'p', 'p', 'n', 3, "It emits special alpha waves from its body that induce headaches even to those just nearby.", "..\\..\\Img\\BS\\BS_032.jpg", new Attack("Recover", "defensive", "Discard 1 Psychic Energy card attached to Kadabra in order to use this attack. Remove all damage counters from Kadabra.", 0, new EnergyCost(0, 0, 0, 0, 0, 2, 0)), new Attack("Super Psy", "offensive", "", 50, new EnergyCost(1, 0, 0, 0, 0, 2, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(50, "Gastly", "basic", 30, 30, 'p', 'n', 'l', 0, "Almost invisible, this gaseous Pokémon cloaks the target and puts it to sleep without notice.", "..\\..\\Img\\BSPainted\\BS_050.jpg", new Attack("Sleeping Gas", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Asleep.", 0, new EnergyCost(0, 0, 0, 0, 0, 1, 0)), new Attack("Destiny Bond", "defensive", "Discard 1 Psychic Energy card attached to Gastly in order to use this attack. If a Pokémon Knocks Out Gastly during your opponent's next turn, Knock Out that Pokémon.", 0, new EnergyCost(1, 0, 0, 0, 0, 1, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(50, "Gastly", "basic", 30, 30, 'p', 'n', 'l', 0, "Almost invisible, this gaseous Pokémon cloaks the target and puts it to sleep without notice.", "..\\..\\Img\\BSPainted\\BS_050.jpg", new Attack("Sleeping Gas", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Asleep.", 0, new EnergyCost(0, 0, 0, 0, 0, 1, 0)), new Attack("Destiny Bond", "defensive", "Discard 1 Psychic Energy card attached to Gastly in order to use this attack. If a Pokémon Knocks Out Gastly during your opponent's next turn, Knock Out that Pokémon.", 0, new EnergyCost(1, 0, 0, 0, 0, 1, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(50, "Gastly", "basic", 30, 30, 'p', 'n', 'l', 0, "Almost invisible, this gaseous Pokémon cloaks the target and puts it to sleep without notice.", "..\\..\\Img\\BSPainted\\BS_050.jpg", new Attack("Sleeping Gas", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Asleep.", 0, new EnergyCost(0, 0, 0, 0, 0, 1, 0)), new Attack("Destiny Bond", "defensive", "Discard 1 Psychic Energy card attached to Gastly in order to use this attack. If a Pokémon Knocks Out Gastly during your opponent's next turn, Knock Out that Pokémon.", 0, new EnergyCost(1, 0, 0, 0, 0, 1, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(29, "Haunter", "second", "Gastly", 60, 60, 'p', 'n', 'l', 1, "Because of its ability to slip through block walls, it is said to be from another dimension.", "..\\..\\Img\\BSPainted\\BS_029.jpg", new Attack("Hypnosis", "offensive", "The Defending Pokémon is now Asleep.", 0, new EnergyCost(0, 0, 0, 0, 0, 1, 0)), new Attack("Dream Eater", "offensive", "You can't use this attack unless the Defending Pokémon is Asleep.", 50, new EnergyCost(0, 0, 0, 0, 0, 2, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(29, "Haunter", "second", "Gastly", 60, 60, 'p', 'n', 'l', 1, "Because of its ability to slip through block walls, it is said to be from another dimension.", "..\\..\\Img\\BSPainted\\BS_029.jpg", new Attack("Hypnosis", "offensive", "The Defending Pokémon is now Asleep.", 0, new EnergyCost(0, 0, 0, 0, 0, 1, 0)), new Attack("Dream Eater", "offensive", "You can't use this attack unless the Defending Pokémon is Asleep.", 50, new EnergyCost(0, 0, 0, 0, 0, 2, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(49, "Drowzee", "basic", 50, 50, 'p', 'p', 'n', 1, "Puts enemies to sleep, then eats their dreams. Occasionally gets sick from eating bad dreams.", "..\\..\\Img\\BS\\BS_049.jpg", new Attack("Pound", "offensive", "", 10, new EnergyCost(1, 0, 0, 0, 0, 0, 0)), new Attack("Confuse Ray", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Confused.", 10, new EnergyCost(0, 0, 0, 0, 0, 2, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(49, "Drowzee", "basic", 50, 50, 'p', 'p', 'n', 1, "Puts enemies to sleep, then eats their dreams. Occasionally gets sick from eating bad dreams.", "..\\..\\Img\\BS\\BS_049.jpg", new Attack("Pound", "offensive", "", 10, new EnergyCost(1, 0, 0, 0, 0, 0, 0)), new Attack("Confuse Ray", "offensive", "Flip a coin. If heads, the Defending Pokémon is now Confused.", 10, new EnergyCost(0, 0, 0, 0, 0, 2, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(31, "Jynx", "basic", 70, 70, 'p', 'p', 'n', 2, "Merely by meditating, the Pokémon launches a powerful psychic energy attack.", "..\\..\\Img\\BSPainted\\BS_031.jpg", new Attack("Doubleslap", "offensive", "Flip 2 coins. This attack does 10 damage times the number of heads.", 10, new EnergyCost(0, 0, 0, 0, 0, 1, 0)), new Attack("Meditate", "offensive", "Does 20 damage plus 10 more damage for each damage counter on the Defending Pokémon.", 20, new EnergyCost(1, 0, 0, 0, 0, 2, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(31, "Jynx", "basic", 70, 70, 'p', 'p', 'n', 2, "Merely by meditating, the Pokémon launches a powerful psychic energy attack.", "..\\..\\Img\\BSPainted\\BS_031.jpg", new Attack("Doubleslap", "offensive", "Flip 2 coins. This attack does 10 damage times the number of heads.", 10, new EnergyCost(0, 0, 0, 0, 0, 1, 0)), new Attack("Meditate", "offensive", "Does 20 damage plus 10 more damage for each damage counter on the Defending Pokémon.", 20, new EnergyCost(1, 0, 0, 0, 0, 2, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(10, "Mewtwo", "basic", 60, 60, 'p', 'p', 'n', 3, "A scientist created this Pokémon after years of horrific gene-splicing and DNA engineering experiments.", "..\\..\\Img\\BSPainted\\BS_010.jpg", new Attack("Psychic", "offensive", "Does 10 damage plus 10 more damage for each Energy card attached to the Defending Pokémon.", 10, new EnergyCost(1, 0, 0, 0, 0, 1, 0)), new Attack("Barrier", "defensive", "Discard 1 Psychic Energy card attached to Mewtwo in order to use this attack. During your opponent's next turn, prevent all effects of attacks, including damage, done to Mewtwo.", 0, new EnergyCost(0, 0, 0, 0, 0, 2, 0)), new List<char>(), false));
            ai.AddToDeck(new Pokemon(91, "Bill", "trainer", "global", 't', "Draw 2 cards.", "..\\..\\Img\\BS\\BS_091.jpg"));
            ai.AddToDeck(new Pokemon(91, "Bill", "trainer", "global", 't', "Draw 2 cards.", "..\\..\\Img\\BS\\BS_091.jpg"));
            ai.AddToDeck(new Pokemon(71, "Computer Search", "global", "trainer", 't', "Discard 2 of the other cards from your hand in order to search your deck for any card and put it into your hand. Shuffle your deck afterward.", "..\\..\\Img\\BS\\BS_071.jpg"));
            ai.AddToDeck(new Pokemon(80, "Defender", "trainer", "directed", 't', "Attach Defender to 1 of your Pokémon. At the end of your opponent's next turn, discard Defender. Damage done to that Pokémon by attacks is reduced by 20 (after applying Weakness and Resistance).", "..\\..\\Img\\BS\\BS_080.jpg"));
            ai.AddToDeck(new Pokemon(93, "Gust of Wind", "trainer", "global", 't', "Choose 1 of your opponent's Benched Pokémon and switch it with his or her Active Pokémon.", "..\\..\\Img\\BS\\BS_093.jpg"));
            ai.AddToDeck(new Pokemon(93, "Gust of Wind", "trainer", "global", 't', "Choose 1 of your opponent's Benched Pokémon and switch it with his or her Active Pokémon.", "..\\..\\Img\\BS\\BS_093.jpg"));
            ai.AddToDeck(new Pokemon(94, "Potion", "trainer", "directed", 't', "Remove up to 2 damage counters from 1 of your Pokémon.", "..\\..\\Img\\BS\\BS_094.jpg"));
            ai.AddToDeck(new Pokemon(88, "Professor Oak", "trainer", "global", 't', "Discard your hand, then draw 7 cards.", "..\\..\\Img\\BS\\BS_088.jpg"));
            ai.AddToDeck(new Pokemon(90, "Super Potion", "trainer", "directed", 't', "Discard 1 Energy card attached to 1 of your own Pokémon in order to remove up to 4 damage counters from that Pokémon.", "..\\..\\Img\\BS\\BS_090.jpg"));
            ai.AddToDeck(new Pokemon(95, "Switch", "trainer", "global", 't', "Switch 1 of your Benched Pokémon with your Active Pokémon.", "..\\..\\Img\\BS\\BS_095.jpg"));
            ai.AddToDeck(new Pokemon(95, "Switch", "trainer", "global", 't', "Switch 1 of your Benched Pokémon with your Active Pokémon.", "..\\..\\Img\\BS\\BS_095.jpg"));
            ai.AddToDeck(new Pokemon(100, "Lightning Energy", "energy", 'e', "..\\..\\Img\\BS\\BS_100.jpg"));
            ai.AddToDeck(new Pokemon(100, "Lightning Energy", "energy", 'e', "..\\..\\Img\\BS\\BS_100.jpg"));
            ai.AddToDeck(new Pokemon(100, "Lightning Energy", "energy", 'e', "..\\..\\Img\\BS\\BS_100.jpg"));
            ai.AddToDeck(new Pokemon(100, "Lightning Energy", "energy", 'e', "..\\..\\Img\\BS\\BS_100.jpg"));
            ai.AddToDeck(new Pokemon(100, "Lightning Energy", "energy", 'e', "..\\..\\Img\\BS\\BS_100.jpg"));
            ai.AddToDeck(new Pokemon(100, "Lightning Energy", "energy", 'e', "..\\..\\Img\\BS\\BS_100.jpg"));
            ai.AddToDeck(new Pokemon(100, "Lightning Energy", "energy", 'e', "..\\..\\Img\\BS\\BS_100.jpg"));
            ai.AddToDeck(new Pokemon(100, "Lightning Energy", "energy", 'e', "..\\..\\Img\\BS\\BS_100.jpg"));
            ai.AddToDeck(new Pokemon(100, "Lightning Energy", "energy", 'e', "..\\..\\Img\\BS\\BS_100.jpg"));
            ai.AddToDeck(new Pokemon(100, "Lightning Energy", "energy", 'e', "..\\..\\Img\\BS\\BS_100.jpg"));
            ai.AddToDeck(new Pokemon(100, "Lightning Energy", "energy", 'e', "..\\..\\Img\\BS\\BS_100.jpg"));
            ai.AddToDeck(new Pokemon(100, "Lightning Energy", "energy", 'e', "..\\..\\Img\\BS\\BS_100.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));
            ai.AddToDeck(new Pokemon(101, "Psychic Energy", "energy", 'p', "..\\..\\Img\\BS\\BS_101.jpg"));

            player.Shuffle();
            ai.Shuffle();

            


            DeckSize.Text = "x" + player.NumberOfCards().ToString();
            ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
            OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();

            PlayerHpBar.Visible = false;
            RemHp.Visible = false;
            MaxHp.Visible = false;
            HpLabel1.Visible = false;

            OpponentHpBar.Visible = false;
            OpCardName.Visible = false;
            OMaxHp.Visible = false;
            HpLabel2.Visible = false;
            OpponentDeck.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            Deck.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");

            EnergyBox2.Visible = false;
            RightClickMenu.Enabled = false;

            
            playBackgroundMusic();
            PictureZoom.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            OpponentZoom.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");

            HandIcon1.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            HandIcon2.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            HandIcon3.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");

            gameMessage.Text = "Welcome to the game please start by drawing 7 cards";

            UpdateHandView();
            UpdateBenchView();
            AIUpdateBenchView();
            UpdateActivePokemonView();
            UpdateAIActivePokemonView();
            UpdateDiscardView();
            UpdateAIDiscardView();
        }

        private bool GameOver()
        {
            if(player.NumberOfCards() <= 0 || playerPrizes == 0)
            {
                return true;
            }
            else if(ai.NumberOfCards() <= 0 || aiPrizes == 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            System.Console.WriteLine("After assignment, myInt: {0}", 3);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpponentHpBar.Value -= 20;
            OMaxHp.Text = OpponentHpBar.Value.ToString();
            gameMessage.Text = "You attacked your enemy with 20 damage";
            playSimpleSound();
            PictureZoom.Image = Image.FromFile("..\\..\\Img\\BS\\BS_001.jpg");
        }

        private void playSimpleSound()
        {
            SoundPlayer simpleSound = new SoundPlayer("..\\..\\Sounds\\draw.wav");
            simpleSound.Play();
        }

        private void playBackgroundMusic()
        {
            SoundPlayer backgroundSound = new SoundPlayer("..\\..\\Sounds\\aquatic_ambience.wav");
            backgroundSound.Play();
        }

        private void HoverOn_Click(object sender, EventArgs e)
        {
            int num = 0;
            PictureBox n = (PictureBox)sender;
            switch (n.Name)
            {
                case "Hand1":
                    num = 0;
                    break;
                case "Hand2":
                    num = 1;
                    break;
                case "Hand3":
                    num = 2;
                    break;
                case "Hand4":
                    num = 3;
                    break;
                case "Hand5":
                    num = 4;
                    break;
                case "Hand6":
                    num = 5;
                    break;
                case "Hand7":
                    num = 6;
                    break;
                case "Hand8":
                    num = 7;
                    break;
                case "Hand9":
                    num = 8;
                    break;
                case "Hand10":
                    num = 9;
                    break;
                default:
                    break;
            }

            ZoomInfo(num);

            int bench_size = bench.NumberOfCards();

            if(player_Hand.ShowType(num) == "basic")
            {
                (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Clear();
            }
            else if(player_Hand.ShowType(num) == "trainer" && player_Hand.ShowImpact(num) == "global")
            {
                (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Clear();
            }
            else
            {
                switch (bench_size)
                {
                    case 0:
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Clear();
                        if (active_Pokemon.ShowName() != "null")
                        {
                            (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(active_Pokemon.ShowName()).Click += new EventHandler(pokemonEnergyLoadClick);                           
                        }
                        break;
                    case 1:
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Clear();                       
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(0)).Click += new EventHandler(pokemonEnergyLoadClick);

                        if (active_Pokemon.ShowName() != "null")
                        {
                            (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(active_Pokemon.ShowName()).Click += new EventHandler(pokemonEnergyLoadClick);
                        }
                        break;
                    case 2:
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Clear();                 
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(0)).Click += new EventHandler(pokemonEnergyLoadClick);
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(1)).Click += new EventHandler(pokemonEnergyLoadClick);
                        if (active_Pokemon.ShowName() != "null")
                        {
                            (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(active_Pokemon.ShowName()).Click += new EventHandler(pokemonEnergyLoadClick);                            
                        }
                        break;
                    case 3:
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Clear();                        
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(0)).Click += new EventHandler(pokemonEnergyLoadClick);
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(1)).Click += new EventHandler(pokemonEnergyLoadClick);
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(2)).Click += new EventHandler(pokemonEnergyLoadClick);
                        if (active_Pokemon.ShowName() != "null")
                        {
                            (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(active_Pokemon.ShowName()).Click += new EventHandler(pokemonEnergyLoadClick);
                        }
                        break;
                    case 4:
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Clear();                       
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(0)).Click += new EventHandler(pokemonEnergyLoadClick);
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(1)).Click += new EventHandler(pokemonEnergyLoadClick);
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(2)).Click += new EventHandler(pokemonEnergyLoadClick);
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(3)).Click += new EventHandler(pokemonEnergyLoadClick);
                        if (active_Pokemon.ShowName() != "null")
                        {
                            (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(active_Pokemon.ShowName()).Click += new EventHandler(pokemonEnergyLoadClick);
                        }
                        break;
                    case 5:
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Clear();                       
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(0)).Click += new EventHandler(pokemonEnergyLoadClick);
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(1)).Click += new EventHandler(pokemonEnergyLoadClick);
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(2)).Click += new EventHandler(pokemonEnergyLoadClick);
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(3)).Click += new EventHandler(pokemonEnergyLoadClick);
                        (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(bench.ShowName(4)).Click += new EventHandler(pokemonEnergyLoadClick);
                        if (active_Pokemon.ShowName() != "null")
                        {
                            (RightClickMenu.Items[0] as ToolStripMenuItem).DropDownItems.Add(active_Pokemon.ShowName()).Click += new EventHandler(pokemonEnergyLoadClick);
                        }
                        break;
                    
                }

                
            }
            
        }

        private void HoverOff_Click(object sender, EventArgs e)
        {
            PictureZoom.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");

            HpLabel1.Visible = false;
            MaxHp.Visible = false;
            RemHp.Visible = false;
            PlayerHpBar.Visible = false;
            EnergyBox1.Visible = false;
            CardName.Visible = false;
        }

        private void HoverOnBench_Click(object sender, EventArgs e)
        {
            int num = 0;
            PictureBox n = (PictureBox)sender;
            switch (n.Name)
            {
                case "PictureBench1":
                    num = 0;
                    break;
                case "PictureBench2":
                    num = 1;
                    break;
                case "PictureBench3":
                    num = 2;
                    break;
                case "PictureBench4":
                    num = 3;
                    break;
                case "PictureBench5":
                    num = 4;
                    break;
                default:
                    break;
            }

            PictureZoom.Image = Image.FromFile(bench.ShowCard(num));
            RemHp.Text = bench.ShowRemHp(num).ToString();
            CardName.Visible = true;
            CardName.Text = bench.ShowName(num);
            MaxHp.Text = bench.ShowHp(num).ToString();
            ZoomBenchInfo(num);
        }

        private void HoverOnAI_Bench_Click(object sender, EventArgs e)
        {
            int num = 0;
            PictureBox n = (PictureBox)sender;
            switch (n.Name)
            {
                case "OpponentBench1":
                    num = 0;
                    break;
                case "OpponentBench2":
                    num = 1;
                    break;
                case "OpponentBench3":
                    num = 2;
                    break;
                case "OpponentBench4":
                    num = 3;
                    break;
                case "OpponentBench5":
                    num = 4;
                    break;
                default:
                    break;
            }

            OpponentZoom.Image = Image.FromFile(aibench.ShowCard(num));
            
            OpCardName.Text = aibench.ShowName(num);
            OMaxHp.Text = aibench.ShowHp(num).ToString();

            OpCardName.Visible = true;
            HpLabel2.Visible = true;
            OMaxHp.Visible = true;
            OpponentHpBar.Maximum = aibench.ShowHp(num);
            OpponentHpBar.Value = aibench.ShowRemHp(num);
            OpponentHpBar.Visible = true;
            EnergyBox2.Visible = true;

            switch (aibench.ShowEnergy(num))
            {
                case 'f':
                    EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fire.gif");
                    break;
                case 'w':
                    EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Water.gif");
                    break;
                case 'g':
                    EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Grass.gif");
                    break;
                case 'p':
                    EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Psychic.gif");
                    break;
                case 'l':
                    EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fighting.gif");
                    break;
                case 'e':
                    EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Lightning.gif");
                    break;
                case 'c':
                    EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Colorless.gif");
                    break;
                default:
                    EnergyBox2.Visible = false;
                    break;
            }
        }
        private void HoverOnActive_Click(object sender, EventArgs e)
        {
            if (active_Pokemon.ShowName() != "null")
            {
                CardName.Text = active_Pokemon.ShowName();
                PlayerHpBar.Maximum = active_Pokemon.ShowHP();
                PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                MaxHp.Text = active_Pokemon.ShowHP().ToString();
                RemHp.Text = active_Pokemon.ShowRemHP().ToString();
                PictureZoom.Image = Image.FromFile(active_Pokemon.ShowImage());

                CardName.Visible = true;
                HpLabel1.Visible = true;
                MaxHp.Visible = true;
                RemHp.Visible = true;
                PlayerHpBar.Visible = true;
                EnergyBox1.Visible = true;

                switch (active_Pokemon.ShowEnergy())
                {
                    case 'f':
                        EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fire.gif");
                        break;
                    case 'w':
                        EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Water.gif");
                        break;
                    case 'g':
                        EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Grass.gif");
                        break;
                    case 'p':
                        EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Psychic.gif");
                        break;
                    case 'l':
                        EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fighting.gif");
                        break;
                    case 'e':
                        EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Lightning.gif");
                        break;
                    case 'c':
                        EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Colorless.gif");
                        break;
                    default:
                        EnergyBox1.Visible = false;
                        break;
                }

                switch (active_Pokemon.NumberOfAttacks())
                {
                    case 0:
                        (RightClickMenu3.Items[0] as ToolStripMenuItem).DropDownItems.Clear();
                        break;
                    case 1:
                        (RightClickMenu3.Items[0] as ToolStripMenuItem).DropDownItems.Clear();
                        (RightClickMenu3.Items[0] as ToolStripMenuItem).DropDownItems.Add(active_Pokemon.ShowAttackName(1)).Click += new EventHandler(PerformAttack);
                        break;
                    case 2:
                        (RightClickMenu3.Items[0] as ToolStripMenuItem).DropDownItems.Clear();
                        (RightClickMenu3.Items[0] as ToolStripMenuItem).DropDownItems.Add(active_Pokemon.ShowAttackName(1)).Click += new EventHandler(PerformAttack);
                        (RightClickMenu3.Items[0] as ToolStripMenuItem).DropDownItems.Add(active_Pokemon.ShowAttackName(2)).Click += new EventHandler(PerformAttack);
                        break;
                    case 3:
                        (RightClickMenu3.Items[0] as ToolStripMenuItem).DropDownItems.Clear();
                        (RightClickMenu3.Items[0] as ToolStripMenuItem).DropDownItems.Add(active_Pokemon.ShowAttackName(1)).Click += new EventHandler(PerformAttack);
                        (RightClickMenu3.Items[0] as ToolStripMenuItem).DropDownItems.Add(active_Pokemon.ShowAttackName(2)).Click += new EventHandler(PerformAttack);
                        (RightClickMenu3.Items[0] as ToolStripMenuItem).DropDownItems.Add(active_Pokemon.ShowAttackName(3)).Click += new EventHandler(PerformAttack);
                        break;

                }
            }
            else
            {
                ActivePokemonZoomAllInvisible();
            }
        }

        private void HoverOnAI_Active_Click(object sender, EventArgs e)
        {
            if (ai_Active_Pokemon.ShowName() != "null")
            {
                OpCardName.Text = ai_Active_Pokemon.ShowName();
                OpponentHpBar.Maximum = ai_Active_Pokemon.ShowHP();
                OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                OMaxHp.Text = ai_Active_Pokemon.ShowHP().ToString();

                OpponentZoom.Image = Image.FromFile(ai_Active_Pokemon.ShowImage());

                OpCardName.Visible = true;
                HpLabel2.Visible = true;
                OMaxHp.Visible = true;
                OpponentHpBar.Visible = true;
                EnergyBox2.Visible = true;

                switch (ai_Active_Pokemon.ShowEnergy())
                {
                    case 'f':
                        EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fire.gif");
                        break;
                    case 'w':
                        EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Water.gif");
                        break;
                    case 'g':
                        EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Grass.gif");
                        break;
                    case 'p':
                        EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Psychic.gif");
                        break;
                    case 'l':
                        EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fighting.gif");
                        break;
                    case 'e':
                        EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Lightning.gif");
                        break;
                    case 'c':
                        EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Colorless.gif");
                        break;
                    default:
                        EnergyBox2.Visible = false;
                        break;
                }
            }
            else
            {
                AIActivePokemonZoomAllInvisible();
            }
        }
        private void HoverOnDiscard_Click(object sender, EventArgs e)
        {
           //erase all the menu
           (RightClickDiscard.Items[0] as ToolStripMenuItem).DropDownItems.Clear();

            //based on amount of cards in the discard pile then will soon show amount of cards
            for (int i = 0; i < discard.TotalNumber(); i++)
            {
                if(discard.TotalNumber() == 0)
                {
                           //it does not show any cards
                }
                else
                {
                           //it shows as many cards as the ones that are in the discard
                    (RightClickDiscard.Items[0] as ToolStripMenuItem).DropDownItems.Add(discard.ShowName(i)).MouseHover += new EventHandler(ShowDiscardCard);
                }
            }
           
        }
        private void HoverOnAIDiscard_Click(object sender, EventArgs e)
        {
            //erase all the menu
            (RightClickAIDiscard.Items[0] as ToolStripMenuItem).DropDownItems.Clear();

            //based on amount of cards in the discard pile then will soon show amount of cards
            for (int i = 0; i < ai_discard.TotalNumber(); i++)
            {
                if (ai_discard.TotalNumber() == 0)
                {
                    //it does not show any cards
                }
                else
                {
                    //it shows as many cards as the ones that are in the discard
                    (RightClickAIDiscard.Items[0] as ToolStripMenuItem).DropDownItems.Add(ai_discard.ShowName(i)).MouseHover += new EventHandler(ShowAIDiscardCard);
                }
            }

        }
        private void ShowDiscardCard(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            PlayerHpBar.Visible = false;
            CardName.Visible = true;
            HpLabel1.Visible = false;
            MaxHp.Visible = false;
            RemHp.Visible = false;
            EnergyBox1.Visible = false;
            
            if (item != null)
            {

                int index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);
                PictureZoom.Image = Image.FromFile(discard.ShowCard(index));
                CardName.Text = discard.ShowName(index);
              
            };
        }
        private void ShowAIDiscardCard(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            OpponentHpBar.Visible = false;
            OpCardName.Visible = true;
            HpLabel2.Visible = false;
            OMaxHp.Visible = false;
            EnergyBox2.Visible = false;

            if (item != null)
            {

                int index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);
                OpponentZoom.Image = Image.FromFile(ai_discard.ShowCard(index));
                OpCardName.Text = ai_discard.ShowName(index);

            };
        }
        public void ActivePokemonZoomAllInvisible()
        {
            PictureZoom.Visible = false;
            RemHp.Visible = false;
            MaxHp.Visible = false;
            CardName.Visible = false;
            EnergyBox1.Visible = false;
            PlayerHpBar.Visible = false;
        }

        public void AIActivePokemonZoomAllInvisible()
        {
            OpponentZoom.Visible = false;
            OMaxHp.Visible = false;
            OpCardName.Visible = false;
            EnergyBox2.Visible = false;
            OpponentHpBar.Visible = false;
        }
        public void Draw_Click(object sender, EventArgs e)
        {
            if(isPreGameTurn == true)
            {
                player.Shuffle();
                player.Shuffle();
                SoundPlayer simpleSound = new SoundPlayer("..\\..\\Sounds\\draw.wav");
                simpleSound.Play();

                Draw.Visible = false;


                player_Hand.DrawCard(player.DrawCard());
                player_Hand.DrawCard(player.DrawCard());
                player_Hand.DrawCard(player.DrawCard());
                player_Hand.DrawCard(player.DrawCard());
                player_Hand.DrawCard(player.DrawCard());
                player_Hand.DrawCard(player.DrawCard());
                player_Hand.DrawCard(player.DrawCard());

                ai_Hand.DrawCard(ai.DrawCard());
                ai_Hand.DrawCard(ai.DrawCard());
                ai_Hand.DrawCard(ai.DrawCard());
                ai_Hand.DrawCard(ai.DrawCard());
                ai_Hand.DrawCard(ai.DrawCard());
                ai_Hand.DrawCard(ai.DrawCard());
                ai_Hand.DrawCard(ai.DrawCard());

                DeckSize.Text = "x" + player.NumberOfCards().ToString();
                ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
                OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
                UpdateHandView();
                if (player_Hand.NumOfBasicPokemon() == 0 && ai_Hand.NumOfBasicPokemon() != 0)
                {
                    gameMessage.Text = "As you do not have any Basic Pokémon, you should perform Mulligan and draw another 7 cards.";

                    StartCheck.Visible = false;
                    Mulligan.Visible = true;
                    FlipCoin.Visible = false;
                }
                else if (player_Hand.NumOfBasicPokemon() == 0 && ai_Hand.NumOfBasicPokemon() == 0)
                {
                    gameMessage.Text = "Apparently you and your opponent do not have any Basic Pokémons, so both of you should perform Mulligan.";

                    StartCheck.Visible = false;
                    Mulligan2.Location = new Point(576, 957);
                    Mulligan2.Visible = true;
                    FlipCoin.Visible = false;
                }
                else if(player_Hand.NumOfBasicPokemon() != 0 && ai_Hand.NumOfBasicPokemon() == 0)
                {
                    gameMessage.Text = "Your opponent does not have any Basic Pokémons, so he should perform Mulligan.";

                    StartCheck.Visible = false;
                    Mulligan3.Location = new Point(576, 957);
                    Mulligan3.Visible = true;
                    FlipCoin.Visible = false;
                }
                else
                {
                    RightClickMenu.Enabled = true;
                    StartCheck.Visible = false;
                    Done.Visible = false;
                    FlipCoin.Visible = false;
                    gameMessage.Text = "You drew 7 cards from your Deck and your opponent also does. Please play Basic Pókemons";
                }
            }
            else
            {
                player_Hand.DrawCard(player.DrawCard());
                DeckSize.Text = "x" + player.NumberOfCards().ToString();
                UpdateHandView();
                if(active_Pokemon.ThereIsActivePokemon() == true)
                {
                    StartCheck.Visible = true;
                    Done.Visible = true;
                }
                else
                {
                    StartCheck.Visible = false;
                    Done.Visible = false;
                }                
            }
       
        }


        public void UpdateActivePokemonView()
        {
            

            if (active_Pokemon.ShowName() != "null")
            {
                PictureActive.Image = Image.FromFile(active_Pokemon.ShowImage());
                PictureActive.Visible = true;

                if (active_Pokemon.EnergyLoadedCount() == 0)
                {
                    ActiveEnergy1.Visible = false;
                    ActiveEnergy2.Visible = false;
                    ActiveEnergy3.Visible = false;
                    ActiveEnergy4.Visible = false;
                    ActiveEnergy5.Visible = false;
                }
                else if (active_Pokemon.EnergyLoadedCount() == 1)
                {
                    ActiveEnergy1.Visible = true;
                    ActiveEnergy1.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(0));
                    ActiveEnergy2.Visible = false;
                    ActiveEnergy3.Visible = false;
                    ActiveEnergy4.Visible = false;
                    ActiveEnergy5.Visible = false;
                }
                else if (active_Pokemon.EnergyLoadedCount() == 2)
                {
                    ActiveEnergy1.Visible = true;
                    ActiveEnergy1.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(0));
                    ActiveEnergy2.Visible = true;
                    ActiveEnergy2.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(1));
                    ActiveEnergy3.Visible = false;
                    ActiveEnergy4.Visible = false;
                    ActiveEnergy5.Visible = false;
                }
                else if (active_Pokemon.EnergyLoadedCount() == 3)
                {
                    ActiveEnergy1.Visible = true;
                    ActiveEnergy1.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(0));
                    ActiveEnergy2.Visible = true;
                    ActiveEnergy2.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(1));
                    ActiveEnergy3.Visible = true;
                    ActiveEnergy3.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(2));
                    ActiveEnergy4.Visible = false;
                    ActiveEnergy5.Visible = false;
                }
                else if (active_Pokemon.EnergyLoadedCount() == 4)
                {
                    ActiveEnergy1.Visible = true;
                    ActiveEnergy1.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(0));
                    ActiveEnergy2.Visible = true;
                    ActiveEnergy2.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(1));
                    ActiveEnergy3.Visible = true;
                    ActiveEnergy3.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(2));
                    ActiveEnergy4.Visible = true;
                    ActiveEnergy4.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(3));
                    ActiveEnergy5.Visible = false;
                }
                else if (active_Pokemon.EnergyLoadedCount() == 5)
                {
                    ActiveEnergy1.Visible = true;
                    ActiveEnergy1.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(0));
                    ActiveEnergy2.Visible = true;
                    ActiveEnergy2.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(1));
                    ActiveEnergy3.Visible = true;
                    ActiveEnergy3.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(2));
                    ActiveEnergy4.Visible = true;
                    ActiveEnergy4.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(3));
                    ActiveEnergy5.Visible = true;
                    ActiveEnergy5.Image = Image.FromFile(active_Pokemon.ReturnEnergyLoadedImg(4));
                }

            }
            else
            {
                PictureActive.Visible = false;
                ActiveEnergy1.Visible = false;
                ActiveEnergy2.Visible = false;
                ActiveEnergy3.Visible = false;
                ActiveEnergy4.Visible = false;
                ActiveEnergy5.Visible = false;
            }
        }

        public void UpdateAIActivePokemonView()
        {


            if (ai_Active_Pokemon.ShowName() != "null")
            {
                OpponentActive.Image = Image.FromFile(ai_Active_Pokemon.ShowImage());
                OpponentActive.Visible = true;

                if (ai_Active_Pokemon.EnergyLoadedCount() == 0)
                {
                    OActiveEnergy1.Visible = false;
                    OActiveEnergy2.Visible = false;
                    OActiveEnergy3.Visible = false;
                    OActiveEnergy4.Visible = false;
                    OActiveEnergy5.Visible = false;
                }
                else if (ai_Active_Pokemon.EnergyLoadedCount() == 1)
                {
                    OActiveEnergy1.Visible = true;
                    OActiveEnergy1.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(0));
                    OActiveEnergy2.Visible = false;
                    OActiveEnergy3.Visible = false;
                    OActiveEnergy4.Visible = false;
                    OActiveEnergy5.Visible = false;
                }
                else if (ai_Active_Pokemon.EnergyLoadedCount() == 2)
                {
                    OActiveEnergy1.Visible = true;
                    OActiveEnergy1.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(0));
                    OActiveEnergy2.Visible = true;
                    OActiveEnergy2.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(1));
                    OActiveEnergy3.Visible = false;
                    OActiveEnergy4.Visible = false;
                    OActiveEnergy5.Visible = false;
                }
                else if (ai_Active_Pokemon.EnergyLoadedCount() == 3)
                {
                    OActiveEnergy1.Visible = true;
                    OActiveEnergy1.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(0));
                    OActiveEnergy2.Visible = true;
                    OActiveEnergy2.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(1));
                    OActiveEnergy3.Visible = true;
                    OActiveEnergy3.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(2));
                    OActiveEnergy4.Visible = false;
                    OActiveEnergy5.Visible = false;
                }
                else if (ai_Active_Pokemon.EnergyLoadedCount() == 4)
                {
                    OActiveEnergy1.Visible = true;
                    OActiveEnergy1.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(0));
                    OActiveEnergy2.Visible = true;
                    OActiveEnergy2.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(1));
                    OActiveEnergy3.Visible = true;
                    OActiveEnergy3.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(2));
                    OActiveEnergy4.Visible = true;
                    OActiveEnergy4.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(3));
                    OActiveEnergy5.Visible = false;
                }
                else if (ai_Active_Pokemon.EnergyLoadedCount() == 5)
                {
                    OActiveEnergy1.Visible = true;
                    OActiveEnergy1.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(0));
                    OActiveEnergy2.Visible = true;
                    OActiveEnergy2.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(1));
                    OActiveEnergy3.Visible = true;
                    OActiveEnergy3.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(2));
                    OActiveEnergy4.Visible = true;
                    OActiveEnergy4.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(3));
                    OActiveEnergy5.Visible = true;
                    OActiveEnergy5.Image = Image.FromFile(ai_Active_Pokemon.ReturnEnergyLoadedImg(4));
                }

            }
            else
            {
                OpponentActive.Visible = false;
                OActiveEnergy1.Visible = false;
                OActiveEnergy2.Visible = false;
                OActiveEnergy3.Visible = false;
                OActiveEnergy4.Visible = false;
                OActiveEnergy5.Visible = false;
            }
        }
        public void UpdateHandView()
        {
            
            int hand_size = player_Hand.NumberOfCards();

            Hand1.Visible = false;
            Hand2.Visible = false;
            Hand3.Visible = false;
            Hand4.Visible = false;
            Hand5.Visible = false;
            Hand6.Visible = false;
            Hand7.Visible = false;
            Hand8.Visible = false;
            Hand9.Visible = false;
            Hand10.Visible = false;
            switch (hand_size)
            {
                case 0:            
                    break;

                case 1:
                    Hand1.Location = new Point(384, 830);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    
                    break;

                case 2:
                    Hand1.Location = new Point(384, 830);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 830);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
           
                    break;
                case 3:
                    Hand1.Location = new Point(384, 830);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 830);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 830);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));

                    break;
                case 4:
                    Hand1.Location = new Point(384, 830);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 830);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 830);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 830);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));

                    break;
                case 5:
                    Hand1.Location = new Point(384, 830);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 830);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 830);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 830);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 830);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(player_Hand.ShowCard(4));
                    break;

                case 6:
                    Hand1.Location = new Point(384, 830);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 830);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 830);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 830);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 830);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(player_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 830);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(player_Hand.ShowCard(5));
                    break;

                case 7:
                    Hand1.Location = new Point(384, 830);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 830);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 830);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 830);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 830);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(player_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 830);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(player_Hand.ShowCard(5));
                    Hand7.Location = new Point(911, 830);
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(player_Hand.ShowCard(6));
                    break;

                case 8:
                    Hand1.Location = new Point(384, 830);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 830);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 830);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 830);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 830);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(player_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 830);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(player_Hand.ShowCard(5));
                    Hand7.Location = new Point(911, 830);
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(player_Hand.ShowCard(6));
                    Hand8.Location = new Point(999, 830);
                    Hand8.Visible = true;
                    Hand8.Image = Image.FromFile(player_Hand.ShowCard(7));
                    break;

                case 9:
                    Hand1.Location = new Point(384, 830);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 830);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 830);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 830);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 830);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(player_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 830);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(player_Hand.ShowCard(5));
                    Hand7.Location = new Point(911, 830);
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(player_Hand.ShowCard(6));
                    Hand8.Location = new Point(999, 830);
                    Hand8.Visible = true;
                    Hand8.Image = Image.FromFile(player_Hand.ShowCard(7));
                    Hand9.Location = new Point(1087, 830);
                    Hand9.Visible = true;
                    Hand9.Image = Image.FromFile(player_Hand.ShowCard(8));
                    break;

                case 10:
                    Hand1.Location = new Point(384, 830);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 830);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 830);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 830);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 830);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(player_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 830);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(player_Hand.ShowCard(5));
                    Hand7.Location = new Point(911, 830);
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(player_Hand.ShowCard(6));
                    Hand8.Location = new Point(999, 830);
                    Hand8.Visible = true;
                    Hand8.Image = Image.FromFile(player_Hand.ShowCard(7));
                    Hand9.Location = new Point(1087, 830);
                    Hand9.Visible = true;
                    Hand9.Image = Image.FromFile(player_Hand.ShowCard(8));
                    Hand10.Location = new Point(1175, 830);
                    Hand10.Visible = true;
                    Hand10.Image = Image.FromFile(player_Hand.ShowCard(9));
                    break;

            }
        }

        public void ShowAIHandView()
        {
            int hand_size = ai_Hand.NumberOfCards();

            Hand1.Visible = false;
            Hand2.Visible = false;
            Hand3.Visible = false;
            Hand4.Visible = false;
            Hand5.Visible = false;
            Hand6.Visible = false;
            Hand7.Visible = false;
            Hand8.Visible = false;
            Hand9.Visible = false;
            Hand10.Visible = false;
            switch (hand_size)
            {
                case 0:
                    break;

                case 1:
                    Hand1.Location = new Point(384, 440);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(ai_Hand.ShowCard(0));

                    break;

                case 2:
                    Hand1.Location = new Point(384, 440);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(ai_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 440);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(ai_Hand.ShowCard(1));

                    break;
                case 3:
                    Hand1.Location = new Point(384, 440);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(ai_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 440);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(ai_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 440);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(ai_Hand.ShowCard(2));

                    break;
                case 4:
                    Hand1.Location = new Point(384, 440);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(ai_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 440);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(ai_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 440);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(ai_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 440);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(ai_Hand.ShowCard(3));

                    break;
                case 5:
                    Hand1.Location = new Point(384, 440);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(ai_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 440);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(ai_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 440);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(ai_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 440);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(ai_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 440);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(ai_Hand.ShowCard(4));
                    break;

                case 6:
                    Hand1.Location = new Point(384, 440);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(ai_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 440);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(ai_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 440);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(ai_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 440);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(ai_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 440);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(ai_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 440);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(ai_Hand.ShowCard(5));
                    break;

                case 7:
                    Hand1.Location = new Point(384, 440);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(ai_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 440);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(ai_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 440);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(ai_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 440);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(ai_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 440);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(ai_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 440);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(ai_Hand.ShowCard(5));
                    Hand7.Location = new Point(911, 440);
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(ai_Hand.ShowCard(6));
                    break;

                case 8:
                    Hand1.Location = new Point(384, 440);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(ai_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 440);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(ai_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 440);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(ai_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 440);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(ai_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 440);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(ai_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 440);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(ai_Hand.ShowCard(5));
                    Hand7.Location = new Point(911, 440);
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(ai_Hand.ShowCard(6));
                    Hand8.Location = new Point(999, 440);
                    Hand8.Visible = true;
                    Hand8.Image = Image.FromFile(ai_Hand.ShowCard(7));
                    break;

                case 9:
                    Hand1.Location = new Point(384, 440);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(ai_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 440);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(ai_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 440);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(ai_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 440);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(ai_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 440);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(ai_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 440);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(ai_Hand.ShowCard(5));
                    Hand7.Location = new Point(911, 440);
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(ai_Hand.ShowCard(6));
                    Hand8.Location = new Point(999, 440);
                    Hand8.Visible = true;
                    Hand8.Image = Image.FromFile(ai_Hand.ShowCard(7));
                    Hand9.Location = new Point(1087, 440);
                    Hand9.Visible = true;
                    Hand9.Image = Image.FromFile(ai_Hand.ShowCard(8));
                    break;

                case 10:
                    Hand1.Location = new Point(384, 440);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(ai_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 440);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(ai_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 440);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(ai_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 440);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(ai_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 440);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(ai_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 440);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(ai_Hand.ShowCard(5));
                    Hand7.Location = new Point(911, 440);
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(ai_Hand.ShowCard(6));
                    Hand8.Location = new Point(999, 440);
                    Hand8.Visible = true;
                    Hand8.Image = Image.FromFile(ai_Hand.ShowCard(7));
                    Hand9.Location = new Point(1087, 440);
                    Hand9.Visible = true;
                    Hand9.Image = Image.FromFile(ai_Hand.ShowCard(8));
                    Hand10.Location = new Point(1175, 440);
                    Hand10.Visible = true;
                    Hand10.Image = Image.FromFile(ai_Hand.ShowCard(9));
                    break;
            }
        }

        public void UpdateBenchView()
        {
            int bench_size = bench.NumberOfCards();

            PictureBench1.Visible = false;
            Bench1Energy1.Visible = false;
            Bench1Energy2.Visible = false;
            Bench1Energy3.Visible = false;
            Bench1Energy4.Visible = false;
            Bench1Energy5.Visible = false;

            PictureBench2.Visible = false;
            Bench2Energy1.Visible = false;
            Bench2Energy2.Visible = false;
            Bench2Energy3.Visible = false;
            Bench2Energy4.Visible = false;
            Bench2Energy5.Visible = false;

            PictureBench3.Visible = false;
            Bench3Energy1.Visible = false;
            Bench3Energy2.Visible = false;
            Bench3Energy3.Visible = false;
            Bench3Energy4.Visible = false;
            Bench3Energy5.Visible = false;

            PictureBench4.Visible = false;
            Bench4Energy1.Visible = false;
            Bench4Energy2.Visible = false;
            Bench4Energy3.Visible = false;
            Bench4Energy4.Visible = false;
            Bench4Energy5.Visible = false;

            PictureBench5.Visible = false;
            Bench5Energy1.Visible = false;
            Bench5Energy2.Visible = false;
            Bench5Energy3.Visible = false;
            Bench5Energy4.Visible = false;
            Bench5Energy5.Visible = false;

            switch (bench_size)
            {
                case 0:
                  
                    break;
                case 1:
                    PictureBench1.Visible = true;               
                    PictureBench1.Image = Image.FromFile(bench.ShowCard(0));
                    if(bench.LoadedEnergyCount(0) == 0)
                    {
                        Bench1Energy1.Visible = false;
                        Bench1Energy2.Visible = false;
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 1)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = false;
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 2)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 3)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 4)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = true;
                        Bench1Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 3));
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 5)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = true;
                        Bench1Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 3));
                        Bench1Energy5.Visible = true;
                        Bench1Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 4));
                    }

                    PictureBench2.Visible = false;
                    PictureBench3.Visible = false;
                    PictureBench4.Visible = false;
                    PictureBench5.Visible = false;
                    break;
                case 2:
                    PictureBench1.Visible = true;
                    PictureBench1.Image = Image.FromFile(bench.ShowCard(0));
                    if (bench.LoadedEnergyCount(0) == 0)
                    {
                        Bench1Energy1.Visible = false;
                        Bench1Energy2.Visible = false;
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 1)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = false;
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 2)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 3)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 4)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = true;
                        Bench1Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 3));
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 5)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = true;
                        Bench1Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 3));
                        Bench1Energy5.Visible = true;
                        Bench1Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 4));
                    }
                    

                    PictureBench2.Visible = true;
                    PictureBench2.Image = Image.FromFile(bench.ShowCard(1));

                    if (bench.LoadedEnergyCount(1) == 0)
                    {
                        Bench2Energy1.Visible = false;
                        Bench2Energy2.Visible = false;
                        Bench2Energy3.Visible = false;
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 1)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = false;
                        Bench2Energy3.Visible = false;
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 2)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = false;
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 3)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = true;
                        Bench2Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 2));
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 4)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = true;
                        Bench2Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 2));
                        Bench2Energy4.Visible = true;
                        Bench2Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 3));
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 5)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = true;
                        Bench2Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 2));
                        Bench2Energy4.Visible = true;
                        Bench2Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 3));
                        Bench2Energy5.Visible = true;
                        Bench2Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 4));
                    }

                    PictureBench3.Visible = false;
                    PictureBench4.Visible = false;
                    PictureBench5.Visible = false;
                    break;
                case 3:
                    PictureBench1.Visible = true;
                    PictureBench1.Image = Image.FromFile(bench.ShowCard(0));
                    if (bench.LoadedEnergyCount(0) == 0)
                    {
                        Bench1Energy1.Visible = false;
                        Bench1Energy2.Visible = false;
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 1)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = false;
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 2)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 3)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 4)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = true;
                        Bench1Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 3));
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 5)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = true;
                        Bench1Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 3));
                        Bench1Energy5.Visible = true;
                        Bench1Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 4));
                    }

                    PictureBench2.Visible = true;
                    PictureBench2.Image = Image.FromFile(bench.ShowCard(1));
                    if (bench.LoadedEnergyCount(1) == 0)
                    {
                        Bench2Energy1.Visible = false;
                        Bench2Energy2.Visible = false;
                        Bench2Energy3.Visible = false;
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 1)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = false;
                        Bench2Energy3.Visible = false;
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 2)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = false;
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 3)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = true;
                        Bench2Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 2));
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 4)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = true;
                        Bench2Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 2));
                        Bench2Energy4.Visible = true;
                        Bench2Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 3));
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 5)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = true;
                        Bench2Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 2));
                        Bench2Energy4.Visible = true;
                        Bench2Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 3));
                        Bench2Energy5.Visible = true;
                        Bench2Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 4));
                    }
                    PictureBench3.Visible = true;
                    PictureBench3.Image = Image.FromFile(bench.ShowCard(2));
                    if (bench.LoadedEnergyCount(2) == 0)
                    {
                        Bench3Energy1.Visible = false;
                        Bench3Energy2.Visible = false;
                        Bench3Energy3.Visible = false;
                        Bench3Energy4.Visible = false;
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 1)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = false;
                        Bench3Energy3.Visible = false;
                        Bench3Energy4.Visible = false;
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 2)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = true;
                        Bench3Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 1));
                        Bench3Energy3.Visible = false;
                        Bench3Energy4.Visible = false;
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 3)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = true;
                        Bench3Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 1));
                        Bench3Energy3.Visible = true;
                        Bench3Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 2));
                        Bench3Energy4.Visible = false;
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 4)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = true;
                        Bench3Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 1));
                        Bench3Energy3.Visible = true;
                        Bench3Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 2));
                        Bench3Energy4.Visible = true;
                        Bench3Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 3));
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 5)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = true;
                        Bench3Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 1));
                        Bench3Energy3.Visible = true;
                        Bench3Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 2));
                        Bench3Energy4.Visible = true;
                        Bench3Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 3));
                        Bench3Energy5.Visible = true;
                        Bench3Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 4));
                    }

                    PictureBench4.Visible = false;
                    PictureBench5.Visible = false;
                    break;
                case 4:
                    PictureBench1.Visible = true;
                    PictureBench1.Image = Image.FromFile(bench.ShowCard(0));
                    if (bench.LoadedEnergyCount(0) == 0)
                    {
                        Bench1Energy1.Visible = false;
                        Bench1Energy2.Visible = false;
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 1)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = false;
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 2)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 3)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 4)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = true;
                        Bench1Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 3));
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 5)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = true;
                        Bench1Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 3));
                        Bench1Energy5.Visible = true;
                        Bench1Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 4));
                    }

                    PictureBench2.Visible = true;
                    PictureBench2.Image = Image.FromFile(bench.ShowCard(1));
                    if (bench.LoadedEnergyCount(1) == 0)
                    {
                        Bench2Energy1.Visible = false;
                        Bench2Energy2.Visible = false;
                        Bench2Energy3.Visible = false;
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 1)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = false;
                        Bench2Energy3.Visible = false;
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 2)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = false;
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 3)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = true;
                        Bench2Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 2));
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 4)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = true;
                        Bench2Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 2));
                        Bench2Energy4.Visible = true;
                        Bench2Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 3));
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 5)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = true;
                        Bench2Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 2));
                        Bench2Energy4.Visible = true;
                        Bench2Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 3));
                        Bench2Energy5.Visible = true;
                        Bench2Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 4));
                    }
                    PictureBench3.Visible = true;
                    PictureBench3.Image = Image.FromFile(bench.ShowCard(2));
                    if (bench.LoadedEnergyCount(2) == 0)
                    {
                        Bench3Energy1.Visible = false;
                        Bench3Energy2.Visible = false;
                        Bench3Energy3.Visible = false;
                        Bench3Energy4.Visible = false;
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 1)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = false;
                        Bench3Energy3.Visible = false;
                        Bench3Energy4.Visible = false;
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 2)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = true;
                        Bench3Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 1));
                        Bench3Energy3.Visible = false;
                        Bench3Energy4.Visible = false;
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 3)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = true;
                        Bench3Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 1));
                        Bench3Energy3.Visible = true;
                        Bench3Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 2));
                        Bench3Energy4.Visible = false;
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 4)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = true;
                        Bench3Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 1));
                        Bench3Energy3.Visible = true;
                        Bench3Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 2));
                        Bench3Energy4.Visible = true;
                        Bench3Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 3));
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 5)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = true;
                        Bench3Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 1));
                        Bench3Energy3.Visible = true;
                        Bench3Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 2));
                        Bench3Energy4.Visible = true;
                        Bench3Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 3));
                        Bench3Energy5.Visible = true;
                        Bench3Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 4));
                    }

                    PictureBench4.Visible = true;
                    PictureBench4.Image = Image.FromFile(bench.ShowCard(3));
                    if (bench.LoadedEnergyCount(3) == 0)
                    {
                        Bench4Energy1.Visible = false;
                        Bench4Energy2.Visible = false;
                        Bench4Energy3.Visible = false;
                        Bench4Energy4.Visible = false;
                        Bench4Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(3) == 1)
                    {
                        Bench4Energy1.Visible = true;
                        Bench4Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 0));
                        Bench4Energy2.Visible = false;
                        Bench4Energy3.Visible = false;
                        Bench4Energy4.Visible = false;
                        Bench4Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(3) == 2)
                    {
                        Bench4Energy1.Visible = true;
                        Bench4Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 0));
                        Bench4Energy2.Visible = true;
                        Bench4Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 1));
                        Bench4Energy3.Visible = false;
                        Bench4Energy4.Visible = false;
                        Bench4Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(3) == 3)
                    {
                        Bench4Energy1.Visible = true;
                        Bench4Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 0));
                        Bench4Energy2.Visible = true;
                        Bench4Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 1));
                        Bench4Energy3.Visible = true;
                        Bench4Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 2));
                        Bench4Energy4.Visible = false;
                        Bench4Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(3) == 4)
                    {
                        Bench4Energy1.Visible = true;
                        Bench4Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 0));
                        Bench4Energy2.Visible = true;
                        Bench4Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 1));
                        Bench4Energy3.Visible = true;
                        Bench4Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 2));
                        Bench4Energy4.Visible = true;
                        Bench4Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 3));
                        Bench4Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(3) == 5)
                    {
                        Bench4Energy1.Visible = true;
                        Bench4Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 0));
                        Bench4Energy2.Visible = true;
                        Bench4Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 1));
                        Bench4Energy3.Visible = true;
                        Bench4Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 2));
                        Bench4Energy4.Visible = true;
                        Bench4Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 3));
                        Bench4Energy5.Visible = true;
                        Bench4Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 4));
                    }
                    PictureBench5.Visible = false;                  
                    break;
                case 5:
                    PictureBench1.Visible = true;
                    PictureBench1.Image = Image.FromFile(bench.ShowCard(0));
                    if (bench.LoadedEnergyCount(0) == 0)
                    {
                        Bench1Energy1.Visible = false;
                        Bench1Energy2.Visible = false;
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 1)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = false;
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 2)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = false;
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 3)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = false;
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 4)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = true;
                        Bench1Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 3));
                        Bench1Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(0) == 5)
                    {
                        Bench1Energy1.Visible = true;
                        Bench1Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 0));
                        Bench1Energy2.Visible = true;
                        Bench1Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 1));
                        Bench1Energy3.Visible = true;
                        Bench1Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 2));
                        Bench1Energy4.Visible = true;
                        Bench1Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 3));
                        Bench1Energy5.Visible = true;
                        Bench1Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(0, 4));
                    }

                    PictureBench2.Visible = true;
                    PictureBench2.Image = Image.FromFile(bench.ShowCard(1));
                    if (bench.LoadedEnergyCount(1) == 0)
                    {
                        Bench2Energy1.Visible = false;
                        Bench2Energy2.Visible = false;
                        Bench2Energy3.Visible = false;
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 1)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = false;
                        Bench2Energy3.Visible = false;
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 2)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = false;
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 3)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = true;
                        Bench2Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 2));
                        Bench2Energy4.Visible = false;
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 4)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = true;
                        Bench2Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 2));
                        Bench2Energy4.Visible = true;
                        Bench2Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 3));
                        Bench2Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(1) == 5)
                    {
                        Bench2Energy1.Visible = true;
                        Bench2Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 0));
                        Bench2Energy2.Visible = true;
                        Bench2Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 1));
                        Bench2Energy3.Visible = true;
                        Bench2Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 2));
                        Bench2Energy4.Visible = true;
                        Bench2Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 3));
                        Bench2Energy5.Visible = true;
                        Bench2Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(1, 4));
                    }
                    PictureBench3.Visible = true;
                    PictureBench3.Image = Image.FromFile(bench.ShowCard(2));
                    if (bench.LoadedEnergyCount(2) == 0)
                    {
                        Bench3Energy1.Visible = false;
                        Bench3Energy2.Visible = false;
                        Bench3Energy3.Visible = false;
                        Bench3Energy4.Visible = false;
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 1)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = false;
                        Bench3Energy3.Visible = false;
                        Bench3Energy4.Visible = false;
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 2)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = true;
                        Bench3Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 1));
                        Bench3Energy3.Visible = false;
                        Bench3Energy4.Visible = false;
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 3)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = true;
                        Bench3Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 1));
                        Bench3Energy3.Visible = true;
                        Bench3Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 2));
                        Bench3Energy4.Visible = false;
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 4)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = true;
                        Bench3Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 1));
                        Bench3Energy3.Visible = true;
                        Bench3Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 2));
                        Bench3Energy4.Visible = true;
                        Bench3Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 3));
                        Bench3Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(2) == 5)
                    {
                        Bench3Energy1.Visible = true;
                        Bench3Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 0));
                        Bench3Energy2.Visible = true;
                        Bench3Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 1));
                        Bench3Energy3.Visible = true;
                        Bench3Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 2));
                        Bench3Energy4.Visible = true;
                        Bench3Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 3));
                        Bench3Energy5.Visible = true;
                        Bench3Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(2, 4));
                    }

                    PictureBench4.Visible = true;
                    PictureBench4.Image = Image.FromFile(bench.ShowCard(3));
                    if (bench.LoadedEnergyCount(3) == 0)
                    {
                        Bench4Energy1.Visible = false;
                        Bench4Energy2.Visible = false;
                        Bench4Energy3.Visible = false;
                        Bench4Energy4.Visible = false;
                        Bench4Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(3) == 1)
                    {
                        Bench4Energy1.Visible = true;
                        Bench4Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 0));
                        Bench4Energy2.Visible = false;
                        Bench4Energy3.Visible = false;
                        Bench4Energy4.Visible = false;
                        Bench4Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(3) == 2)
                    {
                        Bench4Energy1.Visible = true;
                        Bench4Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 0));
                        Bench4Energy2.Visible = true;
                        Bench4Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 1));
                        Bench4Energy3.Visible = false;
                        Bench4Energy4.Visible = false;
                        Bench4Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(3) == 3)
                    {
                        Bench4Energy1.Visible = true;
                        Bench4Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 0));
                        Bench4Energy2.Visible = true;
                        Bench4Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 1));
                        Bench4Energy3.Visible = true;
                        Bench4Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 2));
                        Bench4Energy4.Visible = false;
                        Bench4Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(3) == 4)
                    {
                        Bench4Energy1.Visible = true;
                        Bench4Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 0));
                        Bench4Energy2.Visible = true;
                        Bench4Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 1));
                        Bench4Energy3.Visible = true;
                        Bench4Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 2));
                        Bench4Energy4.Visible = true;
                        Bench4Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 3));
                        Bench4Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(3) == 5)
                    {
                        Bench4Energy1.Visible = true;
                        Bench4Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 0));
                        Bench4Energy2.Visible = true;
                        Bench4Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 1));
                        Bench4Energy3.Visible = true;
                        Bench4Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 2));
                        Bench4Energy4.Visible = true;
                        Bench4Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 3));
                        Bench4Energy5.Visible = true;
                        Bench4Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(3, 4));
                    }
                    PictureBench5.Visible = true;
                    PictureBench5.Image = Image.FromFile(bench.ShowCard(4));
                    if (bench.LoadedEnergyCount(4) == 0)
                    {
                        Bench5Energy1.Visible = false;
                        Bench5Energy2.Visible = false;
                        Bench5Energy3.Visible = false;
                        Bench5Energy4.Visible = false;
                        Bench5Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(4) == 1)
                    {
                        Bench5Energy1.Visible = true;
                        Bench5Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 0));
                        Bench5Energy2.Visible = false;
                        Bench5Energy3.Visible = false;
                        Bench5Energy4.Visible = false;
                        Bench5Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(4) == 2)
                    {
                        Bench5Energy1.Visible = true;
                        Bench5Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 0));
                        Bench5Energy2.Visible = true;
                        Bench5Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 1));
                        Bench5Energy3.Visible = false;
                        Bench5Energy4.Visible = false;
                        Bench5Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(4) == 3)
                    {
                        Bench5Energy1.Visible = true;
                        Bench5Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 0));
                        Bench5Energy2.Visible = true;
                        Bench5Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 1));
                        Bench5Energy3.Visible = true;
                        Bench5Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 2));
                        Bench5Energy4.Visible = false;
                        Bench5Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(4) == 4)
                    {
                        Bench5Energy1.Visible = true;
                        Bench5Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 0));
                        Bench5Energy2.Visible = true;
                        Bench5Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 1));
                        Bench5Energy3.Visible = true;
                        Bench5Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 2));
                        Bench5Energy4.Visible = true;
                        Bench5Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 3));
                        Bench5Energy5.Visible = false;
                    }
                    else if (bench.LoadedEnergyCount(4) == 5)
                    {
                        Bench5Energy1.Visible = true;
                        Bench5Energy1.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 0));
                        Bench5Energy2.Visible = true;
                        Bench5Energy2.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 1));
                        Bench5Energy3.Visible = true;
                        Bench5Energy3.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 2));
                        Bench5Energy4.Visible = true;
                        Bench5Energy4.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 3));
                        Bench5Energy5.Visible = true;
                        Bench5Energy5.Image = Image.FromFile(bench.ReturnEnergyLoadedImg(4, 4));
                    }
                    break;
            }
        }

        public void CreateDiscardView()
        {
            int discard_size = discard.TotalNumber();

            Hand1.Visible = false;
            Hand2.Visible = false;
            Hand3.Visible = false;
            Hand4.Visible = false;
            Hand5.Visible = false;
            Hand6.Visible = false;
            Hand7.Visible = false;
            Hand8.Visible = false;
            Hand9.Visible = false;
            Hand10.Visible = false;
            switch (discard_size)
            {
                case 0:
                    break;

                case 1:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(discard.ShowCard(0));

                    break;

                case 2:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(discard.ShowCard(0));
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(discard.ShowCard(1));

                    break;
                case 3:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(discard.ShowCard(0));
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(discard.ShowCard(1));
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(discard.ShowCard(2));

                    break;
                case 4:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(discard.ShowCard(0));
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(discard.ShowCard(1));
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(discard.ShowCard(2));
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(discard.ShowCard(3));

                    break;
                case 5:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(discard.ShowCard(0));
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(discard.ShowCard(1));
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(discard.ShowCard(2));
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(discard.ShowCard(3));
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(discard.ShowCard(4));
                    break;

                case 6:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(discard.ShowCard(0));
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(discard.ShowCard(1));
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(discard.ShowCard(2));
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(discard.ShowCard(3));
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(discard.ShowCard(4));
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(discard.ShowCard(5));
                    break;

                case 7:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(discard.ShowCard(0));
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(discard.ShowCard(1));
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(discard.ShowCard(2));
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(discard.ShowCard(3));
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(discard.ShowCard(4));
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(discard.ShowCard(5));
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(discard.ShowCard(6));
                    break;

                case 8:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(discard.ShowCard(0));
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(discard.ShowCard(1));
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(discard.ShowCard(2));
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(discard.ShowCard(3));
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(discard.ShowCard(4));
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(discard.ShowCard(5));
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(discard.ShowCard(6));
                    Hand8.Visible = true;
                    Hand8.Image = Image.FromFile(discard.ShowCard(7));
                    break;

                case 9:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(discard.ShowCard(0));
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(discard.ShowCard(1));
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(discard.ShowCard(2));
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(discard.ShowCard(3));
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(discard.ShowCard(4));
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(discard.ShowCard(5));
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(discard.ShowCard(6));
                    Hand8.Visible = true;
                    Hand8.Image = Image.FromFile(discard.ShowCard(7));
                    Hand9.Visible = true;
                    Hand9.Image = Image.FromFile(discard.ShowCard(8));
                    break;

                case 10:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(discard.ShowCard(0));
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(discard.ShowCard(1));
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(discard.ShowCard(2));
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(discard.ShowCard(3));
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(discard.ShowCard(4));
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(discard.ShowCard(5));
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(discard.ShowCard(6));
                    Hand8.Visible = true;
                    Hand8.Image = Image.FromFile(discard.ShowCard(7));
                    Hand9.Visible = true;
                    Hand9.Image = Image.FromFile(discard.ShowCard(8));
                    Hand10.Visible = true;
                    Hand10.Image = Image.FromFile(discard.ShowCard(9));
                    break;
            }
        }
        public void AIUpdateBenchView()
        {
            int bench_size = aibench.NumberOfCards();

            OpponentBench1.Visible = false;
            OBench1Energy1.Visible = false;
            OBench1Energy2.Visible = false;
            OBench1Energy3.Visible = false;
            OBench1Energy4.Visible = false;
            OBench1Energy5.Visible = false;

            OpponentBench2.Visible = false;
            OBench2Energy1.Visible = false;
            OBench2Energy2.Visible = false;
            OBench2Energy3.Visible = false;
            OBench2Energy4.Visible = false;
            OBench2Energy5.Visible = false;

            OpponentBench3.Visible = false;
            OBench3Energy1.Visible = false;
            OBench3Energy2.Visible = false;
            OBench3Energy3.Visible = false;
            OBench3Energy4.Visible = false;
            OBench3Energy5.Visible = false;

            OpponentBench4.Visible = false;
            OBench4Energy1.Visible = false;
            OBench4Energy2.Visible = false;
            OBench4Energy3.Visible = false;
            OBench4Energy4.Visible = false;
            OBench4Energy5.Visible = false;

            OpponentBench5.Visible = false;
            OBench5Energy1.Visible = false;
            OBench5Energy2.Visible = false;
            OBench5Energy3.Visible = false;
            OBench5Energy4.Visible = false;
            OBench5Energy5.Visible = false;

            switch (bench_size)
            {
                case 0:

                    break;
                case 1:
                    OpponentBench1.Visible = true;
                    OpponentBench1.Image = Image.FromFile(aibench.ShowCard(0));
                    if (aibench.LoadedEnergyCount(0) == 0)
                    {
                        OBench1Energy1.Visible = false;
                        OBench1Energy2.Visible = false;
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 1)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = false;
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 2)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 3)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 4)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = true;
                        OBench1Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 3));
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 5)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = true;
                        OBench1Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 3));
                        OBench1Energy5.Visible = true;
                        OBench1Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 4));
                    }

                    OpponentBench2.Visible = false;
                    OpponentBench3.Visible = false;
                    OpponentBench4.Visible = false;
                    OpponentBench5.Visible = false;
                    break;
                case 2:
                    OpponentBench1.Visible = true;
                    OpponentBench1.Image = Image.FromFile(aibench.ShowCard(0));
                    if (aibench.LoadedEnergyCount(0) == 0)
                    {
                        OBench1Energy1.Visible = false;
                        OBench1Energy2.Visible = false;
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 1)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = false;
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 2)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 3)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 4)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = true;
                        OBench1Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 3));
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 5)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = true;
                        OBench1Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 3));
                        OBench1Energy5.Visible = true;
                        OBench1Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 4));
                    }

                    OpponentBench2.Visible = true;
                    OpponentBench2.Image = Image.FromFile(aibench.ShowCard(1));

                    if (aibench.LoadedEnergyCount(1) == 0)
                    {
                        OBench2Energy1.Visible = false;
                        OBench2Energy2.Visible = false;
                        OBench2Energy3.Visible = false;
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 1)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = false;
                        OBench2Energy3.Visible = false;
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 2)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = false;
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 3)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = true;
                        OBench2Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 2));
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 4)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = true;
                        OBench2Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 2));
                        OBench2Energy4.Visible = true;
                        OBench2Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 3));
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 5)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = true;
                        OBench2Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 2));
                        OBench2Energy4.Visible = true;
                        OBench2Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 3));
                        OBench2Energy5.Visible = true;
                        OBench2Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 4));
                    }

                    OpponentBench3.Visible = false;
                    OpponentBench4.Visible = false;
                    OpponentBench5.Visible = false;
                    break;
                case 3:
                    OpponentBench1.Visible = true;
                    OpponentBench1.Image = Image.FromFile(aibench.ShowCard(0));
                    if (aibench.LoadedEnergyCount(0) == 0)
                    {
                        OBench1Energy1.Visible = false;
                        OBench1Energy2.Visible = false;
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 1)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = false;
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 2)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 3)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 4)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = true;
                        OBench1Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 3));
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 5)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = true;
                        OBench1Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 3));
                        OBench1Energy5.Visible = true;
                        OBench1Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 4));
                    }

                    OpponentBench2.Visible = true;
                    OpponentBench2.Image = Image.FromFile(aibench.ShowCard(1));

                    if (aibench.LoadedEnergyCount(1) == 0)
                    {
                        OBench2Energy1.Visible = false;
                        OBench2Energy2.Visible = false;
                        OBench2Energy3.Visible = false;
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 1)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = false;
                        OBench2Energy3.Visible = false;
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 2)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = false;
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 3)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = true;
                        OBench2Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 2));
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 4)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = true;
                        OBench2Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 2));
                        OBench2Energy4.Visible = true;
                        OBench2Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 3));
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 5)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = true;
                        OBench2Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 2));
                        OBench2Energy4.Visible = true;
                        OBench2Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 3));
                        OBench2Energy5.Visible = true;
                        OBench2Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 4));
                    }

                    OpponentBench3.Visible = true;
                    OpponentBench3.Image = Image.FromFile(aibench.ShowCard(2));
                    if (aibench.LoadedEnergyCount(2) == 0)
                    {
                        OBench3Energy1.Visible = false;
                        OBench3Energy2.Visible = false;
                        OBench3Energy3.Visible = false;
                        OBench3Energy4.Visible = false;
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 1)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = false;
                        OBench3Energy3.Visible = false;
                        OBench3Energy4.Visible = false;
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 2)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = true;
                        OBench3Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 1));
                        OBench3Energy3.Visible = false;
                        OBench3Energy4.Visible = false;
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 3)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = true;
                        OBench3Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 1));
                        OBench3Energy3.Visible = true;
                        OBench3Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 2));
                        OBench3Energy4.Visible = false;
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 4)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = true;
                        OBench3Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 1));
                        OBench3Energy3.Visible = true;
                        OBench3Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 2));
                        OBench3Energy4.Visible = true;
                        OBench3Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 3));
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 5)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = true;
                        OBench3Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 1));
                        OBench3Energy3.Visible = true;
                        OBench3Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 2));
                        OBench3Energy4.Visible = true;
                        OBench3Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 3));
                        OBench3Energy5.Visible = true;
                        OBench3Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 4));
                    }

                    OpponentBench4.Visible = false;
                    OpponentBench5.Visible = false;
                    break;
                case 4:
                    OpponentBench1.Visible = true;
                    OpponentBench1.Image = Image.FromFile(aibench.ShowCard(0));
                    if (aibench.LoadedEnergyCount(0) == 0)
                    {
                        OBench1Energy1.Visible = false;
                        OBench1Energy2.Visible = false;
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 1)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = false;
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 2)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 3)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 4)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = true;
                        OBench1Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 3));
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 5)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = true;
                        OBench1Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 3));
                        OBench1Energy5.Visible = true;
                        OBench1Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 4));
                    }

                    OpponentBench2.Visible = true;
                    OpponentBench2.Image = Image.FromFile(aibench.ShowCard(1));

                    if (aibench.LoadedEnergyCount(1) == 0)
                    {
                        OBench2Energy1.Visible = false;
                        OBench2Energy2.Visible = false;
                        OBench2Energy3.Visible = false;
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 1)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = false;
                        OBench2Energy3.Visible = false;
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 2)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = false;
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 3)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = true;
                        OBench2Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 2));
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 4)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = true;
                        OBench2Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 2));
                        OBench2Energy4.Visible = true;
                        OBench2Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 3));
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 5)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = true;
                        OBench2Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 2));
                        OBench2Energy4.Visible = true;
                        OBench2Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 3));
                        OBench2Energy5.Visible = true;
                        OBench2Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 4));
                    }

                    OpponentBench3.Visible = true;
                    OpponentBench3.Image = Image.FromFile(aibench.ShowCard(2));
                    if (aibench.LoadedEnergyCount(2) == 0)
                    {
                        OBench3Energy1.Visible = false;
                        OBench3Energy2.Visible = false;
                        OBench3Energy3.Visible = false;
                        OBench3Energy4.Visible = false;
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 1)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = false;
                        OBench3Energy3.Visible = false;
                        OBench3Energy4.Visible = false;
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 2)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = true;
                        OBench3Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 1));
                        OBench3Energy3.Visible = false;
                        OBench3Energy4.Visible = false;
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 3)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = true;
                        OBench3Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 1));
                        OBench3Energy3.Visible = true;
                        OBench3Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 2));
                        OBench3Energy4.Visible = false;
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 4)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = true;
                        OBench3Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 1));
                        OBench3Energy3.Visible = true;
                        OBench3Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 2));
                        OBench3Energy4.Visible = true;
                        OBench3Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 3));
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 5)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = true;
                        OBench3Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 1));
                        OBench3Energy3.Visible = true;
                        OBench3Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 2));
                        OBench3Energy4.Visible = true;
                        OBench3Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 3));
                        OBench3Energy5.Visible = true;
                        OBench3Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 4));
                    }

                    OpponentBench4.Visible = true;
                    OpponentBench4.Image = Image.FromFile(aibench.ShowCard(3));
                    if (aibench.LoadedEnergyCount(3) == 0)
                    {
                        OBench4Energy1.Visible = false;
                        OBench4Energy2.Visible = false;
                        OBench4Energy3.Visible = false;
                        OBench4Energy4.Visible = false;
                        OBench4Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(3) == 1)
                    {
                        OBench4Energy1.Visible = true;
                        OBench4Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 0));
                        OBench4Energy2.Visible = false;
                        OBench4Energy3.Visible = false;
                        OBench4Energy4.Visible = false;
                        OBench4Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(3) == 2)
                    {
                        OBench4Energy1.Visible = true;
                        OBench4Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 0));
                        OBench4Energy2.Visible = true;
                        OBench4Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 1));
                        OBench4Energy3.Visible = false;
                        OBench4Energy4.Visible = false;
                        OBench4Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(3) == 3)
                    {
                        OBench4Energy1.Visible = true;
                        OBench4Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 0));
                        OBench4Energy2.Visible = true;
                        OBench4Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 1));
                        OBench4Energy3.Visible = true;
                        OBench4Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 2));
                        OBench4Energy4.Visible = false;
                        OBench4Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(3) == 4)
                    {
                        OBench4Energy1.Visible = true;
                        OBench4Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 0));
                        OBench4Energy2.Visible = true;
                        OBench4Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 1));
                        OBench4Energy3.Visible = true;
                        OBench4Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 2));
                        OBench4Energy4.Visible = true;
                        OBench4Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 3));
                        OBench4Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(3) == 5)
                    {
                        OBench4Energy1.Visible = true;
                        OBench4Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 0));
                        OBench4Energy2.Visible = true;
                        OBench4Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 1));
                        OBench4Energy3.Visible = true;
                        OBench4Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 2));
                        OBench4Energy4.Visible = true;
                        OBench4Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 3));
                        OBench4Energy5.Visible = true;
                        OBench4Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 4));
                    }
                    OpponentBench5.Visible = false;
                    break;
                case 5:
                    OpponentBench1.Visible = true;
                    OpponentBench1.Image = Image.FromFile(aibench.ShowCard(0));
                    if (aibench.LoadedEnergyCount(0) == 0)
                    {
                        OBench1Energy1.Visible = false;
                        OBench1Energy2.Visible = false;
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 1)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = false;
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 2)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = false;
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 3)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = false;
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 4)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = true;
                        OBench1Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 3));
                        OBench1Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(0) == 5)
                    {
                        OBench1Energy1.Visible = true;
                        OBench1Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 0));
                        OBench1Energy2.Visible = true;
                        OBench1Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 1));
                        OBench1Energy3.Visible = true;
                        OBench1Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 2));
                        OBench1Energy4.Visible = true;
                        OBench1Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 3));
                        OBench1Energy5.Visible = true;
                        OBench1Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(0, 4));
                    }

                    OpponentBench2.Visible = true;
                    OpponentBench2.Image = Image.FromFile(aibench.ShowCard(1));

                    if (aibench.LoadedEnergyCount(1) == 0)
                    {
                        OBench2Energy1.Visible = false;
                        OBench2Energy2.Visible = false;
                        OBench2Energy3.Visible = false;
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 1)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = false;
                        OBench2Energy3.Visible = false;
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 2)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = false;
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 3)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = true;
                        OBench2Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 2));
                        OBench2Energy4.Visible = false;
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 4)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = true;
                        OBench2Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 2));
                        OBench2Energy4.Visible = true;
                        OBench2Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 3));
                        OBench2Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(1) == 5)
                    {
                        OBench2Energy1.Visible = true;
                        OBench2Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 0));
                        OBench2Energy2.Visible = true;
                        OBench2Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 1));
                        OBench2Energy3.Visible = true;
                        OBench2Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 2));
                        OBench2Energy4.Visible = true;
                        OBench2Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 3));
                        OBench2Energy5.Visible = true;
                        OBench2Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(1, 4));
                    }

                    OpponentBench3.Visible = true;
                    OpponentBench3.Image = Image.FromFile(aibench.ShowCard(2));
                    if (aibench.LoadedEnergyCount(2) == 0)
                    {
                        OBench3Energy1.Visible = false;
                        OBench3Energy2.Visible = false;
                        OBench3Energy3.Visible = false;
                        OBench3Energy4.Visible = false;
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 1)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = false;
                        OBench3Energy3.Visible = false;
                        OBench3Energy4.Visible = false;
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 2)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = true;
                        OBench3Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 1));
                        OBench3Energy3.Visible = false;
                        OBench3Energy4.Visible = false;
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 3)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = true;
                        OBench3Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 1));
                        OBench3Energy3.Visible = true;
                        OBench3Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 2));
                        OBench3Energy4.Visible = false;
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 4)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = true;
                        OBench3Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 1));
                        OBench3Energy3.Visible = true;
                        OBench3Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 2));
                        OBench3Energy4.Visible = true;
                        OBench3Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 3));
                        OBench3Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(2) == 5)
                    {
                        OBench3Energy1.Visible = true;
                        OBench3Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 0));
                        OBench3Energy2.Visible = true;
                        OBench3Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 1));
                        OBench3Energy3.Visible = true;
                        OBench3Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 2));
                        OBench3Energy4.Visible = true;
                        OBench3Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 3));
                        OBench3Energy5.Visible = true;
                        OBench3Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(2, 4));
                    }

                    OpponentBench4.Visible = true;
                    OpponentBench4.Image = Image.FromFile(aibench.ShowCard(3));
                    if (aibench.LoadedEnergyCount(3) == 0)
                    {
                        OBench4Energy1.Visible = false;
                        OBench4Energy2.Visible = false;
                        OBench4Energy3.Visible = false;
                        OBench4Energy4.Visible = false;
                        OBench4Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(3) == 1)
                    {
                        OBench4Energy1.Visible = true;
                        OBench4Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 0));
                        OBench4Energy2.Visible = false;
                        OBench4Energy3.Visible = false;
                        OBench4Energy4.Visible = false;
                        OBench4Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(3) == 2)
                    {
                        OBench4Energy1.Visible = true;
                        OBench4Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 0));
                        OBench4Energy2.Visible = true;
                        OBench4Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 1));
                        OBench4Energy3.Visible = false;
                        OBench4Energy4.Visible = false;
                        OBench4Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(3) == 3)
                    {
                        OBench4Energy1.Visible = true;
                        OBench4Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 0));
                        OBench4Energy2.Visible = true;
                        OBench4Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 1));
                        OBench4Energy3.Visible = true;
                        OBench4Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 2));
                        OBench4Energy4.Visible = false;
                        OBench4Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(3) == 4)
                    {
                        OBench4Energy1.Visible = true;
                        OBench4Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 0));
                        OBench4Energy2.Visible = true;
                        OBench4Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 1));
                        OBench4Energy3.Visible = true;
                        OBench4Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 2));
                        OBench4Energy4.Visible = true;
                        OBench4Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 3));
                        OBench4Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(3) == 5)
                    {
                        OBench4Energy1.Visible = true;
                        OBench4Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 0));
                        OBench4Energy2.Visible = true;
                        OBench4Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 1));
                        OBench4Energy3.Visible = true;
                        OBench4Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 2));
                        OBench4Energy4.Visible = true;
                        OBench4Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 3));
                        OBench4Energy5.Visible = true;
                        OBench4Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(3, 4));
                    }
                    OpponentBench5.Visible = true;
                    OpponentBench5.Image = Image.FromFile(aibench.ShowCard(4));
                    if (aibench.LoadedEnergyCount(4) == 0)
                    {
                        OBench5Energy1.Visible = false;
                        OBench5Energy2.Visible = false;
                        OBench5Energy3.Visible = false;
                        OBench5Energy4.Visible = false;
                        OBench5Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(4) == 1)
                    {
                        OBench5Energy1.Visible = true;
                        OBench5Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 0));
                        OBench5Energy2.Visible = false;
                        OBench5Energy3.Visible = false;
                        OBench5Energy4.Visible = false;
                        OBench5Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(4) == 2)
                    {
                        OBench5Energy1.Visible = true;
                        OBench5Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 0));
                        OBench5Energy2.Visible = true;
                        OBench5Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 1));
                        OBench5Energy3.Visible = false;
                        OBench5Energy4.Visible = false;
                        OBench5Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(4) == 3)
                    {
                        OBench5Energy1.Visible = true;
                        OBench5Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 0));
                        OBench5Energy2.Visible = true;
                        OBench5Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 1));
                        OBench5Energy3.Visible = true;
                        OBench5Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 2));
                        OBench5Energy4.Visible = false;
                        OBench5Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(4) == 4)
                    {
                        OBench5Energy1.Visible = true;
                        OBench5Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 0));
                        OBench5Energy2.Visible = true;
                        OBench5Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 1));
                        OBench5Energy3.Visible = true;
                        OBench5Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 2));
                        OBench5Energy4.Visible = true;
                        OBench5Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 3));
                        OBench5Energy5.Visible = false;
                    }
                    else if (aibench.LoadedEnergyCount(4) == 5)
                    {
                        OBench5Energy1.Visible = true;
                        OBench5Energy1.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 0));
                        OBench5Energy2.Visible = true;
                        OBench5Energy2.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 1));
                        OBench5Energy3.Visible = true;
                        OBench5Energy3.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 2));
                        OBench5Energy4.Visible = true;
                        OBench5Energy4.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 3));
                        OBench5Energy5.Visible = true;
                        OBench5Energy5.Image = Image.FromFile(aibench.ReturnEnergyLoadedImg(4, 4));
                    }
                    break;
            }
        }

        public void UpdateDiscardView()
        {
            if(discard.TotalNumber() == 0)
            {
                DiscardBox.Visible = false;
            }
            else
            {              
                DiscardBox.Image = Image.FromFile(discard.ShowTopImage());
                DiscardBox.Visible = true;
            }           
        }

        public void UpdateAIDiscardView()
        {
            if (ai_discard.TotalNumber() == 0)
            {
                OpponentDiscardBox.Visible = false;
            }
            else
            {
                OpponentDiscardBox.Image = Image.FromFile(ai_discard.ShowTopImage());
                OpponentDiscardBox.Visible = true;
            }
        }

        public void ZoomInfo(int num)
        {
            if (player_Hand.ShowType(num) == "trainer" || player_Hand.ShowType(num) == "energy" || player_Hand.ShowType(num) == "supporter")
            {
                CardName.Text = player_Hand.ShowName(num);
                PictureZoom.Image = Image.FromFile(player_Hand.ShowCard(num));

                HpLabel1.Visible = false;
                MaxHp.Visible = false;
                RemHp.Visible = false;
                PlayerHpBar.Visible = false;
                EnergyBox1.Visible = false;
                CardName.Visible = true;
            }
            else
            {
                CardName.Text = player_Hand.ShowName(num);
                PlayerHpBar.Maximum = player_Hand.ShowHP(num);
                PlayerHpBar.Value = player_Hand.ShowRemHp(num);
                MaxHp.Text = player_Hand.ShowHP(num).ToString();
                RemHp.Text = player_Hand.ShowRemHp(num).ToString();
                PictureZoom.Image = Image.FromFile(player_Hand.ShowCard(num));

                CardName.Visible = true;
                HpLabel1.Visible = true;
                MaxHp.Visible = true;
                RemHp.Visible = true;               
                PlayerHpBar.Visible = true;              
                EnergyBox1.Visible = true;
                CheckEnergy(num);
            }
        }

        public void ZoomBenchInfo(int num)
        {
            CardName.Text = bench.ShowName(num);
            PlayerHpBar.Maximum = bench.ShowHp(num);
            PlayerHpBar.Value = bench.ShowRemHp(num);
            MaxHp.Text = bench.ShowHp(num).ToString();
            RemHp.Text = bench.ShowRemHp(num).ToString();
            PictureZoom.Image = Image.FromFile(bench.ShowCard(num));

            CardName.Visible = true;
            HpLabel1.Visible = true;
            MaxHp.Visible = true;
            RemHp.Visible = true;
            PlayerHpBar.Visible = true;
            EnergyBox1.Visible = true;

            switch (bench.ShowEnergy(num))
                {
                    case 'f':
                        EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fire.gif");
                        break;
                    case 'w':
                        EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Water.gif");
                        break;
                    case 'g':
                        EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Grass.gif");
                        break;
                    case 'p':
                        EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Psychic.gif");
                        break;
                    case 'l':
                        EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fighting.gif");
                        break;
                    case 'e':
                        EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Lightning.gif");
                        break;
                    case 'c':
                        EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Colorless.gif");
                        break;
                    default:
                        EnergyBox1.Visible = false;
                        break;
                }
            
        }

        public void CheckEnergy(int num)
        {
            switch (player_Hand.ShowEnergy(num))
            {
                case 'f':
                    EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fire.gif");
                    break;
                case 'w':
                    EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Water.gif");
                    break;
                case 'g':
                    EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Grass.gif");
                    break;
                case 'p':
                    EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Psychic.gif");
                    break;
                case 'l':
                    EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fighting.gif");
                    break;
                case 'e':
                    EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Lightning.gif");
                    break;
                case 'c':
                    EnergyBox1.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Colorless.gif");
                    break;
                default:
                    EnergyBox1.Visible = false;
                    break;
            }
        }

        private void Play_Click(object sender, EventArgs e)
        {
            if(isPreGameTurn == true)
            {
                int num = GetHandBoxIndex();
                if (player_Hand.ShowType(num) == "basic")
                {
                    if (bench.NumberOfCards() < 5)
                    {
                        gameMessage.Text = "You played " + player_Hand.ShowName(num) + " from your hand to your bench";
                        bench.Add(player_Hand.PlayCard(num));
                        player_Hand.RemoveFromHand(num);
                        UpdateBenchView();
                        UpdateHandView();
                        AIPlaysCards();
                    }
                    else
                    {
                        gameMessage.Text = "Your bench is full!";
                    }
                }
                else if (player_Hand.ShowType(num) == "energy")
                {
                }
                else if (player_Hand.ShowType(num) == "trainer")
                {
                    gameMessage.Text = "You cannot play trainers at this stage.";
                }
                else if (player_Hand.ShowType(num) == "second")
                {
                    if (bench.NumberOfCards() == 0)
                    {
                        gameMessage.Text = "You cannot evolve at this stage.";
                    }
                    else
                    {
                        gameMessage.Text = "You cannot evolve at this stage.";
                    }
                }
            }

            else if (isyourFirstTurn == true)
            {
                int num = GetHandBoxIndex();

                if (player_Hand.ShowType(num) == "basic")
                {
                    if (bench.NumberOfCards() < 5)
                    {
                        gameMessage.Text = "You played " + player_Hand.ShowName(num) + " from your hand to your bench";
                        bench.Add(player_Hand.PlayCard(num));
                        player_Hand.RemoveFromHand(num);
                        UpdateBenchView();
                        UpdateHandView();
                        AIPlaysCards();
                    }

                    else
                    {
                        gameMessage.Text = "Your bench is full!";
                    }
                }

                else if (player_Hand.ShowType(num) == "energy")
                {
                }
                else if (player_Hand.ShowType(num) == "trainer")
                {
                    gameMessage.Text = "You cannot play trainers during your first turn.";
                }
                else if (player_Hand.ShowType(num) == "second")
                {
                    if (bench.NumberOfCards() == 0)
                    {
                        gameMessage.Text = "You cannot evolve your Pokémon during your first turn.";
                    }
                    else
                    {
                        gameMessage.Text = "You cannot evolve your Pokémon during your first turn.";
                    }
                }
            }

            else
            {
                int num = GetHandBoxIndex();
                if (player_Hand.ShowType(num) == "basic")
                {

                    if (bench.NumberOfCards() < 5)
                    {
                        gameMessage.Text = "You played " + player_Hand.ShowName(num) + " from your hand to your bench";
                        bench.Add(player_Hand.PlayCard(num));
                        player_Hand.RemoveFromHand(num);
                        UpdateBenchView();
                        UpdateHandView();
                    }

                    else
                    {
                        gameMessage.Text = "Your bench is full!";
                    }
                }
                else if (player_Hand.ShowType(num) == "energy")
                {
                }
                else if (player_Hand.ShowType(num) == "trainer" && player_Hand.ShowImpact(num) == "global")
                {
                    PlayingTrainerCardGlobal(num);
                }
                else if (player_Hand.ShowType(num) == "second")
                {
                    if (bench.NumberOfCards() == 0)
                    {
                        gameMessage.Text = "You do not have any Basic Pokémon on your bench to evolve from.";
                    }
                    else
                    {
                        gameMessage.Text = "Please choose instead the Pokémon to evolve from in the menu.";
                    }
                }
            }           

        }

        private void AIPlaysCards()
        {
            if(isPreGameTurn == true)
            {
                while(ai_Hand.NumOfBasicPokemon() >0 && aibench.NumberOfCards() < 6)
                {
                    for(int i = 0; i < ai_Hand.NumberOfCards(); i++)
                    {
                        if(ai_Hand.ShowType(i) == "basic")
                        {
                            aibench.Add(ai_Hand.PlayCard(i));
                            ai_Hand.RemoveFromHand(i);
                            AIUpdateBenchView();
                            OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
                        }
                    }
                    
                }
            }
            else if(aiStartsTurn == true)
            {
                if (ai_Hand.NumOfBasicPokemon() == 0)
                {
                    gameMessage.Text = "Your opponent is thinking.. ";
                    timer1.Start();

                }
                while (ai_Hand.NumOfBasicPokemon() > 0 && aibench.NumberOfCards() < 6)
                {
                    for (int i = 0; i < ai_Hand.NumberOfCards(); i++)
                    {
                        if (ai_Hand.ShowType(i) == "basic")
                        {
                            gameMessage.Text = "Your opponent plays " + ai_Hand.ShowName(i).ToLower() + " from his hand to his bench.";
                            aibench.Add(ai_Hand.PlayCard(i));
                            ai_Hand.RemoveFromHand(i);
                            AIUpdateBenchView();
                            timer1.Start();
                            OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
                        }
                    }

                }
            }
            else
            {
                for (int i = 0; i < ai_Hand.NumberOfCards(); i++)
                {
                    if (ai_Hand.ShowType(i) == "basic")
                    {
                        gameMessage.Text = "Your opponent plays " + ai_Hand.ShowName(i).ToLower() + " from his hand to his bench.";
                        aibench.Add(ai_Hand.PlayCard(i));
                        ai_Hand.RemoveFromHand(i);
                        AIUpdateBenchView();
                        OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
                        timer1.Start();
                    }
                }
            }
        }

        private void AIPlaysEnergyCards()
        {
            if(ai_Active_Pokemon.ShowName() == "Pikachu")
            {
                int lightning = 0;
                int colorless = 0;

                for (var i = 0; i < ai_Active_Pokemon.EnergyLoadedCount(); ++i)
                {
                    if (ai_Active_Pokemon.GetEnergyLoadedAt(i) == 'e')
                        lightning++;
                    else
                        colorless++;
                };

                if(lightning < 1)
                {
                    for (int l = 0; l < ai_Hand.NumberOfCards(); l++)
                    {
                        if(ai_Hand.ShowName(l) == "Lightning Energy")
                        {
                            AILoadEnergyBasedOnIndexFound(l);
                            break;
                        }
                    }
                }
                else if(colorless < 1 && lightning != 2)
                {
                    for (int c = 0; c <  ai_Hand.NumberOfCards(); c++)
                    {
                        if (ai_Hand.ShowType(c) == "energy")
                        {
                            AILoadEnergyBasedOnIndexFound(c);
                            break;
                        }
                    }
                }
                else
                {
                    AILoadEnergyToBenchedPokemon();
                }
            }
            else if(ai_Active_Pokemon.ShowName() == "Magnemite")
            {
                int lightning = 0;
                int colorless = 0;

                for (var i = 0; i < ai_Active_Pokemon.EnergyLoadedCount(); ++i)
                {
                    if (ai_Active_Pokemon.GetEnergyLoadedAt(i) == 'e')
                        lightning++;
                    else
                        colorless++;
                };

                if (lightning < 1)
                {
                    for (int l = 0; l < ai_Hand.NumberOfCards(); l++)
                    {
                        if (ai_Hand.ShowName(l) == "Lightning Energy")
                        {
                            AILoadEnergyBasedOnIndexFound(l);
                            break;
                        }
                    }
                }
                else if (colorless < 1 && lightning != 2)
                {
                    for (int c = 0; c < ai_Hand.NumberOfCards(); c++)
                    {
                        if (ai_Hand.ShowType(c) == "energy")
                        {
                            AILoadEnergyBasedOnIndexFound(c);
                            break;
                        }
                    }
                }
                else
                {
                    AILoadEnergyToBenchedPokemon();
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Abra")
            {
                int psychic = 0;
                int colorless = 0;

                for (var i = 0; i < ai_Active_Pokemon.EnergyLoadedCount(); ++i)
                {
                    if (ai_Active_Pokemon.GetEnergyLoadedAt(i) == 'p')
                        psychic++;
                    else
                        colorless++;
                }
                if (psychic < 1)
                {
                    for (int p = 0; p < ai_Hand.NumberOfCards(); p++)
                    {
                        if (ai_Hand.ShowName(p) == "Psychic Energy")
                        {
                            AILoadEnergyBasedOnIndexFound(p);
                            break;
                        }
                    }
                }
                else
                {
                    AILoadEnergyToBenchedPokemon();
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Kadabra")
            {
                int psychic = 0;
                int colorless = 0;

                for (var i = 0; i < ai_Active_Pokemon.EnergyLoadedCount(); ++i)
                {
                    if (ai_Active_Pokemon.GetEnergyLoadedAt(i) == 'p')
                        psychic++;
                    else
                        colorless++;
                }
                if (psychic < 2)
                {
                    for (int p = 0; p < ai_Hand.NumberOfCards(); p++)
                    {
                        if (ai_Hand.ShowName(p) == "Psychic Energy")
                        {
                            AILoadEnergyBasedOnIndexFound(p);
                            break;
                        }
                    }
                }
                else if (colorless < 1 && psychic != 3)
                {
                    for (int c = 0; c < ai_Hand.NumberOfCards(); c++)
                    {
                        if (ai_Hand.ShowType(c) == "energy")
                        {
                            AILoadEnergyBasedOnIndexFound(c);
                            break;
                        }
                    }
                }
                else
                {
                    AILoadEnergyToBenchedPokemon();
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Gastly")
            {
                int psychic = 0;
                int colorless = 0;

                for (var i = 0; i < ai_Active_Pokemon.EnergyLoadedCount(); ++i)
                {
                    if (ai_Active_Pokemon.GetEnergyLoadedAt(i) == 'p')
                        psychic++;
                    else
                        colorless++;
                }
                if (psychic < 2)
                {
                    for (int p = 0; p < ai_Hand.NumberOfCards(); p++)
                    {
                        if (ai_Hand.ShowName(p) == "Psychic Energy")
                        {
                            AILoadEnergyBasedOnIndexFound(p);
                            break;
                        }
                    }
                }
                else
                {
                    AILoadEnergyToBenchedPokemon();
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Haunter")
            {
                int psychic = 0;
                int colorless = 0;

                for (var i = 0; i < ai_Active_Pokemon.EnergyLoadedCount(); ++i)
                {
                    if (ai_Active_Pokemon.GetEnergyLoadedAt(i) == 'p')
                        psychic++;
                    else
                        colorless++;
                }
                if (psychic < 2)
                {
                    for (int p = 0; p < ai_Hand.NumberOfCards(); p++)
                    {
                        if (ai_Hand.ShowName(p) == "Psychic Energy")
                        {
                            AILoadEnergyBasedOnIndexFound(p);
                            break;
                        }
                    }
                }
                else
                {
                    AILoadEnergyToBenchedPokemon();
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Drowzee")
            {
                int psychic = 0;
                int colorless = 0;

                for (var i = 0; i < ai_Active_Pokemon.EnergyLoadedCount(); ++i)
                {
                    if (ai_Active_Pokemon.GetEnergyLoadedAt(i) == 'p')
                        psychic++;
                    else
                        colorless++;
                }

                if (psychic < 2)
                {
                    for (int p = 0; p < ai_Hand.NumberOfCards(); p++)
                    {
                        if (ai_Hand.ShowName(p) == "Psychic Energy")
                        {
                            AILoadEnergyBasedOnIndexFound(p);
                            break;
                        }
                    }
                }
                else
                {
                    AILoadEnergyToBenchedPokemon();
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Jynx")
            {
                int psychic = 0;
                int colorless = 0;               

                for (var i = 0; i < ai_Active_Pokemon.EnergyLoadedCount(); ++i)
                {
                    if (ai_Active_Pokemon.GetEnergyLoadedAt(i) == 'p')
                        psychic++;
                    else
                        colorless++;
                }
                if (psychic < 3)
                {
                    for (int p = 0; p < ai_Hand.NumberOfCards(); p++)
                    {
                        if (ai_Hand.ShowName(p) == "Psychic Energy")
                        {
                            AILoadEnergyBasedOnIndexFound(p);
                            break;
                        }
                    }
                }
                else
                {
                    AILoadEnergyToBenchedPokemon();
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Mewtwo")
            {
                int psychic = 0;
                int colorless = 0;

                for (var i = 0; i < ai_Active_Pokemon.EnergyLoadedCount(); ++i)
                {
                    if (ai_Active_Pokemon.GetEnergyLoadedAt(i) == 'p')
                        psychic++;
                    else
                        colorless++;
                }
                if (psychic < 2)
                {
                    for (int p = 0; p < ai_Hand.NumberOfCards(); p++)
                    {
                        if (ai_Hand.ShowName(p) == "Psychic Energy")
                        {
                            AILoadEnergyBasedOnIndexFound(p);
                            break;
                        }
                    }
                }
                else
                {
                    AILoadEnergyToBenchedPokemon();
                }
            }                     
        }

        private void AILoadEnergyBasedOnIndexFound(int x)
        {
            if (ai_Hand.ShowName(x) == "Fire Energy")
            {
                ai_Active_Pokemon.EnergyLoad('f');
            }

            else if (ai_Hand.ShowName(x) == "Water Energy")
            {
                ai_Active_Pokemon.EnergyLoad('w');
            }
            else if (ai_Hand.ShowName(x) == "Fighting Energy")
            {
                ai_Active_Pokemon.EnergyLoad('l');
            }
            else if (ai_Hand.ShowName(x) == "Psychic Energy")
            {
                ai_Active_Pokemon.EnergyLoad('p');
            }
            else if (ai_Hand.ShowName(x) == "Grass Energy")
            {
                ai_Active_Pokemon.EnergyLoad('g');
            }
            else if (ai_Hand.ShowName(x) == "Lightning Energy")
            {
                ai_Active_Pokemon.EnergyLoad('e');
            }
            else if (ai_Hand.ShowName(x) == "Metal Energy")
            {
                ai_Active_Pokemon.EnergyLoad('m');
            }
            else if (ai_Hand.ShowName(x) == "Dark Energy")
            {
                ai_Active_Pokemon.EnergyLoad('d');
            }
            else if (ai_Hand.ShowName(x) == "Fairy Energy")
            {
                ai_Active_Pokemon.EnergyLoad('a');
            }
            gameMessage.Text = "Your opponent loads one " + ai_Hand.ShowName(x).ToLower() + " to " + ai_Active_Pokemon.ShowName();
            ai_used.Add(ai_Hand.PlayCard(x));
            ai_Hand.RemoveFromHand(x);
            OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
            UpdateAIActivePokemonView();
            timer1.Start();
            aiPlayedEnergy = true;
            
        }

        private void AILoadEnergyToBenchedPokemon()
        {
            aiPlayedEnergy = true;
            timer1.Start();
        }
        private void AIChoosesActivePokemon()
        {
                Random random = new Random();
                int i = random.Next(0, aibench.NumberOfCards());

                ai_Active_Pokemon.Become(aibench.PlayCard(i));
                
                UpdateAIActivePokemonView();
                aibench.RemoveFromBench(i);
                AIUpdateBenchView();

                OpCardName.Text = ai_Active_Pokemon.ShowName();
                OpponentHpBar.Maximum = ai_Active_Pokemon.ShowHP();
                OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                OMaxHp.Text = ai_Active_Pokemon.ShowHP().ToString();

                OpponentZoom.Image = Image.FromFile(ai_Active_Pokemon.ShowImage());

                OpCardName.Visible = true;
                HpLabel2.Visible = true;
                OMaxHp.Visible = true;
                OpponentHpBar.Visible = true;
                EnergyBox2.Visible = true;

                switch (ai_Active_Pokemon.ShowEnergy())
                {
                    case 'f':
                        EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fire.gif");
                        break;
                    case 'w':
                        EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Water.gif");
                        break;
                    case 'g':
                        EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Grass.gif");
                        break;
                    case 'p':
                        EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Psychic.gif");
                        break;
                    case 'l':
                        EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fighting.gif");
                        break;
                    case 'e':
                        EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Lightning.gif");
                        break;
                    case 'c':
                        EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Colorless.gif");
                        break;
                    default:
                        EnergyBox2.Visible = false;
                        break;
                }          
        }

        //Returns the integer index of the picturebox when the user rightcliks on it to then use it to apply it to other functions
        private int GetHandBoxIndex() {

            string name = RightClickMenu.SourceControl.Name.ToString();
            string cut = name.Remove(0, 4);
            int newint = Convert.ToInt32(cut);
            return newint-1;
        }

        //Takes place when user clicks on the chosen attack from the menu
        void PerformAttack(object sender, EventArgs e)
        {
            if (active_Pokemon.ShowEnergy() == ai_Active_Pokemon.ShowWeakness())
            {
                hasWeakness = true;
            }
            else
            {
                hasWeakness = false;
            }
            if (PlusPowerOn == true)
            {
                PlusPowerDamage = 10;
            }
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            if (item != null)
            {
                int index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);
                if (playedAttack == false)
                {            

                    if (index == 0)
                    {
                       
                        if (active_Pokemon.CanPerformAttack(1) == true)
                        {                            
                            AttackingAI(1);
                            playedAttack = true;

                            if (ai_Active_Pokemon.ShowRemHP() == 0)
                            {
                                PlayerEnd.Visible = false;
                                KnockedOut.Location = new Point(683, 957);
                                KnockedOut.Visible = true;
                                FlipCoin.Visible = false;
                                poisonedCheck = false;
                                confusedCheck = false;
                                paralyzedCheck = false;
                                burnedCheck = false;
                                asleepCheck = false;
                            }                         
                        }
                        else
                        {
                            gameMessage.Text = "You do not have enough energies to perform this attack!";
                        }
                        
                    }
                       
                    else if (index == 1)
                    {
                        if (active_Pokemon.CanPerformAttack(2) == true)
                        {
                            AttackingAI(2);
                            playedAttack = true;
                            if (ai_Active_Pokemon.ShowRemHP() == 0)
                            {
                                PlayerEnd.Visible = false;
                                KnockedOut.Location = new Point(683, 957);
                                KnockedOut.Visible = true;
                                FlipCoin.Visible = false;
                                poisonedCheck = false;
                                confusedCheck = false;
                                paralyzedCheck = false;
                                burnedCheck = false;
                                asleepCheck = false;
                            }
                        }
                        else
                        {
                            gameMessage.Text = "You do not have enough energies to perform this attack!";
                        }
                        
                    }

                    else if (index == 2)
                    {
                        if (active_Pokemon.CanPerformAttack(3) == true)
                        {
                            AttackingAI(3);
                            playedAttack = true;
                            if (ai_Active_Pokemon.ShowRemHP() == 0)
                            {
                                PlayerEnd.Visible = false;
                                KnockedOut.Location = new Point(683, 957);
                                KnockedOut.Visible = true;
                                FlipCoin.Visible = false;
                                poisonedCheck = false;
                                confusedCheck = false;
                                paralyzedCheck = false;
                                burnedCheck = false;
                                asleepCheck = false;
                            }
                        }
                        else
                        {
                            gameMessage.Text = "You do not have enough energies to perform this attack!";
                        }
                        
                    }
                }

                else
                {
                    gameMessage.Text = "You can only perform one attack per turn! You can do it again during your next turn";
                }
            };
        }

        void pokemonEnergyLoadClick(object sender, EventArgs e)
        {
            int num = 0;

            switch (RightClickMenu.SourceControl.Name.ToString())
            {
                case "Hand1":
                    num = 0;
                    break;
                case "Hand2":
                    num = 1;
                    break;
                case "Hand3":
                    num = 2;
                    break;
                case "Hand4":
                    num = 3;
                    break;
                case "Hand5":
                    num = 4;
                    break;
                default:
                    break;
            }

            if (player_Hand.ShowType(num) == "energy")
            {

                if (isPreGameTurn == false)
                {
                    ToolStripMenuItem item = sender as ToolStripMenuItem;
                    if (item != null)
                    {
                        int index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);
                        if (playedEnergy == false)
                        {
                            if (index <= (bench.NumberOfCards() - 1))
                            {
                                if (player_Hand.ShowName(num) == "Fire Energy")
                                {
                                    bench.EnergyLoad(index, 'f');
                                }

                                else if (player_Hand.ShowName(num) == "Water Energy")
                                {
                                    bench.EnergyLoad(index, 'w');
                                }
                                else if (player_Hand.ShowName(num) == "Fighting Energy")
                                {
                                    bench.EnergyLoad(index, 'l');
                                }
                                else if (player_Hand.ShowName(num) == "Psychic Energy")
                                {
                                    bench.EnergyLoad(index, 'p');
                                }
                                else if (player_Hand.ShowName(num) == "Grass Energy")
                                {
                                    bench.EnergyLoad(index, 'g');
                                }
                                else if (player_Hand.ShowName(num) == "Lightning Energy")
                                {
                                    bench.EnergyLoad(index, 'e');
                                }
                                else if (player_Hand.ShowName(num) == "Metal Energy")
                                {
                                    bench.EnergyLoad(index, 'm');
                                }
                                else if (player_Hand.ShowName(num) == "Dark Energy")
                                {
                                    bench.EnergyLoad(index, 'e');
                                }
                                else if (player_Hand.ShowName(num) == "Fairy Energy")
                                {
                                    bench.EnergyLoad(index, 'a');
                                }
                                gameMessage.Text = "You loaded a " + player_Hand.ShowName(num).ToLower() + " to " + bench.ShowName(index);
                                player_used.Add(player_Hand.PlayCard(num));
                                player_Hand.RemoveFromHand(num);

                                UpdateHandView();
                                UpdateBenchView();
                                UpdateActivePokemonView();
                                playedEnergy = true;

                            }
                            else
                            {
                                if (player_Hand.ShowName(num) == "Fire Energy")
                                {
                                    active_Pokemon.EnergyLoad('f');
                                }

                                else if (player_Hand.ShowName(num) == "Water Energy")
                                {
                                    active_Pokemon.EnergyLoad('w');
                                }
                                else if (player_Hand.ShowName(num) == "Fighting Energy")
                                {
                                    active_Pokemon.EnergyLoad('l');
                                }
                                else if (player_Hand.ShowName(num) == "Psychic Energy")
                                {
                                    active_Pokemon.EnergyLoad('p');
                                }
                                else if (player_Hand.ShowName(num) == "Grass Energy")
                                {
                                    active_Pokemon.EnergyLoad('g');
                                }
                                else if (player_Hand.ShowName(num) == "Lightning Energy")
                                {
                                    active_Pokemon.EnergyLoad('e');
                                }
                                else if (player_Hand.ShowName(num) == "Metal Energy")
                                {
                                    active_Pokemon.EnergyLoad('m');
                                }
                                else if (player_Hand.ShowName(num) == "Dark Energy")
                                {
                                    active_Pokemon.EnergyLoad('d');
                                }
                                else if (player_Hand.ShowName(num) == "Fairy Energy")
                                {
                                    active_Pokemon.EnergyLoad('a');
                                }
                                gameMessage.Text = "You loaded a " + player_Hand.ShowName(num).ToLower() + " to " + active_Pokemon.ShowName();
                                player_used.Add(player_Hand.PlayCard(num));
                                player_Hand.RemoveFromHand(num);

                                UpdateHandView();
                                UpdateBenchView();
                                UpdateActivePokemonView();
                                playedEnergy = true;
                            }
                        }
                        else
                        {
                            gameMessage.Text = "You already played an energy this turn.";
                        }
                    }
                    
                }
                 else
                {
                    gameMessage.Text = "You cannot load an energy at this stage yet.";
                }
            } 
            
            else if (player_Hand.ShowType(num) == "second")
            {
                if(isPreGameTurn == true)
                {
                    gameMessage.Text = "You cannot evolve at this stage.";
                }
                else if(isyourFirstTurn == true || youStartTurn == true)
                {
                    gameMessage.Text = "You cannot evolve during your first turn.";
                }
                else
                {
                    
                    ToolStripMenuItem item = sender as ToolStripMenuItem;                  
                    if (item != null)
                    {
                        int index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);

                        if (index <= (bench.NumberOfCards() - 1))
                        {
                            if (player_Hand.ShowFirstStage(num) == bench.ShowName(index) && bench.CanEvolve(index) == true)
                            {
                                int damage = bench.ShowHp(index) - bench.ShowRemHp(index);
                                List<char> inheritenergies = bench.GetEnergyLoaded(index);
                                player_used.Add(bench.PlayCard(index));
                                gameMessage.Text = "You evolved " + bench.ShowName(index) + " into " + player_Hand.ShowName(num);
                                bench.PutInto(index, player_Hand.PlayCard(num));
                                bench.InheritDamage(index, damage);
                                bench.InheritEnergies(index, inheritenergies);
                                player_Hand.RemoveFromHand(num);
                                UpdateBenchView();
                                UpdateHandView();
                            }
                            else if (player_Hand.ShowFirstStage(num) == bench.ShowName(index) && bench.CanEvolve(index) == false)
                            {
                                gameMessage.Text = "You can\'t evolve " + bench.ShowName(index) + " as you just played it this turn.";
                            }
                            else
                            {
                                gameMessage.Text = "You can\'t place " + player_Hand.ShowName(num) + " on " + bench.ShowName(index) + ". Find its right evolved form.";
                            }
                        }
                        else
                        {
                            if (player_Hand.ShowFirstStage(num) == active_Pokemon.ShowName() && active_Pokemon.CanEvolve() == true)
                            {
                                int damage = active_Pokemon.ShowHP() - active_Pokemon.ShowRemHP();
                                List<char> inheritenergies = active_Pokemon.GetEnergyLoaded();
                                player_used.Add(active_Pokemon.GetActivePokemon());
                                gameMessage.Text = "You evolved " + active_Pokemon.ShowName() + " into " + player_Hand.ShowName(num);
                                active_Pokemon.Become(player_Hand.PlayCard(num));
                                active_Pokemon.InheritDamage(damage);
                                active_Pokemon.InheritEnergies(inheritenergies);
                                player_Hand.RemoveFromHand(num);
                                UpdateActivePokemonView();
                                UpdateHandView();
                            }
                            else if (player_Hand.ShowFirstStage(num) == active_Pokemon.ShowName() && active_Pokemon.CanEvolve() == false)
                            {
                                gameMessage.Text = "You can\'t evolve " + player_Hand.ShowName(num) + " this turn because you just played it.";
                            }
                            else
                            {
                                gameMessage.Text = "You can\'t place " + player_Hand.ShowName(num) + " on " + active_Pokemon.ShowName() + ". Find its right evolved form.";
                            }
                        }
                    }
                }                
            }

            else if (player_Hand.ShowType(num) == "trainer" && player_Hand.ShowImpact(num) == "directed")
            {
                if (isPreGameTurn == true)
                {
                    gameMessage.Text = "You can\'t use trainer cards at this stage.";
                }
                else if (youStartTurn == true)
                {
                    gameMessage.Text = "You can\'t use trainer cards when you start the game.";
                }
                else
                {
                    ToolStripMenuItem item = sender as ToolStripMenuItem;
                    if (item != null)
                    {
                        int index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);

                        if (index <= (bench.NumberOfCards() - 1))
                        {
                            PlayingTrainerCardOn(num, index);
                        }
                        else
                        {
                            PlayingTrainerCardGlobal(num);                            
                        }
                    }                   
                }
            }
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer("..\\..\\Sounds\\draw.wav");
            simpleSound.Play();
   
            player_Hand.DrawCard(player.DrawCard());
            DeckSize.Text = "x" + player.NumberOfCards().ToString();

            UpdateHandView();
            gameMessage.Text = "You drew a card from your Deck to your Hand";
        }

        private void playAsAttackerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(isPreGameTurn == true)
            {
                int num = 0;
                UpdateBenchView();
                UpdateActivePokemonView();
                switch (RightClickMenu2.SourceControl.Name.ToString())
                {
                    case "PictureBench1":
                        num = 0;
                        break;
                    case "PictureBench2":
                        num = 1;
                        break;
                    case "PictureBench3":
                        num = 2;
                        break;
                    case "PictureBench4":
                        num = 3;
                        break;
                    case "PictureBench5":
                        num = 4;
                        break;
                    default:
                        break;
                }

                if (active_Pokemon.ShowName() == "null")
                {
                    active_Pokemon.Become(bench.PlayCard(num));
                    UpdateActivePokemonView();
                    bench.RemoveFromBench(num);
                    UpdateBenchView();
                    AIChoosesActivePokemon();
                    gameMessage.Text = active_Pokemon.ShowName() + " is your new Active Pokémon. The AI chooses " + ai_Active_Pokemon.ShowName() + ".";
                    StartCheck.Visible = true;
                    StartCheck.Location = new Point(576, 957);

                }
                else
                {
                    gameMessage.Text = "You can't perform this action. You can either, retreat your Active Pokémon or switch it with a Trainer Card";
                }
            }

            else
            {
                int num = 0;
                UpdateBenchView();
                UpdateActivePokemonView();
                switch (RightClickMenu2.SourceControl.Name.ToString())
                {
                    case "PictureBench1":
                        num = 0;
                        break;
                    case "PictureBench2":
                        num = 1;
                        break;
                    case "PictureBench3":
                        num = 2;
                        break;
                    case "PictureBench4":
                        num = 3;
                        break;
                    case "PictureBench5":
                        num = 4;
                        break;
                    default:
                        break;
                }

                if (active_Pokemon.ShowName() == "null")
                {
                    active_Pokemon.Become(bench.PlayCard(num));
                    UpdateActivePokemonView();
                    gameMessage.Text = active_Pokemon.ShowName() + " is your new Active Pokémon.";
                    bench.RemoveFromBench(num);
                    UpdateBenchView();
               
                }
                else
                {
                    gameMessage.Text = "You can't perform this action. You can either, retreat your Active Pokémon or switch it with a Trainer Card";
                }
            }  
            
            
        }

        private void retreatToolStripMenuItem_Click(object sender, EventArgs e)
        {           
            if(bench.NumberOfCards() != 0) {
                energydiscard = active_Pokemon.ShowRetreatCost();
                if (energydiscard <= active_Pokemon.EnergyLoadedCount())
                {
                    PlayerEnd.Visible = false;
                    DisablePlayerActions();
                    energytodiscard = true;
                    if (energydiscard == 1)
                    {
                        gameMessage.Text = "Please choose " + energydiscard + " energy attached to " + active_Pokemon.ShowName() + " and discard them by clicking on them.";
                    }
                    else
                    {
                        gameMessage.Text = "Please choose " + energydiscard + " energies attached to " + active_Pokemon.ShowName() + " and discard them by clicking on them.";
                    }
                }
                else
                {
                    gameMessage.Text = "You do not have enough energies attached to " + active_Pokemon.ShowName() + " yet in order to retreat it";
                }
            }
            else
            {
                gameMessage.Text = "You do not have any Benched Pokémon to swap your Active one.";
            }          
        }

        private void PictureBench_Click(object sender, EventArgs e)
        {
            if(active_Pokemon_Retreat == true)
            {
                if (energydiscard == 0)
                {
                    int num;
                    Pokemon ActivePokemonTemp;
                    PictureBox n = (PictureBox)sender;
                    switch (n.Name)
                    {                       
                        case "PictureBench1":
                            num = 0;
                            gameMessage.Text = "You replaced " + active_Pokemon.ShowName() + " with " + bench.ShowName(num) + ". Now " + bench.ShowName(num) + " is your new Active Pokémon.";
                            ActivePokemonTemp = active_Pokemon.GetActivePokemon();
                            active_Pokemon.Become(bench.PlayCard(num));
                            bench.PutInto(num, ActivePokemonTemp);
                            break;
                        case "PictureBench2":
                            num = 1;
                            gameMessage.Text = "You replaced " + active_Pokemon.ShowName() + " with " + bench.ShowName(num) + ". Now " + bench.ShowName(num) + " is your new Active Pokémon.";
                            ActivePokemonTemp = active_Pokemon.GetActivePokemon();
                            active_Pokemon.Become(bench.PlayCard(num));
                            bench.PutInto(num, ActivePokemonTemp);
                            break;
                        case "PictureBench3":
                            num = 2;
                            gameMessage.Text = "You replaced " + active_Pokemon.ShowName() + " with " + bench.ShowName(num) + ". Now " + bench.ShowName(num) + " is your new Active Pokémon.";
                            ActivePokemonTemp = active_Pokemon.GetActivePokemon();
                            active_Pokemon.Become(bench.PlayCard(num));
                            bench.PutInto(num, ActivePokemonTemp);
                            break;
                        case "PictureBench4":
                            num = 3;
                            gameMessage.Text = "You replaced " + active_Pokemon.ShowName() + " with " + bench.ShowName(num) + ". Now " + bench.ShowName(num) + " is your new Active Pokémon.";
                            ActivePokemonTemp = active_Pokemon.GetActivePokemon();
                            active_Pokemon.Become(bench.PlayCard(num));
                            bench.PutInto(num, ActivePokemonTemp);
                            break;
                        case "PictureBench5":
                            num = 4;
                            gameMessage.Text = "You replaced " + active_Pokemon.ShowName() + " with " + bench.ShowName(num) + ". Now " + bench.ShowName(num) + " is your new Active Pokémon.";
                            ActivePokemonTemp = active_Pokemon.GetActivePokemon();
                            active_Pokemon.Become(bench.PlayCard(num));
                            bench.PutInto(num, ActivePokemonTemp);
                            break;
                        default:
                            break;                            
                    }
                    active_Pokemon_Retreat = false;
                    UpdateActivePokemonView();
                    UpdateBenchView();
                    PlayerEnd.Visible = true;
                    EnablePlayerActions();
                }
                else
                {
                    gameMessage.Text = "You must first discard as many energy cards attached to " + active_Pokemon.ShowName() + " as the energy cost it requires in order to retreat.";
                }
            }
            else if(trainer_switch == true)
            {                
                Pokemon ActivePokemonTemp;
                PictureBox n = (PictureBox)sender;
                int num = 0;
                switch (n.Name)
                {
                    case "PictureBench1":
                        num = 0;
                        break;
                    case "PictureBench2":
                        num = 1;
                        break;
                    case "PictureBench3":
                        num = 2;
                        break;
                    case "PictureBench4":
                        num = 3;
                        break;
                    case "PictureBench5":
                        num = 4;                      
                        break;
                    default:
                        break;
                }
                gameMessage.Text = "You replaced " + active_Pokemon.ShowName() + " with " + bench.ShowName(num) + ". Now " + bench.ShowName(num) + " is your new Active Pokémon.";
                ActivePokemonTemp = active_Pokemon.GetActivePokemon();
                active_Pokemon.Become(bench.PlayCard(num));
                bench.PutInto(num, ActivePokemonTemp);
                trainer_switch = false;
                UpdateActivePokemonView();
                UpdateBenchView();
                PlayerEnd.Visible = true;
                EnablePlayerActions();
            }
            
        }

        private void ActiveEnergy_Click(object sender, EventArgs e)
        {
            if (energytodiscard == true)
            {
                int num = 0;
                PictureBox n = (PictureBox)sender;
                switch (n.Name)
                {
                    case "ActiveEnergy1":
                        num = 0;
                        break;
                    case "ActiveEnergy2":
                        num = 1;
                        break;
                    case "ActiveEnergy3":
                        num = 2;
                        break;
                    case "ActiveEnergy4":
                        num = 3;
                        break;
                    case "ActiveEnergy5":
                        num = 4;
                        break;
                    default:
                        break;
                }

                char x = active_Pokemon.GetEnergyLoadedAt(num);

                int index = 0;

                if (x == 'f')
                {      
                        while(player_used.GetName(index) != "Fire Energy")
                        {
                            index++;
                        }
                }
                else if (x == 'w')
                {
                        while (player_used.GetName(index) != "Water Energy")
                        {
                            index++;
                        }                    
                }
                else if (x == 'g')
                {
                    while (player_used.GetName(index) != "Grass Energy")
                    {
                        index++;
                    }
                }

                Pokemon temp = player_used.GetCard(index);
                discard.Add(temp);

                int index2 = 0;
                if (x == 'f')
                {
                    while (player_used.GetName(index2) != "Fire Energy")
                    {
                        index2++;
                    }
                }
                else if (x == 'w')
                {
                    while (player_used.GetName(index2) != "Water Energy")
                    {
                        index2++;
                    }
                }
                else if (x == 'g')
                {
                    while (player_used.GetName(index2) != "Grass Energy")
                    {
                        index2++;
                    }
                }
                player_used.RemoveAt(index2);

                active_Pokemon.DiscardEnergyAt(num);
                
                energydiscard -= 1;
                UpdateActivePokemonView();
                UpdateDiscardView();
                if (energydiscard == 0)
                {
                    gameMessage.Text = "Please choose now your Benched Pokémon to replace " + active_Pokemon.ShowName() + " by clicking on it.";
                    active_Pokemon_Retreat = true;
                    energytodiscard = false;
                }
                else
                {
                    gameMessage.Text = "You should discard " + energydiscard + " more energies.";
                }
                               
            }
            
        }

        private void AIActiveEnergy_Click(object sender, EventArgs e)
        {
            if (aienergytodiscard == true)
            {
                int num = 0;
                PictureBox n = (PictureBox)sender;
                switch (n.Name)
                {
                    case "OActiveEnergy1":
                        num = 0;
                        break;
                    case "OActiveEnergy2":
                        num = 1;
                        break;
                    case "OActiveEnergy3":
                        num = 2;
                        break;
                    case "OActiveEnergy4":
                        num = 3;
                        break;
                    case "OActiveEnergy5":
                        num = 4;
                        break;
                    default:
                        break;
                }

                char x = ai_Active_Pokemon.GetEnergyLoadedAt(num);

                int index = 0;

                if (x == 'e')
                {
                    while (ai_used.GetName(index) != "Lightning Energy")
                    {
                        index++;
                    }
                }
                else if (x == 'p')
                {
                    while (ai_used.GetName(index) != "Psychic Energy")
                    {
                        index++;
                    }
                }
                else if (x == 'g')
                {
                    while (ai_used.GetName(index) != "Grass Energy")
                    {
                        index++;
                    }
                }

                Pokemon temp = ai_used.GetCard(index);
                discard.Add(temp);

                int index2 = 0;
                if (x == 'e')
                {
                    while (ai_used.GetName(index2) != "Lightning Energy")
                    {
                        index2++;
                    }
                }
                else if (x == 'p')
                {
                    while (ai_used.GetName(index2) != "Psychic Energy")
                    {
                        index2++;
                    }
                }
                else if (x == 'g')
                {
                    while (ai_used.GetName(index2) != "Grass Energy")
                    {
                        index2++;
                    }
                }
                ai_used.RemoveAt(index2);
                ai_Active_Pokemon.DiscardEnergyAt(num);
                UpdateAIActivePokemonView();
                UpdateAIDiscardView();
                gameMessage.Text = "You have succesfully discarded an Energy Card from the Defending Pokémon.";               
                aienergytodiscard = false;
                PlayerEnd.Visible = true;
                UpdateHandView();
                UpdateDiscardView();
                GetViewBackToNormal();
            }
        }

        private void FlipCoin_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer("..\\..\\Sounds\\flipcoin.wav");
            simpleSound.Play();
            gameMessage.Visible = false;
            CoinResult.Visible = false;
            FlipCoin.Visible = false;
            Thread.Sleep(2000);
            Random random = new Random();
            int result = random.Next(0,2);
            if(result == 0)
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\Wizards_Silver_Chansey_Coin.png");
                CoinResult.Visible = true;              
                gameMessage.Visible = true;
                PlayerEnd.Visible = true;
                
                if (confusedCheck == true)
                {
                    gameMessage.Text = "You flip the coin and get Heads. " + ai_Active_Pokemon.ShowName() + " is now Confused.";
                    confusedCheck = false;
                    AIConfused = true;
                }
                else if(poisonedCheck == true)
                {
                    gameMessage.Text = "You flip the coin and get Heads. " + ai_Active_Pokemon.ShowName() + " is now Poisoned.";
                    poisonedCheck = false;
                    AIPoisoned = true;
                }
                else if(asleepCheck == true)
                {
                    gameMessage.Text = "You flip the coin and get Heads. " + ai_Active_Pokemon.ShowName() + " is now Asleep.";
                    asleepCheck = false;
                    AIAsleep = true;
                }
                else if (burnedCheck == true)
                {
                    gameMessage.Text = "You flip the coin and get Heads. " + ai_Active_Pokemon.ShowName() + " is now Burned.";
                    burnedCheck = false;
                    AIBurned = true;
                }
                else if (paralyzedCheck == true)
                {
                    gameMessage.Text = "You flip the coin and get Heads. " + ai_Active_Pokemon.ShowName() + " is now Paralyzed.";
                    paralyzedCheck = false;
                    AIParalyzed = true;
                }
                else if (dealsNothingCheck == true)
                {
                    ai_Active_Pokemon.DealDamage(30, hasWeakness, PlusPowerDamage);
                    OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                    if (ai_Active_Pokemon.ShowRemHP() == 0)
                    {
                        gameMessage.Text = "You flip the coin and get Heads. You deal 30 damage on " + ai_Active_Pokemon.ShowName() + " and gets Knocked out.";
                        PlayerEnd.Visible = false;
                        KnockedOut.Location = new Point(683, 957);
                        KnockedOut.Visible = true;
                    }
                    else
                    {
                        gameMessage.Text = "You flip the coin and get Heads. You deal 30 damage on " + ai_Active_Pokemon.ShowName() + ".";
                    }
                    dealsNothingCheck = false;                  
                }                         
            }
            else
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\tails.gif");
                CoinResult.Visible = true;
                gameMessage.Text = "You flip the coin and get Tails, nothing else happens.";
                gameMessage.Visible = true;
                PlayerEnd.Visible = true;
            }
        }
        private void KnockedOut_Click(object sender, EventArgs e)
        {

            gameMessage.Text = ai_Active_Pokemon.ShowName() + " is Knocked out so it is to be replaced by a Benched Pokémon.";
            //Clear Active Card completely from effects

            ai_Active_Pokemon.ClearEverthingFromCard();
            AIConfused = false;
            AIParalyzed = false;
            AIAsleep = false;
            AIPoisoned = false;
            AIBurned = false;

            //Passing all the used cards to the discard
            for(int i = 0; i < ai_used.Count(); i++)
            {
                ai_discard.Add(ai_used.GetCard(i));
            }
            ai_used.DiscardAll();

            //Clearing the views
            ai_discard.Add(ai_Active_Pokemon.GetActivePokemon());
            OpCardName.Visible = false;
            EnergyBox2.Visible = false;
            OpponentZoom.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            HpLabel2.Visible = false;
            OMaxHp.Visible = false;
            OpponentHpBar.Visible = false;
            ai_Active_Pokemon.Become(new Pokemon(0, "null", "", 'u', ""));
            UpdateAIDiscardView();
            UpdateAIActivePokemonView();
  
            KnockedOut.Visible = false;
            Replace.Location = new Point(683, 957);
            Replace.Visible = true;
        }
        private void Mulligan_Click(object sender, EventArgs e)
        {
            player.ShuffleCardFromHandIntoDeck(player_Hand.PlayCard(0));
            player.ShuffleCardFromHandIntoDeck(player_Hand.PlayCard(1));
            player.ShuffleCardFromHandIntoDeck(player_Hand.PlayCard(2));
            player.ShuffleCardFromHandIntoDeck(player_Hand.PlayCard(3));
            player.ShuffleCardFromHandIntoDeck(player_Hand.PlayCard(4));
            player.ShuffleCardFromHandIntoDeck(player_Hand.PlayCard(5));
            player.ShuffleCardFromHandIntoDeck(player_Hand.PlayCard(6));
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);

            ai_Hand.DrawCard(ai.DrawCard());

            player.Shuffle();

            player_Hand.DrawCard(player.DrawCard());
            player_Hand.DrawCard(player.DrawCard());
            player_Hand.DrawCard(player.DrawCard());
            player_Hand.DrawCard(player.DrawCard());
            player_Hand.DrawCard(player.DrawCard());
            player_Hand.DrawCard(player.DrawCard());
            player_Hand.DrawCard(player.DrawCard());

            DeckSize.Text = "x" + player.NumberOfCards().ToString();
            ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
            OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();

            gameMessage.Text = "You draw again 7 cards and your opponent draws an extra card.";
            UpdateHandView();
            EnablePlayerActions();
            Mulligan.Visible = false;
            SoundPlayer mulligan = new SoundPlayer("..\\..\\Sounds\\mulligan.wav");
            mulligan.Play();
            if (player_Hand.NumOfBasicPokemon() == 0 && ai_Hand.NumOfBasicPokemon() != 0)
            {
                gameMessage.Text = "As you do not have any Basic Pokémon, you should perform Mulligan and draw another 7 cards.";

                StartCheck.Visible = false;
                Mulligan.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
            else if (player_Hand.NumOfBasicPokemon() == 0 && ai_Hand.NumOfBasicPokemon() == 0)
            {
                gameMessage.Text = "Apparently you and your opponent do not have any Basic Pokémons, so both of you should perform Mulligan.";

                StartCheck.Visible = false;
                Mulligan2.Location = new Point(576, 957);
                Mulligan2.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
            else if (player_Hand.NumOfBasicPokemon() != 0 && ai_Hand.NumOfBasicPokemon() == 0)
            {
                gameMessage.Text = "Your opponent does not have any Basic Pokémons, so he should perform Mulligan.";

                StartCheck.Visible = false;
                Mulligan3.Location = new Point(576, 957);
                Mulligan3.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
        }

        private void Mulligan2_Click(object sender, EventArgs e)
        {
            player.ShuffleCardFromHandIntoDeck(player_Hand.PlayCard(0));
            player.ShuffleCardFromHandIntoDeck(player_Hand.PlayCard(1));
            player.ShuffleCardFromHandIntoDeck(player_Hand.PlayCard(2));
            player.ShuffleCardFromHandIntoDeck(player_Hand.PlayCard(3));
            player.ShuffleCardFromHandIntoDeck(player_Hand.PlayCard(4));
            player.ShuffleCardFromHandIntoDeck(player_Hand.PlayCard(5));
            player.ShuffleCardFromHandIntoDeck(player_Hand.PlayCard(6));
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);

            ai.ShuffleCardFromHandIntoDeck(ai_Hand.PlayCard(0));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.PlayCard(1));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.PlayCard(2));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.PlayCard(3));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.PlayCard(4));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.PlayCard(5));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.PlayCard(6));
            ai_Hand.RemoveFromHand(0);
            ai_Hand.RemoveFromHand(0);
            ai_Hand.RemoveFromHand(0);
            ai_Hand.RemoveFromHand(0);
            ai_Hand.RemoveFromHand(0);
            ai_Hand.RemoveFromHand(0);
            ai_Hand.RemoveFromHand(0);


            ai_Hand.DrawCard(ai.DrawCard());
            player_Hand.DrawCard(player.DrawCard());

            player.Shuffle();
            ai.Shuffle();

            player_Hand.DrawCard(player.DrawCard());
            player_Hand.DrawCard(player.DrawCard());
            player_Hand.DrawCard(player.DrawCard());
            player_Hand.DrawCard(player.DrawCard());
            player_Hand.DrawCard(player.DrawCard());
            player_Hand.DrawCard(player.DrawCard());
            player_Hand.DrawCard(player.DrawCard());

            ai_Hand.DrawCard(ai.DrawCard());
            ai_Hand.DrawCard(ai.DrawCard());
            ai_Hand.DrawCard(ai.DrawCard());
            ai_Hand.DrawCard(ai.DrawCard());
            ai_Hand.DrawCard(ai.DrawCard());
            ai_Hand.DrawCard(ai.DrawCard());
            ai_Hand.DrawCard(ai.DrawCard());

            DeckSize.Text = "x" + player.NumberOfCards().ToString();
            ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
            OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();

            gameMessage.Text = "You draw again 7 cards and your opponent draws 7 too. Both also draw an extra card for Mulligan.";
            UpdateHandView();
            EnablePlayerActions();
            Mulligan2.Visible = false;
            SoundPlayer mulligan = new SoundPlayer("..\\..\\Sounds\\mulligan.wav");
            mulligan.Play();

            if (player_Hand.NumOfBasicPokemon() == 0 && ai_Hand.NumOfBasicPokemon() != 0)
            {
                gameMessage.Text = "As you do not have any Basic Pokémon, you should perform Mulligan and draw another 7 cards.";

                StartCheck.Visible = false;
                Mulligan.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
            else if (player_Hand.NumOfBasicPokemon() == 0 && ai_Hand.NumOfBasicPokemon() == 0)
            {
                gameMessage.Text = "Apparently you and your opponent do not have any Basic Pokémons, so both of you should perform Mulligan.";

                StartCheck.Visible = false;
                Mulligan2.Location = new Point(576, 957);
                Mulligan2.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
            else if (player_Hand.NumOfBasicPokemon() != 0 && ai_Hand.NumOfBasicPokemon() == 0)
            {
                gameMessage.Text = "Your opponent does not have any Basic Pokémons, so he should perform Mulligan.";

                StartCheck.Visible = false;
                Mulligan3.Location = new Point(576, 957);
                Mulligan3.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
        }

        private void Mulligan3_Click(object sender, EventArgs e)
        {
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.PlayCard(0));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.PlayCard(1));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.PlayCard(2));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.PlayCard(3));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.PlayCard(4));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.PlayCard(5));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.PlayCard(6));
            ai_Hand.RemoveFromHand(0);
            ai_Hand.RemoveFromHand(0);
            ai_Hand.RemoveFromHand(0);
            ai_Hand.RemoveFromHand(0);
            ai_Hand.RemoveFromHand(0);
            ai_Hand.RemoveFromHand(0);
            ai_Hand.RemoveFromHand(0);

            player_Hand.DrawCard(player.DrawCard());
            UpdateHandView();

            ai.Shuffle();

            ai_Hand.DrawCard(ai.DrawCard());
            ai_Hand.DrawCard(ai.DrawCard());
            ai_Hand.DrawCard(ai.DrawCard());
            ai_Hand.DrawCard(ai.DrawCard());
            ai_Hand.DrawCard(ai.DrawCard());
            ai_Hand.DrawCard(ai.DrawCard());
            ai_Hand.DrawCard(ai.DrawCard());

            DeckSize.Text = "x" + player.NumberOfCards().ToString();
            ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
            OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();

            gameMessage.Text = "Your opponent draws 7 cards. You draw an extra card for Mulligan.";


            EnablePlayerActions();
            Mulligan3.Visible = false;
            SoundPlayer mulligan = new SoundPlayer("..\\..\\Sounds\\mulligan.wav");
            mulligan.Play();

            if (player_Hand.NumOfBasicPokemon() == 0 && ai_Hand.NumOfBasicPokemon() != 0)
            {
                gameMessage.Text = "As you do not have any Basic Pokémon, you should perform Mulligan and draw another 7 cards.";

                StartCheck.Visible = false;
                Mulligan.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
            else if (player_Hand.NumOfBasicPokemon() == 0 && ai_Hand.NumOfBasicPokemon() == 0)
            {
                gameMessage.Text = "Apparently you and your opponent do not have any Basic Pokémons, so both of you should perform Mulligan.";

                StartCheck.Visible = false;
                Mulligan2.Location = new Point(576, 957);
                Mulligan2.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
            else if (player_Hand.NumOfBasicPokemon() != 0 && ai_Hand.NumOfBasicPokemon() == 0)
            {
                gameMessage.Text = "Your opponent does not have any Basic Pokémons, so he should perform Mulligan.";

                StartCheck.Visible = false;
                Mulligan3.Location = new Point(576, 957);
                Mulligan3.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
        }

        private void StartCheck_Click(object sender, EventArgs e)
        {
            Heads.Visible = true;
            Tails.Visible = true;
            Heads.Location = new Point(576, 957); 
            Tails.Location = new Point(683, 957);
            gameMessage.Text = "Choose either Heads or Tails to see who starts.";
            StartCheck.Visible = false;
            DisablePlayerActions();
            isPreGameTurn = false;
            SoundPlayer youdone = new SoundPlayer("..\\..\\Sounds\\youdone.wav");
            youdone.Play();
        }

        private void Heads_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer("..\\..\\Sounds\\flipcoin.wav");
            simpleSound.Play();
            gameMessage.Visible = false;
            CoinResult.Visible = false;
            Heads.Visible = false;
            Tails.Visible = false;
            Thread.Sleep(2000);
            Random random = new Random();
            int result = random.Next(0, 2);
            if (result == 0)
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\Wizards_Silver_Chansey_Coin.png");
                CoinResult.Visible = true;
                gameMessage.Text = "You flip the coin and get Heads. You start. Please draw a card!";
                gameMessage.Visible = true;
                isPreGameTurn = false;
                youStartTurn = true;
                Draw2.Location = new Point(576, 957);
                Draw2.Visible = true;
                
            }
            else
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\tails.gif");
                CoinResult.Visible = true;
                gameMessage.Text = "You flip the coin and get Tails. Your opponent starts";
                gameMessage.Visible = true;
                isPreGameTurn = false;
                aiStartsTurn = true;
                Next1.Location = new Point(576, 957);
                Next1.Visible = true;
            }
        }

        private void Tails_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer("..\\..\\Sounds\\flipcoin.wav");
            simpleSound.Play();
            gameMessage.Visible = false;
            CoinResult.Visible = false;
            Heads.Visible = false;
            Tails.Visible = false;
            Thread.Sleep(2000);
            Random random = new Random();
            int result = random.Next(0, 2);
            if (result == 0)
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\Wizards_Silver_Chansey_Coin.png");
                CoinResult.Visible = true;
                gameMessage.Text = "You flip the coin and get Heads. Your opponent starts";
                gameMessage.Visible = true;
                isPreGameTurn = false;
                aiStartsTurn = true;
                Next1.Location = new Point(576, 957);
                Next1.Visible = true;
            }
            else
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\tails.gif");
                CoinResult.Visible = true;
                gameMessage.Text = "You flip the coin and get Tails. You start. Please draw a card!";
                gameMessage.Visible = true;
                isPreGameTurn = false;
                youStartTurn = true;
                Draw2.Location = new Point(576, 957);
                Draw2.Visible = true;
            }
        }

        private void Draw2_Click(object sender, EventArgs e)
        {
            CoinResult.Visible = false;

            PlayerEnd.Location = new Point(683, 957);
            PlayerEnd.Visible = true;
          

            player_Hand.DrawCard(player.DrawCard());
            DeckSize.Text = "x" + player.NumberOfCards().ToString();
            gameMessage.Text = "You draw a card.";
            UpdateHandView();
            Draw2.Visible = false;
            EnablePlayerActions();
            SoundPlayer draw_sound = new SoundPlayer("..\\..\\Sounds\\draw.wav");
            draw_sound.Play();
        }

        private void Next1_Click(object sender, EventArgs e)
        {
            CoinResult.Visible = false;
            
            ai_Hand.DrawCard(ai.DrawCard());
            ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
            OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
            gameMessage.Text = "Your opponent draws a card.";
            Next1.Visible = false;
            //Next2.Location = new Point(683, 957);
            //Next2.Visible = true;
            
            aiPlayedEnergy = false;
            aiPlayedAttack = false;
            hasWeakness = false;
            playSimpleSound();
            _ticks = 0;
            timer1.Start();
            
        }

        private void Next2_Click(object sender, EventArgs e)
        {
            if (trainer_lass == true)
            {
                //discard all the trainer cards from AI hand
                for (int i = ai_Hand.NumberOfCards() - 1; i >= 0; i--)
                {
                    if (ai_Hand.ShowType(i) == "trainer")
                    {
                        ai_discard.Add(ai_Hand.PlayCard(i));
                        ai_Hand.RemoveFromHand(i);
                    }
                }

                //discard all the trainer cards from player's hand
                for (int i = player_Hand.NumberOfCards() - 1; i >= 0; i--)
                {
                    if (player_Hand.ShowType(i) == "trainer")
                    {
                        discard.Add(ai_Hand.PlayCard(i));
                        player_Hand.RemoveFromHand(i);
                    }
                }
                trainer_lass = false;
                Next2.Visible = false;
                PlayerEnd.Visible = false;
                GetViewBackToNormal();
                gameMessage.Text = "You discarded all the Trainer cards from both hands.";
            }
        }

        private void EndOpponentsTurn_Click(object sender, EventArgs e)
        {
            if(aiStartsTurn == true)
            {
                aiStartsTurn = false;
                isyourFirstTurn = true;
            }
            else if(isOpponentFirstTurn == true)
            {
                isOpponentFirstTurn = false;
                isYourTurn = true;
            }
            else
            {
                isYourTurn = true;
            }
            isOpponentsTurn = false;
            EndOpponentsTurn.Visible = false;
            playedEnergy = false;
            playedAttack = false;
            Draw2.Location = new Point(576, 957);
            Draw2.Visible = true;

            bench.ChangeCanEvolveStatusToTrue();
            active_Pokemon.ChangeCanEvolveStatusToTrue();
            if (AIPoisoned == true)
            {
                ai_Active_Pokemon.ReceiveDamage(10);
                OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                gameMessage.Text = "As your opponent is Poisoned, it receives 10 damage after his turn.";
                if (ai_Active_Pokemon.ShowRemHP() == 0)
                {
                    isOpponentsTurn = true;
                    isYourTurn = false;
                    Draw2.Visible = false;
                    KnockedOut.Location = new Point(683, 957);
                    KnockedOut.Visible = true;
                }
            }
            else if (AIBurned == true)
            {
                ai_Active_Pokemon.ReceiveDamage(20);
                OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                gameMessage.Text = "As your opponent is Burned, it receives 20 damage after his turn.";
                if (ai_Active_Pokemon.ShowRemHP() == 0)
                {
                    isOpponentsTurn = true;
                    isYourTurn = false;
                    Draw2.Visible = false;
                    KnockedOut.Location = new Point(683, 957);
                    KnockedOut.Visible = true;
                }
            }
            else if (AIParalyzed == true)
            {
                gameMessage.Text = "Your opponent is no longer Paralyzed after the end of his turn.";
                AIParalyzed = false;
            }
        }

        private void PlayerEnd_Click(object sender, EventArgs e)
        {
            if(youStartTurn == true)
            {
                youStartTurn = false;
                isOpponentFirstTurn = true;
            }
            else if (isyourFirstTurn == true)
            {
                isyourFirstTurn = false;
                isOpponentsTurn = true;
            }
            else
            {
                isOpponentsTurn = true;
            }
            isYourTurn = false;            
            PlayerEnd.Visible = false;
            Next1.Location = new Point(683, 957);
            Next1.Visible = true;

            aibench.ChangeCanEvolveStatusToTrue();
            ai_Active_Pokemon.ChangeCanEvolveStatusToTrue();

            PlusPowerOn = false;

            if(AIPoisoned == true)
            {
                ai_Active_Pokemon.ReceiveDamage(10);
                OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                gameMessage.Text = "As your opponent is Poisoned, it receives 10 damage after your turn.";
                if (ai_Active_Pokemon.ShowRemHP() == 0)
                {
                    isYourTurn = true;
                    isOpponentsTurn = false;
                    Next1.Visible = false;
                    KnockedOut.Location = new Point(683, 957);
                    KnockedOut.Visible = true;
                }
            }
            else if(AIBurned == true)
            {
                ai_Active_Pokemon.ReceiveDamage(20);
                OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                gameMessage.Text = "As your opponent is Burned, it receives 20 damage after your turn.";
                if (ai_Active_Pokemon.ShowRemHP() == 0)
                {
                    isYourTurn = true;
                    isOpponentsTurn = false;
                    Next1.Visible = false;
                    KnockedOut.Location = new Point(683, 957);
                    KnockedOut.Visible = true;
                }
            }
        }

        private void DisablePlayerActions()
        {
            RightClickMenu.Enabled = false;
            RightClickMenu2.Enabled = false;
            RightClickMenu3.Enabled = false;
        }
        private void EnablePlayerActions()
        {
            RightClickMenu.Enabled = true;
            RightClickMenu2.Enabled = true;
            RightClickMenu3.Enabled = true;
        }

        private void AttackingAI(int index)
        {
            DisablePlayerActions();
            if (active_Pokemon.ShowName() == "Charmander")
            {
                if(active_Pokemon.ShowAttackName(index) == "Scratch")
                {
                    ai_Active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                    gameMessage.Text = "You perform Scratch and deal 10 damage on " + ai_Active_Pokemon.ShowName();  
                }
                else if(active_Pokemon.ShowAttackName(index) == "Ember")
                {
                    ai_Active_Pokemon.DealDamage(30, hasWeakness, PlusPowerDamage);
                    OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                    gameMessage.Text = "You perform Ember and deal 30 damage on " + ai_Active_Pokemon.ShowName() + " . You also discard a Fire Energy";
                    int e = 0;
                    while(player_used.GetName(e) != "Fire Energy")
                    {
                        e++;
                    }     
                    discard.Add(player_used.GetCard(e));
                    player_used.RemoveAt(e);
                    active_Pokemon.DiscardEnergyType('f');
                    UpdateActivePokemonView();
                    UpdateDiscardView();
                }
            }
            else if(active_Pokemon.ShowName() == "Charmeleon")
            {
                if (active_Pokemon.ShowAttackName(index) == "Slash")
                {
                    ai_Active_Pokemon.DealDamage(30, hasWeakness, PlusPowerDamage);
                    gameMessage.Text = "You perform Slash and deal 30 damage on " + ai_Active_Pokemon.ShowName();                                         
                    OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                }
                else if (active_Pokemon.ShowAttackName(index) == "Flamethrower")
                {
                    ai_Active_Pokemon.DealDamage(50, hasWeakness, PlusPowerDamage);
                    gameMessage.Text = "You perform Flamethrower and deal 50 damage on " + ai_Active_Pokemon.ShowName() + ". You also discard a Fire Energy.";
                    OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                    int e = 0;
                    while (player_used.GetName(e) != "Fire Energy")
                    {
                        e++;
                    }
                    discard.Add(player_used.GetCard(e));
                    player_used.RemoveAt(e);
                    active_Pokemon.DiscardEnergyType('f');
                    UpdateActivePokemonView();
                    UpdateDiscardView();
                }
            }
            else if (active_Pokemon.ShowName() == "Vulpix")
            {
                if (active_Pokemon.ShowAttackName(index) == "Confuse Ray")
                {
                    ai_Active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    gameMessage.Text = "You perform Confuse Ray and deal 10 damage on " + ai_Active_Pokemon.ShowName();
                    OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                    PlayerEnd.Visible = false;
                    if(AIConfused == false)
                    {
                        FlipCoin.Visible = true;
                        confusedCheck = true;
                    }
                    else
                    {
                        FlipCoin.Visible = false;
                        PlayerEnd.Visible = true;
                        confusedCheck = false;
                    }
                }
            }
            else if (active_Pokemon.ShowName() == "Ninetales")
            {
                if (active_Pokemon.ShowAttackName(index) == "Lure")
                {
                    if(aibench.NumberOfCards() == 0)
                    {
                        gameMessage.Text = "Your opponent does not have any Benched Pokémon. Please perform another action or end your turn.";

                    }
                    else
                    {
                        ChangeOpponentActivePokemon();
                    }                   
                }
                else if (active_Pokemon.ShowAttackName(index) == "Fire Blast")
                {
                    ai_Active_Pokemon.DealDamage(80, hasWeakness, PlusPowerDamage);
                    gameMessage.Text = "You perform Fire Blast and deal 80 damage on " + ai_Active_Pokemon.ShowName() + " . You also discard a fire energy";
                    OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                    int e = 0;
                    while (player_used.GetName(e) != "Fire Energy")
                    {
                        e++;
                    }
                    discard.Add(player_used.GetCard(e));
                    player_used.RemoveAt(e);
                    active_Pokemon.DiscardEnergyType('f');
                    UpdateActivePokemonView();
                    UpdateDiscardView();
                }
            }
            else if (active_Pokemon.ShowName() == "Growlithe")
            {
                if (active_Pokemon.ShowAttackName(index) == "Flare")
                {
                    ai_Active_Pokemon.DealDamage(20, hasWeakness, PlusPowerDamage);
                    gameMessage.Text = "You perform Flare and deal 20 damage on " + ai_Active_Pokemon.ShowName();
                    OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                }
            }
            else if (active_Pokemon.ShowName() == "Arcanine")
            {
                if (active_Pokemon.ShowAttackName(index) == "Flamethrower")
                {
                    ai_Active_Pokemon.DealDamage(50, hasWeakness, PlusPowerDamage);
                    gameMessage.Text = "You perform Flamethrower and deal 50 damage on " + ai_Active_Pokemon.ShowName() + " . You also discard a fire energy";
                    OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                    int e = 0;
                    while (player_used.GetName(e) != "Fire Energy")
                    {
                        e++;
                    }
                    discard.Add(player_used.GetCard(e));
                    player_used.RemoveAt(e);
                    active_Pokemon.DiscardEnergyType('f');
                    UpdateActivePokemonView();
                    UpdateDiscardView();
                }
                else if (active_Pokemon.ShowAttackName(index) == "Take Down")
                {
                    ai_Active_Pokemon.DealDamage(80, hasWeakness, PlusPowerDamage);
                    active_Pokemon.DealDamage(30, hasWeakness, PlusPowerDamage);
                    gameMessage.Text = "You perform Take Down and deal 80 damage on " + ai_Active_Pokemon.ShowName() + " . Arcanine also deals 30 to itself.";
                    OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                    PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                }
            }
            else if (active_Pokemon.ShowName() == "Weedle")
            {
                if (active_Pokemon.ShowAttackName(index) == "Poison Sting")
                {
                    ai_Active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    gameMessage.Text = "You perform Poison Sting and deal 10 damage on " + ai_Active_Pokemon.ShowName();
                    OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                    PlayerEnd.Visible = false;
                    if (AIPoisoned == false)
                    {
                        FlipCoin.Visible = true;
                        poisonedCheck = true;
                    }
                    else
                    {
                        FlipCoin.Visible = false;
                        PlayerEnd.Visible = true;
                        poisonedCheck = false;
                    }
                }
            }
            else if (active_Pokemon.ShowName() == "Nidoran Male")
            {
                if (active_Pokemon.ShowAttackName(index) == "Horn Hazard")
                {
                    
                    gameMessage.Text = "Flip the coin to see if this attack deals any damage..";                  
                    PlayerEnd.Visible = false;
                    FlipCoin.Visible = true;
                    dealsNothingCheck = true;
                }
            }
            else if (active_Pokemon.ShowName() == "Tangela")
            {
                if (active_Pokemon.ShowAttackName(index) == "Bind")
                {
                    ai_Active_Pokemon.DealDamage(20, hasWeakness, PlusPowerDamage);
                    gameMessage.Text = "You perform Bind and deal 20 damage on " + ai_Active_Pokemon.ShowName();
                    OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                    PlayerEnd.Visible = false;
                    if (AIParalyzed == false)
                    {
                        FlipCoin.Visible = true;
                        paralyzedCheck = true;
                    }
                    else
                    {
                        FlipCoin.Visible = false;
                        PlayerEnd.Visible = true;
                        paralyzedCheck = false;
                    }
                }
                else if (active_Pokemon.ShowAttackName(index) == "Poisonpowder")
                {
                    ai_Active_Pokemon.DealDamage(20, hasWeakness, PlusPowerDamage);
                    gameMessage.Text = "You perform Poisonpowder and deal 20 damage on " + ai_Active_Pokemon.ShowName() +". Your opponent is also now Poisoned.";
                    OpponentHpBar.Value = ai_Active_Pokemon.ShowRemHP();
                    AIPoisoned = true;
                }
            }
        }

        private void AIAttacks(int index)
        {
            if (ai_Active_Pokemon.ShowName() == "Abra")
            {
                if (ai_Active_Pokemon.ShowAttackName(index) == "Psychock")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                    gameMessage.Text = "The AI performs Psychock on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Drowzee")
            {
                if (ai_Active_Pokemon.ShowAttackName(index) == "Pound")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                    gameMessage.Text = "The AI performs Pound on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
                else if (ai_Active_Pokemon.ShowAttackName(index) == "Confuse Ray")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                    gameMessage.Text = "The AI performs Confuse Ray on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Gastly")
            {
                if (ai_Active_Pokemon.ShowAttackName(index) == "Sleeping Gas")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                    gameMessage.Text = "The AI performs Sleeping Gas on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
                else if(ai_Active_Pokemon.ShowAttackName(index) == "Destiny Bond")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                    gameMessage.Text = "The AI performs Destiny Bond on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Jynx")
            {
                if (ai_Active_Pokemon.ShowAttackName(index) == "Doubleslap")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                    gameMessage.Text = "The AI performs Doubleslap on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Magnemite")
            {
                if (ai_Active_Pokemon.ShowAttackName(index) == "Thunder Wave")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                    gameMessage.Text = "The AI performs Thunder Wave on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
                else if (ai_Active_Pokemon.ShowAttackName(index) == "Self Destruct")
                {
                    active_Pokemon.DealDamage(40, hasWeakness, PlusPowerDamage);
                    PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                    gameMessage.Text = "The AI performs Self Destruct on " + active_Pokemon.ShowName() + " and deals 40 damage.";
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Mewtwo")
            {
                if (ai_Active_Pokemon.ShowAttackName(index) == "Psychic")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                    gameMessage.Text = "The AI performs Psychic on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Pikachu")
            {
                if (ai_Active_Pokemon.ShowAttackName(index) == "Gnaw")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                    gameMessage.Text = "The AI performs Gnaw on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
                else if (ai_Active_Pokemon.ShowAttackName(index) == "Thunder Jolt")
                {
                    active_Pokemon.DealDamage(30, hasWeakness, PlusPowerDamage);
                    PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                    ai_Active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    gameMessage.Text = "The AI performs Thunder Jolt on " + active_Pokemon.ShowName() + " and deals 30 damage and 10 to itself.";
                }
            }
            
            timer1.Start();
        }
        private void PlayingTrainerCardOn(int x, int y)
        {
            //x is the card index from the hand and y is the card index on the bench
            if (player_Hand.ShowName(x) == "Potion")
            {
                bench.HealHp(20, y);
                gameMessage.Text = "You use a Potion and heal 20 damage on " + bench.ShowName(y);
                discard.Add(player_Hand.PlayCard(x));
                player_Hand.RemoveFromHand(x);
                UpdateHandView();
                UpdateDiscardView();
            }
            else if (player_Hand.ShowName(x) == "Super Potion")
            {
                bench.HealHp(40, y);
                gameMessage.Text = "You use a Super Potion and heal 40 damage on " + bench.ShowName(y) + ". You also discard an Energy Card.";
                discard.Add(player_Hand.PlayCard(x));
                player_Hand.RemoveFromHand(x);
                UpdateHandView();
                UpdateDiscardView();
            }
        }
        private void PlayingTrainerCardGlobal(int x)
        {
            //x is the card index from the hand
            if (player_Hand.ShowName(x) == "Potion")
            {
                active_Pokemon.HealHp(20);
                gameMessage.Text = "You use a Potion and heal 20 damage on " + active_Pokemon.ShowName();
                PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                discard.Add(player_Hand.PlayCard(x));
                player_Hand.RemoveFromHand(x);
                UpdateHandView();
                UpdateDiscardView();
            }
            else if (player_Hand.ShowName(x) == "Super Potion")
            {
                active_Pokemon.HealHp(40);
                gameMessage.Text = "You use a Super Potion and heal 40 damage on " + active_Pokemon.ShowName() + ". You also discard an Energy Card.";
                PlayerHpBar.Value = active_Pokemon.ShowRemHP();
                discard.Add(player_Hand.PlayCard(x));
                player_Hand.RemoveFromHand(x);
                UpdateHandView();
                UpdateDiscardView();
            }
            else if (player_Hand.ShowName(x) == "Gust of Wind")
            {
                if (aibench.NumberOfCards() == 0)
                {
                    gameMessage.Text = "Your opponent does not have any Benched Pokémon.";
                }
                else
                {
                    PlayerEnd.Visible = false;
                    ChangeOpponentActivePokemon();
                    discard.Add(player_Hand.PlayCard(x));
                    player_Hand.RemoveFromHand(x);
                    UpdateHandView();
                    UpdateDiscardView();
                }
            }
            else if (player_Hand.ShowName(x) == "PlusPower")
            {
                gameMessage.Text = "You use PlusPower on " + active_Pokemon.ShowName() + ". Its attacks do 10 more damage till the end of this turn.";
                PlusPowerOn = true;
                discard.Add(player_Hand.PlayCard(x));
                player_Hand.RemoveFromHand(x);
                UpdateHandView();
                UpdateDiscardView();
            }
            else if (player_Hand.ShowName(x) == "Energy Removal")
            {
                if(ai_Active_Pokemon.EnergyLoadedCount() != 0)
                {
                    DiscardEnergyCardToAI();
                    discard.Add(player_Hand.PlayCard(x));
                    player_Hand.RemoveFromHand(x);
                    PlayerEnd.Visible = false;
                }
                else
                {
                    gameMessage.Text = "The Defending Pokémon does not have any Energy Cards loaded yet.";
                }
            }
            else if (player_Hand.ShowName(x) == "Energy Retrieval")
            {
                if(discard.CountEnergiesInDiscard() != 0 && player_Hand.NumberOfCards() > 0)
                {
                    gameMessage.Text = "You play Energy Retrieval, please choose up to two Energy Cards to discard to the Defending Pokémon.";
                    ChooseCardFromDiscard();
                    player_Hand.RemoveFromHand(x);
                    PlayerEnd.Visible = false;
                }
                else if(player_Hand.NumberOfCards() == 0)
                {
                    gameMessage.Text = "You can't play Energy Removal as you do not have any cards in your hand.";
                }
                else
                {
                    gameMessage.Text = "You can't play Energy Removal as you do not have any Energy Cards in your discard.";
                }
            }
            else if (player_Hand.ShowName(x) == "Switch")
            {
                if (bench.NumberOfCards() != 0)
                {
                    gameMessage.Text = "You play Switch. Please choose the Benched Pokémon to swap your Active one.";
                    PlayerEnd.Visible = false;
                    player_Hand.RemoveFromHand(x);
                    trainer_switch = true;
                }
                else
                {
                    gameMessage.Text = "The Defending Pokémon does not have any Energy Cards loaded yet.";
                }
            }
            else if (player_Hand.ShowName(x) == "Lass")
            {
                gameMessage.Text = "You play Lass. You can see your Opponents's hand now, also you show yours.";
                PlayerEnd.Visible = false;
                trainer_lass = true;
                Next2.Visible = true;
                Next2.Location = new Point(683, 957);
                player_Hand.RemoveFromHand(x);
                HideBackground();
                ShowAIHandView();
            }
        }

        private void ChangeOpponentActivePokemon()
        {
            PictureActive.Visible = false;
            ActiveEnergy1.Visible = false;
            ActiveEnergy2.Visible = false;
            ActiveEnergy3.Visible = false;
            ActiveEnergy4.Visible = false;
            ActiveEnergy5.Visible = false;
            DiscardBox.Visible = false;
            OpponentActive.Location = new Point(1319, 237);
            OActiveEnergy1.Visible = false;
            OActiveEnergy2.Visible = false;
            OActiveEnergy3.Visible = false;
            OActiveEnergy4.Visible = false;
            OActiveEnergy5.Visible = false;
            Deck.Visible = false;
            DeckSize.Visible = false;
            OpponentDeck.Visible = false;
            ODeckSize.Visible = false;
            HandIcon1.Visible = false;
            HandIcon2.Visible = false;
            HandIcon3.Visible = false;
            OHandNumber.Visible = false;
            PictureZoom.Visible = false;
            CardName.Visible = false;
            PlayerHpBar.Visible = false;
            MaxHp.Visible = false;
            RemHp.Visible = false;
            EnergyBox1.Visible = false;
            HpLabel1.Visible = false;
            OpCardName.Visible = false;
            OpponentHpBar.Visible = false;
            OMaxHp.Visible = false;
            EnergyBox2.Visible = false;
            PictureBench1.Visible = false;
            PictureBench2.Visible = false;
            PictureBench3.Visible = false;
            PictureBench4.Visible = false;
            PictureBench5.Visible = false;
            Bench1Energy1.Visible = false;
            Bench1Energy2.Visible = false;
            Bench1Energy3.Visible = false;
            Bench1Energy4.Visible = false;
            Bench1Energy5.Visible = false;
            Bench2Energy1.Visible = false;
            Bench2Energy2.Visible = false;
            Bench2Energy3.Visible = false;
            Bench2Energy4.Visible = false;
            Bench2Energy5.Visible = false;
            Bench3Energy1.Visible = false;
            Bench3Energy2.Visible = false;
            Bench3Energy3.Visible = false;
            Bench3Energy4.Visible = false;
            Bench3Energy5.Visible = false;
            Bench4Energy1.Visible = false;
            Bench4Energy2.Visible = false;
            Bench4Energy3.Visible = false;
            Bench4Energy4.Visible = false;
            Bench4Energy5.Visible = false;
            Bench5Energy1.Visible = false;
            Bench5Energy2.Visible = false;
            Bench5Energy3.Visible = false;
            Bench5Energy4.Visible = false;
            Bench5Energy5.Visible = false;

            OBench1Energy1.Visible = false;
            OBench1Energy2.Visible = false;
            OBench1Energy3.Visible = false;
            OBench1Energy4.Visible = false;
            OBench1Energy5.Visible = false;
            OBench2Energy1.Visible = false;
            OBench2Energy2.Visible = false;
            OBench2Energy3.Visible = false;
            OBench2Energy4.Visible = false;
            OBench2Energy5.Visible = false;
            OBench3Energy1.Visible = false;
            OBench3Energy2.Visible = false;
            OBench3Energy3.Visible = false;
            OBench3Energy4.Visible = false;
            OBench3Energy5.Visible = false;
            OBench4Energy1.Visible = false;
            OBench4Energy2.Visible = false;
            OBench4Energy3.Visible = false;
            OBench4Energy4.Visible = false;
            OBench4Energy5.Visible = false;
            OBench5Energy1.Visible = false;
            OBench5Energy2.Visible = false;
            OBench5Energy3.Visible = false;
            OBench5Energy4.Visible = false;
            OBench5Energy5.Visible = false;
            OpponentDiscardBox.Visible = false;
            OpponentBench1.Location = new Point(480, 407);
            OpponentBench2.Location = new Point(621, 407);
            OpponentBench3.Location = new Point(789, 407);
            OpponentBench4.Location = new Point(936, 407);
            OpponentBench5.Location = new Point(1089, 407);
            PlayerEnd.Visible = false;
            ai_Active_Pokemon_Retreat = true;
            gameMessage.Text = "Please choose the Pokémon with which you wish to swap for " + ai_Active_Pokemon.ShowName();
        }

        private void DiscardEnergyCardToAI()
        {
            PictureActive.Visible = false;
            ActiveEnergy1.Visible = false;
            ActiveEnergy2.Visible = false;
            ActiveEnergy3.Visible = false;
            ActiveEnergy4.Visible = false;
            ActiveEnergy5.Visible = false;
            DiscardBox.Visible = false;
            OpponentActive.Location = new Point(1319, 237);
            Deck.Visible = false;
            DeckSize.Visible = false;
            OpponentDeck.Visible = false;
            ODeckSize.Visible = false;
            HandIcon1.Visible = false;
            HandIcon2.Visible = false;
            HandIcon3.Visible = false;
            OHandNumber.Visible = false;
            PictureZoom.Visible = false;
            CardName.Visible = false;
            PlayerHpBar.Visible = false;
            MaxHp.Visible = false;
            RemHp.Visible = false;
            EnergyBox1.Visible = false;
            HpLabel1.Visible = false;
            OpCardName.Visible = false;
            OpponentHpBar.Visible = false;
            OMaxHp.Visible = false;
            EnergyBox2.Visible = false;
            PictureBench1.Visible = false;
            PictureBench2.Visible = false;
            PictureBench3.Visible = false;
            PictureBench4.Visible = false;
            PictureBench5.Visible = false;
            Bench1Energy1.Visible = false;
            Bench1Energy2.Visible = false;
            Bench1Energy3.Visible = false;
            Bench1Energy4.Visible = false;
            Bench1Energy5.Visible = false;
            Bench2Energy1.Visible = false;
            Bench2Energy2.Visible = false;
            Bench2Energy3.Visible = false;
            Bench2Energy4.Visible = false;
            Bench2Energy5.Visible = false;
            Bench3Energy1.Visible = false;
            Bench3Energy2.Visible = false;
            Bench3Energy3.Visible = false;
            Bench3Energy4.Visible = false;
            Bench3Energy5.Visible = false;
            Bench4Energy1.Visible = false;
            Bench4Energy2.Visible = false;
            Bench4Energy3.Visible = false;
            Bench4Energy4.Visible = false;
            Bench4Energy5.Visible = false;
            Bench5Energy1.Visible = false;
            Bench5Energy2.Visible = false;
            Bench5Energy3.Visible = false;
            Bench5Energy4.Visible = false;
            Bench5Energy5.Visible = false;

            OBench1Energy1.Visible = false;
            OBench1Energy2.Visible = false;
            OBench1Energy3.Visible = false;
            OBench1Energy4.Visible = false;
            OBench1Energy5.Visible = false;
            OBench2Energy1.Visible = false;
            OBench2Energy2.Visible = false;
            OBench2Energy3.Visible = false;
            OBench2Energy4.Visible = false;
            OBench2Energy5.Visible = false;
            OBench3Energy1.Visible = false;
            OBench3Energy2.Visible = false;
            OBench3Energy3.Visible = false;
            OBench3Energy4.Visible = false;
            OBench3Energy5.Visible = false;
            OBench4Energy1.Visible = false;
            OBench4Energy2.Visible = false;
            OBench4Energy3.Visible = false;
            OBench4Energy4.Visible = false;
            OBench4Energy5.Visible = false;
            OBench5Energy1.Visible = false;
            OBench5Energy2.Visible = false;
            OBench5Energy3.Visible = false;
            OBench5Energy4.Visible = false;
            OBench5Energy5.Visible = false;
            OpponentDiscardBox.Visible = false;
            OpponentBench1.Visible = false;
            OpponentBench2.Visible = false;
            OpponentBench3.Visible = false;
            OpponentBench4.Visible = false;
            OpponentBench5.Visible = false;
            PlayerEnd.Visible = false;
            aienergytodiscard = true;
            
            gameMessage.Text = "Please choose the Energy Card which you want to discard ";
        }

        private void ChooseCardFromDiscard()
        {
            PictureActive.Visible = false;
            ActiveEnergy1.Visible = false;
            ActiveEnergy2.Visible = false;
            ActiveEnergy3.Visible = false;
            ActiveEnergy4.Visible = false;
            ActiveEnergy5.Visible = false;
            DiscardBox.Visible = false;
            OpponentActive.Visible = false;
            Deck.Visible = false;
            CardName.Visible = false;
            DeckSize.Visible = false;
            OpponentDeck.Visible = false;
            ODeckSize.Visible = false;
            HandIcon1.Visible = false;
            HandIcon2.Visible = false;
            HandIcon3.Visible = false;
            OHandNumber.Visible = false;
            PictureZoom.Visible = false;
            CardName.Visible = false;
            PlayerHpBar.Visible = false;
            MaxHp.Visible = false;
            RemHp.Visible = false;
            EnergyBox1.Visible = false;
            HpLabel1.Visible = false;
            HpLabel2.Visible = false;
            OpCardName.Visible = false;
            OpponentHpBar.Visible = false;
            OMaxHp.Visible = false;
            EnergyBox2.Visible = false;
            PictureBench1.Visible = false;
            PictureBench2.Visible = false;
            PictureBench3.Visible = false;
            PictureBench4.Visible = false;
            PictureBench5.Visible = false;
            Bench1Energy1.Visible = false;
            Bench1Energy2.Visible = false;
            Bench1Energy3.Visible = false;
            Bench1Energy4.Visible = false;
            Bench1Energy5.Visible = false;
            Bench2Energy1.Visible = false;
            Bench2Energy2.Visible = false;
            Bench2Energy3.Visible = false;
            Bench2Energy4.Visible = false;
            Bench2Energy5.Visible = false;
            Bench3Energy1.Visible = false;
            Bench3Energy2.Visible = false;
            Bench3Energy3.Visible = false;
            Bench3Energy4.Visible = false;
            Bench3Energy5.Visible = false;
            Bench4Energy1.Visible = false;
            Bench4Energy2.Visible = false;
            Bench4Energy3.Visible = false;
            Bench4Energy4.Visible = false;
            Bench4Energy5.Visible = false;
            Bench5Energy1.Visible = false;
            Bench5Energy2.Visible = false;
            Bench5Energy3.Visible = false;
            Bench5Energy4.Visible = false;
            Bench5Energy5.Visible = false;

            OBench1Energy1.Visible = false;
            OBench1Energy2.Visible = false;
            OBench1Energy3.Visible = false;
            OBench1Energy4.Visible = false;
            OBench1Energy5.Visible = false;
            OBench2Energy1.Visible = false;
            OBench2Energy2.Visible = false;
            OBench2Energy3.Visible = false;
            OBench2Energy4.Visible = false;
            OBench2Energy5.Visible = false;
            OBench3Energy1.Visible = false;
            OBench3Energy2.Visible = false;
            OBench3Energy3.Visible = false;
            OBench3Energy4.Visible = false;
            OBench3Energy5.Visible = false;
            OBench4Energy1.Visible = false;
            OBench4Energy2.Visible = false;
            OBench4Energy3.Visible = false;
            OBench4Energy4.Visible = false;
            OBench4Energy5.Visible = false;
            OBench5Energy1.Visible = false;
            OBench5Energy2.Visible = false;
            OBench5Energy3.Visible = false;
            OBench5Energy4.Visible = false;
            OBench5Energy5.Visible = false;
            OpponentDiscardBox.Visible = false;
            OpponentBench1.Visible = false;
            OpponentBench2.Visible = false;
            OpponentBench3.Visible = false;
            OpponentBench4.Visible = false;
            OpponentBench5.Visible = false;
            PlayerEnd.Visible = false;
            aienergytodiscard = true;
            energy_retrieval = true;
            gameMessage.Text = "Please choose the Card from your hand which you want to discard ";
        }

        private void HideBackground()
        {
            PictureActive.Visible = false;
            ActiveEnergy1.Visible = false;
            ActiveEnergy2.Visible = false;
            ActiveEnergy3.Visible = false;
            ActiveEnergy4.Visible = false;
            ActiveEnergy5.Visible = false;
            DiscardBox.Visible = false;
            OpponentActive.Visible = false;
            Deck.Visible = false;
            CardName.Visible = false;
            DeckSize.Visible = false;
            OpponentDeck.Visible = false;
            OpponentZoom.Visible = false;
            ODeckSize.Visible = false;
            HandIcon1.Visible = false;
            HandIcon2.Visible = false;
            HandIcon3.Visible = false;
            OHandNumber.Visible = false;
            PictureZoom.Visible = false;
            CardName.Visible = false;
            PlayerHpBar.Visible = false;
            MaxHp.Visible = false;
            RemHp.Visible = false;
            EnergyBox1.Visible = false;
            HpLabel1.Visible = false;
            HpLabel2.Visible = false;
            OpCardName.Visible = false;
            OpponentHpBar.Visible = false;
            OMaxHp.Visible = false;
            EnergyBox2.Visible = false;
            PictureBench1.Visible = false;
            PictureBench2.Visible = false;
            PictureBench3.Visible = false;
            PictureBench4.Visible = false;
            PictureBench5.Visible = false;
            Bench1Energy1.Visible = false;
            Bench1Energy2.Visible = false;
            Bench1Energy3.Visible = false;
            Bench1Energy4.Visible = false;
            Bench1Energy5.Visible = false;
            Bench2Energy1.Visible = false;
            Bench2Energy2.Visible = false;
            Bench2Energy3.Visible = false;
            Bench2Energy4.Visible = false;
            Bench2Energy5.Visible = false;
            Bench3Energy1.Visible = false;
            Bench3Energy2.Visible = false;
            Bench3Energy3.Visible = false;
            Bench3Energy4.Visible = false;
            Bench3Energy5.Visible = false;
            Bench4Energy1.Visible = false;
            Bench4Energy2.Visible = false;
            Bench4Energy3.Visible = false;
            Bench4Energy4.Visible = false;
            Bench4Energy5.Visible = false;
            Bench5Energy1.Visible = false;
            Bench5Energy2.Visible = false;
            Bench5Energy3.Visible = false;
            Bench5Energy4.Visible = false;
            Bench5Energy5.Visible = false;

            OBench1Energy1.Visible = false;
            OBench1Energy2.Visible = false;
            OBench1Energy3.Visible = false;
            OBench1Energy4.Visible = false;
            OBench1Energy5.Visible = false;
            OBench2Energy1.Visible = false;
            OBench2Energy2.Visible = false;
            OBench2Energy3.Visible = false;
            OBench2Energy4.Visible = false;
            OBench2Energy5.Visible = false;
            OBench3Energy1.Visible = false;
            OBench3Energy2.Visible = false;
            OBench3Energy3.Visible = false;
            OBench3Energy4.Visible = false;
            OBench3Energy5.Visible = false;
            OBench4Energy1.Visible = false;
            OBench4Energy2.Visible = false;
            OBench4Energy3.Visible = false;
            OBench4Energy4.Visible = false;
            OBench4Energy5.Visible = false;
            OBench5Energy1.Visible = false;
            OBench5Energy2.Visible = false;
            OBench5Energy3.Visible = false;
            OBench5Energy4.Visible = false;
            OBench5Energy5.Visible = false;
            OpponentDiscardBox.Visible = false;
            OpponentBench1.Visible = false;
            OpponentBench2.Visible = false;
            OpponentBench3.Visible = false;
            OpponentBench4.Visible = false;
            OpponentBench5.Visible = false;
            PlayerEnd.Visible = false;
        }
        private void GetViewBackToNormal()
        {
            PictureActive.Visible = true;
            DiscardBox.Visible = true;
            OpponentActive.Location = new Point(789, 247);
            UpdateAIActivePokemonView();
            Deck.Visible = true;
            DeckSize.Visible = true;
            OpponentDeck.Visible = true;
            ODeckSize.Visible = true;
            HandIcon1.Visible = true;
            HandIcon2.Visible = true;
            HandIcon3.Visible = true;
            OHandNumber.Visible = true;
            PictureZoom.Visible = true;
            CardName.Visible = true;
            PlayerHpBar.Visible = true;
            MaxHp.Visible = true;
            RemHp.Visible = true;
            OpponentZoom.Visible = true;
            EnergyBox1.Visible = true;
            HpLabel1.Visible = true;
            OpCardName.Visible = true;
            OpponentHpBar.Visible = true;
            OMaxHp.Visible = true;
            EnergyBox2.Visible = true;
            HpLabel2.Visible = true;
            ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
            OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
            DeckSize.Text = "x" + player.NumberOfCards().ToString();
            UpdateHandView();
            UpdateActivePokemonView();
            UpdateBenchView();
            UpdateDiscardView();
            
            UpdateActivePokemonView();
            UpdateAIActivePokemonView();
            AIUpdateBenchView();
            UpdateAIDiscardView();
            
            OpponentDiscardBox.Visible = true;
            OpponentBench1.Location = new Point(789, 48);
            OpponentBench2.Location = new Point(938, 48);
            OpponentBench3.Location = new Point(633, 48);
            OpponentBench4.Location = new Point(1089, 48);
            OpponentBench5.Location = new Point(480, 48);
            PlayerEnd.Visible = true;
            ai_Active_Pokemon_Retreat = false;
           
        }

        private void AIPictureBench_Click(object sender, EventArgs e)
        {
            if (ai_Active_Pokemon_Retreat == true)
            {
                int num;
                Pokemon ActivePokemonTemp;
                PictureBox n = (PictureBox)sender;
                switch (n.Name)
                {
                    case "OpponentBench1":
                        num = 0;
                        gameMessage.Text = "You replaced " + ai_Active_Pokemon.ShowName() + " with " + aibench.ShowName(num) + ". Now " + aibench.ShowName(num) + " is the AI's Active Pokémon.";
                        ActivePokemonTemp = ai_Active_Pokemon.GetActivePokemon();
                        ai_Active_Pokemon.Become(aibench.PlayCard(num));
                        aibench.PutInto(num, ActivePokemonTemp);
                        break;
                    case "OpponentBench2":
                        num = 1;
                        gameMessage.Text = "You replaced " + ai_Active_Pokemon.ShowName() + " with " + aibench.ShowName(num) + ". Now " + aibench.ShowName(num) + " is the AI's Active Pokémon.";
                        ActivePokemonTemp = ai_Active_Pokemon.GetActivePokemon();
                        ai_Active_Pokemon.Become(aibench.PlayCard(num));
                        aibench.PutInto(num, ActivePokemonTemp);
                        break;
                    case "OpponentBench3":
                        num = 2;
                        gameMessage.Text = "You replaced " + ai_Active_Pokemon.ShowName() + " with " + aibench.ShowName(num) + ". Now " + aibench.ShowName(num) + " is the AI's Active Pokémon.";
                        ActivePokemonTemp = ai_Active_Pokemon.GetActivePokemon();
                        ai_Active_Pokemon.Become(aibench.PlayCard(num));
                        aibench.PutInto(num, ActivePokemonTemp);
                        break;
                    case "OpponentBench4":
                        num = 3;
                        gameMessage.Text = "You replaced " + ai_Active_Pokemon.ShowName() + " with " + aibench.ShowName(num) + ". Now " + aibench.ShowName(num) + " is the AI's Active Pokémon.";
                        ActivePokemonTemp = ai_Active_Pokemon.GetActivePokemon();
                        ai_Active_Pokemon.Become(aibench.PlayCard(num));
                        aibench.PutInto(num, ActivePokemonTemp);
                        break;
                    case "OpponentBenchh5":
                        num = 4;
                        gameMessage.Text = "You replaced " + ai_Active_Pokemon.ShowName() + " with " + aibench.ShowName(num) + ". Now " + aibench.ShowName(num) + " is the AI's Active Pokémon.";
                        ActivePokemonTemp = ai_Active_Pokemon.GetActivePokemon();
                        ai_Active_Pokemon.Become(aibench.PlayCard(num));
                        aibench.PutInto(num, ActivePokemonTemp);
                        break;
                    default:
                        break;
                }
                GetViewBackToNormal();
            }
        }
        private void Replace_Click(object sender, EventArgs e)
        {
            Replace.Visible = false;
            AIChoosesActivePokemon();
            gameMessage.Text = "Your opponent chooses now " + ai_Active_Pokemon.ShowName() + " as his Active Pokémon.";
            if (isYourTurn == true)
            {
                isYourTurn = false;
                PlayerEnd.Visible = true;
            }
            else
            {
                isOpponentsTurn = false;
                EndOpponentsTurn.Visible = true;
            }        
        }

        private void LureTest_Click(object sender, EventArgs e)
        {
            gameMessage.Text = "You play Lass. You can see your Opponents's hand now, also you show yours.";
            PlayerEnd.Visible = false;
            trainer_lass = true;
            Next2.Visible = true;
            Next2.Location = new Point(683, 957);

            HideBackground();
            ShowAIHandView();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            
            if (ai_Hand.NumOfBasicPokemon() > 0 && aibench.NumberOfCards() < 5)
            {
                _ticks++;
                if (_ticks == 20)
                {
                    timer1.Stop();
                    AIPlaysCards();
                    _ticks = 0;
                }
            }
            else if (ai_Hand.NumOfBasicEnergies() > 0 && aiPlayedEnergy == false)
            {
                _ticks++;
                if (_ticks == 20)
                {
                    timer1.Stop();
                    AIPlaysEnergyCards();
                    _ticks = 0;
                }
            }
            else if (ai_Active_Pokemon.CanPerformAttack(2) == true && aiPlayedAttack == false)
            {
                _ticks++;
                if (_ticks == 20)
                {
                    timer1.Stop();

                    //checking if the attack includes weakness modifier
                    if (active_Pokemon.ShowWeakness() == ai_Active_Pokemon.ShowEnergy())
                    {
                        hasWeakness = true;
                    }
                    AIAttacks(2);
                    aiPlayedAttack = true;
                    if (active_Pokemon.ShowRemHP() == 0)
                    {
                        EndOpponentsTurn.Visible = false;
                        active_Pokemon.ClearEverthingFromCard();
                        gameMessage.Text = active_Pokemon.ShowName() + " is Knocked out. Replace it by a Benched Pokémon.";
                        for (int i = 0; i < player_used.Count(); i++)
                        {
                            discard.Add(player_used.GetCard(i));
                        }
                        player_used.DiscardAll();

                        KnockedOut.Location = new Point(683, 957);
                        KnockedOut.Visible = true;
                        FlipCoin.Visible = false;
                        poisonedCheck = false;
                        confusedCheck = false;
                        paralyzedCheck = false;
                        burnedCheck = false;
                        asleepCheck = false;
                    }
                    _ticks = 0;
                }
            }
            else if (ai_Active_Pokemon.CanPerformAttack(1) == true && aiPlayedAttack == false)
            {
                _ticks++;
                if (_ticks == 20)
                {
                    timer1.Stop();

                    //checking if the attack includes weakness modifier
                    if(active_Pokemon.ShowWeakness() == ai_Active_Pokemon.ShowEnergy())
                    {
                        hasWeakness = true;
                    }
                    AIAttacks(1);
                    aiPlayedAttack = true;
                    _ticks = 0;
                }
            }
            else
            {
                _ticks++;
                if (_ticks == 25)
                {
                    
                    timer1.Stop();
                    _ticks = 0;
                    EndOpponentsTurn.Location = new Point(683, 957);
                    EndOpponentsTurn.Visible = true;                   
                    SoundPlayer sound = new SoundPlayer("..\\..\\Sounds\\proceed.wav");
                    sound.Play();
                }
                
            }            
            
        }

        private void Hand1_Click(object sender, EventArgs e)
        {
            if(energy_retrieval == true)
            {
                int num = 0;
                Pokemon ActivePokemonTemp;
                PictureBox n = (PictureBox)sender;
                switch (n.Name)
                {
                    case "Hand1":
                        num = 0;
                        break;
                    case "Hand2":
                        num = 1;
                        break;
                    case "Hand3":
                        num = 2;
                        break;
                    case "Hand4":
                        num = 3;
                        break;
                    case "Hand5":
                        num = 4;
                        break;
                    case "Hand6":
                        num = 5;
                        break;
                    case "Hand7":
                        num = 6;
                        break;
                    case "Hand8":
                        num = 7;
                        break;
                    case "Hand9":
                        num = 8;
                        break;
                    case "Hand10":
                        num = 9;
                        break;
                    default:
                        break;
                }

                gameMessage.Text = "You discarded " + player_Hand.ShowName(num) + ", now choose the energy cards from your discard.";
                ActivePokemonTemp = player_Hand.PlayCard(num);
                discard.Add(ActivePokemonTemp);
                player_Hand.RemoveFromHand(num);

                energy_retrieval = false;
                energy_retrieval_2 = true;
                CreateDiscardView();
            }
            else if (energy_retrieval_2 == true)
            {
                int num = 0;
                Pokemon ActivePokemonTemp;
                PictureBox n = (PictureBox)sender;
                switch (n.Name)
                {
                    case "Hand1":
                        num = 0;
                        break;
                    case "Hand2":
                        num = 1;
                        break;
                    case "Hand3":
                        num = 2;
                        break;
                    case "Hand4":
                        num = 3;
                        break;
                    case "Hand5":
                        num = 4;
                        break;
                    case "Hand6":
                        num = 5;
                        break;
                    case "Hand7":
                        num = 6;
                        break;
                    case "Hand8":
                        num = 7;
                        break;
                    case "Hand9":
                        num = 8;
                        break;
                    case "Hand10":
                        num = 9;
                        break;
                    default:
                        break;
                }

                if (discard.ShowType(num) == "energy" && discard.CountEnergiesInDiscard() > 1)
                {
                    gameMessage.Text = "You put " + discard.ShowName(num) + " in your hand. Choose another Energy now.";
                    ActivePokemonTemp = discard.GetCard(num);
                    player_Hand.DrawCard(ActivePokemonTemp);
                    discard.RemoveFromDiscard(num);
                    energy_retrieval_2 = false;
                    energy_retrieval_3 = true;
                    CreateDiscardView();
                }
                else if (discard.ShowType(num) == "energy" && discard.CountEnergiesInDiscard() == 1)
                {
                    gameMessage.Text = "You put " + discard.ShowName(num) + " in your hand.";
                    ActivePokemonTemp = discard.GetCard(num);
                    player_Hand.DrawCard(ActivePokemonTemp);
                    discard.RemoveFromDiscard(num);
                    energy_retrieval_2 = false;
                    UpdateHandView();
                    GetViewBackToNormal();
                    UpdateDiscardView();
                    PlayerEnd.Visible = true;
                }
                else
                {
                    gameMessage.Text = "Please choose an Energy Card.";
                }
            }
            else if (energy_retrieval_3 == true)
            {
                int num = 0;
                Pokemon ActivePokemonTemp;
                PictureBox n = (PictureBox)sender;
                switch (n.Name)
                {
                    case "Hand1":
                        num = 0;
                        break;
                    case "Hand2":
                        num = 1;
                        break;
                    case "Hand3":
                        num = 2;
                        break;
                    case "Hand4":
                        num = 3;
                        break;
                    case "Hand5":
                        num = 4;
                        break;
                    case "Hand6":
                        num = 5;
                        break;
                    case "Hand7":
                        num = 6;
                        break;
                    case "Hand8":
                        num = 7;
                        break;
                    case "Hand9":
                        num = 8;
                        break;
                    case "Hand10":
                        num = 9;
                        break;
                    default:
                        break;
                }

                if (discard.ShowType(num) == "energy")
                {
                    gameMessage.Text = "You put " + discard.ShowName(num) + " in your hand.";
                    ActivePokemonTemp = discard.GetCard(num);
                    player_Hand.DrawCard(ActivePokemonTemp);
                    discard.RemoveFromDiscard(num);
                    energy_retrieval_3 = false;
                    UpdateHandView();
                    UpdateDiscardView();
                    GetViewBackToNormal();
                    PlayerEnd.Visible = true;
                }
                else
                {
                    gameMessage.Text = "Please choose an Energy Card.";
                }
            }
        }
    }
}
