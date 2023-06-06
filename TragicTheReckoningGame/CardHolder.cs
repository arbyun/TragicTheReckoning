using System.Collections.Generic;

namespace TragicTheReckoningGame
{
    public abstract class CardHolder
    {
        protected internal List<Card> CardList;

        internal void Populate(List<Card> cardList, Card cardToAdd, int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                cardList.Add(cardToAdd);
            }
        }
    }
}