namespace TragicTheReckoningGame
{
    public class Card
    {
        public readonly string Name;
        public readonly int Cost;
        public readonly int Ap;
        public int Dp;
        public Deck InDeck; //the deck the card is in
        public bool InPlay;

        /// <summary>
        /// Construtor das cartas contendo todos os parametros necessários
        /// </summary>
        /// <param name="name"></param>
        /// <param name="cost"></param>
        /// <param name="ap"></param>
        /// <param name="dp"></param>
        /// <param name="inDeck"></param>
        public Card(string name, int cost, int ap, int dp, Deck inDeck=null)
        {
            Name = name;
            Cost = cost;
            Ap = ap;
            Dp = dp;
            InDeck = inDeck;
        }
        
        /// <summary>
        /// Representação das cartas no ecrã
        /// </summary>
        /// <returns></returns>
        public override string ToString() => $"{Name}/{Cost}/{Ap}/{Dp}";

        //Function to make the damage be delt to defence 
        public void Damage(int damage) => Dp -= damage;
    }
}