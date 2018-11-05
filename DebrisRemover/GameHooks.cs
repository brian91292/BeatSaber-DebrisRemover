using PlayHooky;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using System.Diagnostics;

namespace DebrisRemover
{

    public class NoteDebrisSpawnerHooks
    {
        public static void SpawnDebris(NoteDebrisSpawner t, NoteCutInfo noteCutInfo, NoteController noteController)
        {
        }
    }

    public class GameHooks : MonoBehaviour
    {
        private HookManager hookManager;
        private Dictionary<string, MethodInfo> hooks;
        public bool hooked = false;

        private void Awake()
        {
            UnityEngine.Object.DontDestroyOnLoad((UnityEngine.Object)((UnityEngine.Component)this).gameObject);
            this.hookManager = new HookManager();
            this.hooks = new Dictionary<string, MethodInfo>();
        }

        private void OnDestroy()
        {
            RemoveHooks();
        }

        public void ApplyHooks()
        {
            if (!hooked)
            {
                this.Hook("DebrisSpawner", typeof(NoteDebrisSpawner).GetMethod("SpawnDebris"), typeof(NoteDebrisSpawnerHooks).GetMethod("SpawnDebris"));
                hooked = true;
            }
        }

        public void RemoveHooks()
        {
            if (!hooked)
            {
                foreach (string key in this.hooks.Keys)
                    this.UnHook(key);

                hooked = false;
            }
        }

        private bool Hook(string key, MethodInfo target, MethodInfo hook)
        {
            if (this.hooks.ContainsKey(key))
                return false;
            try
            {
                this.hooks.Add(key, target);
                this.hookManager.Hook(target, hook);
                Plugin.Log($"{key} hooked!");
                return true;
            }
            catch (Win32Exception ex)
            {
                Plugin.Log($"Unrecoverable Windows API error: {(object)ex}");
                return false;
            }
            catch (Exception ex)
            {
                Plugin.Log($"Unable to hook method, : {(object)ex}");
                return false;
            }
        }

        private bool UnHook(string key)
        {
            MethodInfo original;
            if (!this.hooks.TryGetValue(key, out original))
                return false;
            this.hookManager.Unhook(original);
            return true;
        }
    }
}
