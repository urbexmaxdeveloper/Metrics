using MetricsAgent.DataAccess;
using MetricsAgent.Models;
using MetricsAgent.Models.Types;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : BaseController<HddMetricsController>
    {
        private readonly IHddMetricsDataAdapter _hddMetricsDataAdapter;

        public HddMetricsController(IHddMetricsDataAdapter hddMetricsDataAdapter,
            ILogger<HddMetricsController> logger) : base(logger)
        {
            _hddMetricsDataAdapter = hddMetricsDataAdapter;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ValueTime request)
        {
            _logger.LogInformation("Create hdd metric.");
            _hddMetricsDataAdapter.Create(new HddMetric
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("GetAll hdd metric.");
            return Ok(_hddMetricsDataAdapter.GetAll());
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation("Get hdd metric by id.");
            return Ok(_hddMetricsDataAdapter.GetById(id));
        }

        [HttpPut("update")]
        public IActionResult Update(HddMetric item)
        {
            _logger.LogInformation("Update hdd metric.");
            _hddMetricsDataAdapter.Update(new HddMetric
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
            _logger.LogInformation("Delete hdd metric.");
            _hddMetricsDataAdapter.Delete(id);
            return Ok();
        }
    }
}
