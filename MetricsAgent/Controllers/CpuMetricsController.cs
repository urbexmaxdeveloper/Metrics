using AutoMapper;
using MetricsAgent.DataAccess;
using MetricsAgent.DataAccess.DataAdapters;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
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
            ILogger<CpuMetricsController> logger, Mapper mapper):base(logger,mapper)
        {
            _cpuMetricsDataAdapter = cpuMetricsDataAdapter;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ValueTime request)
        {
            _logger.LogInformation("Create cpu metric.");
            _cpuMetricsDataAdapter.Create(_mapper.Map<CpuMetric>(request));            
            return Ok();
        }

        [HttpGet("getall")]
        public IActionResult GetAll()
        {
            _logger.LogInformation("GetAll cpu metric.");
            return Ok(_mapper.Map<List<CpuMetricDto>>(_cpuMetricsDataAdapter.GetAll()));
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
            _cpuMetricsDataAdapter.Update(_mapper.Map<CpuMetric>(item));
            return Ok();
        }

        [HttpDelete("delete")]
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
            return Ok(_mapper.Map<List<CpuMetricDto>>(_cpuMetricsDataAdapter.GetByTimePeriod(fromTime, toTime)));
        }  
    } 
}
