using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.ExceptionServices;
using System.Text;
using System.Threading.Tasks;

namespace PokerStatsProj
{
    internal class Dealer
    {
        public List<string> DeckOfCards { get; set; }

        public Dealer() {


            string[] suitsList = {"S", "H", "C", "D"};
            string[] numberingList = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K" };

            DeckOfCards = new List<string>();

            for (int i = 0; i < numberingList.Length; i++)
            {
                for (int j = 0; j < suitsList.Length; j++)
                {
                    DeckOfCards.Add(numberingList[i] + suitsList[j]);
                }
            }
        }


        /*public static void Shuffle(this Random rng, string[] deck) //Stolen from https://stackoverflow.com/questions/108819/best-way-to-randomize-an-array-with-net
        {
            int n = deck.Length;
            while (n > 1)
            {
                int k = rng.Next(n--);
                string temp = deck[n];
                deck[n] = deck[k];
                deck[k] = temp;
            }
        } */ //NIIIICK I NEED HELP


        public string[,] Deal(int numbOfPlayers)
        {
            // Shuffle();
            //Above fxn isn't made yet

            string[,] hands = new string[numbOfPlayers, 2];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < numbOfPlayers; j++)
                {
                    hands[j,i] = DeckOfCards[i+j]; //"[j,i]" Gives each player one card at a time by convention
                }
            }
            return hands;
        }

        public string[] DealFlopTurnRiver(int numbOfPlayers)
        {
            int n = numbOfPlayers;
            string[] communityCards = new string[5];

            for (int i = 0; i < 3; i++)
            {
                communityCards[i] = DeckOfCards[n*2 + i + 1]; //The "+1" indicates a "burned" card by convention. Similar convention is followed below
            }
            communityCards[3] = DeckOfCards[n*2 + 5];
            communityCards[4] = DeckOfCards[n*2 + 7];

            return communityCards;
        }


    }
}
