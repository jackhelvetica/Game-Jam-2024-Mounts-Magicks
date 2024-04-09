using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static bool hasFallenOff = false;
    public GameObject mount1;
    public GameObject mount2;
    public GameObject rider1;
    public GameObject rider2;

    //Respawn
    private Vector3 spawnPointA = new Vector3(-15, 3, 0);
    private Vector3 spawnPointB = new Vector3(15, 3, 0);

    //Others
    public ManaBar manaBarAScript;
    public ManaBar manaBarBScript;

    //Iframes
    public float invincibilityLength = 1f;
    private float invincibilityCounter;
    public SkinnedMeshRenderer mountRenderer;
    public SkinnedMeshRenderer riderRenderer;
    private float flashCounter;
    public float flashLength = 0.1f;
    public GameObject currentlyDyingGO;

    //Round management
    public static int roundNumber = 1;
    public GameObject scoreBoard;
    public GameObject scoreMarker;
    public static bool player1won;
    public static bool player2won;
    public List<Vector3> p1ScoreMarkerPos = new List<Vector3>();
    public List<Vector3> p2ScoreMarkerPos = new List<Vector3>();
    public int player1WinCount = 0;
    public int player2WinCount = 0;
    public Button readyButton;
    public GameObject roundOverText;
    public GameObject gameOverText;
    public CharacterHandler characterHandlerScript;
    public static GameManager instance;
    
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }
    void Update()
    {
        if (CharacterHandler.setGameManager)
        {
            mount1 = GameObject.FindWithTag("Mount1");
            mount2 = GameObject.FindWithTag("Mount2");
            rider1 = GameObject.FindWithTag("Rider1");
            rider2 = GameObject.FindWithTag("Rider2");
            
            CharacterHandler.setGameManager = false;
        }

        //Iframes
        if (invincibilityCounter > 0)
        {
            currentlyDyingGO.GetComponent<Mount>().isInvincible = true;
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if (flashCounter <= 0)
            {
                mountRenderer.enabled = !mountRenderer.enabled;
                riderRenderer.enabled = !riderRenderer.enabled;
                flashCounter = flashLength;
            }

            if (invincibilityCounter <= 0)
            {
                mountRenderer.enabled = true;
                currentlyDyingGO.GetComponent<Mount>().isInvincible = false;
            }
        }
    }

    //Respawn
    public void Respawn(GameObject mount)
    {
        if (mount.CompareTag("Mount1"))
        {
            manaBarAScript.RefillMana();
            currentlyDyingGO = mount;

            //Spawnpoint
            mount.transform.position = spawnPointA;
            mount.transform.rotation = Quaternion.Euler(0, 90, 0);

            //Iframe
            mountRenderer = mount1.GetComponentInChildren<SkinnedMeshRenderer>();
            riderRenderer = rider1.GetComponentInChildren<SkinnedMeshRenderer>();
        }
        else if (mount.CompareTag("Mount2"))
        {
            manaBarBScript.RefillMana();
            currentlyDyingGO = mount;

            //Spawnpoint
            mount.transform.position = spawnPointB;
            mount.transform.rotation = Quaternion.Euler(0, 270, 0);

            //Iframe
            mountRenderer = mount2.GetComponentInChildren<SkinnedMeshRenderer>();
            riderRenderer = rider2.GetComponentInChildren<SkinnedMeshRenderer>();
        }

        //Iframes
        if (invincibilityCounter <= 0)
        {
            invincibilityCounter = invincibilityLength;
            mountRenderer.enabled = false;
            riderRenderer.enabled = false;
            flashCounter = flashLength;
        }
    }
    
    public void BackToSpawn()
    {
        mount1.transform.position = spawnPointA;
        mount1.transform.rotation = Quaternion.Euler(0, 90, 0);
        mount2.transform.position = spawnPointB;
        mount2.transform.rotation = Quaternion.Euler(0, 270, 0);
    }

    public void NextRound()
    {
        mount1.GetComponent<PlayerInput>().DeactivateInput();
        mount2.GetComponent<PlayerInput>().DeactivateInput();
        rider1.GetComponent<PlayerInput>().DeactivateInput();
        rider2.GetComponent<PlayerInput>().DeactivateInput();

        StartCoroutine(RoundOverCountdown());

        //Add score marker        
        if (player1won)
        {
            GameObject scoreMarkerGO = Instantiate(scoreMarker, p1ScoreMarkerPos[player1WinCount - 1], Quaternion.identity);
            scoreMarkerGO.transform.SetParent(scoreBoard.transform, false);
            player1won = false;
        }
        else if (player2won)
        {
            GameObject scoreMarkerGO = Instantiate(scoreMarker, p2ScoreMarkerPos[player2WinCount - 1], Quaternion.identity);
            scoreMarkerGO.transform.SetParent(scoreBoard.transform, false);
            player2won = false;
        }

        readyButton.Select();
        roundNumber++;
    }
    IEnumerator RoundOverCountdown()
    {       
        Vector3 punchScale = new Vector3(0.5f, 0.5f, 0.5f);
        float punchDuration = 1;
        int punchVibrato = 0;
        float punchElasticity = 0;

        if (roundNumber < 3)
        {
            roundOverText.SetActive(true);
            roundOverText.transform.DOPunchScale(punchScale, punchDuration, punchVibrato, punchElasticity);
            yield return new WaitForSeconds(1.5f);
            roundOverText.SetActive(false);
        }
        else if (roundNumber == 3)
        {
            gameOverText.SetActive(true);
            gameOverText.transform.DOPunchScale(punchScale, punchDuration, punchVibrato, punchElasticity);
            yield return new WaitForSeconds(1.5f);
            gameOverText.SetActive(false);
        }        
        scoreBoard.SetActive(true);
        BackToSpawn();

    }

    public void ReadyNextRound()
    {
        if (roundNumber < 4)
        {
            scoreBoard.SetActive(false);
            StartCoroutine(characterHandlerScript.Countdown(3));
        }
        else if (roundNumber == 4)
        {
            SceneManager.LoadScene("WinScreen");           
        }
    }
}
