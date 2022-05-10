using UnityEngine;
using Core.Characters;

namespace Core.AI
{
    
    public abstract class BehaviourCondition : ScriptableObject
    {
        
        public string displayName = "NO_NAME";
        public string id;
        public string description = "NO_DESC";

        public abstract bool Evaluate(Character actor, Character targets);

    }

}

