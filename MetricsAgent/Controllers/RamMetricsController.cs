using MetricsAgent.DataAccess;
using MetricsAgent.Models;
using MetricsAgent.Models.Types;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/ram")]
    [ApiController]
    public class RamMetricsController : BaseController<RamMetricsController>
    {
        private readonly IRamMetricsDataAdapter _ramMetricsDataAdapter;

        public RamMetricsController(IRamMetricsDataAdapter ramMetricsDataAdapter,
            ILogger<RamMetricsController> logger) : base(logger)
        {
            _ramMetricsDataAdapter = ramMetricsDataAdapter;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ValueTime request)
        {
            _logger.LogInformation("Create ram metric.");
            _ramMetricsDataAdapter.Create(new RamMetric
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("GetAll ram metric.");
            return Ok(_ramMetricsDataAdapter.GetAll());
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation("Get ram metric by id.");
            return Ok(_ramMetricsDataAdapter.GetById(id));
        }

        [HttpPut("update")]
        public IActionResult Update(RamMetric item)
        {
            _logger.LogInformation("Update ram metric.");
            _ramMetricsDataAdapter.Update(new RamMetric
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
            _logger.LogInformation("Delete ram metric.");
            _ramMetricsDataAdapter.Delete(id);
            return Ok();
        }
    }
}
