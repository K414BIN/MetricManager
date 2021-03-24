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
            var listEnumerator = _holder.Values.GetEnumerator();
            foreach (var hold in _holder.Values)
            {
                if (hold.Date >= firstDate && hold.Date <= lastDate)
                {
                    var currentValue = listEnumerator.Current;
                 
                    return Ok(currentValue);
                        
                }
            }
            return Ok();
        }
        [HttpPut("update")]
        public IActionResult Update([FromQuery] DateTime date , [FromQuery] int newTemperatureC)
        {
            foreach (var hold in _holder.Values)
            {
       
                if (date==hold.Date)
                {
                    hold.TemperatureC = newTemperatureC;
                    hold.Summary = "Changed by User";
                }

            }
            return Ok();
        }

        [HttpDelete("delete")]
        public IActionResult Delete([FromQuery] DateTime firstDate, [FromQuery] DateTime lastDate)
        {
            int  index = 0;
            int  firstIndex = 0;
            var listEnumerator = _holder.Values.GetEnumerator();
         // подсчитаем сколько всего значений
            foreach (var hold in _holder.Values)
            {
           
                if (hold.Date >= firstDate && hold.Date <= lastDate)
                {
                    index++;
                  
                }
            }
            if (index == 0) return Ok();
            // Теперь найдем первое вхождение
            foreach (var hold in _holder.Values)
            {
                if (hold.Date >= firstDate && hold.Date <= lastDate)
                {
                    var first = listEnumerator.Current;
                    firstIndex = Convert.ToInt32(firstIndex);
                }
            }
            // удалим все значения диапазона
            _holder.Values.RemoveRange(firstIndex, index);
          
            return Ok();
        }
    }
}
