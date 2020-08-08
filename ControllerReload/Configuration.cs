using GTA;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace ControllerReload
{
    /// <summary>
    /// The configuration of ControllerReload.
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// The controls that need to be pressed to trigger a reload.
        /// </summary>
        [JsonProperty("controls")]
        public List<Control> Controls { get; set; } = new List<Control>() { Control.FrontendRb, Control.FrontendCancel };
    }
}
