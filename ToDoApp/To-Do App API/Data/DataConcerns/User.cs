using System;
using System.Collections.Generic;

namespace Repository.DataConcerns;

public partial class User
{
    public int UserId { get; set; }

    public string UserName { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<TodoTask> TodoTasks { get; set; } = new List<TodoTask>();
}
