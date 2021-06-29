using GoodsPlan.Infrastructure.Models.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodsPlan.Warehouses.Models
{
    public class Warehouse : EntityBase
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Address { get; set; }

        public decimal SizeInCubicMeters { get; set; }

        public long UserId { get; set; }
    }
}
