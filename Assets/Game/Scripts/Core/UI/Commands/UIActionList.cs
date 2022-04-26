using UnityEngine;
using Core.Characters;
using Core.Actions;

[RequireComponent(typeof(SelectableList))]
public class UIActionList : MonoBehaviour
{

    private SelectableList _selectableList;
    private PartyManager _partyManager;
    private ActionList _actionList;
    private Character _selectedCharacter;

    private int _lastActionCount;

    private void Awake() {

        _selectableList = GetComponent<SelectableList>();
        _partyManager = PartyManager.current;

    }

    private void Update() {

        if(_actionList.actions.Length != _lastActionCount) {

            SetElements();

        }

        _lastActionCount = _actionList.actions.Length;

    }

    private void OnEnable() {
        
        // Get selected character
        _selectedCharacter = _partyManager.PartyMembers[_partyManager.selectedCharacter].GetComponent<Character>();

        // Get action list
        _actionList = _selectedCharacter.actions;

    }

    private void SetElements() {

        // Set elements in UI list
        var actions = _selectedCharacter.actions.actions;
        _selectableList.SetElements(actions);

    }

}
