using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class CommandButton : Button
{
    
    public System.Action<Command> pressed = delegate {};

    private TextMeshProUGUI _textMesh;
    private Color _originalColor;

    public Command command;

    public bool isHighlighted => IsHighlighted();

    private bool _interactable;

    protected override void Awake() {

        base.Awake();

        _textMesh = GetComponentInChildren<TextMeshProUGUI>();
        _originalColor = _textMesh.color;

    }

    public override void OnPointerClick(PointerEventData eventData) {

        if(command != null && interactable) {

            pressed(command);
            command.Execute();

        }

    }

    public override void OnSubmit(BaseEventData eventData) {

        if(command != null && interactable) {

            pressed(command);
            command.Execute();
            
        }

    }

    public void SetCommand(Command command) {

        this.command = command;

    }

    public void SetInteractable(bool interactable) {

        if(interactable) {

            _textMesh.color = _originalColor;
            _interactable = false;

        } else if (!interactable) {

            _textMesh.color = Color.gray;
            _interactable = true;

        }

        

    }

}
