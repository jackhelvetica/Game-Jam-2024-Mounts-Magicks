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
        SceneManager.LoadScene("Level");
        //Load story scene instead
        //In story scene, play images coroutine, then load level scene
    }
    public void Quit()
    {
        Debug.Log("Quit game");
        //Application.Quit();
    }
}
