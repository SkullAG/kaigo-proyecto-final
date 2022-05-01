using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommandButton : Button
{
    
    public System.Action<Command> pressed = delegate {};

    public Command command;

    public bool isHighlighted => IsHighlighted();

    protected override void Awake() {

        base.Awake();

    }

    public override void OnPointerClick(PointerEventData eventData) {

        if(command != null) {

            pressed(command);
            command.Execute();

        }

    }

    public override void OnSubmit(BaseEventData eventData) {

        if(command != null) {

            pressed(command);
            command.Execute();
            
        }

    }

    public void SetCommand(Command command) {

        this.command = command;

    }

}
