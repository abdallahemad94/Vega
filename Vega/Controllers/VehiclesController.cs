using System;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using Vega.ApiViewModels;
using Vega.Common;
using Vega.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Drawing;
using System.Linq;
using Microsoft.Extensions.Options;

namespace Vega.Controllers
{
    [Route("/api/vehicles/")]
    public class VehiclesController : Controller
    {
        private VegaDbContext DbContext { get; }
        private IMapper Mapper { get; }
        public IHostingEnvironment Env { get; }
        private VehicleRepository Repository { get; }
        private UnitOfWork UnitOfWork { get; }
        private PhotosSettings PhotosSettings { get; }
        public VehiclesController(VegaDbContext context, IMapper mapper, IVehicleRepository repository, IUnitOfWork uow,
            IHostingEnvironment env, IOptionsSnapshot<PhotosSettings> options)
        {
            DbContext = context;
            Mapper = mapper;
            Env = env;
            Repository = (VehicleRepository) repository;
            UnitOfWork = (UnitOfWork) uow;
            PhotosSettings = options.Value;
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

        [HttpGet("get/photos/{vehicleId}")]
        public async Task<IActionResult> GetPhotos([FromRoute] int vehicleId)
        {
            if (vehicleId == default)
                return BadRequest();
            if (await Repository.GetVehicle(vehicleId) == default)
                return NotFound();
            List<VehiclePhoto> photos = await Repository.GetPhotos(vehicleId);

            return Ok(Mapper.Map<List<VehiclePhoto>, List<VehiclePhotoResource>>(photos));
        }

        [HttpPost("add/photos/{vehicleId}")]
        public async Task<IActionResult> AddPhoto([FromRoute]int vehicleId, [FromForm]IFormFile file)
        {
            if (vehicleId == default || file == null)
                return BadRequest();
            if (await Repository.GetVehicle(vehicleId) == default)
                return NotFound();
            if (file.Length == 0)
                return BadRequest("empty file");
            if (file.Length > PhotosSettings.MaxBytes)
                return BadRequest("big file");
            if (!PhotosSettings.IsSupportedFileType(file.FileName))
                return BadRequest("not supported type");

            string fileRootPath = Path.Combine(Env.WebRootPath, "Photos");
            if (!Directory.Exists(fileRootPath))
                Directory.CreateDirectory(fileRootPath);
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string fullFilePath = Path.Combine(fileRootPath, fileName);
            using(FileStream sw = new FileStream(fullFilePath, FileMode.Create))
            {
                await file.CopyToAsync(sw);
            }

            Image thumb;
            using(var s = new FileStream(fullFilePath, FileMode.Open))
            {
                Image img = Image.FromStream(s);
                thumb = img.GetThumbnailImage(32, 32, () => false, IntPtr.Zero);
            }
            string thumbFileName = Path.GetFileNameWithoutExtension(fileName) + "-thumb" + Path.GetExtension(fileName);
            thumb.Save(Path.Combine(fileRootPath, thumbFileName));

            VehiclePhoto photo = new VehiclePhoto() { FileName = fileName, VehicleId = vehicleId };
            await Repository.AddPhoto(photo);
            await UnitOfWork.CompleteAsync();

            return Ok(Mapper.Map<VehiclePhoto, VehiclePhotoResource>(photo));
        }
    }
}
