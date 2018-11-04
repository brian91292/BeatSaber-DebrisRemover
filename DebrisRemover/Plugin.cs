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

        public void OnApplicationStart()
        {
            _gameHooks = new GameObject().AddComponent<GameHooks>();
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
