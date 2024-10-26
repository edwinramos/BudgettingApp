using BudgettingApp.Interfaces;
using BudgettingApp.Models;
using BudgettingApp.Views.Goal.Templates;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace BudgettingApp.ViewModels
{
    public partial class MovementsViewModel : ObservableObject
    {
        ILocalDbService<Goal> _dbDashboardItem;
        ILocalDbService<Expense> _dbExpenseItem;

        public MovementsViewModel(ILocalDbService<Goal> dbDashboardItem, ILocalDbService<Expense> dbExpenseItem)
        {
            _dbDashboardItem = dbDashboardItem;
            _dbExpenseItem = dbExpenseItem;
        }

        [RelayCommand]
        async Task AddMovement()
        {
            await App.Current.MainPage.ShowPopupAsync(new ExpensePage(Model.Key));
        }

        [RelayCommand]
        async Task DeleteExpense(Expense item)
        {
            var res = await Shell.Current.CurrentPage.DisplayActionSheet("Options", "Cancel", "", "View Comments", "Delete");
            switch (res)
            {
                case "View Comments":
                    await Shell.Current.CurrentPage.DisplayAlert("Comments", item.Comment, "Ok");
                    break;
                case "Delete":
                    _dbExpenseItem.Delete(item.Key);
                    break;
                default:
                    break;
            }

            await RefreshExpenses(Model.Key);
        }
        public ObservableCollection<Expense> Expenses { get; set; } = new ObservableCollection<Expense>();
        [ObservableProperty]
        public Goal model;
        public async Task RefreshExpenses(string goalKey)
        {
            Model = _dbDashboardItem.GetAll().FirstOrDefault(x => x.Key == goalKey);
            Expenses.Clear();
            var ls = _dbExpenseItem.GetAll().Where(x => x.GoalKey == goalKey);
            foreach (var item in ls)
            {
                Expenses.Add(item);
            }
            Model.Expenses = Expenses.Sum(x => x.ExpenseAmount);
            _dbDashboardItem.Update(Model);
            //OnPropertyChanged(nameof(Model));
        }
    }
}
