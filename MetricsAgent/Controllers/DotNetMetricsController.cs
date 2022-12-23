using AutoMapper;
using MetricsAgent.DataAccess;
using MetricsAgent.Models;
using MetricsAgent.Models.Types;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/metrics/dotnet")]
    [ApiController]
    public class DotnetMetricsController : BaseController<DotnetMetricsController>
    {
        private readonly IDotnetMetricsDataAdapter _dotnetMetricsDataAdapter;

        public DotnetMetricsController(IDotnetMetricsDataAdapter dotnetMetricsDataAdapter,
            ILogger<DotnetMetricsController> logger, Mapper mapper) : base(logger, mapper)
        {
            _dotnetMetricsDataAdapter = dotnetMetricsDataAdapter;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ValueTime request)
        {
            _logger.LogInformation("Create dotnet metric.");
            _dotnetMetricsDataAdapter.Create(_mapper.Map<DotnetMetric>(request));
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
        public IActionResult Update(DotnetMetric item)
        {
            _logger.LogInformation("Update dotnet metric.");
            _dotnetMetricsDataAdapter.Update(_mapper.Map<DotnetMetric>(item));
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete(int id)
        {
            _logger.LogInformation("Delete dotnet metric.");
            _dotnetMetricsDataAdapter.Delete(id);
            return Ok();
        }
    }
}
