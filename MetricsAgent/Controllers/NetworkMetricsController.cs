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
    public class NetworkMetricsController : ControllerBase
    {
        private readonly ILogger<NetworkMetricsController> _logger;
        private readonly INetworkMetricsRepository _networkMetricsRepository;
        private readonly IMapper _mapper;

        public NetworkMetricsController(INetworkMetricsRepository networkMetricsRepository,
            ILogger<NetworkMetricsController> logger,
            IMapper mapper)
        {
            _networkMetricsRepository = networkMetricsRepository;
            _logger = logger;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] NetworkMetricCreateRequest request)
        {
            _logger.LogInformation("Create cpu metric.");
            _networkMetricsRepository.Create(_mapper.Map<NetworkMetric>(request));
            //new Models.CpuMetric
            //{
            //    Value = request.Value,
            //    Time = (long)request.Time.TotalSeconds
            //});
            return Ok();
        }

        [HttpGet("from/{fromTime}/to/{toTime}")]
        //[Route("from/{fromTime}/to/{toTime}")]
        public ActionResult<IList<NetworkMetricDto>> GetNetworkMetrics([FromRoute] TimeSpan fromTime, [FromRoute] TimeSpan toTime)
        {
            _logger.LogInformation("Get cpu metrics call.");
            return Ok(_mapper.Map<List<NetworkMetricDto>>(_networkMetricsRepository.GetByTimePeriod(fromTime, toTime)));



            //return Ok(
            //    _cpuMetricsRepository.GetByTimePeriod(fromTime, toTime).Select(element => new CpuMetricDto
            //{
            //    Value = element.Value,
            //    Time = element.Time
            //}).ToList());
        }

        [HttpGet("all")]
        public ActionResult<IList<NetworkMetricDto>> GetNetworkMetricsAll() => Ok(_mapper.Map<List<NetworkMetricDto>>(_networkMetricsRepository.GetAll()));

    }

    public class MyType
    {
        public int Prop1 { get; set; }
        public int Prop2 { get; set; }
    }
}

