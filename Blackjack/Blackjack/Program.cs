using System;
using System.Threading;
using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    class Program
    {
        
        private static Game Game = new Game();

        private static string[] validAwnsersYes = new string[] { "yes", "Yes", "YES", "ja", "Ja", "JA", "h", "H" };

        private static string[] validAwnsersNo = new string[] { "no", "No", "NO", "nej", "Nej", "NEJ", "s", "S" };

        static void Main(string[] args)
        {
            while (true)
            {
                switch (Game.GameStatus)
                {
                    case GameStatus.Menu:
                            Menu();
                        break;
                    case GameStatus.Betting:
                            PlaceBet();
                        break;
                    case GameStatus.Won:
                            Won();
                        break;
                    case GameStatus.Lost:
                            Lost();
                        break;
                    case GameStatus.Playing:
                            Playing();
                        break;
                    case GameStatus.Displayingreslut:
                            DisplayResult();
                        break;
                    case GameStatus.Tie:
                            Tie();
                        break;
                    case GameStatus.BlackJack:
                            BlackJack();
                        break;
                    default:
                        break;
                }
            }
            //public methods
            void Menu()
            {
                Game.Player.Money = 1000; //reset money
                Game.Reset();
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("---------- BLACKJACK ----------");
                Console.ForegroundColor = ConsoleColor.White;

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("\n---------- GLOBAL COMMANDS ----------");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("menu");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" = return to menu");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("close");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" = close application");

                do
                {
                    Console.ForegroundColor = ConsoleColor.Blue;
                    Console.Write("\nSit Down: ");
                    Console.ForegroundColor = ConsoleColor.White;

                    try
                    {
                        //read input and execute action depending on input
                        string awnser = Console.ReadLine();
                        if (AwnserEqualsAny(awnser, validAwnsersNo)) Environment.Exit(0); //exit application
                        if (AwnserEqualsAny(awnser, validAwnsersYes)) return; //start game
                        if (awnser == "close") Environment.Exit(0); //exit application
                        if (awnser == "menu") { Game.GameStatus = GameStatus.Menu; return; } //return to menu
                        else throw new Exception("Invaild awnser");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("\n" + e.Message);
                    }
                } while (true);
            }

            void PlaceBet()
            {
                //variables
                double currentMoney = Game.Player.Money;
                double bet = 0;
                double totalBetAmount = 0;

                List<string> allowedNumbers = new List<string>() { "1", "2", "3", "4", "5", "6" };

                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("---------- Commands ----------");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("deal");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" = start game");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("clear");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" = reset bet");
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.Write("rebet");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(" = the last placed bet");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\n---------- Bet amount ----------");
                Console.ForegroundColor = ConsoleColor.White;

                Console.WriteLine("1 = $0.1");
                Console.WriteLine("2 = $1");
                Console.WriteLine("3 = $5");
                Console.WriteLine("4 = $10");
                Console.WriteLine("5 = $25");
                Console.WriteLine("6 = $100");

                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("--------------------------------");
                Console.ForegroundColor = ConsoleColor.White;

                do
                {
                    try
                    {
                        Console.WriteLine("\nCurrent money: $" + Math.Round(currentMoney, 2));
                        Console.WriteLine("bet: $" + Math.Round(totalBetAmount, 2));

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("Bet amount: ");
                        Console.ForegroundColor = ConsoleColor.White;

                        string awnserString = Console.ReadLine(); //read input

                        switch (awnserString)
                        {
                            case "1":
                                bet = 0.1f;
                                break;
                            case "2":
                                bet = 1;
                                break;
                            case "3":
                                bet = 5;
                                break;
                            case "4":
                                bet = 10;
                                break;
                            case "5":
                                bet = 25;
                                break;
                            case "6":
                                bet = 100;
                                break;
                            case "close":
                                Environment.Exit(0); //exit application
                                break;
                            case "menu":
                                // return to menu
                                Game.GameStatus = GameStatus.Menu; 
                                return;
                            case "clear":
                                //reset bet
                                bet = 0;
                                totalBetAmount = 0;
                                currentMoney = Game.Player.Money;
                                break;
                            case "deal":
                                //start game
                                Console.Clear();
                                Game.Player.LastBet = totalBetAmount;
                                Game.Player.Money = currentMoney;
                                Game.Player.CurrentBet = totalBetAmount;
                                Game.GameStatus = GameStatus.Playing;
                                return;
                            case "rebet":
                                //bet same amount as last bet and start game
                                Console.Clear();

                                //if value of last bet is over money 
                                if (Game.Player.LastBet > currentMoney) { Console.WriteLine("\nNot enough money"); break; }
                                    
                                //set bet to 0.1 if last bet = 0
                                if (Game.Player.LastBet >= 0.1f) Game.Player.CurrentBet = Game.Player.LastBet;
                                else Game.Player.CurrentBet = 0.1f;

                                //apply and return
                                Game.Player.Money -= Game.Player.LastBet;
                                Game.GameStatus = GameStatus.Playing;
                                return;
                            default:
                                throw new Exception("Invalid awnser");
                        }

                        //limitations
                        if (totalBetAmount + bet > 300) Console.WriteLine("\nNot allowed to bet over $300"); 
                        else if (bet > currentMoney) Console.WriteLine("\nNot enough money"); 
                        else { currentMoney -= bet; totalBetAmount += bet; }
                    }
                    catch(Exception e)
                    {
                        Console.WriteLine("\n" + e.Message);
                    }
                } while (true);
            }

            void Playing()
            {
                
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write($"Dealer has: ");
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(Game.Dealer.Hand[0].ToString());
                Console.ForegroundColor = ConsoleColor.Gray;
                Console.WriteLine(", Hidden");
                Console.ForegroundColor = ConsoleColor.White;

                Game.PlayerDraw();
                if (Game.GameStatus != GameStatus.Playing) return; //controll still playing
                Game.AskNextMove();
                if (Game.GameStatus != GameStatus.Playing) return; //controll still playing

                Game.DealerDraw();

                Game.GameStatus = GameStatus.Displayingreslut;
            }

            void DisplayResult()
            {
                Console.Clear();

                Console.ForegroundColor = ConsoleColor.Cyan;
                Console.Write("Player has: ");
                Console.ForegroundColor = ConsoleColor.White;

                Console.Write(Game.Player.ToString());
                Console.WriteLine($" ({Game.Player.BestValue})");

                Console.WriteLine();

                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("Dealer has: ");
                Console.ForegroundColor = ConsoleColor.White;

                //write dealer hand with a pause between cards
                for (int i = 0; i < Game.Dealer.Hand.Count; i++)
                {
                    if (i < Game.Dealer.Hand.Count - 1) Console.Write($"{Game.Dealer.Hand[i].ToString()}, ");
                    else Console.Write($"{Game.Dealer.Hand[i].ToString()}");

                    Thread.Sleep(1000);
                }
                Console.WriteLine($" ({Game.Dealer.HighValue})");

                WinLogic();
            }

            void Won()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine($"\nYou Win : ${Math.Round(Game.Player.CurrentBet * 2, 2)}");
                Console.ForegroundColor = ConsoleColor.White;

                Game.Player.Money += Game.Player.CurrentBet * 2;
                Thread.Sleep(3000);
                Game.Reset();
            }

            void Lost()
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nYou Lose");
                Console.ForegroundColor = ConsoleColor.White;

                Thread.Sleep(3000);
                Game.Reset();
            }

            void Tie()
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine("\nYou tied");
                Console.ForegroundColor = ConsoleColor.White;

                Game.Player.Money += Game.Player.CurrentBet;
                Thread.Sleep(3000);
                Game.Reset();
            }

            void BlackJack()
            {
                Console.ForegroundColor = ConsoleColor.Magenta;
                Console.WriteLine($"\nBlackJack : ${Math.Round(Game.Player.CurrentBet * 2, 2)}");
                Console.ForegroundColor = ConsoleColor.White;

                Game.Player.Money += Game.Player.CurrentBet * 2;
                Thread.Sleep(3000);
                Game.Reset();
            }
        }

        //private methods
        private static bool AwnserEqualsAny(string awnser, string[] array)
        {
            foreach (var element in array) if (awnser == element) return true;
            return false;
        }

        private static  void WinLogic()
        {
            int dealerSum = Game.Dealer.HighValue;
            int playerSum = Game.Player.BestValue;

            //win
            if (dealerSum > 21 || dealerSum < playerSum)
            {
                Game.GameStatus = GameStatus.Won;
                return;
            }
            //tie
            if (dealerSum == 20 && playerSum == 20)
            {
                Game.GameStatus = GameStatus.Tie;
                return;
            }
            //lost
            if (dealerSum > playerSum || dealerSum == 17 && playerSum == 17 || dealerSum == 18 && playerSum == 18 || dealerSum == 19 && playerSum == 19)
            {
                Game.GameStatus = GameStatus.Lost;
                return;
            }
        }
    }
}