using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{
    public void Jugar_al_lvl()
    {

        SceneManager.LoadScene(1, LoadSceneMode.Single);

    }
    public void Jugar_al_tutorial()
    {

        SceneManager.LoadScene(1);


    }
    public void Menu_Principal()
    {

        SceneManager.LoadScene(0);


    }
    public void Game_Over()
    {

        SceneManager.LoadScene(3);

    }
    public void Credits()
    {

        SceneManager.LoadScene(4);


    }
    public void Salir_del_juego()
    {

        Application.Quit();

    }   
}
