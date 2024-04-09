using System.Text.Json;
using TheMealDB.Core.Interfaces;
using TheMealDB.Core.Services;
using TheMealDB.Core.ViewModel;
using TheMealDB.Data.Models;
using TheMealDB.Maui.Views;

namespace TheMealDB.Maui
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel(new MealService());
        }
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await (BindingContext as MainPageViewModel)?.LoadCategoriesAsync();

        }
        private async void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var viewModel = BindingContext as MainPageViewModel;
            if (viewModel == null) return;

            var selectedCategory = e.CurrentSelection.FirstOrDefault() as MealCategories;
            if (selectedCategory != null)
            {
                viewModel.SelectedCategory = selectedCategory;
                await viewModel.CategorySelectedAsync();
            }
        }
    }
}