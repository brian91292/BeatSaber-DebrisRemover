using IllusionPlugin;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DebrisRemover
{
    public class Plugin : IPlugin
    {
        public string Name => "DebrisRemover";
        public string Version => "1.0.0";

        private GameHooks _gameHooks;
        public static bool Enabled = true;

        public void OnApplicationStart()
        {
            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;

            _gameHooks = new GameObject().AddComponent<GameHooks>();
        }

        private void ToggleDebrisRemover(bool enabled)
        {
            if (enabled) _gameHooks.ApplyHooks();
            else _gameHooks.RemoveHooks();
        }

        private void SceneManagerOnActiveSceneChanged(Scene arg0, Scene arg1)
        {
            if (arg1.name == "Menu")
            {
                var tmpEnabled = ModPrefs.GetBool(Name, "Enabled", Enabled);
                if (tmpEnabled != Enabled)
                {
                    ToggleDebrisRemover(tmpEnabled);
                    Enabled = tmpEnabled;
                }

                var toggle = GameOptionsUI.CreateToggleOption("Debris Remover");
                toggle.GetValue = Enabled;
                toggle.OnToggle += (b) =>
                {
                    Enabled = b;
                    Plugin.Log($"{(b ? "Enabled" : "Disabled")}");
                    ModPrefs.SetBool(Name, "Enabled", Enabled);

                    ToggleDebrisRemover(b);
                };
            }
        }

        private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
        {
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
