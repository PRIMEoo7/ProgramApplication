using Newtonsoft.Json;

namespace ProgramApplicationApi.Models
{
    public class ApplicationModel
    {
        [JsonProperty("id")]
        public Guid ApplicationId { get; set; }

        [JsonProperty("programId")]
        public Guid ProgramId { get; set; }
        public PersonalInfo? PersonalInfo { get; set; }
        public List<CustomQuestions>? CustomQuestions { get; set; }
        public ApplicationModel()
        {
            PersonalInfo = new PersonalInfo();
        }
    }

    
    

}
