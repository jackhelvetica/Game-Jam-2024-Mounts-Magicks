using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : MonoBehaviour
{
    private GameObject mount;
    public GameObject rider;

    //Colour
    public Material blueMat;

    //Knockback
    public bool detectKnockback = false;

    //static doesn't work because you have 2 mounts and 2 riders!

    private void Start()
    {
        //Assign Mount to Hammer
        if (rider.CompareTag("Rider1"))
        {
            mount = GameObject.FindGameObjectWithTag("Mount1");
        }
        else if (rider.CompareTag("Rider2"))
        {
            mount = GameObject.FindGameObjectWithTag("Mount2");

            //Colour
            GetComponent<Renderer>().material = blueMat;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (KnockBack.activateKnockback)
        {
            Rigidbody opponentRb = other.GetComponent<Rigidbody>();
            Mount opponentMount = other.GetComponent<Mount>();

            if (opponentRb != null && other.tag != mount.tag) //prevent hitting yourself
            {
                float distance = Vector3.Distance(mount.transform.position, other.transform.position);
                //Debug.Log("Distance is " + distance);
                //Debug.Log("My position is " + mount.transform.position);
                //Debug.Log("Opponent's position is " + other.transform.position);
                Debug.Log("Detect knockback mount is " + detectKnockback);

                if (distance <= 9f && detectKnockback)
                {
                    Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                    opponentMount.GetCriticalKnockbacked(knockbackDirection);
                    FindObjectOfType<AudioManagerScript>().Play("Critical Hit");
                }
                else if (distance > 0f)
                {
                    Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                    opponentMount.GetNormalKnockbacked(knockbackDirection);
                    FindObjectOfType<AudioManagerScript>().Play("Normal Hit");
                }           
            }

            KnockBack.activateKnockback = false;
        }
    }
}
