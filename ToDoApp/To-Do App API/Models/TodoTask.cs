using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concerns
{
    public class TodoTask
    {
        public int TaskId { get; set; }

        public string TaskName { get; set; } = null!;

        public string? TaskDescription { get; set; }

        public DateTime CreatedOn { get; set; }

        public DateTime? CompletedOn { get; set; }

        public int UserId { get; set; }

        public int? StatusId { get; set; }

        public string? TaskStatus { get; set; }

    }
}
