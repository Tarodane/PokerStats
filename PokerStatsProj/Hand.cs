﻿using System;
using System.Data;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PokerStatsProj
{

    [Flags]
    public enum PokerHand
    { 
        HighCard = 0,
        Pair,
        TwoPair,
        ThreeKind,
        Straight,
        Flush,
        FullHouse,
        FourKind,
        StraightFlush,
        RoyalFlush = 9
    }

    //I want this class to be designed to hold exactly 5 cards. Most of the *proper*/*expected* functionality will degrade if there are not 5 cards
    public class Hand
	{
        public List<Card> Cards { get; set; }
        public (int, List<int>) HandRank { get; private set; }

		public Hand()
		{
			Cards = new List<Card>();
        }
		
		public Hand(Card card)
		{
            Cards = new List<Card> { card };
            
        }

        public Hand(List<Card> cards)
        {
            Cards = cards;
            Cards.Sort();
            if (cards.Count == 5)
                HandRank = CalcHandRank(Cards);
        }

        public void SortCards()
		{
			Cards.Sort();
		}

		public void Add(Card card)
		{
			Cards.Add(card);
		}

		public void Clear()
		{
			Cards.Clear();
		}
        
        public static Card HighCard(List<Card> cards)
        {
            Card holderCard = cards[0];
            for (int i = 1; i < cards.Count; i++)
            {
                if (holderCard < cards[i])
                    holderCard = cards[i];
            }
            return holderCard;
        }

        internal void SetHandRank()
        {
            HandRank = CalcHandRank(Cards);
        }

        internal (int, List<int>) CalcHandRank()
        {
            return CalcHandRank(Cards);
        }

        public static (int, List<int>) CalcHandRank(List<Card> cards)
        {
            List<int> distinctValsList = ListHandValues(cards).Distinct().ToList();
            distinctValsList.Sort();
            distinctValsList.Reverse();

            if (distinctValsList.Count == 0) //In case we're given an empty hand
                return (-1, new List<int> { 0 });

            if (distinctValsList.Count > 4) {
                bool flushBool = true;
                bool straightBool = true;

                for (int i = 0; i < cards.Count - 1; i++) //Inneficient programming, but passable at this small scale (n<=5), but any of these methods are inherently inefficient imo
                {
                    if (distinctValsList[i] != (distinctValsList[i+1] + 1)) //Plus 1 because distinctValsList is in descending order
                        straightBool = false;

                    if (cards[i].Suit != cards[i+1].Suit)
                        flushBool = false;
                }


                var holderList = new List<int>{ 14, 5, 4, 3, 2 }; //Still need to code in that this specific situation considers ace as a 1, and 5 is the high card 

                if (distinctValsList.All(x => holderList.Contains(x)))
                {
                    straightBool = true;
                    //distinctValsList[holderList.Count - 1]
                }

                if (straightBool || flushBool)
                {
                    //Straight = 4, Flush = 5, Straight Flush = 8, Royal Flush = 9
                    return (IndicatorFxn(straightBool)*(int)PokerHand.Straight + IndicatorFxn(flushBool)*(int)PokerHand.Flush - IndicatorFxn(flushBool && straightBool && (HighCard(cards).Value != 'A')),
                       distinctValsList); //Returns all 5 cards w/ high card first
                }
                return ((int)PokerHand.HighCard, distinctValsList); // Technically unnecessary if the above if-check wasn't there and all of its nested code ran normally
            }

            //I guess we break convention here and make some hands of size 2, 3, and 4
            if (distinctValsList.Count == 2) //Either a full house or a quad
            {
                List<Hand> quadCheck = CardLibrary.ComboFunction(0, 4, cards);
                foreach (Hand i in quadCheck)
                {
                    if (i.Cards[0].Value == i.Cards[1].Value && i.Cards[1].Value == i.Cards[2].Value && i.Cards[2].Value == i.Cards[3].Value)
                    {
                        if (distinctValsList[0] != CardValToInt(i.Cards[0]))
                            distinctValsList.Reverse();
                        return ((int)PokerHand.FourKind, distinctValsList); //returns only the quad's card and high card val
                    }
                }
            }

            if (distinctValsList.Count != 4)
            {
                List<Hand> tripCheck = CardLibrary.ComboFunction(0, 3, cards);
                foreach (Hand i in tripCheck)
                {
                    if (i.Cards[0].Value == i.Cards[1].Value && i.Cards[1].Value == i.Cards[2].Value)
                    {
                        if (distinctValsList.Count == 2)
                        {
                            if (distinctValsList[0] != CardValToInt(i.Cards[0]))
                                distinctValsList.Reverse();
                            return ((int)PokerHand.FullHouse, distinctValsList);
                        }
                        else
                        {
                            if (distinctValsList[0] != CardValToInt(i.Cards[0]))
                            {
                                distinctValsList.Remove(CardValToInt(i.Cards[0]));
                                distinctValsList.Insert(0, CardValToInt(i.Cards[0]));
                            }
                            return ((int)PokerHand.ThreeKind, distinctValsList);
                        }
                    }
                }
            }

            List<Hand> pairCheck = CardLibrary.ComboFunction(0, 2, cards);
            Card firstPairCheck = new();

            foreach (Hand i in pairCheck)
            {
                if (i.Cards[0].Value == i.Cards[1].Value)
                {
                    if (distinctValsList.Count == 4)
                    {
                        if (distinctValsList[0] != CardValToInt(i.Cards[0]))
                        {
                            distinctValsList.Remove(CardValToInt(i.Cards[0]));
                            distinctValsList.Insert(0, CardValToInt(i.Cards[0]));
                        }
                        return ((int)PokerHand.Pair, distinctValsList);
                    }
                    else if (firstPairCheck.Value != '\0')
                    {
                        List<int> pairOrder = new List<int> { CardValToInt(firstPairCheck), CardValToInt(i.Cards[0]) };
                        pairOrder.Sort();
                        pairOrder.Reverse();

                        foreach (int j in pairOrder)
                        {
                            distinctValsList.Remove(j);
                            distinctValsList.Insert(0, j);
                        }

                        return ((int)PokerHand.TwoPair, distinctValsList);
                    }
                    else
                        firstPairCheck = i.Cards[0];
                }
            }
            return (-2, new List<int> {0}); //This line *should* never run in theory
        }

        
        internal Hand CalcBetterHand(Hand otherHand)
        {
            return CalcBetterHand(this, otherHand);
        }

        public static Hand CalcBetterHand(Hand lhs, Hand rhs)
        {
            if (lhs.HandRank == (null, null) && lhs.Cards.Count == 5)
                lhs.SetHandRank();
            if (rhs.HandRank == (null, null) && rhs.Cards.Count == 5)
                rhs.SetHandRank();

            if (lhs.HandRank.Item1 == rhs.HandRank.Item1)
            {
                for (int i = 0; i < lhs.HandRank.Item2.Count; i++)
                {
                    if (lhs.HandRank.Item2[i] > rhs.HandRank.Item2[i])
                        return lhs;
                    else if (lhs.HandRank.Item2[i] < rhs.HandRank.Item2[i])
                        return rhs;
                }
                return lhs;
            }
            else if (lhs.HandRank.Item1 < rhs.HandRank.Item1)
                return rhs;

            return lhs;
        }



        public static List<int> ListHandValues(List<Card> cards)
        {
            /*List<int> result = new();
            foreach (Card card in cards)
            {
                result.Add(CardValToInt(card));
            }
            return result;
*/
            return cards.Select(x => CardValToInt(x)).ToList();
        }

        public static int CardValToInt(Card card)
        {
            switch (card.Value)
            {
                case '2': return 2;
                case '3': return 3;
                case '4': return 4;
                case '5': return 5;
                case '6': return 6;
                case '7': return 7;
                case '8': return 8;
                case '9': return 9;
                case 'T': return 10;
                case 'J': return 11;
                case 'Q': return 12;
                case 'K': return 13;
                case 'A': return 14;
            }
            return 0;
        }

        public static int IndicatorFxn(bool a) { if (a) { return 1; } return 0; }

        public static Hand ParseString(string s) //Use when given a string representing a hand
        {
            return new Hand(s.Split(',').Select(x => Card.ParseString(x)).ToList());
        }

        public static bool operator ==(Hand lhs, Hand rhs)
        {
            for (int i = 0; i < lhs.Cards.Count; i++)
            {
                if (lhs.Cards[i] != rhs.Cards[i])
                {
                    return false;
                }
            }
            return true;
        }

        public static bool operator !=(Hand lhs, Hand rhs)
        {
            for (int i = 0; i < lhs.Cards.Count; i++)
            {
                if (lhs.Cards[i] == rhs.Cards[i])
                {
                    return false;
                }
            }
            return true;
        }

        public override string ToString() 
		{
			return string.Join(",", Cards);
		}

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (ReferenceEquals(obj, null))
            {
                return false;
            }

            throw new NotImplementedException();
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}