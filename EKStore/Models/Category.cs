using EKStore.Models.Abstracts;

namespace EKStore.Models
{
    public class Category:CommonProp
    {
        public List<Product>? Products { get; set; }
    }
}
