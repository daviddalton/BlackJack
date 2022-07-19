namespace BlackJack
{
    public class Dealer
    {
        public int Score { get; set; }
        public string Cards { get; set; }
        public int AlternateScore { get; set; }
        public bool Stayed { get; set; }

        public Dealer(int score, string cards, int alternateScore)
        {
            Score = score;
            AlternateScore = alternateScore;
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

        public void Play()
        {
            if (Score == 21 || AlternateScore == 21)
            {
                Stayed = true;
            }

            if (Score < 15 && AlternateScore < 15)
            {
                Stayed = false;
            }

            if (Score < 15 && AlternateScore > 18)
            {
                Stayed = true;
            }

            if (Score > 18 && AlternateScore > 18)
            {
                Stayed = true;
            }
        }
    }
}