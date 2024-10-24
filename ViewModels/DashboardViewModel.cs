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
        public DashboardViewModel(ILocalDbService<Goal> dbDashboardItem)
        {
            _dbDashboardItem = dbDashboardItem;


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
        async Task Delete()
        {
            UserDialogs.Instance.Alert("Delete");
            //await Shell.Current.GoToAsync("Dashboard");
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
