using TheMealDB.Core.Interfaces;
using TheMealDB.Core.Models;
using TheMealDB.Core.Services;

namespace TheMealDB.Maui.Views;
[QueryProperty(nameof(MealId), "MealId")]
public partial class RecipePage : ContentPage
{
    IMealService _mealService;
    public string MealId { get; set; }
    public RecipePage()
    {
        _mealService = new MealService();
        InitializeComponent();
    }
    protected override async void OnAppearing()
    {
        if(MealId != null)
        {
            var data = await GetRecipeByIdAsync(MealId);
            if (data != null)
            {
                MealName.Text = data[0].strMeal;
                Instructions.Text = data[0].strInstructions;
                MealImage.Source = ImageSource.FromUri(new Uri(data[0].strMealThumb));
            }
        }
    }
    public async Task<List<RecipeDetail>> GetRecipeByIdAsync(string category)
    {
        return await _mealService.GetRecipeById(category);
    }
}