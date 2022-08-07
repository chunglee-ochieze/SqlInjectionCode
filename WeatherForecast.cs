namespace SqlInjectionCode;

public class WeatherForecast
{
    public DateTime Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}

public class Suburb
{
    public string SuburbName { get; set; }
    public string LgaName { get; set; }
    public string State { get; set; }
    public string PostCode { get; set; }
    public decimal Latitude { get; set; }
    public decimal Longitude { get; set; }
}