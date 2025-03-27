namespace TeamSync.Domain.Entities.ProjectEntities
{
    public class ProjectRole
    {
        public Guid Id { get; set; }
        public string ProjectRoleName { get; set; }
        public List<ProjectUserRole> ProjectUserRoles { get; set; } = [];
    }
}
