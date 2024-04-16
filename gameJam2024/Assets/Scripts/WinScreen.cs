using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class WinScreen : MonoBehaviour
{
    public GameManager gameManagerScript;
    public GameObject mountWin;
    public GameObject riderWin;
    public GameObject sword;
    public Material mountBlueMat;
    public Material riderBlueMat;
    public Material swordBlueMat;
    public TextMeshProUGUI winningPlayerText;
    public Button returnButton;
    private bool playWinAnim = false;

    // Start is called before the first frame update
    void Start()
    {
        returnButton.Select();
        returnButton.interactable = true;
        StartCoroutine(WinMusic());
        gameManagerScript = FindObjectOfType<GameManager>();
        playWinAnim = true;
        
        if (gameManagerScript.player2WinCount > gameManagerScript.player1WinCount) //P1 = red, P2 = blue
        {            
            mountWin.GetComponentInChildren<SkinnedMeshRenderer>().material = mountBlueMat;
            riderWin.GetComponentInChildren<SkinnedMeshRenderer>().material = riderBlueMat;
            sword.GetComponent<Renderer>().material = swordBlueMat;

            winningPlayerText.text = "Team B";
        }
        else if (gameManagerScript.player1WinCount > gameManagerScript.player2WinCount)
        {
            winningPlayerText.text = "Team A";
        }       
    }

    private void Update()
    {
        if (playWinAnim)
        {
            Debug.Log("Play win animation");
            riderWin.GetComponent<Animator>().SetBool("Win", true);
            playWinAnim = false;
        }     
    }

    public void ReturnToMainMenu()
    {
        riderWin.GetComponent<Animator>().SetBool("Win", false);

        SceneManager.LoadScene("MainMenu");

        //Reset variables
        FindObjectOfType<AudioManagerScript>().Stop("Win Music");
        FindObjectOfType<AudioManagerScript>().Stop("Win SFX");
        GameManager.roundNumber = 1;
        gameManagerScript.player1WinCount = 0;
        gameManagerScript.player2WinCount = 0;
    }

    IEnumerator WinMusic()
    {
        FindObjectOfType<AudioManagerScript>().Play("Win SFX");
        yield return new WaitForSeconds(8);
        FindObjectOfType<AudioManagerScript>().Play("Win Music");       
    }
}
