using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUTModels
{
    public class Weight
    {
        public int WeightId { get; set; }
        public int PersonId { get; set; }
        public System.DateTime DateEntered { get; set; }
        public decimal WeightAmount { get; set; }
    }
}
