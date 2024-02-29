using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mount : MonoBehaviour
{
    //Input System
    public MountInput mountInput;
    private InputAction move;

    //Movement
    public Rigidbody rb;
    private Vector2 input;
    public float moveSpeed = 20f;
    public float rotateSpeed = 200f;

    //Others
    private Vector3 spawnPointA = new Vector3(-15, 2, 0);
    private Vector3 spawnPointB = new Vector3(15, 2, 0);
    public Healthbar healthbarScript;

    private void Awake()
    {
        mountInput = new MountInput();
    }

    void Start()
    {
        if (gameObject.CompareTag("Player1"))
        {
            Debug.Log("Player 1 spawned!");
            transform.position = spawnPointA;
        }
        else if (gameObject.CompareTag("Player2"))
        {
            Debug.Log("Player 2 spawned!");
            transform.position = spawnPointB;
        }
    }
    
    void Update()
    {
    }

    private void FixedUpdate()
    {
        MoveMount();
    }
    public void MoveMount()
    {
        input = move.ReadValue<Vector2>();       

        Vector3 direction = new Vector3(input.x, 0, input.y);
        direction.Normalize();

        transform.Translate(direction * moveSpeed * Time.deltaTime, Space.World);

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
    }

    public void MountSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("Mount uses skill!");
        }       
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Death"))
        {
            Debug.Log("Player fell off");
            healthbarScript.health--;
            if (gameObject.CompareTag("Player1"))
            {
                transform.position = spawnPointA;
            }
            else if (gameObject.CompareTag("Player2"))
            {
                transform.position = spawnPointB;
            }
        }
    }

    private void OnEnable()
    {
        move = mountInput.Player.Move;
        move.Enable();
    }
    private void OnDisable()
    {
        move.Disable();
    }
}
