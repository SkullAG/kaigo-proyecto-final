using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnterShop : MonoBehaviour
{
    public GameObject shop;
    private void OnTriggerEnter(Collider other)
    {
        shop.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        shop.SetActive(false);        
    }
    
}
