using UnityEngine;
using NaughtyAttributes;
using Core.Characters;

namespace Core.Actions
{

    public abstract class ActionPhase : ScriptableObject
    {

        public event System.Action onPhaseEnd;
        public event System.Action onPhaseStart;

        public abstract void UpdateLogic(Character actor, Character[] targets);

        public virtual void Start() {
            onPhaseStart();
        }

        public virtual void End() {
            onPhaseEnd();
        }

    }
    
}

