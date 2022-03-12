using UnityEngine;
using Core.AI;
using Core.Characters;

[CreateAssetMenu(fileName = "Example Target Filter", menuName = "Game/AI/Example/Target Filter")]
public class ExampleTargetFilter : TargetFilter
{

    // Abstract method that returns a list of targets (of type Character)
    public override Character[] GetTargets()
    {
        
        // Return every Character in the scene
        Character[] _targets = FindObjectsOfType<Character>();

        return _targets;

    }

}
