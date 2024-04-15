using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DashBar : MonoBehaviour
{
    public Slider dashBar;
    public float maxDash = 10f;
    public float dashAmount = 0f;
    public float dashCharge = 10f;
    public float waitTime = 2f;

    void Start()
    {
    }

    void Update()
    {
        DashRegen();
    }

    public void DashRegen()
    {
        if (dashBar.value < maxDash)
        {
            dashBar.value += waitTime * Time.deltaTime;
        }
        else if (dashBar.value == maxDash)
        {
            dashBar.value = maxDash;
        }
    }

    public void UseDash()
    {
        if (dashBar.value > dashCharge)
        {
            dashBar.value -= dashCharge;
        }
    }

    public void RefillDash()
    {
        dashBar.value = maxDash;
    }

}
