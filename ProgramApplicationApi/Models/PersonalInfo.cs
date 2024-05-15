using Newtonsoft.Json;

namespace ProgramApplicationApi.Models
{
    public class PersonalInfo
    {
        [JsonProperty("id")]
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public int? Phone { get; set; }
        public string? Nationality { get; set; }
        public string? CurrentResidence { get; set; }
        public string? DateOfBirth { get; set; }
        public string? Gender { get; set; }

    }
}
