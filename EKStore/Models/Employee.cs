using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EKStore.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string? FirsName { get; set; }
        public string? LastName { get; set; }
        public int RoleId { get; set; }
        public Role? Role { get; set; }

    }
}