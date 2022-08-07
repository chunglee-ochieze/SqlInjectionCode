using System.Data;
using Microsoft.AspNetCore.Mvc;

namespace SqlInjectionCode.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IDapperHelper _dapper;

    private static readonly string[] Summaries = 
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };
    
    public WeatherForecastController(IDapperHelper dapper)
    {
        _dapper = dapper;
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get()
    {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast
        {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpPost(Name = "PostInjection")]
    public async Task<IActionResult> Post(string suburbName)
    {
        var query = "SELECT * FROM Suburb WHERE SuburbName LIKE '%" + suburbName + "%'";

        var dbSubs = await _dapper.GetAll<Suburb>(query, null, CommandType.Text);

        return Ok(dbSubs);
    }
}