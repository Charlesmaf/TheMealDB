using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDB.Data.Models;

namespace TheMealDB.Core.Responses
{
    public class MealCategoriesResponse
    {
        public List<MealCategories>? Categories { get; set; }
    }
}
