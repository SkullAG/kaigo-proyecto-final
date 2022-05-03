using UnityEngine;
using Core.Gambits;
using TMPro;

public class UIGambitUpdater : MonoBehaviour
{
    
    [SerializeField] private GambitList _gambitList;
    [SerializeField] private GameObject[] _gambitObjects;
    [SerializeField] private GameObject _selectedGambitObject;
    [SerializeField] private string _emptyText = "Empty";

    private int _lastGambitCount = 0;

    public GambitList gambitList => _gambitList;

    private void Update() {

        if(_lastGambitCount != _gambitList.list.Count) {
            UpdateElements();
        }

        _lastGambitCount = _gambitList.list.Count;

    }

    public void UpdateElements() {

        Debug.Log("Update gambits!");

        for ( int i = 0; i < Mathf.Max(_gambitList.list.Count, _gambitObjects.Length); i ++) {

            var _gambitUI = _gambitObjects[i].GetComponent<UIGambit>();

            var _targetText = _gambitUI.targetObject.GetComponentInChildren<TextMeshProUGUI>();
            var _conditionText = _gambitUI.conditionObject.GetComponentInChildren<TextMeshProUGUI>();
            var _actionText = _gambitUI.actionObject.GetComponentInChildren<TextMeshProUGUI>();

            if(i < _gambitObjects.Length) {

                if( i < _gambitList.list.Count ) {

                    _gambitUI.SetInteractable(true);

                    var _gambit = _gambitList.list[i];

                    _targetText.text = _gambit.target != null ? _gambit.target.id : _emptyText;
                    _conditionText.text = _gambit.condition != null ? _gambit.condition.id : _emptyText;
                    _actionText.text = _gambit.actionReference != null ? _gambit.actionReference.sharedAction.displayName : _emptyText;

                    _gambitUI.gambit = _gambit;

                } else {

                    _gambitUI.SetInteractable(false);
                    _gambitUI.gambit = null;
                    
                }

            }

        }

    }

}
