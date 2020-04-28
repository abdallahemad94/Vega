using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Vega.Models;

namespace Vega.Common
{
    public class VehicleRepository : IVehicleRepository
    {
        private VegaDbContext DbContext { get; }

        public VehicleRepository(VegaDbContext context) { DbContext = context; }

        public async Task<Vehicle> GetVehicle(int id, bool includeRelated = true)
        {
            if (!includeRelated)
                return await DbContext.Vehicles.FindAsync(id);

            return await DbContext.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(f => f.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make)
                .SingleOrDefaultAsync(v => v.Id == id);
        }

        public async Task<List<Vehicle>> GetAllVehicles()
        {
            return await DbContext.Vehicles
                .Include(v => v.Features)
                    .ThenInclude(f => f.Feature)
                .Include(v => v.Model)
                    .ThenInclude(m => m.Make) 
                .ToListAsync();
        }

        public async Task Add(Vehicle vehicle)
        {
            await DbContext.Vehicles.AddAsync(vehicle);
        }

        public async Task Delete(Vehicle vehicle)
        {
            await Task.Run( () => DbContext.Vehicles.Remove(vehicle));
        }
    }
}
