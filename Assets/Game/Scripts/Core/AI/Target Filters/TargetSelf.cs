using UnityEngine;
using Core.AI;
using Core.Characters;

[CreateAssetMenu(fileName = "Self", menuName = "Game/AI/Target Filters/Self")]
public class TargetSelf : TargetFilter
{

    public override Character GetTarget(Character actor) {
        return actor;
    }

    public override void DrawGizmos(Character actor) {}

}
