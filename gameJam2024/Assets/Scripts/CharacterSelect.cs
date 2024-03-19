using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CharacterSelect : MonoBehaviour
{
    public Image p1BGempty;
    public Image p2BGempty;
    public Image p3BGempty;
    public Image p4BGempty;

    public List<Image> playersList = new List<Image>();

    void Start()
    {
        p1BGempty = GetComponent<Image>();
        p2BGempty = GetComponent<Image>();
        p3BGempty = GetComponent<Image>();
        p4BGempty = GetComponent<Image>();
    }


    void Update()
    {
        
    }
}
