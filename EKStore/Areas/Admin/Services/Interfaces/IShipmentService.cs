using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EKStore.Models;

namespace EKStore.Areas.Admin.Services.Interfaces
{
    public interface IShipmentService
    {
        public interface IShipmentService
        {
            Task<List<Shipment>> GetAllAsync();
            Task<Shipment> GetByIdAsync(int id);
            Task<bool> AddAsync(Shipment shipment);
            Task<bool> UpdateAsync(Shipment shipment);
            Task<bool> DeleteAsync(int id);
        }

    }
}