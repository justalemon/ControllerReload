using System;
using System.IO;
using System.Reflection;
using GTA;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ControllerReload;

/// <summary>
/// The class controlling the reloads with a controller.
/// </summary>
public class ControllerReload : Script
{
    #region Fields

    private static MethodInfo reloadMethod;
    private static Configuration config;

    #endregion
    
    #region Constructors

    /// <summary>
    /// Creates a new script.
    /// </summary>
    public ControllerReload()
    {
        JsonSerializerSettings settings = new JsonSerializerSettings();
        settings.ObjectCreationHandling = ObjectCreationHandling.Replace;
        settings.Converters.Add(new StringEnumConverter());
        string path = GetRelativeFilePath("ControllerReload.json");

        if (File.Exists(path))
        {
            string contents = File.ReadAllText(path);
            config = JsonConvert.DeserializeObject<Configuration>(contents, settings);
        }
        else
        {
            config = new Configuration();
            string contents = JsonConvert.SerializeObject(config, Formatting.Indented, settings);
            File.WriteAllText(path, contents);
        }

        AssemblyName found = null;

        foreach (Assembly assembly in AppDomain.CurrentDomain.GetAssemblies())
        {
            AssemblyName name = assembly.GetName();

            if (name.Name == "ScriptHookVDotNet")
            {
                found = name;
                break;
            }
        }

        if (found == null)
        {
            throw new InvalidOperationException("The SHVDN Assembly was not found.");
        }

        Type @class = Type.GetType($"ScriptHookVDotNet, {found}", false);
        if (@class == null)
        {
            throw new InvalidOperationException("The ScriptHookVDotNet Class was not found.");
        }

        MethodInfo method = @class.GetMethod("Reload");
        if (method == null)
        {
            throw new InvalidOperationException("The Reload Function/Method was not found.");
        }

        reloadMethod = method;
        Tick += OnTick;
    }

    #endregion

    #region Event Functions

    private void OnTick(object sender, EventArgs e)
    {
        if (Game.LastInputMethod != InputMethod.GamePad)
        {
            return;
        }

        foreach (Control control in config.Controls)
        {
            if (!Game.IsControlPressed(control))
            {
                return;
            }
        }

        reloadMethod.Invoke(null, new object[0]);
    }

    #endregion
}
