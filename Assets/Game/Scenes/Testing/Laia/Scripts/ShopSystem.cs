using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopSystem : MonoBehaviour
{
    public GameObject canvas;
    //public float length;
    public LayerMask box;
    public float radius;
    // Start is called before the first frame update
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnEnableShop();
        }        
    }

    public void OnEnableShop()
    {
        Collider[] EnableShop = Physics.OverlapSphere(transform.position, radius, box);
        /*if ()
        {
            canvas.SetActive(true);
        }*/
    }
}
