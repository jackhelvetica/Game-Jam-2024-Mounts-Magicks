using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputHandler : MonoBehaviour
{
    public GameObject mountPrefab;
    PlayerControllerNew playerControllerNew;

    private void Awake()
    {
        if (mountPrefab != null)
        {
            playerControllerNew = mountPrefab.GetComponent<PlayerControllerNew>();
        }
    }

    void OnMove(InputAction.CallbackContext context)
    {
        playerControllerNew.OnMove(context);
    }
}
