using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Stats {

    [System.Serializable]
    public class Modifier
    {
        
        public enum Type { percentage, flat }

        [SerializeField]
        private Type _type = Type.flat;

        public Type type => _type;

        public float factor = 0;

        public Modifier(Type type, float factor) {

            _type = type;
            this.factor = factor;

        }

        public float CalculateValue(float baseValue, float lastValue) {
            
            if(_type == Type.flat) {

                return lastValue + factor;

            } else if (_type == Type.percentage) {

                return baseValue + (baseValue * factor / 100);

            }

            return lastValue;

        }

    }

}

