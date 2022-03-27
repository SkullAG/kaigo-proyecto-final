using UnityEngine;
using Core.Actions;
using Core.Characters;

public class Delay : ActionPhase
{
    
    private float _timer = 0;
    private int _counter = 0;

    private int _time = 5;

    public Delay(int time) {

        this._time = time;

    }

    public override void Start() {

        Debug.Log( "Casting delay..." );

        base.Start();

    }

    // Update phase's processing
    public override void Update(Character actor, Character[] targets) {

        _timer += Time.deltaTime;

        if(_timer >= 1) {

            _timer = 0;
            _counter++;

            if(_counter >= _time) {

                End(); // Call End method when you want the phase to end and proceed to the next one

            }

        }

    }
        

}
