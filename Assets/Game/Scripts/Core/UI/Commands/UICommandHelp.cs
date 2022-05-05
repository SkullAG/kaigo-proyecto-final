using UnityEngine;
using NaughtyAttributes;
using TMPro;

public class UICommandHelp : MonoBehaviour
{
    
    [SerializeField] private Transform _container;
    [SerializeField] private TextMeshProUGUI _textMesh;
    
    [Tooltip("Format parameters are: command name and command description.")]
    [SerializeField, TextArea] private string _format;

    private void Update() {

        Command _c = CommandSystem.current?.selectedCommand;

        if(_c != null) {

            _container.gameObject.SetActive(true);
            _textMesh.text = string.Format(_format, _c.displayName, _c.displayDescription);

        } else {

            _container.gameObject.SetActive(false);

        }

    }

}
