using BudgettingApp.Interfaces;
using BudgettingApp.Models;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Maui.Views;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BudgettingApp.ViewModels
{
    public partial class ExpensePopupViewModel : ObservableObject
    {
        [ObservableProperty]
        public Expense model;
        ILocalDbService<Expense> _dbExpenseItem;
        Popup _popup;
        public ExpensePopupViewModel(Expense goal)
        {
            Model = goal;
            _dbExpenseItem = App.Current.MainPage.Handler.MauiContext.Services.GetService<ILocalDbService<Expense>>();
            //_popupService = App.Current.MainPage.Handler.MauiContext.Services.GetService<IPopupService>();
        }
        public void Init(Popup popup)
        {
            _popup = popup;
        }
        [RelayCommand]
        private async void SaveExpense()
        {
            Model.ExpenseDateTime = DateTime.Now;
            _dbExpenseItem.Add(Model);
            Cancel();
        }
        [RelayCommand]
        private async void Cancel()
        {
            // Close the popup
            await _popup.CloseAsync();
        }
    }


}
