using UnityEngine;
using NaughtyAttributes;

namespace Core.Stats {

    [System.Serializable]
    public class Resource
    {

        public System.Action<int, int> onValueUpdated = delegate {};
        public System.Action<int, int> onMaxUpdated = delegate { };
        public System.Action<int, int> onValueMax = delegate {};
        public System.Action<int, int> onValueMin = delegate {};

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

            int _lastValue = _currentValue;
            _currentValue = Mathf.Clamp(value, 0, _maxValue);
            onValueUpdated(_lastValue, _currentValue);

            if(value == _maxValue) {

                onValueMax(_lastValue, _currentValue);
                atMax = true;

            } else if (value == 0) {

                onValueMin(_lastValue, _currentValue);
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
            
            onMaxUpdated.Invoke(_maxValue, maximum);
            _maxValue = maximum;
        }

        public int GetMax() {
            return _maxValue;
        }

    }

}

