using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestDerivedClass2 : TestBaseClass
{

    public bool boolean = false;

    public override void DoThing() {
        
        Debug.Log(baseClassField);

    }

}
