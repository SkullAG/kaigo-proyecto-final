using UnityEngine;
using Core.Gambits;
using Core.AI;
using Core.Actions;
using TMPro;

public class GambitUI : MonoBehaviour
{

    [SerializeField] private RectTransform _handleButton;
    [SerializeField] private RectTransform _targetButton;
    [SerializeField] private RectTransform _conditionButton;
    [SerializeField] private RectTransform _actionButton;

    [SerializeField] private string _emptyText = "Empty";
    
    private TextMeshProUGUI _targetText;
    private TextMeshProUGUI _conditionText;
    private TextMeshProUGUI _actionText;

    private Gambit _lastGambit;
    private TargetFilter _lastTargetFilter;
    private GameAction _lastGameAction;
    private BehaviourCondition _lastCondition;

    public Gambit gambit;

    private void Awake() {

        _targetText = _targetButton.GetComponentInChildren<TextMeshProUGUI>();
        _conditionText = _conditionButton.GetComponentInChildren<TextMeshProUGUI>();
        _actionText = _actionButton.GetComponentInChildren<TextMeshProUGUI>();

    }

    private void Update() {

        if(gambit != null) {

            if(gambit.action != _lastGameAction || gambit.target != _lastTargetFilter || gambit.condition != _lastCondition || _lastGambit != gambit) {

                UpdateButtons();

            }

        }

        _lastGambit = gambit;
        _lastCondition = gambit.condition;
        _lastGameAction = gambit.action;
        _lastTargetFilter = gambit.target;

    }

    private void UpdateButtons() {

        if(gambit != null) {

            if(gambit.target == null)
                _targetText.text = _emptyText;
            else
                _targetText.text = gambit.target.id;
            

            if(gambit.condition == null)
                _conditionText.text = _emptyText;
            else
                _conditionText.text = gambit.condition.id;
            

            if(gambit.action == null)
                _actionText.text = _emptyText;
            else
                _actionText.text = gambit.action.id;
            

        }

    }

}
