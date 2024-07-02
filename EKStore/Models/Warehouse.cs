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
        public bool IsDelete { get; set; }
        public bool IsStatus { get; set; }
        public int LocationId { get; set; }
        public Location? Location { get; set; }
        public List<Product>? Products { get; set; }
    }
}