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

        public string id;

        [SerializeReference, NonReorderable, Expandable]
        public List<ActionPhase> phases;

        //private List<ActionPhase> _runtimePhases;

        [SerializeField, ReadOnly]
        protected bool running = false;

        protected ActionPhase currentPhase;
        protected ActionPhase lastPhase;

        public Character actor;
        [NonReorderable] public Character[] targets;

        protected int phaseIndex = 0;

        private void OnEnable() {

            running = false;
            //_runtimePhases = _phases;

        }

        // Abstract methods
        protected abstract void OnExecution();
        protected abstract void OnPhaseStart();
        protected abstract void OnPhaseEnd();
        protected abstract void OnUpdate();

        public void Update() {

            if (running) {

                // Update current phase's logic
                currentPhase.UpdateLogic(actor, targets);

                OnUpdate();

            }

        }
        
        public void Execute() {

            OnExecution();

        }

        protected void NextPhase() {

            lastPhase = phases[phaseIndex];
            phaseIndex++;
            currentPhase = phases[phaseIndex];

            StartPhase(currentPhase);

        }

        protected void StartPhase(ActionPhase phase) {

            if(lastPhase != null) {

                // Stop listening last phase
                lastPhase.onPhaseStart -= OnPhaseStart;
                lastPhase.onPhaseEnd -= OnPhaseEnd;

            }

            phase.onPhaseStart += OnPhaseStart;
            phase.onPhaseEnd += OnPhaseEnd;

            phase.Start();

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

        }

        public bool OnLastPhase() {

            return phaseIndex == phases.Count - 1;
            
        }

    }
    
}

