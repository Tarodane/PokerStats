using System;

namespace PokerStatsProj
{
    public enum CardInt
    {
        Two = 2,
        Three = 3,
        Four = 4,
        Five = 5, 
        Six = 6,
        Seven = 7,
        Eight = 8,
        Nine = 9,
        Ten = 10,
        Jack = 11,
        Queen = 12,
        King = 13, 
        Ace = 14
    }
    public enum SuitChar
    {
        Spaids = 'S',
        Hearts = 'H',
        Clubs = 'C',
        Diamonds = 'D'
    }
    public enum CardChar
    {
        Two = '2',
        Three = '3',
        Four = '4',
        Five = '5',
        Six = '6',
        Seven = '7',
        Eight = '8',
        Nine = '9',
        Ten = 'T',
        Jack = 'J',
        Queen = 'Q',
        King = 'K',
        Ace = 'A'
    }

    internal class Card : IComparable<Card>
	{
		public char Value { get; set; }
		public char Suit { get; set; }

        public Card()
        {
            Value = new char();
            Suit = new char();
        }

		public Card(char value = '\0', char suit = '\0')
		{
			Value = value;
			Suit = suit;
		}

        public Card(string card = "\0\0")
        {
            Value = card[0];
            Suit = card[1];
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
