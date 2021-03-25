using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.IO;

namespace MetricManagerTests.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CrudController : ControllerBase
    {
        private readonly ValuesHolder _holder;

        public CrudController(ValuesHolder holder)
        {
            _holder = holder;
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] WeatherForecastDto  input)
        {
            WeatherForecast weatherForecast = new WeatherForecast();
            weatherForecast.TemperatureC = input.TemperatureC;
            weatherForecast.Date = input.Date;
            weatherForecast.Summary = "Created by Web service";
           _holder.Values.Add(weatherForecast);
            return Ok();
        }

        [HttpGet("read")]
        public IActionResult Read()
        {
         return Ok(_holder.Values.ToArray());
        }
        [HttpGet("browse")]
        public IActionResult Browse([FromQuery] DateTime firstDate, [FromQuery] DateTime lastDate)
        {
            var listOfresult = new List<WeatherForecast>();
            foreach (var weatherForecast in _holder.Values)
            {
                if (weatherForecast.Date >= firstDate && weatherForecast.Date <= lastDate)
                {
                   listOfresult.Add(weatherForecast);
                }
            }
            return Ok(listOfresult.ToArray());
        }
        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date , [FromQuery] int newTemperatureC)
        {
            foreach (var weatherForecast in _holder.Values)
            {
                if (date==weatherForecast.Date)
                {
                    weatherForecast.TemperatureC = newTemperatureC;
                    weatherForecast.Summary = "Changed by User";
                }
            }
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime firstDate, [FromQuery] DateTime lastDate)
        {
            _holder.Values.RemoveAll(item => item.Date >= firstDate && item.Date <= lastDate);
            return Ok();
        }
    }
}
