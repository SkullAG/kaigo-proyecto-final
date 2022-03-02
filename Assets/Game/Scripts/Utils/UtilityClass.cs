using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UtilityClass
{
    
    public static Vector2 GetDirection(float angle) {
        return new Vector2( Mathf.Cos(angle * Mathf.Deg2Rad), Mathf.Sin(angle * Mathf.Deg2Rad) ).normalized;
    }

    public static float Scale01(float n, float min, float max) {
        return (n - min) / (max - min);
    }

    public static void SetX(this Vector3 vector, float x) {
        Vector3 _v = vector;
        _v.x = x;
        vector = _v;
    }

    public static void SetY( this Vector3 vector, float y ) {
        Vector3 _v = vector;
        _v.y = y;
        vector = _v;
    }

    public static void SetZ( this Vector3 vector, float z ) {
        Vector3 _v = vector;
        _v.z = z;
        vector = _v;
    }

    public static bool IsValidLayer(int layer, LayerMask mask) {
        return (mask.value & 1 << layer) != 0;
    }

}
