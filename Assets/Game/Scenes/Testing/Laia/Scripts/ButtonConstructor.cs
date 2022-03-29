using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonConstructor : MonoBehaviour
{
    public GameObject buttons;
    public GameObject _button;

    public void MakeButton()
    {
        Instantiate(_button, transform.position, transform.rotation);
    }
}
