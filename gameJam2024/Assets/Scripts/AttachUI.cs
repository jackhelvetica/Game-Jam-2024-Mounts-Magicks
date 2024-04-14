using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttachUI : MonoBehaviour
{
    private Transform player1Transform;
    private Transform player2Transform;
    private GameObject player1;
    private GameObject player2;
    private Mount mount1Script;
    private Mount mount2Script;

    public GameObject manaBarA;
    public GameObject manaBarB;
    private Vector3 manaBarOffset = new Vector3(-150,-10,0);
    
    public GameObject player1Icon;
    public GameObject player2Icon;
    private Vector3 iconOffset = new Vector3(0, 140, 0);

    public GameObject critIcon1;
    public GameObject critIcon2;
    private Vector3 critIconOffset = new Vector3(0, 220, 0);

    void Update()
    {
        if (CharacterHandler.setUI)
        {
            player1 = GameObject.FindWithTag("Rider1");
            player2 = GameObject.FindWithTag("Rider2");
            player1Transform = player1.transform;
            player2Transform = player2.transform;
            mount1Script = GameObject.FindWithTag("Mount1").GetComponent<Mount>();
            mount2Script = GameObject.FindWithTag("Mount2").GetComponent<Mount>();

            CharacterHandler.setUI = false;
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

        if (mount1Script != null && mount2Script != null) 
        {
            if (mount1Script.enableCritIcon || mount2Script.enableCritIcon)
            {
                Debug.Log("In Crit Zone");
                critIcon1.SetActive(true);
                critIcon2.SetActive(true);
                Vector3 screenPos1 = Camera.main.WorldToScreenPoint(player1Transform.position);
                critIcon1.transform.position = screenPos1 + critIconOffset;
                Vector3 screenPos2 = Camera.main.WorldToScreenPoint(player2Transform.position);
                critIcon2.transform.position = screenPos2 + critIconOffset;
            }
            else
            {
                critIcon1.SetActive(false);
                critIcon2.SetActive(false);
            }
        }
        else
        {
            return;
        }
        
    }
}
