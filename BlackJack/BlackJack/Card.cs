
namespace BlackJack
{
    public class Card
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public int AlternateValue { get; set; }
        public int UsesRemaining { get; set; } = 4;

        public Card(string name, int value, int alternateValue)
        {
            Name = name;
            Value = value;
            AlternateValue = alternateValue;
        }

        public void UseCard()
        {
            UsesRemaining -= 1;
        }
    }
}