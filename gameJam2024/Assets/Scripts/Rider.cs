using System.Collections;
using System.Collections.Generic;
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

    //Others
    public GameObject marker;
    

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
        Attach();
    }

    public void Attach()
    {
        transform.position = marker.transform.position;
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
    }

    public void RiderSkill(InputAction.CallbackContext context)
    {
        if (context.performed)
        {            
            manaBarScript.UseMana();
            if (ManaBar.useMana)
            {
                riderAnimator.SetTrigger("Swing");
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
