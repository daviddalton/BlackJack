using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using BlackJack.Annotations;
using Xamarin.Forms;

namespace BlackJack
{
    public class MainPageViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        
        public int ChipCount { get; set; }
        
        public int DealerScore { get; set; }
        public int PlayerScore { get; set; }
        public int PlayerAlternateScore { get; set; } = 0;
        public string PlayerCards { get; set; } = "";
        public string PlayerStatus { get; set; } = "Playing...";
        public bool GameOver { get; set; } = false;

        private readonly List<Card> _cards = new List<Card>();

        public MainPageViewModel()
        {
            HitCommand = new Command(ChangeTextCommand_Execute);
            ResetCommand = new Command(ResetGameCommand_Execute);
            SetUpCards();
        }
        
        public ICommand HitCommand { get; set; }

        public ICommand ResetCommand { get; set; }

        void ChangeTextCommand_Execute()
        {
            // TODO: Check if the card is an Ace - you can play it as either a 1 or 11
            var card = GetCard();
            UpdatePlayerCards(card);
            UpdatePlayerScoreAndGameStatus(card);

            NotifyOfChangesToGameUI();

        }

        void ResetGameCommand_Execute()
        {
            SetUpCards();
            ResetUI();

            NotifyOfChangesToGameUI();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private Card GetCard()
        {
            var random = new Random();
            var availableCards = _cards.Where(card => card.UsesRemaining > 0);
            return availableCards.ElementAt(random.Next(availableCards.Count()));
            
        }

        private void SetUpCards()
        {
            _cards.Clear();
            _cards.Add(new Card("King", 10, 10));
            _cards.Add(new Card("Queen", 10, 10));
            _cards.Add(new Card("Jack", 10, 10));
            _cards.Add(new Card("10", 10, 10));
            _cards.Add(new Card("9", 9, 9));
            _cards.Add(new Card("8", 8, 8));
            _cards.Add(new Card("7", 7, 7));
            _cards.Add(new Card("6", 6, 6));
            _cards.Add(new Card("5", 5, 5));
            _cards.Add(new Card("4", 4, 4));
            _cards.Add(new Card("3", 3, 3));
            _cards.Add(new Card("2", 2, 2));
            _cards.Add(new Card("Ace", 1, 11));
        }

        private void ResetUI()
        {
            PlayerScore = 0;
            PlayerCards = "";
            PlayerStatus = "Playing...";
            PlayerAlternateScore = 0;
            GameOver = false;
        }

        private void UpdatePlayerCards(Card card)
        {
            if (PlayerCards.Length > 0)
            {
                PlayerCards += " + " + card.Name;
            }
            else
            {
                PlayerCards += card.Name;
            }
        }

        private void UpdatePlayerScoreAndGameStatus(Card card)
        {
            PlayerScore += card.Value;
            PlayerAlternateScore += card.AlternateValue;
            if (PlayerScore == 21 || PlayerAlternateScore == 21)
            {
                BlackJack();
            }

            if (PlayerScore > 21 && PlayerAlternateScore > 21)
            {
                Bust();
            }
        }

        private void NotifyOfChangesToGameUI()
        {
            OnPropertyChanged(nameof(PlayerScore));
            OnPropertyChanged(nameof(GameOver));
            OnPropertyChanged(nameof(PlayerStatus));
            OnPropertyChanged(nameof(PlayerCards));
            OnPropertyChanged(nameof(PlayerAlternateScore));
        }

        private void Bust()
        {
            PlayerStatus = "BUST!";
            GameOver = true;
        }

        private void BlackJack()
        {
            PlayerStatus = "BLACKJACK!";
            GameOver = true;
        }
    }
}