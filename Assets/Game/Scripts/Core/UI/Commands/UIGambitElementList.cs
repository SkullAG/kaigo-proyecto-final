using UnityEngine;
using Core.AI;
using NaughtyAttributes;

public class UIGambitElementList : MonoBehaviour
{
    
    [SerializeField, Expandable] private GameGambitList gameGambitList;
    [Space(15)]
    [SerializeField] private CommandList _targetFilterList;
    [SerializeField] private CommandList _conditionList;
    [SerializeField] private CommandList _actionList;

    private bool _commandsInstanced = false;

    public enum ListType { none = -1, targets, conditions, actions }

    private void Start() {

        CreateCommands();

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

        _targetFilterList.gameObject.SetActive(false);
        _conditionList.gameObject.SetActive(false);
        _actionList.gameObject.SetActive(false);

        switch(type) {

            case (int)ListType.targets:
                _targetFilterList.gameObject.SetActive(true);
                return;

            case (int)ListType.conditions:
                _conditionList.gameObject.SetActive(true);
                return;
                
            case (int)ListType.actions:
                _actionList.gameObject.SetActive(true);
                return;

            default: return;
                
        }

    }

    public void Deactivate() {

        _targetFilterList.gameObject.SetActive(false);
        _conditionList.gameObject.SetActive(false);
        _actionList.gameObject.SetActive(false);

        _commandsInstanced = false;

    }

}
