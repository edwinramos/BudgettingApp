using BudgettingApp.Models;
using BudgettingApp.ViewModels;

namespace BudgettingApp.Views.Goal;

public partial class GoalDetail : ContentPage
{
	GoalDetailViewModel Model;
    public GoalDetail(GoalDetailViewModel model)
    {
		InitializeComponent();
        BindingContext = model;
        Model = model;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        //Model.Refresh();
    }
}