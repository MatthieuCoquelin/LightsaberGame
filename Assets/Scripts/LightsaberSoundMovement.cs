using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;


public class LightsaberSoundMovement : MonoBehaviour
{
    [SerializeField] private XRGrabInteractable m_swordGrip;
    [SerializeField] private BladeAnimation m_bladeAnimation;
    
    [SerializeField] private HandManager m_handSelectEntered;
    [SerializeField] private XRBaseInteractable m_interactable;

    [SerializeField] private AudioSource m_audioSource;
    [SerializeField] private AudioClip m_audioclip;

    InputDevice m_deviceLeftController;
    InputDevice m_deviceRightController;

    private Vector3 m_inputVelocity_LeftController;
    private Vector3 m_inputVelocity_RightController;

    private bool m_isGrabbed;
    private float m_volumeIntensity;

    private void Awake()
    {
        m_isGrabbed = false;
        m_volumeIntensity = 0.1f;
    }

    private void OnEnable()
    {
        m_swordGrip.selectEntered.AddListener(BeginInput);
        m_swordGrip.selectExited.AddListener(StopInput);
        m_interactable.selectEntered.AddListener(m_handSelectEntered.GetHandTag);
    }

    private void OnDisable()
    {
        m_swordGrip.selectEntered.RemoveListener(BeginInput);
        m_swordGrip.selectExited.RemoveListener(StopInput);
        m_interactable.selectEntered.RemoveListener(m_handSelectEntered.GetHandTag);
    }

    private void BeginInput(SelectEnterEventArgs arg0)
    {
        m_isGrabbed = true;
    }

    private void StopInput(SelectExitEventArgs arg0)
    {
        m_isGrabbed = false;
    }

    private void GetLeftController()
    {
        m_deviceLeftController = InputDevices.GetDeviceAtXRNode(XRNode.LeftHand);
    }
    
    private void GetRightController()
    {
        m_deviceRightController = InputDevices.GetDeviceAtXRNode(XRNode.RightHand);
    }
    
    void Start()
    {
        if(!m_deviceLeftController.isValid)
            GetLeftController();
        if (!m_deviceRightController.isValid)
            GetRightController();
    }

    private void Update()
    {
        if (!m_deviceLeftController.isValid)
            GetLeftController();
        if (!m_deviceRightController.isValid)
            GetRightController();
        

        if ((m_deviceLeftController.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out m_inputVelocity_LeftController) 
            && m_inputVelocity_LeftController.magnitude > 6 && m_isGrabbed && m_bladeAnimation.IsOn && m_handSelectEntered.HandTag == "LeftHand") || (
            m_deviceRightController.TryGetFeatureValue(CommonUsages.deviceAngularVelocity, out m_inputVelocity_RightController)
            && m_inputVelocity_RightController.magnitude > 6 && m_isGrabbed && m_bladeAnimation.IsOn && m_handSelectEntered.HandTag == "RightHand"))
        {
            m_audioSource.PlayOneShot(m_audioclip, m_volumeIntensity);
        }
    }

    public bool IsGrabbed
    {
        get { return m_isGrabbed; }
    }

}
