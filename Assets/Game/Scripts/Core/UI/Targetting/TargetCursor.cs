using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetCursor : MonoBehaviour
{
    
    public Transform target;
    public Vector3 offset;

    private void Update() {

        if(target != null) {
            transform.position = target.position + offset;
        }

    }

}
