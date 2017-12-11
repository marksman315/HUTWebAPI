using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUTModels
{
    public class CalorieCount
    {
        public int CalorieCountId { get; set; }
        public int PersonId { get; set; }
        public System.DateTime DatetimeEntered { get; set; }
        public int Calories { get; set; }
    }
}
