using BudgettingApp.Interfaces;
using BudgettingApp.Models;
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
    public partial class DashboardViewModel : ObservableObject
    {
        ILocalDbService<Goal> _dbDashboardItem;
        ILocalDbService<Expense> _dbExpenseItem;
        public DashboardViewModel(ILocalDbService<Goal> dbDashboardItem, ILocalDbService<Expense> dbExpenseItem)
        {
            _dbDashboardItem = dbDashboardItem;
            _dbExpenseItem = dbExpenseItem;

        }

        #region Properties
        public ObservableCollection<Goal> Items { get; set; } = new ObservableCollection<Goal>();
        #endregion

        #region Commands
        [RelayCommand]
        async Task AddNew()
        {
             await Shell.Current.GoToAsync("GoalDetail");
        }
        [RelayCommand]
        async Task Edit(Goal item)
        {
            var navigationParameter = new ShellNavigationQueryParameters
            {
                { "item", item }
            };
             await Shell.Current.GoToAsync("GoalDetail", navigationParameter);
        }
        [RelayCommand]
        async Task Delete(Goal item)
        {
            var res = await Shell.Current.CurrentPage.DisplayAlert("Delete", "Do you want to delete this item?", "Yes", "No");
            if (res)
            {
                var ls = _dbExpenseItem.GetAll().Where(x => x.GoalKey == item.Key);
                foreach (var l in ls) 
                {
                    _dbExpenseItem.Delete(l.Key);
                }
                _dbDashboardItem.Delete(item.Key);
                Refresh();
            }
        }
        #endregion
        public void Refresh()
        {
            Items.Clear();
            var ls = _dbDashboardItem.GetAll();
            foreach (var item in ls)
            {
                item.DisplayTimePeriod = (TimePeriods)item.TimePeriod;
                Items.Add(item);
            }

        }
    }
}
