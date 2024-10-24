using BudgettingApp.Interfaces;
using BudgettingApp.Models;
using BudgettingApp.Views.Goal.Templates;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Controls.UserDialogs.Maui;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgettingApp.ViewModels
{
    public partial class GoalDetailViewModel : ObservableObject, IQueryAttributable
    {
        ILocalDbService<Goal> _dbDashboardItem;
        ILocalDbService<Expense> _dbExpenseItem;
        public ObservableCollection<Expense> Expenses { get; set; } = new ObservableCollection<Expense>();
        public GoalDetailViewModel(ILocalDbService<Goal> dbDashboardItem, ILocalDbService<Expense> dbExpenseItem)
        {
            _dbDashboardItem = dbDashboardItem;
            _dbExpenseItem = dbExpenseItem;
            ColorsList.Add("Red");
            ColorsList.Add("Blue");
            ColorsList.Add("Purple");
            ColorsList.Add("Green");
            ColorsList.Add("Gray");
        }

        #region Properties
        [ObservableProperty]
        public bool canEditItem;
        [ObservableProperty]
        public Goal model;
        [ObservableProperty]
        public List<string> colorsList = new List<string>();
        [ObservableProperty]
        public ObservableCollection<TimePeriods> timePeriods = new ObservableCollection<TimePeriods>(Enum.GetValues(typeof(TimePeriods)) as TimePeriods[]);
        #endregion

        #region Commands
        [RelayCommand]
        async Task Save()
        {
            Model.TimePeriod = (int)Model.DisplayTimePeriod;
            if (string.IsNullOrEmpty(Model.Key))
                _dbDashboardItem.Add(Model);
            else
                _dbDashboardItem.Update(Model);

            UserDialogs.Instance.ShowToast("Saved");
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        async Task AddMovement()
        {
            await App.Current.MainPage.ShowPopupAsync(new ExpensePage(Model.Key));
            //_popupService.ShowPopup<ExpensePopupViewModel>();
            //_popupService.ShowPopup<ExpensePopupViewModel>(onPresenting: viewModel => viewModel.PerformUpdates(10));

        }
        [RelayCommand]
        async Task Delete()
        {
            UserDialogs.Instance.Alert("Delete");
            //await Shell.Current.GoToAsync("Dashboard");
        }
        [RelayCommand]
        async Task DeleteExpense(Expense item)
        {
            var res = await Shell.Current.CurrentPage.DisplayAlert("Delete", "Do you want to delete this item?", "Yes", "No");

            if (res)
                _dbExpenseItem.Delete(item.Key);
            await RefreshExpenses();
        }
        #endregion

        public async Task RefreshExpenses()
        {
            Expenses.Clear();
            var ls = _dbExpenseItem.GetAll().Where(x => x.GoalKey == Model.Key);
            foreach (var item in ls)
            {
                Expenses.Add(item);
            }
            Model.Expenses = Expenses.Sum(x=>x.ExpenseAmount);
            _dbDashboardItem.Update(Model);
            OnPropertyChanged(nameof(Model));
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            var goal = new Goal();
            if (query.Any(x => x.Key == "item"))
                goal = query["item"] as Goal;

            Expenses.Clear();
            var ls = _dbExpenseItem.GetAll().Where(x => x.GoalKey == goal.Key);
            foreach (var item in ls)
            {
                Expenses.Add(item);
            }
            goal.Expenses = Expenses.Sum(x => x.ExpenseAmount);
            CanEditItem = !string.IsNullOrEmpty(goal.Key);
            OnPropertyChanged(nameof(CanEditItem));

            Model = goal;
        }
    }
}
