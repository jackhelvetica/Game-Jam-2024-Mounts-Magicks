using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class CharacterHandler : MonoBehaviour
{
    //Spawn characters
    int index = 0;
    private Vector3 spawnPointA = new Vector3(-15, 3, 0);
    private Vector3 spawnPointB = new Vector3(15, 3, 0);

    public List<GameObject> playersList = new List<GameObject>();
    PlayerInputManager playerInputManager;

    //Character select screen
    public Image characterSelectScreen;
    public Image mountA_UI;
    public Image riderA_UI;
    public Image mountB_UI;
    public Image riderB_UI;
    

    void Start()
    {
        characterSelectScreen.enabled = true;

        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.playerPrefab = playersList[index];
    }

    public void AddPlayer(PlayerInput player)
    {
        Debug.Log("Add player");
        if (index == 0)
        {
            player.transform.position = spawnPointA;
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            player.tag = "Mount1";
            mountA_UI.enabled = true;
            index++;
            playerInputManager.playerPrefab = playersList[index];
        }
        else if (index == 1)
        {
            player.transform.position = spawnPointA;
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            player.tag = "Rider1";
            riderA_UI.enabled = true;
            index++;
            playerInputManager.playerPrefab = playersList[index];
        }
        else if (index == 2)
        {
            player.transform.position = spawnPointB;
            player.transform.rotation = Quaternion.Euler(0, 270, 0);
            player.tag = "Mount2";
            mountB_UI.enabled = true;
            index++;
            playerInputManager.playerPrefab = playersList[index];
        }
        else if (index == 3)
        {
            player.transform.position = spawnPointB;
            player.transform.rotation = Quaternion.Euler(0, 270, 0);
            player.tag = "Rider2";
            riderB_UI.enabled = true;
        }
        else if (index > 3)
        {
            index = 3;
        }

    }
}
