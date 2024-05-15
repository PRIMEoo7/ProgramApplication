using ProgramApplicationApi.Models;
using ProgramApplicationApi.Repositories;

namespace ProgramApplicationApi.Interfaces
{
    public interface IApplicationRepository
    {
        Task<ProgramFieldDefinition> GetProgram(Guid programId, string programTitle);
        Task SubmitApplication(ApplicationModel applicationInfo);
    }
}
