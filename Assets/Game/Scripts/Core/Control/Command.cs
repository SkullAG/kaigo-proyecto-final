using UnityEngine.UI;
using UnityEngine;
using TMPro;

[System.Serializable]
public abstract class Command
{

    public int id;
    public string displayName = "DEFAULT_NAME";
    public string displayDescription = "DEFAULT_DESCRIPTION";

    public abstract void Execute();

    public Command(int id) {

        this.id = id;

    }

}
