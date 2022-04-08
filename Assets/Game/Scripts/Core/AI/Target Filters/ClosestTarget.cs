using System.Collections.Generic;
using Core.AI;
using Core.Characters;
using UnityEngine;
using NaughtyAttributes;

[CreateAssetMenu(fileName = "Closest Target Filter", menuName = "Game/AI/Target Filters/Closest Target")]
public class ClosestTarget : TargetFilter
{

    public float range;
    public LayerMask mask;
    public QueryTriggerInteraction triggerQuery;

    [Tag] public string tag;

    private Collider[] _colliders;

    private void OnEnable() {

        _colliders = null;

    }

    public override Character GetTarget(Character actor)
    {

        Collider[] _collidersLocal = Physics.OverlapSphere(actor.transform.position, range, mask, triggerQuery);

        Character _closest = null;
        float _minDistance = Mathf.Infinity;

        for (int i = 0; i < _collidersLocal.Length; i++) {
            
            // needs optimization
            Character _c = _collidersLocal[i].GetComponent<Character>();

            if(_collidersLocal[i].gameObject.CompareTag(tag) && _c != null) {

                if(_c != actor) {

                    float _dist = Vector3.Distance(_c.transform.position, actor.transform.position);

                    if( _dist < _minDistance ) {

                        _minDistance = _dist;
                        _closest = _c;

                    }

                }

            }

        }

        _colliders = _collidersLocal;

        return _closest; // Need to return closest character

    }

    
    public override void DrawGizmos(Character actor) {

    #if UNITY_EDITOR


        if(actor != null) {

            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(actor.transform.position, range);

            if(_colliders != null) {

                Gizmos.color = Color.green;

                for (int i = 0; i < _colliders.Length; i++) {
                    
                    Gizmos.DrawWireSphere(_colliders[i].transform.position, 0.1f);

                }

            }

        }

    #endif

    }

   

}
