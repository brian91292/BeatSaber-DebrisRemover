using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using UnityEngine;
using System.Diagnostics;
using Ryder.Lightweight;

namespace DebrisRemover
{
    public class GameHooks
    {
        private static Redirection _noteDebrisSpawner_SpawnDebris;

        public static void NoteDebrisSpawner_SpawnDebris(NoteDebrisSpawner t, NoteCutInfo noteCutInfo, NoteController noteController)
        {
            if (!Plugin.Instance.Enabled) _noteDebrisSpawner_SpawnDebris.InvokeOriginal(t, new object[] { noteCutInfo, noteController });
        }

        public static void Apply()
        {
            _noteDebrisSpawner_SpawnDebris = new Redirection(typeof(NoteDebrisSpawner).GetMethod("SpawnDebris"), typeof(GameHooks).GetMethod("NoteDebrisSpawner_SpawnDebris"), true);
        }
    }
}
