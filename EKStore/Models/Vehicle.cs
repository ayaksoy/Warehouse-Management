using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKStore.Models
{
    public class Vehicle
    {
        public int Id { get; set; }
        public string? LicensePlate { get; set; }
        public string? Type { get; set; }
        public List<Shipment>? Shipments { get; set; }
        public bool IsDelete { get; set; }
        public bool IsStatus { get; set; }
    }
}