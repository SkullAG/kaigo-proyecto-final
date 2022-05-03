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
        private ActionReference _actionReference;

        public BehaviourCondition condition => _condition;
        public TargetFilter target => _target;
        public ActionReference actionReference => _actionReference;

    }
    
}

