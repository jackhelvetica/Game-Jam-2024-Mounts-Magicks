using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;

public class Sword : MonoBehaviour
{
    public GameObject mount;
    public GameObject rider;
    private bool assignMountRef = false;

    //Colour
    public Material blueMat;

    //Knockback
    public bool detectKnockback = false;    

    private void Start()
    {
        //Assign Mount to Sword
        if (rider.CompareTag("Rider1"))
        {
            mount = GameObject.FindGameObjectWithTag("Mount1");
        }
    }

    private void Update()
    {
        //Assign Mount to Sword
        if (rider.CompareTag("Rider2") && !assignMountRef && GameObject.FindGameObjectWithTag("Mount2") != null)
        {
            mount = GameObject.FindGameObjectWithTag("Mount2");

            //Colour
            GetComponent<Renderer>().material = blueMat;
            assignMountRef = true;
        }
        else
        {
            return;
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (KnockBack.activateKnockback)
        {
            Rigidbody opponentRb = other.GetComponent<Rigidbody>();
            Mount opponentMount = other.GetComponent<Mount>();

            if (opponentRb != null && other.tag != mount.tag && other.tag != "Sphere") //prevent hitting yourself
            {
                float distance = Vector3.Distance(mount.transform.position, other.transform.position);
                //Debug.Log("Distance is " + distance);
                //Debug.Log("My position is " + mount.transform.position);
                //Debug.Log("Opponent's position is " + other.transform.position);
                //Debug.Log("Detect knockback mount is " + detectKnockback);
                //if (distance <= 6f && detectKnockback && !other.GetComponent<Mount>().isInvincible)

                if (distance <= 6f && !other.GetComponent<Mount>().isInvincible)
                {
                    Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                    opponentMount.GetCriticalKnockbacked(knockbackDirection);                    

                    other.GetComponent<VisualEffect>().Play();
                    FindObjectOfType<AudioManagerScript>().Play("Critical Hit");
                }
                else if (distance > 6f && !other.GetComponent<Mount>().isInvincible)
                {
                    Vector3 knockbackDirection = (other.transform.position - transform.position).normalized;
                    opponentMount.GetNormalKnockbacked(knockbackDirection);

                    //vfx
                    FindObjectOfType<AudioManagerScript>().Play("Normal Hit");
                }
            }
            else
            {
                return;
            }

            KnockBack.activateKnockback = false;
        }
    }   
}
