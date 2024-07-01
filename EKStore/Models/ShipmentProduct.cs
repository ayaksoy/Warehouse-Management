using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKStore.Models
{
    public class ShipmentProduct
    {
        public int Id { get; set; }
        public int ShipmentId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public Product? Product { get; set; }
        public Shipment? Shipment { get; set; }
    }
}