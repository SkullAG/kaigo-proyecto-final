using UnityEngine;
using NaughtyAttributes;
using Core.Characters;

namespace Core.Actions
{

    [System.Serializable]
    public abstract class ActionPhase// : ScriptableObject
    {

        public event System.Action onPhaseEnd = delegate{ };
        public event System.Action onPhaseStart = delegate { };

        public bool started = false;

        protected Character actor;
        protected Character target;

        public abstract void Update();

        public virtual void Start(Character actor, Character target) {

            this.actor = actor;
            this.target = target;

            onPhaseStart();
            
            started = true;

        }

        public virtual void End() {
            onPhaseEnd();
        }

    }
    
}

