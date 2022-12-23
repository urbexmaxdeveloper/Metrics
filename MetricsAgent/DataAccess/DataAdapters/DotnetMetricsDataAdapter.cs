using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.DataAccess.DataAdapters
{
    public class DotnetMetricsDataAdapter : BaseDataAdapter, IDotnetMetricsDataAdapter
    {
        public DotnetMetricsDataAdapter(IOptions<DatabaseOptions> databaseoptions) : base(databaseoptions)
        {}

        public void Create(DotnetMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на вставку данных
            connection.Query("INSERT INTO dotnetmetrics(value, time) VALUES(@value, @time)",new
            {
                value = item.Value,
                time = item.Time
            });            
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на удаление данных
            connection.Execute("DELETE FROM dotnetmetrics WHERE id=@id",new
            {
                id = id
            });
        }

        public IList<DotnetMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на получение всех данных из таблицы
            var returnList = connection.Query<DotnetMetric>("SELECT * FROM dotnetmetrics").ToList();
            return returnList;
        }

        public DotnetMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на получение записи из таблицы по id
            var result = connection.Query<DotnetMetric>("SELECT * FROM dotnetmetrics WHERE id=@id", new
            {
                id = id
            });

            if (result is not null)
            {
                return (DotnetMetric)result;
            }
            else return null;
        }

        public void Update(DotnetMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на обновление данных
            connection.Execute("UPDATE dotnetmetrics SET value = @value, time = @time WHERE id = @id; ", new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }
    }
}
