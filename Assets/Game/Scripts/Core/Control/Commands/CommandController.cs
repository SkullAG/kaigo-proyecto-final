using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine;

using Core.Characters;
using Core.Actions;

public class CommandController : Singleton<CommandController>
{

    public enum SubcommandType {actions, items}
    
    [SerializeField] private InputActionReference _enableCommandsAction;
    [SerializeField] private InputActionReference _disableCommandsAction;

    [SerializeField] private bool _enabled = true;

    private EventSystem _eventSystem;
    private CommandList _commandList;
    private ActionQueue _queue;

    public GameObject _battleCommands;
    public GameObject _actionCommands;
    public GameObject _itemCommands;

    private void Awake() {

        _eventSystem = EventSystem.current;
        _commandList = _battleCommands.GetComponentInChildren<CommandList>();

        SetEnabled(true);

    }

    private void Start() {

        PartyManager.current.characterSelected += OnCharacterSelection;

        _queue = 
            PartyManager.current
                .GetSelectedCharacter()
                .GetComponent<ActionQueue>();

    }

    private void Update() {

        var _selectedCharacter = PartyManager.current.GetSelectedCharacter();

        // If there is a selected character
        // and its queue has been found:
        if( _selectedCharacter && _queue) {
            
            // If character is not busy,
            // enable command window!
            if(_queue.isReady) {

                SetEnabled(true);

            } else {

                SetEnabled(false);

            }

        }
 
    }

    public void SetEnabled(bool enabled) {

        if(enabled) {

            _enabled = true;

            _battleCommands.SetActive(true);

            // If enabling, wait to select first option
            _commandList.commandInstanced += OnCommandInstantiation;

        } else {

            _enabled = false;

            _battleCommands.SetActive(false);

            SetSubcommandsEnabled(SubcommandType.actions, false);
            SetSubcommandsEnabled(SubcommandType.items, false);
            
            _commandList.commandInstanced -= OnCommandInstantiation;

        }

    }

    public void SetSubcommandsEnabled(SubcommandType type, bool enabled) {

        switch(type) {

            case SubcommandType.actions:
                _actionCommands.SetActive(enabled);
                if(enabled) _itemCommands.SetActive(false);
                break;

            case SubcommandType.items:
                _itemCommands.SetActive(enabled);
                if(enabled) _actionCommands.SetActive(false);
                break;

            default: break;

        }

    }

    public CommandList GetSubcommandList(SubcommandType type) {

        switch(type) {

            case SubcommandType.actions: 
                return _actionCommands.GetComponentInChildren<CommandList>();

            case SubcommandType.items: 
                return _itemCommands.GetComponentInChildren<CommandList>();

            default: return null;

        }

    }

    private void OnCommandInstantiation(int count) {

        // When buttons are done instantiating, select first button
        _eventSystem.SetSelectedGameObject(_commandList.GetButtons()[0].gameObject);

        _commandList.commandInstanced -= OnCommandInstantiation;

    }

    private void OnCharacterSelection(Character character) {

        // If character selection changed, change queue reference
        _queue = PartyManager.current.GetSelectedCharacter().GetComponent<ActionQueue>();

    }

}
