using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using DG.Tweening;

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

    //Countdown
    public List<GameObject> countdownObjects = new List<GameObject>();
    
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
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

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
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

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
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

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
            player.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

            playersImageList[index].SetActive(true);
            playerInputList.Add(player);

            index++;
        }
        else if (index == 4)
        {
            readyButton.interactable = true;
            readyButton.Select();
        }
    }

    public void Ready()
    {
        characterSelectScreen.SetActive(false);
        StartCoroutine(Countdown(3));
    }

    public IEnumerator Countdown(int seconds)
    {
        //Tween variables
        Vector3 punchScale = new Vector3(0.5f, 0.5f, 0.5f);
        float punchDuration = 1;
        int punchVibrato = 0;
        float punchElasticity = 0;

        int count = seconds; //count = 3;
        while (count > -1) //will stop once count = 0
        {
            countdownObjects[count].SetActive(true);
            countdownObjects[count].transform.DOPunchScale(punchScale, punchDuration, punchVibrato, punchElasticity);
            yield return new WaitForSeconds(1);
            countdownObjects[count].SetActive(false);
            count--;
        }

        //countdownObjects[count].spriteRenderer.DOFade(0, 1);

        //Start game
        //foreach (var playerInput in playerInputList)
        //{
        //    //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.None;
        //    //Freeze rotation x and z
        //}     
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
