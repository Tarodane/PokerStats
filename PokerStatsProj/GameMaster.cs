using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace PokerStatsProj
{
    public class GameMaster
    {
        public int NumberOfPlayers { get; set; }
        public Player[] Players { get; set; }
        public Dealer Dealer { get; private set; }
        public List<Card> CommunityCards { get; private set; }
        //public float MoneyPot <-- Implement should I decide to make this an actual game

        public GameMaster(int numbOfPlayers = 4)
        {
            if (numbOfPlayers < 1)
            {
                //Throw error?
            }
            NumberOfPlayers = numbOfPlayers;
            Dealer = new Dealer();
            CommunityCards = new List<Card>();
            Players = new Player[NumberOfPlayers];

            for (int i = 0; i < numbOfPlayers; i++)
            {
                Players[i].Statistics.SetRanking(i);
            }
        }

        public void GiveMoney(int player, float money)
        {
            Players[player].GainMoney(money);
        }

        public void TakeMoney(int player, float money)
        {
            Players[player].LoseMoney(money);
        }

        public void GiveHands()
        {
            Card[,] hands = Dealer.Deal(NumberOfPlayers);

            for (int i = 0; i < NumberOfPlayers; i++)
            {
                Players[i].HoleCards[0] = hands[i, 0];
                Players[i].HoleCards[1] = hands[i, 1];
            }
        }
        
        public void FlopTurnRiver()
        {
            CommunityCards = Dealer.DealFlopTurnRiver(NumberOfPlayers);
        }

        public void DisplayPlayerStats(int k) //As in, player 1 to NumberOfPlayers (NOT zero-indexed)
        {
            Players[k-1].Statistics.PrintStatistics(NumberOfPlayers);
        }

        public void DisplayAllPlayerStats()
        {
            foreach (Player player in Players)
            {
                player.Statistics.PrintStatistics(NumberOfPlayers);
            }
        }

        public void DisplayStatsByRanks() //TODO
        {
            foreach (Player player in Players)
            {
            }
            
        }

        public void NewPlay()
        {
            CommunityCards.Clear();
            Dealer.Shuffle();
            GiveHands();
            FlopTurnRiver();
            DisplayAllPlayerStats();
        }
    }
}