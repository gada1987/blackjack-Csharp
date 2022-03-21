using System;
using System.Threading;

namespace Blackjack
{
    class Game
    {
        //fields
        public Player Player { get; set; } = new Player();
        public Player Dealer { get; set; } = new Player();
        public Deck Deck { get; set; } = new Deck(4);
        public GameStatus GameStatus { get; set; }

        //construct
        public Game()
        {
            GameStatus = GameStatus.Menu;
        }

        //public methods
        public void Reset()
        {
            //reset game 

            Console.Clear();
            Player.Reset();
            Dealer.Reset();
            GameStatus = GameStatus.Betting;
            Deck.ResetAndShuffle();
            
            Player.Hand.Add(Deck.Draw());
            Dealer.Hand.Add(Deck.Draw());
            Dealer.Hand.Add(Deck.Draw());
        }
        public void PlayerDraw()
        {
            Card t_card = Deck.Draw(); // draw card from deck
            Player.LastDrawnCard = t_card; // set LastDrawnCard
            Player.Hand.Add(t_card); // add card to hand

            Console.WriteLine($"\nMoney: ${Math.Round(Player.Money, 2)}, Bet: ${Math.Round(Player.CurrentBet, 2)}");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("Player Has: ");
            Console.ForegroundColor = ConsoleColor.White;

            Console.Write(Player.ToString());
            Console.WriteLine($" ({Player.BestValue})");

            Thread.Sleep(1000);

            //return if over 21 or blackjack
            if (Player.BestValue == 21)
            {
                GameStatus = GameStatus.BlackJack;
                return;
            }
            if (Player.LowValue > 21)
            {
                GameStatus = GameStatus.Lost;
                return;
            }

        }
        public void AskNextMove()
        {
            //return if over 21 or blackjack
            if (Player.BestValue == 21)
            {
                GameStatus = GameStatus.BlackJack;
                return;
            }
            if (Player.LowValue > 21)
            {
                GameStatus = GameStatus.Lost;
                return;
            }

            do
            {
                if (Player.canDouble)
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\nHit(h), Stand(s), Double(d): ");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\nHit(h), Stand(s): ");
                    Console.ForegroundColor = ConsoleColor.White;
                }

                try
                {
                    //read input and execute action depending on input
                    string t_awnser = Console.ReadLine();
                    if (t_awnser == "s") return; //return
                    if (t_awnser == "h") { PlayerDraw(); AskNextMove(); return; } //draw card and ask player again
                    if (t_awnser == "d" && Player.canDouble) { PlayerDouble(); PlayerDraw(); return; } //double bet, draw card and return
                    if (t_awnser == "close") Environment.Exit(0); //exit application
                    if (t_awnser == "menu") { GameStatus = GameStatus.Menu; return; } //return to menu
                    throw new Exception("Invalid awnser\n");
                }
                catch (Exception e)
                {
                    Console.WriteLine("\n" + e.Message);
                }

            } while (true);
        }
        public void DealerDraw()
        {
            while (Dealer.HighValue < 17) //while dealer value under 17
            {
                Card t_card = Deck.Draw(); // draw card from deck
                Dealer.LastDrawnCard = t_card; // set LastDrawnCard
                Dealer.Hand.Add(t_card); // add card to hand
            }
        }

        //private methods
        private void PlayerDouble()
        {
            Player.Money -= Player.CurrentBet; //subtract bet from money
            Player.CurrentBet *= 2; //double bet
        }
    }

    enum GameStatus
    {
        Menu,
        Betting,
        Won,
        Lost,
        Playing,
        Displayingreslut,
        Tie,
        BlackJack
    }
}

