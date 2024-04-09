using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheMealDB.Core.Models;

namespace TheMealDB.Core.Responses
{
    public class RecipeDetailResponse
    {
        public List<RecipeDetail> meals { get; set; }
    }
}
