using System;
using System.Collections.Generic;

namespace TragicTheReckoningGame
{
    public class Deck: CardHolder
    {
        private int _maxCards;
        public Player Owner;
        
        // are there cards left in the deck?
        public bool CardsLeft;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="maxCards"></param>
        public Deck(int maxCards)
        {
            _maxCards = maxCards;
            CardList = new List<Card>(capacity: maxCards);

            //add the cards here. for ex:
            Card newCard = new Card("name", 3, 4, 5, this);
            Populate(CardList, newCard, 4);
            
            
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
        /// 
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