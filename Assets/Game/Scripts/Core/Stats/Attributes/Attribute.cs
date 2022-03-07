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

        public readonly List<Modifier> modifiers;

        public float GetModifiedValue() {

            float _result = GetValue();

            var _sortedModifiers = SortModifiers(modifiers);

            foreach (Modifier mod in _sortedModifiers) {
                _result = mod.CalculateValue( GetValue() );
            }

            return _result;

        }

        public float GetValue() {
            return _rawValue;
        }

        public void SetValue(float value) {

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

