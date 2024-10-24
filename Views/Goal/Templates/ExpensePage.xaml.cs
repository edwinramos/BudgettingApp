using BudgettingApp.ViewModels;
using BudgettingApp.Models;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Maui.Core;

namespace BudgettingApp.Views.Goal.Templates;

public partial class ExpensePage : Popup
{
	ExpensePopupViewModel Model;
	//public ExpensePage()
	//{
	//	InitializeComponent();
 //   }
	public ExpensePage(string goalKey)
	{
		InitializeComponent();
		Model = new ExpensePopupViewModel(new Expense { GoalKey = goalKey});
		BindingContext = Model;
		Model.Init(this);
		Closed += async (s, e) => 
		{
            var vm = App.Current.MainPage.Handler.MauiContext.Services.GetService<GoalDetailViewModel>();
			await vm.RefreshExpenses();
        };
	}
}