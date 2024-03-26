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

    //Others
    public GameObject weapon;
    public GameObject hand;
    //public GameObject marker;

    private void Start()
    {
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
    }

    public void Attach()
    {
        if (gameObject.CompareTag("Rider1"))
        {
            GameObject marker1 = GameObject.FindWithTag("Marker1");
            transform.position = marker1.transform.position;
            //transform.parent.GetComponentInChildren<Mount>()
        }
        else if (gameObject.CompareTag("Rider2"))
        {
            GameObject marker2 = GameObject.FindWithTag("Marker2");
            if (marker2 == null)
            {
                return;
            }
            else
            {
                transform.position = marker2.transform.position;
            }                             
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
        if (context.performed)
        {
            manaBarScript.UseMana();
            if (ManaBar.useMana)
            {
                riderAnimator.SetTrigger("Attack");
                KnockBack.activateKnockback = true;
                ManaBar.useMana = false;
            }                     
        }
    }
}
