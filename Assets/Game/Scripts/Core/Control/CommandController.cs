using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.InputSystem;

public class CommandController : Singleton<CommandController>
{

    public GameObject _battleCommands;
    public GameObject _actionCommands;
    public GameObject _itemCommands;

    [SerializeField] private InputActionReference _enableCommandsAction;
    [SerializeField] private InputActionReference _disableCommandsAction;

    private EventSystem _eventSystem;
    private CommandList _commandList;

    private bool _enabled = true;

    private void Awake() {

        _eventSystem = EventSystem.current;
        _commandList = GetComponent<CommandList>();

    }

    private void Update() {

        if(_enableCommandsAction.action.triggered && !_enabled) {

            _battleCommands.SetActive(true);

            _commandList.commandInstanced += OnCommandInstantiation;

        } else if(_disableCommandsAction.action.triggered && _enabled) {

            _battleCommands.SetActive(false);

            _actionCommands.SetActive(false);
            _itemCommands.SetActive(false);

            _commandList.commandInstanced -= OnCommandInstantiation;

        }
 
    }

    private void OnCommandInstantiation(int count) {

        // When buttons are done instantiating, select first button
        _eventSystem.SetSelectedGameObject(_commandList.GetButtons()[0].gameObject);

    }

}
