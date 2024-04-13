using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Domain.Entities
{
    public class PlateOrders
    {
        public Plates? Plates { get; set; }
        public int PlateId { get; set; }
        public int OrderId { get; set; }  
        public Orders? Orders { get; set; }
    }
}
