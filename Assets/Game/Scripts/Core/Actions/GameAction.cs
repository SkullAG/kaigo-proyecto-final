using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Core.Characters;

namespace Core.Actions
{

    public abstract class GameAction : ScriptableObject
    {

        public System.Action onActionStart = delegate {};
        public System.Action onActionEnd = delegate {};

        public string displayName;
        public int id;

        [TextArea]
        public string description;

        public ActionPhase[] phases;

        [SerializeField, ReadOnly]
        protected bool running = false;

        protected ActionPhase currentPhase;
        protected ActionPhase lastPhase;

        [ReadOnly] public Character actor;
        [ReadOnly] public Character target;

        protected int phaseIndex = 0;

        private void Reset() {

            running = false;
            phaseIndex = 0;
            currentPhase = null;
            lastPhase = null;

        }

        // Abstract methods
        protected abstract ActionPhase[] GetPhases();
        protected abstract void OnExecution();
        protected abstract void OnPhaseStart();
        protected abstract void OnPhaseEnd();
        protected abstract void OnUpdate();

        public void Update() {

            if (running && currentPhase != null) {

                // Update current phase's logic
                currentPhase.Update();

                OnUpdate();

            }

        }
        
        public void Execute() {
            
            phases = GetPhases();
            OnExecution();

        }

        protected void NextPhase() {

            lastPhase = phases[phaseIndex];
            phaseIndex++;
            currentPhase = phases[phaseIndex];

            StartPhase(currentPhase);

        }

        protected void StartPhase(ActionPhase phase) {

            if(phases.Length == 0) {

                Debug.LogWarning("Tried to start an action without phases (" + id + ")");
                return;

            }

            if(lastPhase != null) {

                // Stop listening last phase
                lastPhase.onPhaseStart -= OnPhaseStart;
                lastPhase.onPhaseEnd -= OnPhaseEnd;

            }

            phase.onPhaseStart += OnPhaseStart;
            phase.onPhaseEnd += OnPhaseEnd;

            phase.Start(actor, target);

        }

        protected void StartAction() {

            //Debug.Log( $"Action {name} starts." );
            onActionStart();

            /*// Instantiate phases (replace with pooling)
            for(int i = 0; i < _phases.Count; i++) {
                _phases[i] = Instantiate(_phases[i]);
            }*/

            running = true;
            currentPhase = phases[0];

            StartPhase(currentPhase); // Start first phase

        }

        protected void EndAction() {

            running = false;

            //Debug.Log( $"Action {name} ends." );
            onActionEnd();

            Reset();

        }

        public bool OnLastPhase() {

            return phaseIndex == phases.Length - 1;
            
        }

    }
    
}

