using UnityEngine.UI;
using UnityEngine;
using TMPro;

[System.Serializable]
public class Command
{

    public int id;
    public string displayName = "DEFAULT_NAME";
    public string displayDescription = "DEFAULT_DESCRIPTION";

    public virtual void Execute() {}

    public virtual bool IsExecutable() {
        return true;
    }

    public Command(int id) {

        this.id = id;

    }

}
