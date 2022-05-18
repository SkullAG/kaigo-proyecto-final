using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public GameObject textObject;

    public TextMeshProUGUI speaker;
    public TextMeshProUGUI text;

    public void Write(string _text, string _speaker = "")
    {
        Debug.Log("Writing");
        textObject.SetActive(true);
        speaker.text = _speaker;
        text.text = _text;
    }

    public void Clear()
    {
        speaker.text = "";
        text.text = "";
        textObject.SetActive(false);
    }
}
