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
    private bool isReady = false;
    public static bool setUI = false;
    public static bool setGameManager = false;

    //Countdown
    public List<GameObject> countdownObjects = new List<GameObject>();
    private Image countdownObjectImage;
     
    void Start()
    {
        characterSelectScreen.SetActive(true);
        readyButton.interactable = false;

        //Spawn characters
        playerInputManager = GetComponent<PlayerInputManager>();
        playerInputManager.playerPrefab = playersList[index];
    }

    private void Update()
    {
        if (index == 4 && !isReady)
        {
            isReady = true;
            readyButton.interactable = true;
            readyButton.Select();
        }
        if (index > 4)
        {
            index = 4;
        }

        if (playerInputManager.playerCount == 4)
        {
            playerInputManager.DisableJoining();
        }
    }

    public void AddPlayer(PlayerInput player)
    {
        Debug.Log("Add player");
        if (index == 0)
        {
            player.transform.position = spawnPointA;
            player.transform.rotation = Quaternion.Euler(0, 90, 0);
            player.tag = "Mount1";
            player.DeactivateInput();

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
            player.DeactivateInput();

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
            player.DeactivateInput();

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
            player.DeactivateInput();

            playersImageList[index].SetActive(true);
            playerInputList.Add(player);

            index++;
        }
    }

    public void Ready()
    {
        Debug.Log("READY");
        characterSelectScreen.SetActive(false);
        setUI = true;
        setGameManager = true;
        StartCoroutine(Countdown(3));
    }

    public IEnumerator Countdown(int seconds)
    {
        Debug.Log("Starting countdown");
        //Tween variables
        Vector3 punchScale = new Vector3(0.5f, 0.5f, 0.5f);
        float punchDuration = 1;
        int punchVibrato = 0;
        float punchElasticity = 0;

        //Countdown
        int count = seconds; //count = 3;
        while (count > -1) //will stop once count = 0
        {
            Debug.Log("Starting in... " + count + "...");
            countdownObjects[count].SetActive(true);
            if (count > 0) //3...2...1...
            {
                countdownObjects[count].transform.DOPunchScale(punchScale, punchDuration, punchVibrato, punchElasticity);
                yield return new WaitForSeconds(1);
                countdownObjects[count].SetActive(false);
                count--;
            }
            else if (count == 0) //GO!
            {
                Debug.Log("GO!");
                countdownObjectImage = countdownObjects[count].GetComponent<Image>();
                DG.Tweening.Sequence goObjectSequence = DOTween.Sequence();
                    goObjectSequence.Append(countdownObjects[count].transform.DOPunchScale(punchScale, punchDuration, punchVibrato, punchElasticity));
                    goObjectSequence.Append(countdownObjectImage.DOFade(0, 1f));
                yield return new WaitForSeconds(2);
                countdownObjects[count].SetActive(false);
                countdownObjects[count].GetComponent<Image>().DOFade(1, 0f); //Set alpha of GO! back to 1
                count--;
            }          
        }

        //Start game
        foreach (var playerInput in playerInputList)
        {
            Debug.Log("GAME START!");
            playerInput.ActivateInput();
        }          
    }

    public void ResetPlayers()
    {
        Debug.Log("Reset Players");
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
