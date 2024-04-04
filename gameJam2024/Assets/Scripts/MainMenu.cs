using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    void Start()
    {
        
    }

    public void Play()
    {
        SceneManager.LoadScene("CharacterSelect");
    }
    public void Quit()
    {
        Debug.Log("Quit game");
        //Application.Quit();
    }
}
