using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullScreenSystem : MonoBehaviour
{
    public Toggle _toggle;
    void Start()
    {
        if (Screen.fullScreen)
        {
            _toggle.isOn = true;
        }
        else
        {
            _toggle.isOn = false;
        }
    }
    public void ActivateFS(bool FScreen)
    {
        Screen.fullScreen = FScreen;
    }
}
