//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.3
//     from Packages/nl.jessestam.tower-defence/Input/InputActions.inputactions
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
    public partial class @InputActions : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @InputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""InputActions"",
    ""maps"": [
        {
            ""name"": ""Main"",
            ""id"": ""ef38aafa-fa73-4af3-a840-54cd9047147b"",
            ""actions"": [
                {
                    ""name"": ""LeftClick"",
                    ""type"": ""Button"",
                    ""id"": ""c08eed66-1669-4c85-ae3e-310fb51ecbfb"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""RightClick"",
                    ""type"": ""Button"",
                    ""id"": ""132e2f5f-aa8f-440a-8283-fd9ee9c0f39e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""48c26760-ea03-44fd-a95c-7f8aea6543c7"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""MouseDelta"",
                    ""type"": ""Value"",
                    ""id"": ""0ef7746a-dcb3-481b-9446-6a75856dac0b"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Drag"",
                    ""type"": ""Button"",
                    ""id"": ""00d3eb3e-21a7-4739-a049-4e4dd3073986"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""fb1ebb05-36ea-4ee5-a587-b2b8d1a66cdf"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""LeftClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5459d6c9-dd94-443f-b106-e592aefba0c8"",
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
                    ""id"": ""da797ff7-2b50-4bf8-9cd1-85fd7cad92d2"",
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
                    ""id"": ""f1d06383-c4ec-4fe4-a93b-c8b2ca1a0c4b"",
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
                    ""id"": ""84c21b2f-c706-4a93-bd03-8a121e8006e8"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Drag"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""a6859faf-44ed-4233-a4e3-cc7493bc975f"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseDelta"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c11e0eb8-736a-468a-9b6a-3ee43b1f7ea5"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""RightClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""CameraMove"",
            ""id"": ""aee72a07-c351-4f21-9051-52e9120be9d0"",
            ""actions"": [
                {
                    ""name"": ""Vertical"",
                    ""type"": ""Value"",
                    ""id"": ""c8c4745d-42b1-427a-b917-44aa5517d56e"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Horizontal"",
                    ""type"": ""Value"",
                    ""id"": ""72c66e07-b601-4067-a4ab-8925b164b2e2"",
                    ""expectedControlType"": ""Integer"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""106fa77f-12e3-4860-a6b7-10765a0b8604"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""53f4503a-d5db-4ea7-8ae3-3843a2991e8b"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""e939ef70-dfe8-44c9-a42b-dcf6b144e182"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Vertical"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""1D Axis"",
                    ""id"": ""756cecdb-05b8-413f-899c-51ff71e45dd8"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""5aefe0da-c9ba-4717-b16d-4767a8fe1dc8"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""d87ab4c0-c710-47b6-9c62-9ecfcc8cad59"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Horizontal"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        },
        {
            ""name"": ""UI"",
            ""id"": ""29e7f54c-b8ae-4a4c-8158-06340aa29138"",
            ""actions"": [
                {
                    ""name"": ""New action"",
                    ""type"": ""Button"",
                    ""id"": ""c78811e5-57b3-4ddc-bc10-0dd9031c60f0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""OpenPauseMenu"",
                    ""type"": ""Button"",
                    ""id"": ""9b439693-b1cc-4b35-8606-89058970fea7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""7955aaa3-1d18-4ba5-a3e7-1dd49e084069"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""New action"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b636e7a3-93a1-403b-8e5d-f55ffbfe178d"",
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
            m_Main_LeftClick = m_Main.FindAction("LeftClick", throwIfNotFound: true);
            m_Main_RightClick = m_Main.FindAction("RightClick", throwIfNotFound: true);
            m_Main_MousePosition = m_Main.FindAction("MousePosition", throwIfNotFound: true);
            m_Main_MouseDelta = m_Main.FindAction("MouseDelta", throwIfNotFound: true);
            m_Main_Drag = m_Main.FindAction("Drag", throwIfNotFound: true);
            // CameraMove
            m_CameraMove = asset.FindActionMap("CameraMove", throwIfNotFound: true);
            m_CameraMove_Vertical = m_CameraMove.FindAction("Vertical", throwIfNotFound: true);
            m_CameraMove_Horizontal = m_CameraMove.FindAction("Horizontal", throwIfNotFound: true);
            // UI
            m_UI = asset.FindActionMap("UI", throwIfNotFound: true);
            m_UI_Newaction = m_UI.FindAction("New action", throwIfNotFound: true);
            m_UI_OpenPauseMenu = m_UI.FindAction("OpenPauseMenu", throwIfNotFound: true);
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
        private readonly InputAction m_Main_LeftClick;
        private readonly InputAction m_Main_RightClick;
        private readonly InputAction m_Main_MousePosition;
        private readonly InputAction m_Main_MouseDelta;
        private readonly InputAction m_Main_Drag;
        public struct MainActions
        {
            private @InputActions m_Wrapper;
            public MainActions(@InputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @LeftClick => m_Wrapper.m_Main_LeftClick;
            public InputAction @RightClick => m_Wrapper.m_Main_RightClick;
            public InputAction @MousePosition => m_Wrapper.m_Main_MousePosition;
            public InputAction @MouseDelta => m_Wrapper.m_Main_MouseDelta;
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
                    @LeftClick.started -= m_Wrapper.m_MainActionsCallbackInterface.OnLeftClick;
                    @LeftClick.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnLeftClick;
                    @LeftClick.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnLeftClick;
                    @RightClick.started -= m_Wrapper.m_MainActionsCallbackInterface.OnRightClick;
                    @RightClick.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnRightClick;
                    @RightClick.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnRightClick;
                    @MousePosition.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
                    @MousePosition.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
                    @MousePosition.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMousePosition;
                    @MouseDelta.started -= m_Wrapper.m_MainActionsCallbackInterface.OnMouseDelta;
                    @MouseDelta.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnMouseDelta;
                    @MouseDelta.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnMouseDelta;
                    @Drag.started -= m_Wrapper.m_MainActionsCallbackInterface.OnDrag;
                    @Drag.performed -= m_Wrapper.m_MainActionsCallbackInterface.OnDrag;
                    @Drag.canceled -= m_Wrapper.m_MainActionsCallbackInterface.OnDrag;
                }
                m_Wrapper.m_MainActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @LeftClick.started += instance.OnLeftClick;
                    @LeftClick.performed += instance.OnLeftClick;
                    @LeftClick.canceled += instance.OnLeftClick;
                    @RightClick.started += instance.OnRightClick;
                    @RightClick.performed += instance.OnRightClick;
                    @RightClick.canceled += instance.OnRightClick;
                    @MousePosition.started += instance.OnMousePosition;
                    @MousePosition.performed += instance.OnMousePosition;
                    @MousePosition.canceled += instance.OnMousePosition;
                    @MouseDelta.started += instance.OnMouseDelta;
                    @MouseDelta.performed += instance.OnMouseDelta;
                    @MouseDelta.canceled += instance.OnMouseDelta;
                    @Drag.started += instance.OnDrag;
                    @Drag.performed += instance.OnDrag;
                    @Drag.canceled += instance.OnDrag;
                }
            }
        }
        public MainActions @Main => new MainActions(this);

        // CameraMove
        private readonly InputActionMap m_CameraMove;
        private ICameraMoveActions m_CameraMoveActionsCallbackInterface;
        private readonly InputAction m_CameraMove_Vertical;
        private readonly InputAction m_CameraMove_Horizontal;
        public struct CameraMoveActions
        {
            private @InputActions m_Wrapper;
            public CameraMoveActions(@InputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Vertical => m_Wrapper.m_CameraMove_Vertical;
            public InputAction @Horizontal => m_Wrapper.m_CameraMove_Horizontal;
            public InputActionMap Get() { return m_Wrapper.m_CameraMove; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(CameraMoveActions set) { return set.Get(); }
            public void SetCallbacks(ICameraMoveActions instance)
            {
                if (m_Wrapper.m_CameraMoveActionsCallbackInterface != null)
                {
                    @Vertical.started -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnVertical;
                    @Vertical.performed -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnVertical;
                    @Vertical.canceled -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnVertical;
                    @Horizontal.started -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnHorizontal;
                    @Horizontal.performed -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnHorizontal;
                    @Horizontal.canceled -= m_Wrapper.m_CameraMoveActionsCallbackInterface.OnHorizontal;
                }
                m_Wrapper.m_CameraMoveActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Vertical.started += instance.OnVertical;
                    @Vertical.performed += instance.OnVertical;
                    @Vertical.canceled += instance.OnVertical;
                    @Horizontal.started += instance.OnHorizontal;
                    @Horizontal.performed += instance.OnHorizontal;
                    @Horizontal.canceled += instance.OnHorizontal;
                }
            }
        }
        public CameraMoveActions @CameraMove => new CameraMoveActions(this);

        // UI
        private readonly InputActionMap m_UI;
        private IUIActions m_UIActionsCallbackInterface;
        private readonly InputAction m_UI_Newaction;
        private readonly InputAction m_UI_OpenPauseMenu;
        public struct UIActions
        {
            private @InputActions m_Wrapper;
            public UIActions(@InputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Newaction => m_Wrapper.m_UI_Newaction;
            public InputAction @OpenPauseMenu => m_Wrapper.m_UI_OpenPauseMenu;
            public InputActionMap Get() { return m_Wrapper.m_UI; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UIActions set) { return set.Get(); }
            public void SetCallbacks(IUIActions instance)
            {
                if (m_Wrapper.m_UIActionsCallbackInterface != null)
                {
                    @Newaction.started -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                    @Newaction.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                    @Newaction.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnNewaction;
                    @OpenPauseMenu.started -= m_Wrapper.m_UIActionsCallbackInterface.OnOpenPauseMenu;
                    @OpenPauseMenu.performed -= m_Wrapper.m_UIActionsCallbackInterface.OnOpenPauseMenu;
                    @OpenPauseMenu.canceled -= m_Wrapper.m_UIActionsCallbackInterface.OnOpenPauseMenu;
                }
                m_Wrapper.m_UIActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Newaction.started += instance.OnNewaction;
                    @Newaction.performed += instance.OnNewaction;
                    @Newaction.canceled += instance.OnNewaction;
                    @OpenPauseMenu.started += instance.OnOpenPauseMenu;
                    @OpenPauseMenu.performed += instance.OnOpenPauseMenu;
                    @OpenPauseMenu.canceled += instance.OnOpenPauseMenu;
                }
            }
        }
        public UIActions @UI => new UIActions(this);
        public interface IMainActions
        {
            void OnLeftClick(InputAction.CallbackContext context);
            void OnRightClick(InputAction.CallbackContext context);
            void OnMousePosition(InputAction.CallbackContext context);
            void OnMouseDelta(InputAction.CallbackContext context);
            void OnDrag(InputAction.CallbackContext context);
        }
        public interface ICameraMoveActions
        {
            void OnVertical(InputAction.CallbackContext context);
            void OnHorizontal(InputAction.CallbackContext context);
        }
        public interface IUIActions
        {
            void OnNewaction(InputAction.CallbackContext context);
            void OnOpenPauseMenu(InputAction.CallbackContext context);
        }
    }
}