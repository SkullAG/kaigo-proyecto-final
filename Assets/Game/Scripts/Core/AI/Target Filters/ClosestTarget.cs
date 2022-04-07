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

    public override Character[] GetTargets(Character actor)
    {
        
        _colliders = Physics.OverlapSphere(actor.transform.position, range, mask, triggerQuery);

        List<Character> _characters = new List<Character>();

        for (int i = 0; i < _colliders.Length; i++) {
            
            Character _c = _colliders[i].GetComponent<Character>();

            if(_colliders[i].gameObject.CompareTag(tag) && _c != null) {

                _characters.Add(_colliders[i].GetComponent<Character>());

            }

        }

        // Get closest character

        //if(_characters.Count == 0) Debug.Log("No targets found!");

        return _characters.ToArray();

    }

    #if UNITY_EDITOR

    public override void DrawGizmos(Character actor) {

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

    }

    #endif

}
