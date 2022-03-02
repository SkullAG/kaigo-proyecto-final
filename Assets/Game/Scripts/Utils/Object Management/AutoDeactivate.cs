using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDeactivate : MonoBehaviour
{

    [SerializeField]
    private float _time;

    private float _timer;

    private void Update() {

        _timer += Time.deltaTime;

        if(_timer >= _time) {

            _timer = 0;
            gameObject.SetActive(false);
            
        }

    }

}
