using MetricsAgent.Models;

namespace MetricsAgent.DataAccess
{
    public interface ICpuMetricsDataAdapter : IBaseDataAdapter<CpuMetric>
    {
        /*CRUD methods are inherited*/
        IList<CpuMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
