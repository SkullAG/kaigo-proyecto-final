using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

namespace Core.Stats {

    [System.Serializable]
    public class Attribute
    {

        public System.Action<float> onValueChanged = delegate {};

        [SerializeField]
        protected float _rawValue;

        [SerializeField, ReadOnly, AllowNesting]
        protected float _modifiedValue;

        [SerializeField, NonReorderable]
        public List<Modifier> modifiers = new List<Modifier>();

        public const float _MAX = 10;

        public float value {
            get => GetValue();
            set => SetRawValue(value);
        }

        public virtual float GetValue() {

            float _result = ApplyModifiers(_rawValue);
            _modifiedValue = _result;

            return _modifiedValue;

        }

        public virtual float GetRawValue() {
            return _rawValue;
        }

        public virtual void SetRawValue(float value) {

            if(value < 0) _rawValue = 0;
            else _rawValue = value;

            onValueChanged(_rawValue);

        }

        private float ApplyModifiers(float value) {

            float _result = value;

            if(modifiers.Count > 0) {

                var _sortedModifiers = SortModifiers(modifiers);

                foreach (Modifier mod in _sortedModifiers) {
                    _result = mod.CalculateValue( _rawValue, _result );
                }

            }

            return _result;

        }

        // Sort by type
        private IOrderedEnumerable<Modifier> SortModifiers(List<Modifier> modifiers) {

            return modifiers.OrderBy(x => (int)x.type);
            
        }

    }

}

