using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TestDerivedClass : TestBaseClass
{

    public int number = 666;

    public override void DoThing() {
        
        Debug.Log(baseClassField);

    }

}
