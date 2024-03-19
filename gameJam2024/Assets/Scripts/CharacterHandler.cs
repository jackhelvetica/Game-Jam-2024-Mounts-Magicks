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
    public List<GameObject> playersImageList = new List<GameObject>();
    public List<PlayerInput> playerInputList = new List<PlayerInput>();
    public GameObject characterSelectScreen;
    public Button readyButton;
    
    void Start()
    {
        characterSelectScreen.SetActive(true);
        readyButton.interactable = false;

        //Spawn characters
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

            playersImageList[index].SetActive(true);
            playerInputList.Add(player);

            index++;
            playerInputManager.playerPrefab = playersList[index];
        }
        else if (index == 1)
        {
            player.transform.position = spawnPointA;
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            player.tag = "Rider1";

            playersImageList[index].SetActive(true);
            playerInputList.Add(player);

            index++;
            playerInputManager.playerPrefab = playersList[index];
        }
        else if (index == 2)
        {            
            player.transform.position = spawnPointB;
            player.transform.rotation = Quaternion.Euler(0, 270, 0);
            player.tag = "Rider2";

            playersImageList[index].SetActive(true);
            playerInputList.Add(player);

            index++;
            playerInputManager.playerPrefab = playersList[index];
        }
        else if (index == 3)
        {
            player.transform.position = spawnPointB;
            player.transform.rotation = Quaternion.Euler(0, 270, 0);
            player.tag = "Mount2";

            playersImageList[index].SetActive(true);
            playerInputList.Add(player);
        }
        else if (index > 3)
        {
            index = 3;
        }
    }

    public void ResetPlayers()
    {
        index = 0;
        foreach (var playerImage in playersImageList)
        {
            playerImage.SetActive(false);
        }
        foreach (var playerInput in playerInputList)
        {
            Destroy(playerInput);
        }
        playerInputList.Clear();
    }
}
