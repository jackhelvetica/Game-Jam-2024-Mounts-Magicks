using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    public Slider manaBar;
    public static bool useMana;
    public float maxMana = 30f;
    public float manaAmount = 0f;
    public float manaCharge = 10f;
    public float waitTime = 4.5f;

    void Start()
    {
    }

    void Update()
    {
        ManaRegen();
    }

    public void ManaRegen()
    {
        if (manaBar.value < maxMana)
        {
            manaBar.value += waitTime * Time.deltaTime;
        }
        else if (manaBar.value == maxMana)
        {
            manaBar.value = maxMana;
        }
    }

    public void UseMana()
    {
        if (manaBar.value > manaCharge)
        {
            manaBar.value -= manaCharge;
            useMana = true;
        }
        
    }
    
}
