using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKStore.Models
{
    public class Warehouse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int LocationID { get; set; }
        List<Product>? Products { get; set; }
    }
}