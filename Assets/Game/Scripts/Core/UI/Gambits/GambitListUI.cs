using UnityEngine;
using UnityEngine.UI;
using Core.Gambits;

public class GambitListUI : MonoBehaviour
{
    
    [SerializeField]
    private GambitList _gambitList;

    [SerializeField]
    private GameObject[] _transforms;

    private int _lastGambitCount = 0;

    private void Update() {

        if(_lastGambitCount != _gambitList.list.Count) {
            UpdateElements();
        }

        _lastGambitCount = _gambitList.list.Count;

    }

    private void UpdateElements() {

        for ( int i = 0; i < Mathf.Max(_gambitList.list.Count, _transforms.Length); i ++) {

            if(i < _transforms.Length) {

                if( i < _gambitList.list.Count ) {

                    _transforms[i].gameObject.SetActive(true);
                    _transforms[i].GetComponent<GambitUI>().gambit = _gambitList.list[i];

                } else {

                    _transforms[i].gameObject.SetActive(false);
                    _transforms[i].GetComponent<GambitUI>().gambit = null;
                    
                }

            }

        }

    }

}
