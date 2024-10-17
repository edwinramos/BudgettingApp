using BudgettingApp.Views.Goal;
using BudgettingApp.Views;

namespace BudgettingApp
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Dashboard", typeof(Dashboard));
            Routing.RegisterRoute("MainPage", typeof(MainPage));
            Routing.RegisterRoute("GoalDetail", typeof(GoalDetail));
        }
    }
}
