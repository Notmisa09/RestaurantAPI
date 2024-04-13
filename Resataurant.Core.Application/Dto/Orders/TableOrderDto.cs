using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Application.Dto.Orders
{
    public class TableOrderDto
    {
        public int Id { get; set; }
        public decimal Subtotal { get; set; }
        public List<string>? Plates { get; set; }
        public int TableId { get; set; }
        public string? Table { get; set; }
        public string OrderStatus { get; set; }
        public int OrderStatusId { get; set; }
    }
}
