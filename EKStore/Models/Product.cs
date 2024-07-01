using EKStore.Models.Abstracts;

namespace EKStore.Models
{
    public class Product:CommonProp
    {
        public int Quantity { get; set; }
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
        public List<ShipmentProduct>? ShipmentProducts { get; set; }
    }
}
