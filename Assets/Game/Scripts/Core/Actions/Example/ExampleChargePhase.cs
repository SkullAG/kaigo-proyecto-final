using UnityEngine;
using Core.Actions;

namespace Core.Actions.Example
{

    [CreateAssetMenu(fileName = "FireBall", menuName = "Game/Actions/Example/FireBall")]
    public class ExampleChargePhase : ActionPhase {

        private float _timer;
        private int _counter;

        public int timeToExit = 5;

        // Reset parameters
        public override void Initialize() {

            _timer = 0;
            _counter = 0;

        }

        // Update phase's processing
        public override void Update() {

            Debug.Log( "Charging..." );

            _timer += Time.deltaTime;

            if(_timer >= 1) {

                _timer = 0;
                _counter++;

                if(_counter >= timeToExit) {
                    End(); // Call End method when you want the phase to end and proceed to the next one
                }

            }

        }
        
    }
    
}

