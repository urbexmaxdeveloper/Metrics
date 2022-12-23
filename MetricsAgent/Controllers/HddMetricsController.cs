using AutoMapper;
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
            ILogger<HddMetricsController> logger ,Mapper mapper) : base(logger, mapper)
        {
            _hddMetricsDataAdapter = hddMetricsDataAdapter;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ValueTime request)
        {
            _logger.LogInformation("Create hdd metric.");
            _hddMetricsDataAdapter.Create(_mapper.Map<HddMetric>(request));
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
            _hddMetricsDataAdapter.Update(_mapper.Map<HddMetric>(item));
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
