using ProgramApplicationApi.Models;

namespace ProgramApplicationApi.Interfaces
{
    public interface IProgramRepository
    {
        Task CreateProgram(ProgramFieldDefinition programInfo);
        Task UpdateProgram(string programId, string programTitle, ProgramFieldDefinition updatedApplication);
    }
}