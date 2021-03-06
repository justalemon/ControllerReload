using GTA;
using GTA.UI;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.IO;
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
        /// The configuration of Controller Reload.
        /// </summary>
        public static Configuration config = null;

        #endregion

        #region Constructors

        public ControllerReload()
        {
            // Create the serializer settings and file path
            JsonSerializerSettings settings = new JsonSerializerSettings();
            settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
            settings.Converters.Add(new StringEnumConverter());
            string path = GetRelativeFilePath("ControllerReload.json");
            // If there is a configuration file, load it
            if (File.Exists(path))
            {
                string contents = File.ReadAllText(path);
                config = JsonConvert.DeserializeObject<Configuration>(contents, settings);
            }
            // If there is none, create a new configuration and save it
            else
            {
                config = new Configuration();
                string contents = JsonConvert.SerializeObject(config, Formatting.Indented, settings);
                File.WriteAllText(path, contents);
            }

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
            // If the player is not using a controller, return
            if (Game.LastInputMethod != InputMethod.GamePad)
            {
                return;
            }

            // Iterate over the controls that need to be pressed
            foreach (Control control in config.Controls)
            {
                // If is not pressed, return
                if (!Game.IsControlPressed(control))
                {
                    return;
                }
            }

            // If all of the controls were pressed and we got here, trigger a reload
            reloadMethod.Invoke(null, new object[0]);
        }

        #endregion
    }
}
