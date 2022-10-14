using AutoMapper;
using MetricsAgent.Models;
using MetricsAgent.Models.Dto;
using MetricsAgent.Models.Requests;
using MetricsAgent.Services;
using MetricsAgent.Services.Impl;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MetricsAgent.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RamMerticsController : ControllerBase
    {
        private readonly ILogger<RamMerticsController> _logger;
        private readonly IRamMetricsRepository _ramMetricsRepository;
        private readonly IMapper _mapper;

        public RamMerticsController(IRamMetricsRepository ramMetricsRepository,
            ILogger<RamMerticsController> logger,
            IMapper mapper)
        {
            _ramMetricsRepository = ramMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] RamMetricCreateRequest request)
        {
            _logger.LogInformation("Create cpu metric.");
            _ramMetricsRepository.Create(_mapper.Map<RamMetric>(request));
            //new Models.CpuMetric
            //{
            //    Value = request.Value,
            //    Time = (long)request.Time.TotalSeconds
            //});
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<RamMetricDto>> GetRamMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get cpu metrics call.");
            return Ok(_mapper.Map<List<RamMetricDto>>(_ramMetricsRepository.GetByTimePeriod(fromTime, toTime)));



            //return Ok(
            //    _cpuMetricsRepository.GetByTimePeriod(fromTime, toTime).Select(element => new CpuMetricDto
            //{
            //    Value = element.Value,
            //    Time = element.Time
            //}).ToList());
        }

        [HttpGet("all")]
        public ActionResult<IList<RamMetricDto>> GetRamMetricsAll() => Ok(_mapper.Map<List<RamMetricDto>>(_ramMetricsRepository.GetAll()));

    }

    
}

