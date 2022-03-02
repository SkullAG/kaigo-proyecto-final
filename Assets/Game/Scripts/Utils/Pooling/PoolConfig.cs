using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pool Config", menuName = "Game/Pooling/Pool")]
public class PoolConfig : ScriptableObject
{

    public string identifier;
    public Transform pooledObject;
    public int numberOf;

}
