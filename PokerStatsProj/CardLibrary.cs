namespace PokerStatsProj
{
    internal class CardLibrary
    {
        public List<Hand> EveryHand { get; set; } //Should be of size 2598960
        public List<Card> BasicDeck { get; set; } //Same var type as Hand, but not making it a Hand type bc I want to reserve Hand to have max 5 cards

        public CardLibrary()
        {
            char[] suitsList = { 'S', 'H', 'C', 'D' };
            char[] numberingList = { 'A', '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K' };

            BasicDeck = new List<Card>();

            for (int i = 0; i < suitsList.Length; i++)
            {
                for (int j = 0; j < numberingList.Length; j++)
                {
                    BasicDeck.Add(new Card(numberingList[j], suitsList[i]));
                }
            }

            EveryHand = new List<Hand>();
            MakeLibrary();
        }

        private void MakeLibrary()
        {
            Console.WriteLine("Making Hands library");
            EveryHand = ComboFunction(0, 5, BasicDeck);
            for (int i = 0;i < EveryHand.Count; i++)
            {
                EveryHand[i].SortCards();
            }
            Console.WriteLine("Finished creating library");
        }

        //This function is reliant on BasicDeck being a variable available within this function, otherwise I'd have to copy...wait, do I copy it?
        private List<Hand> ComboFunction(int n, int k, List<Card> deck) 
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
    }
}
