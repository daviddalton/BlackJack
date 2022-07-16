using System.Drawing;

namespace BlackJack
{
    public class Card
    {
        public string Name { get; set; }
        public int Value { get; set; }
        public Color Color { get; set; }
        public string Suit { get; set; }
        public int UsesRemaining { get; set; } = 4;

        public Card(string name, int value)
        {
            Name = name;
            Value = value;
        }
    }
}