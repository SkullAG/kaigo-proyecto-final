using UnityEngine;
using Core.Actions;
using Core.AI;

namespace Core.Gambits
{
    
    [System.Serializable]
    public class Gambit
    {
        
        [SerializeField]
        private TargetFilter _target;

        [SerializeField]
        private BehaviourCondition _condition;

        [SerializeField]
        private GameAction _action;

        public BehaviourCondition condition => _condition;
        public TargetFilter target => _target;
        public GameAction action => _action;

    }
    
}

