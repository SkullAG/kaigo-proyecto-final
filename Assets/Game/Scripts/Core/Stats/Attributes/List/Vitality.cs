using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Stats {

    public class Vitality : Attribute
    {
        
        public float CalculateMaximumHealth() {

            float _result = value * 10;

            return _result;

        }

    }

}

