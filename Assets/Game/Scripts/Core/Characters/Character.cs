using UnityEngine;
using Core.Stats;
using Core.Gambits;
using Core.Actions;
using Core.States;
using NaughtyAttributes;

namespace Core.Characters {

    public class Character : MonoBehaviour
    {
        
        [ReadOnly] public bool isBeingTargetted = false;
        [ReadOnly] public Character targettedBy = null;

        public StatList stats;
        public ActionList actions;
        public GambitList gambits;
        public StateList states;
        public ActionQueue queue;
        public NavBodySistem navBody;

        private void Awake() {

            stats = GetComponent<StatList>();
            actions = GetComponent<ActionList>();
            gambits = GetComponent<GambitList>();
            states = GetComponent<StateList>();
            queue = GetComponent<ActionQueue>();
            navBody = GetComponent<NavBodySistem>();

        }

    }

}

