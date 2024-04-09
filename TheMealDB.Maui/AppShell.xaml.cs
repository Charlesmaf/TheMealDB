using TheMealDB.Maui.Views;

namespace TheMealDB.Maui
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();
            Routing.RegisterRoute(nameof(CategoryFiltered), typeof(CategoryFiltered));
            Routing.RegisterRoute(nameof(RecipePage), typeof(RecipePage));
            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
        }
    }
}