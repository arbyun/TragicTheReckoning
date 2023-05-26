namespace TragicTheReckoningGame
{
    public class TurnHandler
    {
        public int CurrentTurnNumber;

        /// <summary>
        /// 
        /// </summary>
        void TurnEnd() => CurrentTurnNumber += 1;

        /// <summary>
        /// 
        /// </summary>
        void PhaseOne()
        {
            
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