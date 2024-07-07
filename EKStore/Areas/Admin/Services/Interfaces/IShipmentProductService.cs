using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EKStore.Models;

namespace EKStore.Areas.Admin.Services.Interfaces
{
    public interface IShipmentProductService
    {
        public Task<bool> AddAsync(ShipmentProduct shipmentProduct);
        public Task<bool> DeleteAsync(int id);
        public Task<ShipmentProduct> GetByIdAsync(int id);
        public Task<List<ShipmentProduct>> GetAllAsync();
    }
}