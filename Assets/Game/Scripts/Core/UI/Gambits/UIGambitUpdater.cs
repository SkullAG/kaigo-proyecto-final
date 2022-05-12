using UnityEngine;
using Core.Gambits;
using TMPro;
using Core.Characters;

public class UIGambitUpdater : MonoBehaviour
{
    
    [SerializeField] private GameObject[] _gambitObjects;
    [SerializeField] private GameObject _selectedGambitObject;
    [SerializeField] private string _emptyText = "Empty";
    [SerializeField] private int _maxDisplayableGambits = 6;

    private GambitList _gambitList;
    private int _lastGambitCount = 0;

    public GambitList gambitList => _gambitList;
    public GameObject[] gambitButton => _gambitObjects;

    private void Start() {

        var _partyManager = PartyManager.current;

        if(_partyManager) SetCharacter(PartyManager.current.PartyMembers[0].GetComponent<Character>());

    }

    private void Update() {

        if(_gambitList != null) {

            if(_lastGambitCount != _gambitList.set.list.Count) {
                UpdateElements();
            }

            _lastGambitCount = _gambitList.set.list.Count;

        }

    }

    public void UpdateElements() {

        if(_gambitList != null) {

            Debug.Log("Update gambits!");

            for ( int i = 0; i < Mathf.Max(_gambitList.set.list.Count, _maxDisplayableGambits); i ++) {

                var _gambitUI = _gambitObjects[i].GetComponent<UIGambit>();

                var _targetText = _gambitUI.targetObject.GetComponentInChildren<TextMeshProUGUI>();
                var _conditionText = _gambitUI.conditionObject.GetComponentInChildren<TextMeshProUGUI>();
                var _actionText = _gambitUI.actionObject.GetComponentInChildren<TextMeshProUGUI>();

                if(i < _gambitObjects.Length) {

                    if( i < _gambitList.set.list.Count ) {
                        
                        _gambitUI.SetInteractable(true);
                        _gambitUI.gameObject.SetActive(true);

                        var _gambit = _gambitList.set.list[i];

                        _targetText.text = _gambit.target != null ? _gambit.target.displayName : _emptyText;
                        _conditionText.text = _gambit.condition != null ? _gambit.condition.displayName : _emptyText;
                        _actionText.text = _gambit.actionReference.sharedAction != null ? _gambit.actionReference.sharedAction.displayName : _emptyText;

                        _gambitUI.gambit = _gambit;

                    } else {
                        
                        _gambitUI.gameObject.SetActive(false);

                        // Set gambit as non interactable
                        //_gambitUI.SetInteractable(false);
                        //_gambitUI.gambit = null;
                        
                    }

                }

            }

        }

    }

    public void SetCharacter(Character character) {

        _gambitList = character.gambits;

    }

    public void SetInteractable(bool interactable) {

        foreach (GameObject obj in _gambitObjects) {
            
            var _gambitUI = obj.GetComponent<UIGambit>();

            _gambitUI.SetInteractable(interactable);

        }

    }

}
