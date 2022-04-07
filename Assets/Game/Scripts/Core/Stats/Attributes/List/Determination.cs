using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Stats {

    [System.Serializable]
    public class Determination : Attribute
    {

        public const int BASE_AP = 5;
        public const int INCREMENT = 5;

        public float CalculateMaxActionPoints() {

            float _result = BASE_AP + ( value - 1 ) * INCREMENT;

            return _result;

        }
        
    }

}

