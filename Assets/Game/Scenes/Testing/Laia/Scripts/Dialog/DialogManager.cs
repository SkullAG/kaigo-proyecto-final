using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    public DialogSystem _ds;
    bool isOFF;
    //public Knightmovement _player;
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (!isOFF)// && //_player.IsGounded)
        {
            isOFF = true;
            _ds.startDialog();
        }
    }
}
