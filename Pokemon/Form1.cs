using Pokemon.Card;
using Pokemon.GameStage;
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
        Prize prize = new Prize();
        Prize aiprize = new Prize();
        Active active_Pokemon = new Active();
        Active ai_Active_Pokemon = new Active();
        Used player_used = new Used();
        Used ai_used = new Used();

        Stage stage = new Stage();
        //Yellow box behind cards when hovered on   hintBox for hand, hintBox2 for Active Pokemon, hintBox3 for Benched Pokemon
        PictureBox hintBox;
        PictureBox hintBox3;
        Panel panel1 = new Panel();

        int mainButtonPosX = 1419;
        int mainButtonPosY = 862;
        //Phase Restrictions
        bool isPreGameTurn = true;
        bool youStartTurn = false;
        bool aiStartsTurn = false;
        bool isYourFirstTurn = false;
        bool isAIFirstTurn = false;
        bool isYourTurn = false;
        bool isAITurn = false;

        //Pokémon Conditions
        bool PlayerConfused = false;
        bool AIConfused = false;
        bool PlayerPoisoned = false;      
        bool AIPoisoned = false;
        bool PlayerAsleep = false;
        bool AIAsleep = false;
        bool PlayerParalyzed = false;
        bool AIParalyzed = false;
        bool PlayerBurned = false;
        bool AIBurned = false;
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
        bool playerLoadEnergyto = false;
        bool opponentLoadEnergyto = false;
        bool energy_retrieval = false;
        bool energy_retrieval_2 = false;
        bool energy_retrieval_3 = false;
        bool trainer_switch = false;
        bool trainer_lass = false;

        private int _ticks;
        string coin_choice = "";
        public Form1()
        {
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            WindowState = FormWindowState.Maximized;

            InitializeComponent();
        }

        public void Form1_Load(object sender, EventArgs e)
        {
            player.PlayWith("Brushfire", active_Pokemon, ai_Active_Pokemon, bench, aibench, player, ai, player_Hand, ai_Hand, discard, ai_discard, prize, aiprize, player_used, ai_used);
            ai.PlayWith("Zap", ai_Active_Pokemon, active_Pokemon, aibench, bench, ai, player, ai_Hand, player_Hand, ai_discard, discard, aiprize, prize, ai_used, player_used);

            player.Shuffle();
            ai.Shuffle();

            DeckSize.Text = "x" + player.NumberOfCards().ToString();
            ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
            OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
            Deck1PopUp.Visible = false;
            Deck2PopUp.Visible = false;
            PrizePopUp1.Visible = false;
            PrizePopUp2.Visible = false;

            OpponentDeck.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            OpponentDeck.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);

            Deck.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");

            RightClickMenu.Enabled = false;

            //playBackgroundMusic();
            PictureZoom.Visible = false;
            OpponentZoom.Visible = false;

            CoinRolling.Location = new Point(887, 2);
            HandIcon1.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            HandIcon2.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            HandIcon3.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            PrizeBox1.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            PrizeBox2.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            PrizeBox2.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
            PrizePlayerNumber.Text = prize.TotalNumber().ToString();
            PrizeOpponentNumber.Text = aiprize.TotalNumber().ToString();
            hintBox = new PictureBox { Name = "hintBox" };
            Controls.Add(hintBox);
            hintBox3 = new PictureBox { Name = "hintBox3" };
            pictureBox4.Controls.Add(hintBox3);
            hintBox.Visible = false;
            hintBox3.Visible = false;
            gameMessage.Visible = false;
            TimerBeginning.Start();
            
            UpdateHandView();
            UpdateBenchView();
            UpdateAIBenchView();
            UpdateActivePokemonView();
            UpdateAIActivePokemonView();
            UpdateDiscardView();
            UpdateAIDiscardView();
            UpdatePlayerUsedView();
            UpdateOpponentUsedView();
        }
        private void ExitButton_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        ///////GAME SEQUENCES//////
        //Beginning of the game
        private void TimerBeginning_Tick(object sender, EventArgs e)
        {
            _ticks++;
            SoundPlayer simpleSound = new SoundPlayer("..\\..\\Sounds\\draw.wav");
            Play.Visible = false;
            PlayPokémonToBenchBeginning.Visible = false;
            if (_ticks == 10)
            {
                player.Shuffle();
                player.Shuffle();
                gameMessage.Visible = true;
                gameMessage.Text = "Let's start by drawing 7 cards.";
            }
            else if (_ticks == 12)
            {
                player_Hand.DrawCard(player.DrawCard());
                ai_Hand.DrawCard(ai.DrawCard());
                simpleSound.Play();
                DeckSize.Text = "x" + player.NumberOfCards().ToString();
                ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
                OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
                UpdateHandView();
            }
            else if (_ticks == 14)
            {
                player_Hand.DrawCard(player.DrawCard());
                ai_Hand.DrawCard(ai.DrawCard());
                simpleSound.Play();
                DeckSize.Text = "x" + player.NumberOfCards().ToString();
                ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
                OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
                UpdateHandView();
            }
            else if (_ticks == 16)
            {
                player_Hand.DrawCard(player.DrawCard());
                ai_Hand.DrawCard(ai.DrawCard());
                simpleSound.Play();
                DeckSize.Text = "x" + player.NumberOfCards().ToString();
                ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
                OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
                UpdateHandView();
            }
            else if (_ticks == 18)
            {
                player_Hand.DrawCard(player.DrawCard());
                ai_Hand.DrawCard(ai.DrawCard());
                simpleSound.Play();
                DeckSize.Text = "x" + player.NumberOfCards().ToString();
                ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
                OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
                UpdateHandView();
            }
            else if (_ticks == 20)
            {
                player_Hand.DrawCard(player.DrawCard());
                ai_Hand.DrawCard(ai.DrawCard());
                simpleSound.Play();
                DeckSize.Text = "x" + player.NumberOfCards().ToString();
                ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
                OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
                UpdateHandView();
            }
            else if (_ticks == 22)
            {
                player_Hand.DrawCard(player.DrawCard());
                ai_Hand.DrawCard(ai.DrawCard());
                simpleSound.Play();
                DeckSize.Text = "x" + player.NumberOfCards().ToString();
                ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
                OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
                UpdateHandView();
            }
            else if (_ticks == 24)
            {
                player_Hand.DrawCard(player.DrawCard());
                ai_Hand.DrawCard(ai.DrawCard());
                simpleSound.Play();
                DeckSize.Text = "x" + player.NumberOfCards().ToString();
                ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
                OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
                UpdateHandView();
                TimerBeginning.Stop();
                _ticks = 0;
                gameMessage.Text = "Choose a Pokémon to be your Active Pokémon.";
                CheckingDraw();
            }
        }
        //Checking if it is Mulligan or not
        private void CheckingDraw()
        {
            if (player_Hand.NumOfBasicPokemon() == 0 && ai_Hand.NumOfBasicPokemon() != 0)
            {
                gameMessage.Text = "As you do not have any Basic Pokémon, you should perform Mulligan and draw another 7 cards.";
                StartCheck.Visible = false;
                Mulligan.Visible = true;
                Mulligan.Location = new Point(mainButtonPosX, mainButtonPosY);
                FlipCoin.Visible = false;
            }
            else if (player_Hand.NumOfBasicPokemon() == 0 && ai_Hand.NumOfBasicPokemon() == 0)
            {
                gameMessage.Text = "Apparently you and your opponent do not have any Basic Pokémons, so both of you should perform Mulligan.";
                StartCheck.Visible = false;
                Mulligan2.Location = new Point(mainButtonPosX, mainButtonPosY);
                Mulligan2.Visible = true;
                FlipCoin.Visible = false;
            }
            else if (player_Hand.NumOfBasicPokemon() != 0 && ai_Hand.NumOfBasicPokemon() == 0)
            {
                gameMessage.Text = "Your opponent does not have any Basic Pokémons, so he should perform Mulligan.";
                StartCheck.Visible = false;
                Mulligan3.Location = new Point(mainButtonPosX, mainButtonPosY);
                Mulligan3.Visible = true;
                FlipCoin.Visible = false;
            }
            else
            {
                RightClickMenu.Enabled = true;               
                StartCheck.Visible = false;
                Done.Visible = false;
                FlipCoin.Visible = false;
            }
        }
        //Clicking on Play as Active from the Strip Menu Chooses the Active Pokémon from Hand directly to Active Zone
        private void PlayAsActiveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PlayAsActiveToolStripMenuItem.Visible = false;
            
            string CardPlace = "Hand";        
            int num = 0;
            for(int i=0; i< 61; i++)
            {
                if(RightClickMenu.SourceControl.Name.ToString() == (CardPlace + (i+1).ToString()))
                {
                    num = i;
                    break;
                }
            }
            if (player_Hand.ShowType(num) == "basic")
            {
                active_Pokemon.Become(player_Hand.ThisCard(num));
                player_Hand.RemoveFromHand(num);
                UpdateActivePokemonView();
                UpdateHandView();
                AIPlaysCards();
                AIChoosesActivePokemon();
                gameMessage.Text = "Choose Pokémon to start on your Bench.";
                StartCheck.Visible = true;
                StartCheck.Location = new Point(mainButtonPosX, mainButtonPosY);
                PlayPokémonToBenchBeginning.Visible = true;
            }     
        }
        private void PlayPokémonToBenchBeginning_Click(object sender, EventArgs e)
        {           
            int num = GetHandBoxIndex();
            if (player_Hand.ShowType(num) == "basic")
            {
                if (bench.NumberOfCards() < 5)
                { 
                    bench.Add(player_Hand.ThisCard(num));
                    player_Hand.RemoveFromHand(num);
                    UpdateBenchView();
                    UpdateHandView();
                    if(player_Hand.NumOfBasicPokemon() == 0)
                    {
                        StartCheck.PerformClick();
                    }
                }
                else
                {
                    StartCheck.PerformClick();
                }
            }
        }
        private void StartCheck_Click(object sender, EventArgs e)
        {
            prize.AddXPrizes(6, player);
            aiprize.AddXPrizes(6, ai);
            PrizePlayerNumber.Text = prize.TotalNumber().ToString();
            PrizeOpponentNumber.Text = aiprize.TotalNumber().ToString();
            PlayPokémonToBenchBeginning.Visible = false;
            Play.Visible = true;
            Heads.Visible = true;
            Tails.Visible = true;
            Heads.Location = new Point(662, 615);
            Tails.Location = new Point(1045, 615);
            gameMessage.Text = "Flip the Coin to see who starts.";
            StartCheck.Visible = false;
            DisablePlayerActions();
        }
        //Actions happening during player's turn with timing to prevent non-necessary clicking, set-up
        private void YourTurnTimer_Tick(object sender, EventArgs e)
        {
            _ticks++;
            SoundPlayer drawSound = new SoundPlayer("..\\..\\Sounds\\draw.wav");
            if (_ticks == 10)
            {
                gameMessage.BackColor = Color.Green;
                gameMessage.Visible = true;
                gameMessage.Text = "Your turn.";
                SoundPlayer sound = new SoundPlayer("..\\..\\Sounds\\spawncard.wav");
                sound.Play();
            }
            else if (_ticks == 20)
            {
                gameMessage.Location = new Point(303, 488);
                gameMessage.Size = new Size(1292, 48);
                gameMessage.BringToFront();
            }
            else if (_ticks == 30)
            {
                YourTurnTimer.Stop();
                _ticks = 0;
                PlayerEnd.Location = new Point(mainButtonPosX, mainButtonPosY);
                PlayerEnd.Visible = true;
                player_Hand.DrawCard(player.DrawCard());
                DeckSize.Text = "x" + player.NumberOfCards().ToString();
                gameMessage.Location = new Point(303, 498);
                gameMessage.BackColor = Color.FromArgb(255, 192, 128);
                gameMessage.Size = new Size(1292, 31);
                gameMessage.Text = "You draw a card.";
                UpdateHandView();
                EnablePlayerActions();
                drawSound.Play();
                AvatarPanel1.BackColor = Color.Yellow;
                AvatarPanel2.BackColor = Color.Black;
            }
        }
        //Ai turn actions with timing =>
        private void AIFirstTurnTimer_Tick(object sender, EventArgs e)
        {
            _ticks++;
            if (_ticks == 10)
            {
                gameMessage.BackColor = Color.Red;
                gameMessage.Visible = true;
                gameMessage.Text = "Opponent's turn.";
                SoundPlayer sound = new SoundPlayer("..\\..\\Sounds\\turn.wav");
                sound.Play();
            }
            else if (_ticks == 20)
            {
                gameMessage.Location = new Point(303, 488);
                gameMessage.Size = new Size(1292, 48);
                gameMessage.BringToFront();
            }
            else if (_ticks == 30)
            {
                AIFirstTurnTimer.Stop();
                _ticks = 0;
                CoinResult.Visible = false;
                ai_Hand.DrawCard(ai.DrawCard());
                ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
                OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
                gameMessage.Location = new Point(303, 498);
                gameMessage.BackColor = Color.FromArgb(255, 192, 128);
                gameMessage.Size = new Size(1292, 31);
                MessageBoxToNormal("");
                UpdateHandView();
                aiPlayedEnergy = false;
                aiPlayedAttack = false;
                hasWeakness = false;
                DrawSound();
                AvatarPanel1.BackColor = Color.Black;
                AvatarPanel2.BackColor = Color.Yellow;
                AITurnTimer.Start();
            }
        }
        private void Timer1_Tick(object sender, EventArgs e)
        {
            if (ai_Hand.NumOfBasicPokemon() > 0 && aibench.NumberOfCards() < 5)
            {
                _ticks++;
                if (_ticks == 15)
                {
                    AITurnTimer.Stop();
                    _ticks = 0;
                    AIPlaysCards();
                }
            }
            else if (ai_Hand.NumOfBasicEnergies() > 0 && aiPlayedEnergy == false)
            {
                _ticks++;
                if (_ticks == 15)
                {
                    AITurnTimer.Stop();
                    AIPlaysEnergyCards();
                    _ticks = 0;
                }
            }
            else if (ai_Active_Pokemon.NumberOfAttacks() == 2 && aiPlayedAttack == false)
            {
                if (ai_Active_Pokemon.CanPerformAttack(2) == true && ShouldPlayAttack() == true)
                {
                    _ticks++;
                    if (_ticks == 20)
                    {
                        AITurnTimer.Stop();

                        //checking if the attack includes weakness modifier
                        if (active_Pokemon.ShowWeakness() == ai_Active_Pokemon.ShowEnergy())
                        {
                            hasWeakness = true;
                        }
                        AIAttacks(2);
                        aiPlayedAttack = true;
                        CheckIfKnockedOut();
                        _ticks = 0;
                        AITurnTimer.Start();
                    }
                }
                else if (ai_Active_Pokemon.CanPerformAttack(1) == true && ShouldPlayAttack() == true)
                {
                    _ticks++;
                    if (_ticks == 20)
                    {
                        AITurnTimer.Stop();

                        //checking if the attack includes weakness modifier
                        if (active_Pokemon.ShowWeakness() == ai_Active_Pokemon.ShowEnergy())
                        {
                            hasWeakness = true;
                        }
                        AIAttacks(1);
                        aiPlayedAttack = true;
                        CheckIfKnockedOut();
                        _ticks = 0;
                        AITurnTimer.Start();
                    }
                }
                else
                {
                    aiPlayedAttack = true;
                    AITurnTimer.Start();
                }
            }
            else if (ai_Active_Pokemon.NumberOfAttacks() == 1 && aiPlayedAttack == false)
            {
                if (ai_Active_Pokemon.CanPerformAttack(1) == true && ShouldPlayAttack() == true)
                {
                    _ticks++;
                    if (_ticks == 20)
                    {
                        AITurnTimer.Stop();
                        _ticks = 0;
                        //checking if the attack includes weakness modifier
                        if (active_Pokemon.ShowWeakness() == ai_Active_Pokemon.ShowEnergy())
                        {
                            hasWeakness = true;
                        }
                        AIAttacks(1);
                        aiPlayedAttack = true;
                        CheckIfKnockedOut();
                        AITurnTimer.Start();
                    }
                }
                else
                {
                    aiPlayedAttack = true;
                    AITurnTimer.Start();
                }
            }
            else
            {
                _ticks++;
                if (_ticks == 25)
                {
                    AITurnTimer.Stop();
                    _ticks = 0;
                    SoundPlayer sound = new SoundPlayer("..\\..\\Sounds\\done.wav");
                    sound.Play();
                    EndAITurn();
                }
            }
        }



        ////////////AI PLAYING CARDS///////////////////////
        private void AIPlaysCards()
        {
            if (isPreGameTurn == true)
            {
                while (ai_Hand.NumOfBasicPokemon() > 0 && aibench.NumberOfCards() < 6)
                {
                    for (int i = 0; i < ai_Hand.NumberOfCards(); i++)
                    {
                        if (ai_Hand.ShowType(i) == "basic")
                        {
                            aibench.Add(ai_Hand.ThisCard(i));
                            ai_Hand.RemoveFromHand(i);
                            UpdateAIBenchView();
                            OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
                        }
                    }

                }
            }
            else if (aiStartsTurn == true)
            {
                if (ai_Hand.NumOfBasicPokemon() == 0)
                {
                    gameMessage.Text = "Your opponent is thinking.. ";
                    AITurnTimer.Start();

                }
                while (ai_Hand.NumOfBasicPokemon() > 0 && aibench.NumberOfCards() < 6)
                {
                    for (int i = 0; i < ai_Hand.NumberOfCards(); i++)
                    {
                        if (ai_Hand.ShowType(i) == "basic")
                        {
                            gameMessage.Text = "Your opponent plays " + ai_Hand.ShowName(i).ToLower() + " from his hand to his bench.";
                            aibench.Add(ai_Hand.ThisCard(i));
                            ai_Hand.RemoveFromHand(i);
                            UpdateAIBenchView();
                            AITurnTimer.Start();
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
                        aibench.Add(ai_Hand.ThisCard(i));
                        ai_Hand.RemoveFromHand(i);
                        UpdateAIBenchView();
                        OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
                        AITurnTimer.Start();
                    }
                }
            }
        }
        private void AIChoosesActivePokemon()
        {
            Random random = new Random();
            int i = random.Next(0, aibench.NumberOfCards());

            ai_Active_Pokemon.Become(aibench.PlayCard(i));

            UpdateAIActivePokemonView();
            aibench.RemoveFromBench(i);
            UpdateAIBenchView();
            OpponentZoom.Image = Image.FromFile(ai_Active_Pokemon.ShowImage());
        }
        private void AIPlaysEnergyCards()
        {
            gameMessage.Text = "To which Pokémon should this energy be loaded to? (make the best decision to challenge yourself!)";
            int index = ai_Hand.ReturnFirstIndexMatchingEnergywithActivePokémon(ai_Active_Pokemon);
            ai_used.GetCardFromHand(index, ai_Hand);
            UpdateOpponentUsedView();
            UpdateOpponentHandAndDeckView();
            opponentLoadEnergyto = true;
        }
        private void AIAttacks(int index)
        {
            if (ai_Active_Pokemon.ShowName() == "Abra")
            {
                if (ai_Active_Pokemon.ShowAttackName(index) == "Psychock")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    UpdateActivePokemonView();

                    if (PlayerConfused != true)
                    {
                        AIFlipsCoin("confusion", 1);
                        if (PlayerConfused == true)
                        {
                            gameMessage.Text = "The AI performs Psychock on " + active_Pokemon.ShowName() + " dealing 10 damage and leaving you CONFUSED.";
                        }
                    }
                    else
                    {
                        gameMessage.Text = "The AI performs Psychock on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                    }
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Drowzee")
            {
                if (ai_Active_Pokemon.ShowAttackName(index) == "Pound")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    UpdateActivePokemonView();
                    gameMessage.Text = "The AI performs Pound on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
                else if (ai_Active_Pokemon.ShowAttackName(index) == "Confuse Ray")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    UpdateActivePokemonView();
                    AIFlipsCoin("confusion", 1);
                    if (PlayerConfused == true)
                    {
                        gameMessage.Text = "The AI performs Confuse Ray on " + active_Pokemon.ShowName() + " dealing 10 damage and leaving you CONFUSED.";
                    }
                    else
                    {
                        gameMessage.Text = "The AI performs Confuse Ray on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                    }
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Gastly")
            {
                if (ai_Active_Pokemon.ShowAttackName(index) == "Sleeping Gas")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    UpdateActivePokemonView();
                    gameMessage.Text = "The AI performs Sleeping Gas on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
                else if (ai_Active_Pokemon.ShowAttackName(index) == "Destiny Bond")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    UpdateActivePokemonView();
                    gameMessage.Text = "The AI performs Destiny Bond on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Jynx")
            {
                if (ai_Active_Pokemon.ShowAttackName(index) == "Doubleslap")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    UpdateActivePokemonView();
                    gameMessage.Text = "The AI performs Doubleslap on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Magnemite")
            {
                if (ai_Active_Pokemon.ShowAttackName(index) == "Thunder Wave")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    UpdateActivePokemonView();
                    gameMessage.Text = "The AI performs Thunder Wave on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
                else if (ai_Active_Pokemon.ShowAttackName(index) == "Self Destruct")
                {
                    active_Pokemon.DealDamage(40, hasWeakness, PlusPowerDamage);
                    active_Pokemon.ReceiveDamage(40);
                    UpdateActivePokemonView();
                    gameMessage.Text = "The AI performs Self Destruct on " + active_Pokemon.ShowName() + " and deals 40 damage.";
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Mewtwo")
            {
                if (ai_Active_Pokemon.ShowAttackName(index) == "Psychic")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    UpdateActivePokemonView();
                    gameMessage.Text = "The AI performs Psychic on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
            }
            else if (ai_Active_Pokemon.ShowName() == "Pikachu")
            {
                if (ai_Active_Pokemon.ShowAttackName(index) == "Gnaw")
                {
                    active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    UpdateActivePokemonView();
                    gameMessage.Text = "The AI performs Gnaw on " + active_Pokemon.ShowName() + " and deals 10 damage.";
                }
                else if (ai_Active_Pokemon.ShowAttackName(index) == "Thunder Jolt")
                {
                    active_Pokemon.DealDamage(30, hasWeakness, PlusPowerDamage);
                    UpdateActivePokemonView();
                    ai_Active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                    gameMessage.Text = "The AI performs Thunder Jolt on " + active_Pokemon.ShowName() + " and deals 30 damage and 10 to itself.";
                }
            }
        }


        //TAKES PLACE AT BEGINNING WHEN CLICKING SOME OPTIONAL BUTTONS
        private void Mulligan_Click(object sender, EventArgs e)
        {
            player.ShuffleCardFromHandIntoDeck(player_Hand.ThisCard(0));
            player.ShuffleCardFromHandIntoDeck(player_Hand.ThisCard(1));
            player.ShuffleCardFromHandIntoDeck(player_Hand.ThisCard(2));
            player.ShuffleCardFromHandIntoDeck(player_Hand.ThisCard(3));
            player.ShuffleCardFromHandIntoDeck(player_Hand.ThisCard(4));
            player.ShuffleCardFromHandIntoDeck(player_Hand.ThisCard(5));
            player.ShuffleCardFromHandIntoDeck(player_Hand.ThisCard(6));
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
                Mulligan.Location = new Point(mainButtonPosX, mainButtonPosY);
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
            else if (player_Hand.NumOfBasicPokemon() == 0 && ai_Hand.NumOfBasicPokemon() == 0)
            {
                gameMessage.Text = "Apparently you and your opponent do not have any Basic Pokémons, so both of you should perform Mulligan.";

                StartCheck.Visible = false;
                Mulligan2.Location = new Point(mainButtonPosX, mainButtonPosY);
                Mulligan2.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
            else if (player_Hand.NumOfBasicPokemon() != 0 && ai_Hand.NumOfBasicPokemon() == 0)
            {
                gameMessage.Text = "Your opponent does not have any Basic Pokémons, so he should perform Mulligan.";

                StartCheck.Visible = false;
                Mulligan3.Location = new Point(mainButtonPosX, mainButtonPosY);
                Mulligan3.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
        }
        private void Mulligan2_Click(object sender, EventArgs e)
        {
            player.ShuffleCardFromHandIntoDeck(player_Hand.ThisCard(0));
            player.ShuffleCardFromHandIntoDeck(player_Hand.ThisCard(1));
            player.ShuffleCardFromHandIntoDeck(player_Hand.ThisCard(2));
            player.ShuffleCardFromHandIntoDeck(player_Hand.ThisCard(3));
            player.ShuffleCardFromHandIntoDeck(player_Hand.ThisCard(4));
            player.ShuffleCardFromHandIntoDeck(player_Hand.ThisCard(5));
            player.ShuffleCardFromHandIntoDeck(player_Hand.ThisCard(6));
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);
            player_Hand.RemoveFromHand(0);

            ai.ShuffleCardFromHandIntoDeck(ai_Hand.ThisCard(0));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.ThisCard(1));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.ThisCard(2));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.ThisCard(3));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.ThisCard(4));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.ThisCard(5));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.ThisCard(6));
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
                Mulligan.Location = new Point(mainButtonPosX, mainButtonPosY);
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
            else if (player_Hand.NumOfBasicPokemon() == 0 && ai_Hand.NumOfBasicPokemon() == 0)
            {
                gameMessage.Text = "Apparently you and your opponent do not have any Basic Pokémons, so both of you should perform Mulligan.";

                StartCheck.Visible = false;
                Mulligan2.Location = new Point(mainButtonPosX, mainButtonPosY);
                Mulligan2.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
            else if (player_Hand.NumOfBasicPokemon() != 0 && ai_Hand.NumOfBasicPokemon() == 0)
            {
                gameMessage.Text = "Your opponent does not have any Basic Pokémons, so he should perform Mulligan.";

                StartCheck.Visible = false;
                Mulligan3.Location = new Point(mainButtonPosX, mainButtonPosY);
                Mulligan3.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
        }
        private void Mulligan3_Click(object sender, EventArgs e)
        {
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.ThisCard(0));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.ThisCard(1));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.ThisCard(2));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.ThisCard(3));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.ThisCard(4));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.ThisCard(5));
            ai.ShuffleCardFromHandIntoDeck(ai_Hand.ThisCard(6));
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
                Mulligan.Location = new Point(mainButtonPosX, mainButtonPosY);
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
            else if (player_Hand.NumOfBasicPokemon() == 0 && ai_Hand.NumOfBasicPokemon() == 0)
            {
                gameMessage.Text = "Apparently you and your opponent do not have any Basic Pokémons, so both of you should perform Mulligan.";

                StartCheck.Visible = false;
                Mulligan2.Location = new Point(mainButtonPosX, mainButtonPosY);
                Mulligan2.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
            else if (player_Hand.NumOfBasicPokemon() != 0 && ai_Hand.NumOfBasicPokemon() == 0)
            {
                gameMessage.Text = "Your opponent does not have any Basic Pokémons, so he should perform Mulligan.";

                StartCheck.Visible = false;
                Mulligan3.Location = new Point(mainButtonPosX, mainButtonPosY);
                Mulligan3.Visible = true;
                FlipCoin.Visible = false;
                DisablePlayerActions();
            }
        }
        private void Heads_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer("..\\..\\Sounds\\flipcoin.wav");
            simpleSound.Play();
            gameMessage.Visible = false;
            Heads.Visible = false;
            Tails.Visible = false;
            coin_choice = "Heads";
            CoinResult.Visible = true;
            CoinBeginningTimer.Start();            
        }
        private void Tails_Click(object sender, EventArgs e)
        {
            SoundPlayer simpleSound = new SoundPlayer("..\\..\\Sounds\\flipcoin.wav");
            simpleSound.Play();
            gameMessage.Visible = false;
            Heads.Visible = false;
            Tails.Visible = false;
            coin_choice = "Tails";
            CoinResult.Visible = true;
            CoinBeginningTimer.Start();
        }
        private void CoinBeginningTimer_Tick(object sender, EventArgs e)
        {
            _ticks++;
            Random random = new Random();
            int result = random.Next(1, 51);
            if (_ticks == 2)
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\Wizards_Silver_Chansey_Coin.png");
            }
            else if(_ticks == 4)
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\tails.gif");
            }
            else if (_ticks == 6)
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\Wizards_Silver_Chansey_Coin.png");
            }
            else if (_ticks == 8)
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\tails.gif");
            }
            else if (_ticks == 10)
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\Wizards_Silver_Chansey_Coin.png");
            }
            else if (_ticks == 12)
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\tails.gif");
            }
            else if (_ticks == 14)
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\Wizards_Silver_Chansey_Coin.png");
            }
            else if (_ticks == 16)
            {
                if (result >= 1 && result < 26)
                {
                    CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\Wizards_Silver_Chansey_Coin.png");
                }
                else
                {
                    CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\tails.gif");
                }
            }
            else if(_ticks == 30)
            {
                CoinBeginningTimer.Stop();
                _ticks = 0;
                if (coin_choice == "Heads")
                {
                    if (result >= 1 && result < 26)
                    {
                        youStartTurn = true;
                        YourTurnTimer.Start();
                    }
                    else
                    {
                        aiStartsTurn = true;
                        AIFirstTurnTimer.Start();
                    }
                }
                else
                {
                    if (result >= 1 && result < 26)
                    {
                        aiStartsTurn = true;
                        AIFirstTurnTimer.Start();
                    }
                    else
                    {
                        youStartTurn = true;
                        YourTurnTimer.Start();
                    }
                }
                isPreGameTurn = false;
                CoinResult.Visible = false;
                UpdateAIActivePokemonView();
                UpdateAIBenchView();
            }
        }



        ////////HOVERING ON SOMETHING////////////
        private void HoverOn_Click(object sender, EventArgs e)
        {
            int num = 0;
            PictureBox n = (PictureBox)sender;
            PictureZoom.Location = new Point(n.Location.X - 90, n.Location.Y - 450);
            PictureZoom.Visible = true;

            Controls.Add(hintBox);
            hintBox.Size = new Size(n.Size.Width + 4, n.Size.Height + 4);
            hintBox.Location = new Point(n.Location.X - 2, n.Location.Y - 2);
            hintBox.BackColor = Color.Yellow;
            hintBox.Visible = true;
            PictureZoom.BringToFront();

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
        }
        private void HoverOff_Click(object sender, EventArgs e)
        {
            PictureZoom.Visible = false;
            OpponentZoom.Visible = false;
            Deck1PopUp.Visible = false;
            Deck2PopUp.Visible = false;
            PrizePopUp1.Visible = false;
            PrizePopUp2.Visible = false;
            hintBox.Visible = false;
            PictureActivePanel.BackColor = Color.Transparent;
            hintBox3.Visible = false;
        }
        private void HoverOnBench_Click(object sender, EventArgs e)
        {
            int num = 0;
            PictureBox n = (PictureBox)sender;
            PictureZoom.Location = new Point(n.Location.X - 90, n.Location.Y - 450);
            PictureZoom.Visible = true;
            hintBox3 = new PictureBox { Name = "hintBox3" };
            pictureBox4.Controls.Add(hintBox3);
            hintBox3.Size = new Size(n.Size.Width + 4, n.Size.Height + 4);
            hintBox3.Location = new Point(n.Location.X - 560, n.Location.Y - 736);
            hintBox3.BackColor = Color.Yellow;
            hintBox3.Visible = true;

            PictureZoom.BringToFront();
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
            ZoomBenchInfo(num);
        }
        private void HoverOnAI_Bench_Click(object sender, EventArgs e)
        {
            if(isPreGameTurn != true)
            {
                int num = 0;
                PictureBox n = (PictureBox)sender;
                OpponentZoom.Location = new Point(n.Location.X - 90, n.Location.Y + 150);
                OpponentZoom.Visible = true;
                OpponentZoom.BringToFront();
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
            }       
        }
        private void HoverOnActive_Click(object sender, EventArgs e)
        {
            if (active_Pokemon.ShowName() != "null")
            {
                PictureZoom.Image = Image.FromFile(active_Pokemon.ShowImage());
                PictureZoom.Location = new Point(PictureActivePanel.Location.X + 200, PictureActivePanel.Location.Y + 50);
                PictureZoom.Visible = true;
                PictureActivePanel.BackColor = Color.Yellow;
           
                PictureZoom.BringToFront();

                if (isYourTurn == true)
                {
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
            }
            else
            {
                ActivePokemonZoomAllInvisible();
            }
        }
        private void HoverOnAI_Active_Click(object sender, EventArgs e)
        {
            if(isPreGameTurn != true)
            {
                if (ai_Active_Pokemon.ShowName() != "null")
                {
                    PictureBox n = (PictureBox)sender;
                    OpponentZoom.Image = Image.FromFile(ai_Active_Pokemon.ShowImage());
                    OpponentZoom.Location = new Point(n.Location.X - 90, n.Location.Y + 150);
                    OpponentZoom.Visible = true;
                    OpponentZoom.BringToFront();
                }
                else
                {
                    AIActivePokemonZoomAllInvisible();
                }
            }       
        }
        private void Deck_MouseHover(object sender, EventArgs e)
        {
            Deck1PopUp.Text = "Your deck - " + player.NumberOfCards().ToString() + " card (s)";
            Deck1PopUp.Visible = true;
        }
        private void OpponentDeck_MouseHover(object sender, EventArgs e)
        {
            Deck2PopUp.Text = "Opponent's deck - " + ai.NumberOfCards().ToString() + " card (s)";
            Deck2PopUp.Visible = true;
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
        private void PrizeBox1_MouseHover(object sender, EventArgs e)
        {
            PrizePopUp1.Visible = true;
        }
        private void PrizeBox2_MouseHover(object sender, EventArgs e)
        {
            PrizePopUp2.Visible = true;
        }
        private void Button_MouseHover(object sender, EventArgs e)
        {
            Button n = (Button)sender;
            n.BackColor = Color.Gold;
        }
        private void Button_MouseLeave(object sender, EventArgs e)
        {
            Button n = (Button)sender;
            if (n.Name == "Heads" || n.Name == "Tails" || n.Name == "FlipCoin")
            {
                n.BackColor = Color.DimGray;
            }
            else
            {
                n.BackColor = Color.FromArgb(0, 192, 0);
            }
        }


        private void ShowDiscardCard(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;
            
            if (item != null)
            {
                int index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);
                PictureZoom.Image = Image.FromFile(discard.ShowCard(index));              
            };
        }
        private void ShowAIDiscardCard(object sender, EventArgs e)
        {
            ToolStripMenuItem item = sender as ToolStripMenuItem;

            if (item != null)
            {

                int index = (item.OwnerItem as ToolStripMenuItem).DropDownItems.IndexOf(item);
                OpponentZoom.Image = Image.FromFile(ai_discard.ShowCard(index));
            };
        }
        public void ActivePokemonZoomAllInvisible()
        {
            PictureZoom.Visible = false;
        }

        public void AIActivePokemonZoomAllInvisible()
        {
            OpponentZoom.Visible = false;
        }

        
        


        //UPDATING VIEWS
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

            if(active_Pokemon.ShowHP() == active_Pokemon.ShowRemHP())
            {
                ActiveDamage.Visible = false;
            }
            else
            {
                ActiveDamage.Text = (active_Pokemon.ShowHP() - active_Pokemon.ShowRemHP()).ToString();
                ActiveDamage.Visible = true;
            }
        }
        public void UpdateAIActivePokemonView()
        {
            if (ai_Active_Pokemon.ShowName() != "null")
            {
                if(isPreGameTurn != true)
                {
                    OpponentActive.Image = Image.FromFile(ai_Active_Pokemon.ShowImage());
                }
                else
                {
                    OpponentActive.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
                }
                OpponentActive.Visible = true;
                OpponentActive.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);

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
                aiStatus.Visible = false;
                OpponentActive.Visible = false;
                OActiveEnergy1.Visible = false;
                OActiveEnergy2.Visible = false;
                OActiveEnergy3.Visible = false;
                OActiveEnergy4.Visible = false;
                OActiveEnergy5.Visible = false;
            }

            if (ai_Active_Pokemon.ShowHP() == ai_Active_Pokemon.ShowRemHP())
            {
                OpponentDamage.Visible = false;
            }
            else
            {
                OpponentDamage.Text = (ai_Active_Pokemon.ShowHP() - ai_Active_Pokemon.ShowRemHP()).ToString();
                OpponentDamage.Visible = true;
            }

            if(AIConfused == true)
            {
                aiStatus.Visible = true;
                aiStatus.Image = Image.FromFile("..\\..\\Img\\Status\\confusion.png");
            }
            else
            {
                aiStatus.Visible = false;
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
                    Hand1.Location = new Point(384, 933);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    
                    break;

                case 2:
                    Hand1.Location = new Point(384, 933);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 933);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
           
                    break;
                case 3:
                    Hand1.Location = new Point(384, 933);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 933);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 933);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));

                    break;
                case 4:
                    Hand1.Location = new Point(384, 933);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 933);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 933);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 933);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));

                    break;
                case 5:
                    Hand1.Location = new Point(384, 933);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 933);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 933);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 933);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 933);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(player_Hand.ShowCard(4));
                    break;

                case 6:
                    Hand1.Location = new Point(384, 933);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 933);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 933);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 933);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 933);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(player_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 933);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(player_Hand.ShowCard(5));
                    break;

                case 7:
                    Hand1.Location = new Point(384, 933);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 933);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 933);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 933);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 933);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(player_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 933);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(player_Hand.ShowCard(5));
                    Hand7.Location = new Point(911, 933);
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(player_Hand.ShowCard(6));
                    break;

                case 8:
                    Hand1.Location = new Point(384, 933);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 933);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 933);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 933);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 933);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(player_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 933);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(player_Hand.ShowCard(5));
                    Hand7.Location = new Point(911, 933);
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(player_Hand.ShowCard(6));
                    Hand8.Location = new Point(999, 933);
                    Hand8.Visible = true;
                    Hand8.Image = Image.FromFile(player_Hand.ShowCard(7));
                    break;

                case 9:
                    Hand1.Location = new Point(384, 933);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 933);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 933);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 933);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 933);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(player_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 933);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(player_Hand.ShowCard(5));
                    Hand7.Location = new Point(911, 933);
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(player_Hand.ShowCard(6));
                    Hand8.Location = new Point(999, 933);
                    Hand8.Visible = true;
                    Hand8.Image = Image.FromFile(player_Hand.ShowCard(7));
                    Hand9.Location = new Point(1087, 933);
                    Hand9.Visible = true;
                    Hand9.Image = Image.FromFile(player_Hand.ShowCard(8));
                    break;

                case 10:
                    Hand1.Location = new Point(384, 933);
                    Hand1.Visible = true;
                    Hand1.Image = Image.FromFile(player_Hand.ShowCard(0));
                    Hand2.Location = new Point(471, 933);
                    Hand2.Visible = true;
                    Hand2.Image = Image.FromFile(player_Hand.ShowCard(1));
                    Hand3.Location = new Point(559, 933);
                    Hand3.Visible = true;
                    Hand3.Image = Image.FromFile(player_Hand.ShowCard(2));
                    Hand4.Location = new Point(647, 933);
                    Hand4.Visible = true;
                    Hand4.Image = Image.FromFile(player_Hand.ShowCard(3));
                    Hand5.Location = new Point(735, 933);
                    Hand5.Visible = true;
                    Hand5.Image = Image.FromFile(player_Hand.ShowCard(4));
                    Hand6.Location = new Point(823, 933);
                    Hand6.Visible = true;
                    Hand6.Image = Image.FromFile(player_Hand.ShowCard(5));
                    Hand7.Location = new Point(911, 933);
                    Hand7.Visible = true;
                    Hand7.Image = Image.FromFile(player_Hand.ShowCard(6));
                    Hand8.Location = new Point(999, 933);
                    Hand8.Visible = true;
                    Hand8.Image = Image.FromFile(player_Hand.ShowCard(7));
                    Hand9.Location = new Point(1087, 933);
                    Hand9.Visible = true;
                    Hand9.Image = Image.FromFile(player_Hand.ShowCard(8));
                    Hand10.Location = new Point(1175, 933);
                    Hand10.Visible = true;
                    Hand10.Image = Image.FromFile(player_Hand.ShowCard(9));
                    break;

            }
        }
        public void UpdateOpponentHandAndDeckView()
        {
            ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
            OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
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
        public void UpdateAIBenchView()
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
                    OpponentBench1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
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
                    OpponentBench1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
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
                    OpponentBench2.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);

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
                    OpponentBench1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
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
                    OpponentBench2.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);

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
                    OpponentBench3.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
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
                    OpponentBench1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
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
                    OpponentBench2.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);

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
                    OpponentBench3.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
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
                    OpponentBench4.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
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
                    OpponentBench1.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
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
                    OpponentBench2.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);

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
                    OpponentBench3.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
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
                    OpponentBench4.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
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
                    OpponentBench5.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
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
            if (isPreGameTurn == true)
            {
                OpponentBench1.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
                OpponentBench2.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
                OpponentBench3.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
                OpponentBench4.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
                OpponentBench5.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            }
        }
        public void UpdateDiscardView()
        {
            if (discard.TotalNumber() == 0)
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
                OpponentDiscardBox.Image.RotateFlip(RotateFlipType.Rotate180FlipNone);
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
        public void UpdatePlayerUsedView()
        {
            if(player_used.Count() != 0)
            {
                PlayerUsedBox.Visible = true;
                PlayerUsedBox.Image = Image.FromFile(player_used.ShowCard(0));
            }
            else
            {
                PlayerUsedBox.Visible = false;
            }          
        }
        public void UpdateOpponentUsedView()
        {
            if (ai_used.Count() != 0)
            {
                OpponentUsedBox.Visible = true;
                OpponentUsedBox.Image = Image.FromFile(ai_used.ShowCard(0));
            }
            else
            {
                OpponentUsedBox.Visible = false;
            }
        }

        public void ZoomInfo(int num)
        {
            if (player_Hand.ShowType(num) == "trainer" || player_Hand.ShowType(num) == "energy" || player_Hand.ShowType(num) == "supporter")
            {
                PictureZoom.Image = Image.FromFile(player_Hand.ShowCard(num));
            }
            else
            {
                PictureZoom.Image = Image.FromFile(player_Hand.ShowCard(num));
            }
        }

        public void ZoomBenchInfo(int num)
        {
            PictureZoom.Image = Image.FromFile(bench.ShowCard(num));            
        }
        private void Play_Click(object sender, EventArgs e)
        {
            if (isYourFirstTurn == true)
            {
                int num = GetHandBoxIndex();

                if (player_Hand.ShowType(num) == "basic")
                {
                    if (bench.NumberOfCards() < 5)
                    {
                        gameMessage.Text = "You played " + player_Hand.ShowName(num) + " from your hand to your bench";
                        bench.Add(player_Hand.ThisCard(num));
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

                else if (player_Hand.ShowType(num) == "energy" && playedEnergy == false)
                {
                    player_used.GetCardFromHand(num, player_Hand);
                    UpdateHandView();
                    UpdatePlayerUsedView();
                    playerLoadEnergyto = true;
                    PlayerEnd.Visible = false;
                    DisablePlayerActions();
                    gameMessage.Text = "Please select the Active or Benched Pokémon to attach the Energy to.";
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
                        bench.Add(player_Hand.ThisCard(num));
                        player_Hand.RemoveFromHand(num);
                        UpdateBenchView();
                        UpdateHandView();
                    }

                    else
                    {
                        gameMessage.Text = "Your bench is full!";
                    }
                }
                else if (player_Hand.ShowType(num) == "energy" && playedEnergy == false)
                {
                    player_used.GetCardFromHand(num, player_Hand);
                    UpdateHandView();
                    UpdatePlayerUsedView();
                    playerLoadEnergyto = true;
                    PlayerEnd.Visible = false;
                    DisablePlayerActions();
                    gameMessage.Text = "Please select the Active or Benched Pokémon to attach the Energy to.";
                }
                else if (player_Hand.ShowType(num) == "trainer" && player_Hand.ShowImpact(num) == "global")
                {
                    PlayingTrainerCardGlobal(num);
                }
                else if (player_Hand.ShowType(num) == "trainer" && player_Hand.ShowImpact(num) == "directed")
                {
                    player_used.GetCardFromHand(num, player_Hand);
                    UpdateHandView();
                    UpdatePlayerUsedView();
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
                            CheckIfKnockedOut();
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
                            CheckIfKnockedOut();
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
                            CheckIfKnockedOut();
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
                                player_used.Add(player_Hand.ThisCard(num));
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
                                player_used.Add(player_Hand.ThisCard(num));
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
                else if(isYourFirstTurn == true || youStartTurn == true)
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
                                bench.PutInto(index, player_Hand.ThisCard(num));
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
                                active_Pokemon.Become(player_Hand.ThisCard(num));
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
                    gameMessage.Text = active_Pokemon.ShowName() + " is your new Active Pokémon.";
                    StartCheck.Visible = true;
                    StartCheck.Location = new Point(mainButtonPosX, mainButtonPosY);
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
                    if(isAITurn == true)
                    {
                        EndOpponentsTurn.Visible = true;
                    }
                    else
                    {
                        PlayerEnd.Visible = true;
                    }
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
                    gameMessage.Visible = true;
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
                    gameMessage.Visible = true;
                    gameMessage.Text = "You do not have enough energies attached to " + active_Pokemon.ShowName() + " yet in order to retreat it";
                }
            }
            else
            {
                gameMessage.Visible = true;
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
            else if(playerLoadEnergyto == true)
            {
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
                bench.AttachCardFromUsed(num, player_used);
                UpdatePlayerUsedView();
                UpdateBenchView();
                PlayerEnd.Visible = true;
                EnablePlayerActions();
                playerLoadEnergyto = false;
                playedEnergy = true;
                gameMessage.Visible = false;
            }
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
            else if (opponentLoadEnergyto == true)
            {
                PictureBox n = (PictureBox)sender;
                int num = 0;
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
                aibench.AttachCardFromUsed(num, ai_used);
                UpdateOpponentUsedView();
                UpdateAIBenchView();
                opponentLoadEnergyto = false;
                aiPlayedEnergy = true;
                gameMessage.Visible = false;
                AITurnTimer.Start();
            }
        }
        private void PictureActive_Click(object sender, EventArgs e)
        {
            if (playerLoadEnergyto == true)
            {               
                active_Pokemon.AttachCardFromUsed(player_used);
                UpdatePlayerUsedView();
                UpdateActivePokemonView();
                PlayerEnd.Visible = true;
                EnablePlayerActions();
                playerLoadEnergyto = false;
                playedEnergy = true;
                gameMessage.Visible = false;
            }
        }
        private void OpponentActive_Click(object sender, EventArgs e)
        {
            if (opponentLoadEnergyto == true)
            {
                ai_Active_Pokemon.AttachCardFromUsed(ai_used);
                UpdateOpponentUsedView();
                UpdateAIActivePokemonView();
                opponentLoadEnergyto = false;
                aiPlayedEnergy = true;
                gameMessage.Visible = false;
                AITurnTimer.Start();
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

                active_Pokemon.DiscardAttachedEnergyClickedOn(x, discard);

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

        



        
        private void Draw2_Click(object sender, EventArgs e)
        {
            CoinResult.Visible = false;

            PlayerEnd.Location = new Point(mainButtonPosX, mainButtonPosY);
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
            DrawSound();
            _ticks = 0;
            AITurnTimer.Start();
            
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
                        ai_discard.Add(ai_Hand.ThisCard(i));
                        ai_Hand.RemoveFromHand(i);
                    }
                }

                //discard all the trainer cards from player's hand
                for (int i = player_Hand.NumberOfCards() - 1; i >= 0; i--)
                {
                    if (player_Hand.ShowType(i) == "trainer")
                    {
                        discard.Add(ai_Hand.ThisCard(i));
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

        private void EndAITurn()
        {
            if (aiStartsTurn == true)
            {
                aiStartsTurn = false;
                isYourFirstTurn = true;
            }
            else if (isAIFirstTurn == true)
            {
                isAIFirstTurn = false;
                isYourTurn = true;
            }
            else
            {
                isYourTurn = true;
            }
            isAITurn = false;
            aibench.ChangeCanEvolveStatusToTrue();
            ai_Active_Pokemon.ChangeCanEvolveStatusToTrue();
            playedEnergy = false;
            playedAttack = false;

            if (AIPoisoned == true)
            {
                ai_Active_Pokemon.ReceiveDamage(10);
                UpdateAIActivePokemonView();
                gameMessage.Text = "As your opponent is Poisoned, it receives 10 damage after his turn.";
                if (ai_Active_Pokemon.ShowRemHP() == 0)
                {
                    isAITurn = true;
                    isYourTurn = false;
                    Draw2.Visible = false;
                    KnockedOut.Location = new Point(683, 957);
                    KnockedOut.Visible = true;
                }
                else
                {
                    YourTurnTimer.Start();
                }
            }
            else if (AIBurned == true)
            {
                ai_Active_Pokemon.ReceiveDamage(20);
                UpdateAIActivePokemonView();
                gameMessage.Text = "As your opponent is Burned, it receives 20 damage after his turn.";
                if (ai_Active_Pokemon.ShowRemHP() == 0)
                {
                    isAITurn = true;
                    isYourTurn = false;
                    Draw2.Visible = false;
                    KnockedOut.Location = new Point(683, 957);
                    KnockedOut.Visible = true;
                }
                else
                {
                    YourTurnTimer.Start();
                }
            }
            else if (AIParalyzed == true)
            {
                gameMessage.Text = "Your opponent is no longer Paralyzed after the end of his turn.";
                AIParalyzed = false;
                YourTurnTimer.Start();
            }
            else{
                YourTurnTimer.Start();
            }
        }
        private void AISwitchTurn()
        {
            if (youStartTurn == true)
            {
                youStartTurn = false;
                isAIFirstTurn = true;
            }
            else if (isYourFirstTurn == true)
            {
                isYourFirstTurn = false;
                isAITurn = true;
            }
            else
            {
                isAITurn = true;
            }
            isYourTurn = false;
            bench.ChangeCanEvolveStatusToTrue();
            active_Pokemon.ChangeCanEvolveStatusToTrue();
            PlayerEnd.Visible = false;
            AvatarPanel1.BackColor = Color.Black;
            AvatarPanel2.BackColor = Color.Yellow;
            PlusPowerOn = false;
            MessageBoxAnnouncement("Opponent's turn");
        }

        private void CheckStage()
        {
            if (AIPoisoned == true)
            {
                ai_Active_Pokemon.ReceiveDamage(10);
                UpdateAIActivePokemonView();
                gameMessage.Text = "As your opponent is Poisoned, it receives 10 damage after your turn.";
                if (ai_Active_Pokemon.ShowRemHP() == 0)
                {
                    isYourTurn = true;
                    isAITurn = false;
                    Next1.Visible = false;
                    KnockedOut.Location = new Point(683, 957);
                    KnockedOut.Visible = true;
                }
            }
            else if (AIBurned == true)
            {
                ai_Active_Pokemon.ReceiveDamage(20);
                UpdateAIActivePokemonView();
                gameMessage.Text = "As your opponent is Burned, it receives 20 damage after your turn.";
                if (ai_Active_Pokemon.ShowRemHP() == 0)
                {
                    isYourTurn = true;
                    isAITurn = false;
                    Next1.Visible = false;
                    KnockedOut.Location = new Point(683, 957);
                    KnockedOut.Visible = true;
                }
            }
        }

        
        private void AIStartsTurn()
        {
            CoinResult.Visible = false;

            ai_Hand.DrawCard(ai.DrawCard());
            ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
            OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
            gameMessage.Text = "Your opponent draws a card.";
            aiPlayedEnergy = false;
            aiPlayedAttack = false;
            hasWeakness = false;
            DrawSound();
            MessageBoxToNormal("");
            gameMessage.Visible = false;
            AITurnTimer.Start();
        }
        private void PlayerEnd_Click(object sender, EventArgs e)
        {
            PlayerEnd.Visible = false;
            AIFirstTurnTimer.Start();
        }

        private void MessageBoxToNormal(string x)
        {
            gameMessage.Text = x;
            gameMessage.Location = new Point(303, 498);
            gameMessage.Size = new Size(1292, 31);
            gameMessage.BackColor = Color.FromArgb(255, 192, 128);
            gameMessage.Font = new Font("Microsoft Sans Serif", 18, FontStyle.Bold);
            gameMessage.ForeColor = Color.Black;
        }
        private void MessageBoxAnnouncement(string x)
        {
            gameMessage.Text = x;
            gameMessage.Location = new Point(303, 498);
            gameMessage.Size = new Size(1292, 77);
            gameMessage.BringToFront();
            gameMessage.BackColor = Color.Navy;
            gameMessage.Font = new Font("Microsoft Sans Serif", 27, FontStyle.Bold);
            gameMessage.ForeColor = Color.White;
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
            if (index == 1)
            {
                active_Pokemon.Active_Pokemon.attack1.UseAttack(
                    active_Pokemon.ShowAttackType(index),
                    active_Pokemon,
                    ai_Active_Pokemon,
                    player_used,
                    discard,
                    active_Pokemon.Active_Pokemon.attack1.damage,
                    active_Pokemon.Active_Pokemon.attack1.self_damage,
                    false,
                    gameMessage,
                    index,
                    PlayerEnd,
                    AttackCoinTimer,
                    active_Pokemon.Active_Pokemon.attack1.afflicted_state
                );
                UpdateAIActivePokemonView();
                UpdateActivePokemonView();
                UpdateDiscardView();
                PlayerEnd.Visible = false;
                PlayerEndTimer.Start();
            }
            else if (index == 2)
            {
                active_Pokemon.Active_Pokemon.attack2.UseAttack(
                    active_Pokemon.ShowAttackType(index),
                    active_Pokemon,
                    ai_Active_Pokemon,
                    player_used,
                    discard,
                    active_Pokemon.Active_Pokemon.attack2.damage,
                    active_Pokemon.Active_Pokemon.attack2.self_damage,
                    false,
                    gameMessage,
                    index,
                    PlayerEnd,
                    AttackCoinTimer,
                    active_Pokemon.Active_Pokemon.attack2.afflicted_state
                );
                UpdateAIActivePokemonView();
                UpdateActivePokemonView();
                UpdateDiscardView();
                PlayerEnd.Visible = false;
                PlayerEndTimer.Start();
            }

            /*   else if (active_Pokemon.ShowName() == "Vulpix")
               {
                   if (active_Pokemon.ShowAttackName(index) == "Confuse Ray")
                   {
                       ai_Active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                       gameMessage.Text = "You perform Confuse Ray and deal 10 damage on " + ai_Active_Pokemon.ShowName();
                       UpdateAIActivePokemonView();
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
                       UpdateAIActivePokemonView();
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
               else if (active_Pokemon.ShowName() == "Arcanine")
               {
                   if (active_Pokemon.ShowAttackName(index) == "Flamethrower")
                   {
                       ai_Active_Pokemon.DealDamage(50, hasWeakness, PlusPowerDamage);
                       gameMessage.Text = "You perform Flamethrower and deal 50 damage on " + ai_Active_Pokemon.ShowName() + " . You also discard a fire energy";
                       UpdateAIActivePokemonView();
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
                       UpdateAIActivePokemonView();
                       UpdateActivePokemonView();
                   }
               }
               else if (active_Pokemon.ShowName() == "Weedle")
               {
                   if (active_Pokemon.ShowAttackName(index) == "Poison Sting")
                   {
                       ai_Active_Pokemon.DealDamage(10, hasWeakness, PlusPowerDamage);
                       gameMessage.Text = "You perform Poison Sting and deal 10 damage on " + ai_Active_Pokemon.ShowName();
                       UpdateAIActivePokemonView();
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
                       UpdateAIActivePokemonView();
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
                       UpdateAIActivePokemonView();
                       AIPoisoned = true;
                   }
               } */
        }
        private void PlayerEndTimer_Tick(object sender, EventArgs e)
        {
            _ticks++;
            if(_ticks == 30)
            {
                PlayerEndTimer.Stop();
                _ticks = 0;
                AIFirstTurnTimer.Start();
            }
        }
        private void AttackCoinTimer_Tick(object sender, EventArgs e)
        {
            _ticks++;
            if(_ticks == 30)
            {
                SoundPlayer simpleSound = new SoundPlayer("..\\..\\Sounds\\flipcoin.wav");
                simpleSound.Play();
                gameMessage.Visible = false;
                CoinResult.Visible = false;
            }
            else if(_ticks == 60)
            {
                AttackCoinTimer.Stop();
                _ticks = 0;
                Random random = new Random();
                int result = random.Next(1, 101);
                if (result > 0 && result < 51)
                {
                    CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\Wizards_Silver_Chansey_Coin.png");
                    CoinResult.Visible = true;
                    gameMessage.Visible = true;
                    PlayerEnd.Visible = true;

                    if (active_Pokemon.Active_Pokemon.attack1.check_status == "Confusion" || active_Pokemon.Active_Pokemon.attack2.check_status == "Confusion" || active_Pokemon.Active_Pokemon.attack3.check_status == "Confusion")
                    {
                        gameMessage.Text = "You flip the coin and get Heads. " + ai_Active_Pokemon.ShowName() + " is now Confused.";
                        AIConfused = true;
                    }
                    else if (active_Pokemon.Active_Pokemon.attack1.check_status == "Poison" || active_Pokemon.Active_Pokemon.attack2.check_status == "Poison" || active_Pokemon.Active_Pokemon.attack3.check_status == "Poison")
                    {
                        gameMessage.Text = "You flip the coin and get Heads. " + ai_Active_Pokemon.ShowName() + " is now Poisoned.";
                        AIPoisoned = true;
                    }
                    else if (active_Pokemon.Active_Pokemon.attack1.check_status == "Sleep" || active_Pokemon.Active_Pokemon.attack2.check_status == "Sleep" || active_Pokemon.Active_Pokemon.attack3.check_status == "Sleep")
                    {
                        gameMessage.Text = "You flip the coin and get Heads. " + ai_Active_Pokemon.ShowName() + " is now Asleep.";
                        AIAsleep = true;
                    }
                    else if (active_Pokemon.Active_Pokemon.attack1.check_status == "Burn" || active_Pokemon.Active_Pokemon.attack2.check_status == "Burn" || active_Pokemon.Active_Pokemon.attack3.check_status == "Burn")
                    {
                        gameMessage.Text = "You flip the coin and get Heads. " + ai_Active_Pokemon.ShowName() + " is now Burned.";
                        AIBurned = true;
                    }
                    else if (active_Pokemon.Active_Pokemon.attack1.check_status == "Paralysis" || active_Pokemon.Active_Pokemon.attack2.check_status == "Paralysis" || active_Pokemon.Active_Pokemon.attack3.check_status == "Paralysis")
                    {
                        gameMessage.Text = "You flip the coin and get Heads. " + ai_Active_Pokemon.ShowName() + " is now Paralyzed.";
                        AIParalyzed = true;
                    }
                    else if (active_Pokemon.Active_Pokemon.attack1.check_status == "Luck" || active_Pokemon.Active_Pokemon.attack2.check_status == "Luck" || active_Pokemon.Active_Pokemon.attack3.check_status == "Luck")
                    {
                        ai_Active_Pokemon.DealDamage(30, hasWeakness, PlusPowerDamage);
                        UpdateAIActivePokemonView();
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
                    }
                    UpdateAIActivePokemonView();
                }
                else
                {
                    CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\tails.gif");
                    CoinResult.Visible = true;
                    gameMessage.Text = "You flip the coin and get Tails, nothing else happens.";
                    gameMessage.Visible = true;
                }
            }
        }
        private void AIFlipsCoin(string type, int reps)
        {
            SoundPlayer simpleSound = new SoundPlayer("..\\..\\Sounds\\flipcoin.wav");
            simpleSound.Play();

            Random random = new Random();
            int result = random.Next(0, 2);

            if (result == 0)
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\Wizards_Silver_Chansey_Coin.png");
                CoinResult.Visible = true;
                if(type == "confusion")
                {
                    PlayerConfused = true;
                    playerStatus.Image = Image.FromFile("..\\..\\Img\\Status\\confusion.png");
                    playerStatus.Visible = true;                  
                }
            }
            else
            {
                CoinResult.Image = Image.FromFile("..\\..\\Img\\Coins\\tails.gif");
                CoinResult.Visible = true;
            }
        }
        

        private bool ShouldPlayAttack()
        {
            return true;

            if (ai_Active_Pokemon.ShowName() == "Magnemite")
            {
                if (ai_Active_Pokemon.ShowAttackName(2) == "Self Destruct" && ai_Active_Pokemon.ShowRemHP() > 20)
                {
                    return false;
                }
            }
        }
        private void PlayingTrainerCardOn(int x, int y)
        {
            //x is the card index from the hand and y is the card index on the bench
            if (player_Hand.ShowName(x) == "Potion")
            {
                bench.HealHp(20, y);
                gameMessage.Text = "You use a Potion and heal 20 damage on " + bench.ShowName(y);
                discard.Add(player_Hand.ThisCard(x));
                player_Hand.RemoveFromHand(x);
                UpdateHandView();
                UpdateDiscardView();
            }
            else if (player_Hand.ShowName(x) == "Super Potion")
            {
                bench.HealHp(40, y);
                gameMessage.Text = "You use a Super Potion and heal 40 damage on " + bench.ShowName(y) + ". You also discard an Energy Card.";
                discard.Add(player_Hand.ThisCard(x));
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
                UpdateActivePokemonView();
                discard.Add(player_Hand.ThisCard(x));
                player_Hand.RemoveFromHand(x);
                UpdateHandView();
                UpdateDiscardView();
            }
            else if (player_Hand.ShowName(x) == "Super Potion")
            {
                active_Pokemon.HealHp(40);
                gameMessage.Text = "You use a Super Potion and heal 40 damage on " + active_Pokemon.ShowName() + ". You also discard an Energy Card.";
                UpdateActivePokemonView();
                discard.Add(player_Hand.ThisCard(x));
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
                    discard.Add(player_Hand.ThisCard(x));
                    player_Hand.RemoveFromHand(x);
                    UpdateHandView();
                    UpdateDiscardView();
                }
            }
            else if (player_Hand.ShowName(x) == "PlusPower")
            {
                gameMessage.Text = "You use PlusPower on " + active_Pokemon.ShowName() + ". Its attacks do 10 more damage till the end of this turn.";
                PlusPowerOn = true;
                discard.Add(player_Hand.ThisCard(x));
                player_Hand.RemoveFromHand(x);
                UpdateHandView();
                UpdateDiscardView();
            }
            else if (player_Hand.ShowName(x) == "Energy Removal")
            {
                if(ai_Active_Pokemon.EnergyLoadedCount() != 0)
                {
                    DiscardEnergyCardToAI();
                    discard.Add(player_Hand.ThisCard(x));
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
            DeckSize.Visible = false;
            OpponentDeck.Visible = false;
            ODeckSize.Visible = false;
            HandIcon1.Visible = false;
            HandIcon2.Visible = false;
            HandIcon3.Visible = false;
            OHandNumber.Visible = false;
            PictureZoom.Visible = false;
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
            DeckSize.Visible = false;
            OpponentDeck.Visible = false;
            OpponentZoom.Visible = false;
            ODeckSize.Visible = false;
            HandIcon1.Visible = false;
            HandIcon2.Visible = false;
            HandIcon3.Visible = false;
            OHandNumber.Visible = false;
            PictureZoom.Visible = false;
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
            ODeckSize.Text = "x" + ai.NumberOfCards().ToString();
            OHandNumber.Text = "X" + ai_Hand.NumberOfCards().ToString();
            DeckSize.Text = "x" + player.NumberOfCards().ToString();
            UpdateHandView();
            UpdateActivePokemonView();
            UpdateBenchView();
            UpdateDiscardView();
            
            UpdateActivePokemonView();
            UpdateAIActivePokemonView();
            UpdateAIBenchView();
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
                isAITurn = false;
                EndOpponentsTurn.Visible = true;
            }        
        }

        private void QuickTest_Click(object sender, EventArgs e)
        {
            AIFlipsCoin("confusion", 1);
        }
        //
        private void AIStartingTurnTimer_Tick(object sender, EventArgs e)
        {
            _ticks++;
            if(_ticks == 5)
            {
                AIStartingTurnTimer.Stop();
                CheckStage();
                AIStartingTurnTimer.Start();
            }
            else if(_ticks == 20) 
            {
                AIStartingTurnTimer.Stop();
                AISwitchTurn();
                AIStartingTurnTimer.Start();
            }
            else if(_ticks == 35)
            {
                AIStartingTurnTimer.Stop();
                _ticks = 0;
                AIStartsTurn();         
            }
        }

        
        private void CheckIfKnockedOut()
        {
            if (ai_Active_Pokemon.ShowRemHP() == 0 && active_Pokemon.ShowRemHP() != 0)
            {
                PlayerEnd.Visible = false;
                EndOpponentsTurn.Visible = false;
                KnockedOut.Location = new Point(683, 957);
                KnockedOut.Visible = true;
                FlipCoin.Visible = false;
            }
            else if(active_Pokemon.ShowRemHP() == 0 && ai_Active_Pokemon.ShowRemHP() != 0)
            {
                PlayerEnd.Visible = false;
                EndOpponentsTurn.Visible = false;
                gameMessage.Text = active_Pokemon.ShowName() + " is Knocked out. Choose a Benched Pokémon now.";
                active_Pokemon.ClearEverthingFromCard();
                PlayerAsleep = false;
                PlayerBurned = false;
                PlayerConfused = false;
                PlayerParalyzed = false;
                PlayerPoisoned = false;

                for (int i = 0; i < player_used.Count(); i++)
                {
                    discard.Add(player_used.GetCard(i));
                }

                player_used.DiscardAll();
                discard.Add(active_Pokemon.GetActivePokemon());
                PictureZoom.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
                active_Pokemon.Become(new Pokemon(0, "null", "", 'u', ""));
                UpdateDiscardView();
                UpdateActivePokemonView();               
            }
            else if(active_Pokemon.ShowRemHP() == 0 && ai_Active_Pokemon.ShowRemHP() == 0)
            {
                PlayerEnd.Visible = false;
                EndOpponentsTurn.Visible = false;
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
            for (int i = 0; i < ai_used.Count(); i++)
            {
                ai_discard.Add(ai_used.GetCard(i));
            }
            ai_used.DiscardAll();

            //Clearing the views
            ai_discard.Add(ai_Active_Pokemon.GetActivePokemon());
            OpponentZoom.Image = Image.FromFile("..\\..\\Img\\BS\\CardBack.jpg");
            ai_Active_Pokemon.Become(new Pokemon(0, "null", "", 'u', ""));
            UpdateAIDiscardView();
            UpdateAIActivePokemonView();

            KnockedOut.Visible = false;
            Replace.Location = new Point(683, 957);
            Replace.Visible = true;
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
                ActivePokemonTemp = player_Hand.ThisCard(num);
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
        private void CheckCard_Click(object sender, EventArgs e)
        {
            int num = 0;

            CheckCardPanel.Location = new Point(645, 2);
            CheckCardPanel.Visible = true;
            
            /*this.Controls.Add(panel1);
            panel1.Size = new Size(1920, 1080);
            panel1.Location = new Point(0,0);
            
            panel1.BackColor = Color.Peru;
            panel1.BringToFront();*/
            CheckCardPanel.BringToFront();

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
            PictureBoxPanel.Image = Image.FromFile(player_Hand.ShowCard(num));

            if(player_Hand.ShowType(num) == "basic" || player_Hand.ShowType(num) == "second" || player_Hand.ShowType(num) == "third"){
                CheckCardName.Text = player_Hand.ShowName(num);
                CheckCardPanel2.Location = new Point(703, 798);
                CheckCardPanel2.Visible = true;
                CheckCardHp.Text = player_Hand.ShowRemHp(num).ToString() + "/" + player_Hand.ShowHP(num).ToString();
                CheckCardPanel2.BringToFront();

                if (player_Hand.ShowWeakness(num).Equals('w'))
                {
                    CheckCardWeakness.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Water.gif"); 
                }
                else if (player_Hand.ShowWeakness(num).Equals('f'))
                {
                    CheckCardWeakness.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fire.gif");
                }
                else if (player_Hand.ShowWeakness(num).Equals('g'))
                {
                    CheckCardWeakness.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Grass.gif"); 
                }
                else if (player_Hand.ShowWeakness(num).Equals('p'))
                {
                    CheckCardWeakness.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Psychic.gif"); 
                }
                else if (player_Hand.ShowWeakness(num).Equals('l'))
                {
                    CheckCardWeakness.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fighting.gif");
                }
                else if (player_Hand.ShowWeakness(num).Equals('e'))
                {
                    CheckCardWeakness.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Lightning.gif"); 
                }
                else if (player_Hand.ShowWeakness(num).Equals('c'))
                {
                    CheckCardWeakness.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Colorless.gif"); ;
                }

                CheckCardResistance.Visible = true;
                label6.Visible = true;
                if (player_Hand.ShowResistance(num).Equals('n'))
                {
                    CheckCardResistance.Visible = false;
                    label6.Visible = false;
                }
                else if (player_Hand.ShowResistance(num).Equals('f'))
                {
                    CheckCardResistance.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fire.gif");
                }
                else if (player_Hand.ShowResistance(num).Equals('g'))
                {
                    CheckCardResistance.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Grass.gif");
                }
                else if (player_Hand.ShowResistance(num).Equals('p'))
                {
                    CheckCardResistance.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Psychic.gif");
                }
                else if (player_Hand.ShowResistance(num).Equals('l'))
                {
                    CheckCardResistance.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Fighting.gif");
                }
                else if (player_Hand.ShowResistance(num).Equals('e'))
                {
                    CheckCardResistance.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Lightning.gif");
                }
                else if (player_Hand.ShowResistance(num).Equals('c'))
                {
                    CheckCardResistance.Image = Image.FromFile("..\\..\\Img\\EnergyBox\\Colorless.gif"); ;
                }

                if(player_Hand.GetRetreatCost(num) == 0)
                {
                    CheckCardR1.Visible = false;
                    CheckCardR2.Visible = false;
                    CheckCardR3.Visible = false;
                    CheckCardR4.Visible = false;
                    CheckCardR5.Visible = false;
                }
                else if (player_Hand.GetRetreatCost(num) == 1)
                {
                    CheckCardR1.Visible = true;
                    CheckCardR2.Visible = false;
                    CheckCardR3.Visible = false;
                    CheckCardR4.Visible = false;
                    CheckCardR5.Visible = false;
                }
                else if (player_Hand.GetRetreatCost(num) == 2)
                {
                    CheckCardR1.Visible = true;
                    CheckCardR2.Visible = true;
                    CheckCardR3.Visible = false;
                    CheckCardR4.Visible = false;
                    CheckCardR5.Visible = false;
                }
                else if (player_Hand.GetRetreatCost(num) == 3)
                {
                    CheckCardR1.Visible = true;
                    CheckCardR2.Visible = true;
                    CheckCardR3.Visible = true;
                    CheckCardR4.Visible = false;
                    CheckCardR5.Visible = false;
                }
                else if (player_Hand.GetRetreatCost(num) == 4)
                {
                    CheckCardR1.Visible = true;
                    CheckCardR2.Visible = true;
                    CheckCardR3.Visible = true;
                    CheckCardR4.Visible = true;
                    CheckCardR5.Visible = false;
                }
                else if (player_Hand.GetRetreatCost(num) == 5)
                {
                    CheckCardR1.Visible = true;
                    CheckCardR2.Visible = true;
                    CheckCardR3.Visible = true;
                    CheckCardR4.Visible = true;
                    CheckCardR5.Visible = true;
                }
            }           
        }
        private void Left_Click(object sender, EventArgs e)
        {
            CheckCardPanel.Visible = false;
            CheckCardPanel2.Visible = false;
            this.Controls.Remove(panel1);
        }
        private void DrawSound()
        {
            SoundPlayer simpleSound = new SoundPlayer("..\\..\\Sounds\\draw.wav");
            simpleSound.Play();
        }

        
    }
}
