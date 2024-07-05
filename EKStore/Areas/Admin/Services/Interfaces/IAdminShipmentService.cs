using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EKStore.Models;

namespace EKStore.Areas.Admin.Services.Interfaces
{
    public interface IAdminShipmentService
    {
        public Task<bool> AddAsync(Shipment shipment);
        public Task<bool> UpdateAsync(Shipment shipment);
        public Task<bool> DeleteAsync(int id);
        public Task<Shipment> GetByIdAsync(int id);
        public Task<List<Shipment>> GetAllAsync();
    }
}