using GTA;
using System;
using System.Reflection;

namespace ControllerReload
{
    public class ControllerReload : Script
    {
        #region Private Fields

        /// <summary>
        /// The Method information of the Reload console command.
        /// </summary>
        private static MethodInfo reloadMethod = null;
        /// <summary>
        /// The script configuration.
        /// </summary>
        public static ScriptSettings config = ScriptSettings.Load("scripts\\ControllerReload.ini");
        /// <summary>
        /// The first control to press.
        /// </summary>
        public static Control PressOne = config.GetValue("ControllerReload", "PressOne", Control.FrontendRb);
        /// <summary>
        /// The second control to press.
        /// </summary>
        public static Control PressTwo = config.GetValue("ControllerReload", "PressTwo", Control.Reload);

        #endregion

        #region Constructors

        public ControllerReload()
        {
            // Create a place to store the assembly
            AssemblyName found = null;

            // Get all of the assemblies loaded in SHVDN
            foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
            {
                // Get the name information
                AssemblyName name = assembly.GetName();
                // If is SHVDN, save it and break the iterator
                if (name.Name == "ScriptHookVDotNet")
                {
                    found = name;
                    break;
                }
            }

            // If SHVDN was not found, raise an exception
            if (found == null)
            {
                throw new InvalidOperationException("The SHVDN Assembly was not found.");
            }
            // Otherwise, find the class and raise an exception if it was not found
            Type @class = Type.GetType($"ScriptHookVDotNet, {found}", false);
            if (@class == null)
            {
                throw new InvalidOperationException("The ScriptHookVDotNet Class was not found.");
            }
            // Then, continue with the method (is the Reload console command)
            MethodInfo method = @class.GetMethod("Reload");
            if (method == null)
            {
                throw new InvalidOperationException("The Reload Function/Method was not found.");
            }

            // At this point, we have a valid method
            // Save it on a field for later use
            reloadMethod = method;
            // And finish with the Tick event
            Tick += ControllerReload_Tick;
        }

        #endregion

        #region Local Events

        private void ControllerReload_Tick(object sender, EventArgs e)
        {
            if (Game.IsControlPressed(PressOne) == true && Game.IsControlPressed(PressTwo) == true)
            {
                reloadMethod.Invoke(null, new object[0]);
            }
        }

        #endregion
    }
}
