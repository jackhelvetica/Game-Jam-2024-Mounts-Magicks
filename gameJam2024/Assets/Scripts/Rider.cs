using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class Rider : MonoBehaviour
{
    //Input System
    public RiderInput riderInput;
    private InputAction look;
    private Vector2 input;

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
    
    private void Awake()
    {
        riderInput = new RiderInput();
    }

    void Start()
    {       
    }

    void Update()
    {
        Look();
        HoldWeapon();
        Attach();
    }

    public void Attach()
    {        
        if (gameObject.CompareTag("Player1"))
        {
            GameObject markerA = GameObject.Find("MarkerA");
            transform.position = markerA.transform.position;
        }
        else if (gameObject.CompareTag("Player2"))
        {
            GameObject markerB = GameObject.Find("MarkerB");
            transform.position = markerB.transform.position;
        }
    }

    public void HoldWeapon()
    {        
        weapon.transform.parent = hand.transform;
    }

    public void Look()
    {
        input = look.ReadValue<Vector2>();

        Vector3 direction = new Vector3(input.x, 0, input.y);
        direction.Normalize();

        if (direction != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotateSpeed * Time.deltaTime);
        }

        if (input.x > 0 || input.y > 0)
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
            Debug.Log("Rider uses skill!");
            manaBarScript.UseMana();
            if (ManaBar.useMana)
            {
                riderAnimator.SetTrigger("Attack");
                KnockBack.activateKnockback = true;
                ManaBar.useMana = false;
            }                     
        }
    }

    private void OnEnable()
    {
        look = riderInput.Player.Look;
        look.Enable();
    }
    private void OnDisable()
    {
        look.Disable();
    }
}
