using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamSync.Domain.Enums
{
    public enum TaskState
    {
        Pending,
        InProgress,
        Completed,
        Canceled,
        Failed
    }
}
