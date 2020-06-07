using Newtonsoft.Json;

namespace Api.Entities
{
    public class Customer : EntityBase
    {
        [JsonProperty("first_name")]
        public string Firstname { get; set; }

        [JsonProperty("last_name")]
        public string Lastname { get; set; }
        public string Email { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
    }
}