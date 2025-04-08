namespace TeamSync.Application.Services.ProjectServices.InvitationServices
{
    public interface IInvitationService
    {
        Task<bool> AcceptProjectInvitationTask(Guid invitationToken);
        Task<string> CreateProjectInvitationTask(Guid projectId);
    }
}
