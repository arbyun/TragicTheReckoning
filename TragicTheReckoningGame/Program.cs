using System;

namespace TragicTheReckoningGame
{
    internal static class Program
    {
        private static bool _gameOver = false;
        private static Player _gameWinner;

        private static Player _playerOne;
        private static Player _playerTwo;

        public static void Main(string[] args)
        {
            Viewer.DrawIntroductionOnScreen();
            _playerOne = Viewer.CreatePlayer(1);
            _playerTwo = Viewer.CreatePlayer(2);

            do
            {
                if (TurnHandler.Instance.CurrentTurnNumber <= 1)
                {
                    TurnHandler.Instance.PhaseOne(_playerOne, _playerTwo, 6);
                }
                else
                {
                    TurnHandler.Instance.PhaseOne(_playerOne, _playerTwo, 1);
                }

                EvaluatePlayerHp(_playerOne);
                EvaluatePlayerHp(_playerTwo);
                EvaluateCardsLeft(_playerOne);
                EvaluateCardsLeft(_playerTwo);

            } while (!_gameOver);

            Viewer.DisplayGameWinner(_gameWinner);
        }

        /// <summary> The EvaluatePlayerHp function checks to see if the player's HP is less than or equal to 0.
        /// If it is, then the Die function will be called.</summary>
        /// <param name="player"> The player that is being evaluated.</param>
        private static void EvaluatePlayerHp(Player player)
        {
            if (player.Hp <= 0)
            {
                Die(player);
            }
        }

        private static void EvaluateCardsLeft(Player player)
        {
            if (!player.PlayerDeck.CardsLeft)
            {
                Die(player);
            }
        }

        
        /// <summary> The Die function is called when the player's health reaches 0. It sets the _gameOver variable to
        /// true, and calls UI.DisplayGameWinner()</summary>
        /// <param name="player"> The player to die.</param>
        private static void Die(Player player)
        {
            _gameOver = true;

            _gameWinner = player == _playerOne ? _playerTwo : _playerOne;
        }

        
    }
}
