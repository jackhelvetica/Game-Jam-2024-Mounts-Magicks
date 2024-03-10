using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private float knockbackForce = 10f;
    private float upwardForce = 10f;
    
    public static bool activateKnockback;
    public float knockbackTime = 0.2f;
    public float hitDirectionForce = 10f; //Knockback in the hit direction
    public float constForce = 5f; //An upwards force
    public float inputForce = 7.5f; //Player's input affect knockback
    private Rigidbody rb;
    private Coroutine knockbackCoroutine;

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

        hitForce = hitDirection * hitDirectionForce;
        constantForce = constantForceDirection * constForce;

        float elapsedTime = 0f;
        while(elapsedTime < knockbackTime)
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
        rb.velocity = hitForce + constantForce + (inputDirection * inputForce);

        isBeingKnockedBack = false;
    }

    public void CallCriticalKnockback(Vector3 hitDirection, Vector3 constantForceDirection, Vector3 inputDirection)
    {
        knockbackCoroutine = StartCoroutine(CriticalKnockbackAction(hitDirection, constantForceDirection, inputDirection));
    }

    public void NormalKnockback(Vector3 knockbackDirection)
    {
        float hitForce = 100f;
        rb.AddForce(knockbackDirection * hitForce);
    }

    //private void OnTriggerEnter(Collider other)
    //{
    //    Rigidbody opponentRb = other.GetComponent<Rigidbody>();
    //    Rider rider = other.GetComponent<Rider>();

    //    if (opponentRb != null)
    //    {
    //        Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
    //        rider.GetKnockbacked(knockbackDirection);
    //    }

    //    if (activateKnockback)
    //    {
    //        Rigidbody opponentRb = other.GetComponent<Rigidbody>();

    //        if (opponentRb != null)
    //        {
    //            //Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
    //            //opponentRb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

    //            Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
    //            Vector3 knockbackForceVector = new Vector3(knockbackDirection.x, 0, knockbackDirection.z);
    //            opponentRb.AddForce(knockbackForceVector * knockbackForce, ForceMode.Impulse);
    //            opponentRb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);

    //            activateKnockback = false;
    //        }
    //    }
    //}
}
