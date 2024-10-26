using BudgettingApp.Interfaces;
using BudgettingApp.Models;
using BudgettingApp.Views;
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

        public GoalDetailViewModel(ILocalDbService<Goal> dbDashboardItem, ILocalDbService<Expense> dbExpenseItem)
        {
            _dbDashboardItem = dbDashboardItem;
            _dbExpenseItem = dbExpenseItem;
        }

        #region Properties
        public bool PreserveState;
        [ObservableProperty]
        public bool canEditItem;
        [ObservableProperty]
        public Goal model;
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
        async Task ShowMovements()
        {
            //await App.Current.MainPage.ShowPopupAsync(new ExpensePage(Model.Key));
            await App.Current.MainPage.Navigation.PushModalAsync(new MovementsPage(Model.Key));
            PreserveState = true;
        }
        
        #endregion

        

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (PreserveState)
            {
                PreserveState = false;
                var expenses = _dbExpenseItem.GetAll().Where(x => x.GoalKey == Model.Key);
                Model.Expenses = expenses.Sum(x => x.ExpenseAmount);
                CanEditItem = !string.IsNullOrEmpty(Model.Key);
                OnPropertyChanged(nameof(Model));
                return;
            }
            var goal = new Goal { BackgroundColor = "Green"};
            if (query.Any(x => x.Key == "item"))
                goal = query["item"] as Goal;

            var ls = _dbExpenseItem.GetAll().Where(x => x.GoalKey == goal.Key);
            goal.Expenses = ls.Sum(x => x.ExpenseAmount);
            CanEditItem = !string.IsNullOrEmpty(goal.Key);
            OnPropertyChanged(nameof(CanEditItem));

            Model = goal;
        }
    }
}
