using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class SelectionMenu : MonoBehaviour
{
    private PlayerControllers playerControllers;

    public Image player1AvatarImage;
    public Image player2AvatarImage;
    public Image player3AvatarImage;
    public Image player4AvatarImage;

    // Start is called before the first frame update
    void Start()
    {
        // Find and assign reference to PlayerControllers script
        playerControllers = FindObjectOfType<PlayerControllers>();

        // Subscribe to the OnJoinGame event
        playerControllers.OnJoinGame += UpdateAvatarImage;
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnJoinGame event
        playerControllers.OnJoinGame -= UpdateAvatarImage;
    }
    private void UpdateAvatarImage(int controllerIndex)
    {
        // Update UI avatar image based on the controller index
        switch (controllerIndex)
        {
            case 0:
                player1AvatarImage.gameObject.SetActive(true);
                break;
            case 1:
                player2AvatarImage.gameObject.SetActive(true);
                break;
            case 2:
                player3AvatarImage.gameObject.SetActive(true);
                break;
            case 3:
                player4AvatarImage.gameObject.SetActive(true);
                break;
            default:
                Debug.LogWarning("Invalid controller index: " + controllerIndex);
                break;
        }
    }
}
