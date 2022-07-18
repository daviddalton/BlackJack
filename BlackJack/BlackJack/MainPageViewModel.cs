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
        
        public Game Game { get; set; }
        public Dealer Dealer { get; set; }
        public Player Player { get; set; }

        private readonly List<Card> _cards = new List<Card>();

        public MainPageViewModel()
        {
            HitCommand = new Command(HitCommand_Execute);
            ResetCommand = new Command(ResetGameCommand_Execute);
            StartGameCommand = new Command(StartGameCommand_Execute);
            SetUpCards();
            Player = SetUpPlayer();
            Dealer = SetUpDealer();
            Game = SetUpGame();
            StartGameCommand_Execute();
        }
        
        public ICommand HitCommand { get; set; }

        public ICommand ResetCommand { get; set; }
        
        public ICommand StartGameCommand { get; set; }

        void StartGameCommand_Execute()
        {
            DealCardsToPlayer();
            DealCardsToDealer();

            NotifyOfChangesToGameUI();
        }

        void DealCardsToDealer()
        {
            AddCardToDealer();
            AddCardToDealer();
        }

        void AddCardToDealer()
        {
            var card = GetCard();
            if (Dealer.Cards.Length > 0)
            {
                Dealer.Cards += " + " + card.Name;
            }
            else
            {
                Dealer.Cards += card.Name;
            }

            UpdateDealerScoreAndGameStatus(card);
        }

        void DealCardsToPlayer()
        {
            HitCommand_Execute();
            HitCommand_Execute();
        }

        void HitCommand_Execute()
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

        private Dealer SetUpDealer()
        {
            return new Dealer(0, "", 0);
        }

        private Player SetUpPlayer()
        {
            return new Player(0, "", 0);
        }

        private Game SetUpGame()
        {
            return new Game("Playing...");
        }

        private void ResetUI()
        {
            Player = SetUpPlayer();
            Dealer = SetUpDealer();
            Game = SetUpGame();

            StartGameCommand_Execute();
        }

        private void UpdatePlayerCards(Card card)
        {
            if (Player.Cards.Length > 0)
            {
                Player.Cards += " + " + card.Name;
            }
            else
            {
                Player.Cards += card.Name;
            }
        }

        private void UpdatePlayerScoreAndGameStatus(Card card)
        {
            Player.Score += card.Value;
            Player.AlternateScore += card.AlternateValue;
            if (Player.Score == 21 || Player.AlternateScore == 21)
            {
                BlackJack();
            }

            if (Player.Score > 21 && Player.AlternateScore > 21)
            {
                Bust();
            }
        }

        private void UpdateDealerScoreAndGameStatus(Card card)
        {
            Dealer.Score += card.Value;
            Dealer.AlternateScore += card.AlternateValue;
            if (Dealer.Score == 21 || Dealer.AlternateScore == 21)
            {
                DealerWins();
            }
        }

        private void NotifyOfChangesToGameUI()
        {
            OnPropertyChanged(nameof(Game));
            OnPropertyChanged(nameof(Player));
            OnPropertyChanged(nameof(Dealer));
        }

        private void Bust()
        {
            Game.Status = "BUST!";
        }

        private void BlackJack()
        {
            Game.Status = "BLACKJACK!";
        }

        private void DealerWins()
        {
            Game.Status = "DEALER WINS!";
        }

        private void DealerPlay()
        {
            if (Dealer.Score == 21 || Dealer.AlternateScore == 21)
            {
                
            }
        }
    }
}