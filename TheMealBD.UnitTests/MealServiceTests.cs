using Xunit;
using System.Threading.Tasks;
using System.Net.Http;
using System.Collections.Generic;
using TheMealDB.Core.Services;
using TheMealDB.Data.Models;
using Moq;
using System.Net;
using System.Text;
using Moq.Protected;

namespace TheMealBD.UnitTests
{
    
    public class MealServiceTests
    {
        [Fact]
        public async Task GetMealCategoriesAsync_ReturnsNonEmptyCategoryList()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent("{\"categories\":[{\"idCategory\":\"1\",\"strCategory\":\"Beef\"}]}")
                })
                .Verifiable();

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://www.themealdb.com/api/json/v1/1/")
            };

            var mealService = new MealService(httpClient);

            // Act
            var result = await mealService.GetMealCategoriesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("Beef", result[0].strCategory);
        }

        [Fact]
        public async Task GetRecipeById_ReturnsRecipeDetails()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var recipeId = "52772";
            var responseContent = "{\"meals\":[{\"idMeal\":\"52772\",\"strMeal\":\"Chicken Handi\"}]}";
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent)
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://www.themealdb.com/api/json/v1/1/")
            };

            var mealService = new MealService(httpClient);

            // Act
            var result = await mealService.GetRecipeById(recipeId);

            // Assert
            Assert.NotNull(result);
            Assert.Single(result);
            Assert.Equal("52772", result[0].idMeal);
            Assert.Equal("Chicken Handi", result[0].strMeal);
        }

        [Fact]
        public async Task GetMealsByCategory_ReturnsMealsList()
        {
            // Arrange
            var mockHttpMessageHandler = new Mock<HttpMessageHandler>();
            var category = "Chicken";
            var responseContent = "{\"meals\":[{\"strMeal\":\"Chicken Curry\",\"idMeal\":\"52806\"}]}";
            mockHttpMessageHandler.Protected()
                .Setup<Task<HttpResponseMessage>>(
                    "SendAsync",
                    ItExpr.IsAny<HttpRequestMessage>(),
                    ItExpr.IsAny<CancellationToken>()
                )
                .ReturnsAsync(new HttpResponseMessage
                {
                    StatusCode = HttpStatusCode.OK,
                    Content = new StringContent(responseContent)
                });

            var httpClient = new HttpClient(mockHttpMessageHandler.Object)
            {
                BaseAddress = new Uri("https://www.themealdb.com/api/json/v1/1/")
            };

            var mealService = new MealService(httpClient);

            // Act
            var result = await mealService.GetMealsByCategory(category);

            // Assert
            Assert.NotNull(result);
            Assert.NotEmpty(result);
            Assert.Equal("Ayam Percik", result[0].strMeal);
            Assert.Equal("53050", result[0].idMeal);
        }
    }
}