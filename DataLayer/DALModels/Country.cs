using System;
using System.Collections.Generic;

namespace DataLayer.DALModels;

public partial class Country
{
    public int Idcountry { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
