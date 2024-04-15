using System.Collections;
using System.Collections.Generic;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(WinMusic());
        gameManagerScript = FindObjectOfType<GameManager>();

        if (gameManagerScript.player2WinCount > gameManagerScript.player1WinCount) //P1 = red, P2 = blue
        {
            riderWin.GetComponent<Animator>().SetBool("Win", true);

            mountWin.GetComponentInChildren<SkinnedMeshRenderer>().material = mountBlueMat;
            riderWin.GetComponentInChildren<SkinnedMeshRenderer>().material = riderBlueMat;
            sword.GetComponent<Renderer>().material = swordBlueMat;

            winningPlayerText.text = "Player 2";
        }
        else if (gameManagerScript.player1WinCount > gameManagerScript.player2WinCount)
        {
            winningPlayerText.text = "Player 1";
        }
    }

    public void ReturnToMainMenu()
    {
        riderWin.GetComponent<Animator>().SetBool("Win", false);

        SceneManager.LoadScene("MainMenu");

        //Reset variables
        FindObjectOfType<AudioManagerScript>().Stop("Win Music");
        FindObjectOfType<AudioManagerScript>().Stop("Win SFX");
        FindObjectOfType<AudioManagerScript>().Play("Main Menu");
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
