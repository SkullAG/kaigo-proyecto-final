using UnityEngine;
using Core.Characters;

namespace Core.States {

    public abstract class Effect : ScriptableObject
    {
        
        public abstract void Apply(Character actor);

    }

}

