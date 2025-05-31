using System;
using System.Collections.Generic;

namespace CW10.Models;

public partial class Partium
{
    public int Id { get; set; }

    public string Nazwa { get; set; } = null!;

    public string? Skrot { get; set; }

    public DateTime DataZalozenia { get; set; }

    public virtual ICollection<Przynaleznosc> Przynaleznoscs { get; set; } = new List<Przynaleznosc>();
}
