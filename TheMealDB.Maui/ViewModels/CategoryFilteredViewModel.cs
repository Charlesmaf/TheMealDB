using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDB.Core.Interfaces;
using TheMealDB.Core.Models;
using TheMealDB.Core.Services;
using TheMealDB.Data.Models;
using TheMealDB.Maui.Views;

namespace TheMealDB.Core.ViewModel
{
    [QueryProperty(nameof(Category), "Category")]
    public partial class CategoryFilteredViewModel : ObservableObject
    {
        private readonly IMealService _mealService;

        [ObservableProperty]
        private ObservableCollection<Meal> meals;
        
        private string category;

        public string Category
        {
            get => category;
            set
            {
                SetProperty(ref category, value);
                LoadMealsAsync();
            }
        }

        [ObservableProperty]
        private Meal selectedMeal;

        public CategoryFilteredViewModel(IMealService mealService)
        {
            _mealService = mealService;
        }
        public CategoryFilteredViewModel()
        {
        }
        [RelayCommand]
        public async Task LoadMealsAsync()
        {
            if (!string.IsNullOrEmpty(category))
            {
                Meals = new ObservableCollection<Meal>(await _mealService.GetMealsByCategory(category));
            }
        }
        [RelayCommand]
        public async Task MealSelectedAsync()
        {
            if (SelectedMeal != null && !string.IsNullOrEmpty(SelectedMeal.idMeal))
            {
                string mealId = Uri.EscapeDataString(SelectedMeal.idMeal);
                await Shell.Current.GoToAsync($"{nameof(RecipePage)}?MealId={mealId}");
            }
        }
    }
}
