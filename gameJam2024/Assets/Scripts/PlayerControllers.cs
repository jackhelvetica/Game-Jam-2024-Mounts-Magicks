//using System.Collections;
//using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerControllers : MonoBehaviour
{
    [SerializeField]
    private float playerSpeed = 2.0f;
    [SerializeField]
    private float jumpHeight = 1.0f;
    [SerializeField]
    private float gravityValue = -9.81f;

    public CharacterController controller;
    public PlayerControls playerController;
    private Vector3 playerVelocity;
    private bool groundedPlayer;

    public InputAction joinLobby;
    public InputAction exitLobby;
    public InputAction isReady;

    // Define a delegate for the event
    public delegate void JoinGameEventHandler(int controllerIndex);

    // Define the event
    public event JoinGameEventHandler OnJoinLobby;
    public event JoinGameEventHandler OnExitLobby;

    public ControllersManager controllersManager;

    private void Awake()
    {
        playerController = new PlayerControls();
    }

    private void Start()
    {
        controller = gameObject.GetComponent<CharacterController>();
    }

    void OnEnable()
    {
        joinLobby = playerController.Player.JoinLobby;
        joinLobby.Enable();

        exitLobby = playerController.Player.ExitLobby;
        exitLobby.Enable();

        isReady = playerController.Player.IsReady;
        isReady.Enable();

    }
    void OnDisable()
    {
        joinLobby.Disable();

        exitLobby.Disable();

        isReady.Disable();
    }


    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        controller.Move(move * Time.deltaTime * playerSpeed);

        if (move != Vector3.zero)
        {
            gameObject.transform.forward = move;
        }

        // Changes the height position of the player..
        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        joinLobby.performed += JoinLobby;
        exitLobby.performed += ExitLobby;
        isReady.performed += IsReady;

    }
    
    public void JoinLobby(InputAction.CallbackContext context)
    {

        string controllerName = context.control.device.name;

        int controllerIndex = controllersManager.GetControllerIndex(controllerName);

        if (context.performed) //if button is pressed
        {
            if (controllerIndex >= 0 && controllerIndex < controllersManager.activePS4Controllers.Count)
            {
                Debug.Log("L1 and R1 are pressed, the controller index is " + controllerIndex);
                
                // Check if the event is not null before invoking it
                if (OnJoinLobby != null)
                {
                    OnJoinLobby(controllerIndex);
                }
            }
            else
            {
                Debug.LogWarning("Invalid controller index: " + controllerIndex);
            }
        }
    }

    public void ExitLobby(InputAction.CallbackContext context)
    {
        string controllerName = context.control.device.name;

        int controllerIndex = controllersManager.GetControllerIndex(controllerName);

        if (context.performed)
        {
            if (controllerIndex >= 0 && controllerIndex < controllersManager.activePS4Controllers.Count)
            {
                Debug.Log("Options button is pressed, the controller index is " + controllerIndex);

                controllersManager.RemoveController(controllerName);
                // Check if the event is not null before invoking it
                if (OnExitLobby != null)
                {
                    OnExitLobby(controllerIndex);
                }
                else
                {
                    Debug.LogWarning("Invalid controller index: " + controllerIndex);
                }
            }
        }
    }
    
    public void IsReady(InputAction.CallbackContext context)
    {
        if (context.performed)
        {

        }
    }

}
