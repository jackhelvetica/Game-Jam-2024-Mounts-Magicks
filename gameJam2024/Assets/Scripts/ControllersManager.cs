using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

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

        // Get all connected gamepads
        var gamepads = Gamepad.all;

        // Iterate over each gamepad
        foreach (var gamepad in gamepads)
        {
            // Get the device name of the gamepad
            string deviceName = gamepad.device.name;

            // Check if the device name starts with "DualShock4GamepadHID"
            if (deviceName.StartsWith("DualShock4GamepadHID"))
            {
                activePS4Controllers.Add(deviceName);
            }
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

    public void RemoveController(string controllerName)
    {
        if (activePS4Controllers.Contains(controllerName))
        {
            activePS4Controllers.Remove(controllerName);
            Debug.Log("Controller removed: " + controllerName);
        }
        else
        {
            Debug.LogWarning("Controller '" + controllerName + "' not found in activePS4Controllers.");
        }
    }

    public int GetControllerIndex(string controllerName)
    {
        if (string.IsNullOrEmpty(controllerName))
        {
            Debug.LogWarning("Controller name is null or empty.");
            return -1; // Or return any appropriate value indicating error
        }

        // Use IndexOf to find the index of the controller name
        int index = activePS4Controllers.IndexOf(controllerName);

        // Print the controller name and its index for verification
        if (index >= 0)
        {
            Debug.Log("Index of controller '" + controllerName + "' is: " + index);
        }
        else
        {
            Debug.LogWarning("Controller '" + controllerName + "' not found in activePS4Controllers.");
        }

        return index;
    }
}
