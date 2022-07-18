namespace BlackJack
{
    public class Player
    {
        public int Score { get; set; }
        public string Cards { get; set; }
        public int AlternateScore { get; set; }

        public Player(int score, string cards, int alternateScore)
        {
            Score = score;
            AlternateScore = AlternateScore;
            Cards = cards;
        }
    }
}