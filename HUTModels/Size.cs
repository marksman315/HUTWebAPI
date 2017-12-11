using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace HUTModels
{
    public class Size
    {
        public int SizeId { get; set; }
        public int PersonId { get; set; }
        public System.DateTime DateEntered { get; set; }
        public Nullable<decimal> Bicep { get; set; }
        public Nullable<decimal> Stomach { get; set; }
    }
}
