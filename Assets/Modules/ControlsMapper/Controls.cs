// GENERATED AUTOMATICALLY FROM 'Assets/Modules/ControlsMapper/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Default"",
            ""id"": ""ff2f0c2f-6e48-4767-9004-92bddfbbe00d"",
            ""actions"": [
                {
                    ""name"": ""SelectSkill"",
                    ""type"": ""Button"",
                    ""id"": ""df85fcd5-911e-44d3-9d1a-dacc163ab04b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f8477182-f638-45ac-b2b1-3080dc20f407"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""SkillSelected"",
            ""id"": ""b026dee7-a018-44b5-90d2-a8ad6a4c4570"",
            ""actions"": [
                {
                    ""name"": ""ResolveSkill"",
                    ""type"": ""Button"",
                    ""id"": ""e9449a80-63c8-466b-adea-c1a2942cb3f9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""CancelSkill"",
                    ""type"": ""Button"",
                    ""id"": ""cf418325-301b-4599-8132-44ba4467d4b8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e365a54d-4009-4cf5-840b-873531cf58b9"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""ResolveSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d99da081-d0cc-4499-bb97-93c741d24cd7"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""CancelSkill"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Default
        m_Default = asset.FindActionMap("Default", throwIfNotFound: true);
        m_Default_SelectSkill = m_Default.FindAction("SelectSkill", throwIfNotFound: true);
        // SkillSelected
        m_SkillSelected = asset.FindActionMap("SkillSelected", throwIfNotFound: true);
        m_SkillSelected_ResolveSkill = m_SkillSelected.FindAction("ResolveSkill", throwIfNotFound: true);
        m_SkillSelected_CancelSkill = m_SkillSelected.FindAction("CancelSkill", throwIfNotFound: true);
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

    // Default
    private readonly InputActionMap m_Default;
    private IDefaultActions m_DefaultActionsCallbackInterface;
    private readonly InputAction m_Default_SelectSkill;
    public struct DefaultActions
    {
        private @Controls m_Wrapper;
        public DefaultActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @SelectSkill => m_Wrapper.m_Default_SelectSkill;
        public InputActionMap Get() { return m_Wrapper.m_Default; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(DefaultActions set) { return set.Get(); }
        public void SetCallbacks(IDefaultActions instance)
        {
            if (m_Wrapper.m_DefaultActionsCallbackInterface != null)
            {
                @SelectSkill.started -= m_Wrapper.m_DefaultActionsCallbackInterface.OnSelectSkill;
                @SelectSkill.performed -= m_Wrapper.m_DefaultActionsCallbackInterface.OnSelectSkill;
                @SelectSkill.canceled -= m_Wrapper.m_DefaultActionsCallbackInterface.OnSelectSkill;
            }
            m_Wrapper.m_DefaultActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SelectSkill.started += instance.OnSelectSkill;
                @SelectSkill.performed += instance.OnSelectSkill;
                @SelectSkill.canceled += instance.OnSelectSkill;
            }
        }
    }
    public DefaultActions @Default => new DefaultActions(this);

    // SkillSelected
    private readonly InputActionMap m_SkillSelected;
    private ISkillSelectedActions m_SkillSelectedActionsCallbackInterface;
    private readonly InputAction m_SkillSelected_ResolveSkill;
    private readonly InputAction m_SkillSelected_CancelSkill;
    public struct SkillSelectedActions
    {
        private @Controls m_Wrapper;
        public SkillSelectedActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @ResolveSkill => m_Wrapper.m_SkillSelected_ResolveSkill;
        public InputAction @CancelSkill => m_Wrapper.m_SkillSelected_CancelSkill;
        public InputActionMap Get() { return m_Wrapper.m_SkillSelected; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SkillSelectedActions set) { return set.Get(); }
        public void SetCallbacks(ISkillSelectedActions instance)
        {
            if (m_Wrapper.m_SkillSelectedActionsCallbackInterface != null)
            {
                @ResolveSkill.started -= m_Wrapper.m_SkillSelectedActionsCallbackInterface.OnResolveSkill;
                @ResolveSkill.performed -= m_Wrapper.m_SkillSelectedActionsCallbackInterface.OnResolveSkill;
                @ResolveSkill.canceled -= m_Wrapper.m_SkillSelectedActionsCallbackInterface.OnResolveSkill;
                @CancelSkill.started -= m_Wrapper.m_SkillSelectedActionsCallbackInterface.OnCancelSkill;
                @CancelSkill.performed -= m_Wrapper.m_SkillSelectedActionsCallbackInterface.OnCancelSkill;
                @CancelSkill.canceled -= m_Wrapper.m_SkillSelectedActionsCallbackInterface.OnCancelSkill;
            }
            m_Wrapper.m_SkillSelectedActionsCallbackInterface = instance;
            if (instance != null)
            {
                @ResolveSkill.started += instance.OnResolveSkill;
                @ResolveSkill.performed += instance.OnResolveSkill;
                @ResolveSkill.canceled += instance.OnResolveSkill;
                @CancelSkill.started += instance.OnCancelSkill;
                @CancelSkill.performed += instance.OnCancelSkill;
                @CancelSkill.canceled += instance.OnCancelSkill;
            }
        }
    }
    public SkillSelectedActions @SkillSelected => new SkillSelectedActions(this);
    public interface IDefaultActions
    {
        void OnSelectSkill(InputAction.CallbackContext context);
    }
    public interface ISkillSelectedActions
    {
        void OnResolveSkill(InputAction.CallbackContext context);
        void OnCancelSkill(InputAction.CallbackContext context);
    }
}
