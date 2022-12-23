using AutoMapper;
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
            ILogger<RamMetricsController> logger, Mapper mapper) : base(logger, mapper)
        {
            _ramMetricsDataAdapter = ramMetricsDataAdapter;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ValueTime request)
        {
            _logger.LogInformation("Create ram metric.");
            _ramMetricsDataAdapter.Create(_mapper.Map<RamMetric>(request));
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
            _ramMetricsDataAdapter.Update(_mapper.Map<RamMetric>(item));
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
