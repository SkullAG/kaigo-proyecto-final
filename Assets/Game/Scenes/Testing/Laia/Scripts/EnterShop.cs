using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnterShop : MonoBehaviour
{
    public InputActionReference OpenShopAction;
    public InputActionReference CloseShopAction;

    private GameObject shop => ShopGetter.current.shop.gameObject;
    private Collider _other;

    public float maxAngle;

    private bool ActivatedShop = false;
    private bool InsideTrigger;

    private void Update()
    {        

        if (OpenShopAction.action.triggered && !PauseManager.GameIsPaused && !ActivatedShop && InsideTrigger)
        {

            Vector3 direction = (transform.position - _other.transform.position).normalized;

            Debug.Log("Intentando abrir tienda");

            float _angle = Vector3.Angle(direction, _other.transform.forward);

            if (Mathf.Abs(_angle) < maxAngle)
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
