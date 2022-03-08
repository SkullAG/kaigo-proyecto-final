using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Stats {

    public class Agility : Attribute
    {

        public const float MOVEMENT_FACTOR = 1;
        public const float ACTION_FACTOR = 1;
        public const float EVASION_FACTOR = 1;

        public const float ATTRIBUTE_MAX = 99;

        public float CalculateMovementSpeed() {

            float _result = value * MOVEMENT_FACTOR;

            return _result;

        }

        public float CalculateActionSpeed() {

            float _result = value * ACTION_FACTOR;

            return _result;

        }

        public float CalculateEvasion(float attackerAgility, float defenderAgility) {

            float _result = ((defenderAgility / attackerAgility ) / ATTRIBUTE_MAX) * 100 * EVASION_FACTOR;

            return _result;

        }   

    }

}

