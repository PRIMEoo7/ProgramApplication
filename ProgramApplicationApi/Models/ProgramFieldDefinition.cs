using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace ProgramApplicationApi.Models
{
    public class ProgramFieldDefinition
    {
        [JsonProperty("id")]
        public Guid ProgramId { get; set; }
        public string ProgramTitle { get; set; }
        public string ProgramDescription { get; set; }
        public PersonalInfo? PersonalInfo { get; set; }
        public List<CustomQuestions>? CustomQuestions { get; set; }
        public ProgramFieldDefinition()
        {
            PersonalInfo = new PersonalInfo();
        }
    }

    
}


