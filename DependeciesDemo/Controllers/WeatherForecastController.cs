using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DependeciesDemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace DependeciesDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private readonly ScopedDisposable _scopedDisposable;
        private readonly TransientDisposable _transientDisposable;
        private readonly SingletonDisposable _singletonDisposable;

        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        public WeatherForecastController(
            ScopedDisposable scopedDisposable,
            TransientDisposable transientDisposable,
            SingletonDisposable singletonDisposable)
        {
            _scopedDisposable = scopedDisposable;
            _transientDisposable = transientDisposable;
            _singletonDisposable = singletonDisposable;
        }

        [HttpGet]
        public async Task<IEnumerable<WeatherForecast>> Get()
        {
            Console.WriteLine($"Get() Start");
            
            var rng = new Random();
            var response = Enumerable.Range(1, 5).Select(index => new WeatherForecast
                {
                    Date = DateTime.Now.AddDays(index),
                    TemperatureC = rng.Next(-20, 55),
                    Summary = Summaries[rng.Next(Summaries.Length)]
                })
                .ToArray();

            await RunWork();

            Console.WriteLine($"Get() End");

            return response;
        }

        private async Task RunWork()
        {
            Console.WriteLine($"Before Task.Run()");
            
            Task.Run(async () =>
            {
                Console.WriteLine($"Task.Run() Work Start.");
                
                await Task.Delay(20000);

                try
                {
                    _singletonDisposable.Use();
                    _transientDisposable.Use();
                    _scopedDisposable.Use();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    throw;
                }
                
                Console.WriteLine($"Task.Run() Work End after 20s.");
            });
            
            Console.WriteLine($"After Task.Run()");
        }
    }
}