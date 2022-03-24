using UnityEngine;
using UnityEngine.EventSystems;
using Core.Gambits;

public class UIGambitHandle : MonoBehaviour, ISelectHandler, IMoveHandler, IDeselectHandler
{

    [SerializeField]
    private RectTransform _parent;

    [SerializeField]
    private float _offset;

    private UIGambitUpdater _updater;
    private Vector3 _originalPosition;

    public bool isSelected = false;

    private void Awake() {

        _updater = GetComponentInParent<UIGambitUpdater>();
        _originalPosition = _parent.localPosition;

    }

    public void OnSelect(BaseEventData eventData) {

        isSelected = true;
        //_parent.localPosition = (Vector2)_originalPosition + Vector2.right * _offset;

    }

    public void OnDeselect(BaseEventData eventData) {

        isSelected = false;
        //_parent.localPosition = _originalPosition;

    }

    public void OnMove(AxisEventData eventData) {

        UIGambit _ui = GetComponentInParent<UIGambit>();
        Gambit _gambit = _ui.gambit;

        switch(eventData.moveDir) {

            case MoveDirection.Up:
                _updater.gambitList.ShiftGambit(-1, _gambit);
                Debug.Log("Move up!");
                break;

            case MoveDirection.Down:
                _updater.gambitList.ShiftGambit(1, _gambit);
                Debug.Log("Move down!");
                break;

        }
        
        _updater.UpdateElements();

    }

}
