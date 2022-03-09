using UnityEngine;
using Core.Actions;

namespace Core.Actions.Example
{
    
    [CreateAssetMenu(fileName = "Casting", menuName = "Game/Actions/Example/Casting")]
    public class ExampleFirePhase : ActionPhase {

        // Reset parameters
        public override void Initialize() {

            //

        }
        
        // Update phase's processing
        public override void Update() {

            Debug.Log( "Fire!" );

            End();

        }

    }

}

