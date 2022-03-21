namespace Blackjack
{
    class Card
    {
        //fields
        public int Value { get; set; }
        public SuitType Suit { get; set; }
        public int BlackJackValue { get; set; } = 1;

        //construct
        public Card(int value, SuitType suit)
        {
            Value = value;
            Suit = suit;

            if (Value == 11 || Value == 12 || Value == 13) BlackJackValue = 10;
            else BlackJackValue = Value;
        }

        //public methods
        public override string ToString()
        {
            //return a single card as a string
            int t_value = Value;
            SuitType t_suit = Suit;

            if (t_value == 1) return "Ace of " + t_suit;
            if (t_value == 11) return "Jack of " + t_suit;
            if (t_value == 12) return "Queen of " + t_suit;
            if (t_value == 13) return "King of " + t_suit;

            return t_value + " of " + t_suit;
        }
    }

    enum SuitType
    {
        Clubs,
        Diamonds,
        Hearts,
        Spades
    }
}
