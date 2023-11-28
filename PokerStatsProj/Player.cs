namespace PokerStatsProj
{
    internal class Player
    {

        public string[] PocketCards { get; set; }
        public int[] Statistics { get; set; }

        public Player()
        {
            Statistics = new int[4];
            PocketCards = new string[2];
        }

        public void CalcStats(int numbOfPlayers, string[] communityCards)
        {
            //Placeholder stat-calc
            for (int i = 0; i < 4; i++)
            {
                Statistics[i] = i;
            }
        }

    }
}
