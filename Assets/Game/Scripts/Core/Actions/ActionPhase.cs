using UnityEngine;
using NaughtyAttributes;

namespace Core.Actions
{

    public abstract class ActionPhase : ScriptableObject
    {

        public event System.Action onPhaseEnd;
        public event System.Action onPhaseStart;

        private void OnEnable() {
            Initialize();
        }

        /// <summary> Resets the action phase parameters. </summary>
        public abstract void Initialize();

        /// <summary> Update processing of this phase. </summary>
        public abstract void Update();

        /// <summary> Start processing of this phase. </summary>
        public virtual void Start() {

            Debug.Log( $"Phase {name} has started." );
            onPhaseStart();

        }

        /// <summary> Ends the phase firing up the corresponding event. </summary>
        protected virtual void End() {

            Debug.Log( $"Phase {name} has ended." );
            onPhaseEnd();

        }


    }
    
}

