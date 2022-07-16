using System;
using Xamarin.Forms;

namespace BlackJack
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }

        void Hit_Clicked(Object sender, EventArgs e)
        {
            Console.WriteLine("Hit!");
        }
        
        void Stay_Clicked(Object sender, EventArgs e)
        {
            Console.WriteLine("Stay...");
        }
    }
}

