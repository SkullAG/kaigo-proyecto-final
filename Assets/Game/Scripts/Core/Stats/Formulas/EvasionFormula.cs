using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EvasionFormula
{
    
    public const float AGILITY_MAX = 10f;
    public const float EVASION_FACTOR = 0.5f;

    public static float Calculate(float agi1, float agi2) {

        float _result = ((agi2 / agi1 ) / AGILITY_MAX) * 100 * EVASION_FACTOR;

        return _result;

    }

}
