using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDB.Core.Models;
using TheMealDB.Data.Models;

namespace TheMealDB.Core.Interfaces
{
    public interface IMealService
    {
        Task<List<RecipeDetail>> GetRecipeById(string recipeId);
        Task<List<MealCategories>> GetMealCategoriesAsync();
        Task<List<Meal>> GetMealsByCategory(string category);
    }
}
