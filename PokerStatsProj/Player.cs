namespace PokerStatsProj
{
    internal class Player
    {

        public Card[] HoleCards { get; set; }
        public int[] Statistics { get; set; }
        public List<Hand> Hands { get; set; }
        public Hand BestHand { get; set; }
        public int Money { get; set; }

        public Player()
        {
            Statistics = new int[4];
            HoleCards = new Card[2];
            Hands = new List<Hand>();
            BestHand = new Hand();
        }

        public void CalcStats(int numbOfPlayers, List<Card> communityCards)
        {
            //Placeholder stat-calc
            for (int i = 0; i < 4; i++)
            {
                Statistics[i] = i/numbOfPlayers;
            }
        }

        public void SetHandsList(List<Card> cards)
        {
            if (cards.Count < 5)
                return;
            Hands = CardLibrary.ComboFunction(cards.Count, 5, cards); //Hard code 5 because we don't want to use this on less than a full hand
        }

    }
}