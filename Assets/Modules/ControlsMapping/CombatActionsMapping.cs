// GENERATED AUTOMATICALLY FROM 'Assets/Modules/ControlsMapper/CombatActionsMapping.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @CombatActionsMapping : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @CombatActionsMapping()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""CombatActionsMapping"",
    ""maps"": [
        {
            ""name"": ""SkillNotSelectedMapping"",
            ""id"": ""ff2f0c2f-6e48-4767-9004-92bddfbbe00d"",
            ""actions"": [],
            ""bindings"": []
        },
        {
            ""name"": ""SkillSelectedMapping"",
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
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Tap(duration=0.5)"",
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
        // SkillNotSelectedMapping
        m_SkillNotSelectedMapping = asset.FindActionMap("SkillNotSelectedMapping", throwIfNotFound: true);
        // SkillSelectedMapping
        m_SkillSelectedMapping = asset.FindActionMap("SkillSelectedMapping", throwIfNotFound: true);
        m_SkillSelectedMapping_ResolveSkill = m_SkillSelectedMapping.FindAction("ResolveSkill", throwIfNotFound: true);
        m_SkillSelectedMapping_CancelSkill = m_SkillSelectedMapping.FindAction("CancelSkill", throwIfNotFound: true);
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

    // SkillNotSelectedMapping
    private readonly InputActionMap m_SkillNotSelectedMapping;
    private ISkillNotSelectedMappingActions m_SkillNotSelectedMappingActionsCallbackInterface;
    public struct SkillNotSelectedMappingActions
    {
        private @CombatActionsMapping m_Wrapper;
        public SkillNotSelectedMappingActions(@CombatActionsMapping wrapper) { m_Wrapper = wrapper; }
        public InputActionMap Get() { return m_Wrapper.m_SkillNotSelectedMapping; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SkillNotSelectedMappingActions set) { return set.Get(); }
        public void SetCallbacks(ISkillNotSelectedMappingActions instance)
        {
            if (m_Wrapper.m_SkillNotSelectedMappingActionsCallbackInterface != null)
            {
            }
            m_Wrapper.m_SkillNotSelectedMappingActionsCallbackInterface = instance;
            if (instance != null)
            {
            }
        }
    }
    public SkillNotSelectedMappingActions @SkillNotSelectedMapping => new SkillNotSelectedMappingActions(this);

    // SkillSelectedMapping
    private readonly InputActionMap m_SkillSelectedMapping;
    private ISkillSelectedMappingActions m_SkillSelectedMappingActionsCallbackInterface;
    private readonly InputAction m_SkillSelectedMapping_ResolveSkill;
    private readonly InputAction m_SkillSelectedMapping_CancelSkill;
    public struct SkillSelectedMappingActions
    {
        private @CombatActionsMapping m_Wrapper;
        public SkillSelectedMappingActions(@CombatActionsMapping wrapper) { m_Wrapper = wrapper; }
        public InputAction @ResolveSkill => m_Wrapper.m_SkillSelectedMapping_ResolveSkill;
        public InputAction @CancelSkill => m_Wrapper.m_SkillSelectedMapping_CancelSkill;
        public InputActionMap Get() { return m_Wrapper.m_SkillSelectedMapping; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(SkillSelectedMappingActions set) { return set.Get(); }
        public void SetCallbacks(ISkillSelectedMappingActions instance)
        {
            if (m_Wrapper.m_SkillSelectedMappingActionsCallbackInterface != null)
            {
                @ResolveSkill.started -= m_Wrapper.m_SkillSelectedMappingActionsCallbackInterface.OnResolveSkill;
                @ResolveSkill.performed -= m_Wrapper.m_SkillSelectedMappingActionsCallbackInterface.OnResolveSkill;
                @ResolveSkill.canceled -= m_Wrapper.m_SkillSelectedMappingActionsCallbackInterface.OnResolveSkill;
                @CancelSkill.started -= m_Wrapper.m_SkillSelectedMappingActionsCallbackInterface.OnCancelSkill;
                @CancelSkill.performed -= m_Wrapper.m_SkillSelectedMappingActionsCallbackInterface.OnCancelSkill;
                @CancelSkill.canceled -= m_Wrapper.m_SkillSelectedMappingActionsCallbackInterface.OnCancelSkill;
            }
            m_Wrapper.m_SkillSelectedMappingActionsCallbackInterface = instance;
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
    public SkillSelectedMappingActions @SkillSelectedMapping => new SkillSelectedMappingActions(this);
    public interface ISkillNotSelectedMappingActions
    {
    }
    public interface ISkillSelectedMappingActions
    {
        void OnResolveSkill(InputAction.CallbackContext context);
        void OnCancelSkill(InputAction.CallbackContext context);
    }
}
