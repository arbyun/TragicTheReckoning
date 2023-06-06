using System;
using System.Collections.Generic;

namespace TragicTheReckoningGame
{
    sealed class Viewer
    {
        internal readonly List<Card> CardsOnScreen = new List<Card>();

        #region Instance Handler

        private static Viewer _instance = null;

        private Viewer()
        {
        }

        public static Viewer Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Viewer();
                }
                return _instance;
            }
        }
        
        #endregion

        /// <summary> Visually displays the current cards in a player's hand.</summary>
        /// <param name="cards"> The cards in the player's hand.</param>
        /// <returns> A list of cards</returns>
        internal static void ShowPlayerCardsInHand(List<Card> cards, Player player)
        {
            Console.WriteLine("Cards in your hand:\n\n");
            
            foreach (Card card in cards)
            {
                Console.WriteLine(card.ToString() + "\n");
            }
            
            Console.WriteLine("\n");
            Console.WriteLine($"You have {player.Mana} mana.");
        }

        /// <summary> Draws the title of the game on screen.</summary>
        internal static void DrawIntroductionOnScreen()
        {
            string t = "TRAGIC! The Reckoning";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (t.Length / 2)) + "}", t));
            Console.WriteLine("\n\n");
        }

        /// <summary> Visually draws the games' instructions on the screen.</summary>
        internal static void DrawInstructionsOnScreen()
        {
            Console.WriteLine("\nPlease write the ID number of the card you wish to play. Keep in mind that " +
                              "you won't be able to play a card if you don't have enough mana for it."); 
            
            Console.WriteLine("If you are done with your turn, please write 'next'.\n");
        }

        /// <summary> The InputListener function listens for input from the player and then checks if it's a valid card
        /// ID. If it is, the function will check if the player has enough mana to play that card. If they do,
        /// then they can play that card.</summary>    
        /// <param name="playerNumber"> Which players turn it is.</param>
        /// <param name="player"> The player who is playing the card.</param>
        internal void InputListener(int playerNumber, Player player)
        {
            string input = Console.ReadLine();

            if (input == "next" || input == "'next'")
            {
                if (playerNumber == 2)
                {
                    TurnHandler.Instance.TurnEnd();
                }

                if (playerNumber == 1)
                {
                    return;
                }
            }

            List<Card> inHand = null;

            if (playerNumber == 1)
            {
                inHand = TurnHandler.Instance.P1CardsInHand;
            }
            else if (playerNumber == 2)
            {
                inHand = TurnHandler.Instance.P2CardsInHand;
            }
            else
            {
                return;
            }
            
            PlayCardLoop(player, inHand, playerNumber);
        }

        /// <summary> The PlayCardLoop function is a loop that allows the player to play cards until they run out of mana
        /// or type "next"</summary>
        /// <param name="player"> The player who is playing the card</param>
        /// <param name="inHand"> The list of cards in the player's hand.</param>
        /// <param name="playerNumber"> Used to determine which player's hand the card should be removed from. </param>
        private void PlayCardLoop(Player player, List<Card> inHand, int playerNumber)
        {
            var input = Console.ReadLine();

            do
            {
                if (input == "next")
                {
                    break;
                }
                
                Card c = inHand.Find(card => card.Id.ToString() == input);

                if (c != null) // Check if the card is found
                {
                    if (player.Mana >= c.Cost)
                    {
                        CardsOnScreen.Add(c);
                        player.Mana -= c.Cost;
                        PlayCard(c);

                        if (playerNumber == 1)
                        {
                            TurnHandler.Instance.P1CardsInHand.Remove(c);
                        }
                        else
                        {
                            TurnHandler.Instance.P2CardsInHand.Remove(c);
                        }
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough mana to play that card.");
                    }
                }
                else
                {
                    Console.WriteLine("Card not found in your hand.");
                }

                input = Console.ReadLine(); // Read the next input
            } while (player.Mana !>= 0);
        }

        /// <summary> Visual indicator of the player having played a card.</summary>
        /// <param name="card"> the card that is being played. </param>
        private static void PlayCard(Card card)
        {
            Console.WriteLine($"{card.Name} was played.");
        }

        /// <summary> Visually draws the cards on screen.</summary>
        internal void DrawCardsOnScreen(Player p1, Player p2)
        {
            Console.WriteLine("\n\n-------- BATTLEFIELD -------\n\n");
            
            Console.WriteLine($"Player 1: {p1.Name} | {p1.Hp} HEALTH\n");
            Console.WriteLine($"Player 2: {p2.Name} | {p2.Hp} HEALTH\n");
            
            foreach (Card c in CardsOnScreen)
            {
                Console.WriteLine(c.ToString() + " played by " + c.InDeck.Owner.Name);
            }
            
            TurnHandler.Instance.TurnEnd();
        }

        /// <summary> Creates a new player object and returns it to the caller.</summary>
        /// <param name="playerNumber"> The number/order of this new player.</param>
        /// <returns> A new player object.</returns>
        internal static Player CreatePlayer(int playerNumber)
        {
            if (playerNumber == 1)
            {
                Console.WriteLine("Please input player 1's name.");
                string p1Name = Console.ReadLine();
                
                return new Player(p1Name, new Deck(20));
            }
            else
            {
                Console.WriteLine("Please input player 2's name.");
                string p2Name = Console.ReadLine();
                
                return new Player(p2Name, new Deck(20));
            }
        }
        
        /// <summary> The DisplayGameWinner function displays the winner of the game and prompts the user to restart
        /// or exit.</summary>        
        /// <param name="gameWinner"> The winner of the game.</param>
        internal static void DisplayGameWinner(Player gameWinner)
        {
            Console.WriteLine($"{gameWinner.Name} won the game!\n\n\nPress any key to restart or escape to exit.");
            ConsoleKeyInfo k = Console.ReadKey();

            if (k.Key is ConsoleKey.Escape)
            {
                Console.Clear();
                System.Environment.Exit(0);
            }
            else
            {
                Program.Main(new []{""});
            }
        }
    }
}