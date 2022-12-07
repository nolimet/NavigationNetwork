//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.3
//     from Packages/nl.jessestam.tower-defence/Input/UIInputActions.inputactions
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
    public partial class @UIInputActions : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @UIInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""UIInputActions"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""a88c65eb-7c6c-4ec7-8bd1-48e448b8f677"",
            ""actions"": [
                {
                    ""name"": ""OpenPauseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""81235f51-3d75-4fdf-8985-9a1ee817b450"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""2438a24d-e7ee-40a5-b766-7e71c84528ff"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""OpenPauseMenu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
            // Main
            m_Main = asset.FindActionMap("Main", throwIfNotFound: true);
            m_Main_OpenPauseMenu = m_Main.FindAction("OpenPauseMenu", throwIfNotFound: true);
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
        private readonly InputAction m_Main_OpenPauseMenu;
        public struct MainActions
        {
            private @UIInputActions m_Wrapper;
            public MainActions(@UIInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @OpenPauseMenu => m_Wrapper.m_Main_OpenPauseMenu;
            public InputActionMap Get() { return m_Wrapper.m_Main; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(MainActions set) { return set.Get(); }
            public void SetCallbacks(IMainActions instance)
            {
                if (m_Wrapper.m_MainActionsCallbackInterface != null)
                {
                    @OpenPauseMenu.started -= m_Wrapper.m_MainActionsCallbackInterface.OnOpenPauseMenu;
                    @OpenPauseMenu.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnOpenPauseMenu;
                    @OpenPauseMenu.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnOpenPauseMenu;
                }
                m_Wrapper.m_MainActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @OpenPauseMenu.started += instance.OnOpenPauseMenu;
                    @OpenPauseMenu.performed += instance.OnOpenPauseMenu;
                    @OpenPauseMenu.canceled += instance.OnOpenPauseMenu;
                }
            }
        }
        public MainActions @Main => new MainActions(this);
        public interface IMainActions
        {
            void OnOpenPauseMenu(InputAction.CallbackContext context);
        }
    }
}
