using UnityEngine.EventSystems;
using UnityEngine;
using UnityEngine.UI;
using Core.Gambits;
using Core.AI;
using Core.Actions;
using TMPro;

public class UIGambit : MonoBehaviour
{

    public Gambit gambit;
    public bool interactable = true;

    public RectTransform handleObject;
    public RectTransform targetObject;
    public RectTransform conditionObject;
    public RectTransform actionObject;

    private UIGambitUpdater _updater;

    private TargetFilter _lastTargetFilter = null;
    private BehaviourCondition _lastCondition = null;
    private GameAction _lastAction = null;
    private CanvasGroup _group;

    private void Awake() {

        _updater = GetComponentInParent<UIGambitUpdater>();
        _group = GetComponent<CanvasGroup>();

    }

    private void FixedUpdate() {

        if(interactable) {

            if(gambit.target != _lastTargetFilter || gambit.condition != _lastCondition || gambit.action != _lastAction) {
                _updater.UpdateElements();
            }

            _lastTargetFilter = gambit.target;
            _lastCondition = gambit.condition;
            _lastAction = gambit.action;

        }

    }

    public void SetInteractable(bool interactable) {

        if(interactable) {

            this.interactable = true;
            _group.interactable = true;
            _group.alpha = 1;


        } else {

            this.interactable = false;
            _group.interactable = false;
            _group.alpha = 0.5f;

        }

    }

}
