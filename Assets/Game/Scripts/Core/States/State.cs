using System.Collections.Generic;
using UnityEngine;
using Core.Characters;

namespace Core.States {

    [CreateAssetMenu(fileName = "State", menuName = "Game/States/State")]
    public class State : ScriptableObject
    {
        
        [SerializeField]
        private List<Effect> _effects = new List<Effect>();

        public string id;

        public void Affect(Character actor) {

            for(int i = 0; i < _effects.Count; i++) {

                _effects[i].Apply(actor);

            }

        }

    }

}

