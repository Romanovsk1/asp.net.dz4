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
    [Route("api/metrics/hdd")]
    [ApiController]
    public class HddMetricsController : ControllerBase
    {
        private readonly ILogger<HddMetricsController> _logger;
        private readonly IHddMetricsRepository _hddMetricsRepository;
        private readonly IMapper _mapper;

        public HddMetricsController(IHddMetricsRepository hddMetricsRepository,
            ILogger<HddMetricsController> logger,
            IMapper mapper)
        {
            _hddMetricsRepository = hddMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] HddMetricCreateRequest request)
        {
            _logger.LogInformation("Create cpu metric.");
            _hddMetricsRepository.Create(_mapper.Map<HddMetric>(request));
            //new Models.CpuMetric
            //{
            //    Value = request.Value,
            //    Time = (long)request.Time.TotalSeconds
            //});
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<HddMetricDto>> GetHddMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get cpu metrics call.");
            return Ok(_mapper.Map<List<CpuMetricDto>>(_hddMetricsRepository.GetByTimePeriod(fromTime, toTime)));



            //return Ok(
            //    _cpuMetricsRepository.GetByTimePeriod(fromTime, toTime).Select(element => new CpuMetricDto
            //{
            //    Value = element.Value,
            //    Time = element.Time
            //}).ToList());
        }

        [HttpGet("all")]
        public ActionResult<IList<HddMetricDto>> GetHddMetricsAll() => Ok(_mapper.Map<List<HddMetricDto>>(_hddMetricsRepository.GetAll()));

    }

    
}

