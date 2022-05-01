using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandButton : Button
{
    
    public System.Action<Command> pressed;

    public Command command;

    public bool isHighlighted => IsHighlighted();

    protected override void Awake() {

        base.Awake();

    }

    public override void OnPointerClick(PointerEventData eventData) {
        pressed(command);
    }

    public override void OnSubmit(BaseEventData eventData) {
        pressed(command);
    }

    public void SetCommand(Command command) {

        this.command = command;

    }

}
