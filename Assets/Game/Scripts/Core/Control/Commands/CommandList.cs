using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class CommandList : MonoBehaviour
{

    public System.Action<int> commandInstanced = delegate {};
    public System.Action<GameObject> commandConfirmed = delegate {};
    
    [SerializeField] private Transform _buttonPrefab;
    [SerializeField] private bool _updateAutomatically = true;

    [SerializeReference] private Command[] _commands;
    [SerializeField] private Button[] _buttons;

    private int _lastElementCount = 0;

    private void OnEnable() {

        if(_commands != null) {

            InstantiateButtons();

        }

    }

    public void SetCommands(Command[] commands) {

        _commands = commands;

        InstantiateButtons(); // Reinstantiate buttons when elements list has changed 

    }

    public Button[] GetButtons() {

        return _buttons;

    }
    

    public void SelectButton(int index) {

        if(_buttons != null) {

            if(_buttons.Length > 0) {
                EventSystem.current.SetSelectedGameObject(_buttons[index].gameObject);
            }

        } 

    }

    public void ExecuteCommand(int index) {

        _commands[index].Execute();

    }

    private void DestroyChildren() {

        // Kill children >:)
        foreach(Transform c in transform) {

            Destroy(c.gameObject);

        }

    }

    private void InstantiateButtons() {

        DestroyChildren();

        _buttons = new Button[_commands.Length];

        // Instantiate each command prefab as children
        for(int i = 0; i < _commands.Length; i++) {

            // Instantiate button prefab
            CommandButton _button = Instantiate(_buttonPrefab).GetComponent<CommandButton>();
            _button.gameObject.name = _commands[i].displayName + " button";

            // Make it child
            _button.transform.SetParent(transform);

            // Set button text
            _button.GetComponentInChildren<TextMeshProUGUI>().text = _commands[i].displayName;

            _button.command = _commands[i];
            _button.pressed += OnButtonPress;
            
            // Add button to array
            _buttons[i] = _button;

        }

        commandInstanced(_commands.Length);

    }

    public int GetCommandIndex(Command command) {

        for(int i = 0; i < _commands.Length; i++) {

            if(_commands[i] == command) {
                return i;
            }

        }

        return -1;

    }

    public void ReplaceCommand(int index, Command command) {

        if(index > -1) {

            Debug.Log("Commands length on replace is " + _commands.Length, gameObject);

            _commands[index] = command;
            InstantiateButtons();

        }

    }

    private void OnButtonPress(Command command) {

        command.Execute();

    }

}
