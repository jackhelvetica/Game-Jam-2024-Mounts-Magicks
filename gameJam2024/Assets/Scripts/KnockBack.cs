using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class KnockBack : MonoBehaviour
{  
    public static bool activateKnockback;

    public float c_knockbackTime = 0.4f;
    public float c_hitDirectionForce = 22f; //Knockback in the hit direction
    public float c_constForce = 3f; //An upwards force
    public float c_inputForce = 2f; //Player's input affect knockback

    public float n_knockbackTime = 0.4f;
    public float n_hitDirectionForce = 22f; 
    public float n_constForce = 1f; 
    public float n_inputForce = 2f; 

    private Rigidbody rb;
    private Coroutine criticalKnockbackCoroutine;
    private Coroutine normalKnockbackCoroutine;


    void Start()
    {
        activateKnockback = false;
        rb = GetComponent<Rigidbody>();
    }

    public bool isBeingKnockedBack { get; private set; }

    public IEnumerator CriticalKnockbackAction(Vector3 hitDirection, Vector3 constantForceDirection, Vector3 inputDirection)
    {
        isBeingKnockedBack = true;

        Vector3 hitForce;
        Vector3 constantForce;
        Vector3 knockbackForce;
        Vector3 combinedForce;

        hitForce = hitDirection * c_hitDirectionForce;
        constantForce = constantForceDirection * c_constForce;

        float elapsedTime = 0f;
        while(elapsedTime < c_knockbackTime)
        {
            //iterate the timer
            elapsedTime += Time.fixedDeltaTime;

            //combine hitForce and constantForce
            knockbackForce = hitForce + constantForce;

            if (inputDirection != Vector3.zero)
            {
                combinedForce = knockbackForce + inputDirection;
            }
            else
            {
                combinedForce = knockbackForce;
            }

            rb.velocity = combinedForce;

            yield return new WaitForFixedUpdate();
        }     

        //Combine hitForce and constantForce
        rb.velocity = hitForce + constantForce + (inputDirection * c_inputForce);

        isBeingKnockedBack = false;
    }

    public void CallCriticalKnockback(Vector3 hitDirection, Vector3 constantForceDirection, Vector3 inputDirection)
    {
        criticalKnockbackCoroutine = StartCoroutine(CriticalKnockbackAction(hitDirection, constantForceDirection, inputDirection));
    }

    public IEnumerator NormalKnockbackAction(Vector3 hitDirection, Vector3 constantForceDirection, Vector3 inputDirection)
    {
        isBeingKnockedBack = true;

        Vector3 hitForce;
        Vector3 constantForce;
        Vector3 knockbackForce;
        Vector3 combinedForce;

        hitForce = hitDirection * n_hitDirectionForce;
        constantForce = constantForceDirection * n_constForce;

        float elapsedTime = 0f;
        while (elapsedTime < n_knockbackTime)
        {
            //iterate the timer
            elapsedTime += Time.fixedDeltaTime;

            //combine hitForce and constantForce
            knockbackForce = hitForce + constantForce;

            if (inputDirection != Vector3.zero)
            {
                combinedForce = knockbackForce + inputDirection;
            }
            else
            {
                combinedForce = knockbackForce;
            }

            rb.velocity = combinedForce;

            yield return new WaitForFixedUpdate();
        }
       
        //Combine hitForce and constantForce
        rb.velocity = hitForce + constantForce + (inputDirection * n_inputForce);
       
        isBeingKnockedBack = false;        
    }

    public void CallNormalKnockback(Vector3 hitDirection, Vector3 constantForceDirection, Vector3 inputDirection)
    {
        normalKnockbackCoroutine = StartCoroutine(NormalKnockbackAction(hitDirection, constantForceDirection, inputDirection));
    }
}
