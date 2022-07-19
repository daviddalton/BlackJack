namespace BlackJack
{
    public class Player
    {
        public int Score { get; set; }
        public string Cards { get; set; }
        public int AlternateScore { get; set; }
        public bool Stayed { get; set; }

        public Player(int score, string cards, int alternateScore)
        {
            Score = score;
            AlternateScore = AlternateScore;
            Cards = cards;
        }

        public void DealCard(Card card)
        {
            if (Cards.Length > 0)
            {
                Cards += " + " + card.Name;
            }
            else
            {
                Cards += card.Name;
            }
            Score += card.Value;
            AlternateScore += card.AlternateValue;
        }
    }
}