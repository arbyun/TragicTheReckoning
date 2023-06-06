using System;
using System.Collections.Generic;

namespace TragicTheReckoningGame
{
    sealed class Viewer
    {
        internal readonly List<Card> CardsOnScreen = null;

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
        internal static void ShowPlayerCardsInHand(List<Card> cards)
        {
            Console.WriteLine("\nCards in your hand:\n\n");
            
            foreach (Card card in cards)
            {
                Console.WriteLine(card.ToString() + "\n");
            }
            
            Console.WriteLine("\n");
        }

        /// <summary> Draws the title of the game on screen.</summary>
        internal static void DrawIntroductionOnScreen()
        {
            string t = "TRAGIC! The Reckoning";
            Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (t.Length / 2)) + "}", t));
            Console.WriteLine("\n\n\n\n");
        }

        /// <summary> Visually draws the games' instructions on the screen.</summary>
        internal static void DrawInstructionsOnScreen()
        {
            Console.WriteLine("Please write the ID number of the card you wish to play. Keep in mind that " +
                              "you won't be able to play a card if you don't have enough mana for it."); 
            
            Console.WriteLine("\nIf you are done with your turn, please write 'next'.");
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
            
            foreach (var c in inHand)
            {
                if (input == c.Id.ToString())
                {
                    if (player.Mana >= c.Cost)
                    {
                        CardsOnScreen.Add(c);
                        player.Mana -= c.Cost;
                        PlayCard(c);
                    }
                    else
                    {
                        Console.WriteLine("You don't have enough mana to draw that card.");
                    }
                }
                else
                {
                    Console.WriteLine($"'{input}' is not a valid ID or the card isn't available in your hands.");
                }
            }
        }

        /// <summary> Visual indicator of the player having played a card.</summary>
        /// <param name="card"> the card that is being played. </param>
        private static void PlayCard(Card card)
        {
            Console.WriteLine($"{card.Name} was played.");
        }

        /// <summary> Visually draws the cards on screen.</summary>
        internal void DrawCardsOnScreen()
        {
            Console.WriteLine("-------- BATTLEFIELD -------\n\n");
            
            foreach (Card c in CardsOnScreen)
            {
                Console.WriteLine(c.ToString() + " played by " + c.InDeck.Owner);
            }
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
                Console.WriteLine("Please input player 1's name.");
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