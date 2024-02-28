using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public GameObject playerPrefab;
    PlayerControllerNew playerControllerNew;

    Vector3 startPos = new Vector3(0, 0, 0);

    private void Awake()
    {
        if (playerPrefab != null)
        {
            playerControllerNew = GameObject.Instantiate(playerPrefab, startPos, transform.rotation).GetComponent<PlayerControllerNew>();
            transform.parent = playerControllerNew.transform;
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerControllerNew.OnMove(context);
    }
}
