using AutoMapper;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [ApiController]
    public class BaseController<T> : ControllerBase
    {
        public readonly ILogger<T> _logger;
        public readonly IMapper _mapper;
        public BaseController(ILogger<T> logger, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
        }
    }
}
