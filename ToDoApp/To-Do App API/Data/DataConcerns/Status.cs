using System;
using System.Collections.Generic;

namespace Repository.DataConcerns;

public partial class Status
{
    public int StatusId { get; set; }

    public string Status1 { get; set; } = null!;

    public virtual ICollection<TodoTask> TodoTasks { get; set; } = new List<TodoTask>();
}
