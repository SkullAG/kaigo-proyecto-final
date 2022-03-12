using UnityEngine;
using Core.Characters;

namespace Core.AI
{
    
    public abstract class BehaviourCondition : ScriptableObject
    {
        
        public abstract bool Evaluate(Character actor, Character[] targets);

    }

}

