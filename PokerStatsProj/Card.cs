using System;

namespace PokerStatsProj
{
	internal class Card: IComparable<Card>
	{
		public char Value { get; set; }
		public char Suit { get; set; }

		public Card(char value = 'A', char suit = 'S')
		{
			Value = value;
			Suit = suit;
		}

		public static bool operator ==(Card lhs, Card rhs)
		{
			if (lhs.Value == rhs.Value && lhs.Suit == rhs.Suit)
			{
				return true;
			}
			return false;
		}

        public static bool operator !=(Card lhs, Card rhs)
        {
            if (lhs.Value != rhs.Value || lhs.Suit != rhs.Suit)
            {
                return true;
            }
            return false;
        }

        public static bool operator <(Card lhs, Card rhs)
        {
            if (lhs.Value < rhs.Value)
            {
                if (lhs.Suit < rhs.Suit)
                    return true;
            }
            return false;
        }

        public static bool operator >(Card lhs, Card rhs)
        {
            if (lhs.Value > rhs.Value)
            {
                if (lhs.Suit > rhs.Suit)
                    return true;
            }
            return false;
        }

        public int CompareTo(Card? other)
        {
            int otherValue = other?.Value ?? 0;

            if (this.Value == otherValue)
            {
                return (this.Suit) - (other?.Suit ?? 0);
            }

            return this.Value - otherValue;
        }

        public static Card ParseString(string s)
        {
            return new Card(s[0], s[1]);
        }



        public override string ToString()
        {
            return $"{Value}{Suit}";

        }

    }
}
