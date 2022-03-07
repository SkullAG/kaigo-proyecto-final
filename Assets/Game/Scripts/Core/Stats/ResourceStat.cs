using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Stats {

    [System.Serializable]
    public abstract class ResourceStat
    {

        public System.Action<int> onValueUpdated = delegate {};
        public System.Action<int> onValueMax = delegate {};
        public System.Action<int> onValueMin = delegate {};

        [SerializeField]
        protected int _currentValue;

        [SerializeField]
        protected int _maxValue;

        public void SetValue(int value) {

            _currentValue = Mathf.Clamp(value, 0, _maxValue);
            onValueUpdated(_currentValue);

            if(value == _maxValue) {
                onValueMax(value);
            } else if (value == 0) {
                onValueMin(value);
            }

        }

        public int GetValue() {
            return _maxValue;
        }

    }

}

