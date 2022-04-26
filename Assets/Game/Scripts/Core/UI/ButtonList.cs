using UnityEngine;
using UnityEngine.UI;
using NaughtyAttributes;
using TMPro;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class ButtonList : MonoBehaviour
{

    public System.Action<int> onListRebuilt = delegate {};
    public System.Action<GameObject> onElementSelected = delegate {};
    
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

        // Kill children >:)
        foreach(Transform c in transform) {
            Destroy(c.gameObject);
        }

    }

    private void InstantiateButtons() {

        DestroyChildren();

        // Make children ._.
        for(int i = 0; i < _elements.Length; i++) {

            Transform _elem = Instantiate(_referenceButton).transform;
            _elem.parent = transform;

            ButtonHelper _helper = _elem.GetComponent<ButtonHelper>();

            _helper.value = i;
            _helper.SendOnClick.AddListener(OnButtonClick);

            TextMeshProUGUI _textMesh = _elem.GetComponentInChildren<TextMeshProUGUI>();
            _textMesh.text = _elements[i].displayName;

        }

        onListRebuilt(_elements.Length);

    }

    private void OnButtonClick(int elementIndex) {

        Debug.Log("Clicked element with index " + elementIndex);

        onElementSelected(transform.GetChild(elementIndex).gameObject);

    }

}
