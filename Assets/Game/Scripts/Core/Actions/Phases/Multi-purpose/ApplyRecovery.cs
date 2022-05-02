using UnityEngine;
using Core.Actions;

public class ApplyRecovery : ActionPhase
{

    private int _recoveredValue;

    // Cualquier parámetro configurable debe pasarse por el constructor
    public ApplyRecovery(int recoveredValue)
    {

        _recoveredValue = recoveredValue;

    }

    public override void Update()
    {
        
        if(started) {

            // Aquí se produce la recuperación
            // La fase debe poder configurarse para recuperar vida, mana, o las dos cosas

            Debug.Log( target.name + " recovered " + _recoveredValue + " health points.");

            BattleLog.current.WriteLine(string.Format(BattleLogFormats.HEALTH_RECOVERED, target.name, _recoveredValue));

            End(); // <---- Esto termina la fase, importante

        }

    }

}
