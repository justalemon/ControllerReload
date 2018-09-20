using GTA;
using System;
using System.Linq;
using System.Reflection;

namespace ControllerReload
{
    public class Main : Script
    {
        /// <summary>
        /// The script configuration.
        /// </summary>
        public static ScriptSettings Config = ScriptSettings.Load("scripts\\ControllerReload.ini");
        /// <summary>
        /// The first control to press.
        /// </summary>
        public static Control PressOne = Config.GetValue("ControllerReload", "PressOne", Control.InteractionMenu);
        /// <summary>
        /// The second control to press.
        /// </summary>
        public static Control PressTwo = Config.GetValue("ControllerReload", "PressTwo", Control.Reload);
        /// <summary>
        /// Assembly type for "GTA.ScriptDomain".
        /// </summary>
        public static Type AssemblyType = typeof(Script).GetTypeInfo().Assembly.GetType("GTA.ScriptDomain");
        /// <summary>
        /// Method used to send keypresses.
        /// </summary>
        public static MethodInfo KeypressMethod = KeypressMethod = AssemblyType.GetMethods(BindingFlags.Instance | BindingFlags.Public).FirstOrDefault((MethodInfo x) => x.Name == "DoKeyboardMessage");
        /// <summary>
        /// Parameters used to call the function.
        /// </summary>
        public static object[] Parameters = new object[] { System.Windows.Forms.Keys.Insert, true, false, false, false };

        public Main()
        {
            Tick += OnTick;
        }

        public void OnTick(object Sender, EventArgs Args)
        {
            if (Game.IsControlPressed(2, PressOne) && Game.IsControlPressed(2, PressTwo))
            {
                // Thanks StackOverflow for all of this shit
                KeypressMethod.Invoke(AssemblyType.GetProperty("CurrentDomain").GetValue(null, null), Parameters);
            }
        }
    }
}
