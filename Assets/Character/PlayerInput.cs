// GENERATED AUTOMATICALLY FROM 'Assets/Character/PlayerInput.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerInput : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInput()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInput"",
    ""maps"": [
        {
            ""name"": ""CharacterControls"",
            ""id"": ""fd776bca-69a2-43fb-928a-e76d9f7d5120"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Value"",
                    ""id"": ""c75a231b-d5ef-4735-a648-757214b7ae5b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Run"",
                    ""type"": ""Button"",
                    ""id"": ""62fe94f0-6473-44ae-9c58-cef5f7d1537e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""Value"",
                    ""id"": ""b1322c7a-5d3b-4671-9509-28f1e0801f3a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PickupObj"",
                    ""type"": ""Button"",
                    ""id"": ""ccf7b1c6-5989-4e06-aa27-aee4ff970641"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PhoneControlLeft"",
                    ""type"": ""Button"",
                    ""id"": ""5bdcb03d-cffe-42e3-b228-0d822a0289da"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PhoneControlRight"",
                    ""type"": ""Button"",
                    ""id"": ""6181da2c-7c8f-40c6-9e98-044dbf8fb3d0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""572ea612-ae74-45a3-8c45-2716d8d9fd09"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASD Keys"",
                    ""id"": ""f63d39a6-01e4-4cf0-9fe8-948519d0ab6a"",
                    ""path"": ""2DVector(mode=1)"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""1757915a-466e-490f-8e52-becbcf623804"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""2d08b0ae-8891-40dc-91b8-cf2370ed0662"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bf303c81-7c3f-4526-80d6-ad98e86f2f5f"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""9fd20696-6947-4ced-b1eb-56ca1a05d2a0"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3e54d477-f584-4301-9af7-832d51e33d46"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d3d31bf4-da38-46e5-85bc-c5bbeea0b1d1"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Run"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3ff9965b-c03c-4953-9f78-c6c9615450c4"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""StickDeadzone"",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ee986f9b-8416-485f-b9cb-f348ddd9e89d"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7c33492f-65ae-4c05-975b-f147ae898f57"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PickupObj"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""86209b54-100d-4323-8da3-765fa350d3e6"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PhoneControlLeft"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f50348fe-dc6b-418b-8457-ecbaf70193b1"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PhoneControlRight"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // CharacterControls
        m_CharacterControls = asset.FindActionMap("CharacterControls", throwIfNotFound: true);
        m_CharacterControls_Movement = m_CharacterControls.FindAction("Movement", throwIfNotFound: true);
        m_CharacterControls_Run = m_CharacterControls.FindAction("Run", throwIfNotFound: true);
        m_CharacterControls_Look = m_CharacterControls.FindAction("Look", throwIfNotFound: true);
        m_CharacterControls_PickupObj = m_CharacterControls.FindAction("PickupObj", throwIfNotFound: true);
        m_CharacterControls_PhoneControlLeft = m_CharacterControls.FindAction("PhoneControlLeft", throwIfNotFound: true);
        m_CharacterControls_PhoneControlRight = m_CharacterControls.FindAction("PhoneControlRight", throwIfNotFound: true);
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

    // CharacterControls
    private readonly InputActionMap m_CharacterControls;
    private ICharacterControlsActions m_CharacterControlsActionsCallbackInterface;
    private readonly InputAction m_CharacterControls_Movement;
    private readonly InputAction m_CharacterControls_Run;
    private readonly InputAction m_CharacterControls_Look;
    private readonly InputAction m_CharacterControls_PickupObj;
    private readonly InputAction m_CharacterControls_PhoneControlLeft;
    private readonly InputAction m_CharacterControls_PhoneControlRight;
    public struct CharacterControlsActions
    {
        private @PlayerInput m_Wrapper;
        public CharacterControlsActions(@PlayerInput wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_CharacterControls_Movement;
        public InputAction @Run => m_Wrapper.m_CharacterControls_Run;
        public InputAction @Look => m_Wrapper.m_CharacterControls_Look;
        public InputAction @PickupObj => m_Wrapper.m_CharacterControls_PickupObj;
        public InputAction @PhoneControlLeft => m_Wrapper.m_CharacterControls_PhoneControlLeft;
        public InputAction @PhoneControlRight => m_Wrapper.m_CharacterControls_PhoneControlRight;
        public InputActionMap Get() { return m_Wrapper.m_CharacterControls; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(CharacterControlsActions set) { return set.Get(); }
        public void SetCallbacks(ICharacterControlsActions instance)
        {
            if (m_Wrapper.m_CharacterControlsActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnMovement;
                @Run.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
                @Run.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
                @Run.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnRun;
                @Look.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLook;
                @Look.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLook;
                @Look.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnLook;
                @PickupObj.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnPickupObj;
                @PickupObj.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnPickupObj;
                @PickupObj.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnPickupObj;
                @PhoneControlLeft.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnPhoneControlLeft;
                @PhoneControlLeft.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnPhoneControlLeft;
                @PhoneControlLeft.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnPhoneControlLeft;
                @PhoneControlRight.started -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnPhoneControlRight;
                @PhoneControlRight.performed -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnPhoneControlRight;
                @PhoneControlRight.canceled -= m_Wrapper.m_CharacterControlsActionsCallbackInterface.OnPhoneControlRight;
            }
            m_Wrapper.m_CharacterControlsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Run.started += instance.OnRun;
                @Run.performed += instance.OnRun;
                @Run.canceled += instance.OnRun;
                @Look.started += instance.OnLook;
                @Look.performed += instance.OnLook;
                @Look.canceled += instance.OnLook;
                @PickupObj.started += instance.OnPickupObj;
                @PickupObj.performed += instance.OnPickupObj;
                @PickupObj.canceled += instance.OnPickupObj;
                @PhoneControlLeft.started += instance.OnPhoneControlLeft;
                @PhoneControlLeft.performed += instance.OnPhoneControlLeft;
                @PhoneControlLeft.canceled += instance.OnPhoneControlLeft;
                @PhoneControlRight.started += instance.OnPhoneControlRight;
                @PhoneControlRight.performed += instance.OnPhoneControlRight;
                @PhoneControlRight.canceled += instance.OnPhoneControlRight;
            }
        }
    }
    public CharacterControlsActions @CharacterControls => new CharacterControlsActions(this);
    public interface ICharacterControlsActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnRun(InputAction.CallbackContext context);
        void OnLook(InputAction.CallbackContext context);
        void OnPickupObj(InputAction.CallbackContext context);
        void OnPhoneControlLeft(InputAction.CallbackContext context);
        void OnPhoneControlRight(InputAction.CallbackContext context);
    }
}
