using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

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

        public bool isRunning => running;

        private void OnEnable() {

            Initialize();
            running = false;
            _runtimePhases = _phases;

        }

        /// <summary> Sets initial values. </summary>
        public abstract void Initialize();

        /// <summary> Executes the action. </summary>
        public abstract void Execute();

        /// <summary> Update action processing. </summary>
        public void Update() {

            if (running) {

                // Update current phase
                _runtimePhases[currentPhaseIndex].Update();

            }

        }

        /// <summary> Starts action execution. </summary>
        protected void StartSequence() {

            Debug.Log( $"Action {name} starts." );

            // Instantiate phases
            for(int i = 0; i < _phases.Count; i++) {
                _runtimePhases[i] = Instantiate(_phases[i]);
            }

            // Start listening first phase
            _runtimePhases[0].onPhaseStart += OnCurrentPhaseStart;
            _runtimePhases[0].onPhaseEnd += OnCurrentPhaseEnd;

            // Start first phase
            running = true;
            _runtimePhases[0].Start();

        }

        /// <summary> Called when current phase starts. </summary>
        protected virtual void OnCurrentPhaseStart() {

            //

        }

        /// <summary> Called when current phase ends. </summary>
        protected virtual void OnCurrentPhaseEnd() {

            // If the current phase is the last, end sequence.
            if(currentPhaseIndex == _phases.Count - 1) {
                EndSequence();
                return;
            }

            // Jump to the next phase.
            Next();

        }

        private void Next() {

            // Stop listening last phase
            _phases[previousPhaseIndex].onPhaseStart -= OnCurrentPhaseStart;
            _phases[previousPhaseIndex].onPhaseEnd -= OnCurrentPhaseEnd;

            previousPhaseIndex = currentPhaseIndex;
            currentPhaseIndex++;

            Debug.Log( $"Next phase: {_phases[currentPhaseIndex].name}" );

            // Start listening next phase
            _phases[currentPhaseIndex].onPhaseStart += OnCurrentPhaseStart;
            _phases[currentPhaseIndex].onPhaseEnd += OnCurrentPhaseEnd;

            // Start next phase
            _phases[currentPhaseIndex].Start();

        }

        /// <summary> Ends the phase sequence. </summary>
        private void EndSequence() {

            Debug.Log( $"Action {name} ends." );

            // Stop listening last phase
            _phases[previousPhaseIndex].onPhaseStart -= OnCurrentPhaseStart;
            _phases[previousPhaseIndex].onPhaseEnd -= OnCurrentPhaseEnd;

            running = false;

        }

    }
    
}

