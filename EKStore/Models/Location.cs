using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKStore.Models
{
    public class Location
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public List<Warehouse>? Warehouses { get; set; }
    }
}