using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mount : MonoBehaviour
{
    //Movement
    public Rigidbody rb;
    private float moveSpeed = 12f;
    private float rotateSpeed = 470f;
    private Vector2 input;

    //Others
    private Vector3 spawnPointA = new Vector3(-15, 2, 0);
    private Vector3 spawnPointB = new Vector3(15, 2, 0);
    public Healthbar healthbarScript;
    public GameManager gameManagerScript;
    public KnockBack knockbackScript;

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

        //Assign Healthbar to Mount
        Mount[] mounts = FindObjectsOfType<Mount>(); //it finds 2 mounts, assign to [0] and [1]
        int playerId = mounts.Length - 1; //playerid = 0
        //print(transform.parent.parent.gameObject.name + " " + playerId);
        Healthbar[] healthBars = FindObjectsOfType<Healthbar>(); //it finds 2 health bars, assign to [0] and [1]
        Debug.Assert(healthBars.Length == 2);
        healthbarScript = healthBars[playerId]; //assign mount to health bar
        //print(healthBars[playerId].name);
    }
    
    void Update()
    {
        //Cannot move while being knockbacked
        if (!knockbackScript.isBeingKnockedBack)
        {
            Move();
        }
    }

    public void Move()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        input = playerInput.actions["Move"].ReadValue<Vector2>();
        //print(transform.parent.parent.gameObject.name + " input " + input);
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
        //Falling off platform
        if (other.CompareTag("Death"))
        {
            Debug.Log("Player fell off");
            healthbarScript.health--;
            if (gameObject.CompareTag("Player1"))
            {
                transform.position = spawnPointA;
                transform.rotation = Quaternion.Euler(0, 90, 0);
            }
            else if (gameObject.CompareTag("Player2"))
            {
                transform.position = spawnPointB;
                transform.rotation = Quaternion.Euler(0, 270, 0);
            }
        }
    }

    public void GetKnockbacked(Vector3 hitDirection)
    {
        Vector3 upForce = new Vector3(0, 10, 0);
        knockbackScript.CallKnockback(hitDirection, upForce, input);
        Debug.Log("Getting knockbacked!");
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
    }
}
