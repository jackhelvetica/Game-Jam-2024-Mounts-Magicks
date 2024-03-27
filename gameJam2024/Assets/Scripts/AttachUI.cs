using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachUI : MonoBehaviour
{
    private Transform player1Transform;
    private Transform player2Transform;
    private GameObject player1;
    private GameObject player2;
    private Vector3 manaBarOffset = new Vector3(-150,-10,0);
    private Vector3 iconOffset = new Vector3(0, 150, 0);
    public GameObject manaBarA;
    public GameObject manaBarB;
    public GameObject player1Icon;
    public GameObject player2Icon;

    void Update()
    {
        if (CharacterHandler.gameStart)
        {
            player1 = GameObject.FindWithTag("Rider1");
            player2 = GameObject.FindWithTag("Rider2");
            player1Transform = player1.transform;
            player2Transform = player2.transform;

            CharacterHandler.gameStart = false;
        }

        if (player1Transform != null && player2Transform != null)
        {
            Vector3 screenPos1 = Camera.main.WorldToScreenPoint(player1Transform.position);
            manaBarA.transform.position = screenPos1 + manaBarOffset;
            player1Icon.transform.position = screenPos1 + iconOffset;
            Vector3 screenPos2 = Camera.main.WorldToScreenPoint(player2Transform.position);
            manaBarB.transform.position = screenPos2 - manaBarOffset + new Vector3(0,40,0);
            player2Icon.transform.position = screenPos2 + iconOffset;
        }
    }
}
