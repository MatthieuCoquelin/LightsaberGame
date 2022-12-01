using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Counter : MonoBehaviour
{
    [SerializeField] private AudioSource m_audioSource = null;
    [SerializeField] private AudioClip m_audioClip = null;

    [SerializeField] private BladeAnimation m_bladeAnimation = null;
    [SerializeField] private HandManager m_handSelectEntered = null;
    [SerializeField] private LightsaberSoundMovement m_lightsaber = null;
    [SerializeField] private XRBaseInteractable m_interactable = null;

    [SerializeField] private XRBaseController m_leftController = null;
    [SerializeField] private XRBaseController m_rightController = null;

    private void OnEnable()
    {
        CounterListener.OnCounterEnter += CounterProjectile;
        CounterListener.OnCounterHappen += CounterSound;
        CounterListener.OnCounterHappen += VibrateController;
        m_interactable.selectEntered.AddListener(m_handSelectEntered.GetHandTag);
    }

    private void OnDisable()
    {
        CounterListener.OnCounterEnter -= CounterProjectile;
        CounterListener.OnCounterHappen -= CounterSound;
        CounterListener.OnCounterHappen -= VibrateController;
        m_interactable.selectEntered.RemoveListener(m_handSelectEntered.GetHandTag);
    }


    private void CounterProjectile(Collider collider)
    {
        collider.gameObject.GetComponent<Rigidbody>().velocity *= -1;
        collider.gameObject.GetComponent<Projectile>().IsCountered = true;
    }

    private void CounterSound()
    {
        m_audioSource.PlayOneShot(m_audioClip);
    }

    private void VibrateController()
    {
        if (m_bladeAnimation.IsOn && m_lightsaber.IsGrabbed && m_handSelectEntered.HandTag == "LeftHand")
            m_leftController.SendHapticImpulse(0.2f, 0.2f);
        if (m_bladeAnimation.IsOn && m_lightsaber.IsGrabbed && m_handSelectEntered.HandTag == "RightHand")
            m_rightController.SendHapticImpulse(0.2f, 0.2f);
    }
}
