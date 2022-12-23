using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;
using System.Diagnostics.CodeAnalysis;

namespace MetricsAgent.DataAccess.DataAdapters
{
    public class NetworkMetricsDataAdapter : BaseDataAdapter, INetworkMetricsDataAdapter
    {
        public NetworkMetricsDataAdapter(IOptions<DatabaseOptions> databaseoptions) : base(databaseoptions)
        { }

        public void Create(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на вставку данных
            connection.Query("INSERT INTO networkmetrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на удаление данных
            connection.Execute("DELETE FROM networkmetrics WHERE id=@id", new
            {
                id = id
            };
        }

        public IList<NetworkMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на получение всех данных из таблицы
            var returnList = connection.Query<NetworkMetric>("SELECT * FROM networkmetrics").ToList();
            return returnList;
        }

        public NetworkMetric GetById(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Open();
            using var cmd = new SQLiteCommand(connection);
            var result = connection.Query<NetworkMetric>("SELECT * FROM networkmetrics WHERE id=@id", new
            {
                id = id
            });

            if (result is not null)
            {
                return (NetworkMetric)result;
            }
            else
            {
                return null;
            }
        }

        public void Update(NetworkMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на обновление данных
            connection.Execute("UPDATE networkmetrics SET value = @value, time = @time WHERE id = @id; ", new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }
    }
}
