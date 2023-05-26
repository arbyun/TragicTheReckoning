using System;

namespace TragicTheReckoningGame
{
    class Program
    {
        private static bool _gameOver = false;
        private string gameWinner;

        public static Player PlayerOne;
        public static Player PlayerTwo;

        static void Main(string[] args)
        {
            Console.WriteLine("Write here an intro or something");
            CreatePlayer(1);
            CreatePlayer(2);
            
            do
            {
                
            } while (!_gameOver);
        }

        static void CreatePlayer(int playerNumber)
        {
            if (playerNumber == 1)
            {
                Console.WriteLine("Please input player 1's name.");
                string p1Name = Console.ReadLine();
                
                PlayerOne = new Player(p1Name, new Deck(20));
            }
            else
            {
                Console.WriteLine("Please input player 1's name.");
                string p2Name = Console.ReadLine();
                
                PlayerTwo = new Player(p2Name, new Deck(20));
            }
        }

        /// <summary>
        /// Evaluates if a card is dead and if so, cleans it from scene.
        /// </summary>
        /// <param name="card"></param>
        void CleanFromScreen(Card card)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        void EvaluatePlayerHp(Player player)
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="player"></param>
        void Die(Player player)
        {
            _gameOver = true;
            DisplayGameWinner();
        }

        /// <summary>
        /// 
        /// </summary>
        void DisplayGameWinner()
        {
            Console.WriteLine($"{gameWinner} won the game!");
        }
    }
}
