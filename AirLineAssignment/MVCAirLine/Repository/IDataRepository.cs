using MVCAirLine.Models;

namespace MVCAirLine.Repository
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAll();

        void Add(AirViewModel airLine);
    }
}

