using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Core.Application.Dto.Tables
{
    public class TableAddDto
    {
        public int Id { get; set; }
        public int PeopleAmount { get; set; }
        public string Description { get; set; }

        //NAVGATION PROPERTIES

        public string? Status { get; set; }
        public int? TableStatusId { get; set; }
        public List<int> Orders { get; set; }

    }
}
