using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class SelectionMenu : MonoBehaviour
{
    private PlayerControllers playerControllers;

    public Image player1AvatarImage;
    public Image player2AvatarImage;
    public Image player3AvatarImage;
    public Image player4AvatarImage;

    public static bool activateRiderA = false;
    public static bool activateMountA = false;
    public static bool activateRiderB = false;    
    public static bool activateMountB = false;

    // Start is called before the first frame update
    void Start()
    {
        // Find and assign reference to PlayerControllers script
        playerControllers = FindObjectOfType<PlayerControllers>();

        // Subscribe to the OnJoinGame event
        playerControllers.OnJoinLobby += EnableAvatarImage;
        playerControllers.OnExitLobby += DisableAvatarImage;
    }

    // Update is called once per frame
    void Update()
    {
     
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnJoinGame event
        playerControllers.OnJoinLobby -= EnableAvatarImage;
        playerControllers.OnExitLobby -= DisableAvatarImage;
    }
    private void EnableAvatarImage(int controllerIndex)
    {
        // Update UI avatar image based on the controller index
        switch (controllerIndex)
        {
            case 0:
                player1AvatarImage.gameObject.SetActive(true);
                activateRiderA = true;
                break;
            case 1:
                player2AvatarImage.gameObject.SetActive(true);
                activateMountA = true;
                break;
            case 2:
                player3AvatarImage.gameObject.SetActive(true);
                activateRiderB = true;
                break;
            case 3:
                player4AvatarImage.gameObject.SetActive(true);
                activateMountB = true;
                break;
            default:
                Debug.LogWarning("Invalid controller index: " + controllerIndex);
                break;
        }
    }

    private void DisableAvatarImage(int controllerIndex)
    {
        switch (controllerIndex)
        {
            case 0:
                player1AvatarImage.gameObject.SetActive(false);
                break;
            case 1:
                player2AvatarImage.gameObject.SetActive(false);
                break;
            case 2:
                player3AvatarImage.gameObject.SetActive(false);
                break;
            case 3:
                player4AvatarImage.gameObject.SetActive(false);
                break;
            default:
                Debug.LogWarning("Invalid controller index: " + controllerIndex);
                break;
        }
    }

    public void GoToMainGame()
    {
        SceneManager.LoadScene(1);
    }
}
