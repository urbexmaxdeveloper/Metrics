using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.DataAccess.DataAdapters
{
    public class HddMetricsDataAdapter : BaseDataAdapter, IHddMetricsDataAdapter
    {
        public HddMetricsDataAdapter(IOptions<DatabaseOptions> databaseoptions) : base(databaseoptions)
        { }

        public void Create(HddMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на вставку данных
            connection.Execute("INSERT INTO hddmetrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на удаление данных
            connection.Execute("DELETE FROM hddmetrics WHERE id=@id", new
            {
                value = id
            });
        }

        public IList<HddMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на получение всех данных из таблицы
            var returnList = connection.Query<HddMetric>("SELECT * FROM hddmetrics").ToList();
            return returnList;
        }

        public HddMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            var result = connection.Query<HddMetric>("SELECT * FROM hddmetrics WHERE id=@id", new
            {
                id = id
            });

            if (result is not null)
            {
                return (HddMetric)result;
            }
            else
            {
                return null;
            }
        }


        public void Update(HddMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на обновление данных
            connection.Query("UPDATE hddmetrics SET value = @value, time = @time WHERE id = @id; ", new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }
    }
}
