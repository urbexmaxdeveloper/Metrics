using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        public readonly ILogger<T> _logger;
        public BaseController(ILogger<T> logger)
        {
            _logger = logger;
        }
    }
}
