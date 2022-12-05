using Microsoft.Extensions.Options;

namespace MetricsAgent.DataAccess.DataAdapters
{
    public abstract partial class BaseDataAdapter
    {
        public string ConnectionString { get; protected set; }

        public readonly IOptions<DatabaseOptions> _databaseOptions;

        public BaseDataAdapter(IOptions<DatabaseOptions> databaseoptions)
        {
            _databaseOptions = databaseoptions;
            ConnectionString = databaseoptions.Value.ConnectionString;
        }
    }
}
