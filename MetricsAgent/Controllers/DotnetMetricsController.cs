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
    public class DotnetMetricsController : ControllerBase
    {
        private readonly ILogger<DotnetMetricsController> _logger;
        private readonly IDotnetMetricsRepository _dotnetMetricsRepository;
        private readonly IMapper _mapper;

        public DotnetMetricsController(IDotnetMetricsRepository dotnetMetricsRepository,
            ILogger<DotnetMetricsController> logger,
            IMapper mapper)
        {
            _dotnetMetricsRepository = dotnetMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] DotnetMetricCreateRequest request)
        {
            _logger.LogInformation("Create cpu metric.");
            _dotnetMetricsRepository.Create(_mapper.Map<DotnetMetric>(request));
            //new Models.CpuMetric
            //{
            //    Value = request.Value,
            //    Time = (long)request.Time.TotalSeconds
            //});
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<DotnetMetricDto>> GetDotnetMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get cpu metrics call.");
            return Ok(_mapper.Map<List<CpuMetricDto>>(_dotnetMetricsRepository.GetByTimePeriod(fromTime, toTime)));



            //return Ok(
            //    _cpuMetricsRepository.GetByTimePeriod(fromTime, toTime).Select(element => new CpuMetricDto
            //{
            //    Value = element.Value,
            //    Time = element.Time
            //}).ToList());
        }

        [HttpGet("all")]
        public ActionResult<IList<DotnetMetricDto>> GetCpuMetricsAll() => Ok(_mapper.Map<List<DotnetMetricDto>>(_dotnetMetricsRepository.GetAll()));

    }

}

