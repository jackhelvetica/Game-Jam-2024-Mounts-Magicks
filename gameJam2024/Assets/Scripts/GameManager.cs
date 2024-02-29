using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public GameObject riderA;
    public GameObject mountA;
    public GameObject riderB;
    public GameObject mountB;

    void Start()
    {
    }

    void Update()
    {
        
    }

    public void SwitchNextSpawnCharacter()
    {

    }

    public void SpawnPlayers()
    {
        if (SelectionMenu.activateRiderA)
        {
            riderA.SetActive(true);
        }
        if (SelectionMenu.activateMountA)
        {
            mountA.SetActive(true);
        }
        if (SelectionMenu.activateRiderB)
        {
            riderB.SetActive(true);
        }
        if (SelectionMenu.activateRiderB)
        {
            mountB.SetActive(true);
        }
    }
}
