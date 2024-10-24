using BudgettingApp.ViewModels;
using Microsoft.Maui.Controls;

namespace BudgettingApp.Views;

public partial class Dashboard : ContentPage
{
    DashboardViewModel Model;
    public Dashboard()
    {
        InitializeComponent();
        Model = Handler.MauiContext.Services.GetService<DashboardViewModel>();
        BindingContext = Model;
        DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
    }
    public Dashboard(DashboardViewModel model)
    {
        InitializeComponent();
        BindingContext = model;
        Model = model;
        DeviceDisplay.MainDisplayInfoChanged += OnMainDisplayInfoChanged;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        Model.Refresh();
    }

    private void OnMainDisplayInfoChanged(object sender, DisplayInfoChangedEventArgs e)
    {
        AdjustGridColumns();
    }

    // Adjust number of columns based on orientation
    private void AdjustGridColumns()
    {
        var orientation = DeviceDisplay.MainDisplayInfo.Orientation;
        if (orientation == DisplayOrientation.Landscape)
        {
            gridItemsLayout.Span = 2; // 2 columns in landscape mode
        }
        else if (orientation == DisplayOrientation.Portrait)
        {
            gridItemsLayout.Span = 1; // 1 column in portrait mode
        }
    }
}