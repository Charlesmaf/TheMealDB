using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDB.Core.Interfaces;
using TheMealDB.Data.Models;
using TheMealDB.Maui.Views;

namespace TheMealDB.Core.ViewModel
{
    public partial class MainPageViewModel : ObservableObject
    {
        private readonly IMealService _mealService;
        [ObservableProperty]
        private ObservableCollection<MealCategories> categories;

        [ObservableProperty]
        private MealCategories selectedCategory;
        public MainPageViewModel(IMealService mealService)
        {
            _mealService = mealService;
        }

        public MainPageViewModel()
        {
        }
        [RelayCommand]
        public async Task LoadCategoriesAsync()
        {
            Categories = new ObservableCollection<MealCategories>(await _mealService.GetMealCategoriesAsync());
        }

        [RelayCommand]
        public async Task CategorySelectedAsync()
        {
            if (!string.IsNullOrEmpty(SelectedCategory?.strCategory))
            {
                string category = Uri.EscapeDataString(SelectedCategory.strCategory);
                await Shell.Current.GoToAsync($"{nameof(CategoryFiltered)}?Category={category}");
            }
        }
    }
}
