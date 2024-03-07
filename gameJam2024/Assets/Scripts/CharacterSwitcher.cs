using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitcher : MonoBehaviour
{
    int index = 0;
    [SerializeField] List<GameObject> playersList = new List<GameObject>();
    PlayerInputManager playerInputManager;

    private Vector3 spawnPointA = new Vector3(-15, 2, 0);
    private Vector3 spawnPointB = new Vector3(15, 2, 0);
    public GameManager gameManagerScript;

    private void Start()
    {
        //playerInputManager = GetComponent<PlayerInputManager>();
        //playerInputManager.playerPrefab = playersList[index];
    }

    public void SwitchNextSpawnCharacter(PlayerInput input)
    {
        Debug.Log("Index = " + index);
        index++;
        if (index >= 1)
        {
            index = 1;
        }
        playerInputManager.playerPrefab = playersList[index];
    }

    public void AddPlayer(PlayerInput player)
    {
        if (index == 0)
        {
            Debug.Log("Player 1 joined");
            player.transform.position = spawnPointA;
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            player.tag = "Player1";
            index++;
        }
        else if (index == 1)
        {
            Debug.Log("Player 1 joined");
            player.transform.position = spawnPointB;
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            player.tag = "Player1";
            index++;
        }
        else if (index == 2)
        {
            Debug.Log("Player 2 joined");
            player.transform.position = spawnPointB;
            player.transform.rotation = Quaternion.Euler(0, 270, 0);
            player.tag = "Player2";
            index++;
        }
        else if (index == 3)
        {
            Debug.Log("Player 2 joined");
            player.transform.position = spawnPointB;
            player.transform.rotation = Quaternion.Euler(0, 270, 0);
            player.tag = "Player2";
        }

    }
}
