using System.Threading.Tasks;
using Ordering.Domain.Core;

namespace Ordering.Repository
{
    public interface IOrderRepository<in TEntity> where TEntity : Entity
    {
        Task SaveAsAsync(TEntity entity);
    }
}