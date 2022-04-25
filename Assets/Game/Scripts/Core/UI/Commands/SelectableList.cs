using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class SelectableList : MonoBehaviour
{

    public System.Action<int> onListRebuilt = delegate {};
    public System.Action<GameObject> onItemSelected = delegate {};
    
    [SerializeField]
    private Button _referenceButton;

    [SerializeField]
    private bool _updateAutomatically = true;

    private IListableElement[] _elements;

    private int _lastElementCount = 0;

    private void Start() {

        if( _referenceButton != null && _elements != null) {

            DestroyChildren();
            InstantiateButtons();

        }

    }

    private void Update() {

        if(_updateAutomatically && _elements != null) {

            if(_lastElementCount != _elements.Length) {

                InstantiateButtons();

            }

            _lastElementCount = _elements.Length;

        }

    }

    public void SetElements(IListableElement[] elements) {

        _elements = elements;

    }

    private void DestroyChildren() {

        // Kill children :)
        foreach(Transform c in transform) {
            Destroy(c.gameObject);
        }

    }

    private void InstantiateButtons() {

        DestroyChildren();

        // Make children :D
        for(int i = 0; i < _elements.Length; i++) {

            Transform _elem = Instantiate(_referenceButton).transform;
            _elem.parent = transform;

        }

        onListRebuilt(_elements.Length);

    }

}
