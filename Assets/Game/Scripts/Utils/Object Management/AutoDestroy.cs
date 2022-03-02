using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestroy : MonoBehaviour
{
    
    public float time = 2.5f;

    private void Awake() {

        Destroy(gameObject, time);

    }

}
