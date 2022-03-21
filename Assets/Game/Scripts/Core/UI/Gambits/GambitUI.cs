using UnityEngine;
using Core.Gambits;
using Core.AI;
using Core.Actions;
using TMPro;

public class GambitUI : MonoBehaviour
{
    
    [SerializeField] private TextMeshProUGUI _targetText;
    [SerializeField] private TextMeshProUGUI _conditionText;
    [SerializeField] private TextMeshProUGUI _actionText;

    private Gambit _lastGambit;
    private TargetFilter _lastTargetFilter;
    private GameAction _lastGameAction;
    private BehaviourCondition _lastCondition;

    public Gambit gambit;

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
            
            // Temp
            _targetText.text = gambit.target != null ? gambit.target.id : "Empty";
            _conditionText.text = gambit.condition != null ? gambit.condition.id : "Empty";
            _actionText.text = gambit.action != null ? gambit.action.id : "Empty";

        }

    }

}
