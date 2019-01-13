using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HUTModels
{
    public class FreezerInventory
    {
        public int FreezerInventoryId { get; set; }
        public string ItemName { get; set; }
        public Nullable<int> Quantity { get; set; }
    }
}
