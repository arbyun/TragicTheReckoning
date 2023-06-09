﻿using System;

namespace TragicTheReckoningGame
{
    public class Card
    {
        public readonly string Name;
        public readonly int Cost;
        public readonly int Ap;
        public readonly int Id;
        public int Dp;
        public readonly Deck InDeck; //the deck the card is in
        public bool isDead = false;

        /// <summary> The Card function creates a new card object with the given name, cost, attack points and defense points.        
        /// It also assigns an ID to the card based on its position in the deck.</summary>
        /// <param name="name"> Name of the card</param>
        /// <param name="cost"> The cost of the card.</param>
        /// <param name="ap"> Attack points</param>
        /// <param name="dp"> Defense points</param>
        /// <param name="inDeck"> The deck the card is in.</param>
        /// <returns> A card object.</returns>
        public Card(string name, int cost, int ap, int dp, Deck inDeck=null)
        {
            Name = name;
            Cost = cost;
            Ap = ap;
            Dp = dp;
            InDeck = inDeck;
            Id = inDeck!.CardList.Count + 1;
        }
        
        /// <summary>
        /// Representação das cartas no ecrã
        /// </summary>
        public override string ToString() => $"[ID: {Id}] ||  {Name}/{Cost} MANA/{Ap} DAMAGE/{Dp} DEFENSE";

        /// <summary>
        /// Makes card suffer damage taking into account their defense points
        /// </summary> 
        public void Damage(int damage) => Dp -= damage;

        public void CardHealthCheck()
        {
            if (Dp <= 0)
            {
                Console.WriteLine($"{this.Name} from {this.InDeck.Owner.Name} was defeated!");
                isDead = true;
            }
        }
    }
}