using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public bool isInvincible = false;

    //Round management
    public static int roundNumber = 1;

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
            isInvincible = true;
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
                isInvincible = false;
            }
        }

        if (isInvincible)
        {
            KnockBack.activateKnockback = false;
        }
        else
        {
            KnockBack.activateKnockback = true;
        }
    }

    //Respawn
    public void Respawn(GameObject mount)
    {
        if (mount.CompareTag("Mount1"))
        {
            manaBarAScript.RefillMana();

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

    public void NextRound()
    {
        roundNumber++;
        //"Round over!" text appear
        //Make scoreboard appear
        //Add a mark on the winner
        //Select "next round" button
        //Close scoreboard
    }
    public void EndGame()
    {
        //Happens when round = 4
        //Go to win scene
        //In win scene, instantiate prefab of winner and play animation on loop. Add "Player X wins!"
    }
}
