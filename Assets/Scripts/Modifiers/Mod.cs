using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Ren.Weapon.Mods
{
    public abstract class Mod : ScriptableObject
    {
        public event Action OnModLoaded;

        public virtual void LoadMod(Weapon weapon)
        {
            OnModLoaded?.Invoke();
        }


    }
}