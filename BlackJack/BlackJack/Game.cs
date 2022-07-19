using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace BlackJack
{
    public class Game
    {
        public string Status { get; set; }
        
        public List<Card> Deck { get; set; }

        public Game(string status, List<Card> deck)
        {
            Status = status;
            Deck = deck;
        }
        
        public List<Card> ShuffleCards()
        {
            Deck.Clear();
            Deck.Add(new Card("King", 10, 10));
            Deck.Add(new Card("Queen", 10, 10));
            Deck.Add(new Card("Jack", 10, 10));
            Deck.Add(new Card("10", 10, 10));
            Deck.Add(new Card("9", 9, 9));
            Deck.Add(new Card("8", 8, 8));
            Deck.Add(new Card("7", 7, 7));
            Deck.Add(new Card("6", 6, 6));
            Deck.Add(new Card("5", 5, 5));
            Deck.Add(new Card("4", 4, 4));
            Deck.Add(new Card("3", 3, 3));
            Deck.Add(new Card("2", 2, 2));
            Deck.Add(new Card("Ace", 1, 11));

            return Deck;
        }

        public Card DrawCard()
        {
            var random = new Random();
            var availableCards = Deck.Where(card => card.UsesRemaining > 0).ToList();
            var cardDrawn = availableCards.ElementAt(random.Next(availableCards.Count()));
            cardDrawn.UseCard();
            return cardDrawn;
        }

        public void CheckForWinner(Player player, Dealer dealer, Timer timer)
        {
            if (player.Score == 21 || player.AlternateScore == 21)
            {
                BlackJack();
            }
            if (player.Score > 21 && player.AlternateScore > 21)
            {
                Bust();
                if (timer != null)
                {
                    timer.Dispose();
                }
            }
            if (dealer.Score == 21 || dealer.AlternateScore == 21)
            {
                DealerWins();
                if (timer != null)
                {
                    timer.Dispose();
                }
            }
            if (dealer.Score > 21 && dealer.AlternateScore > 21)
            {
                PlayerWins();
                if (timer != null)
                {
                    timer.Dispose();
                }
            }

            if (player.Stayed)
            {
                if (dealer.Score < 22 && dealer.AlternateScore < 22)
                {
                    if (dealer.Score > player.Score && dealer.Score > player.AlternateScore || dealer.AlternateScore > player.Score && dealer.AlternateScore > player.AlternateScore)
                    {
                        DealerWins();
                        if (timer != null)
                        {
                            timer.Dispose();
                        }
                    }   
                }
            }
        }

        public void Bust()
        {
            Status = "BUST!";
        }
        
        public void BlackJack()
        {
            Status = "BLACKJACK!";
        }
        
        public void DealerWins()
        {
            Status = "DEALER WINS!";
        }

        public void PlayerWins()
        {
            Status = "PLAYER WINS!";
        }
    }
}