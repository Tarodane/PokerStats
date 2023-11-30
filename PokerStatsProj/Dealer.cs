using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace PokerStatsProj
{
    internal class Dealer
    {
        public List<Card> DeckOfCards { get; set; }

        public Dealer()
        {
            char[] suitsList = {'S', 'H', 'C', 'D'};
            char[] numberingList = { 'A', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K' };

            DeckOfCards = new List<Card>();

            for (int i = 0; i < suitsList.Length; i++)
            {
                for (int j = 0; j < numberingList.Length; j++)
                {
                    DeckOfCards.Add(new Card(numberingList[j], suitsList[i]));
                }
            }
        }


        public void Shuffle() //Fisher-Yates Algorithm
        {
            Random rng = new(DateTime.Now.Nanosecond);
            for (int i = DeckOfCards.Count - 1; i >= 0; i--)
            {
                var k = rng.Next(i+1);
                (DeckOfCards[i], DeckOfCards[k])=(DeckOfCards[k], DeckOfCards[i]); //VS Suggested this, kinda cool
            }
        }

        public Card[,] Deal(int numbOfPlayers)
        {
            Shuffle();

            Card[,] holeCards = new Card[numbOfPlayers, 2];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < numbOfPlayers; j++)
                {
                    holeCards[j,i] = DeckOfCards[i+j]; //"[j,i]" Gives each player one card at a time by convention
                }
            }
            return holeCards;
        }

        public List<Card> DealFlopTurnRiver(int numbOfPlayers)
        {
            int n = numbOfPlayers;
            List<Card> communityCards = new List<Card>();

            for (int i = 0; i < 3; i++)
            {
                communityCards.Add(DeckOfCards[n*2 + i + 1]); //The "+1" indicates a "burned" card by convention. Similar convention is followed below
            }
            communityCards.Add(DeckOfCards[n*2 + 5]);
            communityCards.Add(DeckOfCards[n*2 + 7]);

            return communityCards;
        }


    }
}
