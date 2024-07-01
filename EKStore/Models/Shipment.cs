using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKStore.Models
{
    public class Shipment
    {
        public int Id { get; set; }
        public int FromWarehouseId { get; set; }
        public int ToWarehouseId { get; set; }
        public DateTime ShipmentDate { get; set; }
        public List<ShipmentProduct>? Products { get; set; }
        public int VehicleId { get; set; }
        public int DriverId { get; set; }
        public int Cost { get; set; }
        public Warehouse? FromWarehouse { get; set; }
        public Warehouse? ToWarehouse { get; set; }
        public Vehicle? Vehicle { get; set; }
        public Driver? Driver { get; set; }
    }
}