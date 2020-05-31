using Pokemon.Card;
using Pokemon.Game_Zone;
using Pokemon.Players;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Reflection;
using System.Runtime.Remoting.Channels;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class Form1 : Form
    {

        Deck player = new Deck();
        Hand player_Hand = new Hand();
        Bench bench = new Bench();
        Discard discard = new Discard();
        Prizes prize = new Prizes();
        Active active_Pokemon = new Active();
        Used player_used = new Used();
        bool playedEnergy = false;
        bool playedAttack = false;
        bool active_Pokemon_Retreat = false;
        int energydiscard = 0;
        bool energytodiscard = false;
        public Form1()
        {
            InitializeComponent();   
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            
            player.AddToDeck(new Pokemon(69, "Charmander", "basic", 50, 10, 'f', 'w', 'n', 1, "Obviously prefers hot places. If it gets caught in the rain, steam is said to spout from the tip of its tail", "..\\..\\Img\\BSPainted\\BS_046.jpg", new Attack("Scratch", "", 10, new EnergyCost(1, 0, 0, 0, 0, 0, 0)), new List<char>()));
            player.AddToDeck(new Pokemon(98, "Fire Energy", "energy", "..\\..\\Img\\BS\\BS_098.jpg"));
            player.AddToDeck(new Pokemon(24, "Charmeleon", "second", "Charmander", 80, 80, 'f', 'w', 'n', 1, "It lashes about with its tail to knock down its foe. It then tears up the fallen opponent with sharp claws.", "..\\..\\Img\\BS\\BS_024.jpg", new Attack("Slash", "", 30, new EnergyCost(3, 0, 0, 0, 0, 0, 0)), new Attack("Flamethrower", "Discard 1 Fire Energy card attached to Charmeleon in order to use this attack.", 50, new EnergyCost(1, 0, 2, 0, 0, 0, 0)), new List<char>()));
            player.AddToDeck(new Pokemon(102, "Water Energy", "energy", "..\\..\\Img\\BS\\BS_102.jpg"));
            player.AddToDeck(new Pokemon(54, "Chikorita", "basic", 50, 50, 'g', 'f', 'n', 1, "Its pleasantly aromatic leaves have the ability to check the humidity and temperature.", "..\\..\\Img\\N1\\N1_054.jpg", new Attack("Growl","If the Defending Pokémon attacks Chikorita during your opponent\'s next turn, any damage done to Chikorita is reduced by 10 (before applying Weakness and Resistance).(Benching or evolving either Pokémon ends this effect.)", 0, new EnergyCost(1, 0, 0, 0, 0, 0, 0)), new Attack("Razor Leaf", "", 20, new EnergyCost(1, 0, 0, 1, 0, 0, 0)), new List<char>()));

            player.Shuffle();
            player_Hand.DrawCard(player.DrawCard());
            player_Hand.DrawCard(player.DrawCard());

            DeckSize.Text = player.NumberOfCards().ToString();

            OpponentHpBar.Value = 60;
            OpponentDeck.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            Deck.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");

            EnergyBox2.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Psychic.gif");
            

            OMaxHp.Text = OpponentHpBar.Value.ToString();
            //playBackgroundMusic();
            PictureZoom.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            OpponentZoom.Image = Image.FromFile("..\\..\\Img\\BSPainted\\BS_029.jpg");

            gameMessage.Text = "Hello Tony, what are you thinking to do next?";


            gameMessage.Text = "It is your turn now, you should draw a card";

            UpdateHandView();
            UpdateBenchView();
            UpdateActivePokemonView();
            UpdateDiscardView();

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
                default:
                    break;
            }

            ZoomInfo(num);

            int bench_size = bench.NumberOfCards();

            if(player_Hand.ShowType(num) == "basic")
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
                    (RightClickDiscard.Items[0] as ToolStripMenuItem).DropDownItems.Add(discard.ShowName(i)).Click += new EventHandler(PerformAttack);
                }
            }
           
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
        public void Draw_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer("..\\..\\Sounds\\draw.wav");
            simpleSound.Play();
            Draw.Visible = false;
            YourHand.Visible = true;
            Check.Visible = true;
            Attack.Visible = true;
            Power.Visible = true;
            Retreat.Visible = true;
            Done.Visible = true;
            player_Hand.DrawCard(player.DrawCard());
            DeckSize.Text = player.NumberOfCards().ToString();

            UpdateHandView();
          
            gameMessage.Text = "You drew a card from your Deck to your Hand";

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

        public void UpdateHandView()
        {
            
            int hand_size = player_Hand.NumberOfCards();

            Hand6.Visible = false;
            Hand7.Visible = false;
            Hand8.Visible = false;
            Hand9.Visible = false;
            Hand10.Visible = false;
            Hand11.Visible = false;
            Hand12.Visible = false;
            Hand13.Visible = false;
            Hand14.Visible = false;
            Hand15.Visible = false;
            Hand16.Visible = false;
            Hand17.Visible = false;
    
            switch (hand_size)
            {
                case 0:
                    Hand1.Visible = false;
                    Hand2.Visible = false;
                    Hand3.Visible = false;
                    Hand4.Visible = false;
                    Hand5.Visible = false;
                    break;
                case 1:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Visible = false;
                    Hand3.Visible = false;
                    Hand4.Visible = false;
                    Hand5.Visible = false;
                    break;
                case 2:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Visible = false;
                    Hand4.Visible = false;
                    Hand5.Visible = false;
                    break;
                case 3:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Visible = false;
                    Hand5.Visible = false;
                    break;
                case 4:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));
                    Hand5.Visible = false;
                    break;
                case 5:
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(player_Hand.ShowCard(4));
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
            PictureBench4.Visible = false;
            PictureBench5.Visible = false;
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
                    Bench1Energy1.Visible = true;
                    Bench1Energy2.Visible = true;
                    Bench1Energy3.Visible = true;
                    Bench1Energy4.Visible = true;
                    Bench1Energy5.Visible = true;

                    PictureBench2.Visible = true;
                    PictureBench2.Image = Image.FromFile(bench.ShowCard(1));
                    PictureBench3.Visible = true;
                    PictureBench3.Image = Image.FromFile(bench.ShowCard(2));
                    PictureBench4.Visible = false;
                    PictureBench5.Visible = false;
                    break;
                case 4:
                    PictureBench1.Visible = true;
                    PictureBench1.Image = Image.FromFile(bench.ShowCard(0));
                    Bench1Energy1.Visible = true;
                    Bench1Energy2.Visible = true;
                    Bench1Energy3.Visible = true;
                    Bench1Energy4.Visible = true;
                    Bench1Energy5.Visible = true;

                    PictureBench2.Visible = true;
                    PictureBench2.Image = Image.FromFile(bench.ShowCard(1));
                    PictureBench3.Visible = true;
                    PictureBench3.Image = Image.FromFile(bench.ShowCard(2));
                    PictureBench4.Visible = true;
                    PictureBench4.Image = Image.FromFile(bench.ShowCard(3));
                    PictureBench5.Visible = false;
                    break;
                case 5:
                    PictureBench1.Visible = true;
                    PictureBench1.Image = Image.FromFile(bench.ShowCard(0));
                    Bench1Energy1.Visible = true;
                    Bench1Energy2.Visible = true;
                    Bench1Energy3.Visible = true;
                    Bench1Energy4.Visible = true;
                    Bench1Energy5.Visible = true;

                    PictureBench2.Visible = true;
                    PictureBench2.Image = Image.FromFile(bench.ShowCard(1));
                    PictureBench3.Visible = true;
                    PictureBench3.Image = Image.FromFile(bench.ShowCard(2));
                    PictureBench4.Visible = true;
                    PictureBench4.Image = Image.FromFile(bench.ShowCard(3));
                    PictureBench5.Visible = true;
                    PictureBench5.Image = Image.FromFile(bench.ShowCard(4));
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

        private void Hand1_Click(object sender, EventArgs e)
        {
          
        }

        private void Play_Click(object sender, EventArgs e)
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
            if (player_Hand.ShowType(num) == "basic")
            {
               
                if (bench.NumberOfCards() < 6)
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
            else if (player_Hand.ShowType(num) == "second")
            {
                //Nothing unless the user clicks on the next options so let us just leave this like this.
            }

        }

        //Takes place when user clicks on the chosen attack from the menu
        void PerformAttack(object sender, EventArgs e)
        {

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
                            gameMessage.Text = "You performed " + active_Pokemon.ShowAttackName(1) + " on the Defending Pokémon";
                            playedAttack = true;
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
                            gameMessage.Text = "You performed " + active_Pokemon.ShowAttackName(2) + " on the Defending Pokémon";
                            playedAttack = true;
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
                            gameMessage.Text = "You performed " + active_Pokemon.ShowAttackName(3) + " on the Defending Pokémon";
                            playedAttack = true;
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
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if (item != null)
                {
                    int index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);
                    if (playedEnergy == false)
                    {
                        if (index <= (bench.NumberOfCards()-1))
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
                            //playedEnergy = true;

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
                        }
                    }
                    else
                    {
                        gameMessage.Text = "You already played an energy this turn.";
                    }
                };
            } 
            
            else if (player_Hand.ShowType(num) == "second")
            {
                ToolStripMenuItem item = sender as ToolStripMenuItem;
                if (item != null)
                {
                    int index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);

                    if (index <= (bench.NumberOfCards() - 1))
                    {
                        if (player_Hand.ShowFirstStage(num) == bench.ShowName(index))
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
                        else
                        {
                            gameMessage.Text = "You can\'t place " + player_Hand.ShowName(num) + " on " + bench.ShowName(index) + ". Find its right evolved form.";
                        }
                    }
                    else
                    {
                        if (player_Hand.ShowFirstStage(num) == active_Pokemon.ShowName())
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
                        else
                        {
                            gameMessage.Text = "You can\'t place " + player_Hand.ShowName(num) + " on " + active_Pokemon.ShowName() + ". Find its right evolved form.";
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
            DeckSize.Text = player.NumberOfCards().ToString();

            UpdateHandView();
            gameMessage.Text = "You drew a card from your Deck to your Hand";
        }

        private void playAsAttackerToolStripMenuItem_Click(object sender, EventArgs e)
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

            if(active_Pokemon.ShowName() == "null")
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

        private void retreatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(bench.Count() != 0) {
                energydiscard = active_Pokemon.ShowRetreatCost();
                if (energydiscard <= active_Pokemon.EnergyLoadedCount())
                {
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
                    gameMessage.Text = "You replaced " + active_Pokemon.ShowName() + " with " + bench.ShowName(num) + ". Now " + bench.ShowName(num) + " is your new Active Pokémon.";
                    Pokemon ActivePokemonTemp = active_Pokemon.GetActivePokemon();

                    active_Pokemon.Become(bench.PlayCard(num));
                    bench.PutInto(num, ActivePokemonTemp);
                    active_Pokemon_Retreat = false;
                    UpdateActivePokemonView();
                    UpdateBenchView();
                }
                else
                {
                    gameMessage.Text = "You must first discard as many energy cards attached to " + active_Pokemon.ShowName() + " as the energy cost it requires in order to retreat.";
                }
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

    }
}
