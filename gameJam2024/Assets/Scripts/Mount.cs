using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

//[RequireComponent(typeof(CharacterController))]
public class Mount : MonoBehaviour
{
    //Input System
    public MountInput mountInput;
    private InputAction move;

    //Movement
    public Rigidbody rb;
    private Vector2 input;
    public float moveSpeed = 20f;
    public float rotateSpeed = 400f;

    //Others
    public Transform spawnPointA;
    public Healthbar healthbarScript;
    //private CharacterController controller;

    private void Awake()
    {
        mountInput = new MountInput();
    }

    void Start()
    {
        //controller = gameObject.GetComponent<CharacterController>();
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
            transform.position = spawnPointA.position;
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
