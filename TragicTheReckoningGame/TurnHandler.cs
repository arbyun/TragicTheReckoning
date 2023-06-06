using System;
using System.Collections.Generic;

namespace TragicTheReckoningGame
{
    public sealed class TurnHandler
    {
        public int CurrentTurnNumber = 1;
        public List<Card> P1CardsInHand = new List<Card>();
        public List<Card> P2CardsInHand = new List<Card>();

        #region Instance Handler

        private static TurnHandler _instance = null;

        private TurnHandler()
        {
        }

        public static TurnHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new TurnHandler();
                }
                return _instance;
            }
        }
        
        #endregion

        /// <summary>
        /// End this turn 
        /// </summary>
        internal void TurnEnd() => CurrentTurnNumber += 1;

        /// <summary>
        /// Starts phase 1
        /// </summary>
        internal void PhaseOne(Player p1, Player p2, int cardsToDraw)
        {
            p1.PlayerDeck.ShuffleCards();
            p2.PlayerDeck.ShuffleCards();

            for (int i = 0; i < cardsToDraw; i++)
            {
                if (cardsToDraw <= p1.MaxHandSize)
                {
                    var temp1 = p1.PlayerDeck.DrawCard();
                    P1CardsInHand.Add(temp1);
                }

                if (cardsToDraw <= p2.MaxHandSize)
                {
                    var temp2 = p2.PlayerDeck.DrawCard();
                    P2CardsInHand.Add(temp2);
                }
            }

            p1.ResetMana(CurrentTurnNumber);
            p2.ResetMana(CurrentTurnNumber);

            Console.WriteLine($"\n\nTurn nº{CurrentTurnNumber}");
            PlayCards(p1, P1CardsInHand, 1);
            PlayCards(p2, P2CardsInHand, 2);
            Viewer.Instance.DrawCardsOnScreen(p1, p2);
            PhaseTwo(p1, p2);
        }

        /// <summary> The PlayCards function is the main function that allows a player to play cards.        
        /// It takes in two parameters: a Player object and an integer representing the number of cards in hand.
        /// The function then calls on Viewer's ShowPlayerCardsInHand method, which displays all of the player's
        /// cards on screen and allows the player to play them by calling the InputListener.</summary>
        /// <param name="player"> The player who is playing the cards</param>
        /// <param name="cardsInHand"> The cards in the player's hand</param>
        /// <param name="pNumber"> The player's number/order, used to determine which player's turn it is.</param>
        /// <returns> The card(s) that the player has chosen to play</returns>
        private static void PlayCards(Player player, List<Card> cardsInHand, int pNumber)
        {
            Console.WriteLine($"{player.Name}'s turn.\n");
            Viewer.ShowPlayerCardsInHand(cardsInHand, player);
            Viewer.DrawInstructionsOnScreen();
            Viewer.Instance.InputListener(pNumber, player);
        }

        /// <summary> The PhaseTwo function is the second phase of a turn. It calculates damage between all cards in play,
        /// and then displays it to the console.</summary>
        private void PhaseTwo(Player p1, Player p2)
        {
            List<Card> allies = new List<Card>(); //representing p1's cards
            List<Card> enemies = new List<Card>(); //representing p2's cards

            foreach (Card c in Viewer.Instance.CardsOnScreen)
            {
                if (c.InDeck.Owner == p1)
                {
                    if (c.isDead)
                    {
                        allies.Remove(c);
                        Viewer.Instance.CardsOnScreen.Remove(c);
                    }
                    else
                    { 
                        allies.Add(c);
                    }
                }
                else
                {
                    if (c.isDead)
                    {
                        enemies.Remove(c);
                        Viewer.Instance.CardsOnScreen.Remove(c);
                    }
                    else
                    {
                        enemies.Add(c);
                    }
                    
                }
            }

            //int totalCount = allies.Count > enemies.Count ? allies.Count : enemies.Count;
            int totalCount = Math.Max(allies.Count, enemies.Count);

            for (int i = 0; i < totalCount; i++)
            {
                if (i < allies.Count && i < enemies.Count) // Check if both Allies and Enemies have cards at the current index
                {
                    bool allyWon = CalculateDamage(allies[i], enemies[i], allies);
                    bool enemyWon = CalculateDamage(enemies[i], allies[i], enemies);
                    
                    Console.WriteLine($"{allies[i].Name} attacks {enemies[i].Name}.");
                    Console.WriteLine($"{enemies[i].Name} attacks {allies[i].Name}.\n");

                    allies[i]?.CardHealthCheck();
                    enemies[i]?.CardHealthCheck();
                    
                    // If the current card wins the battle without dying, it keeps battling instead of switching to the next one
                    if (allyWon && allies[i].Dp > 0)
                    {
                        continue;
                    }
                    else if (enemyWon && enemies[i].Dp > 0)
                    {
                        continue;
                    }
                }
                else if (i < allies.Count) // if only Allies has a card at the current index
                {
                    p2.TakeDamage(allies[i].Ap);
                    Console.WriteLine($"{allies[i].Name} attacks {p2.Name}.");
                    Console.WriteLine($"{p2.Name} now has {p2.Hp} health.\n");
                }
                else if (i < enemies.Count) // Only Enemies have a card at the current index
                {
                    p1.TakeDamage(enemies[i].Ap);
                    Console.WriteLine($"{enemies[i].Name} attacks {p1.Name}.");
                    Console.WriteLine($"{p1.Name} now has {p1.Hp} health.\n");
                }

            }
            
            Console.WriteLine($"{p1.Name}'s new health: {p1.Hp}");
            Console.WriteLine($"{p2.Name}'s new health: {p2.Hp}");
        }

        /// <summary> The CalculateDamage function takes two cards as parameters, and calculates the damage that will
        /// be dealt to the attacked card. If the damage is greater than or equal to 0, then it deals that much damage
        /// to the attacked card. Otherwise, if it's less than 0 (meaning that there was a positive difference between
        /// AP and DP), then we deal no damage but instead set InPlay = false for this card and remove it from Play.
        /// </summary>
        /// <param name="attacker"> The card that is attacking</param>
        /// <param name="attacked"> The card that is being attacked.</param>
        /// <param name="hand">The hand the attacker card is from.</param>
        /// <returns> True if the attacking card wins the battle without dying, false otherwise.</returns>
        private static bool CalculateDamage(Card attacker, Card attacked, List<Card> hand)
        {
            int dmg = attacker.Ap - attacked.Dp;

            if (dmg >= 0)
            {
                attacked.Damage(dmg);

                // If the attacked card is killed, check if there is a next card on the index
                if (attacked.Dp <= 0)
                {
                        int nextIndex = hand.IndexOf(attacked) + 1;
                        if (nextIndex < hand.Count)
                        {
                            Card nextCard = hand[nextIndex];
                            int excessDamage = Math.Max(0, -attacked.Dp);
                            CalculateDamage(attacker, nextCard, hand);
                            nextCard.Damage(excessDamage);
                        }
                        else
                        {
                            attacked.InDeck.Owner.TakeDamage(Math.Max(0, -attacked.Dp));
                        }
                }

                return true;
            }

            return false;
        }
    }
}