using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterSwitcher : MonoBehaviour
{
    int index = 0;
    [SerializeField] List<GameObject> playersList = new List<GameObject>();
    PlayerInputManager playerInputManager;
    
    // Start is called before the first frame update
    private void Start()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
        //index = Random.Range(0, playersList.Count);
        playerInputManager.playerPrefab = playersList[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchNextSpawnCharacter(PlayerInput input)
    {
        Debug.Log("Index = " + index);
        //index = Random.Range(0, playersList.Count);
        index++;
        if (index >= 3)
        {
            index = 3;
        }
        playerInputManager.playerPrefab = playersList[index];
    }
}
