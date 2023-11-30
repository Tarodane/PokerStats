//using PokerStatsProj.Card Clearly I need help with the whole C# ecosystem, bc Card isn't being recognized

namespace PokerStatsProj
{
    internal class Player
    {

        public Card[] HoleCards { get; set; }
        public int[] Statistics { get; set; }

        public Player()
        {
            Statistics = new int[4];
            HoleCards = new Card[2];
        }

        public void CalcStats(int numbOfPlayers, List<Card> communityCards)
        {
            //Placeholder stat-calc
            for (int i = 0; i < 4; i++)
            {
                Statistics[i] = i;
            }
        }

    }
}
