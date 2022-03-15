using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ButtonHelper : MonoBehaviour
{
    public int value;

    public UnityEvent<int> SendOnClick;

    public void OnClick()
    {
        SendOnClick.Invoke(value);
    }
}
