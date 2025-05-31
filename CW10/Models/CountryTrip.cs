namespace CW10.Models;

public class CountryTrip
{
    public int IdCountry { get; set; }
    public int IdTrip { get; set; }

    public virtual Country Country { get; set; } = null!;
    public virtual Trip Trip { get; set; } = null!;
}