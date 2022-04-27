using UnityEngine;
using UnityEngine.UI;

public class CommandButton : MonoBehaviour
{
    
    public System.Action<Command> pressed;

    public Command command;
    
    private Button _button;

    private void Awake() {

        _button = GetComponent<Button>();

        _button.onClick.AddListener(Press);

    }

    public void Press() {

        pressed(command);

    }

}
