using WebApi.Data.Enum;

namespace WebApi.Data
{
    public class Workout
    {
        public string Id { get; set; }
        public Dictionary<EnumTrainType, List<string>> Train { get; set; }
    }
}
