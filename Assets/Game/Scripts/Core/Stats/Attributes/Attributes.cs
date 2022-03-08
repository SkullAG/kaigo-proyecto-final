using UnityEngine;
using NaughtyAttributes;

namespace Core.Stats {

    [System.Serializable]
    public class Attributes {

        [Header("Base Attributes")]
        public Attribute prowess; // Overall attack multiplier
        public Attribute robustness; // Overall defense multiplier
        public Attribute vitality; // Maximum health points
        public Attribute perseverance; // Maximum action points
        public Attribute agility; // Chances of evading a hit, movement speed and action speed

        [Header("Derived attributes")]
        public Attribute maximumHP;
        public Attribute maximumAP;
        public Attribute movementSpeed;
        public Attribute actionSpeed;

    }

}

