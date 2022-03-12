using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;
using Core.Characters;

namespace Core.Actions
{

    public abstract class GameAction : ScriptableObject
    {

        [SerializeReference]
        private List<ActionPhase> _phases;
        private List<ActionPhase> _runtimePhases;

        protected int previousPhaseIndex = 0;
        protected int currentPhaseIndex = 0;

        protected bool running = false;

        protected Character actor;
        protected Character[] targets;

        protected ActionPhase currentPhase => _runtimePhases[currentPhaseIndex];
        protected ActionPhase previousPhase => _runtimePhases[previousPhaseIndex];

        protected int lastIndex => _runtimePhases.Count - 1;

        public bool isRunning => running;

        private void OnEnable() {

            running = false;
            _runtimePhases = _phases;

        }

        // Abstract methods
        public abstract void OnExecution();
        protected abstract void OnPhaseStart();
        protected abstract void OnPhaseEnd();
        protected abstract void OnUpdate();

        private void Update() {

            if (running) {

                // Update current phase's logic
                _runtimePhases[currentPhaseIndex].UpdateLogic(actor, targets);

                OnUpdate();

            }

        }
        
        public void Execute(Character actor, Character[] targets) {

            this.actor = actor;
            this.targets = targets;

            OnExecution();
            
        }

        protected void NextPhase() {

            // Stop listening last phase
            _phases[previousPhaseIndex].onPhaseStart -= OnPhaseStart;
            _phases[previousPhaseIndex].onPhaseEnd -= OnPhaseEnd;

            previousPhaseIndex = currentPhaseIndex;
            currentPhaseIndex++;

            Debug.Log( $"Next phase: {_phases[currentPhaseIndex].name}" );

            // Start listening next phase
            _phases[currentPhaseIndex].onPhaseStart += OnPhaseStart;
            _phases[currentPhaseIndex].onPhaseEnd += OnPhaseEnd;

            // Start next phase
            _phases[currentPhaseIndex].Start();

        }

        protected void StartAction() {

            Debug.Log( $"Action {name} starts." );

            // Instantiate phases
            for(int i = 0; i < _phases.Count; i++) {
                _runtimePhases[i] = Instantiate(_phases[i]);
            }

            // Start listening first phase
            _runtimePhases[0].onPhaseStart += OnPhaseStart;
            _runtimePhases[0].onPhaseEnd += OnPhaseEnd;

            // Start first phase
            running = true;
            _runtimePhases[0].Start();

        }

        protected void EndAction() {

            Debug.Log( $"Action {name} ends." );

            // Stop listening last phase
            _phases[previousPhaseIndex].onPhaseStart -= OnPhaseStart;
            _phases[previousPhaseIndex].onPhaseEnd -= OnPhaseEnd;

            running = false;

        }

    }
    
}

