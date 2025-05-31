using System;
using System.Collections.Generic;

namespace CW10.Models;

public partial class Polityk
{
    public int Id { get; set; }

    public string Imie { get; set; } = null!;

    public string Nazwisko { get; set; } = null!;

    public string? Powiedzenie { get; set; }

    public virtual ICollection<Przynaleznosc> Przynaleznoscs { get; set; } = new List<Przynaleznosc>();
}
