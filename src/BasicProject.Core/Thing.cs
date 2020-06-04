using static Newtonsoft.Json.JsonConvert;

namespace BasicProject.Core
{
    public class Thing
    {
        public int Get(int left, int right) =>
            DeserializeObject<int>($"{left + right}");
    }
}
