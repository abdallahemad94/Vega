using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Vega.Models;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using Vega.Common;
using AutoMapper.QueryableExtensions;
using Vega.ApiViewModels;
using System.Security.Cryptography.X509Certificates;
using System.Data;
using System.ComponentModel;
using System.Dynamic;
using System.Net;
using Microsoft.EntityFrameworkCore.Storage;

namespace Vega.Controllers
{
    [Route("/api/vehicles/")]
    public class VehiclesController : Controller
    {
        private VegaDbContext DbContext { get; set; }
        private IMapper Mapper { get; set; }

        public VehiclesController(VegaDbContext context, IMapper mapper)
        {
            DbContext = context;
            Mapper = mapper;
        }

        [HttpGet("get/makes")]
        public async Task<IActionResult> GetMakesAsync()
        {
            return Ok(await DbContext.Makes.Include(m => m.Models).ProjectTo<MakeViewModel>(Mapper.ConfigurationProvider).ToListAsync());
        }

        [HttpGet("get/features")]
        public async Task<IActionResult> GetFeaturesAsync()
        {
            return Ok(await DbContext.Features.ProjectTo<FeatureViewModel>(Mapper.ConfigurationProvider).ToListAsync());
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetVehiclesAsync()
        {
            return Ok(await DbContext.Vehicles.Include(v => v.Features.Select(f => f.Feature)).ProjectTo<VehicleViewModel>(Mapper.ConfigurationProvider).ToListAsync());
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetVehicleByIdAsync([FromRoute]int id)
        {
            Vehicle vehicle = await DbContext.Vehicles
                .Include(v => v.Model)
                .FirstOrDefaultAsync(v => v.Id == id);
            if (vehicle == default || vehicle == null)
                return NotFound("Vehicle not found");
            return Ok(Mapper.Map<Vehicle, VehicleViewModel>(vehicle));
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddVehicle([FromBody] VehicleViewModel vehicleViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Vehicle vehicle = Mapper.Map<Vehicle>(vehicleViewModel);

            await DbContext.Vehicles.AddAsync(vehicle);
            await DbContext.SaveChangesAsync();

            return Ok(Mapper.Map<Vehicle, VehicleViewModel>(vehicle));
        } 

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateVehicle([FromRoute]int id, [FromBody]VehicleViewModel vehicleViewModel)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if(id <= 0 )
                return BadRequest("Id must be greater than 0");

            Vehicle vehicle = await DbContext.Vehicles.FindAsync(id);
            if (vehicle == null)
                return NotFound($"Vehicle with id = {id} not found");

            Mapper.Map<VehicleViewModel, Vehicle>(vehicleViewModel, vehicle);
            await DbContext.SaveChangesAsync();

            return Ok(Mapper.Map<Vehicle, VehicleViewModel>(vehicle));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute]int id)
        {
            Vehicle vehicle = await DbContext.Vehicles.FindAsync(id);
            
            if (vehicle == null)
                return NotFound($"Vehicle with id = {id} not found");
           
            DbContext.Vehicles.Remove(vehicle);
            await DbContext.SaveChangesAsync();

            return Ok("Vehicle Deleted Successfully.");
        }
    }
}
