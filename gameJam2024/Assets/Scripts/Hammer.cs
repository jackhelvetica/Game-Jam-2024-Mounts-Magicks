using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (KnockBack.activateKnockback)
        {
            Rigidbody opponentRb = other.GetComponent<Rigidbody>();
            Mount mount = other.GetComponent<Mount>();

            if (opponentRb != null)
            {
                Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                mount.GetKnockbacked(knockbackDirection);
            }

            KnockBack.activateKnockback = false;
        }
        
    }
}
