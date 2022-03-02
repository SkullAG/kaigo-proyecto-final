using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NaughtyAttributes;

[System.Serializable]
public class Pool
{
    
    public PoolConfig config;
    public Transform parent;

    [SerializeField, NonReorderable, ReadOnly]
    private List<Transform> _list = new List<Transform>();

    public void InstantiateObjects() {

        for(int i = 0; i < config.numberOf; i++) {

            Transform _obj = GameObject.Instantiate(config.pooledObject, Vector3.zero, Quaternion.identity);

            if(parent != null) _obj.SetParent(parent);
            
            _obj.gameObject.SetActive(false);
            _list.Add(_obj);

        }

    }

    public Transform GetObject(Vector3 position, Vector3 scale, float rotation = 0, bool setActive = false) {

        foreach (Transform t in _list){

            if(!t.gameObject.activeInHierarchy) {

                Transform _obj = t;
                t.position = position;
                t.localScale = scale;
                t.eulerAngles = new Vector3(0, 0, rotation);

                if(setActive) t.gameObject.SetActive(true);

                return t;
            }

        }

        return null;

    }

    public void Clear() {
        _list.Clear();
    }

}
