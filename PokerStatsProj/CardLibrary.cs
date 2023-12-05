namespace PokerStatsProj
{
    internal class CardLibrary
    {
        public List<Hand> EveryHand { get; set; } //Should be of size 2598960
        public List<Card> BasicDeck { get; set; } //Same var type as Hand, but not making it a Hand type bc I want to reserve Hand to have max 5 cards
        private bool LibraryMade { get; set; } //I don't want to recreate the library more than once

        public CardLibrary()
        {
            char[] suitsList = { 'S', 'H', 'C', 'D' };
            char[] numberingList = { 'A', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K' };

            BasicDeck = SetUpFxn();

            for (int i = 0; i < suitsList.Length; i++)
            {
                for (int j = 0; j < numberingList.Length; j++)
                {
                    BasicDeck.Add(new Card(numberingList[j], suitsList[i]));
                }
            }

            EveryHand = new List<Hand>();
            LibraryMade = false;
        }

        public static List<Card> SetUpFxn()
        {
            List<Card> holderList = new ();
            char[] suitsList = { 'S', 'H', 'C', 'D' };
            char[] numberingList = { 'A', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K' };

            for (int i = 0; i < suitsList.Length; i++)
            {
                for (int j = 0; j < numberingList.Length; j++)
                {
                    holderList.Add(new Card(numberingList[j], suitsList[i]));
                }
            }
            return holderList;
        }

        public static List<Hand> ComboFunction(int n, int k, List<Card> deck)
        {
            List<Hand> percolatingHand = new();
            List<Hand> leftPercolate = new();
            List<Hand> rightPercolate = new();

            if (k > 1)
            {
                leftPercolate = ComboFunction(n+1, k-1, deck);
            }
            else
            {
                percolatingHand.Add(new Hand(deck[n]));
            }

            if (k < deck.Count-n)
            {
                rightPercolate = ComboFunction(n+1, k, deck);
            }

            for (int i = 0; i < leftPercolate.Count; i++)
            {
                leftPercolate[i].Add(deck[n]);
                percolatingHand.Add(leftPercolate[i]);
            }
            for (int i = 0; i < rightPercolate.Count; i++)
            {
                percolatingHand.Add(rightPercolate[i]);
            }
            return percolatingHand;
        }


        public void MakeLibrary()
        {
            if (LibraryMade)
            {
                Console.WriteLine("Library has already been made, exiting method");
                return;
            }

            LibraryMade = true;

            Console.WriteLine("Making Hands library");
            EveryHand = ComboFunction(0, 5, BasicDeck);
            for (int i = 0;i < EveryHand.Count; i++)
            {
                EveryHand[i].SortCards();
            }
            Console.WriteLine("Finished creating library");
        }
    }
}
