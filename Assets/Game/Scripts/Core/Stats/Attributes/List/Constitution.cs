using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Stats {

    [System.Serializable]
    public class Constitution : Attribute
    {
        public float normalized { get { return value / Attribute._MAX; } }

        public const float MaxReduction = 0.5f;

        public float DamageMultiplier { get { return 1 - (normalized * MaxReduction); } }
    }

}

