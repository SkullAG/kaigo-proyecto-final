using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickableObject : MonoBehaviour
{
    public Objects _obj;

    private void OnTriggerEnter(Collider other)
    {
        PickSystem picker = other.GetComponent<PickSystem>();

        if(picker && picker.PickObj(_obj))
        {
            gameObject.SetActive(false);
        }
    }
}
