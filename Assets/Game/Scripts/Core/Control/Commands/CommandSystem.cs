using UnityEngine;
using UnityEngine.EventSystems;

public class CommandSystem : Singleton<CommandSystem>
{

    public Command selectedCommand => GetSelectedCommand();
    public Command highlightedCommand => GetHighlightedCommand();
    
    public Command GetSelectedCommand() {

        GameObject _selected = EventSystem.current.currentSelectedGameObject;

        if(_selected != null) {

            CommandButton _button = _selected.GetComponent<CommandButton>();

            return _button.command;

        }

        return null;

    }

    public Command GetHighlightedCommand() {

        GameObject _selected = EventSystem.current.currentSelectedGameObject;

        if(_selected != null) {

            CommandButton _button = _selected.GetComponent<CommandButton>();

            if(_button.isHighlighted) {
                return _button.command;
            }

        }

        return null;

    }

}
