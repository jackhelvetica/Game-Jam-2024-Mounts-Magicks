using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnockBack : MonoBehaviour
{
    public float knockbackForce = 100f;
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
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                opponentRb.AddForce(knockbackDirection * knockbackForce, ForceMode.Impulse);
                activateKnockback = false;
            }
        }        
    }
}
