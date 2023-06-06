using System;
using System.Collections.Generic;

namespace TragicTheReckoningGame
{
    public class Deck: CardHolder
    {
        private int _maxCards;
        public Player Owner;
        
        // are there cards left in the deck?
        public bool CardsLeft = true;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="maxCards">número de cartas possiveis</param>
        public Deck(int maxCards)
        {
            _maxCards = maxCards;
            CardList = new List<Card>(capacity: maxCards);

            //add the cards here. for ex:
            Card newCard = new Card("Flying Wand", 1, 1, 1, this);
            Populate(CardList, newCard, 4);

            Card newCard1 = new Card("Severed Monkey Head", 1, 2, 1, this);
            Populate(CardList, newCard1, 4);

            Card newCard2 = new Card("Mystical Rock Wall", 2, 0, 5, this);
            Populate(CardList, newCard2, 2); 

            Card newCard3 = new Card("Lobster McCrabs", 2, 1, 3, this);
            Populate(CardList, newCard3, 2);

            Card newCard4 = new Card("Goblin Troll", 3, 3, 2, this);
            Populate(CardList, newCard4, 2);

            Card newCard5 = new Card("Scorching Heatwave", 4, 5, 3, this);
            Populate(CardList, newCard5, 1);

            Card newCard6 = new Card("Blind Minotaur", 3, 1, 3, this);
            Populate(CardList, newCard6, 1);

            Card newCard7 = new Card("Tim, The Wizard", 5, 6, 4, this);
            Populate(CardList, newCard7, 1);

            Card newCard8 = new Card("Sharply Dressed", 4, 3, 3, this);
            Populate(CardList, newCard8, 1);

            Card newCard9 = new Card("Blue Steel", 2, 2, 2, this);
            Populate(CardList, newCard9, 2);
            
        }

        /// <summary>
        /// The DrawCard function removes the first card in the deck and returns it.
        /// </summary>
        /// <returns> A card object. </returns>
        public Card DrawCard()
        {
            if (CardList.Count != 0)
            {
                Card drawnCard = CardList[0];
                CardList.RemoveAt(0);

                if (CardList[0] == null)
                {
                    CardsLeft = false;
                }

                return drawnCard;
            }

            return null;
        }


        /// <summary>
        /// Baralhar as cartas
        /// </summary>
        public void ShuffleCards()
        {
            Random shuffle = new Random();

            for (int i = CardList.Count - 1; i > 0; i--)
            {
                int j = shuffle.Next(i + 1);
                (CardList[i], CardList[j]) = (CardList[j], CardList[i]);
            }
        }

    }
}