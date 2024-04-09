using Newtonsoft.Json;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TheMealDB.Core.Interfaces;
using TheMealDB.Core.Models;
using TheMealDB.Core.Responses;
using TheMealDB.Data.Models;
namespace TheMealDB.Core.Services
{
    public class MealService : IMealService
    {
        private readonly HttpClient _httpClient;
        private const string BaseUrl = "https://www.themealdb.com/api/json/v1/1/";
        public MealService(HttpClient? httpClient = null)
        {
            _httpClient = httpClient ?? new HttpClient();
        }
        
        public async Task<List<RecipeDetail>> GetRecipeById(string recipeId)
        {
            if (string.IsNullOrEmpty(recipeId))
            {
                Log.Error("The recipe ID provided is null or empty.");
                return new List<RecipeDetail>();
            }

            try
            {
                var response = await _httpClient.GetAsync($"{BaseUrl}lookup.php?i={recipeId}");

                if (!response.IsSuccessStatusCode)
                {
                    Log.Error($"The request failed with status code: {response.StatusCode}");
                    return new List<RecipeDetail>(); 
                }
                var content = await response.Content.ReadAsStringAsync();
                if (string.IsNullOrWhiteSpace(content))
                {
                    Log.Error("The response content is null or whitespace.");
                    return new List<RecipeDetail>(); ;
                }
                var data = JsonConvert.DeserializeObject<RecipeDetailResponse>(content);
                if (data?.meals == null)
                {
                    Log.Error("The deserialized data is null.");
                    return new List<RecipeDetail>();
                }

                return data.meals;
            }
            catch (HttpRequestException e)
            {
                Log.Error($"An error occurred: {e.Message}");
                return new List<RecipeDetail>(); 
            }
            catch (Exception e)
            {
                Log.Error($"An unexpected error occurred: {e.Message}");
                return new List<RecipeDetail>(); 
            }
        }
        
        public async Task<List<MealCategories>> GetMealCategoriesAsync()
        {
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync($"{BaseUrl}categories.php");

                if (response.IsSuccessStatusCode)
                {
                    string content = await response.Content.ReadAsStringAsync();
                    if (string.IsNullOrWhiteSpace(content))
                    {
                        Log.Error("The response content is null or whitespace.");
                        return new List<MealCategories>(); 
                    }

                    var data = JsonConvert.DeserializeObject<MealCategoriesResponse>(content);
                    if (data?.Categories == null)
                    {
                        Log.Error("The deserialized data is null.");
                        return new List<MealCategories>(); 
                    }

                    return data.Categories;
                }
                else
                {
                    Log.Error($"The request failed with status code: {response.StatusCode}");
                    return new List<MealCategories>(); 
                }
            }
            catch (HttpRequestException ex)
            {
                Log.Error($"Error fetching meal categories: {ex.Message}");
                return new List<MealCategories>();
            }
            catch (Exception ex)
            {
                Log.Error($"An unexpected error occurred: {ex.Message}");
                return new List<MealCategories>(); 
            }
        }
        public async Task<List<Meal>> GetMealsByCategory(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                Log.Error("The category provided is null or empty.");
                return new List<Meal>(); 
            }
            try
            {
                var response = await _httpClient.GetStringAsync($"{BaseUrl}filter.php?c={category}");

                if (string.IsNullOrWhiteSpace(response))
                {
                    Log.Error("The response is null or whitespace.");
                    return new List<Meal>(); 
                }
                var mealList = JsonConvert.DeserializeObject<MealResponse>(response);
                if (mealList?.Meals == null)
                {
                    Log.Error("The deserialized meal list is null.");
                    return new List<Meal>(); 
                }
                return mealList.Meals;
            }
            catch (HttpRequestException ex)
            {
                Log.Error($"Error fetching meals by category: {ex.Message}");
                return new List<Meal>(); 
            }
            catch (Exception ex)
            {
                Log.Error($"An unexpected error occurred: {ex.Message}");
                return new List<Meal>(); 
            }
        }

    }
}
