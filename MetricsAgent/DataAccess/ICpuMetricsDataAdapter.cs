using MetricsAgent.Models;

namespace MetricsAgent.DataAccess
{
    public interface ICpuMetricsDataAdapter : IBaseDataAdapter<CpuMetric>
    {
        IList<CpuMetric> GetByTimePeriod(TimeSpan timeFrom, TimeSpan timeTo);
    }
}
