using GTA;
using Newtonsoft.Json;

namespace ControllerReload
{
    /// <summary>
    /// The configuration of ControllerReload.
    /// </summary>
    public class Configuration
    {
        [JsonProperty("first")]
        public Control First { get; set; } = Control.FrontendRb;
        [JsonProperty("second")]
        public Control Second { get; set; } = Control.FrontendCancel;
    }
}
