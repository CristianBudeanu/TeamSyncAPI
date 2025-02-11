using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeamSync.Domain.Entities.ProjectEntities;

namespace TeamSync.Application.Services.Project
{
    public interface IProjectService
    {
        public Task AssignTask(TaskItem task); 
    }
}
