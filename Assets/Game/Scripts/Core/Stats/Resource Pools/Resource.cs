using UnityEngine;
using NaughtyAttributes;

namespace Core.Stats {

    [System.Serializable]
    public class Resource
    {

        public System.Action<int> onValueUpdated = delegate {};
        public System.Action<int> onValueMax = delegate {};
        public System.Action<int> onValueMin = delegate {};

        [SerializeField]
        protected int _currentValue;

        [SerializeField, ReadOnly, AllowNesting]
        protected int _maxValue;

        public bool depleted = false;
        public bool atMax = false;

        public int value {
            get => GetValue();
            set => SetValue(value);
        }

        public int max {
            get => GetMax();
            set => SetMax(value);
        }

        public void SetValue(int value) {

            _currentValue = Mathf.Clamp(value, 0, _maxValue);
            onValueUpdated(_currentValue);

            if(value == _maxValue) {

                onValueMax(value);
                atMax = true;

            } else if (value == 0) {

                onValueMin(value);
                depleted = false;

            } else {

                atMax = false;
                depleted = false;

            }

        }

        public int GetValue() {
            return _currentValue;
        }

        public void SetMax(int maximum) {
            _maxValue = maximum;
        }

        public int GetMax() {
            return _maxValue;
        }

    }

}

