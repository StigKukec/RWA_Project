using System;
using System.Collections.Generic;

namespace DataLayer.DALModels;

public partial class UserType
{
    public int IduserType { get; set; }

    public string Type { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
