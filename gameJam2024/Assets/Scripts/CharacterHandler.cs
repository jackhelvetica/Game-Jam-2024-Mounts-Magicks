using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterHandler : MonoBehaviour
{
    //Spawn characters
    int index = 0;
    private Vector3 spawnPointA = new Vector3(-15, 2, 0);
    private Vector3 spawnPointB = new Vector3(15, 2, 0);

    //Character select screen
    public Image characterSelectScreen;
    public Image mountA_UI;
    public Image riderA_UI;
    public Image mountB_UI;
    public Image riderB_UI;
    

    void Start()
    {
        characterSelectScreen.enabled = true;
        Debug.Log("Create your team");
    }

    public void AddPlayer(PlayerInput player)
    {
        Debug.Log("Add player");
        if (index == 0)
        {
            player.transform.position = spawnPointA;
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            player.tag = "Player1";
            mountA_UI.enabled = true;
            index++;
        }
        else if (index == 1)
        {
            player.transform.position = spawnPointB;
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            player.tag = "Player1";
            riderA_UI.enabled = true;
            index++;
        }
        else if (index == 2)
        {
            player.transform.position = spawnPointB;
            player.transform.rotation = Quaternion.Euler(0, 270, 0);
            player.tag = "Player2";
            mountB_UI.enabled = true;
            index++;
        }
        else if (index == 3)
        {
            player.transform.position = spawnPointB;
            player.transform.rotation = Quaternion.Euler(0, 270, 0);
            player.tag = "Player2";
            riderB_UI.enabled = true;
        }

    }
}
