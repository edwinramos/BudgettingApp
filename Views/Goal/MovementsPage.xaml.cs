using BudgettingApp.ViewModels;
using BudgettingApp.Models;
using System.Reflection;

namespace BudgettingApp.Views
{
    public partial class MovementsPage : ContentPage
    {
        MovementsViewModel Model;
        string _goalKey;
        public MovementsPage(string goalKey)
        {
            InitializeComponent();
            _goalKey = goalKey;
            Model = App.Current.MainPage.Handler.MauiContext.Services.GetService<MovementsViewModel>();
            BindingContext = Model;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();

            await Model.RefreshExpenses(_goalKey);
        }

        private async void TapGestureRecognizer_Tapped(object sender, TappedEventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}
