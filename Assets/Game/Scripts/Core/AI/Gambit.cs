using UnityEngine;
using Core.Actions;
using Core.AI;

using NaughtyAttributes;

namespace Core.Gambits
{
    
    [System.Serializable]
    public class Gambit
    {
        
        [SerializeField, Expandable]
        private TargetFilter _target;

        [SerializeField, Expandable]
        private BehaviourCondition _condition;

        [SerializeField]
        private ActionReference _actionReference;

        public BehaviourCondition condition => _condition;
        public TargetFilter target => _target;
        public ActionReference actionReference => _actionReference;

    }
    
}

