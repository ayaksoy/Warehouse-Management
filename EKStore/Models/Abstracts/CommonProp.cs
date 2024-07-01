using System.ComponentModel.DataAnnotations;

namespace EKStore.Models.Abstracts
{
    abstract public class CommonProp
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public bool IsDelete { get; set; } = false;
        public bool IsStatus { get; set; } = true;

    }
}
