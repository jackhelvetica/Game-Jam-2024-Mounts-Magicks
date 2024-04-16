using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy : MonoBehaviour
{
    //I'm sorry I need to brute force
    void Start()
    {
        Destroy(GameObject.Find("GameManager"));
    }
}
