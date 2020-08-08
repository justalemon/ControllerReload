using GTA;
using GTA.UI;
using System;
using System.Windows.Forms;


namespace ControllerReload
{
    public class ControllerReload : Script
    {
        /// <summary>
        /// The script configuration.
        /// </summary>
        public static ScriptSettings Config = ScriptSettings.Load("scripts\\ControllerReload.ini");
        /// <summary>
        /// The first control to press.
        /// </summary>
        public static GTA.Control PressOne = Config.GetValue("ControllerReload", "PressOne", GTA.Control.FrontendRb);
        /// <summary>
        /// The second control to press.
        /// </summary>
        public static GTA.Control PressTwo = Config.GetValue("ControllerReload", "PressTwo", GTA.Control.Reload);

        public static string ScriptHookReloadButton = Config.GetValue("ControllerReload", "ScriptHookReloadButton", "{F10}");
        
    
        public ControllerReload()
        {
            Tick += OnTick;
        }

        public void OnTick(object Sender, EventArgs Args)
        {
            if (Game.IsControlPressed(PressOne) == true && Game.IsControlPressed(PressTwo) == true)
            {                              
                SendKeys.SendWait(ScriptHookReloadButton);
                GTA.UI.Notification.Show("Scripts Reloaded!");
            }
        }
    }
}
