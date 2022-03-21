using System.Collections.Generic;
using System.Linq;

namespace Blackjack
{
    class Player
    {
        //fields
        public int LowValue => Hand.Sum(x => x.BlackJackValue);
        public int HighValue => Hand.Any(x => x.Value == 1) ? LowValue + 10 : LowValue;
        public int BestValue => (HighValue <= 21) ? HighValue : LowValue;
        public double Money { get; set; }
        public double CurrentBet { get; set; }
        public double LastBet { get; set; } = 0.1f;
        public bool canDouble => CurrentBet * 2 <= Money && CurrentBet >= 10 && Hand.Count == 2 ? true : false;
        public List<Card> Hand { get; set; } = new List<Card>();
        public Card LastDrawnCard { get; set; }
        
        //construct
        public Player()
        {
            Money = 1000;
        }

        //methods
        public void Reset()
        {
            Hand.Clear();
            CurrentBet = 0f;
        }
        public override string ToString()
        {
            //returns all cards in hand as a string

            string t_string = "";

            for (int i = 0; i < Hand.Count; i++)
            {
                int t_value = Hand[i].Value;
                SuitType t_suit = Hand[i].Suit;

                switch(t_value)
                {
                    case 1:
                        t_string += i < Hand.Count - 1 ? $"Ace of {t_suit}, ": $"Ace of {t_suit}";
                        break;
                    case 11:
                        t_string += i < Hand.Count - 1 ? $"Jack of {t_suit}, " : $"Jack of {t_suit}";
                        break;
                    case 12:
                        t_string += i < Hand.Count - 1 ? $"Queen of {t_suit}, " : $"Queen of {t_suit}";
                        break;
                    case 13 :
                        t_string += i < Hand.Count - 1 ? $"King of {t_suit}, " : $"King of {t_suit}";
                        break;
                    default:
                        t_string += i < Hand.Count - 1 ? $"{t_value} of {t_suit}, " : $"{t_value} of {t_suit}";
                        break;
                }
            }

            return t_string;
        }
    }
}
