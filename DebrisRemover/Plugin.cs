using IllusionPlugin;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DebrisRemover
{
    public class Plugin : IPlugin
    {
        public string Name => "DebrisRemover";
        public string Version => "1.0.1";

        private GameHooks _gameHooks;
        public bool Enabled = true;

        public static Plugin Instance;

        public void OnApplicationStart()
        {
            Instance = this;

            SceneManager.activeSceneChanged += SceneManagerOnActiveSceneChanged;
            SceneManager.sceneLoaded += SceneManager_sceneLoaded;

            Enabled = ModPrefs.GetBool(Name, "Enabled", true);
            ModPrefs.SetBool(Name, "Enabled", Enabled);

            _gameHooks = new GameObject().AddComponent<GameHooks>();
            ToggleDebrisRemover(Enabled);
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
                var toggle = GameOptionsUI.CreateToggleOption("Debris Remover");
                toggle.GetValue = Enabled;
                toggle.OnToggle += (b) =>
                {
                    Enabled = b;
                    ToggleDebrisRemover(Enabled);
                    ModPrefs.SetBool(Name, "Enabled", Instance.Enabled);
                    Plugin.Log($"{(b ? "Enabled" : "Disabled")}");
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
