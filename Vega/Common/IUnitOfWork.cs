using System.Threading.Tasks;

namespace Vega.Common
{
    public interface IUnitOfWork
    {
        Task CompleteAsync();
    }
}