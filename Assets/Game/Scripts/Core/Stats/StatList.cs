using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

namespace Core.Stats {

    public class StatList : MonoBehaviour
    {

        private struct Value {

            private const float MIN_STAT = 0.0f;
            private const float MAX_STAT = 99.0f;
            private float _rawValue;
            private float _modifiedValue;

            public List<Modifier> modifiers;

            public float modifiedValue {

                get {

                    float _result = rawValue;

                    var _sortedModifiers = SortModifiers(modifiers);

                    foreach (Modifier mod in _sortedModifiers) { 

                        _result = mod.CalculateValue(rawValue);

                    }

                    return _result;

                }

            }

            public float rawValue {

                set => Mathf.Clamp(_rawValue, MIN_STAT, MAX_STAT);
                get => _rawValue; // Apply stat modifiers here

            }

            private List<Modifier> SortModifiers(List<Modifier> modifiers) {

                return modifiers.OrderBy( x => x.GetType() ) as List<Modifier>;

            }
            
        }

        private Dictionary<StatType, Value> _list = new Dictionary<StatType, Value>();

        public void SetValue(StatType stat, float value) {

            _list[stat] = new Value() { rawValue = value };

        }

        public float GetValue(StatType stat) {

            return _list[stat].modifiedValue;

        }

        public void AddModifier(StatType stat, Modifier modifier) {

            _list[stat].modifiers.Add(modifier);

        }

        public void RemoveModifier(StatType stat, Modifier modifier) {

            _list[stat].modifiers.Remove(modifier);

        }

    }

}

