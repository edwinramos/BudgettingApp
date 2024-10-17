using BudgettingApp.ViewModels;

namespace BudgettingApp.Views;

public partial class Dashboard : ContentPage
{
	DashboardViewModel Model;
    public Dashboard()
    {
        InitializeComponent();
        Model = Handler.MauiContext.Services.GetService<DashboardViewModel>();
        BindingContext = Model;
    }
    public Dashboard(DashboardViewModel model)
	{
		InitializeComponent();
		BindingContext = model;
		Model = model;
	}
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Model.Refresh();
    }
}