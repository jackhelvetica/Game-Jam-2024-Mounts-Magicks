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

    public InputAction join;

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
        join = playerController.Player.JoinGame;
        join.Enable();
        join.performed += JoinGame;
    }
    void OnDisable()
    {
        join.Disable();
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

    }
    
    public void JoinGame(InputAction.CallbackContext context)
    {

        string controllerName = context.control.device.name;

        int controllerIndex = controllersManager.GetControllerIndex(controllerName);

        // Check if the action triggered is the Cross button
        if (context.performed) //if button is pressed
        {
            if (controllerIndex >= 0 && controllerIndex < controllersManager.activePS4Controllers.Count)
            {
                Debug.Log("Cross button pressed, the controller index is" + controllerIndex);
            }
            else
            {
                Debug.LogWarning("Invalid controller index: " + controllerIndex);
            }
        }
    }

}
