using System;
using System.Collections.Generic;

namespace CW10.Models;

public partial class Student
{
    public int Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public int Age { get; set; }

    public virtual ICollection<Group> Groups { get; set; } = new List<Group>();
}
