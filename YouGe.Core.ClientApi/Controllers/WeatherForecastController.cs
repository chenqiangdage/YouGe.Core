using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace YouGe.Core.ClientApi.Controllers
{
    /// <summary>
    /// 天气预报控制器
    /// </summary>
    [ApiController]
    [Route("[controller]")]   
    public class WeatherForecastController : ControllerBase
    {        
        private static readonly string[] Summaries = new[]
        {
            "1", "2", "3", "4", "5", "6", "7", "8", "9", "10"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        /// <summary>
        /// 构造函数 其他接口从构造函数注入
        /// </summary>
        /// <param name="logger"></param>
        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {            
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
