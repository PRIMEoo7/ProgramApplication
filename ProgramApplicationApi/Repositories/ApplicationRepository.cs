using Microsoft.Azure.Cosmos;
using ProgramApplicationApi.Interfaces;
using ProgramApplicationApi.Models;

namespace ProgramApplicationApi.Repositories
{
    public class ApplicationRepository : IApplicationRepository
    {
        private readonly Container _container;
        private readonly IConfiguration _configuration;
        private readonly CosmosClient _cosmosClient;

        public ApplicationRepository(CosmosClient cosmosClient, IConfiguration configuration)
        {
            _cosmosClient = cosmosClient;
            _configuration = configuration;
            var databaseName = configuration["CosmosDb:DatabaseName"];
            var containeName = "ProgramContainer";
            _container = cosmosClient.GetContainer(databaseName, containeName);
        }
        public async Task<ProgramFieldDefinition> GetProgram(Guid programId, string programTitle)
        {
            try
            {

                QueryDefinition queryDefinition = new QueryDefinition("SELECT * FROM c WHERE c.id = @programId")
                    .WithParameter("@programId", programId);

                FeedIterator<ProgramFieldDefinition> queryResultSetIterator = _container.GetItemQueryIterator<ProgramFieldDefinition>(
                    queryDefinition,
                    requestOptions: new QueryRequestOptions { PartitionKey = new PartitionKey(programTitle) });

                while (queryResultSetIterator.HasMoreResults)
                {
                    FeedResponse<ProgramFieldDefinition> currentResultSet = await queryResultSetIterator.ReadNextAsync();
                    foreach (var programInfo in currentResultSet)
                    {
                        return programInfo;
                    }
                }

                return null;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return null;
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while fetching program: {ex.Message}");
            }
        }

        public async Task SubmitApplication(ApplicationModel applicationInfo)
        {
            try
            {
                await _container.CreateItemAsync(applicationInfo, new PartitionKey(applicationInfo.PersonalInfo.FirstName));
            }
           
            catch (Exception ex)
            {
                throw new Exception($"An error occurred while submitting Application: {ex.Message}");
            }
        }
    }
}
