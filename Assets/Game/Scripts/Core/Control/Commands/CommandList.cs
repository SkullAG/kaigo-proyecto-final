using System.Linq;
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

    private Command[] _commands;
    private Button[] _buttons;

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

        EventSystem.current.SetSelectedGameObject(_buttons[index].gameObject);

    }

    public void ExecuteCommand(int index) {

        _commands[index].Execute();

    }

    public Button GetSelectedButton() {

        Button _button = EventSystem.current.currentSelectedGameObject.GetComponent<Button>();

        if(_button != null) {

            return _buttons.FirstOrDefault(x => x == _button);

        }

        return null;

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
            Button _button = Instantiate(_buttonPrefab).GetComponent<Button>();
            _button.gameObject.name = _commands[i].displayName + " button";

            // Make it child
            _button.transform.SetParent(transform);

            // Set button text
            _button.GetComponentInChildren<TextMeshProUGUI>().text = _commands[i].displayName;

            // Add command to button
            CommandButton _commandButton =  _button.gameObject.AddComponent<CommandButton>();
            
            _commandButton.command = _commands[i];
            _commandButton.pressed += OnButtonPress;
            
            // Add button to array
            _buttons[i] = _button;

        }

        commandInstanced(_commands.Length);

    }

    private void OnButtonPress(Command command) {

        command.Execute();

    }

}
