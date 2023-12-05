using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;

namespace PokerStatsProj
{
    internal class GameMaster
    {
        public int NumberOfPlayers { get; set; }
        public List<Card> CommunityCards { get; set; }
        public Dealer Dealer { get; set; }
        public Player[] Players { get; set; } //Order of players maintains ranks

        public GameMaster(int numbOfPlayers = 4)
        {
            CommunityCards = new List<Card>();
            if (numbOfPlayers < 1)
            {
                //Throw error?
            }
            NumberOfPlayers = numbOfPlayers;

            Dealer = new Dealer();
            Players = new Player[NumberOfPlayers];
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
            Players[k].CalcStats(NumberOfPlayers, CommunityCards);
            Console.WriteLine("Player " + (k));
            for (int j = 0; j < 4; j++)
            {
                Console.WriteLine("Stat " + j + ": " + Players[k-1].Statistics[j]); //Can I make a hash table or a switch case to have better stats displayed? e.g. preflop, post-flop, etc.
            }
        }

        public void DisplayAllPlayerStats()
        {
            for (int i = 0; i < NumberOfPlayers; i++)
            {
                Players[i].CalcStats(NumberOfPlayers, CommunityCards);
                Console.WriteLine("Player " + (i+1));
                for (int j = 0; j < 4; j++)
                {
                    Console.WriteLine("Stat " + j + ": " + Players[i].Statistics[j]); //Can I make a hash table or a switch case to have better stats displayed? e.g. preflop, post-flop, etc.
                }
                Console.WriteLine();
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

        public class Comparison
        {

        }

    }
}