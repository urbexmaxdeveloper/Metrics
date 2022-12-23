using Dapper;
using MetricsAgent.Models;
using Microsoft.Extensions.Options;
using System.Data.SQLite;

namespace MetricsAgent.DataAccess.DataAdapters
{
    public class CpuMetricsDataAdapter: BaseDataAdapter,ICpuMetricsDataAdapter
    {
        public CpuMetricsDataAdapter(IOptions<DatabaseOptions> databaseoptions) : base(databaseoptions)
        {}

        public void Create(CpuMetric item)
        {
            // Прописываем и выполняем SQL-запрос на вставку данных
            using var connection = new SQLiteConnection(ConnectionString);
            connection.Execute("INSERT INTO cpumetrics(value, time) VALUES(@value, @time)", new
            {
                value = item.Value,
                time = item.Time
            });
        }

        public void Delete(int id)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на удаление данных
            connection.Execute("DELETE FROM cpumetrics WHERE id=@id", new
            {
                id = id
            });
        }

        public IList<CpuMetric> GetAll()
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на получение всех данных из таблицы
            var returnList = connection.Query<CpuMetric>("SELECT * FROM cpumetrics").ToList();           
            return returnList;
        }

        public CpuMetric GetById(int id)
        {
            //Прописываем в команду SQL-запрос на получение всех данных по id записи
            using var connection = new SQLiteConnection(ConnectionString);
            var result = connection.Query<CpuMetric>("SELECT * FROM cpumetrics WHERE id=@id", new
            {
                id = id
            });

            if (result is not null)
            {
                return (CpuMetric)result;
            }
            else return null;
        }

        public IList<CpuMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на получение всех данных за период из таблицы
            var returnList = connection.Query<CpuMetric>("SELECT * FROM cpumetrics where time >= @timeFrom and time <= @timeTo", new
            {
                timeFrom = timeFrom,
                timeTo = timeTo
            }).ToList();           
            return returnList;
        }

        public void Update(CpuMetric item)
        {
            using var connection = new SQLiteConnection(ConnectionString);
            // Прописываем в команду SQL-запрос на обновление данных
            connection.Query("UPDATE cpumetrics SET value = @value, time = @time WHERE id = @id; ",new
            {
                value = item.Value,
                time = item.Time,
                id = item.Id
            });
        }
    }
}
