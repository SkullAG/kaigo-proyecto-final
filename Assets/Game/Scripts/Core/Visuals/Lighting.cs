using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Lighting : MonoBehaviour
{
    LineRenderer ligtingLine;

    private void Start()
    {
        ligtingLine = GetComponent<LineRenderer>();
    }
}
