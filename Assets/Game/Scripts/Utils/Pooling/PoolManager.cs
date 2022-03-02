using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolManager : Singleton<PoolManager>
{

    [SerializeField]
    private Pool[] _pools;

    [SerializeField]
    private bool _instantiateOnStart = true;

    private void Start() {

        if(_instantiateOnStart) {

            foreach (Pool p in _pools){
                p.InstantiateObjects();
            }

        }

    }

    public Pool GetPool(PoolConfig config) {

        foreach (Pool p in _pools) {

            if(p.config == config) {
                return p;
            }
            
        }

        return null;

    }

    public Pool GetPool(string identifier) {

        foreach (Pool p in _pools) {

            if(p.config.identifier == identifier) {
                return p;
            }
            
        }

        return null;

    }

}
