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
    public partial class GoalDetailViewModel : ObservableObject, IQueryAttributable
    {
        ILocalDbService<Goal> _dbDashboardItem;
        public GoalDetailViewModel(ILocalDbService<Goal> dbDashboardItem)
        {
            _dbDashboardItem = dbDashboardItem;
            ColorsList.Add("Red");
            ColorsList.Add("Blue");
            ColorsList.Add("Purple");
            ColorsList.Add("Green");
            ColorsList.Add("Gray");
        }

        #region Properties
        [ObservableProperty]
        public Goal model;
        [ObservableProperty]
        public List<string> colorsList = new List<string>();
        #endregion

        #region Commands
        [RelayCommand]
        async Task Save()
        {

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
            UserDialogs.Instance.Alert("AddM");
            //await Shell.Current.GoToAsync("Dashboard");
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

        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            if (query.Any(x => x.Key == "item"))
                Model = query["item"] as Goal;
            else
                Model = new Goal();
        }
    }
}
