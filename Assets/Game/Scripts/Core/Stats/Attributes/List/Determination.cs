using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Stats {

    public class Determination : Attribute
    {

        public float CalculateMaxActionPoints() {

            float _result = value * 10;

            return _result;

        }
        
    }

}
