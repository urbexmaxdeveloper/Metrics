using MetricsAgent.Models;

namespace MetricsAgent.DataAccess
{
    public interface IBaseDataAdapter<T> where T : class
    {
        IList<T> GetAll();
        T GetById(int id);
        void Create(T item);
        void Update(T item);
        void Delete(int id);
    }
}
