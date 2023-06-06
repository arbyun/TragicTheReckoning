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
                    allies.Add(c);
                }
                else
                {
                    enemies.Add(c);
                }
            }

            int totalCount = allies.Count > enemies.Count ? allies.Count : enemies.Count;

            for (int i = 0; i < totalCount; i++)
            {
                if (i < allies.Count && i < enemies.Count) // Check if both Allies and Enemies have cards at the current index
                {
                    CalculateDamage(allies[i], enemies[i]);
                    Console.WriteLine($"{allies[i]} attacks {enemies[i]}.");
                    CalculateDamage(enemies[i], allies[i]);
                    Console.WriteLine($"{enemies[i]} attacks {allies[i]}.");
                }
                else if (i < allies.Count) // if only Allies has a card at the current index
                {
                    p2.TakeDamage(allies[i].Ap);
                    Console.WriteLine($"{allies[i]} attacks {p2.Name}.");
                    Console.WriteLine($"{p2.Name} now has {p2.Hp} health.");
                }
                else if (i < enemies.Count) // Only Enemies have a card at the current index
                {
                    p1.TakeDamage(enemies[i].Ap);
                    Console.WriteLine($"{enemies[i]} attacks {p1.Name}.");
                    Console.WriteLine($"{p1.Name} now has {p1.Hp} health.");
                }

                allies[i].CardHealthCheck();

                if (allies[i].Dp <= 0)
                {
                    allies.Remove(allies[i]);
                }

                if (enemies[i].Dp <= 0)
                {
                    enemies.Remove(enemies[i]);
                }
                
                enemies[i].CardHealthCheck();
            }
        }

        
        /// <summary> The CalculateDamage function takes two cards as parameters, and calculates the damage that will
        /// be dealt to the attacked card. If the damage is greater than or equal to 0, then it deals that much damage
        /// to the attacked card. Otherwise, if it's less than 0 (meaning that there was a positive difference between
        /// AP and DP), then we deal no damage but instead set InPlay = false for this card and remove it from Play.
        /// </summary>
        /// <param name="attacker"> The card that is attacking</param>
        /// <param name="attacked"> The card that is being attacked.</param>
        /// <returns> The damage dealt to the attacked card.</returns>
        private static void CalculateDamage(Card attacker, Card attacked)
        {
            int dmg = attacked.Dp - attacker.Ap;

            if (dmg > attacked.Dp)
            {
                attacked.InDeck.Owner.TakeDamage(dmg - attacked.Dp);
                Viewer.Instance.CardsOnScreen.Remove(attacked);
            }
            else
            {
                attacked.Damage(attacker.Ap);
            }
        }
    }
}