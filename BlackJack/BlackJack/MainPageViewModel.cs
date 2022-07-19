using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading;
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
        public Timer Timer;
        
        public MainPageViewModel()
        {
            HitCommand = new Command(HitCommand_Execute);
            ResetCommand = new Command(ResetGameCommand_Execute);
            StartGameCommand = new Command(StartGameCommand_Execute);
            PlayerStayed = new Command(PlayerStayed_Execute);
            Player = NewPlayer();
            Dealer = NewDealer();
            Game = NewGame();
            Game.ShuffleCards();
            StartGameCommand_Execute();
        }

        public ICommand HitCommand { get; set; }

        public ICommand ResetCommand { get; set; }
        
        public ICommand StartGameCommand { get; set; }
        
        public ICommand PlayerStayed { get; set; }
        
        private Dealer NewDealer()
        {
            return new Dealer(0, "", 0);
        }

        private Player NewPlayer()
        {
            return new Player(0, "", 0);
        }

        private Game NewGame()
        {
            return new Game("Playing...", new List<Card>());
        }

        void PlayerStayed_Execute()
        {
            Player.Stayed = true;
            
            DealersTurn();
        }

        void DealersTurn()
        {
            Timer = new Timer(Callback, null, 0, 2000);
        }

        void Callback(object state)
        {
            Dealer.Play();
            DealCardToDealer();
            
            Game.CheckForWinner(Player, Dealer, Timer);
                
            NotifyOfChangesToGameUI();
        }

        void StartGameCommand_Execute()
        {
            DealCardsToPlayer();
            DealCardToDealer();
            Game.CheckForWinner(Player, Dealer, Timer);

            NotifyOfChangesToGameUI();
        }

        void DealCardToDealer()
        {
            Dealer.DealCard(Game.DrawCard());
        }

        void DealCardsToPlayer()
        {
            HitCommand_Execute();
            HitCommand_Execute();
        }

        void HitCommand_Execute()
        {
            Player.DealCard(Game.DrawCard());
            Game.CheckForWinner(Player, Dealer, Timer);

            NotifyOfChangesToGameUI();
        }

        void ResetGameCommand_Execute()
        {
            Game.ShuffleCards();
            ResetGame();

            NotifyOfChangesToGameUI();
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        private void ResetGame()
        {
            if (Timer != null)
            {
                Timer.Dispose();
            }
            Player = NewPlayer();
            Dealer = NewDealer();
            Game = NewGame();
            Game.ShuffleCards();

            StartGameCommand_Execute();
        }

        private void NotifyOfChangesToGameUI()
        {
            OnPropertyChanged(nameof(Game));
            OnPropertyChanged(nameof(Player));
            OnPropertyChanged(nameof(Dealer));
        }
    }
}