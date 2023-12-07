namespace PokerStatsProj
{
    public class Player
    {
        public Card[] HoleCards { get; set; }
        public List<Hand> Hands { get; private set; }
        public Hand BestHand { get; private set; }
        public PlayerStatistics Statistics { get; set; }
        public float Money { get; private set; }

        public Player()
        {
            Statistics = new PlayerStatistics();
            HoleCards = new Card[2];
            Hands = new List<Hand>();
            BestHand = new Hand();
            Money = 0;
        }

        public void GainMoney(float money)
        {
            Money += money;
        }

        public void LoseMoney(float money)
        {
            Money -= money;
        }

        public void SetNewHandsAndBestHand(List<Card> communityCardsByRound)
        {
            List<Card> PocketHandPlusCommunity = communityCardsByRound;
            PocketHandPlusCommunity.Add(HoleCards[0]);
            PocketHandPlusCommunity.Add(HoleCards[1]);

            Hands = CardLibrary.ComboFunction(PocketHandPlusCommunity.Count, 5, PocketHandPlusCommunity); //Hard code 5 because we don't want to use this on less than a full hand
            
            SetNewBestHand();
        }

        internal void SetNewBestHand()
        {
            foreach (Hand hand in Hands)
            {
                if (BestHand != BestHand.CalcBetterHand(hand))
                {
                    BestHand = hand;
                }
            }
        }

        public class PlayerStatistics
        {
            public int Ranking { get; private set; }
            public float ChanceToWin { get; set; }

            public PlayerStatistics()
            {
                Ranking = 0;
                ChanceToWin = 0;
                
            }

            public void PrintStatistics(int numbOfPlayers)
            {
                Ranking = Ranking +1-1; //Holder to remove warnings
                Console.WriteLine("Player.PlayerStatistics.PrintStatistics is an unfinished function");
            }

            public void SetRanking(int ranking)
            {
                Ranking = ranking;
            }
        }


    }
}