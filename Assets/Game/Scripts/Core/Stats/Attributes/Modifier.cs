using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Stats {

    public class Modifier
    {
        
        public enum Type { flat, percentage }

        [SerializeField]
        private Type _type = Type.flat;

        public float factor = 0;

        public float CalculateValue(float value) {

            if(_type == Type.flat) {

                return value + factor;

            } else if (_type == Type.percentage) {

                return value * factor / 100;

            }

            return value;

        }

    }

}

