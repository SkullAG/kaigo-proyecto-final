using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Stats {

    [System.Serializable]
    public class Vitality : Attribute
    {

        public const int BASE_HP = 75;
        public const int INCREMENT = 25;
        
        public float CalculateMaximumHealth() {

            float _result = BASE_HP + ( value - 1 ) * INCREMENT ;

            return _result;

        }

    }

}

