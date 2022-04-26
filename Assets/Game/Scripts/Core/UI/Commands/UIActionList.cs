using UnityEngine;
using Core.Characters;
using Core.Actions;

[RequireComponent(typeof(ButtonList))]
public class UIActionList : MonoBehaviour
{

    private ButtonList _selectableList;
    private PartyManager _partyManager;
    private ActionList _actionList;

    private int _lastSelectedCharacter = -1;

    private void Start() {

        _selectableList = GetComponent<ButtonList>();
        _partyManager = PartyManager.current;

    }

    private void Update() {

        if(_lastSelectedCharacter != _partyManager.selectedCharacter) {

            _actionList = _partyManager.PartyMembers[_partyManager.selectedCharacter].GetComponent<ActionList>();
        
            _selectableList.SetElements(_actionList.actions);

        }

        _lastSelectedCharacter = _partyManager.selectedCharacter;

    }

}
