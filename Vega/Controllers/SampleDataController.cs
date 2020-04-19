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

namespace Vega.Controllers
{
    [Route("api/[controller]")]
    public class SampleDataController : Controller
    {
        private VegaDbContext DbContext { get; set; }
        private IMapper Mapper { get; set; }

        public SampleDataController(VegaDbContext context, IMapper mapper)
        {
            DbContext = context;
            Mapper = mapper;
        }

        [HttpGet("/api/makes")]
        public async Task<IEnumerable<MakeViewModel>> GetMakesAsync()
        {
            return await DbContext.Makes.Include(m => m.Models).ProjectTo<MakeViewModel>(Mapper.ConfigurationProvider).ToListAsync();
        }
    }
}
