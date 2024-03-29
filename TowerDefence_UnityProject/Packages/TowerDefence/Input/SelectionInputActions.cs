//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.3
//     from Packages/nl.jessestam.tower-defence/Input/SelectionInputActions.inputactions
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

namespace TowerDefence.Input
{
    public partial class @SelectionInputActions : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @SelectionInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""SelectionInputActions"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""0484f14b-799a-405f-b2fc-06aed8c52079"",
            ""actions"": [
                {
                    ""name"": ""Click"",
                    ""type"": ""Button"",
                    ""id"": ""f3ba09de-a29b-47bc-8c96-07703b84cbf2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""d5afb6ea-4fa4-4d06-8464-a8362ecc8dc8"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Drag"",
                    ""type"": ""Button"",
                    ""id"": ""f393907b-9fc3-4d05-945b-375552b45c15"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""bf8c4eba-3451-43c1-a6d8-28c8eb821dd0"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Click"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""603bbbfa-0092-404d-82f0-4cb8efaa8bf1"",
                    ""path"": ""<Pointer>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Ctrl+LMB"",
                    ""id"": ""7151e802-a711-4f05-b8cd-4a8e169f6b30"",
                    ""path"": ""OneModifier"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drag"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""modifier"",
                    ""id"": ""66b5246b-1443-4104-a013-5feba8b337cf"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""binding"",
                    ""id"": ""56672909-e4c4-4843-9a2f-2d62b1518e1c"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Main
            m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
            m_Main_Click = m_Main.FindAction("Click", throwIfNotFound: true);
            m_Main_MousePosition = m_Main.FindAction("MousePosition", throwIfNotFound: true);
            m_Main_Drag = m_Main.FindAction("Drag", throwIfNotFound: true);
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

        // Main
        private readonly InputActionMap m_Main;
        private IMainActions m_MainActionsCallbackInterface;
        private readonly InputAction m_Main_Click;
        private readonly InputAction m_Main_MousePosition;
        private readonly InputAction m_Main_Drag;
        public struct MainActions
        {
            private @SelectionInputActions m_Wrapper;
            public MainActions(@SelectionInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Click => m_Wrapper.m_Main_Click;
            public InputAction @MousePosition => m_Wrapper.m_Main_MousePosition;
            public InputAction @Drag => m_Wrapper.m_Main_Drag;
            public InputActionMap Get() { return m_Wrapper.m_Main; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
            public void SetCallbacks(IMainActions instance)
            {
                if (m_Wrapper.m_MainActionsCallbackInterface != null)
                {
                    @Click.started -= m_Wrapper.m_MainActionsCallbackInterface.OnClick;
                    @Click.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnClick;
                    @Click.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnClick;
                    @MousePosition.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
                    @MousePosition.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
                    @MousePosition.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
                    @Drag.started -= m_Wrapper.m_MainActionsCallbackInterface.OnDrag;
                    @Drag.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnDrag;
                    @Drag.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnDrag;
                }
                m_Wrapper.m_MainActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Click.started += instance.OnClick;
                    @Click.performed += instance.OnClick;
                    @Click.canceled += instance.OnClick;
                    @MousePosition.started += instance.OnMousePosition;
                    @MousePosition.performed += instance.OnMousePosition;
                    @MousePosition.canceled += instance.OnMousePosition;
                    @Drag.started += instance.OnDrag;
                    @Drag.performed += instance.OnDrag;
                    @Drag.canceled += instance.OnDrag;
                }
            }
        }
        public MainActions @Main => new MainActions(this);
        public interface IMainActions
        {
            void OnClick(InputAction.CallbackContext context);
            void OnMousePosition(InputAction.CallbackContext context);
            void OnDrag(InputAction.CallbackContext context);
        }
    }
}
