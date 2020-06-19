using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using YouGe.Core.Models.DTModel.Manager;
using YouGe.Core.Interface.IServices.IManager;
namespace YouGe.Core.ManagerApi.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IUserAuthService _userAuthService;
        public WeatherForecastController(ILogger<WeatherForecastController> logger,IUserAuthService userAuthService)
        {
            _logger = logger;
            _userAuthService = userAuthService;
        }
        
        [HttpGet]
        [Authorize(Roles = "admin")]
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

        [AllowAnonymous]
        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] LoginModel model)
        {
            var user = _userAuthService.Authenticate(model.UserName, model.Password);
            if (User == null)
            {
                return BadRequest(new { message = "用户名或者密码不正确" });
            }
            return Ok(user);
        }

    }
}
