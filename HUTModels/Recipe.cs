using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUTModels
{
    public class Recipe
    {
        public int RecipeId { get; set; }
        public string Description { get; set; }
        public System.DateTime DateEntered { get; set; }
        public bool Archived { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; }
    }
}
