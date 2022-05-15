using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnterShop : MonoBehaviour
{
    public GameObject shop;
    public InputActionReference OpenShopAction;
    public InputActionReference CloseShopAction;

    private float angle;
    public float maxAngle;

    private void OnTriggerStay(Collider other)
    {
        if (OpenShopAction.action.triggered && !PauseManager.GameIsPaused)
        {
            Vector3 direction = (transform.position - other.transform.position).normalized;
            angle = Vector3.Angle(direction, other.transform.forward);

            Debug.Log(angle);

            if (Mathf.Abs(angle) < maxAngle)
            {
                shop.SetActive(true);
            }
        }
        //shop.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        shop.SetActive(false);        
    }
    
}
