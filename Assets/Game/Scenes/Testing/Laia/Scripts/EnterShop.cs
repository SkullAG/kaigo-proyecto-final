using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnterShop : MonoBehaviour
{
    public GameObject shop;
    public InputActionReference OpenShopAction;
    public InputActionReference CloseShopAction;

    private Collider _other;

    private float angle;
    public float maxAngle;

    private bool ActivatedShop = false;
    private bool InsideTrigger;

    private void Update()
    {        
        if (OpenShopAction.action.triggered && !PauseManager.GameIsPaused && !ActivatedShop && InsideTrigger)
        {
            Vector3 direction = (transform.position - _other.transform.position).normalized;

            Debug.Log("Intentando abrir tienda");

            if (Mathf.Abs(angle) < maxAngle)
            {
                shop.SetActive(true);
            }
        }        
    }

    private void OnTriggerStay(Collider other)    
    {
        InsideTrigger = true;
        _other = other;
    }

    private void OnTriggerExit(Collider other)
    {
        shop.SetActive(false);
        InsideTrigger = false;
        _other = null;
    }

}
