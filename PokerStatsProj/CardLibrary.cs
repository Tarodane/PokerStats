namespace PokerStatsProj
{
    internal class CardLibrary
    {
        public List<string> EveryHand { get; set; } //Should be of size 2598960
        public string[] BasicDeck { get; set; }

        public CardLibrary()
        {
            EveryHand = new List<string>();

            BasicDeck = new string[52];

            string[] suitsList = { "S", "H", "C", "D" };
            string[] numberingList = { "A", "2", "3", "4", "5", "6", "7", "8", "9", "T", "J", "Q", "K" };

            for (int i = 0; i < 13; i++)
            {
                for (int j = 0; j < 4; j++)
                {
                    BasicDeck[(i+j)] = numberingList[i] + suitsList[j];
                }
            }

        }

        public void MakeLibrary()
        {
            EveryHand = ComboFunction(0, 5);
        }

        public List<List<string>> ComboFunction(int start, int k)
        {
            List<List<string>> finalList = new List<List<string>>();
            finalList.Add(BasicDeck[])
            if (k >1)
            {

            } //Just combine the strings together like normal and split them up at the end so we odn't have to list-list them


            return finalList;
        }



    }
}
