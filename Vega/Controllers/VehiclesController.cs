using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.ApiViewModels;
using Vega.Common;
using Vega.Models;

namespace Vega.Controllers
{
    [Route("/api/vehicles/")]
    public class VehiclesController : Controller
    {
        private VegaDbContext DbContext { get; }
        private IMapper Mapper { get; }
        private VehicleRepository Repository { get; }
        private UnitOfWork UnitOfWork { get; }

        public VehiclesController(VegaDbContext context, IMapper mapper, IVehicleRepository repository, IUnitOfWork uow)
        {
            DbContext = context;
            Mapper = mapper;
            Repository = (VehicleRepository) repository;
            UnitOfWork = (UnitOfWork) uow;
        }

        [HttpGet("get/makes")]
        public async Task<IActionResult> GetMakesAsync()
        {
            List<MakeResource> makes = await DbContext.Makes
                .Include(m => m.Models)
                .ProjectTo<MakeResource>(Mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(makes);
        }

        [HttpGet("get/features")]
        public async Task<IActionResult> GetFeaturesAsync()
        {
            List<KeyValuePairResource> features = await DbContext.Features
                .ProjectTo<KeyValuePairResource>(Mapper.ConfigurationProvider)
                .ToListAsync();

            return Ok(features);
        }

        [HttpGet("get/all")]
        public async Task<IActionResult> GetVehiclesAsync()
        {
            List<VehicleResource> vehicles =
                Mapper.Map<List<Vehicle>, List<VehicleResource>>(await Repository.GetAllVehicles());

            return Ok(vehicles);
        }

        [HttpGet("get/{id}")]
        public async Task<IActionResult> GetVehicleByIdAsync([FromRoute]int id)
        {
            VehicleResource vehicle =
                Mapper.Map<Vehicle, VehicleResource>(await Repository.GetVehicle(id));

            if (vehicle == default)
                return NotFound("Vehicle not found");

            return Ok(vehicle);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddVehicle([FromBody] SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            Vehicle vehicle = Mapper.Map<Vehicle>(saveVehicleResource);

            await Repository.Add(vehicle);
            await UnitOfWork.CompleteAsync();

            vehicle = await Repository.GetVehicle(vehicle.Id);

            return Ok(Mapper.Map<Vehicle, VehicleResource>(vehicle));
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateVehicle([FromRoute]int id, [FromBody]SaveVehicleResource saveVehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (id <= 0)
                return BadRequest("Id must be greater than 0");

            Vehicle vehicle = await Repository.GetVehicle(id);
                
            Mapper.Map(saveVehicleResource, vehicle);

            await UnitOfWork.CompleteAsync();

            return Ok(Mapper.Map<Vehicle, SaveVehicleResource>(vehicle));
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteVehicle([FromRoute]int id)
        {
            Vehicle vehicle = await Repository.GetVehicle(id, false);
            if (vehicle == null)
                return NotFound();
                
            await Repository.Delete(vehicle);
            await UnitOfWork.CompleteAsync();

            return Ok(new OkObjectResult("Vehicle Deleted Successfully."));
        }
    }
}
