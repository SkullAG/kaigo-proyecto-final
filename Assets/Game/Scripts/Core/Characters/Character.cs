using UnityEngine;
using Core.Stats;
using Core.Gambits;
using Core.Actions;
using Core.States;
using NaughtyAttributes;
using System.Collections.Generic;

namespace Core.Characters {

    public class Character : MonoBehaviour
    {
        
        [ReadOnly] public bool isBeingTargetted = false;
        [ReadOnly] public Character targettedBy;

        public bool isAlive => stats.healthPoints.value > 0;

        public StatList stats;
        public ActionList actions;
        public GambitList gambits;
        public StateList states;
        public ActionQueue queue;
        public NavBodySistem navBody;

        public bool isAlly => gameObject.CompareTag(BattleController.current.allyTag);
        public bool isEnemy => gameObject.CompareTag(BattleController.current.enemyTag);
        
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

