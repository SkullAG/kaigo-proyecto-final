using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DialogManagerInformer : MonoBehaviour
{
    public DialogSystem _ds;

    public List<Transform> transformList = new List<Transform>();

    public List<Animator> animatorList = new List<Animator>();

    public List<UnityEvent> eventList = new List<UnityEvent>();

    private void Start()
    {
        DialogManager.current.LoadInformer(this);
    }
}
