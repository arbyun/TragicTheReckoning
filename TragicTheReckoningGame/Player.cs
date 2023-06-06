namespace TragicTheReckoningGame
{
    public class Player
    {
        public readonly string Name;
        public int Hp;
        public readonly int MaxHandSize;
        public int Mana;
        public readonly Deck PlayerDeck;
        
        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="name">Nome do jogador</param>
        /// <param name="playerDeck">O baralho do jogador</param>
        public Player(string name, Deck playerDeck)
        {
            Name = name;
            Hp = 10;
            MaxHandSize = 6;
            PlayerDeck = playerDeck;
            PlayerDeck.Owner = this;
        }

        /// <summary>
        /// Função para a mana
        /// </summary>
        /// <param name="currentTurn">Turno atual</param>
        /// <returns>player mana</returns>
        public int ResetMana(int currentTurn)
        {
            if (currentTurn <= 4)
            {
                return Mana = currentTurn;
            }
            
            return Mana = 5;
        }

        /// <summary>
        /// Função para levar dano
        /// </summary>
        /// <param name="damage">Dano que o jogador leva</param>
        public void TakeDamage(int damage) => Hp -= damage;
    }
}