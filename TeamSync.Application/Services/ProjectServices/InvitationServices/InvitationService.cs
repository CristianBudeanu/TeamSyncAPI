using Microsoft.EntityFrameworkCore;
using TeamSync.Application.Common.GlobalExceptionHandler.CustomExceptions;
using TeamSync.Domain.Entities;
using TeamSync.Helpers.HttpContextHelper;
using TeamSync.Infrastructure.EF.Contexts;

namespace TeamSync.Application.Services.ProjectServices.InvitationServices
{
    public class InvitationService(
        TeamSyncAppContext _context,
        IHttpContextService _httpContextService
        ) : IInvitationService
    {
        public async Task<bool> AcceptProjectInvitationTask(Guid invitationToken)
        {
            var username = _httpContextService.GetUsernameFromToken();

            if (username == "Unknown")
            {
                throw new BadRequestException("User is not authorized.");
            }

            var invitation = await _context.Invitations
                .Where(p => p.Token == invitationToken)
                .FirstOrDefaultAsync();

            if (invitation == null || invitation.ExpiresAt < DateTime.UtcNow)
            {
                throw new BadRequestException("Invitation is not valid or has expired.");
            }

            var user = await _context.Users
                .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null)
            {
                throw new NotFoundException("User not found.");
            }

            var project = await _context.Projects
                .Include(p => p.Members)
                .FirstOrDefaultAsync(p => p.Id == invitation.ProjectId);

            if (project == null)
            {
                throw new NotFoundException("Project not found.");
            }

            // Check if user is already a member
            var isAlreadyInProject = project.Members.Any(m => m.Id == user.Id);

            if (isAlreadyInProject)
            {
                throw new BadRequestException("User is already a member of this project.");
            }

            project.Members.Add(user);
            await _context.SaveChangesAsync();

            return true;
        }

        public async Task<string> CreateProjectInvitationTask(Guid projectId)
        {
            var project = await _context.Projects.Where(id => id.Id == projectId).FirstOrDefaultAsync();
            if (project == null)
            {
                throw new BadRequestException("Project not found.");
            }

            var invitation = await _context.Invitations.Where(p => p.ProjectId == projectId).FirstOrDefaultAsync();

            if (invitation == null)
            {
                invitation = new Invitation(projectId);
                await _context.Invitations.AddAsync(invitation);
                await _context.SaveChangesAsync();
            }
            else if (invitation.ExpiresAt > DateTime.UtcNow)
            {
                return invitation.Token.ToString();
            }
            else
            {
                invitation.Token = Guid.NewGuid();
                invitation.CreatedAt = DateTime.UtcNow;
                invitation.ExpiresAt = DateTime.UtcNow.AddHours(2);
                await _context.Invitations
                    .Where(i => i.Id == invitation.Id)
                    .ExecuteUpdateAsync(setters => setters
                    .SetProperty(i => i.Token, Guid.NewGuid())
                    .SetProperty(i => i.CreatedAt, DateTime.UtcNow)
                    .SetProperty(i => i.ExpiresAt, DateTime.UtcNow.AddHours(2))
    );
                await _context.SaveChangesAsync();
            }
            return invitation.Token.ToString();
        }
    }
}
