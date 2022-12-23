using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.DataAccess.DataAdapters
{
    public class RamMetricsDataAdapter : BaseDataAdapter, IRamMetricsDataAdapter
    {
        public RamMetricsDataAdapter(IOptions<DatabaseOptions> databaseoptions) : base(databaseoptions)
        { }

        public void Create(RamMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на вставку данных
            connection.Execute("INSERT INTO rammetrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на удаление данных
            connection.Execute("DELETE FROM rammetrics WHERE id=@id", new
            {
                id = id
            });
        }

        public IList<RamMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на получение всех данных из таблицы
            var returnList = connection.Query<RamMetric>("SELECT * FROM rammetrics").ToList();
            return returnList;
        }

        public RamMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на получение записи  из таблицы по id
            var result = connection.Query<RamMetric>("SELECT * FROM rammetrics WHERE id=@id", new
            {
                id = id
            });

            if (result is not null)
            {
                return (RamMetric)result;
            }
            else
            {
                return null;
            }
        }

        public void Update(RamMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на обновление данных
            connection.Execute("UPDATE rammetrics SET value = @value, time = @time WHERE id = @id; ", new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }
    }
}
