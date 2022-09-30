using MetricsAgent.DataAccess;
using MetricsAgent.Models;
using MetricsAgent.Models.Types;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/cpu")]
    [ApiController]
    public class CpuMetricsController : BaseController<CpuMetricsController>
    {
        private readonly ICpuMetricsDataAdapter _cpuMetricsDataAdapter;

        public CpuMetricsController(ICpuMetricsDataAdapter cpuMetricsDataAdapter,
            ILogger<CpuMetricsController> logger):base(logger)
        {
            _cpuMetricsDataAdapter = cpuMetricsDataAdapter;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ValueTime request)
        {
            _logger.LogInformation("Create cpu metric.");
            _cpuMetricsDataAdapter.Create(new CpuMetric
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("GetAll cpu metric.");
            return Ok(_cpuMetricsDataAdapter.GetAll());
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation("Get cpu metric by id.");
            return Ok(_cpuMetricsDataAdapter.GetById(id));
        }

        [HttpPut("update")]
        public IActionResult Update(CpuMetric item)
        {
            _logger.LogInformation("Update cpu metric.");
            _cpuMetricsDataAdapter.Update(new CpuMetric
            {
                Id = item.Id,
                Value = item.Value,
                Time = item.Time
            });
            return Ok();
        }

        [HttpDelete("update")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Delete cpu metric.");
            _cpuMetricsDataAdapter.Delete(id);
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<CpuMetric>> GetCpuMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get cpu metrics for period.");
            return Ok(_cpuMetricsDataAdapter.GetByTimePeriod(fromTime, toTime));
        }  
    } 
}
