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
        index = Random.Range(0, playersList.Count);
        playerInputManager.playerPrefab = playersList[index];
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchNextSpawnCharacter(PlayerInput input)
    {
        index = Random.Range(0, playersList.Count);
        playerInputManager.playerPrefab = playersList[index];
    }
}
