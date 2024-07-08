using System.Text.Json.Serialization;

namespace TreeTask.DTOs
{
    public class AssetsTreeCreateDTO
    {
        public string Name { get; set; } = string.Empty;

        [JsonIgnore]
        public string Number { get; set; } = string.Empty;

        public bool IsLast { get; set; }

        public double Money { get; set; }

        public int? ParentId { get; set; }
    }
}
