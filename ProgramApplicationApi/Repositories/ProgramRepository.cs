using Microsoft.Azure.Cosmos;
//using ProgramApplicationApi.Common;
using ProgramApplicationApi.Interfaces;
using ProgramApplicationApi.Models;
using Container = Microsoft.Azure.Cosmos.Container;

namespace ProgramApplicationApi.Repositories
{
    public class ProgramRepository : IProgramRepository
    {
        private readonly Container _container;
        private readonly IConfiguration _configuration;
        private readonly CosmosClient _cosmosClient;

        public ProgramRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            _cosmosClient = cosmosClient;
            _configuration = configuration;
            var databaseName = configuration["CosmosDb:DatabaseName"];
            var containeName = "ProgramContainer";
            _container = cosmosClient.GetContainer(databaseName, containeName);
        }
        public async Task CreateProgram(ProgramFieldDefinition programInfo)
        {
            await _container.CreateItemAsync(programInfo, new PartitionKey(programInfo.ProgramTitle));

        }
        public async Task UpdateProgram(string programId, string programTitle, ProgramFieldDefinition updatedProgram)
        {
            try
            {


                ItemResponse<ProgramFieldDefinition> response = await _container.ReadItemAsync<ProgramFieldDefinition>(
                    partitionKey: new PartitionKey(programTitle),
                    id: programId);

                if (response.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    var existingDocument = response.Resource;

                    existingDocument.ProgramTitle = updatedProgram.ProgramTitle;
                    existingDocument.ProgramDescription = updatedProgram.ProgramDescription;
                    existingDocument.PersonalInfo.FirstName = updatedProgram.PersonalInfo.FirstName;
                    existingDocument.PersonalInfo.LastName = updatedProgram.PersonalInfo.LastName;
                    existingDocument.PersonalInfo.Email = updatedProgram.PersonalInfo.Email;
                    existingDocument.PersonalInfo.Phone = updatedProgram.PersonalInfo.Phone;
                    existingDocument.PersonalInfo.Nationality = updatedProgram.PersonalInfo.Nationality;
                    existingDocument.PersonalInfo.CurrentResidence = updatedProgram.PersonalInfo.CurrentResidence;
                    existingDocument.PersonalInfo.DateOfBirth = updatedProgram.PersonalInfo.DateOfBirth;
                    existingDocument.PersonalInfo.Gender = updatedProgram.PersonalInfo.Gender;
                    existingDocument.CustomQuestions = updatedProgram.CustomQuestions;


                    await _container.ReplaceItemAsync(existingDocument, partitionKey: new PartitionKey(programTitle), id: programId);
                }
                else
                {
                    throw new Exception("Document not found.");
                }
            }
            catch(Exception ex) 
            {
                throw ex;
            }
        }
    }
}

