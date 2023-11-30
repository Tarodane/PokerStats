using System;
namespace PokerStatsProj
{
	internal class Hand
	{
        public List<Card> Cards { get; set; }

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

        public static Hand ParseString(string s)
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


	}
}