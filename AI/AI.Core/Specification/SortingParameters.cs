using System.Text.Json.Serialization;

namespace AI.Core.Specification
{
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public enum SortingParameters
    {
        NameAsc, NameDesc, AgeAsc, AgeDesc
    }
}
