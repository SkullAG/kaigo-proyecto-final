using UnityEngine;
using Core.AI;
using Core.Characters;

[CreateAssetMenu(fileName = "Example Target Filter", menuName = "Game/AI/Example/Target Filter")]
public class ExampleTargetFilter : TargetFilter
{

    // Abstract method that returns a list of targets (of type Character)
    public override Character GetTarget(Character actor)
    {
        
        // Return every Character in the scene
        //Character _target = FindObjectsOfType<Character>();

        return actor;

    }

    public override void DrawGizmos(Character actor) {

        

    }

}
