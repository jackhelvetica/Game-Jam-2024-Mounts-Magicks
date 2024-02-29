//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/Scripts/PlayerControls.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @PlayerControls : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""863efd72-89b0-46aa-bca3-a16340144912"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""PassThrough"",
                    ""id"": ""5d35b830-721a-44fe-b480-f2b32efc86fe"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""JoinLobby"",
                    ""type"": ""Button"",
                    ""id"": ""47a1f951-d0ff-4507-ac3d-a0378978fe61"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""ExitLobby"",
                    ""type"": ""Button"",
                    ""id"": ""00014ce8-62ff-4732-807a-1215643a42b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""IsReady"",
                    ""type"": ""Button"",
                    ""id"": ""094d7757-6c3b-4d80-b193-11310fafa4e9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""One Modifier"",
                    ""id"": ""03b792e5-fe99-44eb-983b-c3210e2b2c4d"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JoinLobby"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Modifier"",
                    ""id"": ""c6c1b590-368e-4b72-b827-a0df864237ba"",
                    ""path"": ""<DualShockGamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JoinLobby"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Binding"",
                    ""id"": ""e133155a-6c0b-4f0d-a458-76d02010f978"",
                    ""path"": ""<DualShockGamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""JoinLobby"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""0a4c08f5-2b8b-4d8e-abf4-797caceff3d9"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Controller"",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7f3ed8f5-8e2a-4c3f-8a26-3489a3c38162"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ExitLobby"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0162c071-9290-460c-b335-524c2fdb5707"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""IsReady"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Controller"",
            ""bindingGroup"": ""Controller"",
            ""devices"": [
                {
                    ""devicePath"": ""<DualShockGamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_Movement = m_Player.FindAction("Movement", throwIfNotFound: true);
        m_Player_JoinLobby = m_Player.FindAction("JoinLobby", throwIfNotFound: true);
        m_Player_ExitLobby = m_Player.FindAction("ExitLobby", throwIfNotFound: true);
        m_Player_IsReady = m_Player.FindAction("IsReady", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_Movement;
    private readonly InputAction m_Player_JoinLobby;
    private readonly InputAction m_Player_ExitLobby;
    private readonly InputAction m_Player_IsReady;
    public struct PlayerActions
    {
        private @PlayerControls m_Wrapper;
        public PlayerActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Player_Movement;
        public InputAction @JoinLobby => m_Wrapper.m_Player_JoinLobby;
        public InputAction @ExitLobby => m_Wrapper.m_Player_ExitLobby;
        public InputAction @IsReady => m_Wrapper.m_Player_IsReady;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnMovement;
                @JoinLobby.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJoinLobby;
                @JoinLobby.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJoinLobby;
                @JoinLobby.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnJoinLobby;
                @ExitLobby.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExitLobby;
                @ExitLobby.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExitLobby;
                @ExitLobby.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnExitLobby;
                @IsReady.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnIsReady;
                @IsReady.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnIsReady;
                @IsReady.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnIsReady;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @JoinLobby.started += instance.OnJoinLobby;
                @JoinLobby.performed += instance.OnJoinLobby;
                @JoinLobby.canceled += instance.OnJoinLobby;
                @ExitLobby.started += instance.OnExitLobby;
                @ExitLobby.performed += instance.OnExitLobby;
                @ExitLobby.canceled += instance.OnExitLobby;
                @IsReady.started += instance.OnIsReady;
                @IsReady.performed += instance.OnIsReady;
                @IsReady.canceled += instance.OnIsReady;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_ControllerSchemeIndex = -1;
    public InputControlScheme ControllerScheme
    {
        get
        {
            if (m_ControllerSchemeIndex == -1) m_ControllerSchemeIndex = asset.FindControlSchemeIndex("Controller");
            return asset.controlSchemes[m_ControllerSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnJoinLobby(InputAction.CallbackContext context);
        void OnExitLobby(InputAction.CallbackContext context);
        void OnIsReady(InputAction.CallbackContext context);
    }
}