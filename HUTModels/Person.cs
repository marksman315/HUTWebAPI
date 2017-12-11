using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUTModels
{
    public class Person
    {
        public int PersonId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public List<Size> Sizes { get; set; }
        public List<Weight> Weights { get; set; }
        public List<CalorieCount> CalorieCounts { get; set; }
    }
}
