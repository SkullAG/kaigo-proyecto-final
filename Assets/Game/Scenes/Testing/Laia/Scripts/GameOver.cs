using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOver : MonoBehaviour
{
    //PartyController _party;
    public GameObject canvasGameOver;
    
    // Update is called once per frame
    void Update()
    {
        if (!PartyController.current.IsPartyAlive()) 
        {
            canvasGameOver.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}
