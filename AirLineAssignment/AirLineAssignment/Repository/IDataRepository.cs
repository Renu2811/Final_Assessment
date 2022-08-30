using AirLineAssignment.Entities;

namespace AirLine_TestCases
{
    public interface IDataRepository<TEntity>
    {
        IEnumerable<TEntity> GetAirLines();

        void Add(AirLine airLine);
    }

}