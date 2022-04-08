using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSingleton : Singleton<InputSingleton>
{
    public PlayerInput input;

    public override void Awake()
    {
        base.Awake();
        DontDestroyOnLoad(this.gameObject);
    }
}
