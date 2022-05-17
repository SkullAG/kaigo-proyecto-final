using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class EnterShop : MonoBehaviour
{
    public GameObject shop;
    public InputActionReference OpenShopAction;
    public InputActionReference CloseShopAction;

    private NavBodySistem _other;

    private float angle;
    public float maxAngle;

    private bool ActivatingShop = false;

    private void Update()
    {
        
            if (OpenShopAction.action.triggered && !PauseManager.GameIsPaused && _other && !ActivatingShop)
            {

                Vector3 direction = (transform.position - _other.transform.position).normalized;

                StartCoroutine(ActiveShop(direction, _other));

                Debug.Log("Intentando abrir tienda");

                /*if (Mathf.Abs(angle) < maxAngle)
                {
                    shop.SetActive(true);
                }*/
            }
        
    }
    private void OnTriggerEnter(Collider other)
    {
        if (!ActivatingShop)
        {
            _other = other.GetComponent<NavBodySistem>();
        }
        /*if (OpenShopAction.action.triggered && !PauseManager.GameIsPaused)
        {
            
            Vector3 direction = (transform.position - other.transform.position).normalized;

            StartCoroutine(ActiveShop(direction, other.GetComponent<NavBodySistem>()));

            Debug.Log("Intentando abrir tienda");

            /*if (Mathf.Abs(angle) < maxAngle)
            {
                shop.SetActive(true);
            }*/
       
        //shop.SetActive(true);
    }

    private void OnTriggerExit(Collider other)
    {
        shop.SetActive(false);
        if (other == _other)

        {            
            other = null;
            StopAllCoroutines();

            ActivatingShop = false;
        }
    }
    
    IEnumerator ActiveShop(Vector3 dir, NavBodySistem other)
    {

        if(other == null)
        {
            yield break;
        }
        ActivatingShop = true;

        while (!(Mathf.Abs(angle) < maxAngle))
        {
            other.RotateTowards(dir);
            angle = Vector3.Angle(dir, other.transform.forward);
            yield return new WaitForEndOfFrame();
        }

        ActivatingShop = false;

        shop.SetActive(true);
    }


}
