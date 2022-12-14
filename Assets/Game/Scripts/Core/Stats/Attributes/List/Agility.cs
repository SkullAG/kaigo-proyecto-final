using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Stats {

    [System.Serializable]
    public class Agility : Attribute
    {

        public const float MOVEMENT_FACTOR = 1;
        public const float ACTION_FACTOR = 1;
        public const float EVASION_FACTOR = 1;

        public const int MIN_SPEED = 3;
        public const int MAX_SPEED = 8;

        public const int MAX_ACTIONSPEED = 10;
        public const int MIN_ACTIONSPEED = 2;

        public const float ATTRIBUTE_MAX = 99;

        public float CalculateMovementSpeed() {

            float _result = value * (MAX_SPEED - MIN_SPEED) / 10 + MIN_SPEED; 

            return _result;

        }

        public float CalculateActionSpeed() {

            float _result = value / MAX_ACTIONSPEED; 

            return _result;

        }

        public float CalculateEvasion(float attackerAgility, float defenderAgility) {

            float _result = ((defenderAgility / attackerAgility ) / ATTRIBUTE_MAX) * 100 * EVASION_FACTOR;

            return _result;

        }   

    }

}

