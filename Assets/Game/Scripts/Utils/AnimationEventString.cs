using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationEventString : MonoBehaviour
{
    
    public System.Action<string> onEventTriggered = delegate {};

    public void OnEventTrigger( string name ) {

        onEventTriggered(name);

    }

}
