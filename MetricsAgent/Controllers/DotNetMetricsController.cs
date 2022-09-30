using MetricsAgent.DataAccess;
using MetricsAgent.Models;
using MetricsAgent.Models.Types;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotNetMetricsController : BaseController<DotNetMetricsController>
    {
        private readonly IDotnetMetricsDataAdapter _dotnetMetricsDataAdapter;

        public DotNetMetricsController(IDotnetMetricsDataAdapter dotnetMetricsDataAdapter,
            ILogger<DotNetMetricsController> logger) : base(logger)
        {
            _dotnetMetricsDataAdapter = dotnetMetricsDataAdapter;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ValueTime request)
        {
            _logger.LogInformation("Create dotnet metric.");
            _dotnetMetricsDataAdapter.Create(new DontnetMetric
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("GetAll dotnet metric.");
            return Ok(_dotnetMetricsDataAdapter.GetAll());
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation("Get dotnet metric by id.");
            return Ok(_dotnetMetricsDataAdapter.GetById(id));
        }

        [HttpPut("update")]
        public IActionResult Update(DontnetMetric item)
        {
            _logger.LogInformation("Update dotnet metric.");
            _dotnetMetricsDataAdapter.Update(new DontnetMetric
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
            _logger.LogInformation("Delete dotnet metric.");
            _dotnetMetricsDataAdapter.Delete(id);
            return Ok();
        }
    }
}
