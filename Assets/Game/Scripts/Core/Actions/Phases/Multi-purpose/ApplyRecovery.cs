using UnityEngine;
using Core.Actions;

public class ApplyRecovery : ActionPhase
{

    //private int _recoveredValue;
    private int heal;
    private int mana;

    // Cualquier parámetro configurable debe pasarse por el constructor
    public ApplyRecovery(int heal, int mana)
    {

        this.heal = heal;
        this.mana = mana;

    }

    public override void Update()
    {
        
        if(started) {

            // Aquí se produce la recuperación
            // La fase debe poder configurarse para recuperar vida, mana, o las dos cosas

            target.stats.healthPoints.value += heal;
            target.stats.actionPoints.value += mana;

            End(); // <---- Esto termina la fase, importante

        }

    }

}
