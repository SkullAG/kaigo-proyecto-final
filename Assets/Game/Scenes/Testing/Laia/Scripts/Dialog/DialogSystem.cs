using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    [SerializeField]    
    public string[] textLine;
    public TextMeshProUGUI lines;
    public float velocity;
    public GameObject UIobj;
    public GameObject enemies;
    public GameObject endsys;
    public int index;
    bool dialogOFF = true;

    // Update is called once per frame
    void Update()
    {
        if (!dialogOFF)
        {
            Time.timeScale = 0f;
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (lines.text == textLine[index])
            {
                enterDialog();
            }
            else
            {
                lines.text = textLine[index];
                StopAllCoroutines();
            }
        }
    }
    
    public void startDialog()
    {
        UIobj.SetActive(true);
        lines.text = string.Empty;
        index = 0;
        StartCoroutine(writeLines());

    }

    public void enterDialog()
    {
        if (index < textLine.Length -1)
        {
            index++;
            lines.text = string.Empty;
            StartCoroutine(writeLines());
        }
        else
        {
            UIobj.SetActive(false);
            Time.timeScale = 1f;
            enemies.SetActive(true);
            endsys.SetActive(true); ;
        }
    }

    IEnumerator writeLines()
    {
        foreach (char c in textLine[index].ToCharArray()) 
        { 
            lines.text += c; 
            yield return new WaitForSecondsRealtime(velocity); 
        }
    }
}
