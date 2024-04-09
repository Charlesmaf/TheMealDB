
using TheMealDB.Core.Interfaces;
using TheMealDB.Core.Models;
using TheMealDB.Core.Services;
using TheMealDB.Core.ViewModel;
using TheMealDB.Data.Models;

namespace TheMealDB.Maui.Views;
[QueryProperty(nameof(Category), "Category")]

public partial class CategoryFiltered : ContentPage
{
    public string Category { get; set; }
    public CategoryFiltered()
	{
		InitializeComponent();
        BindingContext = new CategoryFilteredViewModel(new MealService());
    }
    protected override async void OnNavigatedTo(NavigatedToEventArgs args)
    {
        base.OnNavigatedTo(args);
        await (BindingContext as CategoryFilteredViewModel)?.LoadMealsAsync();
    }
    
}