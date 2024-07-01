using EKStore.Models.Abstracts;

namespace EKStore.Models
{
    public class Product:CommonProp
    {
        public int Quantity { get; set; }
        public double Price { get; set; }
        public int CategoryId { get; set; }
        public int WarehouseId { get; set; }
        public Warehouse? Warehouse { get; set; }
        public Category? Category { get; set; }
    }
}
