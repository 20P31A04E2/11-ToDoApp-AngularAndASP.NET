using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Concerns
{
    public class User
    {
        public int UserId { get; set; }

        public string UserName { get; set; } = null!;

        public string Password { get; set; } = null!;

        //public virtual ICollection<TodoTask> TodoTasks { get; set; } = new List<TodoTask>();
    }
}
