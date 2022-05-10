using UnityEngine;
using Core.Characters;

namespace Core.AI
{

    public abstract class TargetFilter : ScriptableObject
    {
        
        public string displayName = "NO_NAME";
        public string id;
        public string description = "NO_DESC";

        public abstract Character GetTarget(Character actor);
        public abstract void DrawGizmos(Character actor);

    }
    
}

