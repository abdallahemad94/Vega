using System.Threading.Tasks;

namespace Vega.Common
{
    public class UnitOfWork : IUnitOfWork
    {
        private VegaDbContext DbContext { get; set; }

        public UnitOfWork(VegaDbContext context)
        {
            DbContext = context;
        }

        public async Task CompleteAsync()
        {
            await DbContext.SaveChangesAsync();
        }
    }
}
