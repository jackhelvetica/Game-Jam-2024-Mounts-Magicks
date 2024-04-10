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
    public Animator mountAnimator;

    //Others
    public Healthbar healthbarScript;
    private GameManager gameManagerScript;
    private Vector3 spawnPointA = new Vector3(-15, 3, 0);
    private Vector3 spawnPointB = new Vector3(15, 3, 0);

    public bool isInvincible = false;

    //Colour
    public Material redMat;
    public Material blueMat;
    public GameObject mountMesh;

    //Knockback
    public KnockBack knockbackScript;
    private int radius = 5;
    public TrailRenderer tr;

    public float detectKnockbackCounter = 0f;
    public Material blueGlowMat;
    public Material redGlowMat;

    private bool falling = false;

    //Dash
    private float dashTime = 0.05f;
    private float dashSpeed = 35f;
    private float dashCooldown = 1.3f;
    private bool startDashCooldown = false;
    private bool isDashing = false;

    void Start()
    {
        gameManagerScript = FindObjectOfType<GameManager>();

        //Assign Healthbar to Mount
        Mount[] mounts = FindObjectsOfType<Mount>(); //it finds 2 mounts, assign to [0] and [1]
        int playerId = mounts.Length - 1; //playerid = 0
        //print(transform.parent.parent.gameObject.name + " " + playerId);
        Healthbar[] healthBars = FindObjectsOfType<Healthbar>(); //it finds 2 health bars, assign to [0] and [1]
        Debug.Assert(healthBars.Length == 2);
        healthbarScript = healthBars[playerId]; //assign mount to health bar
        //print(healthBars[playerId].name);

        //Set marker and material colour
        if (gameObject.CompareTag("Mount1"))
        {
            transform.position = spawnPointA;
            GameObject marker = transform.Find("Marker").gameObject;
            marker.tag = "Marker1";     
        }
        else if (gameObject.CompareTag("Mount2"))
        {
            //Marker
            transform.position = spawnPointB;
            GameObject marker = transform.Find("Marker").gameObject;
            marker.tag = "Marker2";

            //Colour
            mountMesh.GetComponent<Renderer>().material = blueMat;
        }
    }
    
    void Update()
    {
        //Cannot move while being knockbacked
        if (!knockbackScript.isBeingKnockedBack) //Not being knockbacked
        {
            Move();
            
        }
        else //Being knockbacked
        {
            mountAnimator.SetBool("Move", false);            
        }

        if (IsGrounded()) //On ground
        {
            falling = false;
        }
        else //In air
        {
            falling = true;
        }

        //Dash cooldown
        if (startDashCooldown)
        {
            dashCooldown -= Time.deltaTime;
            if (dashCooldown < 0)
            {
                startDashCooldown = false;
                dashCooldown = 2f;
            }
        }

        Fall();
        MountGlow();
        EnableNextRound();
    }

    void FixedUpdate()
    {
        
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
            mountAnimator.SetBool("Move", true);

            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }
        else
        {
            mountAnimator.SetBool("Move", false);
        }
    }

    public void MountSkill(InputAction.CallbackContext context)
    {
        if (context.performed && !startDashCooldown)
        {
            StartCoroutine(Dash());
        }       
    }

    IEnumerator Dash()
    {
        isDashing = true;

        FindObjectOfType<AudioManagerScript>().Play("Dash");
        startDashCooldown = true;       

        detectKnockbackCounter += Time.deltaTime;
        float startTime = Time.time;
        while (Time.time < startTime + dashTime)
        {
            rb.velocity = transform.forward * dashSpeed;
            //rb.AddForce(transform.forward * dashSpeed, ForceMode.Force);

            tr.emitting = true;

            yield return null;
        }
        
        yield return new WaitForSeconds(0.7f);       
        tr.emitting = false;
        detectKnockbackCounter = 0;       
    }

    private void OnTriggerEnter(Collider other)
    {       
        //Falling off platform
        if (other.CompareTag("Death"))
        {
            FindObjectOfType<AudioManagerScript>().Play("Crowd Cheering");
            tr.emitting = false;
            healthbarScript.health--;

            if (gameObject.CompareTag("Mount1"))
            {
                gameManagerScript.Respawn(gameObject);
            }
            else if (gameObject.CompareTag("Mount2"))
            {
                gameManagerScript.Respawn(gameObject);
            }           
        }
    }

    public void MountGlow()
    {
        if (detectKnockbackCounter > 0)
        {
            if (gameObject.CompareTag("Mount1"))
            {
                mountMesh.GetComponent<Renderer>().material = redGlowMat;
            }
            else if (gameObject.CompareTag("Mount2"))
            {
                mountMesh.GetComponent<Renderer>().material = blueGlowMat;
            }
        }
        else
        {
            if (gameObject.CompareTag("Mount1"))
            {
                mountMesh.GetComponent<Renderer>().material = redMat;
            }
            else if (gameObject.CompareTag("Mount2"))
            {
                mountMesh.GetComponent<Renderer>().material = blueMat;
            }
        }
    }

    public void GetCriticalKnockbacked(Vector3 knockbackDirection)
    {
        tr.emitting = true;

        Vector3 upForce = new Vector3(0, 10, 0);
        knockbackScript.CallCriticalKnockback(knockbackDirection, upForce, input);
        Debug.Log("Critical hit!");
        
    }

    public void GetNormalKnockbacked(Vector3 knockbackDirection)
    {
        Vector3 upForce = new Vector3(0, 1, 0);

        knockbackScript.CallNormalKnockback(knockbackDirection, upForce, input);
        Debug.Log("Normal hit!");
    }

    public void Fall()
    {
        GameObject rider1 = GameObject.FindGameObjectWithTag("Rider1");
        GameObject rider2 = GameObject.FindGameObjectWithTag("Rider2");

        if (rider1 == null || rider2 == null)
        {
            return;
        }

        if (falling)
        {
            if (gameObject.CompareTag("Mount1"))
            {
                rider1.GetComponent<Animator>().SetBool("Fall", true);                
            }
            else if (gameObject.CompareTag("Mount2"))
            {
                rider2.GetComponent<Animator>().SetBool("Fall", true);
            }        
        }
        else
        {            
            if (gameObject.CompareTag("Mount1"))
            {
                rider1.GetComponent<Animator>().SetBool("Fall", false);
            }
            else if (gameObject.CompareTag("Mount2"))
            {
                rider2.GetComponent<Animator>().SetBool("Fall", false);
            }
        }        
    }

    public void EnableNextRound()
    {
        if (healthbarScript.health == 0) //if someone dies
        {
            //Next round
            if (gameObject.CompareTag("Mount1")) //mount1 died
            {
                GameManager.player2won = true;
                gameManagerScript.player2WinCount++;                
            }
            else if (gameObject.CompareTag("Mount2"))
            {
                GameManager.player1won = true;
                gameManagerScript.player1WinCount++;
            }
            gameManagerScript.NextRound();
            healthbarScript.health += 3;
        }        
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(transform.position, radius);
    }

    public bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 0.95f);
    }
}
