using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.InputSystem;

public class Mount : MonoBehaviour
{
    //Movement
    public Rigidbody rb;
    public float moveSpeed = 20f;
    public float rotateSpeed = 200f;

    //Others
    private Vector3 spawnPointA = new Vector3(-15, 2, 0);
    private Vector3 spawnPointB = new Vector3(15, 2, 0);
    public Healthbar healthbarScript;

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

        Mount[] mounts = FindObjectsOfType<Mount>();
        int playerId = mounts.Length - 1;
        print(transform.parent.parent.gameObject.name + " " +playerId);
        Healthbar[] healthBars = FindObjectsOfType<Healthbar>();
        Debug.Assert(healthBars.Length == 2);
        healthbarScript = healthBars[playerId];
    }
    
    void Update()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
        print(transform.parent.parent.gameObject.name + " input " + input);
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
}
