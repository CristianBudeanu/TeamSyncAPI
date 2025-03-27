using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TeamSync.Application.Services.ProjectServices.InvitationServices
{
    public interface IInvitationService
    {
        Task<bool> AcceptProjectInvitationTask(Guid invitationToken);
        Task<string> CreateProjectInvitationTask(Guid projectId);
    }
}
