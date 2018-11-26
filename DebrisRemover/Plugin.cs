using IllusionPlugin;
using System;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using CustomUI.GameplaySettings;

namespace DebrisRemover
{
    public class Plugin : IPlugin
    {
        public string Name => "DebrisRemover";
        public string Version => "1.0.2";

        public bool Enabled = true;

        public static Plugin Instance;

        public void OnApplicationStart()
        {
            Instance = this;

            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;

            Enabled = ModPrefs.GetBool(Name, "Enabled", true);
            ModPrefs.SetBool(Name, "Enabled", Enabled);

            GameHooks.Apply();
        }
        
        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene arg1)
        {

        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
            Plugin.Log($"Scene: {arg0.name}");
            if (arg0.name == "Menu")
            {
                var toggle = GameplaySettingsUI.CreateToggleOption("Debris Remover", "Stops note debris from spawning entirely when you slice a note.");
                toggle.GetValue = Enabled;
                toggle.OnToggle += (b) =>
                {
                    Instance.Enabled = b;
                    ModPrefs.SetBool(Name, "Enabled", Instance.Enabled);
                    Plugin.Log($"{(b ? "Enabled" : "Disabled")}");
                };

                Plugin.Log("Created gameplay settings toggle!");
            }
        }

        float testval = 0;
        private void Toggle2_OnChange(float obj)
        {
            testval = obj;
        }

        float getvalue()
        {
            return testval;
        }

        public void OnApplicationQuit()
        {
        }

        public void OnLevelWasLoaded(int level)
        {

        }

        public void OnLevelWasInitialized(int level)
        {
        }

        public void OnUpdate()
        {
        }

        public void OnFixedUpdate()
        {
        }

        public static void Log(string msg)
        {
            Console.WriteLine($"[DebrisRemover] {msg}");
        }
    }
}
