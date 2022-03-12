using UnityEngine;
using Core.Characters;

namespace Core.AI
{

    public abstract class TargetFilter : ScriptableObject
    {
        
        public abstract Character[] GetTargets();

    }
    
}

