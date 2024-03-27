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

    private Vector3 spawnPointA = new Vector3(-15, 3, 0);
    private Vector3 spawnPointB = new Vector3(15, 3, 0);

    //Iframes
    public float invincibilityLength = 1f;
    private float invincibilityCounter;
    public SkinnedMeshRenderer mountRenderer;
    public SkinnedMeshRenderer riderRenderer;
    private float flashCounter;
    public float flashLength = 0.1f;

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
            }
        }
    }

    //Respawn
    public void Respawn(GameObject mount)
    {
        if (mount.CompareTag("Mount1"))
        {
            mount.transform.position = spawnPointA;
            mount.transform.rotation = Quaternion.Euler(0, 90, 0);
            mountRenderer = mount1.GetComponentInChildren<SkinnedMeshRenderer>();
            riderRenderer = rider1.GetComponentInChildren<SkinnedMeshRenderer>();
        }
        else if (mount.CompareTag("Mount2"))
        {
            mount.transform.position = spawnPointB;
            mount.transform.rotation = Quaternion.Euler(0, 270, 0);
            mountRenderer = mount2.GetComponentInChildren<SkinnedMeshRenderer>();
            riderRenderer = rider2.GetComponentInChildren<SkinnedMeshRenderer>();
        }

        if (invincibilityCounter <= 0)
        {
            invincibilityCounter = invincibilityLength;
            mountRenderer.enabled = false;
            riderRenderer.enabled = false;
            flashCounter = flashLength;
        }
    }

    //Iframes
}
