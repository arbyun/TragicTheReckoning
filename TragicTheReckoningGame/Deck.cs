﻿using System;
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
            Card newCard = new Card("Flying Wand", 1, 1, 1, this);
            Populate(CardList, newCard, 4);

            Card newCard = new Card("Severed Monkey Head", 1, 2, 1, this);
            Populate(CardList, newCard, 4);

            Card newCard = new Card("Mystical Rock Wall", 2, 0, 5, this);
            Populate(CardList, newCard, 2); 

            Card newCard = new Card("Lobster McCrabs", 2, 1, 3, this);
            Populate(CardList, newCard, 2);

            Card newCard = new Card("Goblin Troll", 3, 3, 2, this);
            Populate(CardList, newCard, 2);

            Card newCard = new Card("Scortching Heatwave", 4, 5, 3, this);
            Populate(CardList, newCard, 1);

            Card newCard = new Card("Blind Minotaur", 3, 1, 3, this);
            Populate(CardList, newCard, 1);

            Card newCard = new Card("Tim, The Wizard", 5, 6, 4, this);
            Populate(CardList, newCard, 1);

            Card newCard = new Card("Sharply Dressed", 4, 3, 3, this);
            Populate(CardList, newCard, 1);

            Card newCard = new Card("Blue Steel", 2, 2, 2, this);
            Populate(CardList, newCard, 2);
            
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