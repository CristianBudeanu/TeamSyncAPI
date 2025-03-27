using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamSync.Application.Dto.ProjectDtos
{
    public class ProjectPreviewDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Image { get; set; }
    }
}
