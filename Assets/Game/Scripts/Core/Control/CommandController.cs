using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;


public class CommandController : MonoBehaviour
{

    [SerializeField]
    private GameObject _commandContainer;

    [SerializeField]
    private Command[] _commands;

    private EventSystem _eventSystem;

    public Button selectedButton => GetSelectedCommand(); 

    private bool _enabled;

    private void Awake() {

        _eventSystem = EventSystem.current;

    }

    private void Update() {

        if(GetSelectedCommand() != null) {

            

        }

    }

    public void Toggle() {

        _enabled = !_enabled;

        if(_enabled) {

            _commandContainer.SetActive(true);

        } else {

            _commandContainer.SetActive(false);
            
        }

    }
    
    public Button GetSelectedCommand() {

        GameObject _object = _eventSystem.currentSelectedGameObject;

        if(_object.CompareTag("Command")) {

            return _object.GetComponent<Button>();

        }

        return null;

    }

}
