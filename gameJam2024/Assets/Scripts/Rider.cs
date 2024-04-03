using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rider : MonoBehaviour
{
    //Look
    public float rotateSpeed = 400f;

    //Skill
    public ManaBar manaBarScript;
    public Animator riderAnimator;
    public GameObject attackZone;

    //Colour
    public Material redMat;
    public Material blueMat;
    public GameObject riderMesh;
    public Material blueGlowMat;
    public Material redGlowMat;

    //Others
    public GameObject weapon;
    public GameObject hand;
    public Sword sword;
    public Mount mountScript;

    private void Start()
    {
        sword = GetComponentInChildren<Sword>();

        //Assign Manabar to Rider
        Rider[] riders = FindObjectsOfType<Rider>();
        int playerId = riders.Length - 1;
        //print(transform.gameObject.name + " " + playerId);
        ManaBar[] manaBars = FindObjectsOfType<ManaBar>();
        Debug.Assert(manaBars.Length == 2);
        manaBarScript = manaBars[playerId];
        //print(manaBars[playerId].name); 
    }
    void Update()
    {
        Look();
        HoldWeapon();
        Attach();

        RiderGlow();

        if (mountScript != null)
        {
            if (mountScript.detectKnockbackCounter == 0)
            {
                sword.detectKnockback = false;
            }
        }
        
    }

    public void Attach()
    {
        if (gameObject.CompareTag("Rider1"))
        {
            GameObject marker1 = GameObject.FindWithTag("Marker1");
            transform.position = marker1.transform.position;

            GameObject mount1 = GameObject.FindWithTag("Mount1");
            mountScript = mount1.GetComponent<Mount>();

            //transform.parent.GetComponentInChildren<Mount>()
        }
        else if (gameObject.CompareTag("Rider2"))
        {
            //Marker
            GameObject marker2 = GameObject.FindWithTag("Marker2");
            if (marker2 == null) //rider is spawned before mount so we need this
            {
                return;
            }
            else
            {
                transform.position = marker2.transform.position;
                GameObject mount2 = GameObject.FindWithTag("Mount2");
                mountScript = mount2.GetComponent<Mount>();
            }

            //Colour
            riderMesh.GetComponent<Renderer>().material = blueMat;
        }       
    }

    public void HoldWeapon()
    {        
        weapon.transform.parent = hand.transform;
    }

    public void Look()
    {
        PlayerInput playerInput = GetComponent<PlayerInput>();
        Vector2 input = playerInput.actions["Look"].ReadValue<Vector2>();

        Vector3 direction = new Vector3(input.x, 0, input.y);
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }

        if (input != Vector2.zero)
        {
            attackZone.SetActive(true);
        }
        else
        {
            attackZone.SetActive(false);
        }
    }

    public void RiderSkill(InputAction.CallbackContext context)
    {
        if (context.performed && mountScript.detectKnockbackCounter == 0)
        {
            FindObjectOfType<AudioManagerScript>().Play("Whoosh");
            sword.detectKnockback = false;
            
            manaBarScript.UseMana();
            if (ManaBar.useMana)
            {
                riderAnimator.SetTrigger("Attack");
                KnockBack.activateKnockback = true;
                ManaBar.useMana = false;
            }            
        }
        else if (context.performed && mountScript.detectKnockbackCounter > 0)
        {
            FindObjectOfType<AudioManagerScript>().Play("Whoosh");
            sword.detectKnockback = true;
            
            manaBarScript.UseMana();
            if (ManaBar.useMana)
            {
                riderAnimator.SetTrigger("Attack");
                KnockBack.activateKnockback = true;
                ManaBar.useMana = false;
            }
        }
    }

    public void RiderGlow()
    {
        if (sword.detectKnockback)
        {
            if (gameObject.CompareTag("Rider1"))
            {
                riderMesh.GetComponent<Renderer>().material = redGlowMat;
            }
            else if (gameObject.CompareTag("Rider2"))
            {
                riderMesh.GetComponent<Renderer>().material = blueGlowMat;
            }
        }
        else
        {
            if (gameObject.CompareTag("Rider1"))
            {
                riderMesh.GetComponent<Renderer>().material = redMat;
            }
            else if (gameObject.CompareTag("Rider2"))
            {
                riderMesh.GetComponent<Renderer>().material = blueMat;
            }
        }
    }
}
