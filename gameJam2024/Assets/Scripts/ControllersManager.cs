using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllersManager : MonoBehaviour
{
    // List to store active PS4 controllers
    public List<string> activePS4Controllers = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        // Detect currently connected controllers
        string[] joystickNames = Input.GetJoystickNames();

        // Clear the list before updating
        activePS4Controllers.Clear();

        for (int i = 0; i < joystickNames.Length; i++)
        {

            // Add the PS4 controller to the list
            activePS4Controllers.Add(joystickNames[i]);
        }

        // Display the active PS4 controllers
        DisplayActiveControllers();
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    void DisplayActiveControllers()
    {
        // Print the active controllers to the console
        Debug.Log("Active PS4 Controllers:");

        for (int i = 0; i < activePS4Controllers.Count; i++)
        {
            Debug.Log(activePS4Controllers[i]);
        }
    }

    public int GetControllerIndex(string controllerName)
    {
        return activePS4Controllers.IndexOf(controllerName);
    }
}
