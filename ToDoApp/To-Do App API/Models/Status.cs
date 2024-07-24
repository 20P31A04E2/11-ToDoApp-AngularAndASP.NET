using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concerns
{
    public class Status
    {
        public int StatusId { get; set; }

        public string Status1 { get; set; } = null!;

        public virtual ICollection<TodoTask> TodoTasks { get; set; } = new List<TodoTask>();
    }
}
