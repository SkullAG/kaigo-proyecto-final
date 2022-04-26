using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IListableElement
{

    // This interface is used to recognize elements (as instances of anything) 
    // that can be listed as buttons by the ButtonList class.

    public string displayName { get; }
    public string displayDescription { get; }

}
