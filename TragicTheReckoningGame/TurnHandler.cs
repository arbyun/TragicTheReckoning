namespace TragicTheReckoningGame
{
    public class TurnHandler
    {
        public int CurrentTurnNumber;

        /// <summary>
        /// Ending turn 
        /// </summary>
        void TurnEnd() => CurrentTurnNumber += 1;

        /// <summary>
        /// Starting phase 1
        /// </summary>
        void PhaseOne(Player p1, Player p2, int cardsToDraw)
        {
            p1.PlayerDeck.ShuffleCards();
            p2.PlayerDeck.ShuffleCards();

            for (i = 1, i >= cardsToDraw, i++) {
                p1.PlayerDeck.DrawCard();
                p2.PlayerDeck.DrawCard();
            }

            p1.ResetMana(CurrentTurnNumber);
            p2.ResetMana(CurrentTurnNumber);

            Console.WriteLine($"Turn nº (CurrentTurnNumber)");

        }

        /// <summary>
        /// 
        /// </summary>
        void PhaseTwo()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="attacker"></param>
        /// <param name="attacked"></param>
        void CalculateDamage(Card attacker, Card attacked)
        {
            int dmg = attacked.Dp - attacker.Ap;

            if (dmg > attacked.Dp)
            {
                attacked.InDeck.Owner.TakeDamage(dmg - attacked.Dp);
                attacked.InPlay = false;
            }
            else
            {
                attacked.Damage(attacker.Ap);
            }
        }
    }
}