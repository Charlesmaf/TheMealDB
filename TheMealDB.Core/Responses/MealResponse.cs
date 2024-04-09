using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDB.Core.Models;

namespace TheMealDB.Core.Responses
{
    public class MealResponse
    {
        public List<Meal>? Meals { get; set; }
    }
}
