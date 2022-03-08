using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Stats {

    [System.Serializable]
    public class Attribute
    {

        public System.Action<float> onValueChanged = delegate {};

        private const float MIN_STAT = 0.0f;
        private const float MAX_STAT = 99.0f;

        [SerializeField]
        private float _rawValue;

        [SerializeField]
        private float _modifiedValue;

        public readonly List<Modifier> modifiers;

        public float GetValue() {

            float _result = GetRawValue();

            var _sortedModifiers = SortModifiers(modifiers);

            foreach (Modifier mod in _sortedModifiers) {
                _result = mod.CalculateValue( GetRawValue() );
            }

            _modifiedValue = _result;
            return _modifiedValue;

        }

        public float GetRawValue() {
            return _rawValue;
        }

        public void SetRawValue(float value) {

            Mathf.Clamp(_rawValue, MIN_STAT, MAX_STAT);
            onValueChanged(_rawValue);

        }

        private List<Modifier> SortModifiers(List<Modifier> modifiers) {

            return modifiers.OrderBy(x => x.GetType()) as List<Modifier>;
            
        }

        public Attribute() {

            this.modifiers = new List<Modifier>();
            this._rawValue = 0;

        }

    }

}

