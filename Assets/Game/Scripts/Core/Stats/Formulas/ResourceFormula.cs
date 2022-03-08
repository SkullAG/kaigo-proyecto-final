using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ResourceFormula
{

    public const int HP_LIMIT = 999;
    public const float HP_FACTOR = 1;

    public static float CalculateMaxHP(float vitality, float prowess, float robustness) {

        float _result = vitality * 15 + prowess * 5 + robustness * 10;

        return _result;

    }

    public static float CalculateMaxAP(float prowess, float perseverance) {

        float _result = prowess * 5 + perseverance * 10;

        return _result;

    }

}
