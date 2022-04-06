using UnityEngine;
using Core.Characters;

namespace Core.AI
{

    public abstract class TargetFilter : ScriptableObject
    {
        
        public string id;
        public string description;

        public abstract Character[] GetTargets(Character actor);
        public abstract void DrawGizmos(Character actor);

    }
    
}

