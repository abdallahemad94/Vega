using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.Models;

namespace Vega.Common
{
    public interface IVehicleRepository
    {
        Task<Vehicle> GetVehicle(int id, bool includeRelated);
        Task<List<Vehicle>> GetAllVehicles();
        Task Add(Vehicle vehicle);
        Task Delete(Vehicle vehicle);
    }
}