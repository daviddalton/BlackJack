namespace BlackJack
{
    public class Dealer
    {
        public int Score { get; set; }
        public string Cards { get; set; }
        public int AlternateScore { get; set; }

        public Dealer(int score, string cards, int alternateScore)
        {
            Score = score;
            AlternateScore = alternateScore;
            Cards = cards;
        }
    }
}