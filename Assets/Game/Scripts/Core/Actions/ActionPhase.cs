using UnityEngine;
using NaughtyAttributes;
using Core.Characters;

namespace Core.Actions
{

    [System.Serializable]
    public abstract class ActionPhase// : ScriptableObject
    {

        public event System.Action onPhaseEnd;
        public event System.Action onPhaseStart;

        public abstract void Update(Character actor, Character[] targets);

        public virtual void Start() {
            onPhaseStart();
        }

        public virtual void End() {
            onPhaseEnd();
        }

    }
    
}

