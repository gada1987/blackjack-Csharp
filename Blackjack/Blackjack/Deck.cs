using System;
using System.Collections.Generic;

namespace Blackjack
{
    class Deck
    {
        public List<Card> _cards = new List<Card>();
        private readonly int _nrOfDecks;
        
        //cunstruct
        public Deck(int nrOfDecks)
        {
            _nrOfDecks = nrOfDecks;
            ResetAndShuffle();
        }

        //public methods
        public void ResetAndShuffle()
        {
            _cards.Clear();

            for (int i = 1; i <= _nrOfDecks; i++) // number of decks
            {
                for (int j = 0; j <= 3; j++) // number of suits
                {
                    for (int k = 1; k <= 13; k++) // number of cards in each suit in each deck
                    {
                        _cards.Add(new Card(k, (SuitType)j));
                    }
                }
            }

            Shuffle();
        }
        public void Shuffle()
        {
            //shuffles the order of cards in a deck

            Random rnd = new Random();

            int index = _cards.Count;
            while (index > 1)
            {
                index--;
                int k = rnd.Next(index + 1);
                Card card = _cards[k];
                _cards[k] = _cards[index];
                _cards[index] = card;
            }
        }
        public Card Draw()
        {
            //returns a card and removes it from the deck

            Random rnd = new Random();
            int t_index = rnd.Next(0, _cards.Count);
            Card t_card = _cards[t_index];

            _cards.RemoveAt(t_index);
            return t_card;
        }
    }
}
