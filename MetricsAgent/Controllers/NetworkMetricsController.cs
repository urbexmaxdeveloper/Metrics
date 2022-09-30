using MetricsAgent.DataAccess;
using MetricsAgent.Models;
using MetricsAgent.Models.Types;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/network")]
    [ApiController]
    public class NetworkMetricsController : BaseController<NetworkMetricsController>
    {
        private readonly INetworkMetricsDataAdapter _networkMetricsDataAdapter;

        public NetworkMetricsController(INetworkMetricsDataAdapter networkMetricsDataAdapter,
            ILogger<NetworkMetricsController> logger) : base(logger)
        {
            _networkMetricsDataAdapter = networkMetricsDataAdapter;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ValueTime request)
        {
            _logger.LogInformation("Create network metric.");
            _networkMetricsDataAdapter.Create(new NetworkMetric
            {
                Value = request.Value,
                Time = (long)request.Time.TotalSeconds
            });
            return Ok();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("GetAll network metric.");
            return Ok(_networkMetricsDataAdapter.GetAll());
        }

        [HttpGet("getById")]
        public IActionResult GetById(int id)
        {
            _logger.LogInformation("Get network metric by id.");
            return Ok(_networkMetricsDataAdapter.GetById(id));
        }

        [HttpPut("update")]
        public IActionResult Update(NetworkMetric item)
        {
            _logger.LogInformation("Update network metric.");
            _networkMetricsDataAdapter.Update(new NetworkMetric
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
            _logger.LogInformation("Delete network metric.");
            _networkMetricsDataAdapter.Delete(id);
            return Ok();
        }
    }
}
