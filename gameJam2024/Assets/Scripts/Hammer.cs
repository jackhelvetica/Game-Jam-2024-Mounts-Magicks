using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hammer : MonoBehaviour
{
    public GameObject mount;

    private void OnTriggerEnter(Collider other)
    {
        if (KnockBack.activateKnockback)
        {
            Rigidbody opponentRb = other.GetComponent<Rigidbody>();
            Mount opponentMount = other.GetComponent<Mount>();

            if (opponentRb != null)
            {
                float distance = Vector3.Distance(mount.transform.position, other.transform.position);
                Debug.Log("Distance is " + distance);
                Debug.Log("My position is " + mount.transform.position);
                Debug.Log("Opponent's position is " + other.transform.position);
                if (distance <= 6f)
                {
                    Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                    opponentMount.GetCriticalKnockbacked(knockbackDirection);
                }
                if (distance > 6f)
                {
                    Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                    opponentMount.GetNormalKnockbacked(knockbackDirection);
                }               
            }

            KnockBack.activateKnockback = false;
        }
    }
}
