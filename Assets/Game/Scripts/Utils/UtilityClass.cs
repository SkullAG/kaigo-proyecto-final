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

    public static int FindClosestPointInDirection(Vector2[] points, Vector2 p, Vector2 dir, float fov = 0.25f) {

        float _shortestDistance = Mathf.Infinity;
        int _index = -1;

        for (int i = 0; i < points.Length; i++) {
            
            Vector2 _dir = (points[i] - p).normalized;

            if( (_dir - dir).magnitude < fov ) {

                float _dist = (p - points[i]).sqrMagnitude;

                if( _dist < _shortestDistance ) {

                    _shortestDistance = _dist;
                    _index = i;

                }

            }

        }

        return _index;

    }

}
