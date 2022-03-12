using UnityEngine;
using Core.Actions;
using Core.Characters;

[CreateAssetMenu(fileName = "DelayPhase", menuName = "Game/Actions/Phases/Delay")]
public class Delay : ActionPhase
{
    
    private float _timer = 0;
    private int _counter = 0;

    public int timeToExit = 5;

    // Update phase's processing
    public override void UpdateLogic(Character actor, Character[] targets) {

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
