namespace TragicTheReckoningGame
{
    public class Player
    {
        public string Name;
        public int Hp;
        public int MaxHandSize;
        public int Mana;
        public Deck PlayerDeck;
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="playerDeck"></param>
        public Player(string name, Deck playerDeck)
        {
            Name = name;
            Hp = 10;
            MaxHandSize = 6;
            PlayerDeck = playerDeck;
            PlayerDeck.Owner = this;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentTurn"></param>
        /// <returns></returns>
        public int ResetMana(int currentTurn)
        {
            if (currentTurn >= 4)
            {
                return Mana = currentTurn;
            }
            
            return Mana = 5;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="damage"></param>
        public void TakeDamage(int damage) => Hp -= damage;
    }
}