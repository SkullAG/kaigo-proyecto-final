using UnityEngine;
using Core.AI;
using NaughtyAttributes;
using UnityEngine.UI;
using System.Linq;

public class UIGambitElementList : MonoBehaviour
{
    
    [SerializeField, Expandable] private GameGambitList gameGambitList;
    [Space(15)]

    [SerializeField] private RectTransform _targetFilters;
    [SerializeField] private RectTransform _conditions;
    [SerializeField] private RectTransform _actions;

    private CommandList _targetFilterList;
    private CommandList _conditionList;
    private CommandList _actionList;

    private ScrollRectInputsHandler _targetFiltersHandler;
    private ScrollRectInputsHandler _conditionsHandler;
    private ScrollRectInputsHandler _actionsHandler;

    private bool _commandsInstanced = false;

    public enum ListType { none = -1, targets, conditions, actions }

    private void Start() {

        _targetFilterList = _targetFilters.GetComponentInChildren<CommandList>();
        _conditionList = _conditions.GetComponentInChildren<CommandList>();
        _actionList = _actions.GetComponentInChildren<CommandList>();

        _targetFiltersHandler = _targetFilters.GetComponent<ScrollRectInputsHandler>();
        _conditionsHandler = _conditions.GetComponent<ScrollRectInputsHandler>();
        _actionsHandler = _actions.GetComponent<ScrollRectInputsHandler>();

        // Subscribe :D
        _targetFilterList.commandInstanced += SetButtons;
        _conditionList.commandInstanced += SetButtons;
        _actionList.commandInstanced += SetButtons;

        CreateCommands();

    }

    private void OnDisable() {

        _targetFilterList.commandInstanced -= SetButtons;
        _conditionList.commandInstanced -= SetButtons;
        _actionList.commandInstanced -= SetButtons;

    }

    private void CreateCommands() {

        var _gambits = gameGambitList;
        var _selectedCharacter = PartyManager.current.GetSelectedCharacter();

        // Create target commands
        Command[] _targetCommands = new Command[_gambits.targetFilters.Length];

        for(int i = 0; i < _targetCommands.Length; i++) {

            _targetCommands[i] = new Command(i) {
                displayName = _gambits.targetFilters[i].displayName,
                displayDescription = _gambits.targetFilters[i].description
            };

        }

        _targetFilterList.SetCommands(_targetCommands);

        // Create condition commands
        Command[] _conditionCommands = new Command[_gambits.behaviourConditions.Length];

        for(int i = 0; i < _conditionCommands.Length; i++) {

            _conditionCommands[i] = new Command(i) {
                displayName = _gambits.behaviourConditions[i].displayName,
                displayDescription = _gambits.behaviourConditions[i].description
            };

        }

        _conditionList.SetCommands(_conditionCommands);

        // Create action commands
        Command[] _actionCommands = new Command[_selectedCharacter.actions.references.Length];

        for(int i = 0; i < _actionCommands.Length; i++) {

            _actionCommands[i] = new Command(i) {
                displayName = _selectedCharacter.actions.references[i].sharedAction.displayName,
                displayDescription = _selectedCharacter.actions.references[i].sharedAction.description
            };

        }

        _actionList.SetCommands(_actionCommands);

        _commandsInstanced = true;

    }

    public void Activate(int type) {

        if(!_commandsInstanced) CreateCommands();

        _targetFilters.gameObject.SetActive(false);
        _conditions.gameObject.SetActive(false);
        _actions.gameObject.SetActive(false);

        switch(type) {

            case (int)ListType.targets:
                _targetFilters.gameObject.SetActive(true);
                return;

            case (int)ListType.conditions:
                _conditions.gameObject.SetActive(true);
                return;
                
            case (int)ListType.actions:
                _actions.gameObject.SetActive(true);
                return;

            default: return;
                
        }

    }

    public void Deactivate() {

        _targetFilters.gameObject.SetActive(false);
        _conditions.gameObject.SetActive(false);
        _actions.gameObject.SetActive(false);

        _commandsInstanced = false;

    }

    private void SetButtons(int count) {

        _targetFiltersHandler.SetButtons(_targetFilterList.GetNormalButtons());
        _conditionsHandler.SetButtons(_conditionList.GetNormalButtons());
        _actionsHandler.SetButtons(_actionList.GetNormalButtons());

    }

}
