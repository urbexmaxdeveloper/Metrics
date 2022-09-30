namespace MetricsAgent.DataAccess.DataAdapters
{
    public abstract partial class BaseDataAdapter
    {
        public string ConnectionString { get; protected set; }

        public BaseDataAdapter()
        {
            ConnectionString = "Data Source=metrics.db;Version=3;Pooling=true;Max Pool Size=100;";
        }
    }
}
