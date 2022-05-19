using Core.Characters;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossSingleton : MonoBehaviour
{
    public static Character current;

    // Start is called before the first frame update
    void Awake()
    {
        current = GetComponent<Character>();
    }
}
