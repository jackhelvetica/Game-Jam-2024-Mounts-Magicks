using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    private float knockbackForce = 10f;
    private float upwardForce = 10f;
    public static bool activateKnockback;
    void Start()
    {
        activateKnockback = false;
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (activateKnockback)
        {
            Rigidbody opponentRb = other.GetComponent<Rigidbody>();

            if (opponentRb != null)
            {
                //Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                //opponentRb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);

                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                Vector3 knockbackForceVector = new Vector3(knockbackDirection.x, 0, knockbackDirection.z);
                opponentRb.AddForce(knockbackForceVector * knockbackForce, ForceMode.Impulse);
                opponentRb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);

                activateKnockback = false;
            }
        }                
    }
}
