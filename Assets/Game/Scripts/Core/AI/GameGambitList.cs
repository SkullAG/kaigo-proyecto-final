using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Core.AI
{
    
    [CreateAssetMenu(fileName = "Game Gambit List", menuName = "Game/AI/Game Gambit List")]
    public class GameGambitList : ScriptableObject
    {
        
        public BehaviourCondition[] behaviourConditions;
        public TargetFilter[] targetFilters;

    }

}

