using System;
using System.Collections.Generic;

namespace Repository.DataConcerns;

public partial class TodoTask
{
    public int TaskId { get; set; }

    public string TaskName { get; set; } = null!;

    public string? TaskDescription { get; set; }

    public DateTime CreatedOn { get; set; }

    public DateTime? CompletedOn { get; set; }

    public int UserId { get; set; }

    public int StatusId { get; set; }

    public virtual Status Status { get; set; } = null!;

    public virtual User User { get; set; } = null!;
}
