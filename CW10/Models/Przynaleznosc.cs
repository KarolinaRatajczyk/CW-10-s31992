using System;
using System.Collections.Generic;

namespace CW10.Models;

public partial class Przynaleznosc
{
    public int Id { get; set; }

    public int PartiaId { get; set; }

    public int PolitykId { get; set; }

    public DateTime Od { get; set; }

    public DateTime? Do { get; set; }

    public virtual Partium Partia { get; set; } = null!;

    public virtual Polityk Polityk { get; set; } = null!;
}
